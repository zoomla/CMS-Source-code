define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    var APIResult = require("APIResult");
    var dc = require("dc");
    _base.utils.inherits(_self, _base.Control);

    var html = { s1: "", s2: "" };
    html.s1 = "<div class=\"list_s1\">"
  + "<div class=\"list_item\" ng-repeat=\"item in list.@id.dataMod.list\">"
  + "<a href=\"/Content?Gid={{item.GeneralID}}\" target=\"_blank\" class=\"pic_a\">"
  + "<img src=\"{{item.TopImg}}\" onerror=\"this.src='/Images/nopic.gif';\"; />"
  + "</a>"
  + "<div class=\"list_item_head\">"
  + "<div class=\"head_title\"><a href='/Content?gID={{item.GeneralID}}'>{{item.Title}}</a></div>"
  + "<div class=\"head_date\" ng-bind=\"item.CreateTime| date:'yyyy-MM-dd'\"></div>"
  + "<div class=\"clearfix\"></div>"
  + "</div>"
  + "<div class=\"desc_div\" ng-bind=\"item.synopsis\"></div>"
  + "</div>"
  + "</div>";
    html.s2 = "<div class=\"list_s2\">"
            + "<h2 class=\"title\">信息列表</h2>"
            + "<div class=\"list_item\" ng-repeat=\"item in list.@id.dataMod.list\">"
            + "<div class=\"item_img\"  ng-class=\"{ani_imgstyle: hover}\" ng-mouseenter=\"hover = true\" ng-mouseleave=\"hover = false\" >"
            + "<a href=\"/Content?Gid={{item.GeneralID}}\" target=\"_blank\"><img src=\"{{item.TopImg}}\" onerror=\"this.src='/Images/nopic.gif';\"; /></a>"
            + "</div>"
            + "<div class=\"item_info\">"
            + "<div class=\"info_title\">{{item.Title}}</div>"
            + "<span class=\"time_share\">"
            + "<span class=\"time\" ng-bind=\"item.CreateTime| date:'yyyy-MM-dd'\"></span></span>"
            + "<div class=\"item_content\" ng-bind=\"item.synopsis\"></div>"
            + "<a href=\"Content?Gid={{item.GeneralID}}\" class=\"detail_a\">查看详情 <i class=\"fa fa-chevron-right\"></i></a>"
            + "</div></div></div>";
    html.s3 = "";
    //_self.prototype.htmlTlp = html;
    _self.prototype.Init = function (model, extend) {
        var ref = this;
        ref.htmlTlp = html[model.config.compid];
        dc.content.selbynid(function (model) {
            ref.dataMod.list = model.result;
            eventBase.fire("editor_update", ref.dataMod.id);
        }, "", 1, 3);
        ref._init(model, extend);
    }
    _self.prototype.SetInstance_After = function () {
    }
    module.exports = function () { return _self; }
});