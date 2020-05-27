define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    var APIResult = require("APIResult");
    var Common = require("Common");
    var dc = require("dc");
    _base.utils.inherits(_self, _base.Control);
    var html = "<div class=\"content_s1\">"
 + "<div class=\"con_title\" ng-bind=\"list.@id.temp.Title\"></div>"
 + "<div class=\"con_date\">"
 + "发布时间：{{list.@id.temp.CreateTime|date:'yyyy-MM-dd'}}"
 + "</div>"
 + "<div class=\"con_content\" ng-bind-html=\"list.@id.temp.content|html\"></div>"
 + "</div>";
    _self.prototype.htmlTlp = html;
    _self.prototype.Init = function (model, extend) {
        var ref = this;
        //仅用于临时存
        dc.content.selbyid(function (model) { ref.temp = model.result[0]; }, Common.GetParam("gid"));
        ref._init(model, extend);
    }
    module.exports = function () { return _self; }
});