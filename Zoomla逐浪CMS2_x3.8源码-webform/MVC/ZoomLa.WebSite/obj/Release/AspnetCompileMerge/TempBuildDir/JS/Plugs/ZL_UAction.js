var ZL_UConfig = {
    item: 1,
    storage: "ZL_UActionList",
    server: "/API/UAction.ashx"
};

var ZL_UAction = function () { };
ZL_UAction.prototype = {
    Init: function () {
        var ref = this;
        if (!localStorage[ZL_UConfig.storage] || localStorage[ZL_UConfig.storage] == "") {localStorage[ZL_UConfig.storage] = "[]"; }
        if (!localStorage["idflag"]) { localStorage["idflag"] = ref.GetRanPass(10); }//以此作为身份标识
        ref.CollectInfo();
        setInterval(function () { ref.ServerListener(ref); }, 5000);//每十秒监听一次请求
        console.log("Init ok;", localStorage["idflag"], JSON.parse(localStorage[ZL_UConfig.storage]));
    },
    CollectInfo: function () {
        var model = { title: "", pageurl: "", uid: "", uname: "", ip: "", action: "page", idflag: localStorage["idflag"] };
        if ($("title").length > 0) {
            model.title = $("title").text().replace(/ /g, "");
        }
        model.pageurl = location.href;
        this.AddItem(model);
    },
    AddItem: function (model) {
        var ref = this;
        var list = JSON.parse(localStorage[ZL_UConfig.storage]);
        //Url与最近一次存入的不同才存入,避免刷新页面造成写入
        if (list.length > 0) {
            if (model.pageurl != list[(list.length - 1)].pageurl) {
                list.push(model);
            }
        } else { list.push(model); }
        //达到提交次数,则提交再清空
        if (list.length >= ZL_UConfig.item) {
            ref.PostToServer(list);
            localStorage[ZL_UConfig.storage] = "[]";
        }
        else { localStorage[ZL_UConfig.storage] = JSON.stringify(list); }
    },
    /*--服务器方法--*/
    ServerListener: function (ref) {
        //获取服务端消息,事件处理
        $.post(ZL_UConfig.server, { action: "event", idflag: localStorage["idflag"] }, function (data) {
            if (!data || data == "") { return; }
            console.log("rece:"+data);
            data = JSON.parse(data);
            switch (data.action) {
                case "chat":
                    ref.OpenChat(data.uid);
                    break;
            }
        })
    },
    PostToServer: function (list) {//满足条件,提交数据给服务端
        $.post(ZL_UConfig.server, { action: "add", data: JSON.stringify(list) }, function (data) {
            //console.log("add finished");
        });
    },
    OpenChat: function (uid) {
        var ref = this;//绕过弹窗限制
        var hrefBtn = $('<a href="javascript:;"/>')
        hrefBtn.click(function () { ref.OpenChatWin(uid); });
        hrefBtn.trigger("click");
    },
    OpenChatWin: function (uid) {
        if (!$("#chatdiag_div")[0]) {
            var tlp = "<div id='chatdiag_div' style='position:fixed;top:20%;left:30%;overflow:hidden;width:620px; height:550px;'><iframe src='@url' style='width:620px;height:700px; border:none;'></iframe></div>";
            var url = "/Common/Chat/Chat.aspx?uid=" + uid + "&login=visitor&idflag=" + localStorage["idflag"];
            $('body').append(tlp.replace(/@url/, url));
        }
        //var iTop = (window.screen.availHeight - 30 - 550) / 2;
        //var iLeft = (window.screen.availWidth - 10 - 960) / 2;
        //var chatwin = window.open(url, "_chat", 'height=590, width=610,top=' + iTop + ',left=' + iLeft + ',toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');
    }
};
ZL_UAction.prototype.GetRanPass = function (length, special) {
    var iteration = 0; var password = ""; var randomNumber; if (special == undefined) { var special = false; }
    while (iteration < length) {
        randomNumber = (Math.floor((Math.random() * 100)) % 94) + 33; if (!special) {
            if ((randomNumber >= 33) && (randomNumber <= 47)) { continue; }
            if ((randomNumber >= 58) && (randomNumber <= 64)) { continue; }
            if ((randomNumber >= 91) && (randomNumber <= 96)) { continue; }
            if ((randomNumber >= 123) && (randomNumber <= 126)) { continue; }
        }
        iteration++; password += String.fromCharCode(randomNumber);
    }
    return password;
}
$(function () {
    new ZL_UAction().Init();
})
function ChatClose() {
    $("#chatdiag_div").remove();
}