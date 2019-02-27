<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.pub_input.edit"MasterPageFile="~/Design/Master/Edit.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>输入框管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div ng-app="app" ng-controller="APPCtrl">
<table class="table table-bordered table-striped">
    <tr><td>名称</td><td><input type="text" ng-model="model.config.name" /></td></tr>
    <tr><td>正则</td><td>
        <span ng-repeat="item in regs">
            <label>
                <input type="checkbox" value="{{item.value}}" ng-checked="hascheck(item);" class="regex_chk" />
                <span ng-bind="item.text" ng-click="setregex();"></span>
            </label>
        </span>
    </td></tr>
    <tr><td>背景色</td><td><input type="text"/></td></tr>
    <tr><td></td><td><input type="button" value="保存" class="btn btn-primary" ng-click="save();" /></td></tr>
</table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/Plugs/angular.min.js"></script>
<script>
    angular.module("app", []).controller("APPCtrl", function ($scope) {
        //必填,手机,邮箱,QQ
        $scope.regs = [{ text: "必填", value: "required" }, { text: "手机", value: "mobile" }, { text: "邮箱", value: "email" }, { text: "QQ", value: "qq" }];
        $scope.model = editor.model;
        if (!$scope.model.config.regex) { $scope.model.config.regex = ""; }
        $scope.hascheck = function (item) { return $scope.model.config.regex.indexOf(item.value) > -1; }
        $scope.setregex = function () {
            var result = "";
            $(".regex_chk:checked").each(function () { result += this.value + ","; });
            $scope.model.config.regex = result;
        }
        $scope.save = function () { NotifyUpdate(); CloseSelf(); }
    });
</script>
</asp:Content>