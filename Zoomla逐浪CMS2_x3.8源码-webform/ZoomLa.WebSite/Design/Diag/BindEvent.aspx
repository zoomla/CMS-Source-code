<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BindEvent.aspx.cs" Inherits="Design_Diag_BindLink" MasterPageFile="~/Design/Master/Edit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>事件绑定</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div ng-app="app">
    <div ng-controller="ZLCtrl">
        <table class="table table-bordered">
    <tr>
        <td id="rad_td">
            <label><input type="radio" name="click_rad" value="0" ng-model="click.type"/>不关联</label>
            <label><input type="radio" name="click_rad" value="1" ng-model="click.type" />打开新窗口</label>
            <label><input type="radio" name="click_rad" value="2" ng-model="click.type"/>跳转页面</label>
            <label><input type="radio" name="click_rad" value="3" ng-model="click.type"/>执行JS</label>
            <label><input type="radio" name="click_rad" value="4" ng-model="click.type" />切换场景</label>
            <label><input type="radio" name="click_rad" value="5" ng-model="click.type"/>表单提交</label>
        </td>
    </tr>
    <tr>
        <td>
            <span ng-show="click.type==0">不绑定事件</span>
            <input ng-show="click.type==1||click.type==2" ng-model="click.url" type="text" class="form-control" placeholder="请输入链接,示例:/list?id=1" />
            <textarea ng-show="click.type==3" ng-model="click.js" class="form-control" placeholder="请输入JS代码"  style="height:120px;"></textarea>
            <select ng-show="click.type==4" ng-model="click.index">
                <option ng-repeat="m in list track by $index" value="{{$index}}" ng-selected="{{click.index==$index}}" >{{'第'+($index+1)+'页'}}</option>
            </select>
            <div ng-show="click.type==5">
                <table class="table table-bordered">
                    <tr><td style="width:120px;">表单名称</td><td><input type="text" class="form-control text_200" ng-model="model.config.fname" /></td></tr>
                    <tr><td>提交后提示</td><td><input type="text" class="form-control text_200" ng-model="click.prompt" /></td></tr>
                </table>
            </div>
        </td>
    </tr>
            <tr><td>
                <input type="button" value="保存生效" class="btn btn-primary" ng-click="save();" />
            </td></tr>
</table>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>#rad_td input,label{cursor:pointer;}</style>
<script src="/JS/Plugs/angular.min.js"></script>
<script>
    angular.module("app", []).controller("ZLCtrl", function ($scope) {
        $scope.model = editor.model;
        $scope.click = editor.model.dataMod.click;
        if (!$scope.click) { $scope.click = { type: "0", url: "", js: "",index:"0" }; }
        if (!$scope.click.type) { $scope.click.type = "0"; }
        $scope.save = function () {
            editor.model.dataMod.click = $scope.click;
            CloseSelf();
        }
        $scope.list = top.scence.list;
    });
</script>
</asp:Content>
