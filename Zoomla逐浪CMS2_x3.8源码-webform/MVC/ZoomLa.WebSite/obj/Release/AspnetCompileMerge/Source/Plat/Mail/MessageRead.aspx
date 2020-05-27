<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageRead.aspx.cs" Inherits="ZoomLaCMS.Plat.Mail.MessageRead" MasterPageFile="~/Plat/Main.master"  ValidateRequest="false"%>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<title>阅读短消息</title>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<style>
.Messge_nav { margin-bottom: 10px; margin-top: 10px; }
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="mainDiv container platcontainer">
<div class="btn-group Messge_nav"> <a class="btn btn-info" href="MessageSend.aspx">写邮件</a> <a class="btn btn-info" href="Default.aspx">收件箱</a> <a class="btn btn-info" href="MessageOutbox.aspx">发件箱</a> <a class="btn btn-info" href="MessageDraftbox.aspx">草稿箱</a> <a class="btn btn-info" href="MessageGarbagebox.aspx">垃圾箱</a> <a class="btn btn-info" href="Mobile.aspx">手机短信</a> </div>
<div class="margin_t10">
    <table class="table table-bordered table-striped">
        <tr>
            <td class="text-right td_m">发件人</td>
            <td class="tdRight"><asp:Label ID="LblSender" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">收件人</td>
            <td class="tdRight"><asp:Label ID="LblIncept" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">标题</td>
            <td class="tdRight"><asp:Label ID="LblTitle" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">发送时间</td>
            <td class="tdRight">
                <asp:Label ID="LblSendTime" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">内容</td>
            <td class="tdRight">
                <asp:Literal runat="server" ID="txt_Content"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="text-right">附件</td>
            <td>
                <div id="uploader" class="uploader">
                    <ul class="filelist"></ul>
                </div>
                <asp:HiddenField runat="server" ID="Attach_Hid" />
            </td>
        </tr>
        <tr>
            <td class="text-right">操作</td>
            <td class="tdRight">
                <a href="MessageSend.aspx?id=<%:Mid %>" class="btn btn-primary">回复</a>
                <asp:Label ID="CC_Btn" runat="server" Text="抄送文件无法操作" Visible="false"></asp:Label>
            </td>
        </tr>
    </table>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
<script src="/JS/Controls/ZL_Webup.js"></script>
<script>
    $(function () {
        ZL_Webup.AddReadOnlyLi($("#Attach_Hid").val());
    })
</script>
</asp:Content>