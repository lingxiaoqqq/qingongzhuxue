using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class Statistics : System.Web.UI.Page
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Help;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStatistics();  // 加载统计数据
            }
        }

        private void LoadStatistics()
        {
            LoadJobDemandStatistics();         // 岗位需求统计
            LoadApplicationStatusStatistics(); // 申请状态统计
            LoadApprovalRateStatistics();      // 申请人数与通过率统计
        }

        private void LoadJobDemandStatistics()
        {
            string query = @"
                SELECT j.title, COUNT(a.id) AS ApplicationCount
                FROM Jobs j
                LEFT JOIN Applications a ON j.id = a.job_id
                GROUP BY j.title
                ORDER BY ApplicationCount DESC";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                gvJobDemandStatistics.DataSource = dt;
                gvJobDemandStatistics.DataBind();

                conn.Close();
            }
        }

        private void LoadApplicationStatusStatistics()
        {
            string query = @"
                SELECT 
                    CASE 
                        WHEN status = 0 THEN N'未审核'
                        WHEN status = 1 THEN N'审核通过'
                        WHEN status = 2 THEN N'审核未通过'
                        WHEN status = 3 THEN N'取消申请'
                        WHEN status = 4 THEN N'已发放酬金'
                        ELSE N'未知状态'
                    END AS StatusText,
                    COUNT(*) AS StatusCount
                FROM Applications
                GROUP BY status";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                gvApplicationStatusStatistics.DataSource = dt;
                gvApplicationStatusStatistics.DataBind();

                conn.Close();
            }
        }

        private void LoadApprovalRateStatistics()
        {
            string query = @"
        SELECT 
            COUNT(*) AS TotalApplications,
            SUM(CASE WHEN status = 1 OR status = 4 THEN 1 ELSE 0 END) AS ApprovedApplications,
            (CAST(SUM(CASE WHEN status = 1 OR status = 4 THEN 1 ELSE 0 END) AS FLOAT) / COUNT(*)) * 100 AS ApprovalRate
        FROM Applications";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblTotalApplications.Text = reader["TotalApplications"].ToString();
                    lblApprovedApplications.Text = reader["ApprovedApplications"].ToString();
                    lblApprovalRate.Text = $"{Convert.ToDouble(reader["ApprovalRate"]):F2}%";
                }

                conn.Close();
            }
        }

    }
}
