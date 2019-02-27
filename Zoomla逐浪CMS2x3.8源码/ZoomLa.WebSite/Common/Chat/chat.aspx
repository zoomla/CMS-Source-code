<%@ Page Language="C#" AutoEventWireup="true" CodeFile="chat.aspx.cs" Inherits="test_chat" ClientIDMode="Static" ValidateRequest="false" EnableViewState="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>在线聊天</title>
<style>
* { margin:0;}
body { background:url(chat_bg.jpg) center no-repeat; background-size:cover;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

<div class="container">
<div class="chat_top">
<img class="chat_top_l" src="chat_logo.png" onerror='this.src="/Images/userface/noface.png";' />
<span id="uinfo_name">请先选择用户</span>
<span class="chat_top_r margin_l5" title="点击关闭聊天窗口"><a href="javascript:;" onclick="closechat();" ><i class="fa fa-close"></i></a></span>
<span class="chat_top_r margin_r5" data-toggle="tooltip" data-placement="left" title="点击查看历史记录"><a href="javascript:;" onclick="ShowHistory();"><i class="fa fa-list-alt"></i> 历史记录</a></span>
</div>
<div class="chat_main">
<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12 hidden-xs chat_main_r" style="padding:0">
<div class="chat_main_rt">
<!-- Nav tabs -->
<ul class="nav nav-tabs" role="tablist">
<li role="presentation" class="active"><a href="#home" aria-controls="home" role="tab" data-toggle="tab">客服人员</a></li>
<li role="presentation"><a href="#profile" aria-controls="profile" role="tab" data-toggle="tab">我的好友</a></li>
</ul>
</div>
<!-- Tab panes -->
<div class="tab-content">
<div role="tabpanel" class="tab-pane active" id="home">
<div class="chat_main_rc">
<ul class="media-list">
<asp:Repeater runat="server" ID="Customs_RPT">
<ItemTemplate>
<li onclick="ChangeTalker(<%#"'"+Eval("UserID")+"','"+Eval("UserName")+"','"+Eval("Salt")+"'" %>);" class="media" id="list_item_<%#Eval("HoneyName") %>">
<div class="media-left"><img src="<%#Eval("Salt") %>" class="media-object" onerror="shownoface(this);" /></div>
<div class="media-body media-middle">
<h4 class="media-heading"><%#Eval("UserName") %> <span class="isonline remindgray isonline_<%#Eval("HoneyName") %>">(检测中)</span></h4>
<span id="unread_<%#Eval("HoneyName") %>" class="badge"></span>
</div>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
</div>
<div role="tabpanel" class="tab-pane" id="profile">
<div class="chat_main_rc">
<ul class="media-list">
<asp:Repeater runat="server" ID="Friend_RPT">
<ItemTemplate>
<li onclick="ChangeTalker(<%#"'"+Eval("UserID")+"','"+Eval("UserName")+"','"+Eval("Salt")+"'" %>);" class="media" id="list_item_<%#Eval("HoneyName") %>">
<div class="media-left"><img src="<%#Eval("Salt") %>" class="media-object" onerror="shownoface(this);" /></div>
<div class="media-body media-middle">
<h4 class="media-heading"><%#Eval("UserName") %> <span class="isonline remindgray isonline_<%#Eval("HoneyName") %>">(检测中)</span></h4>
<span id="unread_<%#Eval("HoneyName") %>" class="badge"></span>
</div>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</div>
</div>
</div>
</div>

<div class="col-lg-9 col-md-9 col-sm-9 col-xs-12 chat_main_l" style="padding:0">
<div class="chat_container">
<div id="chat_body_list"></div>
<div id="mymsg">
<textarea id="content" style="height: 135px;"></textarea>
<div class="chat_send">
<span class="remindgray">支持Ctrl+Enter快捷键发送信息</span>
<button type="button" id="sendbtn" class="btn btn-primary pull-right" disabled="disabled" onclick="SendMsg();">发送</button>
<div class="clearfix"></div>
</div>
</div>
</div>
</div>
<div class="clearfix"></div>
</div>
<div class="alert alert-info hidden-xs char_alert" role="alert">
欢迎[<asp:Label runat="server" ID="UserName_L" />]，您可以通过UID来指定聊天对象，如：/Common/Chat/Chat.aspx?uid=1[<a href="javascript:;" onclick="copyToClipBoard();">复制会议室邀请</a>] [<a href="javascript:;" onclick="ShowModal();">退出系统</a>]
<asp:HiddenField runat="server" ID="UserID_Hid" />
</div>
</div>

<div class="list_body" hidden>
<div class="chat_header">
<asp:TextBox CssClass="form-control" placeholder="搜索联系人" onkeyup="SearchUserList()" ID="UserSearch_T" runat="server"></asp:TextBox></div>
<ul style="height: 426px; overflow-y: auto;">
<ul>
<li class="list_group" id="visitor_ul" visible="false" runat="server">
<div class="list_group_title">
<span class="fa fa-chevron-right remindgray"></span>
<span>游客</span>
<span class="remindgray" style="float: right; margin-right: 10px;">
<asp:Label runat="server" ID="Unum_L"></asp:Label>
</span>
<div style="clear: both; height: 8px;"></div>
</div>
</li>
<ul class="list_item_body" id="visitorlist">
<asp:Repeater runat="server" ID="Y_RPT">
<ItemTemplate>
<li onclick="ChangeTalker(<%#"'"+Eval("UserID")+"','"+Eval("UserName")+"','"+Eval("UserFace")+"'" %>);" class="list_item" id="list_item_<%#Eval("UserID") %>">
<img src="<%#Eval("UserFace") %>" class="member_face" onerror="shownoface(this);" />
<p class="member_nick">
<%#Eval("UserName") %><br />
<span class="isonline remindgray isonline_<%#Eval("UserID") %>">(检测中)</span>
<span id="unread_<%#Eval("UserID") %>" class="badge"></span>
</p>
<div style="clear: both;"></div>
</li>
</ItemTemplate>
</asp:Repeater>
</ul>
</ul>
<ul>
<li class="list_group" id="customs_ul" visible="false" runat="server">
<div class="list_group_title">
<span class="fa fa-chevron-right remindgray"></span>
<span>客服人员</span>
<span class="remindgray" style="float: right; margin-right: 10px;">
<asp:Label runat="server" ID="Label1" Text="0"></asp:Label>
</span>
<div style="clear: both; height: 8px;"></div>
</div>
</li>
<ul class="list_item_body">
</ul>
</ul>
<ul>
<li class="list_group">
<div class="list_group_title">
<span class="fa fa-chevron-right remindgray"></span>
<span>我的好友</span>
<span class="remindgray" style="float: right; margin-right: 10px;">
<asp:Label runat="server" ID="Friend_Num"></asp:Label>
</span>
<div style="clear: both; height: 8px;"></div>
</div>
</li>
</ul>
<ul class="list_search" style="display: none;">
</ul>
</ul>
</div>
<div style="float: left; padding-left: 10px; display: none;">
<input type="button" value="获取在线用户" onclick="GetOnlineList();" />
<div>
<audio src="msg.mp3" id="msg_ad" />
</div>
<ul>
<li>
<img id="myfaceimg" style="width: 80px; height: 80px;" onerror="shownoface(this);" /></li>
<li>名字:<input type="text" id="myname_t" /></li>
<li>ID:<input type="text" id="myid_t" /></li>
<li>在线用户：<textarea id="onlineids_text" style="height: 100px;"></textarea>
</li>
<li>与谁聊:<input type="text" id="ReceUser_Hid" />
<input type="button" value="测试" onclick="testF();" />
<input type="button" value="接收" onclick="GetMsg();" />
</li>
<li>获取在线用户:</li>
</ul>
<input type="text" id="test_fid" />
<button type="button" onclick="AddFriend()">添加好友</button>
</div>
<div class="modal fade" id="modeldiv" data-backdrop="static" style="top: 20%; left: 20%;">
<div class="modal-dialog" style="width: 300px;">
<div class="modal-content">
<ul class="nav nav-tabs">
<li class="active"><a href="#tab0" data-toggle="tab">用户登录</a></li>
<li><a href="#tab1" data-toggle="tab">游客登录</a></li>
</ul>
<div class="modal-body">
<div class="tab-content">
<div class="tab-pane active" id="tab0">
<input type="text" id="uname_t" class="form-control" placeholder="用户名" onkeyup="GetEnterCode('focus','pwd_t');" />
<input type="password" id="pwd_t" class="form-control" onkeyup="GetEnterCode('click','ulogin_btn');" />
<input type="button" id="ulogin_btn" value="登录" class="btn btn-primary" onclick="AJAXLogin(); disBtn(this, 2000);" />
<input type="button" value="注册" class="btn btn-default" onclick="GetToReg();" />
</div>
<div class="tab-pane" id="tab1">
<input type="text" id="visname_t" class="form-control" value="游客" onkeyup="GetEnterCode('click','vlogin_btn');" />
<input type="button" id="vlogin_btn" class="btn btn-primary" value="游客登录" onclick="VisitorLogin(); disBtn(this, 2000);" />
</div>
</div>
</div>
</div>
</div>
</div>
<asp:HiddenField runat="server" ID="Wel_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link type="text/css" href="chat.css" rel="stylesheet" />
<style type="text/css">
.form-control {max-width: 200px;margin-bottom: 10px;}
</style>
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.js?ver=3.1"></script>
<script src="/JS/Controls/B_User.js"></script>
<script src="/JS/ZL_Chat.js"></script>
<script>
//将ctrl+13取消,其为自动提交表单
var buser = new B_User();
var zl_chat = new ZL_Chat();
function GetToReg() {
    if (parent) { parent.location = "/User/Register.aspx?ReturnUrl=<%=Request.RawUrl %>"; } else { location = "/User/Register.aspx?ReturnUrl=<%=Request.RawUrl %>"; }
}
function AJAXLogin() {
buser.Login({ name: $("#uname_t").val(), pwd: $("#pwd_t").val() }, function (data) {
//将信息填充入隐藏字段用户ID:用户名:用户头像
if (data == "" || data == -1) { alert("用户名或密码错误!!"); }
else { zl_chat.AddOnline(); }
});
}
function VisitorLogin(name) {
var uname = $("#visname_t").val();
zl_chat.AddOnline(uname);
location = location;
}
function VisitorToLogin(name, uid) {
//管理员后台请求与用户聊天
zl_chat.AddOnline(name, uid);
//location = location;
}
function ShowModal() {
$("#modeldiv").modal({ keyboard: false });
}
//仅刷新游客列表,加上新登录的游客,10秒检测一次
function GetOnlineList() {
zl_chat.GetOnlineList();
}
function BeginInit() {
zl_chat.BeginInit();
}
function SetMyInfo(uid, uname, uface) {
zl_chat.myinfo.UserID = uid;
zl_chat.myinfo.UserName = uname;
zl_chat.myinfo.UserFace = uface;
zl_chat.myinfo.CDate = "00:00:00";
$("#myfaceimg").attr("src", zl_chat.myinfo.UserFace);
$("#myid_t").val(zl_chat.myinfo.UserID);
$("#myname_t").val(zl_chat.myinfo.UserName);
}
//关闭窗口
function closechat() {
    if (confirm("确定要关闭聊天窗口吗")) {
        closeWindows();
    }
}
function closeWindows() {
    var isIE = !!window.ActiveXObject || "ActiveXObject" in window;
    if (isIE) {
        this.focus();
        self.opener = this;
        self.close();
    } else {
        document.title = 'about:blank';
        location.replace('about:blank');
    }
}
//复制会话链接
function copyToClipBoard() {
    var url = window.location.href;
    var arr = url.split('?');
    window.clipboardData.setData('text', arr[0] + "?uid=" + $("#UserID_Hid").val());
}
//显示欢迎信息
function ShowWel(uname) {
var $cbody = zl_chat.GetBodyByID(0);
var msg = zl_chat.TlpReplace(zl_chat.othertlp, { UserFace: "", UserName: uname, CDate: "<%=DateTime.Now%>" }, $("#Wel_Hid").val());
$($cbody).append(msg);
setTimeout(zl_chat.setScrollBottom, 1);
}

var interval = null;
var chatue = null;
$(function () {
$(".list_group").click(function () {
$(this).parent().find(".list_item_body").toggle("fast");
});
chatue = UE.getEditor('content', {
    toolbars: [['Undo', 'Redo', 'Bold', 'Italic', 'NumberedList', 'BulletedList', 'Smiley', 'ShowBlocks', 'Maximize', 'underline', 'fontborder', 'strikethrough', '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'emotion', 'simpleupload']]
});
//监控回车事件
setTimeout(function () {
chatue.focus();
chatue.addListener("keydown", function (type, e) {
if (e.ctrlKey && e.keyCode == 13) { zl_chat.SendMsg(); return false; }
});
}, 1000);
});
//------------------------------
function SendMsg() {
zl_chat.SendMsg();
}
function ChangeTalker(uid, uname,userface)//支持切换
{
    $(".chat_top_l").attr("src", userface);
    return zl_chat.ChangeTalker(uid, uname);
}
function ShowHistory() {
var cuid = $("#ReceUser_Hid").val();
if (!cuid || cuid == "") { alert("尚未选定用户"); return; }
window.location.href="/Common/Chat/ChatHistory.aspx?suid=" + cuid;
}   
//---------------------------------
//用户好友列表
var userlist;
function InitUserList(obj) {
userlist = obj;
}
function SearchUserList() {
var text = $("#UserSearch_T").val();
$('#list_search').html('');
if (text != "") {
$('.list_group').hide();
$('.list_item_body').hide();
$('.list_search').show();
var listhtml = "";
var temp = "<li onclick=\"ChangeTalker(@fid,'@UserName','@UserFace');\" class='list_item' id='list_item_@fid'><img src='@UserFace' class='member_face' onerror=\"shownoface(this);\" /><p class='member_nick'>@UserName<br /><span class='isonline remindgray isonline_@fid'>(检测中)</span><span id='unread_@fid' class='badge'></span></p><div style='clear:both;'></div></li>";
for (var i = 0; i < userlist.length; i++) {
if (userlist[i].UserName.indexOf(text) > -1) {
listhtml += temp.replace(/@fid/g, userlist[i].Fid).replace(/@UserName/g, userlist[i].UserName).replace(/@UserFace/g, userlist[i].UserFace);
}
}
$('.list_search').html(listhtml);
} else {
$('.list_group').show();
$('.list_search').hide();
}
}
//添加好友
function AddFriend() {
$.ajax({
type: 'POST',
url: '/API/UserCheck.ashx',
data: { action: 'AddFriend', value: $('#test_fid').val() },
success: function (data) {
if (data != "[]" && data != "-1") {
var obj = JSON.parse(data);
userlist.push(obj[0]);
var temp = "<li onclick=\"ChangeTalker(@fid,'@UserName','@UserFace');\" class='list_item' id='list_item_@fid'><img src='@UserFace' class='member_face' onerror=\"shownoface(this);\" /><p class='member_nick'>@UserName<br /><span class='isonline remindgray isonline_@fid'>(检测中)</span><span id='unread_@fid' class='badge'></span></p><div style='clear:both;'></div></li>";
var html = temp.replace(/@fid/g, obj[0].Fid).replace(/@UserName/g, obj[0].UserName).replace(/@UserFace/g, obj[0].UserFace);
$('.myfriends').append(html);
$("#Friend_Num").text(parseInt($("#Friend_Num").text()) + 1);
}
}
});
}
$(function () {
  $('[data-toggle="tooltip"]').tooltip()
})
</script>
</asp:Content>
