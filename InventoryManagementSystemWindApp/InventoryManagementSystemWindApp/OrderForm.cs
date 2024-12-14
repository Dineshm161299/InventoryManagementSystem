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
    public partial class OrderForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DINESH\SQLEXPRESS;Initial Catalog=INVdb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;   
        public OrderForm()
        {
            InitializeComponent();
            LoadOrder();
        }
        public void LoadOrder()
        {
            double total = 0;
            int i = 0;
            dgvOrder.Rows.Clear();
            cmd = new SqlCommand("SELECT OrderId, OrderDate, O.ProID, P.PName, O.CustId, C.CName, Qty, Price, Total FROM OrderTb AS O JOIN CustomerTb AS C ON O.CustId=C.CId JOIN ProductTb AS P ON O.ProId=P.PId WHERE CONCAT(OrderId, OrderDate, O.ProID, P.PName, O.CustId, C.CName, Qty, Price) LIKE '%"+textsearch.Text+"%' ", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, dr[0].ToString(),Convert.ToDateTime(dr[1].ToString()).ToString("dd/MM/yyyy"), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString());
                total += Convert.ToInt32(dr[8].ToString());  
            }
            dr.Close();
            conn.Close();

            lblQty.Text = i.ToString();
            lblTAmt.Text = total.ToString();    
        }
        private void AddCustomerbtn_Click(object sender, EventArgs e)
        {
            OrderModuleForm orderModuleForm = new OrderModuleForm();
            orderModuleForm.ShowDialog();
            LoadOrder();
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;

            if (colName == "Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this Order ?", "Delete Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("DELETE FROM OrderTb WHERE OrderId LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Record has been Successfully Deleted!", "Delete");

                    cmd = new SqlCommand("UPDATE ProductTb SET PQty = (PQty+@PQty) WHERE PId LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", conn);
                    cmd.Parameters.AddWithValue("@PQty", Convert.ToInt16(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString()));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                LoadOrder();
            }

        }

        private void textsearch_TextChanged(object sender, EventArgs e)
        {
            LoadOrder();
        }

       
    }
}
