<%@ Page Language="C#" AutoEventWireup="true" CodeFile="edit.aspx.cs" Inherits="Design_Diag_Gallery_edit" MasterPageFile="~/Design/Master/Edit.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>幻灯片组件</title></asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="server">
<div ng-app="app" style="min-height:400px;">
<ul id="formul" class="list-unstyled" ng-controller="NoteCtrl" >
    <li class="boders" ng-repeat="item in list | orderBy: 'orderid'">
        <img data-id="{{item.$$hashKey}}" ng-src="{{item.url}}"/>
        <div><input type="text" class="form-control" style="border:none;" ng-model="item.name" placeholder="请输入文字" /></div>
        <div class="opdiv">
            <a href="javascript:;" class="btn btn-xs btn-info" ng-click="remove(item);"><i class="fa fa-remove"></i></a>
        </div>
    </li>
    <li class="ui-state-disabled" title="添加图片" id="uppics_btn">
        <i class="fa fa-plus" style="font-size: 5em;"></i>
    </li>
    <li style="clear:both;display:none;"></li>
</ul>
</div>
</asp:Content>
<asp:Content ContentPlaceHolderID="Script" runat="server">
<link rel="stylesheet" href="/App_Themes/V3.css" />
<style id="styles">
img{width:100%; height:80px;}
#formul li{float:left;cursor:pointer;margin-left:10px;width:100px; border:1px solid #ccc; border-radius:0 0 4px 4px;margin-bottom:2px;}
#formul li:hover {border:1px solid #ff6a00}
#formul li .opdiv {border-top:1px solid #ddd;padding:4px;}
.highlight{border:2px solid #ff6a00!important; border-radius:5px;  width:100px!important; height:80px!important;}
.ui-state-disabled {color: #808080; height: 140px; padding-top: 28px;text-align:center;border:1px solid #ddd;}
</style>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/JS/jquery-ui.min.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<script>
var scope = null;

angular.module("app", []).controller("NoteCtrl", function ($scope, $compile) {
    scope = $scope;
    scope.list = editor.model.dataMod.list;
    $scope.remove = function (item) {
        ArrCOM.RemoveByID(editor.model.dataMod.list, item.$$hashKey, "$$hashKey");
        NotifyUpdate();
    }
    $("#formul").sortable({
        placeholder: "highlight",
        cursor: 'crosshair',
        cancel: ".ui-state-disabled",
        update: function (evt) {
            $scope.$apply(function () {
                $("#formul li img").each(function (i, v) {
                    var obj = ArrCOM.GetByID(editor.model.dataMod.list, $(v).data('id'), "$$hashKey")
                    obj.orderid = i + 1;
                });
            });
            FireEvent("render");
            NotifyUpdate();
        }
    }).disableSelection();
});
$(function () {
    ZL_Webup.config.json.ashx = "action=design";
    $("#uppics_btn").click(ZL_Webup.ShowFileUP);
})
function AddAttach(file, ret, pval) {
    var model = GetItemModel()[0];
    model.url = ret._raw;
    editor.model.dataMod.list.push(model);
    scope.$digest();
    FireEvent("render");
    NotifyUpdate();
    ZL_Webup.attachDiag.CloseModal();
}
function GetItemModel(num, type) {
    var list = [];
    num = Convert.ToInt(num, 1);
    var getModelByType = function (type) {
        //url:图片路径,css应用时的状态,name==text:用于承载名称或字符,tip:""提示
        return { name: "标题", tip: "", orderid: 0, url: "", href: "#", target: "", css: "", fa: "", show: true, addon: "" };
    }
    for (var i = 0; i < num; i++) {
        var item = getModelByType(type);
        item.orderid = i;
        list.push(item);
    }
    return list;
}
</script>
</asp:Content>