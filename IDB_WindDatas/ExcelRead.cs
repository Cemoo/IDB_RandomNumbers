using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDB_WindDatas.Interface;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.IO;
using System.Data;
using FastExcel;

namespace IDB_WindDatas
{
    public class ExcelRead : IRead
    {
        public WindData windData;
        public List<Row> list = new List<Row>();
        public List<WindData> listData = new List<WindData>();

        public void ReadFromDB()
        {
            throw new NotImplementedException();
        }

        public List<WindData> ReadFromExcel(string path)
        {

            // Get the input file paths
            FileInfo inputFile = new FileInfo(path);

            //Create a worksheet
            FastExcel.Worksheet worksheet = null;

            // Create an instance of Fast Excel
            using (FastExcel.FastExcel fastExcel = new FastExcel.FastExcel(inputFile, true))
            {
                // Read the rows using worksheet name
                worksheet = fastExcel.Read("Sheet1");
 
                worksheet = fastExcel.Read(1);
                list = worksheet.Rows.ToArray().ToList();
                foreach (var item in list)
                {
                    windData = new WindData();
                    foreach (var cellItem in item.Cells)
                    {
                        if (item.RowNumber != 1 )
                        {
                            switch (cellItem.ColumnNumber)
                            {
                                case 1:
                                    windData.year = cellItem.Value.ToString();
                                    break;
                                case 2:
                                    windData.month = cellItem.Value.ToString();
                                    break;
                                case 3:
                                    windData.day = cellItem.Value.ToString();
                                    break;
                                case 4:
                                    windData.timeUTC = cellItem.Value.ToString();
                                    break;
                                case 5:
                                    windData.windSpeed = cellItem.Value.ToString();
                                    break;

                                default:
                                    break;
                            }
                        }  
                       
                    }
                    if (item.RowNumber != 1)
                    {
                        listData.Add(FixDirection(windData));
                    }
                }

            }

            return listData;

        }

        public void ReadFromText(string path)
        {
            throw new NotImplementedException();
        }

        public WindData FixDirection(WindData data)
        {
            if (data.windSpeed != "")
            {
                if (data.windSpeed != null)
                {
                    string[] arr = data.windSpeed.Split('/');
                    data.windSpeed = arr[0].Trim();
                    data.direction = "/" + arr[1];
                }
           
            }

            return data;
        }
    }
}
