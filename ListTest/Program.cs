namespace ListTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ListTest listTest = new ListTest();
            // làm việc đơn luồng
            // listTest.TestListRunManual();

            // làm việc đa luồng
            listTest.Run();
        }
    }
}