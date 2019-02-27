<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PageDesign.aspx.cs" Inherits="ZoomLaCMS.Design.SPage.PageDesign" MasterPageFile="~/Common/Master/Empty2.master" EnableViewState="false" ValidateRequest="false" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <%=pageMod.PageRes %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="btn-group-vertical" style="position: fixed; left: 20px; top: 20px; z-index: 9999;">
        <a href="Default.aspx?id=<%:Mid %>" class="btn btn-default" title="返回布局"><i class="fa fa-pencil fa-2x"></i></a>
        <a href="<%=pageMod.ViewUrl %>" class="btn btn-default" title="预览" target="_blank"><i class="fa fa-eye fa-2x"></i></a>
        <a href="javascript:;" onclick="ShowComDiag('AddPage.aspx?ID=<%:Mid %>','页面配置');" class="btn btn-default" title="配置"><i class="fa fa-cog fa-2x"></i></a>
    </div>
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
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
        /*.return_btn{display:inline-block;width:50px;height:50px;position:fixed;top:20px;left:20px;text-align:center; border-radius:4px;background-color:rgba(0, 0, 0, 0.50);color:#fff;padding-top:10px;z-index:101;}
.return_btn:hover{color:#fff;background-color:rgba(134, 60, 8, 0.50);}*/
        .comp { position: relative; outline: 1px dashed #337ab7; }
        .comp p { margin-bottom: 0px; }
        .comp .mask { z-index: 100; background-color: rgba(0, 0, 0, 0.50); width: 100%; height: 100%; left: 0; top: 0; position: absolute; cursor: pointer; text-align: center; font-size: 16px; color: #fff; font-weight: bold; display: none; }
        .comp .mask .txt { position: absolute; top: 40%; left: 40%; }
        .width1100 { width: 1100px; }
		.min100 { min-height:100px;}
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
        $scope.comp=null;//当前正在操作的元素
        $scope.showEdit=function(comp){
            $scope.comp=comp;
            console.log(comp,comdiag);
            ShowComDiag("/design/spage/edit/"+comp.type+".aspx",comp.name);
        }
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
            $http.post("/design/spage/api.ashx?action=base64", {"base64":comp.data.value,"dslabel":"<%=pageMod.PageDSLabel%>"},postCfg).success(function(html){
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
    document.write=function(){}
    document.writeln=function(){}
    //compile阶段进行标签解析和变换，link阶段进行数据绑定等操
    //http://damoqiongqiu.iteye.com/blog/1917971/
    //controller先运行，compile后运行，link不运行(link就是compile中的postLin
    function closeDiag(){comdiag.CloseModal();}
    </script>
    <%=pageMod.PageBottom %>
</asp:Content>
