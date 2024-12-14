using System;
using System.CodeDom;
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
    public partial class UsersForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DINESH\SQLEXPRESS;Initial Catalog=INVdb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        public UsersForm()
        {
            InitializeComponent();
            LoadUser();
        }

        public void LoadUser()
        {
            int i = 0;
            dgvUser.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM UserTb", conn); 
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvUser.Rows.Add(i,dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
                
            }
            dr.Close(); 
            conn.Close();

        }

        private void AddUserbtn_Click(object sender, EventArgs e)
        {
            UserModuleForm userModuleForm = new UserModuleForm();
            userModuleForm.btnSave.Enabled = true;
            userModuleForm.btnUpdate.Enabled = false; 
            userModuleForm.ShowDialog();
            LoadUser();
        }

        private void dgvUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvUser.Columns[e.ColumnIndex].Name;
            if(colName == "Edit")
            {
                UserModuleForm userModuleForm = new UserModuleForm();
                userModuleForm.textUsername.Text = dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString();
                userModuleForm.textfullname.Text = dgvUser.Rows[e.RowIndex].Cells[2].Value.ToString();
                userModuleForm.textPass.Text = dgvUser.Rows[e.RowIndex].Cells[3].Value.ToString();
                userModuleForm.textPhone.Text = dgvUser.Rows[e.RowIndex].Cells[4].Value.ToString();

                userModuleForm.btnSave.Enabled = false;
                userModuleForm.btnUpdate.Enabled = true;
                userModuleForm.textUsername.Enabled = false;
                userModuleForm.ShowDialog();

            }
            else if(colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this User ?","Delete Record",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM UserTb WHERE Username LIKE '" + dgvUser.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been Successfully Deleted!","Delete");

                }
                LoadUser();

            }
        }
    }
}  
