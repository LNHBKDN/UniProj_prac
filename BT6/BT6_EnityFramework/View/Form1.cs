using BT6_EnityFramework.BLL;
using BT6_EnityFramework.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT6_EnityFramework
{
    public partial class Form1 : Form
    {
        public delegate void UpdateForm();
        public UpdateForm updateDgv;
        private Detail f2 = new Detail();
        public Form1()
        {
            InitializeComponent();
            setCBBLSH();
            setCBBSort();

        }
        private void setCBBSort()
        {
            BLL_QLSV bll = new BLL_QLSV();
            cbbSort.Items.AddRange(bll.getColName().ToArray());
            cbbSort.Items.RemoveAt(2);
        }
        private void setCBBLSH()
        {
            BLL_QLSV bll = new BLL_QLSV();
            cbbLSH.DataSource = bll.setDataCBBLSH();
        }
        private void LoadDGV()
        {
            int txtLop = ((CBBItems)cbbLSH.SelectedItem).Value;
            if(txtLop == 0)
            {
                using(LNHEntities1 eni= new LNHEntities1())
                {
                    dgv.Refresh();
                    dgv.DataSource = eni.SV1.Select(p => new {p.MSSV, p.NAMESV, p.LSH.NAMELOP, p.GENDER, p.DTB}).ToList();
                }
            }
            else
            {
                using(LNHEntities1 eni = new LNHEntities1())
                {
                    dgv.Refresh();
                    dgv.DataSource = eni.SV1.Where(p => p.LSH.ID_LOP == txtLop)
                    .Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP,p.NGAYSINH, p.GENDER, p.DTB }).ToList();
                }
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string txtName = txtBxFind.Text;
            int txtLop = ((CBBItems)cbbLSH.SelectedItem).Value;
            if (txtLop == 0)
            {
                using (LNHEntities1 eni = new LNHEntities1())
                {
                    dgv.Refresh();
                    dgv.DataSource = eni.SV1.Where(p => p.NAMESV.Contains(txtName))
                    .Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP,p.NGAYSINH ,p.GENDER, p.DTB }).ToList();
                }
            }
            else
            {
                using (LNHEntities1 eni = new LNHEntities1())
                {
                    dgv.Refresh();
                    dgv.DataSource = eni.SV1.Where(p => p.LSH.ID_LOP == txtLop && p.NAMESV.Contains(txtName))
                    .Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP,p.NGAYSINH , p.GENDER, p.DTB }).ToList();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(dgv.SelectedRows.Count == 1)
            {
                DataGridViewRow selectedRow = dgv.CurrentRow;
                f2.mssv = selectedRow.Cells["MSSV"].Value.ToString();
                f2.textBoxMSSV.Text = f2.mssv;
                f2.textBoxMSSV.Enabled = false;

                f2.textBoxName.Text = selectedRow.Cells["NAMESV"].Value.ToString();
                f2.comboBoxLopSH.Text = selectedRow.Cells["NAMELOP"].Value.ToString();
                f2.textBoxDTB.Text = selectedRow.Cells["DTB"].Value.ToString();
                f2.dateTimeNgaySinh.Text = selectedRow.Cells["NGAYSINH"].Value.ToString();
                f2.radioButtonMale.Checked = Convert.ToBoolean(selectedRow.Cells["GENDER"].Value.ToString());
                f2.ShowDialog();
                f2.updateDgv += LoadDGV;
            }
            else
            {
                MessageBox.Show("CHon 1 Row Di!!!",MessageBoxIcon.Warning.ToString());
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            f2.ShowDialog();
            f2.updateDgv += LoadDGV;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if(dgv.SelectedRows.Count == 1)
            {
                string mssv = dgv.CurrentRow.Cells["MSSV"].Value.ToString();
                using (LNHEntities1 eni = new LNHEntities1())
                {
                    SV1 sv = eni.SV1.Where(p => p.MSSV == mssv).FirstOrDefault();
                    eni.SV1.Remove(sv);
                    eni.SaveChanges();
                }
            }
            LoadDGV();
        }

        private void btnSort_Click(object sender, EventArgs e)
        {
            string txtSort = cbbSort.Text;
            int txtLop = ((CBBItems)cbbLSH.SelectedItem).Value;
            if ( txtSort == "MSSV")
            {
                if(txtLop == 0)
                {
                    using (LNHEntities1 eni = new LNHEntities1())
                    {
                        dgv.DataSource = eni.SV1.OrderByDescending(p => p.MSSV)
                   .Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP, p.NGAYSINH, p.GENDER, p.DTB }).ToList();
                    }
                }
                else
                {
                    using(LNHEntities1 eni = new LNHEntities1())
                    {
                       dgv.DataSource = eni.SV1.OrderByDescending(p =>p.MSSV).Where(p => p.LSH.ID_LOP == txtLop)
                       .Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP, p.NGAYSINH, p.GENDER, p.DTB }).ToList();
                    }
                }

            }
            else if (txtSort == "NAMESV")
            {
                if (txtLop == 0)
                {
                    using (LNHEntities1 eni = new LNHEntities1())
                    {
                        dgv.DataSource = eni.SV1.OrderByDescending(p => p.MSSV)
                        .Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP, p.NGAYSINH, p.GENDER, p.DTB }).ToList();
                    }
                }
                else
                {
                    using (LNHEntities1 eni = new LNHEntities1())
                    {
                        dgv.DataSource = eni.SV1.OrderByDescending(p => p.NAMESV).Where(p => p.LSH.ID_LOP == txtLop)
                        .Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP, p.NGAYSINH, p.GENDER, p.DTB }).ToList();
                    }
                }
            }
            else if (txtSort == "NGAYSINH")
            {
                if (txtLop == 0)
                {
                    using (LNHEntities1 eni = new LNHEntities1())
                    {
                        dgv.DataSource = eni.SV1.OrderByDescending(p => p.MSSV)
                        .Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP, p.NGAYSINH, p.GENDER, p.DTB }).ToList();
                    }
                }
                else
                {
                    using (LNHEntities1 eni = new LNHEntities1())
                    {
                        dgv.DataSource = eni.SV1.OrderByDescending(p => p.NGAYSINH).Where(p => p.LSH.ID_LOP == txtLop)
                        .Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP, p.NGAYSINH, p.GENDER, p.DTB }).ToList();
                    }
                }
            }
            else if (txtSort == "DTB")
            {
                if (txtLop == 0)
                {
                    using (LNHEntities1 eni = new LNHEntities1())
                    {
                        dgv.DataSource = eni.SV1.OrderByDescending(p => p.MSSV)
                        .Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP, p.NGAYSINH, p.GENDER, p.DTB }).ToList();
                    }
                }
                else
                {
                    using (LNHEntities1 eni = new LNHEntities1())
                    {
                        dgv.DataSource = eni.SV1.OrderByDescending(p => p.DTB).Where(p => p.LSH.ID_LOP == txtLop)
                        .Select(p => new { p.MSSV, p.NAMESV, p.LSH.NAMELOP, p.NGAYSINH, p.GENDER, p.DTB }).ToList();
                    }
                }
            }
            else
            {
                MessageBox.Show("Chọn 1 thuộc tính để Sort", MessageBoxIcon.Warning.ToString());
            }
        }
    }
}
