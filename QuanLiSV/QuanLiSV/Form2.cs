using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiSV
{
    public partial class Form2 : Form
    {
        public string flag;
        public Form2()
        {
            InitializeComponent();
            SetCBB();
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(flag == "add")
            {
                if (CheckData())
                {
                    ClassSV sv = new ClassSV();
                    sv.MSSV = textBoxMSSV.Text;
                    sv.Name = textBoxName.Text;
                    sv.DTB = Convert.ToDouble(textBoxDTB.Text);
                    sv.NgaySinh = dateTimeNgaySinh.Text;
                    sv.Gender = radioButtonMale.Checked;
                    sv.lopSH = comboBoxLopSH.Text;

                    DataBase.Instance.dt.Rows.Add(
                        textBoxMSSV.Text,
                        textBoxName.Text,
                        radioButtonMale.Checked,
                        comboBoxLopSH.Text,
                        dateTimeNgaySinh.Text,
                        textBoxDTB.Text
                    );
                    this.Close();
                }
            }
            else if(flag == "edit")
            {              
                if (CheckData())
                {
                    
                    ClassSV sv = new ClassSV();       
                    sv.Name = textBoxName.Text;
                    sv.DTB = Convert.ToDouble(textBoxDTB.Text);
                    sv.NgaySinh = dateTimeNgaySinh.Text;
                    sv.Gender = radioButtonMale.Checked;
                    sv.lopSH = comboBoxLopSH.Text;

                    DataBase.Instance.dt.Rows.Add(
                        textBoxMSSV.Text,
                        textBoxName.Text,
                        radioButtonMale.Checked,
                        comboBoxLopSH.Text,
                        dateTimeNgaySinh.Text,
                        textBoxDTB.Text
                    );
                    QLSV.Instance.RemoveSV( this.textBoxMSSV.Text );
                    this.Close( );
                }
                
            }
            
        }
        public bool CheckData()
        {
            if (string.IsNullOrWhiteSpace(textBoxMSSV.Text))
            {
                MessageBox.Show("Chua nhap MSSV", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Chua nhap Ten", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrWhiteSpace(dateTimeNgaySinh.Text))
            {
                MessageBox.Show("Chua nhap Ngay Sinh", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if (string.IsNullOrWhiteSpace(textBoxDTB.Text))
            {
                MessageBox.Show("Chua nhap DTB", "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SetCBB()
        {
            
            foreach (string lopSH in QLSV.Instance.getAllLopSH())
            {
                comboBoxLopSH.Items.Add(lopSH);
            }
        }
        private void comboBoxLopSH_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBoxGender_Enter(object sender, EventArgs e)
        {

        }

        private void dateTimeNgaySinh_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
