<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Progress.aspx.cs" Inherits="test_diag_Progress" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>进度管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="complete_list" ng-app="app">
<ul class="list-unstyled" ng-controller="listCtrl">
    <li ng-repeat="item in list" ng-class="{'finish':item.isok}">
        <strong class="num" ng-if="item.isok==false">{{item.index}}</strong>
        <strong class="num on"  ng-if="item.isok"><i class="fa fa-check"></i></strong>
        <span style="position:absolute;left:-1px; border-bottom:2px solid #fff;height:42px; width:40px;"></span>
        <span class="title" ng-bind="item.title"></span>
        <span class="desc" ng-bind="item.desc"></span>
        <span class="pull-right">
            <a href="javascript:;" ng-show="!item.isok" class="btn btn-sm btn-warning" onclick="parent.closeDiag();">前往</a>
            <a href="javascript:;" ng-show="item.isok" class="btn btn-sm btn-default">完成</a>
        </span>
        <div class="clearfix"></div>
    </li>
</ul>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.complete_list {border:none;background:#fff;position:relative;overflow:hidden;}
.complete_list ul {position:relative;}
.complete_list ul li {padding:10px 16px; margin-left:40px; position:relative;border-bottom:1px solid #efefef;border-left:1px solid #ddd;}
.complete_list ul li.finish {color:#999;}
.complete_list .num {width:38px;position:absolute;background:#EAEAEA;left:-20px;
font-size:16px;color:#bbb;font-weight:normal;text-align:center;z-index:10;border-radius:50%;padding-top:7px;padding-bottom:7px;}
.complete_list .on.num {padding-top:11px;padding-bottom:11px;background-color:#FFC65A;color:#fff;}
.complete_list .title {display:inline-block;width:240px;margin-left:28px;}
.complete_list .desc {margin-left:20px;}
</style>
<script src="/JS/Plugs/angular.min.js"></script>
<script>
    angular.module("app", []).controller("listCtrl",function ($scope) {
    var list = [];
    list.push({ index: 1, title: "创建新游记", desc: "完成度 +20%", isok: true });
    list.push({ index: 2, title: "添加游记头图", desc: "完成度 +15%", isok: false });
    list.push({ index: 3, title: "至少1张照片", desc: "完成度 +20%", isok: false });
    list.push({ index: 4, title: "至少添加1个段落", desc: "完成度 +15%", isok: false });
    list.push({ index: 5, title: "设置封面图片", desc: "完成度 +10%", isok: false });
    list.push({ index: 6, title: "设置视频", desc: "完成度 +10%", isok: false });
    list.push({ index: 7, title: "添加音乐", desc: "完成度 +10%", isok: false });
    var comMod = parent.scope.comMod;
    if (comMod.topimg != "") { list[1].isok = true; }

    if (comMod.comlist.GetByID("image", "type")) { list[2].isok = true; list[4].isok = true; }
    if (comMod.comlist.GetByID("para", "type")) { list[3].isok = true; }
    
    if (comMod.comlist.GetByID("video", "type")) { list[5].isok = true; }
    if (comMod.mp3 != "") { list[6].isok = true; }
    $scope.list = list;
})
</script>
</asp:Content>

