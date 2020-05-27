define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    var dc = require("dc");
    _base.utils.inherits(_self, _base.Control);
    //_self.prototype.wrapper = '<div ng-model="list.@id" id="@id" class="onlydrag" style="@style">{html}</div>';
    _self.prototype.htmlTlp += '<img class="imgcomp" id="@id" style="@imgstyle" ng-src="{{list.@id.dataMod.src}}"/>';
    _self.prototype.Init = function (model, extend) {
        var ref = this;
        switch (model.config.compid) {
            case "logo":
                dc.site.sel(function (mod) { ref.dataMod.src = mod.result.Logo; eventBase.fire("editor_update"); });
                break;
            default:
                break;
        }
        ref._init(model, extend);
    }
    //处理拖动事件,动态调整大小
    _self.prototype.resize = function (ui) {
        var ref = this;
        var $tar = ref.instance.find(".imgcomp");
        $tar.width(ui.width).height(ui.height);
    }
    _self.prototype.PreSave = function () {
        this._presave();
        this.config.imgstyle = this.instance.find(".imgcomp").attr("style");
        return { "dataMod": this.dataMod, "config": this.config };
    }
    module.exports = function () { return _self; }
});