<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageRead.aspx.cs" Inherits="ZoomLa.WebSite.User.MessageRead" ValidateRequest="false" MasterPageFile="~/User/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>阅读短消息</title>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <div class="mainDiv">
            <div class="us_topinfo">
                <ul class="Messge_nav">
                    <li><a href="MessageSend.aspx">写邮件</a></li>
                    <li class="active"><a href="Message.aspx">收件箱</a></li>
                    <li><a href="MessageOutbox.aspx">发件箱</a></li>
                    <li><a href="MessageDraftbox.aspx">草稿箱</a></li>
                    <li><a href="MessageGarbagebox.aspx">垃圾箱</a></li>
                    <li><a href="mobile.aspx">手机短信</a></li>
                </ul>
             
                <div style="clear: both;"></div>
            </div>
            <div style="margin-top: 10px;">
                <div class="us_seta" style="margin-top: 5px;">
                    <table class="table table-bordered" style="width: 100%; margin: auto; line-height: 25px;">
                        <tr>
                            <td class="tdLeft">发件人：</td>
                            <td class="tdRight">
                                <asp:Label ID="LblSender" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="tdLeft">收件人：</td>
                            <td class="tdRight">
                                <asp:Label ID="LblIncept" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="tdLeft">短消息主题：</td>
                            <td class="tdRight">
                                <asp:Label ID="LblTitle" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="tdLeft">发送时间：</td>
                            <td class="tdRight">
                                <asp:Label ID="LblSendTime" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                        <tr>
                            <td class="tdLeft">短消息内容：</td>
                            <td class="tdRight" style="height: 300px;">
                                <asp:Literal runat="server" ID="txt_Content"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft">附件：</td>
                            <td runat="server" id="publicAttachTD"></td>
                        </tr>
                        <tr>
                            <td class="tdLeft">操作：</td>
                            <td class="tdRight">
                                <asp:Button ID="BtnReply" runat="server" Text="回复" CssClass="btn btn-primary" OnClick="BtnReply_Click" />
                                <asp:Label ID="LBCopy" runat="server" Text="抄送文件无法操作" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
</asp:Content>
