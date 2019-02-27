define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    var html = {};
    html.div = "<div></div>";
    html.footer_s1 ="<div class=\"footer_s1\">"
                    +"<span class=\"footer_sp share\">"
                    +"<a href=\"{{item.href}}\" target=\"{{item.target}}\" ng-repeat=\"item in list.@id.dataMod.blogs\"><i class=\"{{item.fa}}\"></i></a>"
                    +"</span>"
                    +"<span class=\"footer_sp bar_link\">"
                    +"<a href=\"{{item.href}}\" target=\"{{item.target}}\" ng-repeat=\"item in list.@id.dataMod.links\">{{item.name}}</a>"
                    +"</span>"
                    +"<span class=\"footer_sp copy\">"
                    +"<em>©中华人民共和国网警备案号:3601040103 经营许可证号:工商3601002021063 沪ICP备09077823号</em>"
                    +"<em>本网站基于®Zoomla!逐浪CMS内核开发</em>"
                    +"</span>"
                    +"</div>"
    _self.prototype.Init_Pre = function (model, extend) {
        var ref = this;
        ref.htmlTlp = html[model.config.compid];
    }
    module.exports = function () { return _self; }
});