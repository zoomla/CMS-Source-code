<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WxPayConfig.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.WxPayConfig" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>支付配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td class="td_m">支付APPID:</td>
                <td>
                    <ZL:TextBox ID="APPID_T" runat="server" CssClass="form-control text_300" AllowEmpty="false" /></td>
            </tr>
            <tr>
                <td>支付Secret:</td>
                <td>
                    <ZL:TextBox ID="Secret_T" runat="server" CssClass="form-control text_300" AllowEmpty="false" /></td>
            </tr>
            <tr>
                <td>商户编号:</td>
                <td>
                    <ZL:TextBox ID="AccountID_T" runat="server" CssClass="form-control text_300" AllowEmpty="false" /></td>
            </tr>
            <tr>
                <td>商户密钥</td>
                <td>
                    <ZL:TextBox ID="Key_T" runat="server" CssClass="form-control text_300" AllowEmpty="false" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="Save_B" runat="server" OnClick="Save_B_Click" CssClass="btn btn-info" Text="保存" />
                    <a href="WxAppManage.aspx" class="btn btn-info">返回</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
