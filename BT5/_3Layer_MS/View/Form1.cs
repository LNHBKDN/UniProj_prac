using _3Layer_MS.BLL;
using _3Layer_MS.DAL;
using _3Layer_MS.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3Layer_MS
{
    public partial class Form1 : Form
    {
        public delegate void updateForm();
        public updateForm updateData;
        private Details f2 = new Details();
        public Form1()
        {
            InitializeComponent();
            LoadData();
            setCBB();
            setSortCBB();
        }
        private void setCBB()
        {
            QLSVBLL bll = new QLSVBLL();
            cbbLSH.Items.AddRange(bll.getCBB().ToArray());           
        }
        private void setSortCBB()
        {
            QLSVBLL bll = new QLSVBLL();
            cbbSort.Items.AddRange(bll.getColumnNameBll().ToArray());   
        }
        private void cbbLSH_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Id_Lop = ((CBBItem)cbbLSH.SelectedItem).Value;
            QLSVBLL bll = new QLSVBLL();
            dgv.DataSource = bll.getSVbyIDlop(Id_Lop);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            f2.updateData += LoadData;
            f2.ShowDialog();
        }
        public void LoadData()
        {
            QLSVBLL bll = new QLSVBLL();
            dgv.DataSource = bll.setDataBLL();

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if(dgv.SelectedRows.Count == 1) 
                {
                    DataGridViewRow selectedRow = dgv.CurrentRow;
                    string mssv = selectedRow.Cells["MSSV"].Value.ToString();
                    QLSVBLL bll = new QLSVBLL();
                    bll.delSVBLL(mssv);
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Choose 1 row", MessageBoxIcon.Warning.ToString());
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 1)
            {

                try
                {
                    DataGridViewRow selectedRow = dgv.CurrentRow;
                    f2.mssv = selectedRow.Cells["MSSV"].Value.ToString();
                    f2.textBoxMSSV.Text = f2.mssv;
                    f2.textBoxMSSV.Enabled = false;

                    f2.textBoxName.Text = selectedRow.Cells["NAMESV"].Value.ToString();
                    f2.comboBoxLopSH.Text = selectedRow.Cells["NAMELOP"].Value.ToString();
                    f2.textBoxDTB.Text =  selectedRow.Cells["DTB"].Value.ToString();
                    f2.dateTimeNgaySinh.Text = selectedRow.Cells["NGAYSINH"].Value.ToString();
                    f2.radioButtonMale.Checked = Convert.ToBoolean(selectedRow.Cells["GENDER"].Value.ToString());

                    f2.ShowDialog();
                    f2.updateData += LoadData;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, MessageBoxIcon.Warning.ToString());
                }
            }
            else
            {
                MessageBox.Show("Choose 1 row", MessageBoxIcon.Warning.ToString());
            }

        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            string txtSrt = cbbSort.Text.Trim();
            string txtLop = cbbLSH.Text;
            QLSVBLL bll = new QLSVBLL();
            dgv.DataSource = bll.SortBLL(txtSrt,txtLop);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string nameSv = txtBxFind.Text.Trim();
            string txtLop = cbbLSH.Text;
            QLSVBLL bll = new QLSVBLL();
            dgv.DataSource = bll.getSVByNameBLL(nameSv,txtLop);
        }
    }
}
