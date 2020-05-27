<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IDCMessage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.IDC.IDCMessage" MasterPageFile="~/Manage/I/Default.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>订单信息</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<asp:TextBox runat="server" ID="Info_T" TextMode="MultiLine" style="resize:none;width:100%;height:500px;"></asp:TextBox>
<div class="text-center margin_t5">
    <asp:Button runat="server" ID="Save_Btn" Text="保存信息" OnClick="Save_Btn_Click" class="btn btn-info" />
    <input type="button" value="关闭窗口" onclick="parent.CloseDiag();" class="btn btn-default" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>