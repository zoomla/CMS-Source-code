define(function (require, exports, module) {
    var $ = require("jquery");
    var JsonHelper = {
        FillItem: function (stlp, list, itemBound) {
            //用于单传一个json模型(不准确)
            //if (!(list instanceof Array)) { var arr = []; arr.push(list); list = arr; }
            var $result = $("<div>");
            for (var i = 0; i < list.length; i++) {
                var model = list[i];
                var item = function (mod) {
                    var tlp = stlp;
                    var keyArr = [];
                    for (var key in mod) {
                        keyArr.push(key);
                    }
                    //将key字符长度最大的放前面
                    keyArr.sort(function (a, b) { return a.length > b.length ? -1 : 1; });
                    for (var i = 0; i < keyArr.length; i++) {
                        var key = keyArr[i];
                        tlp = Replace(tlp,"@" + key, mod[key]);
                    }
                    tlp = Replace(tlp,"@_model", JSON.stringify(model));//将整个模型作为参数传入
                    var $item = $(tlp);
                    //需要以JS解析的
                    var $fun = $item.find("fun");
                    $fun.each(function () {
                        var html = $(this).html();
                        $(this).html(eval(html));
                    })
                    if (itemBound) {
                        itemBound($item, mod);
                    }
                    return $item;
                }(model);
                $result.append(item);
            }//for end;
            return $result.children();
        }//Fill Item End;
    };
    module.exports = JsonHelper;
    function Replace(str,str1, str2)
    {
        var rs = str.replace(new RegExp(str1, "gm"), str2);
        return rs;
    }
});