<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FastCreate.aspx.cs" Inherits="Design_mbh5_FastCreate" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/Plugins/ionic/css/ionic.css" rel="stylesheet">
<title>快速生成</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="main" ng-app="app"ng-controller="APPCtrl" >
<div class="bar bar-header bar-calm">
    <a class="button icon ion-chevron-left" href="AddScence.aspx"></a>
    <h1 class="title">快速生成</h1>
</div>
<div class="scroll-content ionic-scroll has-header has-tabs" style="background-color:#f5f5f5;">
<div id="imgs_wrap">
    <a href="javascript:;" onclick="$('#file_up').click();" style="border:1px dashed #ddd;">
        <img src="/Plugins/Ueditor/dialogs/image/images/image.png" />
    </a>
    <a href="javascript:;" ng-repeat="item in imgs track by $index">
        <img ng-src="{{item.src}}" />
        <button type="button" class="button btn_del button-small button-assertive button-block" ng-click="delimg(item);"><i class="fa fa-trash-o" style="font-size:20px;"></i></button>
    </a>
</div>
<input type="file" id="file_up" accept="image/*" style="display:none;" onchange="picup.upload();" />
<asp:HiddenField runat="server" ID="imgs_hid" />
<asp:Button runat="server" ID="save_btn" style="display:none;" OnClick="save_btn_Click" />
</div>
<button class="button button-balanced button-block" type="button" ng-click="save();" ng-disabled="cansubmit()" style="position:fixed;bottom:0px;margin:0px;">
        生成场景
    </button>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<%--<script src="/JS/Plugs/angular.min.js"></script>--%>
<style type="text/css">
.pc_div{height:500px;}
#imgs_wrap {width:98%;margin:8px;min-height:300px;}
#imgs_wrap a { width:31%;height:100px;text-align:center;display:inline-block;position:relative;margin-right:5px;margin-bottom:30px;background-color:#fff;}
.pc_div #imgs_wrap a{height:150px;}
#imgs_wrap .btn_del {position:absolute;bottom:-40px;border-radius:0px;}
#imgs_wrap img {max-width:100%;max-height:100%;margin: auto; position: absolute;  top: 0; left: 0; bottom: 0; right: 0; }
</style>
<link href="/dist/css/weui.min.css" rel="stylesheet" />
<script src="/JS/Controls/ZL_Webup.js"></script>
<script src="/JS/Mobile/ResizeImg/lrz.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/Plugins/ionic/js/ionic.bundle.js"></script>
<script src="/Design/JS/weui.js"></script>
<script>
//console.log = function (msg) { alert(msg); }
//console.info = function (msg) { alert(msg); }
//console.error = function (msg) { alert(msg); }
//window.onerror = function (msg) { alert(msg); }
picup.zip.enable = true;
picup.$up = $("#file_up");
picup.up_before = function () { weui.toast.wait(); }
picup.up_after = function (data) {
    scope.imgs.push({ id: Math.random(), src: data });
    scope.$digest();
    weui.toast.close();
}
var scope = null;
angular.module("app", []).controller("APPCtrl", function ($scope) {
    scope = $scope;
    weui.toast.wait();
    $scope.imgs = [];
    //$scope.imgs.push({ id: Math.random(), src: "/images/nopic.gif" });
    //$scope.imgs.push({ id: Math.random(), src: "/images/nopic.gif" });
    //$scope.imgs.push({ id: Math.random(), src: "/images/nopic.gif" });
    //$scope.imgs.push({ id: Math.random(), src: "/images/nopic.gif" });
    //$scope.imgs.push({ id: Math.random(), src: "/images/nopic.gif" });
    $scope.delimg = function (item) {
        for (var i = 0; i < $scope.imgs.length; i++) {
            if ($scope.imgs[i].id == item.id) { $scope.imgs.splice(i, 1); break; }

        }
    }
    //提交图片,生成新场景
    $scope.save = function () {
        if ($scope.imgs.length < 1) { alert("未指定图片"); return; }
        var imgs = "";
        for (var i = 0; i < $scope.imgs.length; i++) {
            imgs += $scope.imgs[i].src + "|";
        }
        $("#imgs_hid").val(imgs);
        $("#save_btn").click();
    }
    $scope.cansubmit = function () { return $scope.imgs.length < 1; }
    weui.toast.close();
    $(function () {
        if ('<%=IsPC%>' == 'True') { $(".main").addClass("pc_div"); }
    })
});
</script>
</asp:Content>