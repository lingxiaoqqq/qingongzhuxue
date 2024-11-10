<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveApplications.aspx.cs" Inherits="WebApplication2.ApproveApplications" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>申请审核 - 勤工助学管理系统</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f0f2f5;
            margin: 0;
            padding: 0;
        }
        .navbar {
            background-color: #007bff;
            padding: 15px;
            color: white;
            text-align: center;
        }
        .navbar a {
            color: white;
            text-decoration: none;
            padding: 10px;
            font-weight: bold;
        }
        .navbar a:hover {
            background-color: #0056b3;
            border-radius: 5px;
        }
        .container {
            margin-top: 20px;
            padding: 30px;
        }
        .card {
            background-color: white;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            padding: 20px;
            margin-bottom: 20px;
        }
        .btn {
            background-color: #007bff;
            color: white;
            padding: 10px 15px;
            border-radius: 5px;
            cursor: pointer;
            text-decoration: none;
        }
        .btn:hover {
            background-color: #0056b3;
        }
        .btn-danger {
            background-color: #dc3545;
        }
        .btn-danger:hover {
            background-color: #c82333;
        }
        .btn-success {
            background-color: #28a745;
        }
        .btn-success:hover {
            background-color: #218838;
        }
    </style>
</head>
<body>
    <div class="navbar">
        <h2>申请审核 - 勤工助学管理系统</h2>
    </div>

    <form id="form1" runat="server">
        <div class="container">
            <div class="card">
                <h3>待审核的申请</h3>
                <table border="1" cellpadding="10" cellspacing="0">
                    <thead>
                        <tr>
                            <th>学生姓名</th>
                            <th>岗位名称</th>
                            <th>申请时间</th>
                            <th>操作</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptApplications" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("StudentName") %></td>
                                    <td><%# Eval("JobTitle") %></td>
                                    <td><%# Eval("ApplicationDate") %></td>
                                    <td>
                                        <a href="ApproveApplication.aspx?id=<%# Eval("ApplicationId") %>" class="btn btn-success">批准</a>
                                        <a href="RejectApplication.aspx?id=<%# Eval("ApplicationId") %>" class="btn btn-danger">拒绝</a>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
        </div>
    </form>
</body>
</html>
