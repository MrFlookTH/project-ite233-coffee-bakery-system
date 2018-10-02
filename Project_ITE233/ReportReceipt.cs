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
    public partial class ReportReceipt : Form
    {
        public ReportReceipt()
        {
            InitializeComponent();
        }

        private void ReportReceipt_Load(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            // TODO: This line of code loads data into the 'Coffee_MilkTakeYouBackDataSet.DataTable1' table. You can move, or remove it, as needed.
            this.DataTable1TableAdapter.FillByIDO(this.Coffee_MilkTakeYouBackDataSet.DataTable1,23);

            this.reportViewer1.RefreshReport();
            Cursor.Current = Cursors.Default;
        }
    }
}
