<%@ Page Language="C#" Title="读取短消息" AutoEventWireup="true" CodeFile="MessageRead.aspx.cs" Inherits="User.MessageRead" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>读取短消息</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="margin_t10">
    <table class="table table-bordered table-striped">
        <tr>
            <td class="text-right td_m">发件人</td>
            <td class="tdRight">
                <asp:Label ID="LblSender" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">收件人</td>
            <td class="tdRight">
                <asp:Label ID="LblIncept" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">标题</td>
            <td class="tdRight">
                <asp:Label ID="LblTitle" runat="server" Text="Label"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">发送时间</td>
            <td class="tdRight">
                <asp:Label ID="LblSendTime" runat="server" Text="Label"></asp:Label></td>
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