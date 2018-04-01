using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IDB_WindDatas.Interface;
using System.IO;
using System.Threading;

namespace IDB_WindDatas
{
    public partial class Main : Form
    {
        WindDataDataContext db = new WindDataDataContext();

        public Main()
        {
            InitializeComponent();
        }

        Process pro;
        private string _path = "";
        List<WindData> list = new List<WindData>();
        List<WindData> realList = new List<WindData>();
        List<WindData> mainList = new List<WindData>();

        List<Thread> thList = new List<Thread>();

        private void btnRead_Click(object sender, EventArgs e)
        {
            Fetch();
        }

        public void Fetch()
        {
            if (_path != "")
            {
                pro = new Process(CreateRead());
                list = pro.Read(_path);

                //foreach (var item in list)
                //{
                //    Console.WriteLine(FixDate(item));
                //    var str = FixDate(item);
                //    InsertDB(item);
                //    lblStatus.Text = "Data added to DB";  
                //}

                if (mainList.Count != 0)
                {
                    foreach (var item in mainList)
                    {
                        foreach (var listItem in list)
                        {
                            var str = FixDate(listItem);
                            if (item.day == str)
                            {
                                item.direction = listItem.direction;
                                item.windSpeed = listItem.windSpeed;
                            }

                        }

                    }

                    foreach (var item in mainList)
                    {
                        InsertDB(item);
                    }
                }

                
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Thread thMain = new Thread(FillMainList);
            //FillMainList();
            thMain.IsBackground = true;
            thMain.Start();
            thList.Add(thMain);
        }

        private IRead CreateRead()
        {
            IRead read;
            read = new ExcelRead(); 

            return read;
        }


        private void btnBrowse_Click(object sender, EventArgs e)
        {
            _path = OpenFile();
            lblStatus.Text = _path;
        }   

        private string OpenFile()
        {
            int size = -1;
            DialogResult result = ofd.ShowDialog(); // Show the dialog.
            string file = "";
            if (result == DialogResult.OK) // Test result.
            {
                file = ofd.FileName;
                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                }
                catch (IOException)
                {
                }
            }
            return file;
        }

        public void InsertDB(WindData item)
        {
            if (list.Count != 0)
            {
                Wind w = new Wind();
                w.Date = item.day;
                w.Speed = item.windSpeed;
                w.Direction = item.direction;
                db.Winds.InsertOnSubmit(w);
                //db.SubmitChanges();
            }
        }

        public string FixDate(WindData item) {
            string time = "";
            string month = "";
            string day = "";
            string year = "";


            year = item.year.Replace("20", "").Trim();

            if (int.Parse(item.month) < 10)
            {
                month = "0" + item.month;
            } else {
                month = item.month;
            }

            if (int.Parse(item.day) < 10)
            {
                day = "0" + item.day;
            }
            else
            {
                day = item.day;
            }


            if (int.Parse(item.timeUTC) < 10)
            {
                time = "0" + item.timeUTC + ":00";
            }
            else
            {
                time = item.timeUTC + ":00";
            }

            return day + "-" + month + "-" + year + " " + time;
        }

        public void FillMainList()
        {
            DateTime startDate = new DateTime(2011, 01, 01);
            DateTime finishDate = new DateTime(2013, 01, 01);

            var finish = false;
           // mainList.Add(startDate.ToString("dd-MM-yy HH:mm"));
            mainList.Add(new WindData
            {
                day = startDate.ToString("dd-MM-yy HH:mm")
            });
            while (!finish)
            {
                if (startDate.AddHours(1) != finishDate)
                {
                    startDate = startDate.AddHours(1);
                    mainList.Add(new WindData
                    {
                        day = startDate.ToString("dd-MM-yy HH:mm")
                    });
                }
                else
                {
                    //list.Add(startDate.AddHours(1).ToString("yyyy-MM-dd HH:mm:ss"));
                    finish = true;
                }
                Console.WriteLine(startDate.ToString("yyyy-MM-dd HH:mm"));
            }
        }



    }
}
