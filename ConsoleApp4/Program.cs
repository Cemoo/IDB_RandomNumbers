using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

namespace IDB_RandomNumbers
{
    class Program
    {
        private const int V = 1000;
        static List<string> list = new List<string>();
        static List<string> rndList = new List<string>();
        static void Main(string[] args)
        {
            var list = ListOfNumbers();
            if (list.Count>0)
            {
                List<string> randomList = GetRandomFor(V);
                //Console.WriteLine((from a in randomList where a == "5" select a).Count());
                WriteToExcel();
                Console.ReadLine();
            }
 
        }

        public static List<string> ListOfNumbers()
        {
            
            for (double i = 0; i <= 16; i = i + 0.1)
            {
                list.Add(Truncate(i));
                Console.WriteLine(Truncate(i));
            }
            return list;
        } 
        
        public static string Truncate(double num)
        {
            var arrCount = num.ToString().Split('.');
            if (arrCount.Length > 1) {
                var numBeforeComma = num.ToString().Split('.')[0];
                var numAfterComma = num.ToString().Split('.')[1];
                numAfterComma = numAfterComma.Substring(0, 1);
                return numBeforeComma + "." + numAfterComma;
            }
            else
            {
                return num.ToString();
            }
        }

        public static List<string> GetRandomFor(int times)
        {
            var rnd = new Random();
            for (int i = 0; i < times; i++)
            {
                int index = rnd.Next(list.Count);
                rndList.Add(list[index]);
            }
            
            return rndList;
        }

        public static void WriteToExcel()
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("Excel is not properly installed!!");
                return;
            }

            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


            for (int i = 1; i < V; i++)
            {
                xlWorkSheet.Cells[i, 1] = "Two";
            }

            xlWorkBook.SaveAs("c:\\Users\\Cemal\\Desktop\\randomNumbers.xlsx", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

        }
    }
}
