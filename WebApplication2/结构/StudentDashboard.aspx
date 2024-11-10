<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="WebApplication2.StudentDashboard" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>学生岗位申请</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="text-center mb-4">岗位申请</h2>

            <asp:Button ID="btnRefresh" runat="server" Text="刷新" CssClass="btn btn-secondary mb-3" OnClick="btnRefresh_Click" />
            
            <!-- 筛选条件 -->
            <asp:DropDownList ID="ddlFilterStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilterStatus_SelectedIndexChanged">
                <asp:ListItem Value="All" Text="所有岗位" />
                <asp:ListItem Value="0" Text="未审核中" />
                <asp:ListItem Value="1" Text="审核通过" />
                <asp:ListItem Value="2" Text="审核未通过" />
                <asp:ListItem Value="4" Text="已发放酬金" />
            </asp:DropDownList>

            <asp:DropDownList ID="ddlSortOrder" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSortOrder_SelectedIndexChanged">
                <asp:ListItem Value="TimeAsc" Text="按发布时间升序" />
                <asp:ListItem Value="TimeDesc" Text="按发布时间降序" />
                <asp:ListItem Value="TitleAsc" Text="按标题升序" />
                <asp:ListItem Value="TitleDesc" Text="按标题降序" />
            </asp:DropDownList>

            <!-- 岗位列表 -->
                <asp:GridView ID="gvJobs" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                    OnRowCommand="gvJobs_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="title" HeaderText="岗位标题" SortExpression="title" />
                        <asp:BoundField DataField="description" HeaderText="岗位描述" SortExpression="description" />
                        <asp:BoundField DataField="place" HeaderText="岗位地点" SortExpression="place" />
                        <asp:BoundField DataField="time" HeaderText="工作时间" SortExpression="time" />
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:Button ID="btnApply" runat="server" 
                                            Text='<%# GetApplicationStatusText(Eval("status")) %>' 
                                            CommandName="Apply" 
                                            CommandArgument='<%# Eval("id") %>' 
                                            CssClass="btn btn-primary"
                                            Enabled='<%# Eval("status") == DBNull.Value %>' />
                                <asp:Button ID="btnViewDetails" runat="server" Text="查看详情" 
                                            CommandName="ViewDetails" 
                                            CommandArgument='<%# Eval("id") %>' 
                                            CssClass="btn btn-info" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>




            <!-- 申请历史 -->
            <h3 class="mt-4">我的申请历史</h3>
           <asp:GridView ID="gvApplicationHistory" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mt-4"
    OnRowCommand="gvApplicationHistory_RowCommand">
    <Columns>
        <asp:BoundField DataField="title" HeaderText="岗位标题" SortExpression="title" />
        <asp:BoundField DataField="status" HeaderText="申请状态" SortExpression="status" />
        <asp:BoundField DataField="time" HeaderText="申请时间" SortExpression="time" />
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:Button ID="btnCancelApplication" runat="server" Text="取消申请" CommandName="CancelApplication" 
                            CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger" 
                            Enabled='<%# Eval("status").ToString() == "0" %>' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

        </div>
    </form>
</body>
</html>
