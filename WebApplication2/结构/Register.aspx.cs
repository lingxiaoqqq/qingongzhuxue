using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace WebApplication2
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;  // 获取邮箱
            string identity = ddlIdentity.SelectedValue;

            if (RegisterUser(username, password, phone, email, identity))
            {
                lblMessage.Text = "注册成功!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                Response.Redirect("Login.aspx");
            }
            else
            {
                lblMessage.Text = "注册失败，用户名或邮箱可能已存在。";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool RegisterUser(string username, string password, string phone, string email, string identity)
        {
            string table = identity == "Student" ? "Students" : "Teachers";
            string hashedPassword = HashPassword(password);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string query = $"INSERT INTO {table} (username, password, phone, email) VALUES (@username, @password, @phone, @email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Parameters.AddWithValue("@email", email);  // 插入邮箱字段

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    // Handle the exception (e.g., log it)
                    return false;
                }
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
