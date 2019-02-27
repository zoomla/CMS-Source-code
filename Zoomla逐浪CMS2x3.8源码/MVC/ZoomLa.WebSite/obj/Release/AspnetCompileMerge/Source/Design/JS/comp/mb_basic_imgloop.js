define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    //_self.prototype.htmlTlp = '<div id="@id" ng-model="list.@id.dataMod.text" class="{{list.@id.config.css}}" style="{{list.@id.config.style}}">{{list.@id.dataMod.text}}</div>';
    _self.prototype.htmlTlp = ''
   + '<div id="@id_gallery" class="carousel slide" data-ride="carousel" data-interval="5000" style="height:120px;"><ol class="carousel-indicators">'
   + '<li ng-repeat="item in list.@id.dataMod.items" data-target="#@id_gallery" data-slide-to="{{$index}}" class="{{item.css}}"></li>'
   + '</ol>'
   + '<div class="carousel-inner">'
   + '<div ng-repeat="item in list.@id.dataMod.items" class="item {{item.css}}">'
   + '<img src="{{item.url}}">'
   + '<div class="carousel-caption">{{item.name}}</div>'
   + '</div>'//item end;
   + '</div></div>';
    module.exports = function () { return _self; }
});