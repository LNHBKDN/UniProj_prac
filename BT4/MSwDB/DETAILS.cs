using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MSwDB
{

    public partial class DETAILS : Form
    {
        public string mssv = "";
        public delegate void UpdateDGVDelegate();
        public event UpdateDGVDelegate updateDGV;
        public DETAILS()
        {
            InitializeComponent();
        }
      
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(mssv == "")
            {              
                try
                {
                string s = @"Data Source=LAPTOP-RQB2DO6O\MSSQLSERVER01;Initial Catalog=LNH;Integrated Security=True";
                SqlConnection cnn = new SqlConnection(s);
                string query = "INSERT INTO SV (MSSV, TEN, NGAYSINH, GENDER, LOP, DTB) VALUES (@Ma, @Ten, @NgaySinh, @gen, @lop, @diem)";
                SqlCommand cmd = new SqlCommand(query, cnn);
                cmd.Parameters.AddWithValue("@Ma", textBoxMSSV.Text);
                cmd.Parameters.AddWithValue("@Ten", textBoxName.Text);
                cmd.Parameters.AddWithValue("@Lop", comboBoxLopSH.Text);
                cmd.Parameters.AddWithValue("@NgaySinh", dateTimeNgaySinh.Value.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@gen", radioButtonMale.Checked);
                cmd.Parameters.AddWithValue("@diem", Convert.ToDouble(textBoxDTB.Text));
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();             
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                try
                {
                    string query = "UPDATE SV SET Ten=@Ten, gender=@gen, NgaySinh=@NgaySinh, LOP=@Lop, DTB=@diem WHERE MSSV=@Ma";
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@Ma", mssv);
                    cmd.Parameters.AddWithValue("@Ten", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@Lop", comboBoxLopSH.Text);
                    cmd.Parameters.AddWithValue("@NgaySinh", dateTimeNgaySinh.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@gen", radioButtonMale.Checked);
                    cmd.Parameters.AddWithValue("@diem", Convert.ToDouble(textBoxDTB.Text));
                    DBHelper.Instance.ExcuteDB(cmd);
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            updateDGV?.Invoke();
            this.Close();
            
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
