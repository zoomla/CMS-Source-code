<%@ Page Language="C#" AutoEventWireup="true" CodeFile="domonpage.aspx.cs" Inherits="Design_scence_domonpage" MasterPageFile="~/Design/Master/Edit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>页面元素</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped" ng-controller="appCtrl">
    <tr><td style="width:80px;">内容</td><td>类型</td><td>进场时间</td><td>效果</td><td>操作</td></tr>
    <tr ng-repeat="item in list">
        <td ng-bind-html="getcontent(item)|html"></td>
        <td ng-bind="item.config.type"></td>
        <td ng-bind="item.config.animate.delay+'s'"></td>
        <td ng-bind="getname(item.config.animate.effect)"></td>
        <td>
            <a href="javascript:;" title="动画" ng-click="editani(item.id);" class="btn btn-sm btn-info"><i class="fa fa-send"></i></a>
            <a href="javascript:;" title="修改" ng-click="edit(item);" class="btn btn-sm btn-info"><i class="fa fa-pencil"></i></a>
<%--            <a href="javascript::" title="删除" ng-click="del(item.id);" class="btn btn-sm btn-info"><i class="fa fa-trash-o"></i></a>--%>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>.pimg{max-width:50px;max-height:50px;border:1px solid #ccc;padding:1px;}</style>
<script src="/design/h5/js/animap.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script>
    angular.module("app", []).controller("appCtrl", function ($scope) {
    var bodyid = top.scence.bodyid;
    $scope.list = [];
    for (var i = 0; i < top.page.compList.length; i++) {
        var comp = top.page.compList[i];
        if (comp.config.bodyid == bodyid) {
            $scope.list.push(comp);
        }
    }
    $scope.getcontent = function (comp) {
        switch (comp.config.type)
        {
            case "text":
                return comp.dataMod.text;
            case "image":
                return '<img src="' + comp.dataMod.src + '" class="pimg" />';
            default:
                return comp.confi.type;
        }
    }
    $scope.getname = function (effect) { return animap.getname(effect); }
    $scope.editani = function (id) {
        location = "/design/diag/common/animate.aspx?id=" + id;
    }
    $scope.edit = function (item) {
        location = "/design/diag/" + item.config.type + "/edit.aspx?id=" + item.id;;
    }
    }).filter(
		'html', ['$sce', function ($sce) {
		    return function (text) {
		        return $sce.trustAsHtml(text);
		    }
		}]
	);
//根据bodyid筛数据,根据id移除数据,跨iframe后array属性丢失
</script>

</asp:Content>