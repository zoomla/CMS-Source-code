//不同的主题,进行不同的实现即可
//多个聊天框实现多个对象即可,名字不同
var ZLChat = function () { };
ZLChat.prototype.Init = function () {
    this.AddChatDiv();
    this.AddCSS();
}
ZLChat.prototype.OpenChat = function () {
    var config = $("#zlchat").data("option");
    var url = "/Common/Chat/Chat.aspx?CodeID="+config.id;
    var iTop = (window.screen.availHeight - 30 - 550) / 2;
    var iLeft = (window.screen.availWidth - 10 - 960) / 2;
    window.open(url, "_chat", 'height=590, width=610,top=' + iTop + ',left=' + iLeft + ',toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');
}
ZLChat.prototype.AddChatDiv = function () {
    //添加Html至页面并绑定事件
    var ref = this;
    var tlp = $('<a href="javascript:;" style="position:fixed;right:10px;bottom:80px;z-index:10;" class="chat_help" title="点击咨询"><img src="/JS/Plugs/Chat/help.png" /></a>');
    tlp.click(function () { ref.OpenChat(); });
    $("body").append(tlp);
}
ZLChat.prototype.AddCSS = function () {
    //将css加至头部
    //$("head").append('<link href="/JS/Plugs/Chat/chat_def.css" rel="stylesheet" />');
}
ZLChat.prototype.EventBind = function () { };
$(function () {
    var chat = new ZLChat();
    chat.Init();
})