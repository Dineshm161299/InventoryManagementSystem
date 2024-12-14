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
    public partial class CustomerModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DINESH\SQLEXPRESS;Initial Catalog=INVdb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public CustomerModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                if (string.IsNullOrEmpty(textCname.Text) && string.IsNullOrEmpty(textCPhone.Text))
                {
                    MessageBox.Show("Please enter input in the fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                if (MessageBox.Show("Are you save this Customer..?", "Saveing User Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO CustomerTb(CName,CPhone) VALUES(@cName,@cPhone)", conn);
                    cmd.Parameters.AddWithValue("@cName", textCname.Text);
                    cmd.Parameters.AddWithValue("@cPhone", textCPhone.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Customer has been successfully saved","Save");
                    Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            textCname.Clear();  
            textCPhone.Clear(); 
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you Update this Customer..?", "Update Customer Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE CustomerTb SET CName = @CName,CPhone = @CPhone WHERE CId LIKE '" + labCId.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@CName", textCname.Text);
                    cmd.Parameters.AddWithValue("@CPhone", textCPhone.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Customer has been successfully updated ","Update");
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
