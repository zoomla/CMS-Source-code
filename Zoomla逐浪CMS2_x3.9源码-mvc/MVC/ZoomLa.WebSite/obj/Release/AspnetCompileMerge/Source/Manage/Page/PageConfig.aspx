<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageConfig.aspx.cs" Inherits="ZoomLaCMS.Manage.Page.PageConfig" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>黄页配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-striped">
        <tr><td class="td_l">默认审核:</td><td><input type="checkbox" runat="server" id="IsAudit_Chk" class="switchChk" /></td></tr>
        <tr><td>用户可否自建栏目:</td><td><input type="checkbox" runat="server" id="UserCanNode_Chk" class="switchChk" /></td></tr>
        <tr><td></td><td>
            <asp:Button runat="server" ID="Save_Btn" Text="保存" OnClick="Save_Btn_Click" class="btn btn-primary" />
            <a href="PageStyle.aspx" class="btn btn-primary">返回</a></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
<script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
</asp:Content>