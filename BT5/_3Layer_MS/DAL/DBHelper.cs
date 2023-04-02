using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3Layer_MS.DAL
{
    internal class DBHelper
    {
        private static DBHelper _Instance;
        private SqlConnection _cnn;
        public static DBHelper Instance
        {
            get 
            {   if(_Instance == null)
                {
                    string s = @"Data Source=LAPTOP-RQB2DO6O\MSSQLSERVER01;Initial Catalog=_NET;Integrated Security=True";
                    _Instance = new DBHelper(s);
                }
                return _Instance; 
            }
            private set { }
        }
        public DBHelper(string s)
        {
            _cnn = new SqlConnection(s);
        }
        public DataTable getRecord(string query)
        {          
            DataTable dt = new DataTable();
            try
            {
                
                SqlDataAdapter da = new SqlDataAdapter(query,_cnn);
                _cnn.Open();
                da.Fill(dt);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, MessageBoxIcon.Error.ToString());
            }
            finally
            {
                _cnn.Close();
            }
            return dt;
        }
        public DataTable getRecord(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {   cmd.Connection = _cnn;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                _cnn.Open();
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, MessageBoxIcon.Error.ToString());
            }
            finally
            {
                _cnn.Close();
            }
            return dt;
        }
        public void excuteDB(SqlCommand cmd)
        {   
            cmd.Connection = _cnn;
            try
            {   
                
                _cnn.Open();
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,MessageBoxIcon.Error.ToString());
            }
            finally
            {
                _cnn.Close(); 
            }
        }
    }
}
