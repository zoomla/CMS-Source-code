<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WithDraw.aspx.cs" Inherits="User_UserFunc_WithDraw" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>提现申请</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="UserInfo"></div>
<div class="container margin_t10">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/UserFunc/Money/WithDrawLog.aspx">提现日志</a></li>
        <li class="active">提现申请</li>
    </ol>
</div>
<div class="container">
    <table class="table table-bordered table-hover table-striped">
            <tr><td class="td_m">现有金额：</td><td><asp:Label runat="server" ID="NowMoney_L" /></td></tr>
      <%--      <tr><td>申请日期：</td><td><asp:Label runat="server" ID="CDate_L" /></td></tr>--%>
            <tr><td>提现金额：</td><td>
                <asp:TextBox runat="server" ID="Money_T" CssClass="form-control text_300" /><span class="r_red">*</span>
                <asp:RequiredFieldValidator runat="server" ID="MR1" ControlToValidate="Money_T" ErrorMessage="提现金额不能为空" ForeColor="Red" Display="Dynamic" />
                <asp:RegularExpressionValidator runat="server" ID="MR2" ControlToValidate="Money_T" ErrorMessage="金额必须大于0的数字(最多保留两位小数)" ForeColor="Red" ValidationExpression="^(?!0+(?:\.0+)?$)(?:[1-9]\d*|0)(?:\.\d{1,2})?$" Display="Dynamic"/>
            </td></tr>
            <tr><td>开户人：</td><td>
                <asp:TextBox runat="server" CssClass="form-control  text_300" ID="PName_T" /><span class="r_red">*</span>
                <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="PName_T" ErrorMessage="开户人姓名不能为空" ForeColor="Red" />
                                </td></tr>
            <tr><td>银行名称：</td><td><asp:TextBox runat="server" CssClass="form-control text_300" ID="Bank_T" /><span class="r_red">*</span>
                <asp:RequiredFieldValidator runat="server" ID="R2" ControlToValidate="Bank_T" ErrorMessage="银行名称不能为空" ForeColor="Red" />
                            </td></tr>
            <tr><td style="line-height:120px;">开户银行：</td><td>
                <asp:TextBox runat="server" ID="Remark_T" TextMode="MultiLine" CssClass="form-control text_300" Style="height: 120px;resize:none;" /><span class="r_red">*</span>
                <asp:RequiredFieldValidator runat="server" ID="RV5" ControlToValidate="Remark_T" ErrorMessage="开户银行不能为空" ForeColor="Red" />
                              </td></tr>
            <tr><td>银行卡号：</td><td>
                <asp:TextBox runat="server" CssClass="form-control text_300" ID="Account_T" /><span class="r_red">*</span>
                <asp:RequiredFieldValidator runat="server" ID="R3" ControlToValidate="Account_T" ErrorMessage="银行卡号不能为空" ForeColor="Red" Display="Dynamic"/>
                <asp:RegularExpressionValidator runat="server" ID="R4" ControlToValidate="Account_T" ErrorMessage="请输入16或19位银行卡号" ForeColor="Red" ValidationExpression="^(\d{16}|\d{19})$" Display="Dynamic"/>
                                </td></tr>
            <tr><td></td><td><asp:Button runat="server" CssClass="btn btn-primary" ID="Sure_Btn" Text="申请提现" OnClick="Sure_Btn_Click" OnClientClick="disBtn(this,2000);" /></td></tr>
        </table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/ZL_Regex.js"></script>
</asp:Content>
