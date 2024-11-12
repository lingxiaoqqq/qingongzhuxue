<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishNotification.aspx.cs" Inherits="WebApplication2.PublishNotification" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>发布通知</title>
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
        .input-box textarea {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2>发布通知</h2>
            <div class="input-box">
                <label for="title">标题</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" />
            </div>
            <div class="input-box">
                <label for="content">内容</label>
                <asp:TextBox ID="txtContent" runat="server" TextMode="MultiLine" CssClass="form-control" />
            </div>
            <div class="input-box">
                <label for="user_id">用户ID</label>
                <asp:TextBox ID="txtUserId" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="btnPublish" runat="server" Text="发布通知" CssClass="btn" OnClick="btnPublish_Click" />
            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
        </div>
    </form>
</body>
</html>
