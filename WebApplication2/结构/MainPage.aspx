<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainPage.aspx.cs" Inherits="WebApplication2.MainPage" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>主页面</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f2f5;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }

        .container {
            background-color: #fff;
            padding: 2rem;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            width: 350px;
            text-align: center;
        }

        .container h2 {
            margin-bottom: 20px;
        }

        .btn {
            width: 100%;
            padding: 10px;
            border: none;
            background-color: #007bff;
            color: #fff;
            font-size: 16px;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #0056b3;
        }

        .message {
            margin-top: 15px;
            color: red;
        }

        .role-options {
            margin-top: 20px;
            display: flex;
            flex-direction: column;
        }

        .role-options a {
            margin: 10px 0;
            padding: 10px;
            background-color: #28a745;
            color: white;
            text-decoration: none;
            border-radius: 5px;
            text-align: center;
        }

        .role-options a:hover {
            background-color: #218838;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>欢迎来到勤工助学管理系统</h2>

            <!-- 根据身份显示不同内容 -->
            <div class="role-options">
                <!-- 假设身份信息存储在Session中 -->
                <asp:Label ID="lblRoleMessage" runat="server" CssClass="message" Visible="false"></asp:Label>

                <asp:Label ID="lblIdentity" runat="server" Text="身份：" CssClass="message"></asp:Label>

                <asp:Button ID="btnStudent" runat="server" Text="学生操作" CssClass="btn" OnClick="btnStudent_Click" Visible="false" />
                <asp:Button ID="btnTeacher" runat="server" Text="教师操作" CssClass="btn" OnClick="btnTeacher_Click" Visible="false" />

                <asp:Button ID="btnLogout" runat="server" Text="退出登录" CssClass="btn" OnClick="btnLogout_Click" />
            </div>
        </div>
    </form>
</body>
</html>
