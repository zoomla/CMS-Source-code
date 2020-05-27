<%@ Page Language="C#" AutoEventWireup="true" CodeFile="editor.aspx.cs" Inherits="Design_Editor_scence" MasterPageFile="~/Common/Master/Empty.master" EnableViewState="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<asp:Literal runat="server" ID="Meta_L" EnableViewState="false"></asp:Literal>
<link rel="stylesheet" href="/JS/JqueryUI/jquery.ui.resizable.css"/>
<link rel="stylesheet" href="/design/res/css/comp.css" />
<link rel="stylesheet" href="/design/res/css/se_design.css"/>
<link rel="stylesheet" href="/design/h5/css/swiper.min.css">
<link rel="stylesheet" href="/design/h5/css/animate.min.css">
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/design/JS/Plugs/jqueryUI/jquery-ui-1.9.2.custom.min.js"></script>
<script src="/design/JS/Plugs/menu/bootstrap-contextmenu.js"></script>
<script src="/design/h5/js/swiper.min.js"></script>
<script src="/design/h5/js/swiper.animate.min.js"></script>
<asp:Literal runat="server" ID="Resource_L" EnableViewState="false"></asp:Literal>
<title><asp:Literal runat="server" ID="Title_L" EnableViewState="false"></asp:Literal></title>
<style>
html,body,form{height:100%;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div ng-app="app" class="swiper-container editor scence">
    <div id="editorBody" class="swiper-wrapper" ng-controller="appCtrl"></div>
    <img src="/design/h5/images/arrow.png" id="array" class="resize">
    <div class="swiper-pagination"></div>
</div>
<div id="tools">
    <div id="compinfo_div" class="drag_alignLine"></div>
    <div id="dragLine_top" class="drag_alignLine" style="width: 100%; border-bottom: 1px solid #0094ff;"></div>
    <div id="dragLine_left" class="drag_alignLine" style="width: 1px; border-left: 1px solid #0094ff;"></div>
    <div id="dragLine_right" class="drag_alignLine" style="width: 1px; border-right: 1px solid #0094ff;"></div>
    <div id="dragLine_bottom" class="drag_alignLine" style="width: 100%; border-bottom: 1px solid #0094ff;"></div>
    <div id="context-menu">
        <ul id="context-ul" class="dropdown-menu" role="menu" style="font-size:28px;width:260px;">
            <li data-cmd="edit"><a><i class="fa fa-pencil"></i> 修改</a></li>
            <li class="divider"></li>
            <li data-cmd="copy"><a><i class="fa fa-copy"></i> 复制</a></li>
            <li class="divider"></li>
            <li data-cmd="animate"><a tabindex="-1"><i class="fa fa-send"></i> 动画</a></li>
            <li class="divider"></li>
            <li data-cmd="common"><a tabindex="-1"><i class="fa fa-th"></i> 通用</a></li>
            <li class="divider" style="height: 1px;"></li>
            <li data-cmd="remove"><a><i class="fa fa-trash"></i> 移除</a></li>
        </ul>
    </div>
    <div id="diy-rotate">
        <div class="bar bar_rotate bar-radius bar-s2"></div>
        <div class="bar bar-line"></div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    //------------------------
    var editor = { app: null, scope: null, compile: null, diag: null };
    editor.ShowDiag = function (url, diagParam) { top.ShowEditDiag(url, diagParam); }
    //------------------------
    editor.app = angular.module("app", [], function ($compileProvider) { })
        .controller("appCtrl", function ($scope, $compile) {
            editor.scope = $scope;
            $scope.list = {};
            //添加前检测同名元素,有同名元素存在且不为null,则取消添加
            $scope.addDom = function (compObj) {
                if ($scope.list[compObj.id]) { console.log("[" + compObj.id + "]取消添加,有重名元素存在"); return; }
                $scope.list[compObj.id] = compObj;
                var html = compObj.AnalyToHtml({ mode: "design" });
                var template = angular.element(html);
                compObj.SetInstance($compile(template)($scope), document);
                angular.element(document.getElementById(compObj.config.bodyid)).append(compObj.instance);
                $(".comp_wrap").removeClass("active");
                compObj.instance.addClass("active");
            }
            //元素需要另外清零,或元素一个指向其
            $scope.delDom = function (name) {
                if ($scope.list[name]) {
                    $scope.list[name].instance.remove();
                    delete $scope.list[name];
                }
            }
        })
        .filter("html", ["$sce", function ($sce) {
            return function (text) { return $sce.trustAsHtml(text); }
        }]);
    $(function () {
        EventBind();
        top.editor = editor;
        top.tools = tools;
        //diy_rotate.init();
        diy_key.init();

        // 元素操作
        $("#editorBody").on('click', '.comp_wrap', function (event, istrigger) {
            var $obj = $(this);
            $(".comp_wrap").removeClass("active");
            $obj.addClass("active");
            tools.comp = top.page.compList.GetByID($obj.attr("id"));

            //tools.menu.show();
            //tools.menu.css("top", $obj.offset().top - 35).css("left", $obj.offset().left);
            //diy_rotate.comp = comp;
            //diy_rotate.show();
            //阻止冒泡
            //var e = event || window.event;
            //if (e && e.stopPropagation) { e.stopPropagation(); }
            //else { e.cancelBubble = true; }
            //return false;
            event.stopPropagation();
        })
    });
    //----------------集成动画效果
    var myani = { swiper: null };
    myani.init = function () {
        myani.swiper = new Swiper('.swiper-container', {
            direction: 'vertical',
            loop: false,
            simulateTouch: false,
            pagination: '.swiper-pagination',
            onInit: function (swiper) {
                swiperAnimateCache(swiper); //隐藏动画元素 
                swiperAnimate(swiper); //初始化完成开始动画
            },
            onSlideChangeEnd: function (swiper) {
                //必须加这句,否则修改过的style无效
                swiperAnimateCache(swiper); //隐藏动画元素 
                swiperAnimate(swiper);  //每个slide切换结束时也运行当前slide动画
            }
        });
    }
    //------------------------
    var alignLine = { domHeight: 0, width: 0, height: 0 };
    function EventBind() {
        $(".compbody a").each(function () { $(this).click(function () { window.event.returnValue = false; }); });
        $(".candrag,.onlydrag").draggable({
            scroll: false,
            iframeFix: true,
            start: function (e, ui) {
                diy_rotate.clear();
                alignLine.domHeight = $(window).height() + $(window).scrollTop();
                alignLine.width = $(e.target).width();
                alignLine.height = $(e.target).height()
            },
            drag: function (e, ui) {
                var x = ui.offset.left;
                var y = ui.offset.top;
                $('#dragLine_top').css("top", y);

                $("#dragLine_bottom").css("top", y + alignLine.height);

                $('#dragLine_left').height(alignLine.domHeight);
                $('#dragLine_left').css("left", x);

                $("#dragLine_right").height(alignLine.domHeight);
                $('#dragLine_right').css("left", x + alignLine.width);
                $(".drag_alignLine").css("display", "block");
                //---在其上方显示浮动框
                $('#compinfo_div').css("left", (x + 30));
                $("#compinfo_div").css("top", (y - 35));
                $("#compinfo_div").html("X:" + Convert.ToInt(x) + ",Y:" + Convert.ToInt(y));
            },
            stop: function (e, ui) { $(e.target).click(); $('.drag_alignLine').css("display", "none"); }
        });
        $(".dragy").draggable({ axis: "y" });
        $(".candrag,.onlyresize").resizable({
            //ghost: true,
            handles: "all",
            //autoHide: true,
            //helper: "resizable-helper",
            resize: function (e, ui) {
                var x = ui.position.left;
                var y = ui.position.top;
                $('#compinfo_div').css("left", x);
                $("#compinfo_div").css("top", (y - 25));
                $("#compinfo_div").html("W:" + Convert.ToInt(ui.size.width) + ",H:" + Convert.ToInt(ui.size.height));
                if (tools.comp.resize) { tools.comp.resize({ width: ui.size.width, height: ui.size.height }); }
            },
            start: function (e, ui) { $('#compinfo_div').show(); },
            stop: function (e, ui) { $(ui).click(); $('#compinfo_div').hide(); }
        });
        //每个控件自实现自己的菜单,并写好功能
        //未选中控件则为当前page,否则为控件
        $(".comp_wrap").contextmenu({
            target: "#context-menu",//根据不同元素,载入不同html后再加载右键
            before: function (e, context) {
                //e.preventDefault();
                //var id = context[0].id;
                //var comp = parent.page.compList.GetByID(id);
                //$("#context-ul").html('<li><a tabindex="-1">' + context[0].id + '</a></li>');
            },
            onItem: function (context, e) {
                //每个控件自实现单击事件,可以建一些通用的指令,其余特殊的指令,代码自实现
                //规则<li data-cmd="refresh">sfsf</li>
                var id = context[0].id;
                var cmd = $(e.currentTarget).data("cmd");
                var comp = parent.page.compList.GetByID(id);
                //如果组件未找到,则检测是否为自我描述性的组件
                if (!comp) {
                    var $dom = $(context[0]);
                    var labelid = $dom.attr("labelid");
                    if (labelid && labelid != "") {
                        comp = {};
                        comp.editurl = "/Design/Diag/Label/Edit.aspx?LName=" + encodeURI(labelid);
                        comp.diagParam = { autoOpen: true, height: 650, width: 1100 };
                    }
                }
                switch (cmd) {
                    case "edit":
                        {
                            var url = "";
                            if (comp.editurl && comp.editurl != "") { url = comp.editurl; }
                            else { url = "/Design/Diag/" + comp.config.type + "/Edit.aspx?id=" + id; }
                            editor.ShowDiag(url, comp.diagParam);
                        }
                        break;
                    case "copy":
                        parent.CopyComp(comp);
                        break;
                    case "common":
                        editor.ShowDiag("/design/diag/common/edit.aspx?id=" + id);
                        break;
                    case "animate":
                        editor.ShowDiag("/design/diag/common/animate.aspx?id=" + id);
                        break;
                    case "remove":
                        comp.instance.remove();
                        comp.RemoveSelf(parent.page.compList);
                        break;
                    default:
                        console.log("无效命令:" + cmd);
                        break;
                }
                $("#context-menu").hide();
            }
        });
    }
    function CloseDiag() {
        top.page.diag.dialog("close");
        //editor.diag.dialog("close");
    }
    //-------------------------浮动工具栏菜单(统一读其选定的comp)
    var tools = { comp: null, menu: $("#toolsmenu_div") };
    tools.up = function () {
        var zindex = Convert.ToInt(tools.comp.instance.css("z-index"), 0);
        tools.comp.instance.css("z-index", (zindex + 1) + "");
    }
    tools.down = function () {
        var zindex = Convert.ToInt(tools.comp.instance.css("z-index"), 0);
        tools.comp.instance.css("z-index", (zindex - 1));
    }
    tools.bind = function () {
        //弹出窗口,绑定超链接或JS,或pop弹出
        editor.ShowDiag("/design/diag/BindEvent.aspx?id=" + tools.comp.id);
    }
    tools.del = function () {
        //if (!confirm("确定要移除吗?")) { return false; }
        if (!tools.comp) { return; }
        tools.comp.RemoveSelf(top.page.compList);
        tools.clear();
    }
    tools.animate = function () {
        editor.ShowDiag("/design/diag/common/animate.aspx?id=" + tools.comp.id);
    }
    tools.edit = function () {
        var url = "";
        var comp = tools.comp;
        if (comp.editurl && comp.editurl != "") { url = comp.editurl; }
        else { url = "/Design/Diag/" + comp.config.type + "/Edit.aspx?id=" + tools.comp.id; }
        editor.ShowDiag(url, comp.diagParam);
    }
    tools.common = function () { editor.ShowDiag("/design/diag/common/edit.aspx?id=" + tools.comp.id); }
    tools.clear = function () {
        tools.comp = null;
        tools.menu.hide();
    }
    //-------------------------拖动旋转(需要附加于元素内部)
    var diy_conver = {
        $body: $('<div style="position:fixed;width:100%;height:100%;z-index:500;display:none;top:0px;left:0px;overflow:hidden;"></div>'),
        show: function () { var ref = this; $("body").append(ref.$body); ref.$body.show(); },
        clear: function () { var ref = this; ref.$body.hide(); }
    };
    //与动画不兼容,需要处理
    var diy_rotate = {
        $obj: $("#diy-rotate"),
        init: function () {
            var ref = this;
            ref.$obj.find(".bar_rotate:first").mousedown(function () {
                function mouse(e) {
                    //var getTransform = function (style, name) {
                    //    if (!style || style.indexOf(name) < 0) { return ""; }
                    //    //开始获取其中的值transform: name();
                    //    var str = style.split(name + "(")[1];
                    //    str = str.substr(0, str.indexOf(")"));//第一个)
                    //    return str;
                    //}
                    //if (img.attr("style").indexOf("rotate(") > -1) {
                    //    degree = getTransform(img.attr("style"), "rotate").replace("deg", "");
                    //    if (degree && degree != "") { degree = parseFloat(degree); }
                    //}
                    //增加一个遮罩层,将事件放置在遮罩层上
                    
                    var dom = tools.comp.instance;
                    var rot = $(".bar_rotate");
                    var offset = dom.instance.offset();
                    var center_x = (offset.left) + (dom.width() / 2);//dom元素所处的x与y点
                    var center_y = (offset.top) + (dom.height() / 2);
                    var mouse_x = e.pageX, mouse_y = e.pageY;//鼠标所处位置
                    var radians = Math.atan2(mouse_x - center_x, mouse_y - center_y);//返回从 X 轴正向逆时针旋转到点 (x,y) 时经过的角度（-PI 到 PI 之间的值）
                    //Math.PI 返回圆周率π
                    var degree = (radians * (180 / Math.PI) * -1) + 180;
                    dom.css('-moz-transform', 'rotate(' + degree + 'deg)');
                    dom.css('-webkit-transform', 'rotate(' + degree + 'deg)');
                    dom.css('-o-transform', 'rotate(' + degree + 'deg)');
                    dom.css('-ms-transform', 'rotate(' + degree + 'deg)');
                    window.getSelection ? window.getSelection().removeAllRanges() : document.selection.empty();
                    return false;
                }
                //移除动画效果,否则旋转无法即时体现,移动事件绑在遮罩层上
                if (tools.comp.instance.hasClass("ani")) {
                    var css = tools.comp.instance.attr("swiper-animate-effect");
                    tools.comp.instance.removeClass("animated").removeClass(css);
                }
                diy_conver.show();
                var mouseup = function () { $(diy_conver.$body).unbind("mousemove", mouse).unbind("mouseup", mouseup); diy_conver.clear(); }
                setTimeout(function () { $(diy_conver.$body).bind("mousemove", mouse).bind("mouseup", mouseup); }, 10);
            })
        },
        show: function () {
            var ref = this;
            tools.comp.instance.append(ref.$obj);
            ref.$obj.show();
        },
        clear: function () { var ref = this; ref.$obj.hide(); }
    }
    //-------------------------键盘事件处理
    var diy_key = {};//需要点document再点元素,否则事件无法应用
    diy_key.init = function (e) {
        $(window).keydown(diy_key.fun);
    }
    diy_key.fun = function (e) {
        //返回坐标数值
        var getpos = function (name) {//top,left
            var pos = tools.comp.instance.css(name).replace("px", "").replace("em", "").replace("vm", "");
            if (pos == "auto") { pos = 0; }
            return parseInt(pos);
        }
        var changepos = function (name,num) {
            var pos = getpos(name);
            pos += num;
            tools.comp.instance.css(name, pos + "px");
        }
        if (tools.comp) {
            switch (e.keyCode) {
                case 8://back
                case 46://delete
                    tools.del();
                    break;
                case 38://上左下右
                    changepos("top", -2);
                    break;
                case 37:
                    changepos("left", -2);
                    break;
                case 40:
                    changepos("top", 2);
                    break;
                case 39:
                    changepos("left", 2);
                    break;
            }
        }
        if (e.keyCode == 8 || e.keyCode == 116 || (e.ctrlKey && e.keyCode == 82)) { return false; }
    }
</script>
</asp:Content>