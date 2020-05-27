<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FriendSearchManage.aspx.cs" Inherits="manage_Zone_FriendSearchManage" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css"  rel="stylesheet"/>
    <title>搜索好友管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" align="center" class="spacingtitle">搜索好友管理</td>
            </tr>
            <tr>
                <td class="tdleft" style="width:280px">是否开启婚姻搜索条件：</td>
                <td>
                    <input type="checkbox" runat="server" id="SearchFriendMarryKey" class="switchChk"/>
                </td>
            </tr>
            <tr>
                <td align="center">&nbsp;</td>
                <td>&nbsp;<asp:Button ID="btnSubmit" class="btn btn-primary" runat="server" Text="保存" OnClick="btnSubmit_Click" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
        <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
</asp:Content>

