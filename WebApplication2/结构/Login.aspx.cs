using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace WebApplication2
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string hashPassword = HashPassword(password);

            if (ValidateUser(username, hashPassword, ddlIdentity.SelectedValue))
            {
                lblMessage.Text = "Login successful!";
                lblMessage.ForeColor = System.Drawing.Color.Green;
                Response.Redirect("MainPage.aspx");
            }
            else
            {
                lblMessage.Text = "Invalid username or password.";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private bool ValidateUser(string username, string hashedPassword, string identity)
        {
            string table = identity == "Student" ? "Students" : "Teachers";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                string query = $"SELECT id FROM {table} WHERE username = @username AND password = @password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", hashedPassword);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    if (reader.Read())
                    {
                        int userId = Convert.ToInt32(reader["id"]);
                        Session["UserId"] = userId;
                        Session["username"] = username;
                        Session["identity"] = identity;
                        return true;
                    }
                }
            }
            return false;
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
