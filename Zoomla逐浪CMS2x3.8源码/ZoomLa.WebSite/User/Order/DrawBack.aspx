<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DrawBack.aspx.cs" Inherits="User_Order_DrawBack" MasterPageFile="~/User/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li>会员中心</li>
        <li>我的订单</li>
        <li class="active"><a href="<%=Request.RawUrl %>">取消订单</a></li>
    </ol>
    <div>
        <table class="table table-bordered table-striped">
            <tr><td class="td_md">订单号</td><td><asp:Label runat="server" ID="OrderNo_L"></asp:Label></td></tr>
            <tr><td class="td_md">订单金额</td><td><asp:Label runat="server" ID="Orderamounts_L"></asp:Label></td></tr>
            <tr><td class="td_md">下单日期</td><td><asp:Label runat="server" ID="Cdate_L"></asp:Label></td></tr>
            <tr><td>退款理由:<br />(10-500字)</td><td><asp:TextBox runat="server" ID="Back_T" TextMode="MultiLine" CssClass="form-control" style="height:120px;width:100%;max-width:100%;" /></td></tr>
            <tr><td></td><td><asp:Button runat="server" ID="Sure_Btn" OnClick="Sure_Btn_Click" Text="提交" CssClass="btn btn-primary" /></td></tr>
        </table>
    </div>
    <script>
        function CloseCur() {
            parent.window.location = parent.location;
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
</asp:Content>