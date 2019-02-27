<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MemoContext.aspx.cs" Inherits="MemoContext" %>

<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>备忘录详细</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li><a href="MemoList.aspx">备忘录列表</a></li>
        <li class="active">备忘录详细</li>
    </ol>
    <div style="margin-bottom: 10px;">
        <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
    </div>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td id="tdTitle" runat="server" align="center" colspan="5" class="catebar"></td>
        </tr>
        <tr>
            <td id="tdPostTime" runat="server" align="center" colspan="5" class="border1"></td>
        </tr>
        <tr>
            <td height="300" colspan="5" valign="top" class="border1" id="tdContent" runat="server"></td>
        </tr>
        <tr>
            <td width="300" class="border1" id="tdUp" runat="server"></td>
            <td width="300" class="border1" id="tdDown" runat="server"></td>
            <td class="border1"><asp:Label ID="labCount" runat="server" Text=""></asp:Label></td>
        </tr>
    </table>
</asp:Content>
