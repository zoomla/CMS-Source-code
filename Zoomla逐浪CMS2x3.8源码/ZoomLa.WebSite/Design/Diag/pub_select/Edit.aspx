<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Edit.aspx.cs" Inherits="Design_Diag_pub_select_Edit" MasterPageFile="~/Design/Master/Edit.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>输入框管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div ng-app="app" ng-controller="APPCtrl">
<table class="table table-bordered table-striped">
    <tr><td class="td_md">名称</td><td><input type="text" ng-model="model.config.name" /></td></tr>
    <tr><td>选项<br /><a href="javascript:;" class="btn btn-info" ng-click="add();"><i class="fa fa-plus"></i></a></td><td>
        <table class="table table-bordered">
            <tr ng-repeat="item in model.dataMod.list track by $index">
                <td><input type="text" class="form-control text_150" placeholder="选项名" ng-model="item.text"/></td>
                <td><input type="text" class="form-control text_150" placeholder="选项值" ng-model="item.value" /></td>
                <td><a href="javascript:;" title="删除" class="btn btn-info" ng-click="del(item);"><i class="fa fa-minus"></i></a></td>
            </tr>
        </table>
                   </td></tr>
    <tr><td></td><td><input type="button" value="保存" class="btn btn-primary" ng-click="save();" /></td></tr>
</table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/Plugs/angular.min.js"></script>
<script>
    angular.module("app", []).controller("APPCtrl", function ($scope) {
        $scope.model = editor.model;
        $scope.save = function () { NotifyUpdate(); CloseSelf(); }
        $scope.del = function (item) {
            var list = $scope.model.dataMod.list;
            for (var i = 0; i < list.length; i++) {
                if (list[i] == item) { list.splice(i, 1); return; }
            }
        }
        $scope.add = function () { $scope.model.dataMod.list.push({ text: "名称", value: "选项值" }); }
    });
</script>
</asp:Content>