using System.Collections.Concurrent;

namespace ConcurrenQueueTest
{
    /// <summary>
    /// Test làm việc đa luồng với kiểu dữ liệu ConcurrentQueue
    /// </summary>
    public class ConcurrentQueueTest
    {
        #region Multi Thread

        private ConcurrentQueue<Guid> _concurrentQueueDBIds = new ConcurrentQueue<Guid>();

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
                    while (_concurrentQueueDBIds.Count > 0)
                    {
                        Guid dbId;
                        _concurrentQueueDBIds.TryDequeue(out dbId);
                        // luôn phải có try catch khi làm việc đa luồng
                        try
                        {
                            Console.WriteLine(dbId);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{nameof(ConcurrentQueueTest)}{nameof(UpdateDB)} {ex.Message}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{nameof(ConcurrentQueueTest)}{nameof(UpdateDB)} {ex.Message}");
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
            // giả lập trường hợp thêm 1 database nào concurrentQueue
            // sau đó 1 thread khác đọc concurrentQueue database và in ra màn hình
            // dùng đệ quy để thấy 2 việc chạy song song với nhau
            for (int i = 0; i < 3; i++)
            {
                _concurrentQueueDBIds.Enqueue(Guid.NewGuid());
            }

            UpdateDB();

            Thread.Sleep(5000);
            TestUpdateDB();
        }

        #endregion
    }
}
