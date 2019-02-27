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
                model.dataMod.seled = model.dataMod.list[0].value;
                ref.htmlTlp = '<select ng-model="list.@id.dataMod.seled" data-id="@id" class="pub pub_select" style="width:100%;height:80px;font-size:35px;">';
                ref.htmlTlp += '<option ng-repeat="item in list.@id.dataMod.list" value="{{item.value}}" ng-bind="item.text"></option>';
                ref.htmlTlp += "</select>";
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
    _self.prototype.resize = function (ui) {
        var ref = this;
        if (ref.config.compid != "select") { return; }
        var $tar = ref.instance.find(".pub_select");
        $tar.height(ui.height).css("font-size", ui.height / 2);
    }
    module.exports = function () { return _self; }
});