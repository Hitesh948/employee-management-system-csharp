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
using System.Xml.Linq;

namespace EmployeeManagementSystem
{
    public partial class AddEmployee : Form
    {
        public AddEmployee()
        {
            InitializeComponent();
            AcceptButton = btnSaveEmployee;
            txtEmployeeName.Focus();
        }

        private void btnSaveEmployee_Click(object sender, EventArgs e)
        {
            if (txtEmployeeName.Text == "")
            {
                MessageBox.Show("Please enter employee name.");
                txtEmployeeName.Focus(); // moves cursor back
                return;
            }

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["EmployeeDBConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Employees (Name, Department, Salary) VALUES (@Name, @Department, @Salary)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", txtEmployeeName.Text);
                cmd.Parameters.AddWithValue("@Department", cmbDepartment.Text);
                cmd.Parameters.AddWithValue("@Salary", txtSalary.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Employee Saved Successfully!");
                txtEmployeeName.Clear();
                txtSalary.Clear();
                cmbDepartment.SelectedIndex = -1;
                txtEmployeeName.Focus();

            }

        }

        private void txtEmployeeName_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddEmployee_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only numbers and backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }

        }
    }
}
