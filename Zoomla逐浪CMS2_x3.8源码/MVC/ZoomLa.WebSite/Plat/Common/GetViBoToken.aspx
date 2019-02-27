<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetViBoToken.aspx.cs" Inherits="ZoomLaCMS.Plat.Common.GetViBoToken" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>用户绑定</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
      <div>绑定进行中,请稍等片刻</div>
        <div style="display:none;">
        <asp:HiddenField runat="server" ID="OpenID_Hid" />
        <asp:HiddenField runat="server" ID="Token_Hid" />
        <asp:Button runat="server" ID="QQBind_Btn" OnClick="QQBind_Btn_Click" /></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript" src="http://qzonestyle.gtimg.cn/qzone/openapi/qc_loader.js" data-appid="<%:ZoomLa.Components.PlatConfig.QQKey %>" data-callback="true" charset="utf-8"></script>
    <script type="text/javascript">
        function QQBind() {
            QC.Login.getMe(function (openId, accessToken) {
                $("#OpenID_Hid").val(openId);
                $("#Token_Hid").val(accessToken);
                $("#QQBind_Btn").click();
            });
        }
        QQBind();
    </script>
</asp:Content>