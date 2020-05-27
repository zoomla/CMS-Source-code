define(function (require, exports, module) {
    var ZLArray = {};
    ZLArray.MyArr = [];
    ZLArray.GetByID = function (id, name) {
        if (!name || name == "") { name = "id"; }
        for (var i = 0; i < ZLArray.MyArr.length; i++) {
            if (ZLArray.MyArr[i][name] == id) {return ZLArray.MyArr[i]; }
        }
        return null;
    }
    ZLArray.RemoveByID = function (id) {
        for (var i = 0; i < ZLArray.MyArr.length; i++) {
            if (ZLArray.MyArr[i].id == id) { ZLArray.MyArr.splice(i, 1); break; }
        }
    }
    ZLArray.pushNoDup = function (model, name) {
        if (!name) { name = "id"; }
        var isAdd = true;
        for (var i = 0; i < ZLArray.MyArr.length; i++) {
            var me = ZLArray.MyArr[i];
            if (me[name] == model[name]) { isAdd = false; break; }
        }
        if (isAdd) { ZLArray.MyArr.push(model); }
    }
    ZLArray.GetIDS = function (name) {
        //返回数组的ids,默认返回id
        var ids = ""; if (!name) { name = "id"; }
        for (var i = 0; i < ZLArray.MyArr.length; i++) {
            ids += ZLArray.MyArr[i][name] + ",";
        }
        if (ids && ids.length > 0) { ids = ids.substring(0, ids.length - 1); }
        return ids;
    }
    module.exports = ZLArray;
});