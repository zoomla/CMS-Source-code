<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageDesign.aspx.cs" Inherits="Design_SPage_PageDesign" MasterPageFile="~/Common/Master/Empty2.master" EnableViewState="false" ValidateRequest="false"%>
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
.comp p {margin-bottom:0px;}
.comp .mask {z-index:100;background-color:rgba(216, 212, 212, 0.50);width:100%;height:100%;left:0;top:0; position:absolute;cursor:pointer;text-align:center;font-size:16px;color:#fff;font-weight:bold;display:none;}
.comp .mask .txt {position:absolute;top:40%;left:40%;}
.width1100 {width:900px;}
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
    $scope.comp=null;//当前正在操作的元素
    $scope.showEdit=function(comp){
        $scope.comp=comp;
        ShowComDiag("/design/spage/edit/"+comp.type+".aspx",comp.name);
    }
    //自定义内容需要base64处理
    $scope.base64ToHtml=function(comp){ 
        var str=comp.data.value;
        if(str.indexOf("<")>-1){return str;}
        else return  Base64.decode(str);
    }
    $scope.update=function(){
        closeDiag();
        $scope.$digest();
        $.post("default.aspx?id=<%:pageMod.ID%>",{action:"save","layouts":angular.toJson($scope.Layout.list)},function(data){
            APIResult.ifok(data,function(){},function(data){alert(data);})
        })
    }
    setTimeout(function(){
        $(".comp").hover(function(){
            $(this).find('.mask').show()
        },function(){
            $(this).find('.mask').hide();
        });
    },500);
}).filter("html", ["$sce", function ($sce) {
    return function (text) { return $sce.trustAsHtml(text); }
}]).directive("comp",function(){
    return {
        restrict: 'E',
        replace: true,
        templateUrl:"/design/spage/comp.html",
    }
});
    //compile阶段进行标签解析和变换，link阶段进行数据绑定等操
    //http://damoqiongqiu.iteye.com/blog/1917971/
    //controller先运行，compile后运行，link不运行(link就是compile中的postLin
function closeDiag(){comdiag.CloseModal();}
</script>
</asp:Content>