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

        private Task _taskUpdateDB = null;

        private bool _isRunningUpdateDB = false;

        private void UpdateDB()
        {
            if (_isRunningUpdateDB) 
            {
                return;
            }
            _isRunningUpdateDB = true;
            
            if(_taskUpdateDB == null || _taskUpdateDB.IsCompleted)
            {
                _taskUpdateDB = new Task(() =>
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
            _taskUpdateDB.Start();
        }

        /// <summary>
        /// làm việc đa luồng
        /// </summary>
        public void TestUpdateDB()
        {
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
