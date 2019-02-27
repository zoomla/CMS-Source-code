<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreView.aspx.cs" Inherits="ZoomLaCMS.Design.SPage.PreView" EnableViewState="false" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<%=pageMod.PageRes %>
</head>
<body>
<form id="form1" runat="server">
    <div ng-app="APP" ng-controller="ZLCtrl" id="editor">
    <div class="layout_list">
        <div ng-repeat="layout in Layout.list|orderBy:'order' track by $index">
            <div class="layout clearfix" ng-switch="layout.type">
                <div ng-switch-when="fill">
                    <div class="column">
                        <div class="comp_wrap" ng-repeat="comp in layout.middle|orderBy:'order'  track by $index">
                            <comp></comp>
                        </div>
                    </div>
                </div>
                <div ng-switch-when="12" class="container">
                    <div class="column">
                        <div class="comp_wrap" ng-repeat="comp in layout.middle|orderBy:'order'  track by $index">
                            <comp></comp>
                        </div>
                    </div>
                </div>
                <div ng-switch-when="48" class="container">
                    <div class="row">
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 column">
                            <div class="comp_wrapcomp_wrap" ng-repeat="comp in layout.left|orderBy:'order'  track by $index">
                                <comp></comp>
                            </div>
                        </div>
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12 column">
                            <div class="comp_wrap" ng-repeat="comp in layout.right|orderBy:'order'  track by $index">
                                <comp></comp>
                            </div>
                        </div>
                    </div>
                </div>
                <div ng-switch-when="84" class="container">
                    <div class="row">
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-12 column">
                            <div class="comp_wrap" ng-repeat="comp in layout.left|orderBy:'order'  track by $index">
                                <comp></comp>
                            </div>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 column">
                            <div class="comp_wrap" ng-repeat="comp in layout.right|orderBy:'order'  track by $index">
                                <comp></comp>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix"></div>
            </div>
        </div>
    </div>
</div>
</form>
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
    angular.module("APP", []).controller("ZLCtrl", function ($scope,$http) {
        scope=$scope;
        $scope.Layout = {list:<%=pageMod.Layouts%>};
        $scope.labelToHtml=function(comp)
        {
            //content解析出的值附加于html上,不保存
            var str=comp.data.value;
            if(str.indexOf("<")>-1){comp.html=str;}
            else {
                $scope.label(comp);
            }
        }
        $scope.label=function(comp)
        {
            var postCfg={headers:{'X-Requested-With': "XMLHttpRequest", 'Content-Type': 'application/x-www-form-urlencoded;'},transformRequest: function(data) {
                //其不支持如jquery般支持传json
                return $.param(data);
            }};
            $http.post("/design/spage/api.ashx?action=base64&itemid=<%:ItemID%>&cpage=<%:CPage%>", {"base64":comp.data.value,"dslabel":"<%=pageMod.PageDSLabel%>"},postCfg).success(function(html){
                $("#"+comp.id+"_div").html(html);
            })
        }
        //$scope.gethtml=function(comp){return $scope.htmls[comp.id];}
        $scope.update=function(){
            closeDiag();
            $scope.$digest();
            $.post("default.aspx?id=<%:pageMod.ID%>",{action:"save","layouts":angular.toJson($scope.Layout.list)},function(data){
                APIResult.ifok(data,function(){},function(data){alert(data);})
            })
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
document.write=function(){}
document.writeln=function(){}
</script>
<%=pageMod.PageBottom %>
</body>
</html>