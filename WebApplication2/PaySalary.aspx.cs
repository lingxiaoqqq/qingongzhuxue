using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication2
{
    public partial class PaySalary : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadApprovedApplications();
            }
        }

        private void LoadApprovedApplications()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Help;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // 请替换为实际连接字符串
            string query = "SELECT a.ApplicationId, s.Name AS StudentName, j.Title AS JobTitle, a.ApplicationDate FROM Applications a " +
                           "JOIN Students s ON a.UserId = s.Id " +
                           "JOIN Jobs j ON a.JobId = j.Id WHERE a.Status = 1"; // Status = 1 表示已批准

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);
                rptApprovedApplications.DataSource = dt;
                rptApprovedApplications.DataBind();
            }
        }
    }
}
