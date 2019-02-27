<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="nav.aspx.cs" Inherits="ZoomLaCMS.Design.mobile.pop.nav" MasterPageFile="~/Design/Master/MB_POP.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>轮播图</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="page-group" ng-app="app">
    <div id="page_list" class="page page-current" ng-controller="APPCtrl">
        <header class="bar bar-nav">
            <a class="button button-link button-nav pull-left back" href="javascript:;" ng-click="save();">
                <span class="icon icon-left"></span>
                返回
            </a>
             <a href="#page_content" class="button button-fill button-nav pull-right" ng-click="new()">添加数据</a>
            <h1 class="title">数据管理</h1>
        </header>
        <div class="content native-scroll">
        <div class="list-block cards-list">
          <ul>
            <li class="card list_item" ng-repeat="item in list track by $index">
              <div class="card-header">
                  <input type="text" placeholder="标题" ng-model="item.title" />
                  <a href="javascript:;" ng-click="del(item.id);" class="button button-fill button-danger"><i class="fa fa-trash-o"></i> 删除</a>
              </div>
              <div class="card-content">
                <div class="card-content-inner" ng-click="changePic(item);">
                     <img ng-src="{{item.wxico}}" style="max-width:100%;" id="img_{{item.id}}"/>
                </div>
              </div>
         <%--     <div class="card-footer">卡脚</div>--%>
            </li>
          </ul>
        </div>
      </div>
    </div>
    <div id="page_content" class="page" ng-controller="ConCtrl">
         <header class="bar bar-nav"><h1 class="title">内容管理</h1></header>
         <div class="content">
            <div class="list-block">
                <ul>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">标题</div>
                                <div class="item-input">
                                    <input type="text" placeholder="标题" ng-model="model.title" />
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">图片</div>
                                <div class="item-input" ng-click="uppic();">
                                    <img ng-src="{{getpic()}}" style="max-width:100%;"/>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">数据类型</div>
                                <div class="item-input">
                                    <select ng-model="model.dbtype">
                                        <option value="footbar">底部栏</option>
                                        <option value="image">单图</option>
                                        <option value="list" selected="selected">列表</option>
                                        <option value="nav">轮播图</option>
                                    </select>
                                </div>
                            </div>
                        </div>
                    </li>
          <%--          <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">是否标识</div>
                                <div class="item-input">
                                    <label class="label-switch">
                                        <input type="checkbox" />
                                        <div class="checkbox"></div>
                                    </label>
                                </div>
                            </div>
                        </div>
                    </li>--%>
                </ul>
            </div>
            <div class="content-block">
                <div class="row">
                    <div class="col-50"><a href="javascript:;"   ng-click="cancel();" class="button button-big button-fill button-danger back">取消</a></div>
                    <div class="col-50"><a href="javascript:;" ng-click="save();" class="button button-big button-fill button-success back">提交</a></div>
                </div>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/Mobile/ResizeImg/lrz.js"></script>
<script>  
var page = { scope: null, backup: "" };//内容scope
picup.up_before = function () { Zepto.showPreloader(); }
picup.zip.enable = true;
    angular.module("app", [])
.controller("APPCtrl", function ($scope) {
    cfg.firstScope = $scope;
    $scope.list = getlist(cfg.id);
    $scope.changePic = function (item) {
        picup.up_after = function (data) {
            item.wxico = data;
            $scope.$digest();
            Zepto.hidePreloader();
        }
        picup.sel();
    }
    $scope.del = function (id) {
        if (!confirm("确定要删除吗")) { return false; }
        for (var i = 0; i < $scope.list.length; i++) {
            top.tools.updatedata("mb_del", { "id": id }, function (data) { console.log(data); });
            if ($scope.list[i].id == id) {
                $scope.list.splice(i, 1);
                break;
            }
        }
    }
    //重绘nav与提交数据更新后台,title,wxico
    $scope.save = function () {
        if ($scope.list.length > 0) {
            var list = angular.toJson($scope.list);
            $.post("/design/update.ashx?action=mb_nav", { "list": list }, function (data) { })
        }
        parent.tools.scope.$digest();
        parent.tools.nav.swiper.destroy();//兼容处理,更新循环
        parent.tools.nav.init();
        parent.tools.pop.close();
    }
    $scope.new = function () {
        page.backup = "";
        page.scope.model = { id: "", title: "新导航", wxico: "", wxbk: "#000", wxsize: "1", flag: "", dbtype: "nav" };
    }
})
.controller("ConCtrl", function ($scope) {
    page.scope = $scope;
    $scope.model = cfg.newmodel();
    $scope.cancel = function () {
        if (page.backup != "") {
            var item = JSON.parse(page.backup);
            $scope.model.title = item.title;
            $scope.model.wxico = item.wxico;
            $scope.model.wxbk = item.wxbk;
            $scope.model.wxsize = item.wxsize;
            $scope.model.flag = item.flag;
            $scope.model.dbtype = item.dbtype;
            page.backup = "";
        }
        Zepto.router.back("#page_list");
    }
    $scope.save = function () {
        var list = top.tools.clearlist(angular.toJson($scope.model));
        if ($scope.model.id == "") {
            //提交至后台,并将其加入数组
            Zepto.showPreloader();
            top.tools.updatedata("mb_new", { "list": list, type: "nav" }, function (data) {
                $scope.model.id = data;
                cfg.firstScope.list.push($scope.model);
                cfg.firstScope.$digest();
                Zepto.hidePreloader();
                Zepto.router.back("#page_list");
            })
        }
        else {
            //非新数据则提交更新
            top.tools.updatedata("mb_list", { "list": list }, function (data) { })
            Zepto.router.back("#page_list");
        }
    }
    $scope.uppic = function () {
        picup.sel();
        picup.up_after = function (data) {
            $scope.model.wxico = data;
            $scope.$digest();
            Zepto.hidePreloader();
        }
    }
    $scope.getpic = function () {
        if ($scope.model.wxico == "") { return "/Plugins/Ueditor/dialogs/image/images/image.png"; }
        else { return $scope.model.wxico; }
    }
})
</script>
</asp:Content>
