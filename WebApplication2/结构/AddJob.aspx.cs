using System;
using System.Data.SqlClient;

namespace WebApplication2
{
    public partial class AddJob : System.Web.UI.Page
    {
        string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Help;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected void Page_Load(object sender, EventArgs e)
        {
            // 如果需要任何页面初始化操作，可以在这里添加
        }

        // 保存按钮点击事件
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;  // 使用 TextBox 的 Text 属性
            string description = txtDescription.Text;
            string place = txtPlace.Text;
            string time = txtTime.Text;

            // 插入岗位数据到数据库
            string query = "INSERT INTO Jobs (Title, Description, Place, Time) VALUES (@Title, @Description, @Place, @Time)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Title", title);
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Place", place);
                cmd.Parameters.AddWithValue("@Time", time);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    // 成功后跳转到教师工作台页面
                    Response.Redirect("TeacherDashboard.aspx");
                }
                catch (Exception ex)
                {
                    // 这里可以添加错误处理逻辑
                    Response.Write("<script>alert('保存失败: " + ex.Message + "');</script>");
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
