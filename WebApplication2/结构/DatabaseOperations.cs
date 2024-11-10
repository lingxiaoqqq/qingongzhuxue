using System;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2
{
    public class DatabaseOperations
    {
        private string connectionString = "your_connection_string_here";

        #region 获取所有通知
        public DataTable GetNotifications(int userId)
        {
            string query = "SELECT [id], [title], [content], [user_id] FROM [dbo].[Notifications] WHERE [user_id] = @userId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                da.SelectCommand.Parameters.AddWithValue("@userId", userId);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        #endregion

        #region 创建新的通知
        public void CreateNotification(string title, string content, int userId)
        {
            string query = "INSERT INTO [dbo].[Notifications] ([title], [content], [user_id]) VALUES (@title, @content, @userId)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@userId", userId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 获取所有岗位
        public DataTable GetJobs()
        {
            string query = "SELECT [id], [title], [description], [requirements], [time], [place] FROM [dbo].[Jobs]";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }
        #endregion

        #region 创建新的岗位
        public void CreateJob(string title, string description, string requirements, string time, string place)
        {
            string query = "INSERT INTO [dbo].[Jobs] ([title], [description], [requirements], [time], [place]) VALUES (@title, @description, @requirements, @time, @place)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.Parameters.AddWithValue("@requirements", requirements);
                cmd.Parameters.AddWithValue("@time", time);
                cmd.Parameters.AddWithValue("@place", place);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 学生申请岗位
        public void ApplyForJob(int studentId, int jobId, int status, int payment)
        {
            string query = "INSERT INTO [dbo].[Applications] ([user_id], [job_id], [status], [payment]) VALUES (@userId, @jobId, @status, @payment)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@userId", studentId);
                cmd.Parameters.AddWithValue("@jobId", jobId);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@payment", payment);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 更新申请状态
        public void UpdateApplicationStatus(int applicationId, int status)
        {
            string query = "UPDATE [dbo].[Applications] SET [status] = @status WHERE [id] = @applicationId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@applicationId", applicationId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 更新支付状态
        public void UpdatePaymentStatus(int applicationId, int payment)
        {
            string query = "UPDATE [dbo].[Applications] SET [payment] = @payment WHERE [id] = @applicationId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@payment", payment);
                cmd.Parameters.AddWithValue("@applicationId", applicationId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 删除岗位
        public void DeleteJob(int jobId)
        {
            string query = "DELETE FROM [dbo].[Jobs] WHERE [id] = @jobId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@jobId", jobId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion

        #region 删除通知
        public void DeleteNotification(int notificationId)
        {
            string query = "DELETE FROM [dbo].[Notifications] WHERE [id] = @notificationId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@notificationId", notificationId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        #endregion
    }
}
