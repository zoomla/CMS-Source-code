<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSensitivity.aspx.cs" Inherits="Manage_I_Config_AddSensitivity" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加过滤敏感词汇</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="form1" runat="server">
        <table width="99%" cellspacing="1" cellpadding="0" class="table table-striped table-bordered table-hover">
            <td class="spacingtitle" colspan="2" align="center">
                <asp:Label ID="Label1" runat="server" Text="添加敏感词汇"></asp:Label></td>
            <tr class="tdbg">
                <td class="tdbgleft">关键字 </td>
                <td>
                    <asp:TextBox ID="keyword" runat="server" CssClass=" form-control" Width="185px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="Dynamic" runat="server" ErrorMessage="关键字不能为空！" ControlToValidate="keyword"></asp:RequiredFieldValidator></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbgleft">状态 </td>
                <td>
                    <asp:CheckBox ID="istrue" runat="server" Checked="true" Text="启用" /></td>
            </tr>
            <tr class="tdbg">
                <td class="tdbg" colspan="2" align="center">
                    <asp:Button ID="Button1" runat="server" Text=" 添 加 " CssClass="btn btn-primary" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text=" 返 回 " CssClass="btn btn-primary" OnClick="Button2_Click" CausesValidation="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>