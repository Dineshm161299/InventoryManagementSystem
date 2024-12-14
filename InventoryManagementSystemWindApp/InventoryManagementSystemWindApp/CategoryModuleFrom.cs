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

namespace InventoryManagementSystemWindApp
{
    public partial class CategoryModuleForm : Form
    {

        SqlConnection conn = new SqlConnection(@"Data Source=DINESH\SQLEXPRESS;Initial Catalog=INVdb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        public CategoryModuleForm()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(textCatname.Text))
                {
                    MessageBox.Show("Please enter input in the fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                if (MessageBox.Show("Are you save this Category..?", "Saveing Category Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO CategoryTb(CatName) VALUES(@catName)", conn);
                    cmd.Parameters.AddWithValue("@catName", textCatname.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Category has been successfully saved", "Save");
                    Clear();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you Update this Category..?", "Update Category Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE CategoryTb SET CatName = @catName WHERE CatName LIKE '" + labCatId.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@catName", textCatname.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Category has been successfully updated ", "Update");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void Clear()
        {
            textCatname.Clear();
           
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
    }
}
