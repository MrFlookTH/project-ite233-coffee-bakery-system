using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace Project_ITE233
{
    public partial class FormMoveTable : Form
    {
        SqlConnection conn = new SqlConnection();
        public FormMoveTable(string TableNo, List<Button> buttonList)
        {
            InitializeComponent();
            //conn.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Coffee_MilkTakeYouBack;User ID=sa; Password=2321";
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString.ToString();
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                cbMoveTable.Items.Add(TableNo);
                cbMoveTable.SelectedIndex = 0;
                cbMoveTo.SelectedIndex = 0;
                for (int i = 0; i < buttonList.Count; i++)
                {
                    if (buttonList[i].BackColor == System.Drawing.Color.Transparent)
                    {
                        cbMoveTo.Items.Add(string.Format("{0:00}", i + 1));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void FormMoveTable_Load(object sender, EventArgs e)
        {
            
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbMoveTo.SelectedIndex == 0)
                {
                    MessageBox.Show("กรุณาเลือกโต๊ะครับ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "UPDATE [Order] SET TableNo=@MoveTo WHERE TableNo=@TableNo AND Bill='false'";
                comm.Parameters.Add("@MoveTo", SqlDbType.NVarChar, 2).Value = cbMoveTo.SelectedItem;
                comm.Parameters.Add("@TableNo", SqlDbType.NVarChar, 2).Value = cbMoveTable.SelectedItem;
                if (conn.State != ConnectionState.Open) conn.Open();
                if (comm.ExecuteNonQuery() == 1)
                {
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                comm.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            
        }
    }
}
