<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AuthApply.aspx.cs" Inherits="ZoomLaCMS.App.Other.AuthApply"  MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>授权申请</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <div class="panel panel-primary">
            <div class="panel-heading"><span class="fa fa-book"></span><span class="margin_l5">授权申请</span></div>
            <div class="panel-body">
            <div>
                <table class="table table-bordered">
                    <tr><td class="td_m">授权名称：</td><td><span>逐浪APP生成授权</span></td></tr>
                    <tr><td>申请时间：</td><td><span><%:DateTime.Now.ToString("yyyy-MM-dd") %></span></td></tr>
                    <tr><td>有效期限：</td><td>不限定</td></tr>
                </table>
            </div>
            <table class="table table-bordered">
                <tr><td class="td_m">网站地址：</td><td><asp:TextBox runat="server" ID="SiteUrl_T" CssClass="form-control text_300 isurl" /><span class="r_red">*</span></td></tr>
                <tr><td>联系人：</td><td><asp:TextBox runat="server" ID="Contact_T"  CssClass="form-control text_300 required" MaxLength="10"/><span class="r_red">*</span></td></tr>
                <tr><td>手机号码：</td><td><asp:TextBox runat="server" ID="MPhone_T"  CssClass="form-control text_300 phone"/><span class="r_red">*</span></td></tr>
                <tr><td>Email：</td><td>
                    <asp:TextBox runat="server" ID="Email_T"  CssClass="form-control text_300 isemail"/>
                    <span class="r_red">*</span>请填写可用的Email地址,授权码将发往该邮箱</td></tr>
                <tr><td>公司名称：</td><td><asp:TextBox runat="server" ID="CompName_T"  CssClass="form-control text_300" MaxLength="10"/></td></tr>
                <tr><td>QQ号码：</td><td><asp:TextBox runat="server" ID="QQCode_T"  CssClass="form-control text_300" MaxLength="20"/></td></tr>
                <tr><td></td><td><asp:Button runat="server" CssClass="btn btn-primary" ID="Sure_Btn" Text="确认申请" OnClick="Sure_Btn_Click"/></td></tr>
            </table>
            </div>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
        <script src="/JS/ZL_Regex.js"></script>
        <script type="text/javascript" src="/JS/jquery.validate.min.js"></script>
    <script>
        $(function () {
            $.validator.addMethod("phone", function (value) {
                return ZL_Regex.isMobilePhone(value);
            }, "请输入正确的手机号码");
            $.validator.addMethod("isurl", function (value) {
                value = StrHelper.UrlDeal(value);
                return ZL_Regex.isUrl(value);
            }, "链接格式不正确");
            $.validator.addMethod("isemail", function (value) {
                return ZL_Regex.isEmail(value);
            }, "Email地址格式不正确")
            $("form").validate({});
        })
    </script>
</asp:Content>