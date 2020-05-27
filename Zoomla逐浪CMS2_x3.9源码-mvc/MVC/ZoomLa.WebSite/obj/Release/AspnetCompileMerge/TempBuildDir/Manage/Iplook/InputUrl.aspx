<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputUrl.aspx.cs" Inherits="ZoomLaCMS.Manage.Iplook.InputUrl" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>添加URL信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
            <asp:Label ID="info" runat="server" Text="Label"></asp:Label>
        <table  class="table table-striped table-bordered table-hover">
            <tr align="center">
                <td colspan="2" class="spacingtitle">
                    <asp:Label ID="LblTitle" runat="server" Text="添加URL信息" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td class="style2" align="center">
                    <strong>
                        <asp:Label ID="Label6" runat="server" Text="URL："></asp:Label></strong></td>
                <td class="tdbg" align="left">
                    <asp:TextBox ID="url" class="l_input" runat="server" Width="30%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2" align="center">
                    <strong>
                        <asp:Label ID="Label7" runat="server" Text="分类ID："></asp:Label></strong>
                </td>
                <td width="66%" align="left">
                    <asp:TextBox ID="class_ID" class="l_input" runat="server" Width="25%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2" align="center">
                    <asp:Label ID="Label3" runat="server" Text="节点ID："></asp:Label>
                </td>
                <td width="20%" align="left">
                    <asp:TextBox ID="Node_ID" class="l_input" runat="server" Width="25%"></asp:TextBox>
                </td>
            </tr>
            <tr class="tdbgbottom">
                <td colspan="5" class="tdbgbottom">
                    <asp:Button ID="add" class="C_input" runat="server" Text="添加" OnClick="add_Click" />
                    &nbsp;&nbsp;
                </td>
            </tr>
        </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>