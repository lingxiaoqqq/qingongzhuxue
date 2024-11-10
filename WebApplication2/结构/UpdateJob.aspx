<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UpdateJob.aspx.cs" Inherits="WebApplication2.UpdateJob" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>更新岗位</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="text-center mb-4">更新岗位</h2>
            
            <!-- 岗位标题 -->
            <div class="mb-3">
                <label for="txtTitle" class="form-label">岗位标题</label>
                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" required></asp:TextBox>
            </div>
            
            <!-- 岗位描述 -->
            <div class="mb-3">
                <label for="txtDescription" class="form-label">岗位描述</label>
                <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" required></asp:TextBox>
            </div>
            
            <!-- 岗位地点 -->
            <div class="mb-3">
                <label for="txtPlace" class="form-label">岗位地点</label>
                <asp:TextBox ID="txtPlace" runat="server" CssClass="form-control" required></asp:TextBox>
            </div>
            
            <!-- 工作时间 -->
            <div class="mb-3">
                <label for="txtTime" class="form-label">工作时间</label>
                <asp:TextBox ID="txtTime" runat="server" CssClass="form-control" required></asp:TextBox>
            </div>
            
            <!-- 更新按钮 -->
            <asp:Button ID="btnUpdate" runat="server" Text="更新" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
        </div>
    </form>
</body>
</html>
