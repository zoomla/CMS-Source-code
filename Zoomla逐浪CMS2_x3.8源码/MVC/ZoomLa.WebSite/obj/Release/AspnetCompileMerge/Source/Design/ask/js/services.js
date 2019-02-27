angular.module('starter.services', [])

.factory('Ask', function () {
    var api = "/design/ask/server/ask.ashx?action="
    return {
        get: function (id, callback) {
            $.post(api + "get", { "id": id }, function (data) {
                APIResult.ifok(data, callback);
            })
        },
        add: function (model, callback, err) {
            if (ZL_Regex.isEmpty(model.Title)) { err("标题不能为空"); return; }
            if (moment(model.EndDate).fromNow(false).indexOf("ago") > -1) { err("终止时间不能早于或等于当前时间"); return; }
            $.post(api + "add", { "model": angular.toJson(model) }, function (data) {
                APIResult.ifok(data, callback, err);
            });
        },
        del: function (id, callback) {
            $.post(api + "del", { "id": id }, function (data) {
                APIResult.ifok(data, callback);
            });
        },
        list: function (opts, callback) {
            $.post(api + "list", {}, function (data) {
                APIResult.ifok(data, callback);
            })
        },
        newModel: function () { return { "ID": 0, "Title": "", "CUser": 0, "Remind": "", "ZType": 0, "ZStatus": 0,PreViewImg:"",EndDate:"2016/12/01" } }
    };
})
.factory("Question", function () {
    var api = "/design/ask/server/question.ashx?action=";
    return {
        get: function (id, callback) {
            $.post(api + "get", { "id": id }, function (data) {
                var model = APIResult.getModel(data);
                if (APIResult.isok(model)) {
                    if (!model.result.QOption) { model.result.QOption = []; }else { model.result.QOption = JSON.parse(model.result.QOption); }
                    if (!model.result.QFlag) { model.result.QFlag = []; }else { model.result.QFlag = JSON.parse(model.result.QFlag); }
                    callback(model.result);
                }
                else { alert("获取Question失败"); }
            })
        },
        add: function (model, callback,err) {
            if (ZL_Regex.isEmpty(model.QTitle)) { err("标题不能为空"); return; }
            switch (model.QType)
            {
                case "radio":
                case "checkbox":
                case "sort":
                    var options = [];
                    model.QOption.forEach(function (item, index) {
                        if (!ZL_Regex.isEmpty(item.text)) { options.push(item); }
                    })
                    if (options.length < 2) { err("最少需要两个选项,并且不能为空"); return; }
                    model.QOption = options;
                    break;
            }
            var json = angular.toJson(model);
            json = JSON.parse(json);
            json.QOption = JSON.stringify(json.QOption);
            json.QFlag = JSON.stringify(json.QFlag);
            $.post(api + "add", { "model": JSON.stringify(json) }, function (data) { APIResult.ifok(data, callback, err); });
        },
        del: function (id, callback) {
            $.post(api + "del", { "id": id }, function (data) { APIResult.ifok(data, callback); })
        },
        list: function (opts, callback) {
            $.post(api + "list", opts, function (data) {
                var model = APIResult.getModel(data);
                if (APIResult.isok(model)) {
                    for (var i = 0; i < model.result.length; i++) {
                        if (!model.result[i].QOption) { model.result[i].QOption = []; } else { model.result[i].QOption = JSON.parse(model.result[i].QOption); }
                        if (!model.result[i].QFlag) { model.result[i].QFlag = []; } else { model.result[i].QFlag = JSON.parse(model.result[i].QFlag); }
                    }
                    callback(model.result);
                } else { alert(data); }
            });
        },
        move: function (from, target, callback) {
            $.post(api + "move", { "from": from, "target": target }, function (data) {
                APIResult.ifok(data, callback);
            })
        },
        getType: function (qtype) {
            switch (qtype) {
                case "radio":
                    return "单选";
                case "checkbox":
                    return "多选";
                case "blank":
                    return "填空";
                case "score":
                    return "评分";
                case "sort":
                    return "排序";
                default:
                    return "未知[" + qtype + "]";
            }
        },
        newModel: function () { return { "ID": 0, "AskID": 0, "QTitle": "", "QContent": "", "QOption": [], "QType": "", "QFlag": { type: "", rows: 1, maxscore: 5 }, "Required": true, "OrderID": 0, "CUser": 0 }; },
        newOption: function (text) {
            //用math生成的小数位过长,在转dt时会截位
            var model = { text: "", value: parseInt(Math.random() * 10000), checked: false }
            if (text) { model.text = text; }
            return model;
        }
        //maxscore:最大分值,rows:最大行数,type:子类型
        //QOption:客户端用[],提交时转为字符串  {text:提交的文本,value:提交的值(预留),checked:是否选中|初始是否选中}
    }
})
.factory("Answer", function () {
    var api="/design/ask/server/answer.ashx?action=";
    return {
        submit: function (askid, answer, callback) {
            $.post(api + "submit", { "askid": askid, "answer": answer }, function (data) {
                APIResult.ifok(data, callback);
            })
        },
        list: function (opts, callback) {
            $.post(api + "list", {}, function (data) {
                APIResult.ifok(data, callback);
            })
        }
    }
})
.factory("User", function () {
    var api = "/api/usercheck.ashx?action=";
    return {
        //未登录则跳转
        check: function () {
            $.post(api+"HasLogged", {}, function (data) {
                if (data == "-1") { location = "/User/Login.aspx"; }
            })
        }
    }
})
.factory("DB", function () {
    var DB = {
        data: null,
        temp: null,//一次性数据
        set: function (data) { DB.data = data; },
        get: function () { return DB.data; },
        settemp: function (data) { DB.temp = data; },
        gettemp: function () { var data = DB.temp; DB.temp = ""; return data; }
    };
    return DB;
})
.factory("Loading", function ($ionicLoading) {
    return {
        show: function (text) { $ionicLoading.show({ template: '<ion-spinner icon="android"></ion-spinner>' }); },
        hide: function () { $ionicLoading.hide(); }
    }
})