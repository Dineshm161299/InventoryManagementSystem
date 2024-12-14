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

namespace InventoryManagementSystemWindApp
{
    public partial class UserModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DINESH\SQLEXPRESS;Initial Catalog=INVdb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();   
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            this.Dispose(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if(textPass.Text != txtRePass.Text)
                {
                    MessageBox.Show("Password did not match","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrEmpty(textUsername.Text) && string.IsNullOrEmpty(textfullname.Text) && string.IsNullOrEmpty(textPass.Text) && string.IsNullOrEmpty(textPhone.Text))
                {
                    MessageBox.Show("Please enter input in the fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                if (MessageBox.Show("Are you save this user..?", "Saveing User Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO UserTb(UserName,FullName,Password,Phone) VALUES(@UserName,@FullName,@Password,@Phone)", conn);
                    cmd.Parameters.AddWithValue("@UserName", textUsername.Text);
                    cmd.Parameters.AddWithValue("@FullName", textfullname.Text);
                    cmd.Parameters.AddWithValue("@Password", textPass.Text);
                    cmd.Parameters.AddWithValue("@Phone", textPhone.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("User has been successfully saved","Save");
                    Clear();
                }

            }
            catch (Exception ex) 
            { 
                MessageBox.Show(ex.Message);   
            }   

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;  
        }

        public void Clear()
        {
            textUsername.Clear();
            textfullname.Clear();
            textPass.Clear();
            txtRePass.Clear();  
            textPhone.Clear();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {

                if (textPass.Text != txtRePass.Text)
                {
                    MessageBox.Show("Password did not match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("Are you Update this user..?", "Update User Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE UserTb SET FullName = @FullName,Password = @Password,Phone = @Phone WHERE UserName LIKE '"+ textUsername.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@FullName", textfullname.Text);
                    cmd.Parameters.AddWithValue("@Password", textPass.Text);
                    cmd.Parameters.AddWithValue("@Phone", textPhone.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("User has been successfully updated ");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       
    }
}
