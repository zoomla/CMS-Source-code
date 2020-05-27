<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailManage.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Mail.MailManage" MasterPageFile="~/Common/Master/UserEmpty.master"  ValidateRequest="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>邮箱管理</title> 
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="mainDiv">
<div class="mainDiv_l">
<div class="Messge_topul">
<ul class="Messge_nav Messge_navbold">
<li id="MessageSend"><a href="javascript:;" data-target="/Office/Mail/MessageSend.aspx"><i class="fa fa-pencil-square"></i>写信</a></li>
<li id="Message"><a href="javascript:;" data-target="/Office/Mail/MessageSend.aspx"><i class="fa fa-envelope-square"></i>收信</a></li>
<li id="Contact"><a href="javascript:;" data-target="/Office/Other/StructList.aspx?action=struct&value=2"><i class="fa fa-users"></i>通讯录</a></li>
</ul>
</div>
<div class="Messge_mainul">
<ul class="Messge_nav"> 
<li><a href="javascript:;" data-target="/Office/Mail/Message.aspx">收件箱<span style="color:red;">(未读<%=GetUnreadMsg()%>)</span></a></li> 
<li id="MessageOutbox"><a href="javascript:;" data-target="/Office/Mail/MessageOutbox.aspx">发件箱</a></li>
<li id="MessageDraftbox"><a href="javascript:;" data-target="/Office/Mail/MessageDraftbox.aspx">草稿箱</a></li>
<li id="Message"><a href="javascript:;" data-target="/Office/Mail/Message.aspx">查找邮件</a></li>
<li id="MessageGarbagebox"><a href="javascript:;" data-target="/Office/Mail/MessageGarbagebox.aspx">垃圾箱</a></li>
</ul>
</div>
<div class="clearfix"></div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
$().ready(function (e) { 
    $(".Messge_nav li a").click(function (e) {
        var str = $(this).attr("data-target");
        if (str != "") {
            $(".Messge_nav li").removeClass("active");
            $(this).parent().addClass("active");
            parent.showRightCnt(str);
        }
    });
}); 
</script>
</asp:Content>