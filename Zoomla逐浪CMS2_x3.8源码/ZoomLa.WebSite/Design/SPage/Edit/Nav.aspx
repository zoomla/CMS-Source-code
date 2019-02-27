<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nav.aspx.cs" Inherits="Design_SPage_Edit_Nav" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>菜单管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div ng-app="APP" ng-controller="ZLCtrl" style="min-height:500px;">
<table class="table table-bordered">
    <tr><td class="td_md">文字</td><td>链接</td><td class="td_m">操作</td></tr>
    <tr ng-repeat="item in list track by $index">
        <td><input type="text" class="form-control" ng-model="item.text"/></td>
        <td><input type="text" class="form-control" ng-model="item.url"/></td>
        <td><button type="button" ng-click="del(item);" class="btn btn-info"><i class="fa fa-trash-o"></i></button></td>
    </tr>
    <tr><td></td><td colspan="2">
        <button type="button" class="btn btn-info" ng-click="add();"><i class="fa fa-plus"></i> 添加一列</button>
        <input type="button" value="保存信息" class="btn btn-info" ng-click="save();" />
   </td></tr>
</table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/Plugs/angular.min.js"></script>
<script>
    angular.module("APP", []).controller("ZLCtrl", function ($scope) {
        $scope.model = parent.scope.comp;
        $scope.list = $scope.model.data.value;
        $scope.add = function () { $scope.list.push({ url: "#", text: "" }); }
        $scope.del = function (item) {
            for (var i = 0; i < $scope.list.length; i++) {
                if ($scope.list[i] == item) {$scope.list.splice(i, 1); return; }
            }
        }
        $scope.save = function () {
            parent.scope.update();
        }
    });
</script>
</asp:Content>