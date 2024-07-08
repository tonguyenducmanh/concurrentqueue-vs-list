using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListTest
{
    /// <summary>
    /// Test việc làm việc đa luồng với kiểu dữ liệu List
    /// </summary>
    public class ListTest
    {
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

        /// <summary>
        /// làm việc đa luồng
        /// </summary>
        public void Run()
        {

        }
    }
}
