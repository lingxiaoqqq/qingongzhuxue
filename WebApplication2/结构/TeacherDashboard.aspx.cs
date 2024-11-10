using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class TeacherDashboard : System.Web.UI.Page
    {
        // 数据库连接字符串
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Help;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadJobs();
                LoadApplications();
            }
        }

        // 刷新和查看统计页面事件
        protected void btnViewStatistics_Click(object sender, EventArgs e) => Response.Redirect("Statistics.aspx");

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadJobs();
            LoadApplications();
        }

        // 加载岗位信息
        private void LoadJobs()
        {
            string query = "SELECT * FROM Jobs";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                gvJobs.DataSource = dt;
                gvJobs.DataBind();
            }
        }

        protected void gvJobs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvJobs.PageIndex = e.NewPageIndex;
            LoadJobs();  // 刷新加载数据
        }


        // 加载申请信息
        private void LoadApplications()
        {
            string query = @"
        SELECT a.id AS ApplicationId, s.username AS StudentUsername, j.title AS JobTitle, 
               a.status AS ApplicationStatus, a.time AS ApplicationTime 
        FROM Applications a
        JOIN Students s ON a.user_id = s.id
        JOIN Jobs j ON a.job_id = j.id";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                gvApplications.DataSource = dt;
                gvApplications.DataBind();
            }
        }

        protected void gvApplications_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvApplications.PageIndex = e.NewPageIndex;
            LoadApplications();  // 刷新加载数据
        }


        // 岗位管理 - 查看、删除和更新岗位
        protected void gvJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ViewJob")
            {
                string jobTitle = e.CommandArgument.ToString();
                Response.Redirect($"JobDetail.aspx?jobTitle={jobTitle}");
            }
            else if (e.CommandName == "DeleteJob")
            {
                int jobId = Convert.ToInt32(e.CommandArgument);
                DeleteJob(jobId);
            }
            else if (e.CommandName == "UpdateJob")
            {
                int jobId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect($"UpdateJob.aspx?jobId={jobId}");
            }
        }

        private void DeleteJob(int jobId)
        {
            string query = "DELETE FROM Jobs WHERE Id = @jobId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@jobId", jobId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadJobs(); // 删除后重新加载岗位数据
        }

        // 申请审核 - 审核操作
        protected void gvApplications_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int applicationId = Convert.ToInt32(e.CommandArgument);

            if (e.CommandName == "Approve")
            {
                UpdateApplicationStatus(applicationId, 1); // 审核通过
            }
            else if (e.CommandName == "Reject")
            {
                UpdateApplicationStatus(applicationId, 2); // 审核拒绝
            }
            else if (e.CommandName == "Pay")
            {
                PayApplication(applicationId); // 发放酬金操作
            }

            LoadApplications(); // 重新加载申请数据
        }

        // 更新申请状态并发送通知邮件
        private void UpdateApplicationStatus(int applicationId, int status)
        {
            string query = "UPDATE Applications SET status = @status WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@id", applicationId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            var studentEmail = GetStudentEmailByApplicationId(applicationId);
            var statusText = GetStatusText(status);
            var applicationDetails = GetApplicationDetails(applicationId);  // 获取申请的详细信息

            if (!string.IsNullOrEmpty(studentEmail))
            {
                SendEmailNotification(studentEmail, statusText, applicationDetails, this);
            }
        }

        // 获取学生的电子邮件
        private string GetStudentEmailByApplicationId(int applicationId)
        {
            string email = null;
            string query = @"
                SELECT s.email
                FROM Applications a
                JOIN Students s ON a.user_id = s.id
                WHERE a.id = @applicationId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@applicationId", applicationId);

                conn.Open();
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    email = result.ToString();
                }
                conn.Close();
            }

            return email;
        }

        // 根据状态获取文本
        private string GetStatusText(int status) =>
            status == 1 ? "审核通过" :
            status == 2 ? "审核未通过" :
            status == 4 ? "酬金已发放" : "未知状态";

        // 获取申请的详细信息
        private string GetApplicationDetails(int applicationId)
        {
            string details = string.Empty;
            string query = @"
                SELECT s.username AS StudentName, j.title AS JobTitle, a.time AS ApplicationTime
                FROM Applications a
                JOIN Students s ON a.user_id = s.id
                JOIN Jobs j ON a.job_id = j.id
                WHERE a.id = @applicationId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@applicationId", applicationId);

                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    string studentName = reader["StudentName"].ToString();
                    string jobTitle = reader["JobTitle"].ToString();
                    DateTime applicationTime = Convert.ToDateTime(reader["ApplicationTime"]);

                    details = $"学生姓名: {studentName}\n职位: {jobTitle}\n申请时间: {applicationTime.ToString("yyyy-MM-dd HH:mm:ss")}";
                }
                conn.Close();
            }

            return details;
        }

        // 发送邮件通知
        public static void SendEmailNotification(string toEmail, string statusText, string applicationDetails, Page page)
        {
            var fromEmail = "2726792014@qq.com";
            var fromPassword = "mknqzpqdlswtdcih"; // 替换为授权码
            var smtpClient = "smtp.qq.com";
            var smtpPort = 587;

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(fromEmail);
                    mail.To.Add(toEmail);
                    mail.Subject = "申请审核结果通知";

                    // 邮件内容包括状态文本和申请详情
                    mail.Body = $"尊敬的学生，\n\n您的申请审核结果为：{statusText}。\n\n" +
                                "申请详情：\n" +
                                $"{applicationDetails}\n\n" +
                                "感谢您的耐心等待。";
                    mail.IsBodyHtml = false;

                    using (SmtpClient smtp = new SmtpClient(smtpClient, smtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(fromEmail, fromPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);

                        page.ClientScript.RegisterStartupScript(page.GetType(), "EmailSuccess", "alert('邮件发送成功！');", true);
                    }
                }
            }
            catch (SmtpException smtpEx)
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "EmailError", $"alert('邮件发送失败: SMTP 错误 - {smtpEx.Message}');", true);
            }
            catch (Exception ex)
            {
                page.ClientScript.RegisterStartupScript(page.GetType(), "EmailError", $"alert('邮件发送失败: 错误 - {ex.Message}');", true);
            }
        }

        protected void btnJobSearch_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtJobSearch.Text.Trim();  // 获取搜索关键词
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                // 使用参数化查询避免SQL注入
                string query = @"
            SELECT * FROM Jobs 
            WHERE Title LIKE @search OR Place LIKE @search";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@search", "%" + searchKeyword + "%");  // 模糊查询
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    gvJobs.DataSource = dt;
                    gvJobs.DataBind();
                }
            }
            else
            {
                LoadJobs(); // 如果搜索框为空，则加载所有岗位
            }
        }
        protected void btnApplicationSearch_Click(object sender, EventArgs e)
        {
            string searchKeyword = txtApplicationSearch.Text.Trim();  // 获取搜索关键词
            if (!string.IsNullOrEmpty(searchKeyword))
            {
                // 使用参数化查询避免SQL注入
                string query = @"
            SELECT a.id AS ApplicationId, s.username AS StudentUsername, j.title AS JobTitle, 
                   a.status AS ApplicationStatus, a.time AS ApplicationTime 
            FROM Applications a
            JOIN Students s ON a.user_id = s.id
            JOIN Jobs j ON a.job_id = j.id
            WHERE s.username LIKE @search OR j.title LIKE @search";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@search", "%" + searchKeyword + "%");  // 模糊查询
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    gvApplications.DataSource = dt;
                    gvApplications.DataBind();
                }
            }
            else
            {
                LoadApplications(); // 如果搜索框为空，则加载所有申请
            }
        }

        // 新增岗位
        protected void btnAddJob_Click(object sender, EventArgs e) => Response.Redirect("AddJob.aspx");

        // 发放酬金
        private void PayApplication(int applicationId)
        {
            string query = "UPDATE Applications SET status = 4 WHERE id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", applicationId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }
}
