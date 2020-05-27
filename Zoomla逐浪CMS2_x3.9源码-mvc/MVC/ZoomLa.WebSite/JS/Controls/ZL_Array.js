/*提供对Array的原型扩展*/
//--------------------------------------------------------------
//用于Angular
var ArrCOM = {};
ArrCOM.GetByID = function (arr, id, name) {
    if (!name) { name = "id"; }
    if (!id || id == NaN || id == null || id == undefined) { console.log(id, "值为空"); return null; }
    id = (id + "").replace(/ /ig, "");
    for (var i = 0; i < arr.length; i++) {
        if (arr[i][name] == id) { return arr[i]; }
    }
    return null;
}
ArrCOM.RemoveByID = function (arr, id, name) {
    if (!name) { name = "id"; }
    if (!id || id == NaN || id == null || id == undefined) { console.log(id, "值为空"); return null; }
    id = (id + "").replace(/ /ig, "");
    for (var i = 0; i < arr.length; i++) {
        if (arr[i][name] == id) { arr.splice(i, 1); break; }
    }
}
// var tar = ArrCOM.GetNear($scope.qlist, { "dir": dir, order: item.OrderID, field: "OrderID" });
ArrCOM.GetNear = function (arr, cfg) {
    //var opts = {arr:操作的数组,order:当前order,dir:方向,field:排序字段名};
    var result = null;
    var order = cfg.order;
    var dir = cfg.dir;
    if (arr.length < 2) { return result; }
    switch (dir) {
        case "pre":
            for (var i = 0; i < arr.length; i++) {
                var cur = parseInt(arr[i][cfg.field]);//当前要对比的order
                if (cur < order && (!result || cur > result[cfg.field])) {
                    result = arr[i];
                }
            }
            break;
        case "next":
            for (var i = 0; i < arr.length; i++) {
                var cur = parseInt(arr[i][cfg.field]);//当前要对比的order
                if (cur > order && (!result || cur < result[cfg.field])) {
                    result = arr[i];
                }
            }
            break;
        default:
            console.log("getnear:" + dir + "错误");
            break;
    }
    return result;
}
//将数组合并,如果主键重复则不添加
Array.prototype.addAll = function ($array, name) {
    if ($array == null || $array.length == 0) { return; }
    for (var $i = 0; $i < $array.length; $i++) {
        this.pushNoDup($array[$i], name);
    }
}
/*
*添加前进行重复检测,如指定的主键项不存在,也不允许添加
*示例:list.pushNoDup({ p_id: 1, p_title: "test" }, "p_id");
*/
Array.prototype.pushNoDup = function (model, name) {
    if (!name) { name = "id"; }
    var isAdd = true;
    for (var i = 0; i < this.length; i++) {
        var me = this[i];
        if (me[name] == model[name]) { isAdd = false; break; }
    }
    if (isAdd) { this.push(model); }
}
//向开头添加一个元素,如重复则不添加
Array.prototype.unshiftNoDup = function (model, name) {
    if (!name) { name = "id"; }
    var isAdd = true;
    for (var i = 0; i < this.length; i++) {
        var me = this[i];
        if (me[name] == model[name]) { isAdd = false; break; }
    }
    if (isAdd) { this.unshift(model); }
}
//是否包含指定值
Array.prototype.contains = function ($value) {
    for (var $i = 0; $i < this.length; $i++) {
        var $element = this[$i];
        if ($element == $value)
            return true;
    }
    return false;
}
Array.prototype.GetByID = function (id, name) {
    return ArrCOM.GetByID(this, id, name);
}
//返回数组中指定字段值最大的一个元素(用于order排序等)
Array.prototype.GetByMax = function (name) {
    if (this.length < 1) { return null; }
    var model = this[0];
    for (var i = 1; i < this.length; i++) {
        var $element = this[i];
        if ($element[name] > model[name]) { model = $element; }
    }
    return model;
}
//根据ID,更新数组中的指定元素
Array.prototype.UpdateByID = function (model, name) {
    if (!name) { name = "id"; }
    var id = model[name];
    if (!id || id == NaN || id == null || id == undefined) { console.log(id, "值为空"); return null; }
    id = (id + "").replace(/ /ig, "");
    for (var i = 0; i < this.length; i++) {
        if (this[i][name] == id) {
            this[i] = model;
            return true;
        }
    }
    return false;
}
//不允许空格,不允许空值
Array.prototype.RemoveByID = function (id, name) {
    return ArrCOM.RemoveByID(this, id, name);
}
//去除重复值
Array.prototype.unique = function () {
    var data = this || [];
    var a = {}; //声明一个对象，javascript的对象可以当哈希表用
    for (var i = 0; i < data.length; i++) {
        a[data[i]] = true;  //设置标记，把数组的值当下标，这样就可以去掉重复的值
    }
    data.length = 0;

    for (var i in a) { //遍历对象，把已标记的还原成数组
        this[data.length] = i;
    }
    return data;
}
Array.prototype.GetIDS = function (name) {
    //返回数组的ids,默认返回id
    var ids = ""; if (!name) { name = "id";}
    for (var i = 0; i < this.length; i++) {
        ids += this[i][name] + ",";
    }
    if (ids && ids.length > 0) { ids = ids.substring(0, ids.length - 1); }
    return ids;
}
//深度拷贝数组
Array.prototype.Clone = function () {
    function getType(o) {
        var _t;
        return ((_t = typeof (o)) == "object" ? o == null && "null" || Object.prototype.toString.call(o).slice(8, -1) : _t).toLowerCase();
    }
    function extend(destination, source) {
        for (var p in source) {
            if (getType(source[p]) == "array" || getType(source[p]) == "object") {
                destination[p] = getType(source[p]) == "array" ? [] : {};
                arguments.callee(destination[p], source[p]);
            }
            else {
                destination[p] = source[p];
            }
        }
    }
    var destination = [];
    var source = this;
    extend(destination, source);
    return destination;
}
//item,index,array
//低版本IE无ForEach
//if (!Array.prototype.forEach) {
//    Array.prototype.forEach = function (callback, thisArg) {
//        var T, k;
//        if (this == null) {
//            throw new TypeError(" this is null or not defined");
//        }
//        var O = Object(this);
//        var len = O.length >>> 0; // Hack to convert O.length to a UInt32  
//        if ({}.toString.call(callback) != "[object Function]") {
//            throw new TypeError(callback + " is not a function");
//        }
//        if (thisArg) {
//            T = thisArg;
//        }
//        k = 0;
//        while (k < len) {
//            var kValue;
//            if (k in O) {
//                kValue = O[k];
//                callback.call(T, kValue, k, O);
//            }
//            k++;
//        }
//    };
//}
//--------------------------------------------------------------
var JsonHelper = {
    //兼容之前,返回html
    FillData: function (stlp, list) {
        var ref = this;
        var html = ref.FillItem(stlp, list, null).toHTML();
        return html;
    },
    //为了兼容做此处理,返回对象模型
    FillItem: function (stlp, list, itemBound) {
        //用于单传一个json模型,
        //iframe之间传值,会导致其判断Array为false
        //if (!(list instanceof Array)) { var arr = []; arr.push(list); list = arr; }
        function isArray(obj) { return Object.prototype.toString.call(obj) === '[object Array]'; }
        if (!isArray(list)) { var arr = []; arr.push(list); list = arr; }
        var $result = $("<div>");
        for (var i = 0; i < list.length; i++) {
            var model = list[i];
            var item = function (mod) {
                var tlp = stlp;
                var keyArr = [];
                for (var key in mod) { keyArr.push(key); }
                //将key字符长度最大的放前面
                keyArr.sort(function (a, b) { return a.length > b.length ? -1 : 1; });
                for (var j = 0; j < keyArr.length; j++) {
                    tlp = tlp.Replace("@" + keyArr[j], mod[keyArr[j]]);
                }
                tlp = tlp.Replace("@_index", (i+1));
                tlp = tlp.Replace("@_model", JSON.stringify(model));//将整个模型作为参数传入
                var $item = $(tlp);
                //需要以JS解析的
                var $fun = $item.find("fun");
                $fun.each(function () {
                    var html = $(this).html();
                    $(this).html(eval(html));
                })
                //绑定事件,或对其中的元素作进一步判断处理
                if (itemBound) {itemBound($item, mod);}
                return $item;
            }(model);
            $result.append(item);
        }//for end;
        return $result.children();
    }
};
jQuery.fn.extend({
    toHTML: function () {
        var obj = this;
        var html = "";
        for (var i = 0; i < obj.length; i++) {
            html += obj[i].outerHTML;
        }
        return html;
    }
});
String.prototype.Replace = function (str1, str2) {
    var rs = this.replace(new RegExp(str1, "gm"), str2);
    return rs;
}