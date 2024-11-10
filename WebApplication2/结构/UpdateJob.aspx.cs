using System;
using System.Data.SqlClient;

namespace WebApplication2
{
    public partial class UpdateJob : System.Web.UI.Page
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Help;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"; // 请替换为你的数据库连接字符串

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["jobId"] != null)
                {
                    int jobId = int.Parse(Request.QueryString["jobId"]);
                    LoadJobDetails(jobId);
                }
                else
                {
                    Response.Redirect("TeacherDashboard.aspx"); // 如果没有岗位ID，返回到教师工作台
                }
            }
        }

        // 加载岗位详情
        private void LoadJobDetails(int jobId)
        {
            string query = "SELECT title, description, place, time FROM Jobs WHERE id = @jobId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@jobId", jobId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    txtTitle.Text = reader["title"].ToString();
                    txtDescription.Text = reader["description"].ToString();
                    txtPlace.Text = reader["place"].ToString();
                    txtTime.Text = reader["time"].ToString();
                }
                conn.Close();
            }
        }

        // 更新岗位信息
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int jobId = int.Parse(Request.QueryString["jobId"]);
            string query = "UPDATE Jobs SET title = @title, description = @description, place = @place, time = @time WHERE id = @jobId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", txtTitle.Text);
                cmd.Parameters.AddWithValue("@description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@place", txtPlace.Text);
                cmd.Parameters.AddWithValue("@time", txtTime.Text);
                cmd.Parameters.AddWithValue("@jobId", jobId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            // 更新完成后跳转到教师工作台
            Response.Redirect("TeacherDashboard.aspx");
        }
    }
}
