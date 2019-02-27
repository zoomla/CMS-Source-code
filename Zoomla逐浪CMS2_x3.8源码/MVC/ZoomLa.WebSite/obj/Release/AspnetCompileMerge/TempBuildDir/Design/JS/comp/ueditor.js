define(function (require, exports, module) {
    //var tlp = '<div id="@id" ng-model="list.@id.dataMod" class="@css" style="@style"><div class="layout"></div></div>';
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    _self.prototype.Init = function (model, extend) {
        var ref = this;
        ref.htmlTlp = model.dataMod.html;
        ref._init(model, extend);
    }
    _self.prototype.Init_After = function () {
        var ref = this;
        eventBase.add(ref.id + "_render", function (params) {
            $(ref.instance).html(ref.dataMod.html);
        });
    }
    _self.prototype.diagParam = { autoOpen: true, height: 650, width: 1100 };
    module.exports = function () { return _self; }
});