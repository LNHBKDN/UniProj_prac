using BT6_EnityFramework.BLL;
using BT6_EnityFramework.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Migrations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BT6_EnityFramework
{
    public partial class Detail : Form
    {
        public delegate void UpdateForm();
        public UpdateForm updateDgv;
        public string mssv = "";
        public Detail()
        {
            InitializeComponent();
            setSBBLSH();
        }
        private void LoadForm()
        {
            foreach (Control control in this.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).Text = string.Empty;
                }
            }
            foreach (Control control in this.Controls)
            {
                if (control is ComboBox)
                {
                    ((ComboBox)control).Text = string.Empty;
                }
            }
            foreach (Control control in this.Controls)
            {
                if (control is DateTimePicker)
                {
                    ((DateTimePicker)control).Text = string.Empty;
                }
            }
            foreach (Control control in this.Controls)
            {
                if (control is RadioButton)
                {
                    ((RadioButton)control).Text = string.Empty;
                }
            }
        }
        private void setSBBLSH()
        {
            BLL_QLSV bll = new BLL_QLSV();
            comboBoxLopSH.Items.AddRange(bll.setDataCBBLSH().ToArray());
            comboBoxLopSH.Items.RemoveAt(0);
            comboBoxLopSH.DropDownStyle = ComboBoxStyle.DropDownList;
        }
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            LoadForm();
            this.Close();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                SV1 sv = new SV1();
                sv.MSSV = textBoxMSSV.Text;
                sv.GENDER = radioButtonMale.Checked;
                sv.NAMESV = textBoxName.Text;
                sv.DTB = Convert.ToDouble(textBoxDTB.Text);
                sv.NGAYSINH = Convert.ToDateTime(dateTimeNgaySinh.Text);
                sv.ID_LOP = ((CBBItems)comboBoxLopSH.SelectedItem).Value;
                if(mssv == "")
                {

                    using(LNHEntities1 eni = new LNHEntities1())
                    {          
                        eni.SV1.Add(sv);
                        eni.SaveChanges();
                    }
                }
                else
                {
                    using (LNHEntities1 eni = new LNHEntities1())
                    {
                        eni.SV1.AddOrUpdate(sv);
                        eni.SaveChanges();
                    }
                }
            
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, MessageBoxIcon.Warning.ToString());
            }
            finally
            {
                LoadForm();
                updateDgv?.Invoke();
                this.Close();
            }
        }

        private void Detail_Load(object sender, EventArgs e)
        {

        }

        private void Detail_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadForm();
        }
    }
}
