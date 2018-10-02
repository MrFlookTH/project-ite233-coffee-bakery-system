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

namespace Project_ITE233
{
    public partial class FormLogin : Form
    {
        SqlConnection conn = new SqlConnection();
        private bool bool_canlogin = false;
        public bool Canlogin
        {
            get { return bool_canlogin; }
        }
        public FormLogin()
        {
            InitializeComponent();
            conn.ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Coffee_MilkTakeYouBack;User ID=sa; Password=2321";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("กรุณาใส่ Username และ Password","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand comm = new SqlCommand();
                comm.Connection = conn;
                comm.CommandType = CommandType.Text;
                comm.CommandText = "SELECT * FROM Owner WHERE Username = @user AND Password = @pass";
                comm.Parameters.Add("@user", SqlDbType.VarChar, 50).Value = txtUsername.Text;
                comm.Parameters.Add("@pass", SqlDbType.VarChar, 50).Value = txtPassword.Text;
                if (conn.State != ConnectionState.Open)
                {
                    Cursor.Current = Cursors.WaitCursor;
                    conn.Open();
                } 
                SqlDataReader reader = comm.ExecuteReader();
                if (reader.HasRows)
                {
                    bool_canlogin = true;
                    conn.Close();
                    Cursor.Current = Cursors.Default;
                    this.Close();                  
                }
                else
                {
                    MessageBox.Show("Username หรือ Password ไม่่ถูกต้อง","",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    conn.Close();
                    Cursor.Current = Cursors.Default;
                    reader.Close();   
                }           
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormLogin_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void FormLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
