define(function (require, exports, module) {
    var APIResult = {};
    APIResult.Success = 1;
    APIResult.Failed = -1;
    APIResult.getModel = function (str) {
        if (!str || str == "" || str == "[]") { return { "retcode": APIResult.Failed, "retmsg": "", "result": "{}", "addon": "", "action": "" }; }
        var model = JSON.parse(str);
        if (model.result && model.result != "") {
            if (model.result == "True") { model.result = true; }
            else if (model.result == "False") { model.result = false; }
            else if (model.result.indexOf("{") == 0 || model.result.indexOf("[") == 0) { model.result = JSON.parse(model.result); }
        }
        return model;
    }
    //返回的结果是否成功
    APIResult.isok = function (model) {
        if (model && model.retcode == APIResult.Success) { return true; }
    }
    module.exports = APIResult;
});