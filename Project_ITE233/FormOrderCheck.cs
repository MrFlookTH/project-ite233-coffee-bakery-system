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
    public partial class FormOrderCheck : Form
    {
        SqlConnection conn = new SqlConnection();
        public FormOrderCheck(int OrderID)
        {
            InitializeComponent();
            //conn.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Coffee_MilkTakeYouBack;User ID=sa; Password=2321";
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString.ToString();
            ShowTableData(OrderID);
        }

        private void ShowTableData(int OrderID)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "SELECT DateTime,TableNo,CusNo,Total,IDO FROM [Order] WHERE IDO = @IDO";
                comm.Parameters.Add("@IDO", SqlDbType.VarChar, 2).Value = OrderID;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    labelTableNo.Text = "โต๊ะ " + reader[1];
                    labelCusNo.Text = "จำนวน " + reader[2] + " ท่าน";
                    labelDate.Text = reader[0].ToString();
                    labelOrderID.Text = reader[4].ToString();
                    double total = Convert.ToDouble(reader[3]);
                    labelTotal.Text = total.ToString("N2");
                    labelTotal2.Text = total.ToString("N2");
                    conn.Close();
                    ShowOrderDetail();
                }
                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowOrderDetail()
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;

                string sql = "SELECT P.IDP รหัสสินค้า,P.Name รายการ,OD.QTY จำนวน,P.UnitPrice ราคา,OD.Total ราคารวม FROM Order_Detail OD " +
                                   "INNER JOIN Product P ON P.IDP = OD.IDP " +
                                   "INNER JOIN [Order] O ON O.IDO = OD.IDO " +
                                   "WHERE O.IDO = @IDO";
                comm.CommandText = sql;
                comm.Parameters.Add("@IDO", SqlDbType.VarChar, 2).Value = labelOrderID.Text;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvOrder.DataSource = dt;
                comm.Dispose();
                //dgvOrder.Columns[0].Width = 50;
                //dgvOrder.Columns[1].Width = 280;
                //dgvOrder.Columns[3].DefaultCellStyle.Format = "0.00##";
                //dgvOrder.Columns[4].DefaultCellStyle.Format = "0.00##";
            }
            finally
            {
                conn.Close();
            }
        }



    }
}
