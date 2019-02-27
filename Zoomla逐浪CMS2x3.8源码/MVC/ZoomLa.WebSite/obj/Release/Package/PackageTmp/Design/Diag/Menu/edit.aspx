<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edit.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Menu.edit" MasterPageFile="~/Design/Master/Edit.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script src="/JS/Plugs/angular.min.js"></script>
    <script src="/Design/JS/sea.js"></script>
    <title>菜单组件</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div ng-app="app">
   <table class="table table-bordered table-striped" ng-controller="appController">
       <tr><td>序号</td><td>名称</td><td>超链接</td><td>操作</td></tr>
       <tbody ng-model="model.dataMod">
           <tr ng-repeat="item in model.dataMod.items|orderBy:'orderid'">
               <td><input type="text" class="form-control text_50"  ng-model="item.orderid"/></td>
               <td><input type="text" class="form-control text_80"  ng-model="item.name"/></td>
               <td><input type="text" class="form-control text_200" ng-model="item.href"/></td>
               <td><a href="javascript:;" ng-click="remove(item);"><i class="fa fa-remove"></i></a></td>
           </tr>
       </tbody>
       <tr>
           <td colspan="4">
               <input type="button" value="更新修改" class="btn btn-primary" onclick="NotifyUpdate();" />
               <input type="button" value="新增一行" class="btn btn-primary" ng-click="add();" /></td>
       </tr>
   </table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
        angular.module("app", []).controller("appController", function ($scope, $compile) {
            $scope.model = editor.model;
            $scope.remove = function (item) {
                $scope.model.dataMod.items.forEach(function (v, i, _) {
                    if (v == item) {
                        _.splice(i, 1);
                        parent.editor.scope.$digest();
                        return;
                    }
                });
            }//remove end;
            $scope.add = function () {
                editor.model.dataMod.items.push({ orderid: editor.model.dataMod.items.length, name: "名称", href: "#" });
            }
            angular.forEach(editor.model.dataMod.items, function (data, index, arr) {
                if (!data.orderid) { data.orderid = 0; }
            })
        })
    </script>
</asp:Content>