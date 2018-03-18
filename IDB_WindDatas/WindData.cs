using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDB_WindDatas
{
    public class WindData
    {
        public string year { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public string timeUTC{ get; set; }
        public string windSpeed { get; set; }
        public string direction { get; set; }

 
        public List<WindData> DataList(WindData data)
        {
            var list = new List<WindData>();
            list.Add(data);
            return list;
        }
    }
}
