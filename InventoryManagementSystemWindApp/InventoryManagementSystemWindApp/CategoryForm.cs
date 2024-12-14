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
    public partial class CategoryForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DINESH\SQLEXPRESS;Initial Catalog=INVdb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public CategoryForm()
        {
            InitializeComponent();
            LoadCategory();
        }
        public void LoadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM CategoryTb", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, dr[0].ToString(), dr[1].ToString());

            }
            dr.Close();
            conn.Close();

        }

        private void AddUserbtn_Click(object sender, EventArgs e)
        {
            CategoryModuleForm form = new CategoryModuleForm();
            form.btnSave.Enabled = true;
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
            LoadCategory();
        }

        private void dgvCategory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoryModuleForm categoryModuleForm = new CategoryModuleForm();
                categoryModuleForm.labCatId.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                categoryModuleForm.textCatname.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();

                categoryModuleForm.btnSave.Enabled = false;
                categoryModuleForm.btnUpdate.Enabled = true;
                categoryModuleForm.ShowDialog();

            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Category ?", "Delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM CategoryTb WHERE CatId LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been Successfully Deleted!", "Delete");

                }
                LoadCategory();

            }
        }
    }
}
