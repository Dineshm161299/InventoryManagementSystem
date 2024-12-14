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
    public partial class ProductModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DINESH\SQLEXPRESS;Initial Catalog=INVdb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            comboCate.Items.Clear();
            cmd = new SqlCommand("SELECT CatName FROM CategoryTb", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                
                comboCate.Items.Add( dr[0].ToString());

            }
            dr.Close();
            conn.Close();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you save this Product..?", "Saveing Product Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO ProductTb(PName,PQty,PPrice,PDescription,PCategory) VALUES(@PName,@PQty,@PPrice,@PDescription,@PCategory)", conn);
                    cmd.Parameters.AddWithValue("@PName", textProductName.Text);
                    cmd.Parameters.AddWithValue("@PQty",Convert.ToInt16(textQuantity.Text));
                    cmd.Parameters.AddWithValue("@PPrice", Convert.ToInt32(textPrice.Text));
                    cmd.Parameters.AddWithValue("@PDescription", textDescription.Text);
                    cmd.Parameters.AddWithValue("@PCategory", comboCate.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Product has been successfully saved", "Save");
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
            textProductName.Clear();
            textQuantity.Clear();
            textPrice.Clear();
            textDescription.Clear();
            comboCate.Items.Clear();
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you Update this Product..?", "Update Product Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("UPDATE ProductTb SET PName = @PName,PQty = @PQty,PPrice = @PPrice,PDescription = @PDescription,PCategory = @PCategory WHERE PId LIKE '" + labPId.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@PName", textProductName.Text);
                    cmd.Parameters.AddWithValue("@PQty", Convert.ToInt16(textQuantity.Text));
                    cmd.Parameters.AddWithValue("@PPrice", Convert.ToInt32(textPrice.Text));
                    cmd.Parameters.AddWithValue("@PDescription", textDescription.Text);
                    cmd.Parameters.AddWithValue("@PCategory", comboCate.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Product has been successfully updated ");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
