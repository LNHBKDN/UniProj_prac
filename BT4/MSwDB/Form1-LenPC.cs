using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSwDB
{
    public partial class Form1 : Form
    {
        public delegate void UpdateDataGridView();
        public UpdateDataGridView updateDGV;
        private DETAILS f2;
        public Form1()
        {
            
            InitializeComponent();
            setCBBLSH();
            setDGV();
            SetCBBSort();
            f2 = new DETAILS();
            f2.updateDGV += new DETAILS.UpdateDGVDelegate(LoadDataGridView);

        }
        private void setCBBLSH()
        {
            
            string query = "SELECT DISTINCT LOP FROM SV";
            DataTable dt = DBHelper.Instance.getRecord(query);
            DataRow Row = dt.NewRow();
            Row["LOP"] = "All";
            dt.Rows.InsertAt(Row, 0);
            comboBoxLSH.DataSource = dt;
            comboBoxLSH.DisplayMember= "LOP";
        }
        void setDGV()
        {
            string query = "select * from SV";
            DataTable dt = DBHelper.Instance.getRecord(query);
            dataGridViewSV.DataSource = dt;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string txtLop = comboBoxLSH.SelectedItem.ToString();
            string txtTK = txtFind.Text.Trim();
            string query;
            if (txtLop != "All")
            {
                dataGridViewSV.DataSource = null;
                dataGridViewSV.Rows.Clear();
                query = "SELECT * FROM SV WHERE LOP = @Lop AND TEN LIKE @Ten";
            }
            else
            {
                dataGridViewSV.DataSource = null;
                dataGridViewSV.Rows.Clear();
                query = "SELECT * FROM SV WHERE TEN LIKE @Ten";
            }

            SqlCommand cmd = new SqlCommand(query);
            if (txtLop != "All")
            {
                dataGridViewSV.DataSource = null;
                dataGridViewSV.Rows.Clear();
                cmd.Parameters.AddWithValue("@Lop", txtLop);
            }
            cmd.Parameters.AddWithValue("@Ten", "%" + txtTK + "%");

            DataTable dt = DBHelper.Instance.getRecord(cmd);
            dataGridViewSV.DataSource = dt;
            LoadDataGridView();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            f2.updateDGV += LoadDataGridView;
            f2.Show();
        }
        private void LoadDataGridView()
        {
            string query;
            if (comboBoxLSH.Text != "All")
            {
                query = "SELECT * FROM SV WHERE LOP=@Lop";
            }
            else
            {
                query = "SELECT * FROM SV";
            }
            SqlCommand cmd = new SqlCommand(query);
            if (comboBoxLSH.Text != "All")
            {
                cmd.Parameters.AddWithValue("@Lop", comboBoxLSH.Text);
            }
            DataTable dt = DBHelper.Instance.getRecord(cmd);
            dataGridViewSV.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if(dataGridViewSV.SelectedRows.Count == 1) 
            {
                DataGridViewRow row = dataGridViewSV.CurrentRow;
                string mssv = row.Cells["MSSV"].Value.ToString();
                string query = "delete from SV where MSSV = @id";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@id", mssv);
                DBHelper.Instance.ExcuteDB(cmd);
                LoadDataGridView();
            } 
        }

        private void bttEdit_Click(object sender, EventArgs e)
        {
            if(dataGridViewSV.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dataGridViewSV.SelectedRows[0];
  
                f2.textBoxMSSV.Text = selectedRow.Cells["MSSV"].Value.ToString();
                f2.textBoxName.Text = selectedRow.Cells["TEN"].Value.ToString();
                f2.radioButtonMale.Checked =Convert.ToBoolean( selectedRow.Cells["GENDER"].Value.ToString());
                f2.dateTimeNgaySinh.Text = selectedRow.Cells["NGAYSINH"].Value.ToString();
                f2.comboBoxLopSH.Text = selectedRow.Cells["LOP"].Value.ToString();
                f2.textBoxDTB.Text =selectedRow.Cells["DTB"].Value.ToString();
                f2.textBoxMSSV.Enabled = false;
                f2.mssv = selectedRow.Cells["MSSV"].Value.ToString();
                
            }
            f2.Show();
        }
        private void SetCBBSort()
        {
            string query = "SELECT DISTINCT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SV'";
            DataTable dt = DBHelper.Instance.getRecord(query);
            foreach (DataRow row in dt.Rows)
            {
                comboBox1.Items.Add(row["COLUMN_NAME"].ToString());
            }
        }
        private void SortDataView()
        {

            try
            {
                string query = "";
                string sortBy = comboBox1.SelectedItem.ToString();
                if (sortBy == "MSSV")
                    query = "SELECT * FROM SV ORDER BY MSSV ASC";
                if (sortBy == "TEN")
                    query = "SELECT * FROM SV ORDER BY TEN ASC";
                if (sortBy == "NGAYSINH")
                    query = "SELECT * FROM SV ORDER BY NGAYSINH ASC";
                if (sortBy == "GENDER")
                    query = "SELECT * FROM SV ORDER BY GENDER ASC";
                if (sortBy == "LOP")
                    query = "SELECT * FROM SV ORDER BY LOP ASC";
                if (sortBy == "DTB")
                    query = "SELECT * FROM SV ORDER BY DTB ASC";
                
                DataTable dt = DBHelper.Instance.getRecord(query);
                dataGridViewSV.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            SortDataView();
        }
    }
}
