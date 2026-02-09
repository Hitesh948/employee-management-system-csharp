using EmployeeManagementSystem;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class lblTotalEmployeesText : Form
    {
        public lblTotalEmployeesText()
        {
            InitializeComponent();
        }


        private void LoadDashboardData()
        {
            string connectionString =
                ConfigurationManager.ConnectionStrings["EmployeeDBConnection"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Total Employees
                SqlCommand cmd1 = new SqlCommand("SELECT COUNT(*) FROM Employees", con);
                lblTotalEmployees.Text = cmd1.ExecuteScalar().ToString();

                // Departments
                SqlCommand cmd2 = new SqlCommand("SELECT COUNT(DISTINCT Department) FROM Employees", con);
                lblDepartments.Text = cmd2.ExecuteScalar().ToString();

                // Highest Salary
                SqlCommand cmd3 = new SqlCommand("SELECT ISNULL(MAX(Salary),0) FROM Employees", con);
                lalHighestSalary.Text = "₹ " + cmd3.ExecuteScalar().ToString();
            }
        }

        // ⭐ LOAD EVENT
        private void Dashboard_Load(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        // ⭐ AUTO REFRESH AFTER ADD
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            AddEmployee obj = new AddEmployee();
            obj.ShowDialog();   // waits for form to close

            LoadDashboardData(); // ⭐ AUTO refresh
        }

        // ⭐ AUTO REFRESH AFTER DELETE / EDIT
        private void btnViewEmployees_Click(object sender, EventArgs e)
        {
            ViewEmployees view = new ViewEmployees();
            view.ShowDialog();

            LoadDashboardData(); // ⭐ refresh again
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblTotalEmployees_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
    
        }
    }
}
