<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Plat.EMail.Default"  MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">
.leftnav {display:none;}
.colwrap {margin-right:30px;height:850px;}
.colwrap a {text-decoration:none;color:#A6A6A6;}
.col_head {line-height:60px;height:60px;border-bottom:1px dotted #ddd;color:#A6A6A6;position:relative;}
.col_head .title {font-size:30px;}
.col_head .opwrap {position:absolute;right:0px;bottom:-10px;}
.col_head .opwrap a {margin-right:20px;width:50px;height:50px;}
.col_head .fa-ellipsis-h {font-size:40px;position:relative;bottom:-10px;}
.col_body {margin-top:15px;}
.col_body .item {margin-bottom:5px;}
.col_body .item_tit {display:block;width:100%; overflow:hidden;white-space:nowrap;text-overflow:ellipsis;color:#999;height:40px;line-height:40px;}
.col_body .item_op {float:right;color:#AFAFAF;height:30px;line-height:30px;}
.col_body .item_op i {font-size:20px;margin-right:10px;}
.col_body .item_op i:hover {color:#03a9f4;}
.col_body .item_form, .col_body .item_date {font-size:12px;}
.col_body .empty_body {color:#999;}
.col_body .empty_body i {font-size:120px;}
.col_body .empty_body .empty_txt {font-size:18px;margin-top:5px;text-align:center;}
</style>
<title>收件箱</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="col-md-1"></div>
    <div class="colwrap platcontainer col-md-3">
        <div class="col_head">
            <span class="title"><i class="fa fa-inbox"></i> 收件箱</span>
            <div class="opwrap">
             <%--   <a href="MailWrite.aspx"><i class="fa fa-pencil-square" style="font-size:25px;"></i> 新邮件</a>--%>
                <a href="MailList.aspx" title="更多邮件"><i class="fa fa-ellipsis-h"></i></a>
            </div>
        </div>
        <div class="col_body">
            <asp:Repeater runat="server" ID="Rece_RPT" EnableViewState="false">
                <ItemTemplate>
                    <div class="item" id="email_<%#Eval("id") %>">
                        <div class="item_tit"><a href="MailDetail.aspx?ID=<%#Eval("ID") %>" title="浏览"><i class="fa fa-envelope"></i> <%#Eval("Title") %></a></div>
                        <div class="item_op">
                            <a href="MailWrite.aspx?reply=<%#Eval("ID") %>"><i class="fa fa-mail-reply" title="回复"></i></a>
                            <a href="javascript:;" onclick="email.del(<%#Eval("ID") %>);"><i class="fa fa-trash-o" title="删除"></i></a>
                            <span class="item_from">来自：<%#Eval("Receiver") %></span>
                            <span class="item_date"><%#Eval("MailDate","{0:yyyy年MM月dd日 HH:mm}") %></span>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div runat="server" id="Rece_Empty" class="empty_body" visible="false">
                <div class="text-center"><i class="fa fa-mixcloud" style="font-size:120px;"></i></div>
                <div class="empty_txt">收件箱中没有信息</div>
            </div>
        </div>
    </div>
    <div class="colwrap platcontainer col-md-3">
        <div class="col_head">
            <span class="title"><i class="fa fa-history"></i> 已发邮件</span>
            <div class="opwrap">
                <a href="MailWrite.aspx"><i class="fa fa-pencil-square" style="font-size:25px;"></i> 新邮件</a>
                <a href="MailList.aspx?mailtype=1" title="我的邮件"><i class="fa fa-ellipsis-h"></i></a>
            </div>
        </div>
        <div class="col_body">
            <asp:Repeater runat="server" ID="Send_RPT" EnableViewState="false">
                <ItemTemplate>
                    <div class="item" id="email_<%#Eval("id") %>">
                        <div class="item_tit"><a href="MailDetail.aspx?ID=<%#Eval("ID") %>" title="浏览"><i class="fa fa-envelope"></i> <%#Eval("Title") %></a></div>
                        <div class="item_op">
                            <a href="MailDetail.aspx?ID=<%#Eval("ID") %>"><i class="fa fa-eye" title="浏览"></i></a>
                            <a href="javascript:;" onclick="email.del(<%#Eval("ID") %>);"><i class="fa fa-trash-o" title="删除"></i></a>
                            <span class="item_from">来自：<%#Eval("Receiver") %></span>
                            <span class="item_date"><%#Eval("MailDate","{0:yyyy年MM月dd日 HH:mm}") %></span>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div runat="server" id="Send_Empty" class="empty_body" visible="false">
                <div class="text-center"><i class="fa fa-coffee" style="font-size:120px;"></i></div>
                <div class="empty_txt">发件箱中没有信息</div>
            </div>
        </div>
    </div>
    <div class="colwrap platcontainer col-md-3">
        <div class="col_head">
            <span class="title"><i class="fa fa-paper-plane"></i> 站内信息</span>
            <div class="opwrap">
                <a href="/Plat/Mail/MessageSend.aspx"><i class="fa fa-plus" style="font-size:25px;"></i> 新联络</a>
                <a href="/Plat/Mail/" title="更多信息"><i class="fa fa-ellipsis-h"></i></a>
            </div>
        </div>
        <div class="col_body">
            <asp:Repeater runat="server" ID="SiteMail_RPT" EnableViewState="false">
                <ItemTemplate>
                    <div class="item" id="mail_<%#Eval("MsgID") %>">
                        <div class="item_tit"><a href="/Plat/Mail/MessageRead.aspx?id=<%#Eval("MsgID") %>"><i class="fa fa-comment"></i> <%#Eval("Title") %></a></div>
                        <div class="item_op">
                            <a href="/Plat/Mail/MessageSend.aspx?ID=<%#Eval("MsgID") %>"><i class="fa fa-mail-reply" title="回复"></i></a>
             <%--               <a href="javascript:;" onclick="email.mail_del(<%#Eval("MsgID") %>);"><i class="fa fa-trash-o" title="删除"></i></a>--%>
                            <span class="item_from">来自：<%#GetUName() %></span>
                            <span class="item_date"><%#Eval("PostDate","{0:yyyy年MM月dd日 HH:mm}") %></span>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
             <div runat="server" id="SiteMail_Empty" class="empty_body" visible="false">
                <div class="text-center"><i class="fa fa-bell" style="font-size:120px;"></i></div>
                <div class="empty_txt">没有站内信</div>
            </div>
        </div>
    </div>
    <div class="col-md-1"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Modal/APIResult.js"></script>
<script>
$(function () { leftnav.enabled = false; setactive("办公"); });
var email = { api: "/Plat/Email/email.ashx?action=" };
email.del = function (id) {
    if (!confirm("确定要删除吗?")) { return false; }
    var $item = $("#email_" + id);
    $item.remove();
    $.post(email.api + "del", { "ids": id }, function (data) {
        var model = APIResult.getModel(data);
        if (!APIResult.isok(model)) { console.log(model.retmsg); }
    })
}</script>
</asp:Content>