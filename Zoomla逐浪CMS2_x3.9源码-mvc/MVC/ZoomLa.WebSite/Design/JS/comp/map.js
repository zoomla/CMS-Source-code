define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    //百度地图等,wix使用iframe
    _base.utils.inherits(_self, _base.Control);
    _self.prototype.wrapper = '<div id="@id" ng-model="list.@id.dataMod.text" class="diy_comp onlydrag" style="@style width:@width;height:@height;">{html}</div>';
    _self.prototype.htmlTlp += '<div style="position:absolute;width:100%;height:@height;background-color:rgba(255, 255, 255, 0.40)"></div>';
    _self.prototype.htmlTlp += '<iframe src="/Design/Diag/Map/map.aspx" style="width:100%;height:@height;" class="diy_ifr" scrolling="no"/>';
    //_self.prototype.diagParam = { autoOpen: true, height: 650, width: 1100 };
    _self.prototype.AnalyToHtml = function (config) {
        var html = this._AnalyToHtml(config);
        html = html.replace(/@width/g, this.dataMod.width + "px").replace(/@height/g, this.dataMod.height + "px");
        return html;
    }
    _self.prototype.Render = function () {
        this.instance.html($(this.AnalyToHtml({})).html());//只更新内部
        this.instance.css("width", this.dataMod.width);
        this.instance.css("height", this.dataMod.height);
    }
    module.exports = function () { return _self; }
});