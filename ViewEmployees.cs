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

namespace EmployeeManagementSystem
{
    public partial class ViewEmployees : Form
    {
        public ViewEmployees()
        {
            InitializeComponent();
        }

        //  FORM LOAD
        private void ViewEmployees_Load(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        // LOAD METHOD (OUTSIDE LOAD EVENT)
        private void LoadEmployees()
        {
            string connectionString =
                System.Configuration.ConfigurationManager
                .ConnectionStrings["EmployeeDBConnection"]
                .ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlDataAdapter da =
                    new SqlDataAdapter("SELECT * FROM Employees", con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                dgEmployees.DataSource = dt;
            }
        }

        // DELETE BUTTON
        private void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            if (dgEmployees.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an employee to delete.");
                return;
            }

            int employeeId = Convert.ToInt32(
                dgEmployees.SelectedRows[0].Cells["EmployeeID"].Value);

            DialogResult result = MessageBox.Show(
                "Are you sure you want to delete this employee?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                string connectionString =
                    System.Configuration.ConfigurationManager
                    .ConnectionStrings["EmployeeDBConnection"]
                    .ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM Employees WHERE EmployeeID=@id";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", employeeId);

                    con.Open();
                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Employee deleted successfully!");

                //  Refresh Grid
                LoadEmployees();
            }
        }

        private void dgEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEditEmployee_Click(object sender, EventArgs e) //Edit button code.
        {
            if (dgEmployees.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgEmployees.SelectedRows[0].Cells["EmployeeID"].Value);
                string name = dgEmployees.SelectedRows[0].Cells["Name"].Value.ToString();
                string dept = dgEmployees.SelectedRows[0].Cells["Department"].Value.ToString();
                decimal salary = Convert.ToDecimal(dgEmployees.SelectedRows[0].Cells["Salary"].Value);

                EditEmployee editForm = new EditEmployee(id, name, dept, salary);
                editForm.ShowDialog();

                LoadEmployees(); // refresh grid
            }
            else
            {
                MessageBox.Show("Please select an employee first.");
            }

        }
    }
}
