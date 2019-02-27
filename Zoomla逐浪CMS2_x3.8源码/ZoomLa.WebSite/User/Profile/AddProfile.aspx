<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="AddProfile.aspx.cs" Inherits="User_Profile_AddProfile" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>添加返利</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="OrderManage.aspx?state=0">我的订单</a></li>
        <li class="active">用户提交订单</li>
    </ol>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" align="center">
                <asp:Label ID="Label1" runat="server" Text="用户提交订单"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" width="45%">网站名字：</td>
            <td>
                <asp:TextBox ID="txtwname" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" width="45%">订单编号：</td>
            <td>
                <asp:TextBox ID="txtOrderID" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="txtOrderID" ErrorMessage="订单编号不能为空!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" width="45%">日期：</td>
            <td>
                <asp:TextBox ID="txtOrderData" onclick="WdatePicker()" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ControlToValidate="txtOrderData" ErrorMessage="订单日期不能为空!"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="right" width="45%" class="style3">订单金额：</td>
            <td class="style3">
                <asp:TextBox ID="txtOrderMoney" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator12"
                    runat="server" ControlToValidate="txtOrderMoney" ErrorMessage="订单金额必须是数字!" ValidationExpression="^(-?\d+)(\.\d+)?$" SetFocusOnError="True"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="Button1" runat="server" Text="添加" CssClass="btn btn-primary" OnClientClick="javascript:if(check()) return true;else return false;" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" Text="取消" CssClass="btn btn-primary" Width="63px" OnClick="Button2_Click" CausesValidation="False" />
            </td>
        </tr>
    </table>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
