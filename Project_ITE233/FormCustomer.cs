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
    public partial class FormCustomer : Form
    {
        SqlConnection conn = new SqlConnection();
        public FormCustomer()
        {
            InitializeComponent();
            //conn.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Coffee_MilkTakeYouBack;User ID=sa; Password=2321";
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString.ToString();
        }

        private void ShowCustomer()
        {
            string sql = "SELECT IDC รหัส,FirstName ชื่อ,LastName นามสกุล,Tel เบอร์โทร,Email อีเมล,Register_Date วันที่สมัคร FROM Customer ORDER BY IDC DESC";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(sql, conn);
            da.Fill(dt);
            dgvCustomer.DataSource = dt;
            dgvCustomer.Columns[0].Width = 50;
            dgvCustomer.Columns[1].Width = 100;
            dgvCustomer.Columns[2].Width = 100;
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            ShowCustomer();
        }

        private void dgvCustomer_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCustomer.Rows.Count > 0)
            {
                int CurrentRow = dgvCustomer.CurrentRow.Index;
                txtIDC.Text = dgvCustomer.Rows[CurrentRow].Cells[0].Value.ToString();
                txtFirstName.Text = dgvCustomer.Rows[CurrentRow].Cells[1].Value.ToString();
                txtLastName.Text = dgvCustomer.Rows[CurrentRow].Cells[2].Value.ToString();
                txtTel.Text = dgvCustomer.Rows[CurrentRow].Cells[3].Value.ToString();
                txtEmail.Text = dgvCustomer.Rows[CurrentRow].Cells[4].Value.ToString();
            }         
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (btnAdd.Text == "เพิ่ม")
            {
                dgvCustomer.Enabled = false;
                dgvCustomer.ClearSelection();
                txtIDC.Text = "New";
                txtFirstName.Clear();
                txtLastName.Clear();
                txtTel.Clear();
                txtEmail.Clear();
                btnAdd.Text = "บันทึก";
                btnEdit.Text = "ยกเลิก";
                btnDelete.Enabled = false;
            }
            else
            {
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = "INSERT INTO Customer(FirstName,LastName,Tel,Email,Register_Date) " +
                                       "VALUES(@FirstName,@LastName,@Tel,@Email,GETDATE())";
                    comm.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = txtFirstName.Text;
                    comm.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = txtLastName.Text;
                    comm.Parameters.Add("@Tel", SqlDbType.NVarChar, 13).Value = txtTel.Text;
                    comm.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = txtEmail.Text;
                    if (conn.State != ConnectionState.Open) conn.Open();
                    if (comm.ExecuteNonQuery() == 1)
                    {
                        ShowCustomer();
                        btnAdd.Text = "เพิ่ม";
                        btnEdit.Text = "แก้ไข";
                        btnDelete.Enabled = true;
                        MessageBox.Show("บันทึกข้อมูลแล้ว", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvCustomer.Enabled = true;
                    }
                    comm.Dispose();
                }
                finally
                {
                    conn.Close();
                }
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "ยกเลิก")
            {
                btnAdd.Text = "เพิ่ม";
                btnEdit.Text = "แก้ไข";
                btnDelete.Enabled = true;
                dgvCustomer.Enabled = true;
                if (dgvCustomer.Rows.Count > 0)
                {
                    dgvCustomer.Rows[0].Selected = true;
                }
            }
            else
            {
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = "UPDATE Customer SET FirstName=@FirstName,LastName=@LastName,Tel=@Tel,Email=@Email " +
                                       "WHERE IDC=@IDC";
                    comm.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = txtFirstName.Text;
                    comm.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = txtLastName.Text;
                    comm.Parameters.Add("@Tel", SqlDbType.NVarChar, 13).Value = txtTel.Text;
                    comm.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = txtEmail.Text;
                    comm.Parameters.Add("@IDC",SqlDbType.Int).Value = txtIDC.Text;
                    if (conn.State != ConnectionState.Open) conn.Open();
                    if (comm.ExecuteNonQuery() == 1)
                    {
                        int CurrentRow = dgvCustomer.CurrentRow.Index;
                        dgvCustomer.Rows[CurrentRow].Cells[1].Value = txtFirstName.Text;
                        dgvCustomer.Rows[CurrentRow].Cells[2].Value = txtLastName.Text;
                        dgvCustomer.Rows[CurrentRow].Cells[3].Value = txtTel.Text;
                        dgvCustomer.Rows[CurrentRow].Cells[4].Value = txtEmail.Text;
                        MessageBox.Show("แก้ไขข้อมูลแล้ว", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    comm.Dispose();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("ต้องการลบข้อมูลหรือไม่?","กรุณายืนยัน",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = "DELETE FROM Customer WHERE IDC=@IDC";
                    comm.Parameters.Add("@IDC", SqlDbType.Int).Value = txtIDC.Text;
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    if (comm.ExecuteNonQuery() == 1)
                    {
                        int CurrentRow = dgvCustomer.CurrentRow.Index;
                        dgvCustomer.Rows.RemoveAt(CurrentRow);
                        MessageBox.Show("ลบข้อมูลเรียบร้อยแล้ว", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    comm.Dispose();
                }
                catch (Exception ex)
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
}
