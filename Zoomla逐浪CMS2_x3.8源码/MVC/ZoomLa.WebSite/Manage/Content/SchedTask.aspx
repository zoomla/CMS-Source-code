<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchedTask.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.SchedTask" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>内容管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <ul class="nav nav-tabs">
            <li class="active"><a href="#tab1" data-toggle="tab" onclick="ShowTabs(1)">全部任务</a></li>
            <li><a href="#tab0" data-toggle="tab" onclick="ShowTabs(0)">未执行</a></li>
            <li><a href="#tab10" data-toggle="tab" onclick="ShowTabs(10)">已执行</a></li>
        </ul>
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" MouseOverCssClass="tdbgmouseover" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound" OnRowCancelingEdit="EGV_RowCancelingEdit" EnableTheming="False" GridLines="None" Width="100%" CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前无计划任务!" OnPageIndexChanging="EGV_PageIndexChanging" BackColor="White" CellPadding="2" CellSpacing="1">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="ID" ReadOnly="true" />
                <asp:TemplateField HeaderText="任务类型">
                    <ItemTemplate>
                        <%# GetTaskType(Eval("TaskType").ToString()) %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="tooltip-demo">
                            <asp:TextBox CssClass="form-control task_input input-sm" data-toggle="tooltip" data-placement="top" Width="100" title="1为定时发布" runat="server" ID="eTaskType" Text='<%#Eval("TaskType") %>'></asp:TextBox>
                        </div>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="任务内容">
                    <ItemTemplate>
                        <%#Eval("TaskContent") %>
                    </ItemTemplate>
                  <%--  <EditItemTemplate>
                        <div class="tooltip-demo">
                            <asp:TextBox CssClass="form-control task_input input-sm" data-toggle="tooltip" data-placement="top" Width="100" title="输入内容ID" runat="server" ID="eTaskContent" Text='<%#Eval("TaskContent") %>'></asp:TextBox>
                        </div>
                    </EditItemTemplate>--%>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="计划执行时间">
                    <ItemTemplate>
                        <%#GetExTime() %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="tooltip-demo">
                            <asp:TextBox CssClass="form-control task_input input-sm" data-toggle="tooltip" data-placement="top" Width="150" title="执行时间" runat="server" ID="eExecuteTime" Text='<%#Eval("ExecuteTime") %>' onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' })"></asp:TextBox>
                        </div>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status").ToString()) %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <div class="tooltip-demo">
                            <asp:TextBox CssClass="form-control task_input input-sm" data-toggle="tooltip" data-placement="top" Width="100" title="0为未执行，10为已执行" runat="server" ID="eStatus" Text='<%#Eval("Status") %>'></asp:TextBox>
                        </div>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit1" CommandArgument='<%# Container.DisplayIndex %>'>修改</asp:LinkButton>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗');">删除</asp:LinkButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="Save" runat="server" CommandName="Save" CommandArgument='<%# Container.DisplayIndex+":"+Eval("ID") %>'>更新</asp:LinkButton>
                        <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel" CommandArgument='<%# Container.DisplayIndex %>'>取消</asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        function ShowTabs(type) {
            if (type == 1)
            { location.href = 'SchedTask.aspx'; }
            else {
                location.href = 'SchedTask.aspx?type=' + type;
            }
        }
        $().ready(function () {
            if (getParam("type"))
            {
                $("li a[href='#tab" + getParam("type") + "']").parent().addClass("active").siblings("li").removeClass("active");;
            }
        })
    </script>
    <script>
        !function ($) {
            $(function () {
                // tooltip demo
                $('.tooltip-demo').tooltip({
                    selector: "[data-toggle=tooltip]",
                    container: "body"
                })
            })
        }(jQuery)
    </script>
</asp:Content>
