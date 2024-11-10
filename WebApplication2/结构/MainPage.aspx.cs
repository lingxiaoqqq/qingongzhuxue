using System;
using System.Web;

namespace WebApplication2
{
    public partial class MainPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 假设用户身份存储在Session中
            string identity = Session["identity"]?.ToString();

            if (identity == null)
            {
                lblRoleMessage.Text = "请先登录！";
                lblRoleMessage.Visible = true;
                return;
            }

            lblRoleMessage.Visible = false;

            if (identity == "Student")
            {
                lblIdentity.Text = "身份：学生";
                btnStudent.Visible = true;
                Response.Redirect("StudentDashboard.aspx");
            }
            else if (identity == "Teacher")
            {
                lblIdentity.Text = "身份：教师";
                btnTeacher.Visible = true;
                Response.Redirect("TeacherDashboard.aspx");
            }
            else
            {
                lblRoleMessage.Text = "身份错误！";
                lblRoleMessage.Visible = true;
            }
        }

        protected void btnStudent_Click(object sender, EventArgs e)
        {
            // 跳转到学生的主页面或操作界面
            Response.Redirect("StudentDashboard.aspx");
        }

        protected void btnTeacher_Click(object sender, EventArgs e)
        {
            // 跳转到教师的主页面或操作界面
            Response.Redirect("TeacherDashboard.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // 退出登录，清空Session
            Session.Clear();
            Response.Redirect("Login.aspx");
        }
    }
}
