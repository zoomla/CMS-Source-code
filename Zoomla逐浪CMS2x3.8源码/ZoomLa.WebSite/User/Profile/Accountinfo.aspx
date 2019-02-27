<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Accountinfo.aspx.cs" Inherits="User_Profile_Accountinfo" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>收款信息设置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="store"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">收款信息设置</li> 
    </ol>
</div>
<div class="container btn_green">
    <table  class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" ><strong>收款信息(如果您要兑现返利，以下内容为必填项):</strong></td>
        </tr>
        <tr>
            <td width="25%" >支付平台:</td>
            <td>
                <asp:DropDownList ID="ddpay" CssClass="form-control text_md" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="25%">开户行及支行全称：</td>
            <td>
                <asp:TextBox ID="txtBank" CssClass="form-control text_md" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBank" ErrorMessage="请填写开户行!"></asp:RequiredFieldValidator>
                <br />
                如不知道，可以咨询您的银行热线查询卡片的开户信息，请尽量填写准确、完整
                <br />
                （例如：中国工商银行上海分行陆家嘴支行东昌分理处），无须开通网银；
                <br />
                <font color="red">（强烈推荐使用中国工商银行的账户收款，付款后可实时到帐！）</font></td>
        </tr>
        <tr>
            <td >开户人真实姓名:</td>
            <td >
                <asp:TextBox ID="txtOfName" CssClass="form-control text_md" runat="server"></asp:TextBox>此开户人必须是您本人；
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtOfName" ErrorMessage="请填写开户人真实姓名!"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td >开户帐号:</td>
            <td >
                <asp:TextBox ID="txtAccount" runat="server" CssClass="form-control text_md"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="请填写开户帐号!" ControlToValidate="txtAccount"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td >您的真实姓名:</td>
            <td >
                <asp:TextBox ID="txtName" runat="server" CssClass="form-control text_md"></asp:TextBox>
                <asp:Button ID="btnLock" runat="server" Text="绑定您的真实姓名" CssClass="btn btn-primary" OnClick="btnLock_Click" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ErrorMessage="请填写您的真实姓名!"></asp:RequiredFieldValidator>
                <div id="divlock" runat="server">为安全起见，本选项已经被您绑定，不允许修改。有问题请联系返利网客服。 </div>
            </td>
        </tr>
        <tr>
            <td >您的身份证号码:</td>
            <td >
                <asp:TextBox ID="txtCardID" runat="server" CssClass="form-control text_md"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCardID" ErrorMessage="请填写您的身份证号码!"></asp:RequiredFieldValidator>
                <br />
                应税务部门要求，2007年11月30日开始，收款时须填写身份证号码； </td>
        </tr>
        <tr>
            <td colspan="2" align="center" ><font color="red"> 用<a href="https://www.alipay.com/" target="_blank">支付宝</a>收款者请把“开户行及支行全称”一栏填写为“支付宝”，“开户人真实姓名”一栏填写为支付宝的开户人真实中文名称， <br />
        “开户帐号”一栏填写为支付宝的邮件帐号。</font></td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <asp:Button ID="Button1" runat="server" Text="保存" class="btn btn-primary" OnClick="Button1_Click" />
                <asp:Label ID="lblTip" runat="server"></asp:Label>
                <asp:HiddenField ID="hfLockId" runat="server" />
            </td>
        </tr>
    </table>
</div>
</asp:Content>
