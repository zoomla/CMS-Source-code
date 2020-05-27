<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WxConfig.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.WxConfig"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>微信配置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="alert alert-info" role="alert">注意事项: AppID与Secret要与微信公众号下的appID与appsecret一致,公众号名可以自定义设置</div>
    <table class="table table-bordered table-striped">
        <tr>
            <td class="td_m">别名</td>
            <td><asp:TextBox runat="server" ID="Alias_T" CssClass="form-control text_300"/>
                <asp:RequiredFieldValidator ID="RV3" runat="server"
                        ControlToValidate="Alias_T" ErrorMessage="别名不能为空" ForeColor="Red" Display="Dynamic"/>
            </td>
        </tr>
        <tr>
            <td>微信公众号</td>
            <td>
                <asp:TextBox runat="server" ID="WxNo_T" CssClass="form-control text_300"/>
                <asp:RequiredFieldValidator ID="RV4" runat="server" ControlToValidate="WxNo_T" ErrorMessage="公众号不能为空" ForeColor="Red" Display="Dynamic" />
            </td>
        </tr>
        <tr><td>原始ID</td>
           <td>
                <asp:TextBox runat="server" ID="OrginID_T" CssClass="form-control text_300" />
            <asp:RequiredFieldValidator ID="RV5" runat="server" ControlToValidate="OrginID_T" ErrorMessage="原始ID不能为空" ForeColor="Red" Display="Dynamic" />
           </td>
        </tr>
        <tr>
            <td>AppID</td>
            <td>
                <asp:TextBox runat="server" ID="AppID_T" CssClass="form-control text_300" />
                <asp:RequiredFieldValidator ID="RV2" runat="server" ControlToValidate="AppID_T" ErrorMessage="AppID不能为空" ForeColor="Red" Display="Dynamic" />
            </td>
        </tr>
        <tr><td>Secret</td><td>
            <asp:TextBox runat="server" ID="Secret_T" CssClass="form-control text_300" />
                <asp:RequiredFieldValidator ID="RV1" runat="server"
                        ControlToValidate="Secret_T" ErrorMessage="Secret不能为空" ForeColor="Red" Display="Dynamic"/>
            </td></tr>
        <tr><td>Token</td><td>
            <asp:Label runat="server" ID="Token_L" />
            <asp:Button runat="server" ID="ReToken_Btn" Text="重新获取Token" CssClass="btn btn-info margin_l5" OnClick="ReToken_Btn_Click" /></td></tr>
        <tr><td>操作</td><td>
            <asp:Button runat="server" ID="Save_Btn" Text="保存" OnClick="Save_Btn_Click"  CssClass="btn btn-primary"/>
            <a href="WxAppManage.aspx" class="btn btn-primary">返回</a></td></tr>
    </table>
    <script>
    </script>
</asp:Content>