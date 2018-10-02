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
    public partial class FormAddOrder : Form
    {
        private int QTY;
        public int _QTY
        {
            get { return QTY; }
        }
        public FormAddOrder()
        {
            InitializeComponent();
            txtQTY.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                QTY = Convert.ToInt32(txtQTY.Text);
                if (QTY > 0)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("กรุณาป้อนตัวเลข [1-9]", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("ป้อนข้อมูลไม่ถูกต้อง","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                txtQTY.Focus();
            }
        }
    }
}
