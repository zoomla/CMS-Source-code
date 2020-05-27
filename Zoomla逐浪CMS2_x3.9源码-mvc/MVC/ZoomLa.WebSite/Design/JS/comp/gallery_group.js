define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    var html = "<div class=\"gallery_group\">"
      + "<div ng-repeat=\"item in list.@id.dataMod.list | orderBy: 'orderid'\" ng-style=\"{{item.layout}}\" class=\"list_item\" ng-mouseenter=\"item.show = true\" ng-mouseleave=\"item.show = false\">"
      + "<div class=\"list_item_div\"><div class=\"list_item_text\" ng-show=\"item.show\">{{item.name}}</div>"
      + "<img ng-src=\"{{item.url}}\" class=\"list_item_img\" />"
      + "</div>"
      //+ "<div class=\"text-center\">{{item.name}}</div>"
      + "</div>"
      + "</div>";
    //根据布局,为图片元素增加style
    function layoutList(layout, list) {
        for (var i = 0; i < list.length; i++) {
            var index = i % layout.length;//用于循环输出图片
            list[i].layout = { "width": (layout[index] * 25) + "%" };
        }
        return list;
    }
    _self.prototype.htmlTlp = html;
    _self.prototype.Init = function (model, extend) {
        var ref = this;
        //每行四张图,可自由选择布局方式(1=2)
        var layout = [1, 1, 1, 1, 2, 1, 1];
        model.dataMod.list = layoutList(layout, model.dataMod.list);
        ref._init(model, extend);
    }
    _self.prototype.SetInstance_After = function () {
        var ref = this;
        eventBase.add(ref.id + "_render", function (params) {
            //每行四张图,可自由选择布局方式(1=2)
            var layout = [1, 1, 1, 1, 2, 1, 1];
            ref.dataMod.list = layoutList(layout, ref.dataMod.list);
            eventBase.fire("editor_update");
        });
        ref.editurl = "/Design/Diag/Gallery/Edit.aspx?id=" + ref.id;
        //setTimeout(function () {   $(ref.instance).find(".list_item").hover(function () { console.log("move in"); }, function () { console.log("move out"); });},1000);
    }
    module.exports = function () { return _self; }
});