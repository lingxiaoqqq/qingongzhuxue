using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace WebApplication2
{
    public partial class ApproveApplications : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadApplications();
            }
        }

        private void LoadApplications()
        {
            string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Help;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // 请替换为实际连接字符串
            string query = @"
        SELECT 
            a.id AS ApplicationId,
            s.username AS StudentUsername,   -- 修改为 username
            j.title AS JobTitle,
            a.status AS ApplicationStatus,
            a.payment AS ApplicationPayment,
            a.time AS ApplicationDate
        FROM 
            Applications a
        JOIN 
            Students s ON a.user_id = s.id  -- 关联学生表
        JOIN 
            Jobs j ON a.job_id = j.id
        WHERE 
            a.status = 0";  // Status = 0 表示待审核

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                System.Data.DataTable dt = new System.Data.DataTable();
                adapter.Fill(dt);
                rptApplications.DataSource = dt;
                rptApplications.DataBind();
            }
        }

    }
}
