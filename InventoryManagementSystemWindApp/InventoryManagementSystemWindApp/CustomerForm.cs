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
    public partial class CustomerForm : Form
    {

        SqlConnection conn = new SqlConnection(@"Data Source=DINESH\SQLEXPRESS;Initial Catalog=INVdb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public CustomerForm()
        {
            InitializeComponent();
            LoadCustomer();


        }
        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM CustomerTb", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString());

            }
            dr.Close();
            conn.Close();

        }

        private void AddCustomerbtn_Click(object sender, EventArgs e)
        {
            CustomerModuleForm form = new CustomerModuleForm();
            form.btnSave.Enabled = true;
            form.btnUpdate.Enabled = false;
            form.ShowDialog();
            LoadCustomer();

        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvCustomer.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CustomerModuleForm customerModuleForm = new CustomerModuleForm();
                customerModuleForm.labCId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
                customerModuleForm.textCname.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();
                customerModuleForm.textCPhone.Text = dgvCustomer.Rows[e.RowIndex].Cells[3].Value.ToString();

                customerModuleForm.btnSave.Enabled = false;
                customerModuleForm.btnUpdate.Enabled = true;
                customerModuleForm.ShowDialog();

            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Customer ?", "Delete Record ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM CustomerTb WHERE CId LIKE '" + dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been Successfully Deleted!","Delete");

                }
                LoadCustomer();

            }
        }
    }
}





