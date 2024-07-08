using ConcurrenQueueTest;

namespace ListTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConcurrentQueueTest concurrentQueueTest = new ConcurrentQueueTest();

            // làm việc đa luồng
            concurrentQueueTest.TestUpdateDB();
        }
    }
}