var note = {
    getModel: function () {
        return { type: "", id: note.createID(), orderID: note.maxOrderID(), text: "", content: "", openText: false };
    },
    Video: {
        add: function (videoType, content) {
            var com = note.getModel();
            com.type = "video";
            com.videoType = videoType;
            com.content = content;
            note.addToList(com);
        },
        getvideo: function (model) {
            if (!model.content || model.content == "") { return ""; }
            return '<video width="600" height="400" src="' + model.content + '" poster="/Template/V3/style/Images/l_logo.jpg" class="edui-upload-video vjs-default-skin video-js" preload="none" controls=""><source src="' + model.content + '" type="video/mp4"></video>';
        },
        getonline: function (model) {
            //直接写html会出错
            return '<embed type="application/x-shockwave-flash" src="' + model.content + '" quality="high" align="middle" allowscriptaccess="false" allowfullscreen="true" wmode="transparent" width="600" height="400">';
        }
    },
    Img: {
        add: function (src) {
            var com = note.getModel();
            com.type = "image";
            com.content = src;
            note.addToList(com);
        }
    },
    Empty: {
        //空节点,仅包含文字
        add: function () {
            var com = note.getModel();
            note.addToList(com);
        }
    },
    Para: {
        add: function (title, content) {
            var com = note.getModel();
            com.type = "para";
            com.title = title;
            com.content = content;
            note.addToList(com);
        },
        edit: function (item) {
            showPara(item.id);
        }
    },
    Text: {//文本节点,仅在移动端可添加
        add: function () {
            var com = note.getModel();
            com.type = "text";
            note.addToList(com);
        }
    },
    addToList: function (com) {
        //将组件添加入列表,如果第一个节点为空,则修改其,否则直接插入
        var first = scope.comMod.comlist[0];
        if (first && first.type == "") {
            com.text = first.text;
            com.openText = first.openText;
            scope.comMod.comlist[0] = com;
        }
        else { scope.comMod.comlist.push(com); }
    },
    delcom: function (item) {
        scope.hasedit = true;
        scope.comMod.comlist.RemoveByID(item.id);
        //scope.$digest();
    }
};
note.createID = function () {
    var len = ($(".com").length + 1);
    return "com-" + len;
}
note.maxOrderID = function () {
    var len = ($(".com").length + 1);
    return len;
}
note.preAddcom = function (type) {
    scope.hasedit = true;
    var id = note.createID();
    switch (type) {
        case "text":
            note.Text.add();
            break;
        case "image":
            ZL_Webup.config.json = { accept: "img", pval: { "id": id, type: "img" } };
            ZL_Webup.ShowFileUP();
            //note.Img.add(id);
            break;
        case "video":
            //ZL_Webup.config.json = { pval: { "id": id, type: "video" } };
            //ZL_Webup.ShowFileUP();
            var url = $("#video_local_t").val();
            if (!ZL_Regex.extCheck(url, "mp4,swf")) { alert("请输入正确的mp4|swf文件地址"); return false; }
            note.Video.add("video", url);
            hideUPVideo();
            break;
        case "online":
            var url = $("#video_online_t").val();
            if (!ZL_Regex.extCheck(url, "swf")) { alert("请输入正确的swf文件地址"); return false; }
            note.Video.add("video", url);
            note.Video.add("online", url);
            hideUPVideo();
            break;
        case "para":
            showPara();
            //note.Para.add();
            break;
    }
    scope.$digest();
};
note.autoSave = function () {
    setInterval(function () {
        note.preSave();
        $.post("", { "action": "save", value: $("#Save_Hid").val() }, function (data) {
            scope.comMod.id = data;
            console.log("autoSave", data);
        });
    }, (60 * 1000));
}
note.preSave = function () {
    scope.comMod.pic = $(".com-img_item img:first").attr("src");
    $("#Save_Hid").val(angular.toJson(scope.comMod));
    scope.readysave = true;
    return true;
}
note.save = function () {
    note.preSave();
    if (scope.comMod.title == "") { alert("标题不能为空!"); return; }
    $("#Save_Btn").click();
}
note.showText = function (item) {
    //如果未显,则显示,否则聚焦
    if (item.openText == false || item.openText == undefined) { item.openText = true; return; }
    else {
        note.alertText(item.id + "_text");
    }
}
note.hideText = function (item) {
    //隐掉,是否同样把文本清除掉
    item.openText = false;
}
note.alertText = function (id, time) {
    if (!time || time < 1) time = 3;
    var $text = $("#" + id);
    $text.focus();
    for (var i = 0, span = 200; i < time; i++) {
        setTimeout(function () { $text.css("background-color", "#f9f2f4") }, span);
        span += 200;
        setTimeout(function () { $text.css("background-color", "#fff") }, span);
        span += 200;
    }
}