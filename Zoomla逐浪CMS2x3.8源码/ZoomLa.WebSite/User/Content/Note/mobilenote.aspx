<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mobilenote.aspx.cs" Inherits="mobilenote" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>项目管理</title>
<meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0, maximum-scale=1.0, user-scalable=no"/> 
<link href="mobile_note.css" rel="stylesheet" type="text/css" />
<style>
body { background:#f0f0f0; }
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div ng-app="app">
<div ng-controller="NoteCtrl">
<div class="mnote_head">
    <div class="col-sm-2 col-xs-2 mnote_head_back"><a href="mobilelist.aspx"><i class="fa fa-chevron-left"></i></a></div>
    <div class="col-sm-8 col-xs-8 text-center">
        <a class="headbtn active" style="border-right:none;border-radius:5px 0 0 5px">编辑</a><a class="headbtn" href="javascript:;" onclick="note.save()" style="border-radius:0 5px 5px 0">预览</a>
    </div>
    <div class="col-sm-2 col-xs-2">
        <button type="button" class="btn btn-primary" onclick="note.save()">保存</button>
        <asp:Button runat="server" style="display:none;" OnClick="Save_Btn_Click" ID="Save_Btn" Text="保存" />
    </div>
</div>
<div class="mnote_top">
    <div class="mnote_top_c">
        <div class="mnote_top_t" ng-style="{'background':'url({{comMod.topimg}}) center no-repeat','background-size':'cover'}"><div ng-if="comMod.topimg==''" class="mnote_top_ti"><i class="fa fa-image"></i><br />背景图片</div></div>
        <div class="mnote_top_b"><input type="text" class="form-control" placeholder="添加标题" ng-model="comMod.title" /></div>
    </div>
</div><!--mnote_top end-->
<div class="mnote_content">
        <div class="con_item" ng-repeat="item in comMod.comlist |orderBy: 'orderID'" ng-switch="item.type">
        <div ng-switch-when="text" class="com com_text">
            <textarea id="{{item.id}}_text" class="form-control" maxlength="5000" rows="3" ng-model="item.content" placeholder="在这儿添加文字......"></textarea>
        </div>
        <div ng-switch-when="image" class="com com_image">
            <div class="com_image_content" ng-click="selimg(item)">
                <img id="{{item.id}}_img" ng-src="{{item.content}}" />
            </div>
        </div>
        <div ng-switch-when="video" ng-switch="item.videoType" class="com com_video">
            <div ng-switch-when="video" ng-bind-html="note.Video.getvideo(item)|html"></div>
            <div ng-switch-when="online" ng-bind-html="note.Video.getonline(item)|html"></div>
        </div>
        <div ng-switch-when="para" class="com com_para">
            <div id="{{item.id}}_div" class="paralist_item {{item.content}}">
                <img ng-src="/user/content/note/res/{{item.content}}.gif" />
                <input type="text" class="para_title" ng-model="item.title" placeholder="请输入标题" />
            </div>
        </div>
        <div class="remove_div"><a href="javascript:;" ng-click="note.delcom(item)" title="移除"><i class="fa fa-trash"></i></a></div>
        </div>
    </div>
<div class="mnote_tools" id="mnote_tools">
    <ul>
        <li><a ng-click="note.preMobilecom('img');"><i class="fa fa-image"></i>媒体</a></li>
        <li><a href="javascript:;" style="color:#ddd;"><i class="fa fa-camera"></i>照相机</a></li>
        <li><a ng-click="note.preMobilecom('text');"><i class="fa fa-align-left"></i>文本</a></li>
        <li><a ng-click="note.preMobilecom('para');"><i class="fa fa-text-width"></i>段落</a></li>
        <li><a href="javascript:;" style="color:#ddd;"><i class="fa fa-ellipsis-h"></i>更多</a></li>
        <li class="clearfix"></li>
    </ul>
    <div class="clearfix"></div>
</div>
</div>
</div>
<div id="paralist" class="paralist_div">
    <ul class="list-unstyled paralist_ul">
        <li>
            <img data-class="t1" src="/user/content/note/res/ps1.gif" />
        </li>
        <li>
            <img data-class="t2"src="/user/content/note/res/ps2.gif" />
        </li>
        <li>
            <img data-class="t3" src="/user/content/note/res/ps3.gif" />
        </li>
        <li>
            <img data-class="t4" src="/user/content/note/res/ps4.gif" />
        </li>
        <li>
            <img data-class="t5" src="/user/content/note/res/ps5.gif" />
        </li>
    </ul>
</div>
<div class="hidden">
    <input type="file" id="img_top_up" accept="image/*" onchange="settopimg(this)" />
    <input type="file" id="img_up" accept="image/*" onchange="setimg(this);" />
</div>

<asp:HiddenField ID="Save_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<script src="/JS/Mobile/ResizeImg/mobileFix.mini.js"></script>
<script src="/JS/Mobile/ResizeImg/exif.js"></script>
<script src="/JS/Mobile/ResizeImg/lrz.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="note.js"></script>
<script>
    var scope = null;
    var cur_commod = null;
    angular.module("app", []).controller("NoteCtrl", function ($scope) {
        scope = $scope;
        $scope.note = note;
        $scope.readysave = false;
        $scope.comMod = { id: 0, topimg: "", pic: "", title: "", mp3: "", comlist: [] };
        if ($("#Save_Hid").val() != "") {
            $scope.comMod = JSON.parse($("#Save_Hid").val());
        }
        $scope.selimg = function (item) {
            cur_commod = item;
            $("#img_up").click();
        }
        //note.Text.add();
        //note.Img.add("");
        //note.preMobilecom("text");
    }).filter("html", ["$sce", function ($sce) {
        return function (text) { return $sce.trustAsHtml(text); }
    }])
    note.preMobilecom = function (type) {
        //scope.comMod.title = "测试项目";
        var id = note.createID();
        switch (type) {
            case "text":
                note.Text.add();
                break;
            case "para":
                $("#paralist").show();
                window.event.stopPropagation();
                //note.Para.add("");
                break;
            case "img":
                $("#img_up").click();
                //note.Img.add("");
                break;
            default:
                break;
        }
    }
    $(function () {
        //隐藏选择段落
        $('body').click(function () { $("#paralist").hide(); });
        $("#paralist img").click(function () {
            $obj = $(this);
            var id = note.createID();
            scope.$apply(function () {
                note.Para.add("", $obj.data('class'));
            })
            $("#" + id + "_div img").attr('src', "/user/content/note/res/" + $(this).data('class') + ".gif");
            location.href = "#mnote_tools";
        });
        //选择背景图片
        $(".mnote_top_t").click(function () {
            $("#img_top_up").click();
        });
    })
    //背景图
    function settopimg(obj) {
        scope.hasedit = true;
        var fname = $("#img_top_up").val();
        if (!SFileUP.isWebImg(fname)) { alert("请选择图片文件"); return false; }
        lrz(obj.files[0], { width: 400, quality: 0.7 }, function (results) {
            SFileUP.AjaxUpBase64(results.base64, function (data) {
                scope.comMod.topimg = data;
                scope.$digest();
                $(".mnote_top_t").css("background-image", "url(" + scope.comMod.topimg + ")");
            });
        });
        //SFileUP.AjaxUpFile("img_top_up", function (data) {
        //    scope.comMod.topimg = data;
        //    scope.$digest();
        //    $(".mnote_top_t").css("background-image", "url(" + scope.comMod.topimg + ")");
        //});
    }
    function setimg(obj) {
        scope.hasedit = true;
        var fname = $("#img_up").val();
        if (!SFileUP.isWebImg(fname)) { alert("请选择图片文件"); return false; }
        lrz(obj.files[0], { width: 400, quality:0.7 }, function (results) {
            //上传base64
            SFileUP.AjaxUpBase64(results.base64, function (data) {
                if (cur_commod) { cur_commod.content = data; }
                else {
                    scope.$apply(function () {
                        note.Img.add(data); 
                    });

                    location.href = "#mnote_tools";
                }
                //scope.$digest();
                cur_commod = null;
            });
        });
        //SFileUP.AjaxUpFile("img_up", function (data) {
        //    if (cur_commod) { cur_commod.content = data; }
        //    else { note.Img.add(data); }
        //    scope.$digest();
        //    cur_commod = null;
        //});
    }
</script>
</asp:Content>