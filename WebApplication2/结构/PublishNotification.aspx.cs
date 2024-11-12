using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace WebApplication2
{
    public partial class PublishNotification : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        // 发布通知按钮点击事件
        protected void btnPublish_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            string content = txtContent.Text;
            string userId = txtUserId.Text;

            if (PublishNotificationToDB(title, content, userId))
            {
                lblMessage.Text = "通知发布成功!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblMessage.Text = "发布失败，请重试。";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        // 插入通知到数据库
        private bool PublishNotificationToDB(string title, string content, string userId)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            // SQL 插入语句
            string query = "INSERT INTO Notifications (title, content, user_id) VALUES (@title, @content, @user_id)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@content", content);
                cmd.Parameters.AddWithValue("@user_id", userId);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    // 异常处理：可以记录日志或者其他处理方式
                    return false;
                }
            }
        }
    }
}
