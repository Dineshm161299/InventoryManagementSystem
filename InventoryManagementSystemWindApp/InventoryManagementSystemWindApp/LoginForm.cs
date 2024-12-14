using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace InventoryManagementSystemWindApp
{
    public partial class LoginForm : System.Windows.Forms.Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DINESH\SQLEXPRESS;Initial Catalog=INVdb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void checkBoxPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxPass.Checked == false)
            {
                textPass.UseSystemPasswordChar = true;
            }
            else
            {
                textPass.UseSystemPasswordChar = false;
            }
        }

        private void labClear_Click(object sender, EventArgs e)
        {
            textName.Clear();
            textPass.Clear();   
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are You Exit LoginPage","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("SELECT * FROM UserTb WHERE UserName = @UserName AND Password = @Password", conn);
                cmd.Parameters.AddWithValue("@UserName", textName.Text);
                cmd.Parameters.AddWithValue("@Password", textPass.Text);
                conn.Open();
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    MessageBox.Show("Welcome " + dr["FullName"].ToString() +"", "Access Granted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm mainForm = new MainForm();
                    this.Hide();
                    mainForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid UserName Or Password!", "Access Denited", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
