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
    public partial class FormProduct : Form
    {
        SqlConnection conn = new SqlConnection();
        public FormProduct()
        {
            InitializeComponent();
            //conn.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Coffee_MilkTakeYouBack;User ID=sa; Password=2321";
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString.ToString();
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = "SELECT IDCat,Name FROM Categories";
                DataSet ds = new DataSet();
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql,conn);
                da.Fill(ds);
                listBoxCategories.DataSource = ds.Tables[0];
                listBoxCategories.DisplayMember = "Name";
                listBoxCategories.ValueMember = "IDCat";
                listBoxCategories.SelectedIndex = 0;
            }
            catch(Exception ex)
            {
                MessageBox.Show("ไม่สามารถเชื่อมต่อฐานข้อมูล "+ex,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void ShowProduct()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                int IDCat = listBoxCategories.SelectedIndex+1;
                DataTable dt = new DataTable();
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "SELECT IDP รหัส,Name ชื่อ,Description รายละเอียด,UnitPrice ราคา,Status สถานะ FROM Product WHERE IDCat = @IDCat";
                comm.Parameters.Add("@IDCat", SqlDbType.Int).Value = IDCat;
                if (conn.State != ConnectionState.Open)
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                da.Fill(dt);
                dgvProduct.DataSource = dt;
                dgvProduct.Columns[0].Width = 50;
                dgvProduct.Columns[1].Width = 180;
                dgvProduct.Columns[2].Width = 100;
                dgvProduct.Columns[3].Width = 60;
                dgvProduct.Columns[4].Width = 90;
                dgvProduct.Columns[3].DefaultCellStyle.Format = "0.00##";
                txtIDP.Enabled = false;
                if (dt.Rows.Count > 0)
                {
                    dgvProduct.Rows[0].Selected = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ไม่สามารถเชื่อมต่อฐานข้อมูล "+ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                Cursor.Current = Cursors.Default;
            }
        }

        private void listBoxCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowProduct();
        }

        private void dgvProduct_SelectionChanged(object sender, EventArgs e)
        {
            int selectedRow = dgvProduct.CurrentRow.Index;
            txtIDP.Text = dgvProduct.Rows[selectedRow].Cells[0].Value.ToString();
            cbCategories.SelectedIndex = listBoxCategories.SelectedIndex;
            txtName.Text = dgvProduct.Rows[selectedRow].Cells[1].Value.ToString();
            txtDescription.Text = dgvProduct.Rows[selectedRow].Cells[2].Value.ToString();
            txtUnitPrice.Text = dgvProduct.Rows[selectedRow].Cells[3].Value.ToString();
            if (Convert.ToBoolean(dgvProduct.Rows[selectedRow].Cells[4].Value) == true)
            {
                cbStatus.SelectedItem = "Show";
            }
            else
            {
                cbStatus.SelectedItem = "Hide";
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (btnInsert.Text == "เพิ่ม")
            {
                listBoxCategories.Enabled = false;
                dgvProduct.Enabled = false;
                dgvProduct.ClearSelection();
                listBoxCategories.ClearSelected();
                txtIDP.Text = "Product ID";
                txtName.Clear();
                txtUnitPrice.Clear();
                cbStatus.SelectedIndex = 0;
                btnInsert.Text = "บันทึก";
                btnEdit.Text = "ยกเลิก";
                btnDelete.Enabled = false;
            }
            else if (btnInsert.Text == "บันทึก")
            {
                try
                {
                    if (txtName.Text == "" || txtUnitPrice.Text == "")
                    {
                        MessageBox.Show("กรุณาป้อนข้อมูลให้ครบถ้วน","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        return;
                    }
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = "INSERT INTO Product(Name,IDCat,Description,UnitPrice,Status) " +
                                       "VALUES(@Name,@IDCat,@Description,@UnitPrice,@Status)";
                    comm.Parameters.Add("@Name", SqlDbType.NVarChar,50).Value = txtName.Text;
                    comm.Parameters.Add("@IDCat", SqlDbType.Int).Value = cbCategories.SelectedIndex+1;
                    comm.Parameters.Add("@Description", SqlDbType.NVarChar, 100).Value = txtDescription.Text;
                    comm.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = txtUnitPrice.Text;
                    bool status;
                    if(cbStatus.SelectedIndex == 0)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    comm.Parameters.Add("@Status", SqlDbType.Bit).Value = status;
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    if (comm.ExecuteNonQuery() == 1)
                    {
                        MessageBox.Show("บันทึกเรียบร้อยแล้ว","",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        txtName.Clear();
                        txtDescription.Clear();
                        txtUnitPrice.Clear();
                        cbStatus.SelectedIndex = 0;
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error "+ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                txtIDP.Enabled = true;
                txtIDP.Clear();
                txtName.Clear();
                txtUnitPrice.Clear();
                listBoxCategories.Enabled = true;
                dgvProduct.Enabled = true;
                btnInsert.Text = "เพิ่ม";
                btnEdit.Text = "แก้ไข";
                btnDelete.Enabled = true;
                if (listBoxCategories.Items.Count > 0)
                {
                    listBoxCategories.SelectedIndex = 0;
                }
            }
            else if (btnEdit.Text == "แก้ไข")
            {
                try
                {
                    if (txtName.Text == "" || txtUnitPrice.Text == "")
                    {
                        MessageBox.Show("กรุณาป้อนข้อมูลให้ครบถ้วน", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = "UPDATE Product SET Name=@Name,IDCat=@IDCat,Description=@Description,UnitPrice=@UnitPrice,Status=@Status " +
                                       "WHERE IDP=@IDP";
                    comm.Parameters.Add("@IDP",SqlDbType.Int).Value = txtIDP.Text;
                    comm.Parameters.Add("@Name", SqlDbType.NVarChar, 50).Value = txtName.Text;
                    comm.Parameters.Add("@IDCat", SqlDbType.Int).Value = cbCategories.SelectedIndex + 1;
                    comm.Parameters.Add("@Description", SqlDbType.NVarChar, 100).Value = txtDescription.Text;
                    comm.Parameters.Add("@UnitPrice", SqlDbType.Money).Value = txtUnitPrice.Text;
                    bool status;
                    if (cbStatus.SelectedIndex == 0)
                    {
                        status = true;
                    }
                    else
                    {
                        status = false;
                    }
                    comm.Parameters.Add("@Status", SqlDbType.Bit).Value = status;
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    if (comm.ExecuteNonQuery() == 1)
                    {
                        int CurrentRow = dgvProduct.CurrentRow.Index;
                        dgvProduct.Rows[CurrentRow].Cells[1].Value = txtName.Text;
                        dgvProduct.Rows[CurrentRow].Cells[2].Value = txtDescription.Text;
                        dgvProduct.Rows[CurrentRow].Cells[3].Value = txtUnitPrice.Text;
                        if (cbStatus.SelectedIndex == 0)
                        {
                            dgvProduct.Rows[CurrentRow].Cells[4].Value = true;
                        }
                        else
                        {
                            dgvProduct.Rows[CurrentRow].Cells[4].Value = false;
                        }
                        MessageBox.Show("แก้ไขเรียบร้อยแล้ว", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("ต้องการลบข้อมูลหรือไม่?","กรุณายืนยัน",MessageBoxButtons.OKCancel,MessageBoxIcon.Question);
            if (dr == DialogResult.OK)
            {
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = "DELETE FROM Product WHERE IDP=@IDP";
                    comm.Parameters.Add("@IDP", SqlDbType.Int).Value = txtIDP.Text;
                    if (conn.State != ConnectionState.Open)
                    {
                        conn.Open();
                    }
                    if (comm.ExecuteNonQuery() == 1)
                    {
                        int CurrentRow = dgvProduct.CurrentRow.Index;
                        dgvProduct.Rows.RemoveAt(CurrentRow);
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
