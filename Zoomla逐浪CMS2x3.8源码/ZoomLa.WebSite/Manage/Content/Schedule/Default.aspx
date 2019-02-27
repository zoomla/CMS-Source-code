<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Manage_Content_Addon_Schedule_Default" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/SPwd.ascx" TagPrefix="uc1" TagName="SPwd" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>任务列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href="Default.aspx">任务列表</a>[<a href="AddSche.aspx">创建任务</a>]</li>
    <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
    <div id="sel_box" runat="server" class="padding5">
        <div style="display: inline-block; width: 230px;">
            <div class="input-group" style="position: relative; margin-bottom: -12px;">
                <asp:TextBox ID="Skey_T" placeholder="作品标题" runat="server" CssClass="form-control text_md" />
                <span class="input-group-btn">
                    <asp:Button ID="Search_B" runat="server" Text="<%$Resources:L,搜索 %>" class="btn btn-primary" OnClick="Search_B_Click" />
                </span>
            </div>
        </div>
    </div>
</ol>
<uc1:SPwd runat="server" ID="SPwd" Visible="false" />
<div id="maindiv" runat="server" visible="false">
<ul class="nav nav-tabs">
    <li data-type="1"><a href="Default.aspx?TaskType=1">自动进程</a></li>
    <li data-type="3"><a href="Default.aspx?TaskType=3">自动审核</a></li>
    <li data-type="2"><a href="Default.aspx?TaskType=2">首页发布</a></li>
</ul>
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="ID" ItemStyle-CssClass="td_s"/>
            <asp:BoundField HeaderText="任务名称" DataField="TaskName" />
            <asp:TemplateField HeaderText="关联文章" Visible="false">
                <ItemTemplate><%#GetContentLink(Eval("TaskContent").ToString()) %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="执行计划">
                <ItemTemplate>
                    <%#GetExecuteType() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="创建时间" DataField="CDate" />
            <asp:TemplateField HeaderText="任务状态">
                <ItemTemplate>
                    <%#GetStatus() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="执行时间">
                <ItemTemplate>
                      <%#GetExecuteTime() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="最近一次执行">
                <ItemTemplate>
                    <%#GetLastTime() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="备注" DataField="Remind" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="option_style" href="AddSche.aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <a class="option_style" href="ScheLogList.aspx?TaskID=<%#Eval("ID") %>"><i class="fa fa-file-text"></i>日志</a>
                    <asp:LinkButton runat="server" CssClass="option_style" CommandName="execNow" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('将强行执行（忽略系统计划和预先规则），是否继续？');"><i class="fa fa-hourglass-start"></i>强制执行</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
<div>
    <asp:Button runat="server" ID="StartTask_Btn" Text="启动任务" CssClass="btn btn-info" OnClick="StartTask_Btn_Click" OnClientClick="return confirm('确定要将已停用的任务加入队列吗');" />
    <asp:Button runat="server" ID="StopTask_Btn" Text="停用任务" CssClass="btn btn-info" OnClick="StopTask_Btn_Click" OnClientClick="return confirm('确定要停用任务吗');" />
    <asp:Button runat="server" ID="InitTask_Btn" Text="初始化任务" class="btn btn-info" OnClick="InitTask_Btn_Click" OnClientClick="return confirm('初始化会清空队列并重新生成任务,确定要初始化吗');" />
    <a href='TaskList.aspx' class="btn btn-info"><i class="fa fa-eye"></i> 查看运行中的任务队列</a>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
$(function () {
    $(".nav li[data-type='" + <%:TaskType%> + "']").addClass("active");
})
$("#sel_btn").click(function (e) {
    if ($("#sel_box").css("display") == "none") {
        $(this).addClass("active");
        $("#sel_box").slideDown(300);
    }
    else {
        $(this).removeClass("active");
        $("#sel_box").slideUp(200);
    }
});
</script>
</asp:Content>