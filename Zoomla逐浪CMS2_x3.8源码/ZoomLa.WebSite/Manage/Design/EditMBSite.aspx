<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditMBSite.aspx.cs" Inherits="Manage_Design_EditMBSite" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>微站管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped">
    <tr><td class="td_m">微站名称</td><td><ZL:TextBox runat="server" ID="SiteName_T" CssClass="form-control text_300" AllowEmpty="false" /></td></tr>
    <tr><td>预览图片</td><td><ZL:SFileUp runat="server" ID="SiteImg_UP" FType="Img" /></td></tr>
    <tr><td>微站模板</td><td><asp:Label ID="TlpName_L" runat="server" /> </td></tr>
    <tr><td>创建用户</td><td><asp:Label ID="CUser_L" runat="server" /></td></tr>
    <tr><td>创建时间</td><td><asp:Label ID="CDate_L" runat="server" /></td></tr>
    <tr><td>站点配置</td><td><asp:Label ID="SiteCfg_L" runat="server" /></td></tr>
    <tr>
        <td></td>
        <td>
            <asp:Button runat="server" ID="Save_Btn" Text="保存信息" OnClick="Save_Btn_Click" CssClass="btn btn-primary" />
            <a href="MBSiteList.aspx" class="btn btn-default">返回列表</a>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    function showuser(uid) {
        comdiag.ShowModal("../User/UserInfo.aspx?id=" + uid, "用户信息");
    }
</script>
</asp:Content>
