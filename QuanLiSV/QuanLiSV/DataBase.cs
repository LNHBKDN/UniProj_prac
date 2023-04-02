using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiSV
{
    internal class DataBase
    {
        private DataTable _dt ;
        public DataTable dt
        {
            get
            {
                return _dt;
            }
            private set { }
        }
        private static DataBase _Instance;
        public static DataBase Instance
        {
            get
            {
                if(_Instance == null)
                {
                    _Instance = new DataBase();
                }
                return _Instance;
            }
            private set { }
        }
        public DataBase()
        {

            _dt = new DataTable();
            _dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn {ColumnName = "MSSV",DataType = typeof(string)},
                new DataColumn {ColumnName = "Name",DataType = typeof(string)},
                new DataColumn {ColumnName = "Gioi Tinh",DataType = typeof(bool)},
                new DataColumn {ColumnName = "Lop Sinh Hoat",DataType = typeof(string)},
                new DataColumn {ColumnName = "Ngay Sinh", DataType = typeof(string)},
                new DataColumn {ColumnName = "Diem Trung Binh", DataType = typeof(double)}
            });
            _dt.Rows.Add("101", "Le", true, "21DT1", "20/03/2003", 9.5);
            _dt.Rows.Add("102", "Ngoc", false, "21DT2", "21/02/2003", 9);
            _dt.Rows.Add("103", "Hanh", true, "21DT1", "12/06/2003", 5);
            _dt.Rows.Add("104", "Tran", false, "21DT4", "28/11/2003", 9);
            _dt.Rows.Add("105", "Nguyen", true, "21DT2", "23/09/2003", 6.5);
            _dt.Rows.Add("106", "Ly", true, "21DT3", "14/10/2003", 7.5);
            _dt.Rows.Add("107", "Ngo", false, "21DT3", "30/03/2003", 8.5);

        }
    }
}
