<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobDetail.aspx.cs" Inherits="WebApplication2.JobDetail" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>岗位详情</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="text-center mb-4">岗位详情</h2>

            <!-- 岗位标题 -->
            <div class="mb-3">
                <label for="lblTitle" class="form-label">岗位标题</label>
                <asp:Label ID="lblTitle" runat="server" CssClass="form-control"></asp:Label>
            </div>

            <!-- 岗位描述 -->
            <div class="mb-3">
                <label for="lblDescription" class="form-label">岗位描述</label>
                <asp:Label ID="lblDescription" runat="server" CssClass="form-control"></asp:Label>
            </div>

            <!-- 岗位地点 -->
            <div class="mb-3">
                <label for="lblPlace" class="form-label">岗位地点</label>
                <asp:Label ID="lblPlace" runat="server" CssClass="form-control"></asp:Label>
            </div>

            <!-- 工作时间 -->
            <div class="mb-3">
                <label for="lblTime" class="form-label">工作时间</label>
                <asp:Label ID="lblTime" runat="server" CssClass="form-control"></asp:Label>
            </div>

            <!-- 返回按钮 -->
            <asp:Button ID="btnBack" runat="server" Text="返回" CssClass="btn btn-secondary" OnClick="btnBack_Click" />
        </div>
    </form>
</body>
</html>
