<%@ Page Language="C#" MasterPageFile="~/Common/Master/Empty.master" AutoEventWireup="true" CodeBehind="note.aspx.cs" Inherits="ZoomLaCMS.Plat.Note.note" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>项目管理</title>
    <style>
    .addon table tr{margin:5px;}
    .addon table tr td{padding:5px;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div ng-app="app">
<div ng-controller="NoteCtrl">
<div class="note">
<div class="note-head">
<div class="container">
<div class="row">
<div class="col-lg-8 col-md-8 note-head-l">
<div class="note-head-ls"><a href="/Plat/"><i class="fa fa-home fa-2x"></i></a><span>项目管理</span></div>
<div class="cp_num">
<i ng-repeat="item in complete.nums" ng-class="complete.getclass(item);"></i>
<strong ng-bind="complete.getcomplete()+'%'"></strong>
</div>
<a href="javascript:;" onclick="setProgress();" title="查看详情" class="note-head-ld">完成度详情 <i class="fa fa-chevron-down"></i></a>
</div>
<div class="col-lg-4 col-md-4 note-head-r">
<button type="button" class="btn btn-warning" onclick="note.save();">预览</button>
<a href="/Plat/Note/ProList.aspx" class="btn btn-link">项目列表</a>
</div>
</div>
</div>
</div>
<div class="h81"></div>
<div class="note-topimg" id="note-topimg" ng-style="{'background':'url({{comMod.topimg==''?'res/page_bg.jpg':comMod.topimg}}) center no-repeat','background-size':'cover'}">
<div class="container">
<div class="note-topimg-c">
<div class="note_topimg_btns" ng-hide="comMod.topimg!=''">
<div class="pull-left"><a href="javascript:;" onclick="selTopimg();"></a></div>
<div class="pull-left">
<div class="h3">设置项目图片</div>
<div class="h5">图片建议选择尺寸大于1680px的高清大图，如相机原图</div>
</div>
<div class="clearfix"></div>
</div>
<div class="set_title">
<div class="set_btn" ng-hide="comMod.topimg==''">
<a class="a_set" title="设置头图"><i class="fa fa-cog"></i><span>设置头图</span></a>
<ul>
<li><a title="重新上传头图" onclick="selTopimg();"><i class="fa fa-image"></i><span>重新上传头图</span></a></li>
</ul>
</div>
<input type="text" class="form-control" ng-model="comMod.title" placeholder="填写项目标题" maxlength="48" />
</div>
</div>
</div>
</div>
<div class="note-content container" id="note-content">
<%--发起人，参与人，失效时间，评论--%>
<div class="addon" style="padding:10px;box-shadow:rgba(0,0,0,0.08) 0px 2px 5px 0px,rgba(0,0,0,0.08) 0px 2px 10px 0px;margin-bottom:25px;">
    <table>
        <tr>
            <td>发起人</td><td><input type="text" class="form-control text_300" ng-model="cuser" disabled /></td></tr>
        <tr>
            <td>参与人</td>
            <td>
                <div class="input-group" style="width:401px;"><input type="text" id="UserName_T" class="form-control text_300" ng-model="comMod.ParticNames" />
                <span class="input-group-btn"><input type="button" class="btn btn-info" style="width:101px;" value="选择用户" onclick="SelUser();" /></span></div>
            </td>
        </tr>
        <tr>
            <td>失效时间</td><td><input type="text" id="EDate_T" class="form-control text_300" ng-model="comMod.EDate" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' });" /></td></tr>
    </table>
</div>


<div class="con-item" ng-repeat="item in comMod.comlist |orderBy: 'orderID'" ng-switch="item.type">
<div class="toolbar">
<div ng-show="bar.active!=item.id" class="toolbar_c">
<a href="javascript:;"><i class="fa fa-close bar-btn" ng-click="bar.toggle(item);"></i></a>
<a href="javascript:;" ng-click="note.showText(item);"><i class="fa fa-text-width bar-btn"></i>添加文字</a>
<a href="javascript:;" onclick="note.preAddcom('image');"><i class="fa fa-image bar-btn"></i>添加图片</a>
<a href="javascript:;" onclick="showUPVideo(this);"><i class="fa fa-video-camera bar-btn"></i>添加视频</a>
<a href="javascript:;" onclick="note.preAddcom('para');"><i class="fa fa-navicon bar-btn"></i>添加段落</a>
<a href="javascript:;" ng-click="note.delcom(item);" ng-if="$index!=0"><i class="fa fa-remove bar-btn"></i>移除节点</a>
</div>
<div ng-show="bar.active==item.id" class="toolbar_c">
<a href="javascript:;"><i class="fa fa-plus bar-btn" ng-click="bar.toggle(item);"></i></a>
</div>
</div>
<div class="com-text" ng-show="item.openText">
<textarea id="{{item.id}}_text" maxlength="5000" ng-model="item.text" placeholder="在这儿添加文字......"></textarea>
<div class="margin-top">
<i class="fa fa-smile-o fabtn" title="选择表情" onclick="selemotion(this);" ng-click="setcur(item);"></i>
<input type="button" value="取消" class="btn btn-info" ng-click="note.hideText(item);" />
</div>
</div>
<div ng-switch-when="image" class="com com-image">
<div class="com-img_item"><img ng-src="{{item.content}}" class="com-img_img"></div>
</div>
<div ng-switch-when="video" ng-switch="item.videoType" class="con-item com com-video">
<div ng-switch-when="video" ng-bind-html="note.Video.getvideo(item)|html"></div>
<div ng-switch-when="online" ng-bind-html="note.Video.getonline(item)|html"></div>
</div>
<div ng-switch-when="para" class="con-item com com-para">
<h2 class="{{'para '+item.content}}">
<span ng-bind="item.title" class="pull-left para_title"></span>
<span class="pull-left com_para_btns">
<i class="fa fa-pencil" title="修改段落" ng-click="note.Para.edit(item);"></i>
<i class="fa fa-trash" title="删除段落" ng-click="note.delcom(item);"></i>
</span>
</h2>
</div>
</div>
</div>
<div class="note-music container">
<h4>音乐</h4>
<div class="addmusic">
<%--        <span ng-show="comMod.mp3==''">背景音乐请选择后缀为.mp3的音乐文件</span>
<span ng-show="comMod.mp3!=''"><span>已上传 - {{getfname(comMod.mp3)}}</span><a href="javascript:;" ng-click="delmp3();">删除音乐</a></span>
<input type="button" class="btn btn-info" value="浏览" ng-click="selmp3();" />--%>
<span>背景音乐请选择后缀为.mp3的音乐文件</span><input type="text" class="form-control wid600" ng-model="comMod.mp3" placeholder="请输入音乐链接" />
</div>
</div>
<div class="container margin_t20 margin_b20">
<%-- <asp:LinkButton runat="server" CssClass="btn_draft" OnClick="SaveToDraft_Btn_Click" ID="LinkButton1" OnClientClick="return note.preSave();" title="保存草稿">保存草稿</asp:LinkButton>--%>
<div class="pull-right">
<input type="button" value="预览" class="btn btn-default" style="width: 100px;" onclick="note.save();" />
<input type="button" value="发布项目" class="btn btn-info" onclick="note.save();" />
</div>
</div>
</div>
<div class="sortbox" ng-show="comMod.comlist.length>1">
<a title="展开" class="pu_items pu_open" onclick="drag.open();"><i class="fa fa-chevron-left"></i><strong>顺序编辑器</strong> <span>项目文字及照片</span></a>
<ul class="sortul list-unstyled">
<li ng-repeat="item in comMod.comlist | orderBy: 'orderID'"><img ng-src="{{getImgByType(item)}}" /></li>
</ul>
<div id="sortlist_box" class="drag_up" style="display:none;"  ng-show="comMod.comlist.length>1">
<div class="pu_items pu_close" onclick="drag.close();"><i class="fa  fa-chevron-right"></i></div>    
<div id="drag_main" class="drag_main">
<div class="du_top clearfix"><strong>拖拽调整照片或文字顺序</strong></div>
<iframe id="sort_ifr" src="diag/Sort.aspx" style="width:100%;height:500px;border:none;overflow:hidden;"></iframe>
</div>
</div>
<div id="rise_down" class="rise_down" style="display:none;">
<a class="go_rise" title="返回顶部" onclick="Control.Scroll.ToTop();"><i class="fa fa-chevron-up"></i>顶部</a>
<a class="go_down" title="返回底部"  onclick="Control.Scroll.ToBottom();">底部<i class="fa fa-chevron-down"></i></a>
</div>
</div>
<div class="hidden">
<div class="tool-upvideo" id="upvideo_div" style="position: absolute;">
<a href="javascript:;" onclick="hideUPVideo();" class="upvideo-close" title="关闭"><i class="fa fa-remove"></i></a>
<div class="upvideo-head">
<a href="javascript:;" data-pane="upvideo-file" class="vt_btn active"><i class="fa fa-upload"></i>本地上传</a>
<a href="javascript:;" data-pane="upvideo-online" class="vt_btn"><i class="fa fa-toggle-right"></i>在线视频</a>
<div class="clearfix"></div>
</div>
<div class="vt-pane upvideo-file">
<%-- <input type="button" value="上传视频" style="width: 160px;" class="btn btn-info btn-lg" onclick="note.preAddcom('video');" />
<div style="color: #666; margin-top: 20px;">请上传200M以内的视频</div>--%>
<div style="color: #666; margin-top: 20px;">请复制粘贴网站视频源文件地址</div>
<input type="text" id="video_local_t" class="form-control margin-top" placeholder="请输入网站视频链接" />
<input type="button" class="margin-top btn btn-info pull-right" value="确定" onclick="note.preAddcom('video');" />
</div>
<div class="vt-pane upvideo-online">
<div style="color: #666; margin-top: 20px;">请复制粘贴视频源文件地址,swf结尾</div>
<input type="text" id="video_online_t" class="form-control margin-top" placeholder="请输入在线视频链接" />
<input type="button" class="margin-top btn btn-info pull-right" value="确定" onclick="note.preAddcom('online');" />
</div>
</div>
<audio id="mp3_audio" autoplay></audio>
<asp:HiddenField runat="server" ID="Save_Hid" />
<asp:HiddenField runat="server" ID="CUser_Hid" />
<asp:Button runat="server" ID="Save_Btn" OnClick="Save_Btn_Click" />
<input type="file" id="mp3_file" onchange="setmp3();" accept="audio/mp3" />
<input type="file" id="topimg_file" onchange="setTopimg();" accept="image/*" />
</div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link href="/Plugins/Ueditor/third-party/video-js/video-js.min.css" rel="stylesheet" />
<link href="note.css" rel="stylesheet" />
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Controls/Control.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/Plugins/Ueditor/third-party/video-js/video.js"></script>
<script src="note.js"></script>
<script>
    var scope = null, $upvideo = null, $emotion = $('<iframe src="/Plugins/Ueditor/dialogs/emotion/ImgFace.html" class="emotion_ifr" id="emotion_ifr"></iframe>');
    angular.module("app", [])
    .controller("NoteCtrl", function ($scope, $compile) {
        scope = $scope;
        $scope.note = note;
        $scope.hasedit = false; $scope.readysave = false;
        $scope.comMod = { id: 0, topimg: "", pic: "", title: "", mp3: "", comlist: [], ParticIDS: "", EDate: "<%=DateTime.Now.AddMonths(1).ToString("yyyy/MM/dd HH:mm")%>" };
        $scope.cuser = "";
        if ($("#Save_Hid").val() != "") { $scope.comMod = JSON.parse($("#Save_Hid").val()); }
        if ($("#CUser_Hid").val() != "") { $scope.cuser = $("#CUser_Hid").val(); }
        if ($scope.comMod.comlist.length < 1) { $scope.note.Empty.add(); }
        //----------------
        $scope.getfname = function (name) { var start = name.lastIndexOf("/") + 1; return name.substring(start, name.length - 1); }
        $scope.selmp3 = function () { $("#mp3_file").click(); }
        $scope.delmp3 = function () { $scope.comMod.mp3 = ""; $("#mp3_audio").attr("src", ""); }
        //----------------
        $scope.setcur = function (item) {
            $scope.curitem = item;
        }
        //边栏图片
        $scope.getImgByType = function (item) {
            switch (item.type) {
                case "":
                    return "/plat/note/res/word_v2.gif";
                case "image":
                    return item.content;
                case "video":
                    return "/plat/note/res/video_v2.gif";
                case "para":
                    return "/plat/note/res/para_v2.gif";
                default:
                    return item.type;
            }
        }
        /*complete*/
        $scope.complete = {};
        $scope.complete.nums = [10, 20, 30, 40, 50, 60, 70, 80, 90, 100];
        //$scope.complete.index = 20;
        $scope.complete.getclass = function (val) {
            var index = $scope.complete.getcomplete();
            index = parseInt(index / 10) * 10;
            if (val < index) { return "completed"; }
            else if (val > index) { return ""; }
            else if (val == index) { return "completed on"; }
        }
        $scope.complete.getcomplete = function () {
            var index = 20;
            if ($scope.comMod.topimg != "") { index += 15; }
            if ($scope.comMod.mp3 != "") { index += 10; }
            //if($scope.comMod.)//设置封面
            if ($scope.comMod.comlist && $scope.comMod.comlist.length > 0) {
                if ($scope.comMod.comlist.GetByID("image", "type")) { index += 15; index += 10; }
                if ($scope.comMod.comlist.GetByID("para", "type")) { index += 15; }
                if ($scope.comMod.comlist.GetByID("video", "type")) { index += 15; }
            }
            //$scope.complete.index = index;
            return index;
        }

        /*bar*/
        $scope.bar = {};
        $scope.bar.active = "";
        $scope.bar.toggle = function (item) {
            if (item.id == $scope.bar.active) { $scope.bar.active = ""; }
            else { $scope.bar.active = item.id; }
            //$scope.$digest();
        }
    })
    .filter("html", ["$sce", function ($sce) {
        return function (text) { return $sce.trustAsHtml(text); }
    }])
    var selemotion = function (obj) {
        var $obj = $(obj);
        //如果在当前选定中已打开,则再点击则关闭
        var $em = $obj.parent().find(".emotion_ifr:visible");
        if ($em.length > 0) { $emotion.hide(); }
        else { $obj.after($emotion); $emotion.show(); }
    }
    //--选择参与人
    var TemDiag = new ZL_Dialog();
    function SelUser() {
        TemDiag.title = "选择用户";
        TemDiag.maxbtn = false;
        TemDiag.url = "/Common/Dialog/SelGroup.aspx";
        TemDiag.ShowModal();
    }
    function UserFunc(list, select) {
        var names = "";
        var ids = "";
        for (var i = 0; i < list.length; i++) {
            names += list[i].UserName + ",";
            ids += list[i].UserID + ",";
        }
        $("#UserName_T").val(names);
        scope.comMod.ParticIDS = ids;
        TemDiag.CloseModal()
    }
    //--------------------------
    //WUFile {name: "z_logo.jpg", size: 12309, type: "image/jpeg", lastModifiedDate: Fri Jan 22 2016 14:52:22 GMT+0800 (中国标准时间), id: "WU_FILE_1"…}
    //note.aspx:121 Object {_raw: "/UploadFiles/Admin/admin1/20160129/z_logo.jpg"}
    //note.aspx:121 undefined
    function showUPVideo(obj) { $(obj).after($upvideo); $upvideo.show(); }
    function hideUPVideo() { $upvideo.hide(); }
    $(function () {
        //------视频
        $upvideo = $("#upvideo_div");
        $("#upvideo_div").remove();
        $upvideo.find(".vt_btn").click(function () {
            $(".vt_btn").removeClass("active");
            $(this).addClass("active");
            $(".vt-pane").hide();
            $("." + $(this).data("pane")).show();
        });
        note.autoSave();
        /*右侧排序*/
        $(window).scroll(function (event) {
            var top = $(window).scrollTop();
            if (top > 300) { $("#rise_down").fadeIn(800); }
            else { $("#rise_down").fadeOut(800); }
        });
        /*TopImg*/
        $(".set_btn").hover(function () { $(this).addClass("on"); }, function () { $(this).removeClass("on"); });
    })
    function AddAttach(file, ret, pval) {
        switch (pval.type) {
            case "img":
                note.Img.add(ret._raw);
                break;
            case "video":
                note.Video.add("video", ret._raw);
                hideUPVideo();
                break;
            case "sort":
                note.Img.sort(ret._raw);
                break;
        }
        scope.$digest();
    }
    /*-------TopImg-------*/
    function selTopimg() { $("#topimg_file").click(); }
    function setTopimg() {
        scope.hasedit = true;
        var fname = $("#topimg_file").val();
        if (!SFileUP.isWebImg(fname)) { alert("请选择图片文件"); return false; }
        SFileUP.AjaxUpFile("topimg_file", function (data) {
            //showCut(data);
            scope.comMod.topimg = data;
            scope.$digest();
            $("#note-topimg").css("background-image", "url(" + scope.comMod.topimg + ")");
        });
    }
    function cutTopimg() {
        $(".modal_ifr")[0].contentWindow.cutpic();
    }
    function cutok(url) {
        scope.comMod.topimg = url;
        $("#note-topimg").css("background-image", "url(" + scope.comMod.topimg + ")");
        scope.$digest();
        closeDiag();
    }
    function setmp3() {
        var fname = $("#mp3_file").val();
        if (!fname || fname == "") { return false; }
        fname = fname.toLowerCase();
        if (fname.indexOf(".mp3") < 0) { alert("只能上传mp3文件"); return false; }
        SFileUP.AjaxUpFile("mp3_file", function (data) {
            scope.comMod.mp3 = data;
            $("#mp3_audio").attr("src", data); scope.$digest();
        });
    }
    function InsertSmiley(data) {
        scope.curitem.text += data.title;
        scope.$digest();
    }
    window.onbeforeunload = function () {
        if (scope.hasedit && scope.readysave == false) {
            return "你正在编辑的项目未保存草稿!";
        }
    }
    /*-------对话框-------*/
    var diag = new ZL_Dialog();
    diag.maxbtn = false;
    diag.backdrop = true;
    diag.reload = true;
    function showPara(id) {
        diag.title = "选择段落";
        diag.reload = true;
        diag.width = "paragraph_diag";
        diag.url = "/plat/note/diag/addparagraph.aspx";
        if (id) { diag.url += "?id=" + id; }
        diag.ShowModal();
    }
    function showCut(url) {
        if (!url) { url = scope.comMod.topimg; }
        diag.url = "diag/cutpic.aspx?ipath=" + url;
        diag.title = '图片裁剪 <input type="button" class="btn btn-info margin-r" value="确定" onclick="cutTopimg();" />';
        diag.width = "widlg";
        diag.ShowModal();
    }
    function setProgress() {
        diag.width = "wid600";
        diag.reload = true; diag.title = "完成度详情"; diag.url = "diag/Progress.aspx?ProID=" + scope.comMod.id; diag.ShowModal();
    }
    function closeDiag() {
        diag.CloseModal();
    }
    /*拉动窗*/
    var drag = {};
    drag.open = function () {
        $("#sortlist_box").show();
        $("#sortlist_box").removeClass("drag_div_close");
        $("#sortlist_box").addClass("drag_div_open");
        $("#sort_ifr")[0].contentWindow.update();
    }
    drag.close = function () {
        $("#sortlist_box").removeClass("drag_div_open");
        $("#sortlist_box").addClass("drag_div_close");
        setTimeout(function () { $("#sortlist_box").hide(); }, 800);
    }
</script>
</asp:Content>