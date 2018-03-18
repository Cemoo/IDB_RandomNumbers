using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDB_WindDatas.Interface
{
    public interface IRead
    {
        List<WindData> ReadFromExcel(string path);
        void ReadFromText(string path);
        void ReadFromDB();
    }
}
