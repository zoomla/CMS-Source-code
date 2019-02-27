<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderPay.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.Diag.OrderPay" MasterPageFile="~/Manage/I/Default.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head" >
    <title>订单支付</title>
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <style> 
        #Plat_RBT tr td { padding-left:5px; } 
        #BreadDiv{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-hover">
        <tr>
            <td class="td_m">订单编号:</td>
            <td><asp:Label ID="OrderID_L" runat="server" /></td>
        </tr>
        <tr>
            <td>支付方式:</td>
            <td>
                <asp:RadioButtonList ID="Plat_RBT" runat="server" DataValueField="PayPlatID" DataTextField="PayPlatName" Font-Size="11" RepeatDirection="Horizontal" >
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td>支付金额:</td>
            <td>
                <ZL:TextBox ID="Money_T" runat="server" CssClass="form-control text_x" AllowEmpty="false" Text="0" ValidType="FloatZeroPostive" />
            </td>
        </tr>
        <tr>
            <td>同步扣除</td>
            <td>
                <input id="SyncDeduct_Chk" runat="server" type="checkbox" checked="checked" class="switchChk" /><span style="color:green;">*仅在虚拟币支付下生效,同步扣除用户虚拟币</span>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="OrderPay_B" runat="server" CssClass="btn btn-info" OnClick="OrderPay_B_Click" OnClientClick="return confirm('确定修改为已支付吗');" Text="确认支付" />
                <button class="btn btn-info" onclick="parent.CloseDiag();" >取消</button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/dist/js/bootstrap-switch.js"></script>
</asp:Content>

