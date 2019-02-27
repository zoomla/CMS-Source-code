define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    var html = "<div class=\"mb_basic_cube\">"
            + "<div ng-repeat=\"item in list.@id.dataMod.items\" class=\"list_item\">"
            + "<a href=\"#\" ng-style=\"{{item.bkcolor}};\" class=\"list_item_a\">"
            + "<i class=\"{{item.icon}}\" style=\"font-size: 1.5em;\"></i>"
            + "</a>"
            + "<div style=\"color: #000; \">{{item.name}}</div>"
            + "</div>"
            + "</div>";
    _self.prototype.htmlTlp = html;
    module.exports = function () { return _self; }
});