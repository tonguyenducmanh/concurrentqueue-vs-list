using System.Collections.Concurrent;

namespace ConcurrenQueueTest
{
    /// <summary>
    /// Test làm việc đa luồng với kiểu dữ liệu ConcurrentQueue
    /// </summary>
    public class ConcurrentQueueTest
    {
        #region Multi Thread

        private ConcurrentQueue<Guid> _listDatabaseId = new ConcurrentQueue<Guid>();
        
        private Task _taskUpdateDB = null;

        private bool _isRunningUpdateDB = false;

        private void UpdateDB()
        {
            if (_isRunningUpdateDB)
            {
                return;
            }
            _isRunningUpdateDB = true;

            if (_taskUpdateDB == null || _taskUpdateDB.IsCompleted)
            {
                _taskUpdateDB = new Task(() =>
                {
                    try
                    {
                        while (_listDatabaseId.Count > 0)
                        {
                            Guid dbId;
                            _listDatabaseId.TryDequeue(out dbId);
                            Console.WriteLine(dbId);
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
            _taskUpdateDB.Start();
        }

        /// <summary>
        /// làm việc đa luồng
        /// </summary>
        public void TestUpdateDB()
        {
            for (int i = 0; i < 3; i++)
            {
                _listDatabaseId.Enqueue(Guid.NewGuid());
            }

            UpdateDB();

            Thread.Sleep(5000);
            TestUpdateDB();
        }

        #endregion
    }
}
