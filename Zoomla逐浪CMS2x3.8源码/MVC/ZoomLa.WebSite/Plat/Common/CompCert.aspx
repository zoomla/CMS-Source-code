<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompCert.aspx.cs" Inherits="ZoomLaCMS.Plat.Common.CompCert" MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>申请认证</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
   <div class="container platcontainer">
       <div runat="server" id="ok_div" visible="false">
            <table class="table table-bordered table-striped">
                <tr><td class="td_m">企业简称</td><td><ZL:TextBox runat="server" ID="CompShort_T" CssClass="form-control text_300" AllowEmpty="false" /></td></tr>
                <tr><td class="td_m">企业名称</td><td><ZL:TextBox runat="server" ID="CompName_T" CssClass="form-control text_300" AllowEmpty="false" /></td></tr>
                <tr><td>联系电话</td><td><ZL:TextBox runat="server" ID="Telephone_T" CssClass="form-control text_300" ValidType="PhoneNumber" /></td></tr>
                <tr><td>联系手机</td><td><ZL:TextBox runat="server" ID="Mobile_T" CssClass="form-control text_300" ValidType="MobileNumber" /></td></tr>
                <tr><td>企业邮箱</td><td><ZL:TextBox runat="server" ID="Mails_T" CssClass="form-control text_300" AllowEmpty="false" ValidType="Mail" /></td></tr>
                <tr><td></td><td><asp:Button runat="server" ID="Save_Btn" Text="提交申请" OnClick="Save_Btn_Click"  CssClass="btn btn-info"/></td></tr>
            </table> 
       </div>
       <div runat="server" id="err_div" visible="false" class="alert alert-warning"></div>
 </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>