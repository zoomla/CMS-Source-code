<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="idcren.aspx.cs" Inherits="ZoomLaCMS.Cart.idcren" MasterPageFile="~/Cart/order.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>充值续费</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="head_div hidden-xs">
        <a href="/">
            <img src="<%=Call.LogoUrl %>" /></a>
        <div class="input-group pull-right skey_div text_300">
            <input type="text" id="skey_t" placeholder="全站检索" class="form-control skey_t" data-enter="0" />
            <span class="input-group-btn">
                <input type="button" value="搜索" class="btn btn-default" onclick="skey();" data-enter="1" />
            </span>
        </div>
        <div class="clearfix"></div>
    </div>
    <table class="table table-bordered table-striped">
        <tr>
            <td class="td_m">续费订单</td>
            <td>
                <asp:Label runat="server" ID="OrderNo_L"></asp:Label></td>
        </tr>
        <tr>
            <td>绑定域名</td>
            <td>
                <asp:Label runat="server" ID="Domain_L"></asp:Label></td>
        </tr>
        <tr>
            <td>产品名称</td>
            <td>
                <asp:Label runat="server" ID="Proname_L"></asp:Label></td>
        </tr>
        <tr>
            <td>到期时限</td>
            <td>
                <asp:Label runat="server" ID="ETime_L"></asp:Label></td>
        </tr>
        <tr>
            <td>订购时限</td>
            <td>
                <asp:DropDownList runat="server" ID="IDCTime_DP" DataTextField="name" DataValueField="time" CssClass="form-control text_md"></asp:DropDownList></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button runat="server" ID="Submit_Btn" Text="提交订单" OnClick="Submit_Btn_Click" CssClass="btn btn-info" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
