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

        private void btnRead_Click(object sender, EventArgs e)
        {
            if (_path != "")
            {
                pro = new Process(CreateRead());
                list = pro.Read(_path);

                foreach (var item in list)
                {
                    Console.WriteLine(FixDate(item));
                    InsertDB(item);
                    lblStatus.Text = "Data added to DB";
                }
            }
          
        }

        private void Main_Load(object sender, EventArgs e)
        {
            
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
                w.Date = FixDate(item);
                w.Speed = item.windSpeed;
                w.Direction = item.direction;
                db.Winds.InsertOnSubmit(w);
                db.SubmitChanges();
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
    }
}
