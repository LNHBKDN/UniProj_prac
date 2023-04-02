using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSwDB
{
    internal class DBHelper
    {
        private static DBHelper _Instance;
        private SqlConnection _cnn;
        public static DBHelper Instance
        {
            get
            {
                if(_Instance == null)
                {
                    string s = @"Data Source=LAPTOP-RQB2DO6O\MSSQLSERVER01;Initial Catalog=LNH;Integrated Security=True";
                    _Instance = new DBHelper(s);
                }
                return _Instance;
            }
            private set { }
        }
        private DBHelper(string s)
        {
            _cnn = new SqlConnection(s);
        }
        public DataTable getRecord(string query)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query,_cnn);
            _cnn.Open();
            da.Fill(dt);
            _cnn.Close();
            return dt;
        }
        public DataTable getRecord(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            cmd.Connection = _cnn;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            _cnn.Open();
            da.Fill(dt);
            _cnn.Close();
            return dt;
        }
        public DataTable getRecord(string spName, SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(spName, _cnn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(parameters);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            _cnn.Open();
            da.Fill(dt);
            _cnn.Close();
            return dt;
        }
        public void ExcuteDB(string query)
        {
            SqlCommand cmd = new SqlCommand(query, _cnn);
            _cnn.Open();
            cmd.ExecuteNonQuery();
            _cnn.Close();
        }
        public void ExcuteDB(SqlCommand cmd)
        {          
            cmd.Connection = _cnn;

            try
            {
                _cnn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _cnn.Close();
            }
        }

    }
}
