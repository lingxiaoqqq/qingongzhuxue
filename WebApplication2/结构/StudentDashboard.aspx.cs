using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class StudentDashboard : System.Web.UI.Page
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Help;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadJobs();
                LoadApplicationHistory(GetCurrentStudentId());
            }
        }

        private void LoadJobs()
        {
            int studentId = GetCurrentStudentId();
            string filterStatus = ddlFilterStatus.SelectedValue;
            string sortOrder = ddlSortOrder.SelectedValue;

            // 获取分页相关参数
            int pageIndex = gvJobs.PageIndex;
            int pageSize = gvJobs.PageSize;
            int offset = pageIndex * pageSize;

            string query = @"
        SELECT j.id, j.title, j.description, j.place, j.time, a.status
        FROM Jobs j
        LEFT JOIN Applications a ON j.id = a.job_id AND a.user_id = @studentId";

            if (filterStatus != "All")
            {
                query += " WHERE a.status = @status";
            }

            switch (sortOrder)
            {
                case "TimeAsc": query += " ORDER BY j.time ASC"; break;
                case "TimeDesc": query += " ORDER BY j.time DESC"; break;
                case "TitleAsc": query += " ORDER BY j.title ASC"; break;
                case "TitleDesc": query += " ORDER BY j.title DESC"; break;
            }

            query += $" OFFSET {offset} ROWS FETCH NEXT {pageSize} ROWS ONLY";  // 分页查询

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentId", studentId);
                if (filterStatus != "All")
                    cmd.Parameters.AddWithValue("@status", filterStatus);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                gvJobs.DataSource = reader;
                gvJobs.DataBind();
                conn.Close();
            }
        }



        private void LoadApplicationHistory(int studentId)
        {
            int pageIndex = gvApplicationHistory.PageIndex;
            int pageSize = gvApplicationHistory.PageSize;
            int offset = pageIndex * pageSize;

            string query = @"
        SELECT j.title, a.status, a.time, a.id
        FROM Applications a
        JOIN Jobs j ON a.job_id = j.id
        WHERE a.user_id = @studentId
        ORDER BY a.time DESC
        OFFSET @offset ROWS FETCH NEXT @pageSize ROWS ONLY"; // 添加分页查询

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@studentId", studentId);
                cmd.Parameters.AddWithValue("@offset", offset);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    gvApplicationHistory.DataSource = dt;
                    gvApplicationHistory.DataBind();
                }
                else
                {
                    gvApplicationHistory.DataSource = null;
                    gvApplicationHistory.DataBind();
                }
                conn.Close();
            }
        }
        

        protected void gvJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Apply")
            {
                int jobId = Convert.ToInt32(e.CommandArgument);
                int studentId = GetCurrentStudentId();

                if (studentId > 0)
                {
                    if (!IsJobAlreadyApplied(jobId, studentId))
                    {
                        ApplyForJob(studentId, jobId);
                        LoadJobs();
                    }
                    else
                    {
                        Response.Write("<script>alert('您已申请该岗位或状态不可申请');</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('请先登录');</script>");
                }
            }
            else if (e.CommandName == "ViewDetails")
            {
                int jobId = Convert.ToInt32(e.CommandArgument);

                // 跳转到 StudentDetail 页面，并传递 jobId 参数
                Response.Redirect($"StudentDetail.aspx?jobId={jobId}");
            }
        }



        protected void gvApplicationHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CancelApplication")
            {
                // 获取申请记录的ID
                int applicationId = Convert.ToInt32(e.CommandArgument);

                // 调用取消申请的方法
                CancelApplication(applicationId);

                // 刷新岗位列表和申请历史
                LoadJobs();
                LoadApplicationHistory(GetCurrentStudentId());
            }
        }


        private bool IsJobAlreadyApplied(int jobId, int studentId)
        {
            string query = "SELECT COUNT(*) FROM Applications WHERE user_id = @userId AND job_id = @jobId AND status = 0";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", studentId);
                cmd.Parameters.AddWithValue("@jobId", jobId);

                conn.Open();
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();

                return count > 0;
            }
        }

        private void ApplyForJob(int studentId, int jobId)
        {
            string query = "INSERT INTO Applications (user_id, job_id, status, payment, time) VALUES (@user_id, @job_id, 0, 0, GETDATE())";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@user_id", studentId);
                cmd.Parameters.AddWithValue("@job_id", jobId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private void CancelApplication(int applicationId)
        {
            string query = "UPDATE Applications SET status = 3 WHERE id = @applicationId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@applicationId", applicationId);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        private int GetCurrentStudentId()
        {
            // 检查 Session 中是否存在 "UserId"
            if (Session["UserId"] != null)
            {
                // 从 Session 中获取并返回当前登录学生的 ID
                return Convert.ToInt32(Session["UserId"]);
            }
            else
            {
                // 如果 Session 中没有存储 UserId，说明用户没有登录
                // 可以抛出异常或者返回一个默认值 -1（表示未登录）
                return -1;
            }
        }


        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadJobs();
            LoadApplicationHistory(GetCurrentStudentId());
        }

        protected void ddlFilterStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadJobs();
        }

        protected void ddlSortOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadJobs();
        }
        protected void gvJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvJobs.PageIndex = e.NewPageIndex;
            LoadJobs();
        }

        protected void gvApplicationHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvApplicationHistory.PageIndex = e.NewPageIndex;
            LoadApplicationHistory(GetCurrentStudentId());
        }

        protected string GetApplicationStatusText(object status)
        {
            if (status == DBNull.Value)
                return "申请";

            switch (status.ToString())
            {
                case "0": return "未审核中";
                case "1": return "审核通过";
                case "2": return "审核未通过";
                case "3": return "已取消";
                case "4": return "已发放酬金";
                default: return "未知状态";
            }
        }
    }
}
