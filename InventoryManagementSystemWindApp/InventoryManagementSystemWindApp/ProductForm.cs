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
    public partial class ProductForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DINESH\SQLEXPRESS;Initial Catalog=INVdb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();


        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM ProductTb WHERE CONCAT(PName, PPrice, PDescription, PCategory) LIKE '%"+textsearch.Text+"%' ", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString());

            }
            dr.Close();
            conn.Close();

        }
        private void AddCustomerbtn_Click(object sender, EventArgs e)
        {
            ProductModuleForm productModuleForm = new ProductModuleForm();
            productModuleForm.btnSave.Enabled = true;
            productModuleForm.btnUpdate.Enabled = false;
            productModuleForm.ShowDialog();
            LoadProduct();



        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm productModuleForm = new ProductModuleForm();
                productModuleForm.labPId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                productModuleForm.textProductName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                productModuleForm.textQuantity.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                productModuleForm.textPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                productModuleForm.textDescription.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                productModuleForm.comboCate.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();

                productModuleForm.btnSave.Enabled = false;
                productModuleForm.btnUpdate.Enabled = true;
                productModuleForm.ShowDialog();

            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Product ?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM ProductTb WHERE PId LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been Successfully Deleted!", "Delete");

                }
                LoadProduct();

            }
        }

        private void textsearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }    

}
