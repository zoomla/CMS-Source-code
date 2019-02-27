<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewContent.aspx.cs" Inherits="MIS_ZLOA_ViewContent" MasterPageFile="~/User/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>查看文章</title>
<style>
#popImg{ display:none;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="draftnav">
    <a href="/MIS/OA/Main.aspx">行政公文</a>/<a href="ContentManage.aspx?NodeID=<%=Request.QueryString["NodeID"] %>">内容管理</a>/<span>浏览内容</span>
</div>
<div id="site_main" style="margin:10px;">
    <table class="table_li table-border" cellspacing="0" cellpadding="0">
        <tr><td colspan="8" class="trhead">内容浏览</td></tr>
        <tr>
            <td >密级：</td><td><asp:Label runat="server" ID="SecretL"></asp:Label></td>
            <td>紧急程度：</td><td><asp:Label runat="server" ID="UrgencyL"></asp:Label></td>
            <td>重要程度：</td><td><asp:Label runat="server" ID="ImportanceL"></asp:Label></td>
        </tr>
        <tr>
            <td>标题:</td><td colspan="3"><asp:Label ID="Title" runat="server" /></td>
            <td>发文日期：</td><td><asp:Label ID="LbCreatTime" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr><td>发文人：</td><td colspan="3"><asp:Label ID="userNameL" runat="server" ></asp:Label></td>
            <td>发文部门：</td><td colspan="3"><asp:Label ID="userGroupL" runat="server"></asp:Label></td></tr>
         <tr><td>附件：</td><td runat="server" id="publicAttachTD" colspan="7"></td></tr>
        <tr><td colspan="8" style="min-height:400px;"><asp:Literal ID="ContentHtml" runat="server"></asp:Literal></td></tr>
    </table>
</div>
</asp:Content>
