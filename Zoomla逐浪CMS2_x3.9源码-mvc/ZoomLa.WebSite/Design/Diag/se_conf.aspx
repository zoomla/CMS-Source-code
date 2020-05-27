<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="se_conf.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.se_conf" MasterPageFile="~/Design/Master/Edit.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>场景设置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped" ng-controller="ZLCtrl">
    <tr><td class="td_md">页面方向</td>
        <td>
             <label ng-repeat="item in directArr"><input type="radio" name="direction_rad" value="{{item.direction}}" ng-model="conf.direction" />{{item.name}}</label>
        </td>
    </tr>
    <tr><td>页面切换</td><td>
            <label ng-repeat="item in effectArr"><input type="radio" name="effect_rad" value="{{item.effect}}" ng-model="conf.effect" />{{item.name}}</label>
    </td></tr>
    <tr><td>自动播放</td><td><input type="text" class="form-control text_150" ng-model="conf.autoplay" /><span>0为不自动播放,单位:毫秒</span></td></tr>
    <tr><td>循环播放</td><td><input type="checkbox" ng-model="conf.loop" /></td></tr>
    <tr><td>音乐自动播放</td><td><input type="checkbox" ng-model="conf.automusic" /></td></tr>
    <tr><td></td><td>
        <input type="button" value="保存" class="btn btn-info" ng-click="save();" />
        <input type="button" value="关闭" class="btn btn-default" onclick="CloseSelf();" />
     </td></tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/Plugs/angular.min.js"></script>
<script>
    angular.module("app", []).controller("ZLCtrl",function ($scope) {
        $scope.directArr = [{ direction: "vertical", name: "垂直" }, { direction: "horizontal", name: "横向" }];
        $scope.effectArr = [{ effect: "slide", name: "滑动" }, { effect: "fade", name: "渐隐" }, { effect: "cube", name: "立方" }, { effect: "coverflow", name: "封面" }, { effect: "flip", name: "翻转" }]
        $scope.conf = top.scence.conf;
        console.log($scope.conf.automusic);
        if (typeof ($scope.conf.automusic) == "undefined") { $scope.conf.automusic = true;}
        $scope.save = function () {
            CloseSelf();
        }
    });
</script>
</asp:Content>