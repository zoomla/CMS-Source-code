<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LmUserListTree.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.profile.LmUserListTree"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>联盟会员树状图</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover" id="usernode">
        <tr>
            <td class="spacingtitle" align="center">
                联盟会员树状图
            </td>
        </tr>
    </table>
    <asp:TreeView ID="UserTree" CssClass="border" runat="server" Style="margin-left: 10px;" ImageSet="Arrows" Width="98%" Visible="false">
        <HoverNodeStyle ForeColor="#5555DD" />
        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" HorizontalPadding="5px" NodeSpacing="0px" VerticalPadding="0px" />
        <ParentNodeStyle Font-Bold="False" />
        <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px" VerticalPadding="0px" />
    </asp:TreeView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <%--<style type="text/css">
        #UserTree
        {
            background-color: rgb(199, 224, 246);
            background: url(../../../App_Themes/AdminDefaultTheme/images/tdbg.jpg);
        }
        a:hover
        {
            text-decoration: none;
        }
        #UserTree td
        {
            line-height: 25px;
        }
    </style>--%>
</asp:Content>

   


