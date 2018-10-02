using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_ITE233
{
    public partial class FormCustomerNo : Form
    {
        private int CustomerNo;
        public int CusNo
        {
            get { return CustomerNo; }
        }
        public FormCustomerNo()
        {
            InitializeComponent();
        }
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtCustomerNo.Text == "")
            {
                MessageBox.Show("กรุณาป้อนจำนวนลูกค้า","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtCustomerNo.Focus();
            }
            else
            {
                try
                {
                    CustomerNo = Convert.ToInt16(txtCustomerNo.Text);
                    this.DialogResult = DialogResult.OK;
                }
                catch
                {
                    MessageBox.Show("กรุณาป้อนเฉพาะตัวเลข [0-9]", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }               
            }
        }
    }
}
