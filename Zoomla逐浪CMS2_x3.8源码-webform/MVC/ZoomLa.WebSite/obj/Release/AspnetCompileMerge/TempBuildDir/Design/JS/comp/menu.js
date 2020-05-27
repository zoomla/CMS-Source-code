define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    var dc = require("dc");
    _base.utils.inherits(_self, _base.Control);
     //{ name: "菜单1", href: "javascript:;", css: "active", orderid: 1 }
    _self.prototype.Init_Pre = function (model, extend) {
        var menu_wrap = "", menu_item = ""; model.dataMod.items = [];
        switch (model.config.compid) {
            case "h1":
                menu_wrap = '<nav class="navbar navbar-default"><div class="container-fluid">'
                       + '<div class="navbar-header"></div>'
                       + '<ul class="nav navbar-nav" ng-model="list.@id.dataMod">{html}</ul></div></nav>';
                menu_item = '<li ng-repeat="item in list.@id.dataMod.items|orderBy:\'orderid\'" class="{{item.css}}"><a href="{{item.href}}">{{item.name}}</a></li>';
                break;
            case "h2":
                menu_wrap = '<ul class="nav nav-pills" ng-model="list.@id.dataMod">{html}</ul>';
                menu_item = '<li ng-repeat="item in list.@id.dataMod.items|orderBy:\'orderid\'" class="{{item.css}}"><a href="{{item.href}}">{{item.name}}</a></li>';
                break;
            case "h3":
                menu_wrap = '<div class="btn-group" role="group" aria-label="..." ng-model="list.@id.dataMod">{html}</div>';
                menu_item = '<a href="{{item.href}}" ng-repeat="item in list.@id.dataMod.items|orderBy:\'orderid\'" class="btn btn-primary {{item.css}}" >{{item.name}}</a>';
                break;
            case "v1":
                menu_wrap = '<div class="list-group" ng-model="list.@id.dataMod">{html}</div>';
                menu_item = '<a href="{{item.href}}" ng-repeat="item in list.@id.dataMod.items|orderBy:\'orderid\'" class="list-group-item list-group-item-success {{item.css}}">{{item.name}}</a>';
                break;
            case "v2":
                menu_wrap = '<div class="list-group" ng-model="list.@id.dataMod">{html}</div>';
                menu_item = '<a href="{{item.href}}" ng-repeat="item in list.@id.dataMod.items|orderBy:\'orderid\'" class="list-group-item list-group-item-info {{item.css}}">{{item.name}}</a>';
                break;
        }
        this.htmlTlp = menu_wrap.replace("{html}", menu_item);
    }
    _self.prototype.Init_After = function () {
        var ref = this;
        dc.node.sel(function (mod) {
            var items = [];
            for (var i = 0; i < mod.result.length; i++) {
                var item = mod.result[i];
                items.push({ href: "/List?NodeID=" + item.NodeID, "name": item.NodeName, "css": "" });
            }
            ref.dataMod.items = items;
            eventBase.fire("editor_update", ref.dataMod.id);
        });
    }
    module.exports = function () { return _self; }
});