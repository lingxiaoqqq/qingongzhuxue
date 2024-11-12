<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="WebApplication2.Statistics" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>数据统计</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css" rel="stylesheet">
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
            width: 80%;
            max-width: 1200px;
        }

        h1, h2 {
            text-align: center;
            margin-bottom: 20px;
        }

        .gridview-container {
            margin-bottom: 30px;
        }

        .gridview-container .table {
            margin-top: 20px;
            border-collapse: collapse;
        }

        .stats-container {
            margin-top: 30px;
        }

        .stats-container p {
            font-size: 16px;
        }

        .stats-container label {
            font-weight: bold;
        }

        /* 按钮样式 */
        .btn-back {
            display: block;
            margin: 20px auto;
            width: 200px;
            padding: 10px;
            text-align: center;
            font-size: 18px;
            background-color: #007bff;
            color: white;
            border-radius: 5px;
            text-decoration: none;
        }

        .btn-back:hover {
            background-color: #0056b3;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>岗位需求与申请情况统计</h1>



            <div class="gridview-container">
                <h2>岗位需求统计</h2>
                <asp:GridView ID="gvJobDemandStatistics" runat="server" AutoGenerateColumns="True" CssClass="table table-striped" />
            </div>

            <div class="gridview-container">
                <h2>申请状态统计</h2>
                <asp:GridView ID="gvApplicationStatusStatistics" runat="server" AutoGenerateColumns="True" CssClass="table table-striped" />
            </div>

            <div class="stats-container">
                <h2>申请人数与通过率</h2>
                <p>总申请人数：<asp:Label ID="lblTotalApplications" runat="server" CssClass="badge bg-info"></asp:Label></p>
                <p>审核通过人数：<asp:Label ID="lblApprovedApplications" runat="server" CssClass="badge bg-success"></asp:Label></p>
                <p>审核通过率：<asp:Label ID="lblApprovalRate" runat="server" CssClass="badge bg-warning"></asp:Label></p>
            </div>
                        <!-- 返回按钮 -->
            <a href="TeacherDashboard.aspx" class="btn-back">返回教师工作台</a>
        </div>
    </form>
</body>
</html>
