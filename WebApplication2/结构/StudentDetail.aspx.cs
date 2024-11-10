using System;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2
{
    public partial class StudentDetail : System.Web.UI.Page
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Help;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string jobId = Request.QueryString["jobId"];
                if (!string.IsNullOrEmpty(jobId))
                {
                    LoadJobDetail(Convert.ToInt32(jobId));
                }
            }
        }

        // 根据岗位ID加载岗位详细信息
        private void LoadJobDetail(int jobId)
        {
            string query = "SELECT * FROM Jobs WHERE id = @JobId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@JobId", jobId);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblTitle.Text = row["Title"].ToString();
                    lblDescription.Text = row["Description"].ToString();
                    lblPlace.Text = row["Place"].ToString();
                    lblTime.Text = row["Time"].ToString();

                }
                else
                {
                    // 如果没有找到该岗位，提示用户
                    lblMessage.Text = "岗位信息不存在。";
                }
            }
        }

        // 返回按钮点击事件
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("StudentDashboard.aspx");
        }
    }
}
