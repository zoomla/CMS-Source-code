define(function (require, exports, module) {
    //BootStrap幻灯片
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    //_self.prototype.htmlTlp = '<div id="@id" ng-model="list.@id.dataMod.text" class="{{list.@id.config.css}}" style="{{list.@id.config.style}}">{{list.@id.dataMod.text}}</div>';
    _self.prototype.htmlTlp = ''
   + '<div id="@id" class="carousel slide" data-ride="carousel" data-interval="5000"><ol class="carousel-indicators">'
   + '<li ng-repeat="item in list.@id.dataMod.list| orderBy: \'orderid\'" data-target="#@id" data-slide-to="{{$index}}" ng-class=\"{active:$index==0}\"></li>'
   + '</ol>'
   + '<div class="carousel-inner">'
   + '<div ng-repeat="item in list.@id.dataMod.list| orderBy: \'orderid\'" class="item container" style="padding:0px;" ng-class=\"{active:$index==0}\">'
   + '<img src="{{item.url}}">'
   //+ '<div class="carousel-caption">{{item.name}}</div>'
   + '</div>'//item end;
   + '</div>'
   + '<a class="carousel-control left" href="#@id" data-slide="prev"><span class="fa fa-chevron-left" style="font-size:3em;"></span></a>'
   + '<a class="carousel-control right" href="#@id" data-slide="next"><span class="fa fa-chevron-right" style="font-size:3em;"></span></a></div>';
    _self.prototype.Init_Pre = function (model, extend) {

    }
    module.exports = function () { return _self; }
});