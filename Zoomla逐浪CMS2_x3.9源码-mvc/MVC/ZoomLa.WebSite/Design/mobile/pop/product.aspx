<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="product.aspx.cs" Inherits="ZoomLaCMS.Design.mobile.pop.product" MasterPageFile="~/Design/Master/MB_POP.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>商品修改</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="page-group" ng-app="app">
    <div id="page_list" class="page page-current" ng-controller="APPCtrl">
        <header class="bar bar-nav">
            <a class="button button-link button-nav pull-left back" href="javascript:;" ng-click="save();">
                <span class="icon icon-left"></span>
                返回
            </a>
             <a href="#page_content" class="button button-fill button-nav pull-right" ng-click="new()">添加数据</a>
            <h1 class="title">商品管理</h1>
        </header>
        <div class="content native-scroll">
        <div class="list-block cards-list">
          <ul>
            <li class="card list_item" ng-repeat="item in list track by $index">
              <div class="card-header">
                  <span ng-bind="item.proname"></span>
                  <a href="javascript:;" ng-click="del(item.id);" class="button button-fill button-danger"><i class="fa fa-trash-o"></i> 删除</a>
              </div>
              <div class="card-content">
                <div class="card-content-inner">
                    <div class="img_wrap">
                    <span ng-bind-html="getimg(item.pics)|html"></span>
                </div>
                    <div class="con_wrap">
                        <div><span class="name">价格：</span><span class="val" ng-bind="item.price|number:2"></span></div>
                    </div>
                    <div class="op_wrap" ng-click="edit(item);">
                    <i class="fa fa-pencil fa-2x"></i>
                    <div>
                        <span>编辑</span>
                    </div>
                </div>
                </div>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
    <div id="page_content" class="page" ng-controller="ConCtrl">
         <header class="bar bar-nav"><h1 class="title">商品详情</h1></header>
         <div class="content">
            <div class="list-block" style="margin:0;">
                <ul>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">品名</div>
                                <div class="item-input">
                                    <input type="text" placeholder="品名" ng-model="model.proname" />
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">价格</div>
                                <div class="item-input">
                                    <input type="text" ng-model="model.price" />
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                        <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">图片</div>
                                <div class="item-input" ng-click="uppic();">
                                    <img ng-src="{{getpic()}}" style="max-width: 100%;" />
                                </div>
                            </div>
                        </div>
                    </li>
                    <li>
                         <div class="item-content">
                            <div class="item-inner">
                                <div class="item-title label">描述</div>
                                <div class="item-input">
                                    <textarea style="width:100%;height:100px;" ng-model="model.proinfo"></textarea>
                                </div>
                            </div>
                        </div>
                    </li>
                    <li onclick="openEditor();" style="display:none;">
                         <div class="item-content">
                            <div class="item-inner">
                                 <div class="item-input" id="content_div"></div>
                            </div>
                        </div>
                    </li>
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
<%--<div id="pop_div" class="popup popup-edit">
    <div class="content">
         <iframe id="pop_ifr" class="popifr" style="width:100%;height:100%;border:none;" src="editor.aspx"></iframe>
    </div>
</div>--%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.list_item .img_wrap {width:80px;padding:10px;text-align:center;display:table-cell;vertical-align:middle;}
.list_item .img_wrap i {font-size:50px;color:#ccc;}
.list_item .img_wrap img {max-width:50px;}
.list_item .con_wrap {display:table-cell;width:65%;}
.list_item .con_wrap > div {height:25px;}
.list_item .con_wrap .name {min-width:80px;text-align:right;display:inline-block;color:#ccc;}
.list_item .con_wrap .bkval {border:1px solid #ddd;width:60px;height:20px;display:inline-block;}
.list_item .op_wrap {text-align:center;display:table-cell;width:80px;vertical-align:middle;color:#999;}
#content_div {height:120px;overflow-y:auto;}
#content_div img {max-width:100%;}
</style>
<script src="/JS/Mobile/ResizeImg/lrz.js"></script>
<script>
picup.up_before = function () { Zepto.showPreloader(); }
picup.zip.enable = true;
var page = { scope: null, backup: "" };//内容scope
angular.module("app", [])
.controller("APPCtrl", function ($scope) {
    cfg.firstScope = $scope;
    $scope.list = getlist(cfg.id);
    $scope.getimg = function (url) {
        if (url.indexOf("fa ") > -1) { return '<i class="' + url + '" class="media-object"></i>'; }
        else { return '<img src="' + url + '" class="media-object" />'; }
    }
    $scope.del = function (id) {
        if (!confirm("确定要删除吗")) { return false; }
        top.tools.updatedata("mb_pro_del", { "id": id }, function (data) { console.log(data); });
        for (var i = 0; i < $scope.list.length; i++) {
            if ($scope.list[i].id == id) {
                $scope.list.splice(i, 1);
                break;
            }
        }
    }
    //保存列表
    $scope.save = function () {
        cfg.close();
    }
    //选定编辑某条内容
    $scope.edit = function (item) {
        page.scope.model = item;
        page.backup = angular.toJson(item);
        if (!item.content || item.content == "") { $("#content_div").html('<span style="color:#999;">点击编辑内容</span>'); }
        else { $("#content_div").html(item.content); }
        Zepto.router.load("#page_content");
    }
    $scope.new = function () {
        page.backup = "";
        page.scope.model = { id: "", proname: "", pics: "", proinfo: "", content: "", price: "0.00" };
    }
})
.controller("ConCtrl", function ($scope) {
    page.scope = $scope;
    $scope.cancel = function () {
        if (page.backup != "") {
            var item = JSON.parse(page.backup);
            //id,proname,pics,proinfo,content,price,cdate
            $scope.model.proname = item.proname;
            $scope.model.pics = item.pics;
            $scope.model.proinfo = item.proinfo;
            $scope.model.content = item.content;
            $scope.model.price = item.price;
            page.backup = "";
        }
        Zepto.router.back("#page_list");
    }
    $scope.save = function () {
        $scope.model.content = $("#content_div").html();
        //检测是否符合规范
        var price = parseFloat($scope.model.price);
        if (!price || isNaN(price) || price <= 0) { alert("价格不正确"); return; }
        if (!$scope.model.proname || $scope.model.proname == "") { alert("商品名称不能为空"); return; }
        //------------------
        var list = top.tools.clearlist(angular.toJson($scope.model));
        if ($scope.model.id == "") {
            //提交至后台,并将其加入数组
            Zepto.showPreloader();
            top.tools.updatedata("mb_pro_update", { "list": list }, function (data) {
                $scope.model.id = data;
                cfg.firstScope.list.push($scope.model);
                cfg.firstScope.$digest();
                Zepto.hidePreloader();
                Zepto.router.back("#page_list");
            })
        }
        else {
            //非新数据则提交更新
            top.tools.updatedata("mb_pro_update", { "list": list }, function (data) { console.log(list, data); })
            Zepto.router.back("#page_list");
        }
    }
    $scope.uppic = function () {
        picup.sel();
        picup.up_after = function (data) {
            $scope.model.pics = data;
            $scope.$digest();
            Zepto.hidePreloader();
        }
    }
    $scope.getpic = function () {
        if (!$scope.model || $scope.model.pics == "") { return "/Plugins/Ueditor/dialogs/image/images/image.png"; }
        else { return $scope.model.pics; }
    }
}).filter('html', ['$sce', function ($sce) { return function (text) { return $sce.trustAsHtml(text); } }]);
function openEditor() {
    //Zepto.popup('.popup-edit');
    //$("#pop_ifr")[0].contentWindow.settxt($("#content_div").html());
}
function closeEditor(content) {
    Zepto.closeModal();
}
function saveEditor(content) {
    $("#content_div").html(content);
}
</script>
</asp:Content>