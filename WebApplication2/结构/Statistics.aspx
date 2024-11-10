<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Statistics.aspx.cs" Inherits="WebApplication2.Statistics" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <title>数据统计</title>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <h1>岗位需求与申请情况统计</h1>
            
            <h2>岗位需求统计</h2>
            <asp:GridView ID="gvJobDemandStatistics" runat="server" AutoGenerateColumns="True" />

            <h2>申请状态统计</h2>
            <asp:GridView ID="gvApplicationStatusStatistics" runat="server" AutoGenerateColumns="True" />

            <h2>申请人数与通过率</h2>
            <p>总申请人数：<asp:Label ID="lblTotalApplications" runat="server"></asp:Label></p>
            <p>审核通过人数：<asp:Label ID="lblApprovedApplications" runat="server"></asp:Label></p>
            <p>审核通过率：<asp:Label ID="lblApprovalRate" runat="server"></asp:Label></p>
        </div>
    </form>
</body>
</html>
