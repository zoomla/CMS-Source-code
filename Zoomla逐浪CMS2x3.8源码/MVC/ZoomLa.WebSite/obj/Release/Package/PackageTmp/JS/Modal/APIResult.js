var APIResult = {};
APIResult.Success = 1;
APIResult.Failed = -1;
APIResult.getModel = function (str) {
    var failedMod = { "retcode": APIResult.Failed, "retmsg": "", "result": "{}", "addon": "", "action": "" };
    if (!str || str == "" || str == "[]") { return failedMod; }
    try {
        var model = null;
        if (typeof str == "string") { model = JSON.parse(str); }
        else { model = str; }
        if (model.result && model.result != "") {
            if (model.result == "True") { model.result = true; }
            else if (model.result == "False") { model.result = false; }
            else if (model.result.indexOf("{") == 0 || model.result.indexOf("[") == 0) { model.result = JSON.parse(model.result); }
        }
        return model;
    } catch (ex) { failedMod.retmsg = "Parse Error:" + str; return failedMod; }
}
//返回的结果是否成功
APIResult.isok = function (model) {
    if (model && model.retcode == APIResult.Success) { return true; }
}
//返回数据,成功回调,失败回调
APIResult.ifok = function (data, ok,err) {
    var model = APIResult.getModel(data);
    if (APIResult.isok(model)) { if (ok) { ok(model.result); } }
    else {
        if (err) { err(data); }
        console.log(model.action + " failed:", model.retmsg, data);
    }
}