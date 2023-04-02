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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLiSV
{
    public partial class Form1 : Form
    {
        
        public void DGV()
        {
            dataGridViewSV.DataSource = DataBase.Instance.dt;
            dataGridViewSV.RefreshEdit();
        }
        public void SetSort()
        {
            comboBoxSortby.Items.Clear();
            comboBoxSortby.Items.Add("MSSV");
            comboBoxSortby.Items.Add("Name");
            comboBoxSortby.Items.Add("Gioi Tinh");
            comboBoxSortby.Items.Add("Lop");
            comboBoxSortby.Items.Add("Ngay Sinh");
            comboBoxSortby.Items.Add("DTB");
            comboBoxSortby.SelectedIndex = 0;
        }
        public Form1()
        {
            InitializeComponent();
            DGV();
            SetCBB();
            SetSort();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.flag = "add";
            f2.Show();
        }

        private void SetCBB()
        {
            comboBoxChooseClass.Items.Clear();
            comboBoxChooseClass.Items.Add("All");
            foreach (string lopSH in QLSV.Instance.getAllLopSH()) {
                comboBoxChooseClass.Items.Add(lopSH);
            }
        }

        private void comboBoxChooseClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            try
            {
                Form2 f2 = new Form2();
                    if(dataGridViewSV.SelectedRows.Count == 1 && dataGridViewSV.SelectedRows != null)
                    {          
                        f2.textBoxMSSV.Text = this.dataGridViewSV.CurrentRow.Cells["MSSV"].Value.ToString();
                        f2.textBoxName.Text = this.dataGridViewSV.CurrentRow.Cells["Name"].Value.ToString();
                        f2.comboBoxLopSH.Text = this.dataGridViewSV.CurrentRow.Cells["Lop Sinh Hoat"].Value.ToString();
                        f2.textBoxDTB.Text =  this.dataGridViewSV.CurrentRow.Cells["Diem Trung Binh"].Value.ToString();
                        f2.dateTimeNgaySinh.Text = this.dataGridViewSV.CurrentRow.Cells["Ngay Sinh"].Value.ToString();
                        f2.radioButtonMale.Checked = Convert.ToBoolean(this.dataGridViewSV.CurrentRow.Cells["Gioi Tinh"].Value);
                
                    }
                    f2.textBoxMSSV.Enabled = false;
                    f2.flag = "edit";
                    f2.Show();
            }
            catch 
            {
                MessageBox.Show("Chưa có dữ liệu", "Thông báo");   
            }
            
        }

        private void dataGridViewSV_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            dataGridViewSV.SelectionMode= DataGridViewSelectionMode.FullRowSelect;
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewSV.SelectedRows.Count == 1 && dataGridViewSV.SelectedRows != null)
                    {
                        QLSV.Instance.RemoveSV(this.dataGridViewSV.CurrentRow.Cells["MSSV"].Value.ToString());
                        SetCBB();
                    }
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message, "Thông Báo");
            }
            
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridViewSV.Sort(dataGridViewSV.Columns[comboBoxSortby.SelectedIndex],ListSortDirection.Ascending);
            }
            
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Thông Báo");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string searchValue = textBoxSearch.Text;
            int rowIndex = -1;

            dataGridViewSV.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dataGridViewSV.Rows)
                {
                    if (row.Cells[row.Index].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = row.Index;
                        dataGridViewSV.Rows[row.Index].Selected = true;
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message,"Thông Báo");
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string s = @"Data Source=LAPTOP-RQB2DO6O\MSSQLSERVER01;Initial Catalog=LNH;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(s);
            List<ClassSV> li = new List<ClassSV>();
            string query = "select * from SV";
            /*string query = "select count (*) from SV";*/
            /*string query = "insert into SV values ('1998','NGU3',1,9,8) ";*/
            SqlCommand cmd = new SqlCommand(query, cnn);
           
            cnn.Open();
          //  int n = (int)cmd.ExecuteScalar();
            cmd.ExecuteNonQuery();
            SqlDataReader r = cmd.ExecuteReader();
            while (r.Read())
            {
                li.Add(new ClassSV
                {
                    MSSV = r["MSSV"].ToString(),
                    Name = r["NameSV"].ToString(),
                    DTB = Convert.ToDouble(r["DTB"].ToString()),
                });

            }

            /*MessageBox.Show("Them thanh cong");*/
            cnn.Close();
            //MessageBox.Show(n.ToString());
            dataGridViewSV.DataSource = li;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string s = @"Data Source=LAPTOP-RQB2DO6O\MSSQLSERVER01;Initial Catalog=LNH;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(s);
            string query = "select * from SV";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            cnn.Open();
            da.Fill(dt);
            cnn.Close();
            /*da.Fill(ds);*/
            /*da.Fill(ds, "NGu");*/
            /* dataGridViewSV.DataSource = ds.Tables["NGu"];
             */
            dataGridViewSV.DataSource = dt;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string query = "select  * from  SV";
            string query1 = "insert into SV values ('1992','NGU6',1,3,6)";
            DBHelper.Instance.ExcuteDB(query);
            dataGridViewSV.DataSource = DBHelper.Instance.GetRecord(query);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string s = @"Data Source=LAPTOP-RQB2DO6O\MSSQLSERVER01;Initial Catalog=LNH;Integrated Security=True";
            SqlConnection cnn = new SqlConnection(s);
            SqlParameter p1 = new SqlParameter("@Name", textQuery.Text);
            string query = "select *from SV where NameSV =" + "@Name";
            SqlCommand cmd = new SqlCommand(query, cnn);
            cmd.Parameters.Add(p1);
            List<ClassSV> li = new List<ClassSV>();
            cnn.Open();
            SqlDataReader r = cmd.ExecuteReader();
            while(r.Read()){

                li.Add(new ClassSV
                {
                    MSSV = r["MSSV"].ToString(),
                    Name = r["NameSV"].ToString(),
                    DTB = Convert.ToDouble(r["DTB"].ToString()),
                });
            }
            cnn.Close();
            dataGridViewSV.DataSource = li;
        }
    }
}
