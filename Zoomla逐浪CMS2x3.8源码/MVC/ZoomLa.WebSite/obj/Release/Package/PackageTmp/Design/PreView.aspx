<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PreView.aspx.cs" Inherits="ZoomLaCMS.Design.PreView" EnableViewState="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<asp:Literal runat="server" ID="Meta_L" EnableViewState="false"></asp:Literal>
<title><asp:Literal runat="server" ID="Title_L" EnableViewState="false"></asp:Literal></title>
<link href="/Design/res/css/comp.css" rel="stylesheet" />
<script src="/Design/JS/Plugs/covervid.js"></script>
<asp:Literal runat="server" ID="Resource_L" EnableViewState="false"></asp:Literal>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div ng-app="app">
        <div id="editorBody" ng-controller="appCtrl">
            <div id="midBody" class="container" style="position: relative; padding: 0px;"></div>
            <div id="mainBody"></div>
        </div>
    </div>
    <div runat="server" id="tlpprompt_div" visible="false" class="panel panel-info" style="position: fixed; width: 100%; padding: 10px; top: 0; text-align: center;background-color:rgba(255, 255, 255, 0.40)">
        <span runat="server" id="tlpinfo_sp"></span>
        <div runat="server" id="nologin_wrap" visible="false" style="display:inline-block;">
            <a href="/User/Login.aspx?ReturnUrl=<%:HttpUtility.UrlEncode(Request.RawUrl) %>" class="btn btn-info">立即登陆</a>
            选用此模板,<a class="btn btn-link" href="/User/Register.aspx">注册会员</a>
        </div>
        <div runat="server" id="logged_wrap" visible="false" style="display:inline-block;">
            <asp:LinkButton runat="server" ID="Apply_Btn" CssClass="btn btn-info" OnClientClick="return confirm('确定要应用该模板吗?');" OnClick="Apply_Btn_Click">选用此模板</asp:LinkButton>
            <a href="/design/" class="btn btn-info">进入设计台</a>
        </div>
    </div>
    <div id="PowerZoomlaT">
        <a href="http://www.z01.com/pub" target="_blank" title="基于Zoomla!逐浪CMS-点击免费下载企业版">
            <span></span><i class="fa ZoomlaICO2015"></i></a>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/Design/JS/sea.js"></script>
<script src="/JS/Modal/EventBase.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script>
var sitecfg=<%=sitecfg%>;
var scope;
seajs.use(["page"], function (page) {

    page.guid = "<%:pageMod.guid%>";
    page.token="<%:Session.SessionID%>";
    page.pageData =<%=pageMod.page%>;
    page.compData = <%=pageMod.comp%>;
    page.comp_global=<%=comp_global%>
    page.extendData=<%=extendData%>;

    page.instance = $(document);
    page.init();
    for (var i = 0; i < page.compList.length; i++) {
        page.compList[i].mode="view";
        scope.addDom(page.compList[i]);
    }
});
var app = angular.module("app", [], function ($compileProvider) { })
    .controller("appCtrl", function ($scope, $compile) {
        scope = $scope;
        $scope.list = {};
        $scope.addDom = function (compObj) {
            if ($scope.list[compObj.id]) {  return; }
            $scope.list[compObj.id] = compObj;
            var html = $(compObj.AnalyToHtml());
            html.attr("style",compObj.config.style);
            compObj.SetInstance($compile(angular.element(html))($scope),document);
            //-----确定加入哪一个body中
            var bodyid = "midBody";
            if (compObj.config.bodyid && compObj.config.bodyid != "") { bodyid = compObj.config.bodyid; }
            //-----
            angular.element(document.getElementById(bodyid)).append(compObj.instance);
        }
    })
    .filter("html", ["$sce", function ($sce) {
        return function (text) { return $sce.trustAsHtml(text); }
    }]);
//-----事件监测
eventBase.add("editor_update",function(param){
    scope.$digest();
});
</script>
</asp:Content>
