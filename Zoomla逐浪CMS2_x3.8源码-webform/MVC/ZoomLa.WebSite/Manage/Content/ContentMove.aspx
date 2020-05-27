<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContentMove.aspx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.ContentMove" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>内容管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered">
        <tr class="text-center"><td colspan="2">批量移动内容到其他节点</td></tr>
        <tr>
            <td class="td_m text-right">内容ID：</td>
            <td class="bqright">
                <asp:TextBox ID="TxtContentID" class="form-control text_300"  runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RV1" runat="server" ControlToValidate="TxtContentID" ErrorMessage="内容ID不能为空" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td class="text-right">目标节点：</td>
            <td>
                <select name="targetNode" class="form-control text_300">
                    <asp:Literal ID="Nodes_Li" runat="server"></asp:Literal>
                </select>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="BatMove_Btn" class="btn btn-primary" runat="server" Text="批量处理" OnClick="BatMove_Btn_Click" />
                <a href="ContentManage.aspx" class="btn btn-primary">取消</a>
            </td>
        </tr>
    </table>
</asp:Content>
