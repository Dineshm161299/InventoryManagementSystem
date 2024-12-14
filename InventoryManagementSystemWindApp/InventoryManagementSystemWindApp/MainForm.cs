using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InventoryManagementSystemWindApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        private Form activeForm ; 
        private void openChildForm(Form ChildForm)
        {
            if (activeForm != null)
                activeForm.Close();
               activeForm = ChildForm;
            ChildForm.TopLevel = false;
            ChildForm.FormBorderStyle = FormBorderStyle.None;
            ChildForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(ChildForm);
            panelMain.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
            

        }

        private void UserButton_Click(object sender, EventArgs e)
        {
            openChildForm(new UsersForm());
        }

        private void CustomerButton_Click(object sender, EventArgs e)
        {
            openChildForm(new CustomerForm());
        }

        private void CategoryButton_Click(object sender, EventArgs e)
        {
            openChildForm(new CategoryForm());
        }

        private void ProductButton_Click(object sender, EventArgs e)
        {
            openChildForm(new ProductForm());
        }

        private void OrderButton_Click(object sender, EventArgs e)
        {
            openChildForm(new OrderForm());
        }

       
    }
}
