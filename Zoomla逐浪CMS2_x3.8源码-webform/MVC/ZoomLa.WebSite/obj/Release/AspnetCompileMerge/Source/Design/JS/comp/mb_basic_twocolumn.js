define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    var html = "<div class=\"mb_basic_twocolumn\">"
         + "<a ng-repeat=\"item in list.@id.dataMod.items\" class=\"list_item\" ng-style=\"{{item.bkcolor}}\">"
         + "<i class=\"{{item.icon}} list_item_icon\"></i>"
         + "<span style=\"vertical-align:middle;display:inline-block;\" ng-bind-html=\"item.desc|html\"></span>"
         + "</a>"
         + "</div>";
    _self.prototype.htmlTlp = html;
    module.exports = function () { return _self; }
});