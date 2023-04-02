using _3Layer_MS.BLL;
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
    public partial class Details : Form
    {
        public delegate void updateForm();
        public event updateForm updateData;
        public string mssv = "";
        public Details()
        {
            InitializeComponent();
            setCBB();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Refresh();
        }
        private void setCBB()
        {
            QLSVBLL bll = new QLSVBLL();
            comboBoxLopSH.Items.AddRange(bll.getCBB().ToArray());
            comboBoxLopSH.Items.RemoveAt(0);
        }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {       
                SV1 sv = new SV1();
                sv.MSSV = textBoxMSSV.Text;
                sv.NameSV = textBoxName.Text;
                sv.ID_Lop = ((CBBItem)comboBoxLopSH.SelectedItem).Value;
                sv.Gender = radioButtonMale.Checked;
                sv.NgaySinh = Convert.ToDateTime(dateTimeNgaySinh.Value.ToString("dd/MM/yyyy"));
                sv.DTB = Convert.ToDouble(textBoxDTB.Text);
                QLSVBLL bll = new QLSVBLL();
                if(mssv == "")
                {
                    bll.addSVBLL(sv);
                }
                else
                {
                    bll.delSVBLL(mssv);
                    bll.addSVBLL(sv);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                mssv = "";
                updateData?.Invoke();
                this.Close();
                this.Refresh();
            }
        }

        private void Details_FormClosed(object sender, FormClosedEventArgs e)
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
    }
}
