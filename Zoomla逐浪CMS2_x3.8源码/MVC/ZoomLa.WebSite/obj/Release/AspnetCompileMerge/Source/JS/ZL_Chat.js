var curChat;
var ZL_Chat = function () { curChat = this };

ZL_Chat.prototype.ajaxurl = "chat.ashx";
ZL_Chat.prototype.boundary = "------asjdfohponzvnzcvapowtunzafadsfwt";
ZL_Chat.prototype.othertlp = "<div class='otherchat'>"
             + "<div class='otherchat_face'><img src='@UserFace' class='userface otherface' onerror='this.src=\"/Images/userface/noface.png\";' /></div>"
             + "<div class='chat_cont_c'><p class='other_time'><span class='remindgray'>(@CDate)</span></p><div class='chat_content othercontent'>@Content</div><span class='arrow-before arrow'></span><span class='arrow-after arrow'></span></div></div>";//别人的模板
ZL_Chat.prototype.mytlp = "<div class='mychat'>"
               + "<div class='otherchat_face'><img src='@UserFace' class='userface otherface' onerror='this.src=\"/Images/userface/noface.png\";' /></div>"
               + "<div class='mychat_c'><p class='other_time'><span class='remindgray'>(@CDate)</span></p><div class='chat_content'>@Content</div><span class='arrow-before arrow'></span><span class='arrow-after arrow'></span></div></div>";//我说的话模板
ZL_Chat.prototype.myinfo = { UserID: "", UserName: "", UserFace: "", CDate: "00:00:00" };
ZL_Chat.prototype.chat_body_tlp = "<div id='chat_body_@uid' class='chat_body'><div class='chat_div'></div></div>";

//仅刷新游客列表,加上新登录的游客,10秒检测一次
ZL_Chat.prototype.GetOnlineList = function () {
    var onlinetlp = "<li onclick='ChangeTalker(\"@UserID\",\"@UserName\");' class='list_item' id='list_item_@UserID'>"
                          + "<img src='/Images/userface/noface.png' class='member_face'>"
                          + "<p class='member_nick'>@UserName<br>"
                          + "<span class='isonline remindgray isonline_@UserID'>[在线]</span>"
                          + "<span id='unread_@UserID' class='badge'></span></p>"
                          + "<div style='clear:both;'></div></li>";
    $.ajax({
        type: "Post",
        url: curChat.ajaxurl,
        data: { action: "getonlinelist" },
        success: function (data) {
            if (data != "" && data != "[]") {
                data = JSON.parse(data);//所有在线游客json

                for (var i = 0; i < data.length; i++) {
                    if ($("#list_item_" + data[i].UserID).length < 1) {
                        var tlp = onlinetlp.replace(/@UserID/g, data[i].UserID.replace(/ /g, "")).replace(/@UserName/g, data[i].UserName);
                        $("#visitorlist").append(tlp);
                    }//for end;
                }
            }
        }
    });
}
//更新在线用户信息
ZL_Chat.prototype.UpdateOnline = function (ids) {
    $(".isonline").text("[不在线]");
    var onlinearr = ids.split(',');
    for (var i = 0; i < onlinearr.length; i++) {
        if (onlinearr[i] == "") continue;
        $(".isonline_" + onlinearr[i]).text("[在线]");
    }
    $("#onlineids_text").val(ids);
}
//获取在线信息
ZL_Chat.prototype.GetOnline = function () {
    var ref = this;
    $.ajax({
        type: "Post",
        url: curChat.ajaxurl,
        data: { action: "getonline" },
        success: function (data) {
            ref.UpdateOnline(data);
        },
        error: function (data) {
        }
    });
}
//获取消息
ZL_Chat.prototype.GetMsg = function () {
    var ref = curChat;
    $.ajax({
        type: "Post",
        url: "chat.ashx",
        data: { action: "getmsg", uid: ref.myinfo.UserID, rece: $("#ReceUser_Hid").val() },
        success: function (data) {
            ref.GetMsgCallBack(data);
        },
        error: function (data) {
        }
    });
}
//获取历史聊天记录
ZL_Chat.prototype.GetHistoryMsg = function (uid) {
    var ref = curChat;
    $.ajax({
        type: "Post",
        url: "chat.ashx",
        data: { action: "gethistorymsg", uid: ref.myinfo.UserID, rece: $("#ReceUser_Hid").val() },
        success: function (data) {
            if (data != "" && data != "[]") {
                var $curbody = ref.GetCurBody();
                var arr = data.split(ref.boundary);
                var json = JSON.parse(arr[0]);
                var msg = "";
                for (var i = 0; i < json.length; i++) {
                    if (json[i].Type == "other") { msg += ref.TlpReplace(ref.othertlp, json[i], arr[(i + 1)]); }
                    else { msg += ref.TlpReplace(ref.mytlp, json[i], arr[(i + 1)]); }
                }
                $curbody.append(msg);
                ref.setScrollBottom();
            }
        },
        error: function (data) {
        }
    });
}
//获取当前内容域
ZL_Chat.prototype.GetCurBody = function () {
    var uid = $("#ReceUser_Hid").val();
    return $("#chat_body_" + uid).find(".chat_div");
}
ZL_Chat.prototype.setScrollBottom = function () {
    var $curbody = curChat.GetCurBody();
    var uid = $("#ReceUser_Hid").val();
    $("#chat_body_" + uid).scrollTop($curbody.height() + 30);
}
//更新是否收到新的消息
ZL_Chat.prototype.UpdateUnread = function (ids) {
    var hasmsg = false;
    var arr = ids.split(",");
    for (var i = 0; i < arr.length; i++) {
        if (arr[i] == "" || arr[i] == $("#ReceUser_Hid").val()) continue;//有信息,并且不是当前聊天人
        else {
            if ($("#unread_" + arr[i]).text() == "") {
                $("#unread_" + arr[i]).text("未读");
                hasmsg = true;
            }
        }
    }//for end;
    if (hasmsg) { document.getElementById("msg_ad").play(); }//提示
}
//获取消息的回调函数
ZL_Chat.prototype.GetMsgCallBack = function (data) {
    if (data != "" && data != "[]") {
        var $curbody = this.GetCurBody();
        var arr = data.split(this.boundary);
        var json = JSON.parse(arr[0]);
        for (var i = 0; i < json.length; i++) {
            var msg = this.TlpReplace(this.othertlp, json[i], arr[(i + 3)]);
            $curbody.append(msg);
            setTimeout(this.setScrollBottom, 1);
        }
        this.UpdateOnline(arr[1]);
        this.UpdateUnread(arr[2]);
    }
}
ZL_Chat.prototype.BeginInit = function () {
    this.GetOnline();
    //$("#list_item_" + myinfo.UserID).hide(); //隐掉自己
    interval = setInterval(this.GetMsg, 2000);
    onlineInterval = setInterval(this.GetOnlineList, 10000);
}
//内容替换
ZL_Chat.prototype.TlpReplace = function (tlp, json, content) {
    return tlp.replace("@UserFace", json.UserFace).replace("@UserName", json.UserName)
                           .replace("@CDate", json.CDate).replace("@Content", content);
}
//发送消息
ZL_Chat.prototype.SendMsg = function () {
    var msg = chatue.getContent();
    var $curbody = this.GetCurBody();
    console.log(msg, $("#ReceUser_Hid").val());
    if (msg == "" || $("#ReceUser_Hid").val() == "") { return; }
    $.ajax({
        type: "Post",
        url: "chat.ashx",
        data: { action: "sendmsg", content: msg, rece: $("#ReceUser_Hid").val(), uid: this.myinfo.UserID },
        success: function (data) { },
        error: function (data) { }
    });
    var date = new Date();
    this.myinfo.CDate = date.getHours() + ":" + date.getMinutes() + ":" + date.getSeconds();
    $curbody.append(this.TlpReplace(this.mytlp, this.myinfo, msg));
    $("#content").val("");//必须要清下,否则有换行等Bug
    chatue.setContent("");
    setTimeout(this.setScrollBottom, 1);
}
//添加用户进入在线用户
ZL_Chat.prototype.AddOnline = function (uname, uid) {
    var ref = this;
    $.ajax({
        type: "Post",
        url: curChat.ajaxurl,
        data: { action: "userlogin", name: uname, userid: uid },
        success: function (data) {
            $('#modeldiv').modal('hide');//这里之后再getmsg等
            ref.myinfo = JSON.parse(data);
            ref.myinfo.CDate = "00:00:00";
            ref.BeginInit();
            $("#myfaceimg").attr("src", ref.myinfo.UserFace);
            $("#myid_t").val(ref.myinfo.UserID);
            $("#myname_t").val(ref.myinfo.UserName);
            $("#UserName_L").val(ref.myinfo.UserName);
            $("#UserID_Hid").val(ref.myinfo.UserID);
        }
    });//IsLogged end;
}
//获取指定ID的聊天窗体,如果无则新建
ZL_Chat.prototype.GetBodyByID = function (uid) {
    $chatbody = $("#chat_body_" + uid);
    if ($chatbody.length < 1)//不存在,新建
    {
        $("#chat_body_list").append(this.chat_body_tlp.replace("@uid", uid));
        $chatbody = $("#chat_body_" + uid);
        this.GetHistoryMsg(uid);
    }
    return $chatbody;
}
//改变交谈对象
ZL_Chat.prototype.ChangeTalker = function (uid, uname) {
    if (uid == "" || uid == $("#ReceUser_Hid").val()) return;
    $("#sendbtn")[0].disabled = "";
    $("#uinfo_name").text(uname);
    $("#ReceUser_Hid").val(uid);
    $("#unread_" + uid).text("");
    $(".chat_body").hide();
    $chatbody = this.GetBodyByID(uid);
    $chatbody.show();
    this.setScrollBottom();
    this.GetMsg();
    //if (interval == null) {
    //    interval = setInterval(GetMsg, 2000);
    //}
    return false;
}
//读取用户的未读消息
ZL_Chat.prototype.GetUnreadMsg = function () {
    var ref = curChat;
    $.ajax({
        type: "Post",
        url: "chat.ashx",
        data: { action: "getunreadmsg", uid: ref.myinfo.UserID },
        success: function (data) {
            ref.ReadMsg(data);
        },
        error: function (data) {
            console.error(data);
        }
    });
}
ZL_Chat.prototype.ReadMsg = function (data) {
    if (data != "" && data != "[]") {
        var list = [];
        var arr = data.split(this.boundary);
        var json = JSON.parse(arr[0]);
        for (var i = 0; i < json.length; i++) {
            json[i].Content = arr[(i + 1)];
        }
        console.log(json);
        return json;
    }
    console.log("data",data);
}