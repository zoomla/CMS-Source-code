<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RunSql.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.RunSql" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/SPwd.ascx" TagPrefix="uc1" TagName="SPwd" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link rel="stylesheet" href="/Plugins/CodeMirror/lib/codemirror.css">
<link rel="stylesheet" href="/Plugins/CodeMirror/theme/eclipse.css">
<title>SQL执行</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <uc1:SPwd runat="server" ID="SPwd" Visible="false" />
    <div runat="server" visible="false" id="maindiv">
        <ul class="nav nav-tabs">
            <li class="active"><a href="#Tabs0" data-toggle="tab">录入脚本</a></li>
            <li><a href="#Tabs1" data-toggle="tab">上传脚本</a></li>
        </ul>
        <div class="tab-content">
            <div class="tab-pane active" id="Tabs0">
                <div id="sql_div" runat="server" >
                    <div style="border-top:1px solid #ddd;border-bottom:1px solid #ddd;"><textarea runat="server" id="Sql_T" style="display: none;"></textarea></div>
                    <div class="margin_t5 text-center">
                        <asp:Button ID="RunSql_B" runat="server" Text="执行SQL语句" CssClass="btn btn-primary" OnClick="RunSql_B_Click" />
                        <asp:Button ID="SaveSql_B" runat="server" Text="保存为脚本" CssClass="btn btn-primary" OnClick="SaveSql_B_Click" />
                    </div>
                </div>
                <div id="result_div" runat="server" visible="false">
                    <ZL:ExGridView ID="Result_EGV" runat="server" AutoGenerateColumns="true" CssClass="table table-striped table-bordered table-hover"></ZL:ExGridView>
                    <div class="Conent_fix"><asp:Button ID="Return_B" runat="server" CssClass="btn btn-info" OnClick="Return_B_Click" Text="返 回" /></div>
                </div>
            </div>
            <div class="tab-pane" id="Tabs1">
                <asp:UpdatePanel ID="EGV_Panel" runat="server" ChildrenAsTriggers="True">
                    <ContentTemplate>
                       <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
                                OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
                                CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
                                <Columns>
                                   <asp:BoundField HeaderText="文件名" DataField="name" />
                                    <asp:BoundField HeaderText="文件大小" DataField="size" />
                                    <asp:BoundField HeaderText="创建时间" DataField="CreateTime" DataFormatString="{0:yyyy年MM月dd日 HH:mm:ss}" />
                                    <asp:BoundField HeaderText="路径" DataField="Path" />
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" CssClass="option_style" CommandName="execute" CommandArgument='<%#Eval("Path") %>' OnClientClick="return confirm('确定要执行该脚本吗');"><i class="fa fa-play" title="执行"></i>执行</asp:LinkButton>
                                            <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("Path") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </ZL:ExGridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="clearfix"></div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/Plugins/CodeMirror/lib/codemirror.js"></script>
<script src="/Plugins/CodeMirror/mode/sql.js"></script>
<script>
$(function () {
    var editor = CodeMirror.fromTextArea(document.getElementById("Sql_T"), {
        mode: "text/x-mssql",
        height: "300px",
        lineNumbers: true,
        textWrapping: false,
        styleActiveLine: true,
    });
})
</script>
</asp:Content>
