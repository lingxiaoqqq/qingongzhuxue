<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TeacherDashboard.aspx.cs" Inherits="WebApplication2.TeacherDashboard" %>

<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>教师工作台</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-mQ93f40WpSXBKMgDgOjtK0Ed9YzMk3hxj+4j++y4TfFjTbpy2pXKHbp3hssLRfG7" crossorigin="anonymous">
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f8f9fa;
            padding-top: 30px;
        }

        /* 容器和卡片样式 */
        .container {
            background-color: #fff;
            padding: 30px;
            border-radius: 15px;
            box-shadow: 0 2px 15px rgba(0, 0, 0, 0.1);
        }

        .card {
            border: none;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
            margin-bottom: 20px;
            border-radius: 15px;
        }

        .card-header {
            background: linear-gradient(90deg, #4e73df, #2e59d9);
            color: white;
            font-size: 18px;
            font-weight: bold;
            border-radius: 15px 15px 0 0;
        }

        .card-footer {
            background-color: transparent;
            border-top: none;
            text-align: center;
        }

        /* 按钮样式 */
        .btn-custom,
        .btn-success-custom,
        .btn-danger-custom,
        .btn-disabled-custom {
            font-size: 16px;
            padding: 10px 20px;
            border-radius: 5px;
            width: auto;
            transition: background-color 0.3s, transform 0.3s;
        }

        .btn-success-custom {
            background-color: #28a745;
            color: white;
        }

        .btn-danger-custom {
            background-color: #dc3545;
            color: white;
        }

        .btn-disabled-custom {
            background-color: #ccc;
            color: #666;
            cursor: not-allowed;
            opacity: 0.6;
        }

        .btn-custom:hover,
        .btn-success-custom:hover,
        .btn-danger-custom:hover {
            background-color: #0056b3;
            transform: scale(1.05);
        }

        .btn-disabled-custom:hover {
            background-color: #ccc;
            cursor: not-allowed;
        }

        .btn-custom:active,
        .btn-success-custom:active,
        .btn-danger-custom:active {
            transform: scale(0.95);
        }

        /* 表格样式 */
        .table th {
            background-color: #f8f9fa;
            color: #333;
            font-weight: bold;
        }

        .table-striped tbody tr:nth-child(odd) {
            background-color: #f1f1f1;
        }

        .table-hover tbody tr:hover {
            background-color: #f1f1f1;
            cursor: pointer;
        }

        /* 输入框和按钮的样式 */
        .form-control {
            border-radius: 5px;
            padding: 10px;
            font-size: 14px;
        }

        /* 响应式样式 */
        @media (max-width: 768px) {
            .container {
                padding: 20px;
            }

            .card-header {
                font-size: 16px;
            }

            .btn-custom,
            .btn-danger-custom,
            .btn-success-custom {
                font-size: 14px;
                padding: 8px 16px;
            }
        }
        /* 让按钮横向排列并保持间距 */
        .button-row {
            display: flex;
            justify-content: space-between;
            margin-bottom: 20px;
        }
        /* 分页样式 */
        .pagination-container {
            text-align: center;
            margin-top: 20px;
        }

        .pagination .page-item.active .page-link {
            background-color: #4e73df;
            color: white;
        }

        .pagination .page-link {
            color: #007bff;
            padding: 10px 20px;
            border-radius: 5px;
            transition: background-color 0.3s;
        }

        .pagination .page-link:hover {
            background-color: #f1f1f1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h2 class="text-center mb-4">教师工作台</h2>
            
            <!-- 操作按钮横向排列 -->
            <div class="button-row">
                <div class="col-md-3">
                    <asp:Button ID="btnRefresh" runat="server" Text="刷新" CssClass="btn btn-secondary mb-3" OnClick="btnRefresh_Click" />
                </div>
                <div class="col-md-3 text-center">
                    <asp:Button ID="btnViewStatistics" runat="server" Text="查看数据统计" OnClick="btnViewStatistics_Click" CssClass="btn btn-info" />
                </div>
                <div class="col-md-3 text-center">
                    <asp:Button ID="btnPublishNotification" runat="server" Text="发布通知" CssClass="btn btn-warning" OnClick="btnPublishNotification_Click" />
                </div>
                <div class="col-md-3 text-end">
                    <asp:Button ID="btnAddjob" runat="server" Text="添加工作" CssClass="btn btn-secondary mb-3" OnClick="btn_AddJob" />
                </div>
            </div>


            <!-- 岗位管理 -->
            <div class="card">
                <div class="card-header">
                    岗位管理
                </div>
                <div class="card-body">
                    <div class="row mb-3">
                        <div class="col-md-8">
                            <asp:TextBox ID="txtJobSearch" runat="server" CssClass="form-control" placeholder="请输入岗位标题或地点" />
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnJobSearch" runat="server" Text="查找岗位" CssClass="btn btn-primary" OnClick="btnJobSearch_Click" />
                        </div>
                    </div>
                    
                    <asp:GridView ID="gvJobs" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover"
                        OnRowCommand="gvJobs_RowCommand" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvJobs_PageIndexChanging" EmptyDataText="没有符合条件的岗位。">
                        <Columns>
                            <asp:BoundField DataField="Title" HeaderText="岗位标题" SortExpression="Title" />
                            <asp:BoundField DataField="Description" HeaderText="岗位描述" SortExpression="Description" />
                            <asp:BoundField DataField="Place" HeaderText="岗位地点" SortExpression="Place" />
                            <asp:BoundField DataField="Time" HeaderText="工作时间" SortExpression="Time" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnView" runat="server" Text="查看" CssClass="btn-custom" CommandName="ViewJob" CommandArgument='<%# Eval("Title") %>' />
                                    <asp:Button ID="btnDelete" runat="server" Text="删除" CssClass="btn-danger-custom" CommandName="DeleteJob" CommandArgument='<%# Eval("Id") %>' OnClientClick="return confirm('确认删除吗?');" />
                                    <asp:Button ID="btnUpdate" runat="server" Text="更新" CssClass="btn-success-custom" CommandName="UpdateJob" CommandArgument='<%# Eval("Id") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- 申请审核 -->
            <div class="card">
                <div class="card-header">
                    申请审核
                </div>
                <div class="card-body">
                    <div class="row mb-3">
                        <div class="col-md-8">
                            <asp:TextBox ID="txtApplicationSearch" runat="server" CssClass="form-control" placeholder="请输入学生用户名或岗位标题" />
                        </div>
                        <div class="col-md-4">
                            <asp:Button ID="btnApplicationSearch" runat="server" Text="查找申请" CssClass="btn btn-primary" OnClick="btnApplicationSearch_Click" />
                        </div>
                    </div>

                    <asp:GridView ID="gvApplications" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-hover"
                        OnRowCommand="gvApplications_RowCommand" DataKeyNames="ApplicationId" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvApplications_PageIndexChanging" EmptyDataText="没有审核记录。">
                        <Columns>
                            <asp:BoundField DataField="StudentUsername" HeaderText="学生用户名" SortExpression="StudentUsername" />
                            <asp:BoundField DataField="JobTitle" HeaderText="岗位标题" SortExpression="JobTitle" />
                            <asp:BoundField DataField="ApplicationStatus" HeaderText="申请状态" SortExpression="ApplicationStatus" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnApprove" runat="server" Text="审核通过" CommandName="Approve" CommandArgument='<%# Eval("ApplicationId") %>' 
                                        CssClass='<%# Eval("ApplicationStatus").ToString() == "0" ? "btn-success-custom" : "btn-disabled-custom" %>'
                                        Enabled='<%# Eval("ApplicationStatus").ToString() == "0" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnReject" runat="server" Text="审核拒绝" CommandName="Reject" CommandArgument='<%# Eval("ApplicationId") %>' 
                                        CssClass='<%# Eval("ApplicationStatus").ToString() == "0" ? "btn-danger-custom" : "btn-disabled-custom" %>' 
                                        Enabled='<%# Eval("ApplicationStatus").ToString() == "0" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnPay" runat="server" Text="发放酬金" CommandName="Pay" CommandArgument='<%# Eval("ApplicationId") %>' 
                                        CssClass='<%# Eval("ApplicationStatus").ToString() == "1" ? "btn-success-custom" : "btn-disabled-custom" %>' 
                                        Enabled='<%# Eval("ApplicationStatus").ToString() == "1" %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <asp:Label ID="lblMessage" runat="server" CssClass="message"></asp:Label>
        </div>
    </form>

    <!-- 引入 Bootstrap JS 和 Popper.js -->
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.3/dist/umd/popper.min.js" integrity="sha384-NgAtFJtBjq1lvjMdBhfiv9X1kj7fHLP8BqnjQdm6xrtLnGJWoZEKVb7gVBrV5RfL" crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/js/bootstrap.min.js" integrity="sha384-66RplfHpu1eYER5fvZ1A3Fs3A2tNK2kC1E2uPz1gAe5ixVuZQ9f0l94VggL+QyN6" crossorigin="anonymous"></script>
</body>
</html>
