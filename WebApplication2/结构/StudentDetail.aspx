<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDetail.aspx.cs" Inherits="WebApplication2.StudentDetail" %>

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
            
            <!-- 消息提示 -->
            <asp:Label ID="lblMessage" runat="server" CssClass="alert alert-warning" Visible="false"></asp:Label>

            <div class="mb-3">
                <label class="form-label">岗位标题：</label>
                <asp:Label ID="lblTitle" runat="server" CssClass="form-control"></asp:Label>
            </div>

            <div class="mb-3">
                <label class="form-label">岗位描述：</label>
                <asp:Label ID="lblDescription" runat="server" CssClass="form-control"></asp:Label>
            </div>

            <div class="mb-3">
                <label class="form-label">岗位地点：</label>
                <asp:Label ID="lblPlace" runat="server" CssClass="form-control"></asp:Label>
            </div>

            <div class="mb-3">
                <label class="form-label">工作时间：</label>
                <asp:Label ID="lblTime" runat="server" CssClass="form-control"></asp:Label>
            </div>



            <div class="text-center">
                <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" CssClass="btn btn-secondary" />
            </div>
        </div>
    </form>
</body>
</html>
