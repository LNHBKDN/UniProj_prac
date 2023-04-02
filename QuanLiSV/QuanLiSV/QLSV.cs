using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiSV
{
    internal class QLSV
    {
        private QLSV() { }

        private static QLSV _Instance = null;

        public static QLSV Instance
        {
            get {
                if (_Instance == null) {
                    _Instance = new QLSV();
                }
                return _Instance;
            }
            private set { }
        }

        public List<ClassSV> getAllSV()
        {
            List<ClassSV> list = new List<ClassSV>();
            foreach(DataRow sv in DataBase.Instance.dt.Rows )
            {
                list.Add(new ClassSV
                {
                    MSSV = sv.ItemArray[0].ToString(),
                    Name = sv.ItemArray[1].ToString(),
                    Gender = Convert.ToBoolean(sv.ItemArray[2]),
                    lopSH = sv.ItemArray[3].ToString(),
                    NgaySinh = sv.ItemArray[4].ToString(),
                    DTB = Convert.ToDouble(sv.ItemArray[5]),
                });
            }
            return list;
        }
        public void RemoveSV(string mssv)
        {
            foreach (DataRow row in DataBase.Instance.dt.Rows)
            {
                if (row.ItemArray[0].ToString() == mssv)
                {
                    DataBase.Instance.dt.Rows.Remove(row);
                    break;
                }
            }
        }
        public List<string> getAllLopSH() {
            List<string> lopSHs = new List<string>();
            foreach (ClassSV sv in this.getAllSV()) {
                if (!lopSHs.Contains(sv.lopSH)) {
                    lopSHs.Add(sv.lopSH);
                }
            }
            return lopSHs;
        }

    }
}
