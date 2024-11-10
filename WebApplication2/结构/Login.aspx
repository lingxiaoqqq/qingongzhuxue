<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication2.Login" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login Page</title>
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

        .input-box {
            margin-bottom: 15px;
            text-align: left;
        }

        .input-box label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }

        .input-box input,
        .input-box select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
        }

        .btn {
            width: 100%;
            padding: 10px;
            border: none;
            background-color: #28a745;
            color: #fff;
            font-size: 16px;
            border-radius: 5px;
            cursor: pointer;
        }

        .btn:hover {
            background-color: #218838;
        }

        .switch-link {
            margin-top: 15px;
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>登录</h2>
            <div class="input-box">
                <label for="identity">身份</label>
                <asp:DropDownList ID="ddlIdentity" runat="server">
                    <asp:ListItem Text="学生" Value="Student"></asp:ListItem>
                    <asp:ListItem Text="教师" Value="Teacher"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="input-box">
                <label for="username">用户名</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
            </div>
            <div class="input-box">
                <label for="password">密码</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="登录" CssClass="btn" OnClick="btnLogin_Click" />
            <asp:Label ID="lblMessage" runat="server" Text=" " ForeColor="Red"></asp:Label>
            <a href="Register.aspx" class="switch-link">点击注册</a>
        </div>
    </form>
</body>
</html>
