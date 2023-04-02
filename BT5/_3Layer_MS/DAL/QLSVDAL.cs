using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _3Layer_MS.DTO;
namespace _3Layer_MS.DAL
{
    internal class QLSVDAL
    {
        public SV1 getSVByDataRow(DataRow row)
        {
            return new SV1
            {
                MSSV = row["MSSV"].ToString(),
                NameSV = row["NAMESV"].ToString(),
                Gender = Convert.ToBoolean(row["GENDER"].ToString()),
                NgaySinh = Convert.ToDateTime(row["NGAYSINH"].ToString()),
                DTB = Convert.ToDouble(row["DTB"].ToString()),
                ID_Lop = Convert.ToInt32(row["ID_LOP"].ToString())
            };               
        }
        public LSH getLSHByDataRow(DataRow row)
        {
            return new LSH
            {
                ID_Lop = Convert.ToInt32(row["ID_LOP"].ToString()),
                LopName = row["NAMELOP"].ToString()
            };
        }
        public List<LSH> getAllLSH()
        {
            List<LSH> res = new List<LSH>();
            foreach (DataRow row in DBHelper.Instance.getRecord("select * from LSH").Rows)
            {
                res.Add(getLSHByDataRow(row));
            }
            return res;
        }
        public DataTable getSVbyIDLopDAL(int id)
        {
            DataTable dt = new DataTable();
            string query = "SELECT SV1.MSSV, SV1.NAMESV, LSH.NAMELOP, SV1.GENDER, SV1.NGAYSINH, SV1.DTB\r\nFROM SV1\r\nINNER JOIN LSH  ON SV1.ID_LOP = LSH.ID_LOP\r\nWHERE SV1.ID_LOP = @id;";
            SqlCommand cmd = new SqlCommand(query);
            cmd.Parameters.AddWithValue("@id", id);
            dt = DBHelper.Instance.getRecord(cmd);
            return dt;
        }
        public void addSVDAL(SV1 sv)
        {   
            string query = "";
            if (sv == null)
            {
                return;
            }
            else
            {
                try
                {
                    query = "INSERT INTO SV1 (MSSV, NAMESV , GENDER, NGAYSINH, DTB, ID_LOP) VALUES (@mssv,@ten, @gender,@day,@diem,@idlop)";
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("mssv", sv.MSSV);
                    cmd.Parameters.AddWithValue("@ten", sv.NameSV);
                    cmd.Parameters.AddWithValue("@gender", sv.Gender);
                    cmd.Parameters.AddWithValue("@day", sv.NgaySinh);
                    cmd.Parameters.AddWithValue("@diem", sv.DTB);
                    cmd.Parameters.AddWithValue("@idlop", sv.ID_Lop);

                    DBHelper.Instance.excuteDB(cmd);
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
            
        }
        public void updateSVDAL(SV1 sv)
        {
            string query = "";
            DBHelper.Instance.getRecord(query);
        }
        public void delSVDAL(string mssv)
        {
            try
            {
                string query = "DELETE FROM SV1 WHERE MSSV = @mssv";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@mssv", mssv);
                DBHelper.Instance.excuteDB(cmd);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        
        }
        public DataTable setDataDAL()
        {
            DataTable dt = new DataTable();
            string query = "SELECT SV1.MSSV, SV1.NAMESV, LSH.NAMELOP, SV1.GENDER, SV1.NGAYSINH, SV1.DTB\r\nFROM SV1\r\nINNER JOIN LSH  ON SV1.ID_LOP = LSH.ID_LOP;";
            dt = DBHelper.Instance.getRecord(query);
            return dt;
        }
        public DataTable getSVbyNameDAL(string name,string lop) 
        {
            DataTable dt = new DataTable();
            string query = "";
            if(lop == "All")
            {
                query = "SELECT SV1.MSSV, SV1.NAMESV, LSH.NAMELOP, SV1.GENDER, SV1.NGAYSINH, SV1.DTB\r\nFROM SV1\r\nINNER JOIN LSH  ON SV1.ID_LOP = LSH.ID_LOP\r\nWHERE SV1.NAMESV LIKE @ten";
            }
            else
            {
                query = "SELECT SV1.MSSV, SV1.NAMESV, LSH.NAMELOP, SV1.GENDER, SV1.NGAYSINH, SV1.DTB\r\nFROM SV1\r\nINNER JOIN LSH  ON SV1.ID_LOP = LSH.ID_LOP\r\nWHERE SV1.NAMESV LIKE @ten AND LSH.NAMELOP LIKE @lop;";
            }       
            SqlCommand cmd = new SqlCommand(query);            
            cmd.Parameters.AddWithValue("@ten","%"+name + "%");
            cmd.Parameters.AddWithValue("@lop", "%" + lop + "%");
            return dt = DBHelper.Instance.getRecord(cmd);
        }
        public DataTable getColumnNameDAL()
        {
            string query = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SV1' AND COLUMN_NAME != 'ID_LOP'";
            DataTable dt = DBHelper.Instance.getRecord(query);
            return dt;
        }
        public DataTable getSortRecord(string txtSrt,string lop)
        {
            DataTable dt = new DataTable();
            string query = "";
            if(lop  == "All")
            {
                    query = string.Format("SELECT SV1.MSSV, SV1.NAMESV, LSH.NAMELOP, SV1.GENDER, SV1.NGAYSINH, SV1.DTB\r\nFROM SV1\r\nINNER JOIN LSH  ON SV1.ID_LOP = LSH.ID_LOP\r\nORDER BY {0} ASC;", txtSrt);
            }
            else
            {
                query = string.Format("SELECT SV1.MSSV, SV1.NAMESV, LSH.NAMELOP, SV1.GENDER, SV1.NGAYSINH, SV1.DTB\r\nFROM SV1\r\nINNER JOIN LSH  ON SV1.ID_LOP = LSH.ID_LOP\r\n WHERE LSH.NAMELOP = '{1}'\r\nORDER BY {0} ASC ;", txtSrt,lop);
            }
               

            return dt = DBHelper.Instance.getRecord(query);
        }
    }
}
