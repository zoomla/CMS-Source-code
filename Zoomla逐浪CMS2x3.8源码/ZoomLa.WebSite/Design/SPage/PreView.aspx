<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreView.aspx.cs" Inherits="Design_SPage_PreView" MasterPageFile="~/Common/Master/Empty2.master" EnableViewState="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title><%:pageMod.PageName %></title>
<%=pageMod.PageRes %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div ng-app="APP" ng-controller="ZLCtrl" id="editor">
       <div class="layout_list">
           <div ng-class="{'container':layout.type!='fill'}" ng-repeat="layout in Layout.list|orderBy:'order' track by $index">
               <div class="layout row clearfix" ng-switch="layout.type">
                   <div ng-switch-when="fill">
                       <div class="column">
                           <div class="comp_wrap" ng-repeat="comp in layout.middle|orderBy:'order'  track by $index">
                               <comp></comp>
                           </div>
                       </div>
                   </div>
                   <div ng-switch-when="12">
                       <div class="col-md-12 column">
                           <div class="comp_wrap" ng-repeat="comp in layout.middle|orderBy:'order'  track by $index">
                               <comp></comp>
                           </div>
                       </div>
                   </div>
                   <div ng-switch-when="48" class="container">
                       <div class="col-md-4 column">
                           <div class="comp_wrapcomp_wrap" ng-repeat="comp in layout.left|orderBy:'order'  track by $index">
                               <comp></comp>
                           </div>
                       </div>
                       <div class="col-md-8 column">
                           <div class="comp_wrap" ng-repeat="comp in layout.right|orderBy:'order'  track by $index">
                               <comp></comp>
                           </div>
                       </div>
                   </div>
                   <div ng-switch-when="84" class="container">
                       <div class="col-md-8 column">
                           <div class="comp_wrap" ng-repeat="comp in layout.left|orderBy:'order'  track by $index">
                               <comp></comp>
                           </div>
                       </div>
                       <div class="col-md-4 column">
                           <div class="comp_wrap" ng-repeat="comp in layout.right|orderBy:'order'  track by $index">
                               <comp></comp>
                           </div>
                       </div>
                   </div>
                   <div class="clearfix"></div>
               </div>
           </div>
       </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.comp {position:relative;}
.comp .mask {display:none;}
.comp p {margin-bottom:0px;}
</style>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script src="/JS/Plugs/base64.js"></script>
<script>
var scope = null;
angular.module("APP", []).controller("ZLCtrl", function ($scope) {
    scope=$scope;
    $scope.Layout = {list:<%=pageMod.Layouts%>};
    $scope.base64ToHtml=function(comp){ 
        var str=comp.data.value;
        if(str.indexOf("<")>-1){return str;}
        else return  Base64.decode(str);
    }
}).filter("html", ["$sce", function ($sce) {
    return function (text) { return $sce.trustAsHtml(text); }
}]).directive("comp",function(){
    return {
        restrict: 'E',
        replace: true,
        templateUrl:"/design/spage/comp.html",
    }
});
</script>
</asp:Content>