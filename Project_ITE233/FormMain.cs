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
    public partial class FormMain : Form
    {
        SqlConnection conn = new SqlConnection();
        private string TableNo;
        List<Button> buttonList = new List<Button>();

        public FormMain()
        {
            InitializeComponent();
            //conn.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Coffee_MilkTakeYouBack;User ID=sa; Password=2321";
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString.ToString();

            buttonList.Add(btnTable01);
            buttonList.Add(btnTable02);
            buttonList.Add(btnTable03);
            buttonList.Add(btnTable04);
            buttonList.Add(btnTable05);
            buttonList.Add(btnTable06);
            buttonList.Add(btnTable07);
            buttonList.Add(btnTable08);
            buttonList.Add(btnTable09);
            buttonList.Add(btnTable10);
            UpdateTableStatus();
        }

        private void ShowLogin()
        {
            FormLogin obj_login = new FormLogin();
            do
            {
                if (obj_login.ShowDialog() == DialogResult.Cancel)
                {
                    Application.Exit();
                    break;
                }
            } while (!obj_login.Canlogin);
            this.Show();
        }

        private void Main_Shown(object sender, EventArgs e)
        {
            //this.WindowState = FormWindowState.Maximized;
            this.Hide();
            ShowLogin();
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //แสดงรายการสินค้า
            if (tabControl1.SelectedTab == tabProduct)
            {
                if (TableNo != null)
                {
                    try
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        SqlCommand comm = new SqlCommand();
                        DataTable dt = new DataTable();
                        comm.Connection = conn;
                        comm.CommandType = CommandType.Text;
                        comm.CommandText = "SELECT IDP รหัส,Name ชื่อสินค้า,UnitPrice ราคา FROM Product WHERE Status='True'";
                        if(conn.State != ConnectionState.Open)
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(comm);
                        da.Fill(dt);
                        dgvProduct.DataSource = dt;
                        dgvProduct.Columns[0].Width = 40;
                        dgvProduct.Columns[1].Width = 250;
                        dgvProduct.Columns[2].DefaultCellStyle.Format = "0.00##";
                        dgvProduct.Focus();
                        btnToast.Enabled = true;
                        btnCoffee.Enabled = true;
                        btnPasta.Enabled = true;
                        btnDrink.Enabled = true;
                        btnExtra.Enabled = true;
                    }
                    catch
                    {
                        MessageBox.Show("Can't connect to database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        conn.Close();
                        Cursor.Current = Cursors.Default;
                    }
                }
                else
                {
                    MessageBox.Show("กรุณาเลือกโต๊ะก่อนครับ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tabControl1.SelectedTab = tabTable;
                }
            }
            else if (tabControl1.SelectedTab == tabTable)
            {
                btnToast.Enabled = false;
                btnCoffee.Enabled = false;
                btnPasta.Enabled = false;
                btnDrink.Enabled = false;
                btnExtra.Enabled = false;
            }
        }

        private void btnToast_Click(object sender, EventArgs e)
        {
            if (TableNo != null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    SqlCommand comm = new SqlCommand();
                    DataTable dt = new DataTable();
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT IDP รหัส,Name ชื่อสินค้า,UnitPrice ราคา FROM Product " +
                                       "WHERE IDCat = 1 AND Status='True'";
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    da.Fill(dt);
                    dgvProduct.DataSource = dt;
                    dgvProduct.Columns[0].Width = 40;
                    dgvProduct.Columns[1].Width = 250;
                    dgvProduct.Columns[2].DefaultCellStyle.Format = "0.00##";
                    btnToast.Enabled = false;
                    btnCoffee.Enabled = true;
                    btnPasta.Enabled = true;
                    btnDrink.Enabled = true;
                    btnExtra.Enabled = true;
                    dgvProduct.Focus();
                }
                catch
                {
                    MessageBox.Show("Can't connect to database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกโต๊ะก่อนครับ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedTab = tabTable;
            }
        }

        private void btnCoffee_Click(object sender, EventArgs e)
        {
            if (TableNo != null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    SqlCommand comm = new SqlCommand();
                    DataTable dt = new DataTable();
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT IDP รหัส,Name ชื่อสินค้า,UnitPrice ราคา FROM Product " +
                                       "WHERE IDCat = 2 AND Status='True'";
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    da.Fill(dt);
                    dgvProduct.DataSource = dt;
                    dgvProduct.Columns[0].Width = 40;
                    dgvProduct.Columns[1].Width = 250;
                    dgvProduct.Columns[2].DefaultCellStyle.Format = "0.00##";
                    btnToast.Enabled = true;
                    btnCoffee.Enabled = false;
                    btnPasta.Enabled = true;
                    btnDrink.Enabled = true;
                    btnExtra.Enabled = true;
                    dgvProduct.Focus();
                }
                catch
                {
                    MessageBox.Show("Can't connect to database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกโต๊ะก่อนครับ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedTab = tabTable;
            }
        }

        private void btnPasta_Click(object sender, EventArgs e)
        {
            if (TableNo != null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    SqlCommand comm = new SqlCommand();
                    DataTable dt = new DataTable();
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT IDP รหัส,Name ชื่อสินค้า,UnitPrice ราคา FROM Product " +
                                       "WHERE IDCat = 3 AND Status='True'";
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    da.Fill(dt);
                    dgvProduct.DataSource = dt;
                    dgvProduct.Columns[0].Width = 40;
                    dgvProduct.Columns[1].Width = 250;
                    dgvProduct.Columns[2].DefaultCellStyle.Format = "0.00##";
                    btnToast.Enabled = true;
                    btnCoffee.Enabled = true;
                    btnPasta.Enabled = false;
                    btnDrink.Enabled = true;
                    btnExtra.Enabled = true;
                    dgvProduct.Focus();
                }
                catch
                {
                    MessageBox.Show("Can't connect to database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกโต๊ะก่อนครับ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedTab = tabTable;
            }
        }

        private void btnDrink_Click(object sender, EventArgs e)
        {
            if (TableNo != null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    SqlCommand comm = new SqlCommand();
                    DataTable dt = new DataTable();
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT IDP รหัส,Name ชื่อสินค้า,UnitPrice ราคา FROM Product " +
                                       "WHERE IDCat = 4 AND Status='True'";
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    da.Fill(dt);
                    dgvProduct.DataSource = dt;
                    dgvProduct.Columns[0].Width = 40;
                    dgvProduct.Columns[1].Width = 250;
                    dgvProduct.Columns[2].DefaultCellStyle.Format = "0.00##";
                    btnToast.Enabled = true;
                    btnCoffee.Enabled = true;
                    btnPasta.Enabled = true;
                    btnDrink.Enabled = false;
                    btnExtra.Enabled = true;
                    dgvProduct.Focus();
                }
                catch
                {
                    MessageBox.Show("Can't connect to database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกโต๊ะก่อนครับ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedTab = tabTable;
            }
        }

        private void btnExtra_Click(object sender, EventArgs e)
        {
            if (TableNo != null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    SqlCommand comm = new SqlCommand();
                    DataTable dt = new DataTable();
                    comm.Connection = conn;
                    comm.CommandType = CommandType.Text;
                    comm.CommandText = "SELECT IDP รหัส,Name ชื่อสินค้า,UnitPrice ราคา FROM Product " +
                                       "WHERE IDCat = 5 AND Status='True'";
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(comm);
                    da.Fill(dt);
                    dgvProduct.DataSource = dt;
                    dgvProduct.Columns[0].Width = 40;
                    dgvProduct.Columns[1].Width = 250;
                    dgvProduct.Columns[2].DefaultCellStyle.Format = "0.00##";
                    btnToast.Enabled = true;
                    btnCoffee.Enabled = true;
                    btnPasta.Enabled = true;
                    btnDrink.Enabled = true;
                    btnExtra.Enabled = false;
                    dgvProduct.Focus();
                }
                catch
                {
                    MessageBox.Show("Can't connect to database", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                    Cursor.Current = Cursors.Default;
                }
            }
            else
            {
                MessageBox.Show("กรุณาเลือกโต๊ะก่อนครับ", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                tabControl1.SelectedTab = tabTable;
            }
        }

        private void btnTableReset()
        {
            btnTable01.Enabled = true;
            btnTable02.Enabled = true;
            btnTable03.Enabled = true;
            btnTable04.Enabled = true;
            btnTable05.Enabled = true;
            btnTable06.Enabled = true;
            btnTable07.Enabled = true;
            btnTable08.Enabled = true;
            btnTable09.Enabled = true;
            btnTable10.Enabled = true;
            UpdateTableStatus();
        }

        private void ResetAll()
        {
            btnTableReset();
            labelCusNo.Text = "";
            labelTableNo.Text = "";
            labelTotal.Text = "0.00";
            labelTotal2.Text = "0.00";
            labelDate.Text = "n/a";
            labelOrderID.Text = "n/a";
            dgvOrder.DataSource = null;
            dgvProduct.DataSource = null;
            btnShowProduct.Enabled = false;
            tabControl1.SelectedTab = tabTable;
            btnDiscount.Enabled = false;
            btnEdit.Enabled = false;
            btnRemove.Enabled = false;
            btnFirst.Enabled = false;
            btnBack.Enabled = false;
            btnForward.Enabled = false;
            btnLast.Enabled = false;
            TableNo = null;
            btnCancel.Enabled = false;
            btnResetOrder.Enabled = false;
            btnMoveTable.Enabled = false;
            btnBill.Enabled = false;
            btnGetMoney.Enabled = false;
        }

        private void NewCustomer(int CusNo)
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "INSERT INTO [Order](DateTime,TableNo,CusNo) VALUES(GETDATE(),@TableNo,@CusNo)";
            comm.Parameters.Add("@TableNo", SqlDbType.VarChar, 2).Value = TableNo;
            comm.Parameters.Add("@CusNo", SqlDbType.Int, 2).Value = CusNo;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                conn.Open();
                comm.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error "+ ex,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                comm.Dispose();
                UpdateTableStatus();
                Cursor.Current = Cursors.Default;
            }
        }

        private void ShowTableData()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "SELECT DateTime,TableNo,CusNo,Total,IDO FROM [Order] WHERE TableNo = @TableNo AND Bill = 'false'";
                comm.Parameters.Add("@TableNo", SqlDbType.VarChar, 2).Value = TableNo;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlDataReader reader = comm.ExecuteReader();
                reader.Read();
                labelTableNo.Text = "โต๊ะ " + reader[1];
                labelCusNo.Text = "จำนวน " + reader[2] + " ท่าน";
                labelDate.Text = reader[0].ToString();
                labelOrderID.Text = reader[4].ToString();
                double total = Convert.ToDouble(reader[3]);
                labelTotal.Text = total.ToString("N2");
                labelTotal2.Text = total.ToString("N2");
                conn.Close();
                btnCancel.Enabled = true;
                btnMoveTable.Enabled = true;
                ShowOrderDetail();
                Cursor.Current = Cursors.Default;
            }
            catch(Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาด "+ex ,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }        
        }

        private void ShowOrderDetail()
        {
            try
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandText = "SELECT P.IDP รหัส,P.Name รายการ,OD.QTY จำนวน,P.UnitPrice ราคา,OD.Total ราคารวม FROM Order_Detail OD " +
                                   "INNER JOIN Product P ON P.IDP = OD.IDP " +
                                   "INNER JOIN [Order] O ON O.IDO = OD.IDO " +
                                   "WHERE O.TableNo = @TableNo AND Bill = 'false'";
                comm.Parameters.Add("@TableNo", SqlDbType.VarChar, 2).Value = TableNo;
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(comm);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvOrder.DataSource = dt;
                comm.Dispose();
                dgvOrder.Columns[0].Width = 50;
                dgvOrder.Columns[1].Width = 280;
                dgvOrder.Columns[3].DefaultCellStyle.Format = "0.00##";
                dgvOrder.Columns[4].DefaultCellStyle.Format = "0.00##";
                if (dgvOrder.Rows.Count > 0)
                {
                    dgvOrder.Rows[dgvOrder.Rows.Count - 1].Selected = true;
                    //btnShowProduct.Enabled = true;
                    //btnDiscount.Enabled = true;
                    btnEdit.Enabled = true;
                    btnRemove.Enabled = true;
                    btnFirst.Enabled = true;
                    btnBack.Enabled = true;
                    btnForward.Enabled = true;
                    btnLast.Enabled = true;
                    btnResetOrder.Enabled = true;
                    btnBill.Enabled = true;
                    btnGetMoney.Enabled = true;
                }
                else
                {
                    btnShowProduct.Enabled = false;
                    btnDiscount.Enabled = false;
                    btnEdit.Enabled = false;
                    btnRemove.Enabled = false;
                    btnFirst.Enabled = false;
                    btnBack.Enabled = false;
                    btnForward.Enabled = false;
                    btnLast.Enabled = false;
                    btnResetOrder.Enabled = false;
                    btnBill.Enabled = false;
                    btnGetMoney.Enabled = false;
                }
            }
            finally
            {
                conn.Close();
            }
        }

        private void InsertOrderDetail(int QTY)
        {
            Cursor.Current = Cursors.WaitCursor;
            int selectedRow = dgvProduct.CurrentRow.Index;
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "SELECT * FROM Order_Detail WHERE IDO=@IDO AND IDP=@IDP";
            comm.Parameters.Add("@IDO", SqlDbType.Int).Value = labelOrderID.Text;
            comm.Parameters.Add("@IDP", SqlDbType.Int).Value = dgvProduct[0, selectedRow].Value;
            comm.Parameters.Add("@QTY", SqlDbType.Int).Value = QTY;
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Close();
                foreach (DataGridViewRow row in this.dgvOrder.Rows)
                {              
                    if (Convert.ToInt32(row.Cells[0].Value) == Convert.ToInt32(dgvProduct[0, selectedRow].Value))
                    {                      
                        int OldQTY = Convert.ToInt32(row.Cells[2].Value);
                        comm.CommandText = "UPDATE Order_Detail SET QTY=@NewQTY, Total=@NewTotal WHERE IDO=@IDO AND IDP=@IDP";
                        int NewQTY = OldQTY + QTY;
                        comm.Parameters.Add("@NewQTY", SqlDbType.Int).Value = NewQTY;
                        double Total = Convert.ToDouble(dgvProduct[2, selectedRow].Value) * Convert.ToDouble(NewQTY);
                        comm.Parameters.Add("@NewTotal", SqlDbType.Money).Value = Total;
                        comm.ExecuteNonQuery();
                        comm.Dispose();
                    }
                }
            }
            else
            {
                reader.Close();
                comm.CommandText = "INSERT INTO Order_Detail(IDO,IDP,QTY,Total) VALUES(@IDO,@IDP,@QTY,@Total)";
                double Total = Convert.ToDouble(dgvProduct[2, selectedRow].Value) * Convert.ToDouble(QTY);
                comm.Parameters.Add("@Total", SqlDbType.Money).Value = Total;
                comm.ExecuteNonQuery();
                comm.Dispose();
            }
            conn.Close();
            ShowTableData();
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            double Total = 0;
            foreach (DataGridViewRow row in this.dgvOrder.Rows)
            {
                Total += Convert.ToDouble(row.Cells[4].Value);
            }
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "UPDATE [Order] SET Total=@Total WHERE IDO=@IDO";
            comm.Parameters.Add("@Total",SqlDbType.Money).Value = Total;
            comm.Parameters.Add("@IDO", SqlDbType.Int).Value = Convert.ToInt32(labelOrderID.Text);
            conn.Open();
            comm.ExecuteNonQuery();
            conn.Close();
            labelTotal.Text = Total.ToString("N2");
            labelTotal2.Text = Total.ToString("N2");
        }

        private void UpdateTableStatus()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                for (int i = 0; i < buttonList.Count; i++)
                {
                    comm.CommandText = "SELECT COUNT(*) FROM [Order] WHERE TableNo=@TableNo AND Bill='false'";
                    comm.Parameters.Add("@TableNo", SqlDbType.NVarChar, 2).Value = string.Format("{0:00}",i+1);
                    SqlDataReader reader = comm.ExecuteReader();
                    reader.Read();
                    if (Convert.ToInt16(reader[0]) == 1)
                    {
                        buttonList[i].BackColor = System.Drawing.Color.DarkSalmon;
                    }
                    else
                    {
                        buttonList[i].BackColor = System.Drawing.Color.Transparent;
                    }
                    reader.Close();
                    comm.Parameters.Clear();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
                Cursor.Current = Cursors.Default;
            }
        }


        private bool IsTableAvailable(string TableNo)
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandType = CommandType.Text;
            comm.CommandText = "SELECT * FROM [Order] WHERE TableNo = @TableNo AND bill = 'false'";
            comm.Parameters.Add("@TableNo", SqlDbType.VarChar, 2).Value = TableNo;
            conn.Open();
            SqlDataReader reader = comm.ExecuteReader();      
            if (reader.HasRows)
            {
                conn.Close(); 
                return false;
            }
            else
            {
                conn.Close(); 
                return true;
            }
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            if (btn.Name == "btnTable01")
            {
                if (IsTableAvailable("01"))
                {
                    FormCustomerNo frmCusNo = new FormCustomerNo();
                    if (frmCusNo.ShowDialog() == DialogResult.OK)
                    {
                        TableNo = "01";
                        NewCustomer(frmCusNo.CusNo);
                        btnTableReset();
                        btn.Enabled = false;
                        frmCusNo.Dispose();
                        ShowTableData();
                    }
                }
                else
                {
                    TableNo = "01";
                    ShowTableData();
                    btnTableReset();
                    btn.Enabled = false;
                }              
            }
            else if (btn.Name == "btnTable02")
            {
                if (IsTableAvailable("02"))
                {
                    FormCustomerNo frmCusNo = new FormCustomerNo();
                    if (frmCusNo.ShowDialog() == DialogResult.OK)
                    {
                        TableNo = "02";
                        NewCustomer(frmCusNo.CusNo);
                        btnTableReset();
                        btn.Enabled = false;
                        frmCusNo.Dispose();
                        ShowTableData();
                    }
                }
                else
                {
                    TableNo = "02";
                    ShowTableData();
                    btnTableReset();
                    btn.Enabled = false;
                }             
            }
            else if (btn.Name == "btnTable03")
            {
                if (IsTableAvailable("03"))
                {
                    FormCustomerNo frmCusNo = new FormCustomerNo();
                    if (frmCusNo.ShowDialog() == DialogResult.OK)
                    {
                        TableNo = "03";
                        NewCustomer(frmCusNo.CusNo);
                        btnTableReset();
                        btn.Enabled = false;
                        frmCusNo.Dispose();
                        ShowTableData();
                    }
                }
                else
                {
                    TableNo = "03";
                    ShowTableData();
                    btnTableReset();
                    btn.Enabled = false;
                }             
            }
            else if (btn.Name == "btnTable04")
            {
                if (IsTableAvailable("04"))
                {
                    FormCustomerNo frmCusNo = new FormCustomerNo();
                    if (frmCusNo.ShowDialog() == DialogResult.OK)
                    {
                        TableNo = "04";
                        NewCustomer(frmCusNo.CusNo);
                        btnTableReset();
                        btn.Enabled = false;
                        frmCusNo.Dispose();
                        ShowTableData();
                    }
                }
                else
                {
                    TableNo = "04";
                    ShowTableData();
                    btnTableReset();
                    btn.Enabled = false;
                }             
            }
            else if (btn.Name == "btnTable05")
            {
                if (IsTableAvailable("05"))
                {
                    FormCustomerNo frmCusNo = new FormCustomerNo();
                    if (frmCusNo.ShowDialog() == DialogResult.OK)
                    {
                        TableNo = "05";
                        NewCustomer(frmCusNo.CusNo);
                        btnTableReset();
                        btn.Enabled = false;
                        frmCusNo.Dispose();
                        ShowTableData();
                    }
                }
                else
                {
                    TableNo = "05";
                    ShowTableData();
                    btnTableReset();
                    btn.Enabled = false;
                }             
            }
            else if (btn.Name == "btnTable06")
            {
                if (IsTableAvailable("06"))
                {
                    FormCustomerNo frmCusNo = new FormCustomerNo();
                    if (frmCusNo.ShowDialog() == DialogResult.OK)
                    {
                        TableNo = "06";
                        NewCustomer(frmCusNo.CusNo);
                        btnTableReset();
                        btn.Enabled = false;
                        frmCusNo.Dispose();
                        ShowTableData();
                    }
                }
                else
                {
                    TableNo = "06";
                    ShowTableData();
                    btnTableReset();
                    btn.Enabled = false;
                }             
            }
            else if (btn.Name == "btnTable07")
            {
                if (IsTableAvailable("07"))
                {
                    FormCustomerNo frmCusNo = new FormCustomerNo();
                    if (frmCusNo.ShowDialog() == DialogResult.OK)
                    {
                        TableNo = "07";
                        NewCustomer(frmCusNo.CusNo);
                        btnTableReset();
                        btn.Enabled = false;
                        frmCusNo.Dispose();
                        ShowTableData();
                    }
                }
                else
                {
                    TableNo = "07";
                    ShowTableData();
                    btnTableReset();
                    btn.Enabled = false;
                }             
            }
            else if (btn.Name == "btnTable08")
            {
                if (IsTableAvailable("08"))
                {
                    FormCustomerNo frmCusNo = new FormCustomerNo();
                    if (frmCusNo.ShowDialog() == DialogResult.OK)
                    {
                        TableNo = "08";
                        NewCustomer(frmCusNo.CusNo);
                        btnTableReset();
                        btn.Enabled = false;
                        frmCusNo.Dispose();
                        ShowTableData();
                    }
                }
                else
                {
                    TableNo = "08";
                    ShowTableData();
                    btnTableReset();
                    btn.Enabled = false;
                }             
            }
            else if (btn.Name == "btnTable09")
            {
                if (IsTableAvailable("09"))
                {
                    FormCustomerNo frmCusNo = new FormCustomerNo();
                    if (frmCusNo.ShowDialog() == DialogResult.OK)
                    {
                        TableNo = "09";
                        NewCustomer(frmCusNo.CusNo);
                        btnTableReset();
                        btn.Enabled = false;
                        frmCusNo.Dispose();
                        ShowTableData();
                    }
                }
                else
                {
                    TableNo = "09";
                    ShowTableData();
                    btnTableReset();
                    btn.Enabled = false;
                }             
            }
            else if (btn.Name == "btnTable10")
            {
                if (IsTableAvailable("10"))
                {
                    FormCustomerNo frmCusNo = new FormCustomerNo();
                    if (frmCusNo.ShowDialog() == DialogResult.OK)
                    {
                        TableNo = "10";
                        NewCustomer(frmCusNo.CusNo);
                        btnTableReset();
                        btn.Enabled = false;
                        frmCusNo.Dispose();
                        ShowTableData();
                    }
                }
                else
                {
                    TableNo = "10";
                    ShowTableData();
                    btnTableReset();
                    btn.Enabled = false;
                }             
            }
        }

        private void dgvProduct_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            FormAddOrder frmAddOrder = new FormAddOrder();
            if (frmAddOrder.ShowDialog() == DialogResult.OK)
            {
                InsertOrderDetail(frmAddOrder._QTY);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            if (dgvOrder.Rows.Count != 0)
            {
                dgvOrder.Rows[0].Selected = true;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (dgvOrder.Rows.Count != 0)
            {
                int currentRow = dgvOrder.SelectedRows[0].Index;
                if (currentRow > 0)
                {     
                    dgvOrder.Rows[currentRow - 1].Selected = true;
                }
            }           
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            if (dgvOrder.Rows.Count != 0)
            {
                int currentRow = dgvOrder.SelectedRows[0].Index;
                if (currentRow < dgvOrder.Rows.Count - 1)
                {
                    dgvOrder.Rows[currentRow + 1].Selected = true;
                }
            }   
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (dgvOrder.Rows.Count != 0)
            {
                dgvOrder.Rows[dgvOrder.Rows.Count - 1].Selected = true;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dgvOrder.Rows.Count > 0)
            {
                DialogResult dr = MessageBox.Show("คุณต้องการลบหรือไม่?","กรุณายืนยัน",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = "DELETE FROM Order_Detail WHERE IDO=@IDO AND IDP=@IDP";
                    comm.Parameters.Add("@IDO", SqlDbType.Int).Value = Convert.ToInt32(labelOrderID.Text);
                    int currentRow = dgvOrder.SelectedRows[0].Index;
                    int IDP = Convert.ToInt32(dgvOrder[0, currentRow].Value);
                    comm.Parameters.Add("@IDP", SqlDbType.Int).Value = IDP;
                    try
                    {
                        conn.Open();
                        comm.ExecuteNonQuery();
                    }
                    finally
                    {
                        conn.Close();
                        ShowTableData();
                        UpdateTotal();
                    }
                }               
            }
            else
            {
                MessageBox.Show("ไม่มีข้อมูล","",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }      
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("คุณต้องยกเลิกการขายหรือไม่?","กรุณายืนยัน",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = "DELETE FROM [Order] WHERE IDO=@IDO";
                    comm.Parameters.Add("@IDO", SqlDbType.Int).Value = Convert.ToInt32(labelOrderID.Text);
                    conn.Open();
                    comm.ExecuteNonQuery();
                    ResetAll();
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        private void btnResetOrder_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("คุณต้องการล้างรายการทั้งหมดหรือไม่?","กรุณายืนยัน",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandText = "DELETE FROM Order_Detail WHERE IDO=@IDO";
                    comm.Parameters.Add("@IDO", SqlDbType.Int).Value = Convert.ToInt32(labelOrderID.Text);
                    conn.Open();
                    comm.ExecuteNonQuery();
                    ShowTableData();
                    UpdateTotal();
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

        private void btnBill_Click(object sender, EventArgs e)
        {
            ReportReceipt rpReceipt = new ReportReceipt();
            rpReceipt.ShowDialog();
        }

        private void ProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormProduct frmProduct = new FormProduct();
            frmProduct.ShowDialog();
        }

        private void CustomerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCustomer frmCustomer = new FormCustomer();
            frmCustomer.ShowDialog();
        }

        private void btnMoveTable_Click(object sender, EventArgs e)
        {
            FormMoveTable frmMoveTable = new FormMoveTable(TableNo, buttonList);
            if (frmMoveTable.ShowDialog() == DialogResult.OK)
            {
                UpdateTableStatus();
                ResetAll();
                MessageBox.Show("ย้ายโต๊ะเรียบร้อยแล้ว", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnGetMoney_Click(object sender, EventArgs e)
        {
            SqlCommand comm = new SqlCommand();
            comm.Connection = conn;
            comm.CommandText = "UPDATE [Order] SET Bill='True' WHERE IDO=@IDO";
            comm.Parameters.Add("@IDO",SqlDbType.Int).Value = labelOrderID.Text;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }
            if (comm.ExecuteNonQuery() == 1)
            {
                MessageBox.Show("รับเงินเรียบร้อยแล้ว", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetAll();
                UpdateTableStatus();
            }
        }

        private void OrderCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSearchOrder frmSearchOrder = new FormSearchOrder();
            if (frmSearchOrder.ShowDialog() == DialogResult.OK)
            {
                FormOrderCheck frmOrderCheck = new FormOrderCheck(frmSearchOrder.OrderID);
                frmOrderCheck.ShowDialog();
            }
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ShowLogin();
        }

    }
}
