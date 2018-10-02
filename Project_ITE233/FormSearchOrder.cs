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
    public partial class FormSearchOrder : Form
    {
        int _OrderID;
        public int OrderID
        {
            get { return _OrderID; }
        }
        public FormSearchOrder()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtOrderID.Text != "")
            {
                try
                {
                    _OrderID = Convert.ToInt32(txtOrderID.Text);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch
                {
                    MessageBox.Show("กรุณาป้อนข้อมูลให้ถูกต้อง","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                
            }
        }
    }
}
