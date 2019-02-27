define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    _self.prototype.Init_Pre = function (model, extend) {
        var ref = this;
        switch (model.config.type)
        {
            case "pub_button":
                ref.htmlTlp = '<button type="button" ng-bind="list.@id.dataMod.value" class="pub_button" style="' + model.config.btnstyle + '"/>';
                break;
            default:
                ref.htmlTlp += '<button type="button" ng-bind-html="list.@id.dataMod.value|html" style="padding:0;' + model.config.btnstyle + '" class="active ' + model.config.class + '">' + model.config.htm + '</button>';
                break;
        }
        //如果是design模式下,还需要加个遮罩,用于方便用户点击,同于地图组件
        if (model.mode == "design") {
            ref.htmlTlp = '<div class="comp_mask"></div>' + ref.htmlTlp;
        }
    }
    //处理拖动事件,动态调整大小
    _self.prototype.resize = function (ui) {
        var ref = this;
        var $tar = ref.instance.find("button");
        $tar.css("width", ui.width).css("height", ui.height);
    }
    _self.prototype.PreSave = function () {
        this._presave();
        this.config.btnstyle = this.instance.find("button").attr("style");
        return { "dataMod": this.dataMod, "config": this.config };
    }
    module.exports = function () { return _self; }
});