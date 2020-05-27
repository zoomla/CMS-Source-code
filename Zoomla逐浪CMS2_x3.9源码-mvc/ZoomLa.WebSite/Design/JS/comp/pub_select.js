define(function (require, exports, module) {
    //var tlp = '<div id="@id" ng-model="list.@id.dataMod" class="@css" style="@style"><div class="layout"></div></div>';
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    _self.prototype.htmlTlp = '<span ng-bind="list.@id.dataMod.text"></span>';
    _self.prototype.Init_Pre = function (model, extend) {
        var ref = this;
        switch (model.config.compid) {
            case "radio":
                ref.htmlTlp = '';
                break;
            case "checkbox":
                ref.htmlTlp = '';
                break;
            case "select":
                break;
            default:
                alert(model.confi.compid) + "类型不正确";
                break;
        }
        //如果是design模式下,还需要加个遮罩,用于方便用户点击,同于地图组件
        if (model.mode == "design") {
            ref.htmlTlp = '<div class="comp_mask"></div>' + ref.htmlTlp;
        }
    }
    module.exports = function () { return _self; }
});