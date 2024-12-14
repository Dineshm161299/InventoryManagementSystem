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
    public partial class OrderModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=DINESH\SQLEXPRESS;Initial Catalog=INVdb;Integrated Security=True");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader dr;
        int qty = 0;

        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        private void pictureBoxExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM CustomerTb WHERE CONCAT(CId,CName) LIKE '%"+textSearchCust.Text+"%' ", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, dr[0].ToString(), dr[1].ToString());

            }
            dr.Close();
            conn.Close();

        }
        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM ProductTb WHERE CONCAT(PName, PPrice, PDescription, PCategory) LIKE '%" + textSearchProt.Text + "%' ", conn);
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

        private void textSearchCust_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void textSearchProt_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textCustId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            textCustName.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();

        }
        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textProId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            textProName.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            textPrice.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            GetOty();
            if (Convert.ToInt32(textQty.Value)>qty)
            {
                MessageBox.Show("Instock Quantity is not enough!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textQty.Value = textQty.Value - 1;
                return;
            }
            if (Convert.ToInt32(textQty.Value) > 0)
            {
                int total = Convert.ToInt16(textPrice.Text) * Convert.ToInt32(textQty.Value);
                textTotal.Text = total.ToString();
            }
        }

        private void btnOrdInsert_Click(object sender, EventArgs e)
        {

            try
            {
                if (textCustId.Text == "")
                {
                    MessageBox.Show("Pleace Select Customer!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (textProId.Text == "")
                {
                    MessageBox.Show("Pleace Select Product!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
               
                if (MessageBox.Show("Are you sure you want to insert this Order this !", "Saveing Order Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("INSERT INTO OrderTb(OrderDate,ProId,CustID,Qty,Price,Total) VALUES(@OrderDate,@ProId,@CustID,@Qty,@Price,@Total)", conn);
                    cmd.Parameters.AddWithValue("@OrderDate", dtOrder.Value);
                    cmd.Parameters.AddWithValue("@ProId", Convert.ToInt16(textProId.Text));
                    cmd.Parameters.AddWithValue("@CustID", Convert.ToInt16(textCustId.Text));
                    cmd.Parameters.AddWithValue("@Qty", Convert.ToInt16(textQty.Value));
                    cmd.Parameters.AddWithValue("@Price", Convert.ToInt16(textPrice.Text));
                    cmd.Parameters.AddWithValue("@Total", Convert.ToInt16(textTotal.Text));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Order has been successfully Inserted", "Save");


                    cmd = new SqlCommand("UPDATE ProductTb SET PQty = (PQty-@PQty) WHERE PId LIKE '" + textProId.Text + "' ", conn);
                    cmd.Parameters.AddWithValue("@PQty", Convert.ToInt16(textQty.Value));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Clear();
                    LoadProduct();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            textCustId.Clear(); 
            textCustName.Clear();

            textProId.Clear();
            textProName.Clear();    

            textPrice.Clear();  
            textQty.Value = 0;
            textTotal.Clear();  
            dtOrder.Value = DateTime.Now;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        public void GetOty()
        {
            cmd = new SqlCommand("SELECT PQty FROM ProductTb WHERE PId = '" + textProId.Text + "' ", conn);
            conn.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
              
                qty = Convert.ToInt32(dr[0].ToString());

            }
            dr.Close();
            conn.Close();
        }

        
    }
}
