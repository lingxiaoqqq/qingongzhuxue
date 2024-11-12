<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StudentDashboard.aspx.cs" Inherits="WebApplication2.StudentDashboard" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>学生岗位申请</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css" rel="stylesheet">
    <style>
        /* 优化通知面板的样式 */
        .notification-panel {
            background-color: #f8f9fa;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
            margin-bottom: 20px;
        }

        .notification-panel h3 {
            font-size: 24px;
            margin-bottom: 15px;
        }

        .notification {
            background-color: #ffffff;
            padding: 15px;
            border: 1px solid #ddd;
            border-radius: 8px;
            margin-bottom: 10px;
            transition: background-color 0.3s ease;
        }

        .notification:hover {
            background-color: #f1f1f1;
        }

        .notification h4 {
            font-size: 18px;
            margin-bottom: 10px;
        }

        .notification p {
            font-size: 14px;
            margin-bottom: 5px;
        }

        .notification small {
            font-size: 12px;
            color: #777;
        }

        /* 控制通知面板的最大高度，超出部分显示滚动条 */
        .notification-panel-container {
            max-height: 300px;
            overflow-y: auto;
        }

        /* 固定按钮的大小 */
        .btn-fixed {
            width: 150px; /* 设置固定宽度 */
            height: 40px; /* 设置固定高度 */
            font-size: 14px; /* 可根据需要调整字体大小 */
            text-align: center; /* 按钮文本居中 */
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="text-center mb-4">岗位申请</h2>

            <!-- 最新通知面板 -->
            <div class="notification-panel-container">
                    <div class="notification-panel">
                        <h3>最新通知</h3>
                        <asp:Repeater ID="rptNotifications" runat="server">
                            <ItemTemplate>
                                <div class="notification">
                                    <h4><%# Eval("title") %></h4>
                                    <p><%# Eval("content") %></p>
                                    <small>发布人: <%# Eval("username") %></small> <!-- 显示教师的用户名 -->
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>

            <!-- 筛选条件 -->
            <asp:Button ID="btnRefresh" runat="server" Text="刷新" CssClass="btn btn-secondary mb-3 btn-fixed" OnClick="btnRefresh_Click" />

            <asp:DropDownList ID="ddlFilterStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFilterStatus_SelectedIndexChanged">
                <asp:ListItem Value="All" Text="所有岗位" />
                <asp:ListItem Value="0" Text="未审核中" />
                <asp:ListItem Value="1" Text="审核通过" />
                <asp:ListItem Value="2" Text="审核未通过" />
                <asp:ListItem Value="4" Text="已发放酬金" />
                <asp:ListItem Value="Unapplied" Text="未申请" />
                <asp:ListItem Value="Applied" Text="已申请" />
            </asp:DropDownList>

            <asp:DropDownList ID="ddlSortOrder" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlSortOrder_SelectedIndexChanged">
                <asp:ListItem Value="TimeAsc" Text="按发布时间升序" />
                <asp:ListItem Value="TimeDesc" Text="按发布时间降序" />
                <asp:ListItem Value="TitleAsc" Text="按标题升序" />
                <asp:ListItem Value="TitleDesc" Text="按标题降序" />
            </asp:DropDownList>

            <!-- 岗位列表 -->
            <asp:GridView ID="gvJobs" runat="server" AutoGenerateColumns="False" CssClass="table table-striped"
                OnRowCommand="gvJobs_RowCommand" EmptyDataText="没有符合条件的岗位。">
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
                                        CssClass="btn btn-primary btn-fixed"
                                        Enabled='<%# Eval("status") == DBNull.Value %>' />
                            <asp:Button ID="btnViewDetails" runat="server" Text="查看详情" 
                                        CommandName="ViewDetails" 
                                        CommandArgument='<%# Eval("id") %>' 
                                        CssClass="btn btn-info btn-fixed" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

            <!-- 申请历史 -->
            <h3 class="mt-4">我的申请历史</h3>
            <asp:GridView ID="gvApplicationHistory" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mt-4"
                OnRowCommand="gvApplicationHistory_RowCommand" EmptyDataText="没有审核记录。">
                <Columns>
                    <asp:BoundField DataField="title" HeaderText="岗位标题" SortExpression="title" />
                    <asp:BoundField DataField="status" HeaderText="申请状态" SortExpression="status" />
                    <asp:BoundField DataField="time" HeaderText="申请时间" SortExpression="time" />
                        <asp:TemplateField HeaderText="操作" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Button ID="btnCancelApplication" runat="server" Text="取消申请" CommandName="CancelApplication" 
                                            CommandArgument='<%# Eval("id") %>' CssClass="btn btn-danger btn-sm btn-fixed" 
                                            Enabled='<%# Eval("status").ToString() == "0" %>' 
                                            style="min-width: 120px; height: 35px; text-align: center;" />
                            </ItemTemplate>
                        </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
    </form>
</body>
</html>
