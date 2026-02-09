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
    public partial class EditEmployee : Form
    {//create variables
        int employeeId;

        string connectionString = System.Configuration.ConfigurationManager
            .ConnectionStrings["EmployeeDBConnection"].ConnectionString;
        public EditEmployee(int id, string name, string dept, decimal salary)
        {
            InitializeComponent();

            employeeId = id;

            txtEmployeeName.Text = name;
            cmbDepartment.Text = dept;
            txtSalary.Text = salary.ToString();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateEmployee_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "UPDATE Employees SET Name=@Name, Department=@Dept, Salary=@Salary WHERE EmployeeID=@ID";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Name", txtEmployeeName.Text);
                cmd.Parameters.AddWithValue("@Dept", cmbDepartment.Text);
                cmd.Parameters.AddWithValue("@Salary", txtSalary.Text);
                cmd.Parameters.AddWithValue("@ID", employeeId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Employee updated successfully!");
            this.Close();

        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
