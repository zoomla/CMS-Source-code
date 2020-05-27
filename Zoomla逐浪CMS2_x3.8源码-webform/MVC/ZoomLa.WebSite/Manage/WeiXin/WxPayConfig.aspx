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
                    <ZL:TextBox ID="APPID_T" runat="server" CssClass="form-control text_300" Enabled="false"/><span class="rd_green">同于公众号APPID</span></td>
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
            <tr><td>商户证书</td>
                <td><asp:TextBox runat="server" ID="SSLPath_T" class="form-control text_300"/><span class="rd_green"> 证书颁发名称(红包,退款等出账操作需使用证书)</span><span> [运行mmc--证书,查看颁发名称]</span></td>
            </tr>
            <tr><td>证书密码</td><td><asp:TextBox runat="server" ID="SSLPassword_T" class="form-control text_300" /><span class="rd_green"> 默认为商户号</span></td></tr>
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
