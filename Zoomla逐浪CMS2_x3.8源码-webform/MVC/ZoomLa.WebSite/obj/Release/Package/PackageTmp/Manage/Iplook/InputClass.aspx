<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputClass.aspx.cs" Inherits="ZoomLaCMS.Manage.Iplook.InputClass"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type">
    <title>添加分类</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <thead class="spacingtitle">
            <tr>
                <td colspan="2" align="center">添加分类
                </td>
            </tr>
        </thead>
        <tbody class="tdbg">
            <tr>
                <td style="width: 20%">所属分类:
                </td>
                <td align="left">
                    <asp:DropDownList ID="leadto_ID" runat="server" Width="155px" DataTextField="class_name" DataValueField="class_ID"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 20%">分类名称:
                </td>
                <td align="left">
                    <asp:TextBox ID="class_name" class="l_input" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="submit" class="btn btn-primary" runat="server" OnClick="submit_Click" Text="提交" />&nbsp;
                <asp:Button ID="submit0" class="btn btn-primary" runat="server" Text="返回" OnClick="submit0_Click" /></td>
            </tr>
        </tbody>
    </table>
</asp:Content>