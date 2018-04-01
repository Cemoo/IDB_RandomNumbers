using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetDateTimeArray
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime startDate = new DateTime(2011, 01, 01);
            DateTime finishDate = new DateTime(2013, 01, 01);

            List<string> list = new List<string>();
            var finish = false;
            list.Add(startDate.ToString("yyyy-MM-dd HH:mm:ss"));
            while (!finish)
            {
                if (startDate.AddHours(1)!= finishDate)
                {
                    startDate = startDate.AddHours(1);
                    list.Add(startDate.ToString("yyyy-MM-dd HH:mm:ss"));
                } else
                {
                    //list.Add(startDate.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss"));
                    finish = true;
                }
                Console.WriteLine(startDate.ToString("yyyy-MM-dd HH:mm:ss"));
            }

            Console.Read();
            
        }
    }
}
