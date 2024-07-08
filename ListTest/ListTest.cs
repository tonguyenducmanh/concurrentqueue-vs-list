namespace ListTest
{
    /// <summary>
    /// Test việc làm việc đa luồng với kiểu dữ liệu List
    /// </summary>
    public class ListTest
    {
        #region Single Thread
        /// <summary>
        /// làm việc tuần tự với list
        /// </summary>
        public void TestListRunManual()
        {
            // chuẩn bị biến
            List<Guid> list = new List<Guid>();

            // chuẩn bị dữ liệu
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    list.Add(Guid.NewGuid());
                }
                // giả lập việc thread chạy lâu
                Thread.Sleep(2000);
            }
            // kết quả 
            foreach (Guid guid in list)
            {
                Console.WriteLine(guid);
            }
        }

        #endregion

        #region Multi Thread

        private List<Guid> _listDatabaseId = new List<Guid>();

        private object _lockDatabaseObj = new object();

        private bool _isRunningUpdateDB = false;

        private void UpdateDB()
        {
            // nếu đã có task run rồi thì cứ chạy tiếp vòng while trong task đó
            if (_isRunningUpdateDB)
            {
                return;
            }
            _isRunningUpdateDB = true;

            // chưa có thì run task mới
            Task.Run(() =>
            {
                try
                {
                    while (_listDatabaseId.Count > 0)
                    {
                        lock (_lockDatabaseObj) 
                        {
                            Console.WriteLine(_listDatabaseId.FirstOrDefault());
                            _listDatabaseId.RemoveAt(0);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{nameof(ListTest)}{nameof(UpdateDB)} {ex.Message}");
                }
                finally
                {
                    _isRunningUpdateDB = false;
                }
            });
        }

        /// <summary>
        /// làm việc đa luồng
        /// </summary>
        public void TestUpdateDB()
        {
            // giả lập trường hợp thêm 1 database nào list
            // sau đó 1 thread khác đọc list database và in ra màn hình
            // dùng đệ quy để thấy 2 việc chạy song song với nhau
            for (int i = 0; i < 3; i++)
            {
                // phải gọi lock thủ công
                lock (_lockDatabaseObj) 
                { 
                    _listDatabaseId.Add(Guid.NewGuid());
                }
            }

            UpdateDB();

            Thread.Sleep(5000);
            TestUpdateDB();
        }

        #endregion
    }
}
