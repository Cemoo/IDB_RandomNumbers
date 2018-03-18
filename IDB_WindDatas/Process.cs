using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDB_WindDatas.Interface;
namespace IDB_WindDatas
{
    public class Process
    {
        IRead read = null;
        bool isExcel = false;
        
        public Process(IRead _read)
        {
            read = _read;
        }

        public List<WindData> Read(string path)
        {
            return read.ReadFromExcel(path);
        }
    }
}
