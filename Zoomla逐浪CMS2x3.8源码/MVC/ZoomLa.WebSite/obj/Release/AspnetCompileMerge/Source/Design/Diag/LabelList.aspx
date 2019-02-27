<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LabelList.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.LabelList" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script src="/Design/JS/sea.js"></script>
    <script src="/JS/Plugs/angular.min.js"></script>
    <link href="/Design/res/css/edit/common.css" rel="stylesheet" />
    <title>标签管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
   <div ng-app="app">
       <table class="table table-bordered table-striped" ng-controller="appController">
       <tr><td>序号</td><td>引用标签</td><td>操作</td></tr>
       <tbody ng-model="model.dataMod">
           <tr ng-repeat="item in compList|filter:myFilter">
               <td class="td_m">{{$index+1}}</td>
               <td><div style="width:400px;text-overflow:ellipsis;overflow-x:hidden;">{{item.dataMod.alias}}</div></td>
               <td>
                   <a href="javascript:;" title="删除标签" ng-click="remove(item);"><i class="fa fa-remove"></i></a>
               </td>
           </tr>
       </tbody>
   </table>
   </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script>
        var editor = { id: "<%:Request.QueryString["ID"]%>"};
        angular.module("app", []).controller("appController", function ($scope, $compile) {
            $scope.compList = top.page.compList;
            $scope.myFilter = function (item) {
                return item.config.type == "label";
            };
            $scope.remove = function (item) {
                $scope.compList.forEach(function (v, i, _) {
                    if (v == item) {
                        _.splice(i, 1);
                        item.RemoveSelf();
                        return;
                    }
                });
            }//remove end;
        })
    </script>
</asp:Content>
