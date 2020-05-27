<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopMailConfig.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.ShopMailConfig" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>送货方式设置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="3" class="text-center">店铺邮件设置</td>
        </tr>
        <tr>
            <td class="text-right" style="width: 30%"><strong>邮件类型：</strong></td>
            <td colspan="2">
                <asp:RadioButtonList ID="RadioButtonList1" runat="server"  RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                    <asp:ListItem Value="1" Selected="True">团购成功邮件</asp:ListItem>
                    <asp:ListItem Value="2">团购失败邮件</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td class="text-right" style="width: 30%"><strong>邮件标题：</strong></td>
            <td style="width: 300px" valign="top">
                <asp:TextBox ID="txtTitle" class="form-control" runat="server" Width="300px"></asp:TextBox>
            </td>
            <td valign="top">请在标题中使用关键字{ProName}</td>
        </tr>
        <tr>
            <td class="text-right" style="width: 30%" valign="top"><strong>邮件内容：</strong></td>
            <td style="width: 300px" valign="top">
                <asp:TextBox ID="txtContext" class="form-control" runat="server" Width="300px" TextMode="MultiLine" Rows="10" Height="70px"></asp:TextBox>
            </td>
            <td valign="top">请在内容中使用{UserName}</td>
        </tr>
        <tr>
            <td class="text-right" style="width: 30%" valign="top"></td>
            <td valign="top" colspan="2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" ErrorMessage="请输入邮件标题"></asp:RequiredFieldValidator>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtContext" ErrorMessage="请输入邮件内容"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td colspan="3" class="text-center">
                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="提交" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
