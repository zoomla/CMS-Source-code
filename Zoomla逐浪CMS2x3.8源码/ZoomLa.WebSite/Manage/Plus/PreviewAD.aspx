<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreviewAD.aspx.cs" Inherits="manage_Plus_PreviewAD" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>广告版位管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered">
        <tr>
            <td colspan="2" align="center"><strong>预览版位JS效果</strong></td>
        </tr>
        <tr>
            <td style="height: 25px" align="center">
                <a href="javascript:this.location.reload();">刷新页面</a>&nbsp;&nbsp;&nbsp;&nbsp; <a href="ADZoneManage.aspx">返回上页</a>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                <div style="height: 400px" id="ShowJS" runat="server"></div>
            </td>
        </tr>
    </table>
</asp:Content>
