<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CheckPwd.aspx.cs" Inherits="Design_h5_CheckPwd" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">
.pcview_bg{background-position:center;background-repeat:no-repeat; background-size:cover;left: 0px; top: 0px; right: 0px; bottom: 0px; position: fixed;background-image:url(/UploadFiles/bg_pcview.jpg)}
</style>
<title>密码校验</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="pcview_bg">
    <div class="panel panel-info" style="width:320px;margin:0 auto;margin-top:100px;">
        <div class="panel-heading">该场景需要验证访问密码</div>
        <div class="panel-body">
            <asp:TextBox runat="server" ID="AccessPwd_T" TextMode="Password" class="form-control" placeholder="请输入访问密码" />
            <asp:RequiredFieldValidator runat="server" ID="RV1" ForeColor="Red" ControlToValidate="AccessPwd_T" ErrorMessage="访问密码不能为空" Display="Dynamic" />
            <span class="r_red" runat="server" id="remind_sp"></span>
        </div>
        <div class="panel-footer text-center">
            <asp:Button runat="server" ID="SurePwd_Btn" class="btn btn-primary" Text="确认密码" OnClick="SurePwd_Btn_Click" />
            <a href="/" class="btn btn-default margin_l20">返回首页</a>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>