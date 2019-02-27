<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Editor.aspx.cs" Inherits="Design_Editor_Editor" EnableViewState="false" %><!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<asp:Literal runat="server" ID="Meta_L" EnableViewState="false"></asp:Literal>
<link rel="stylesheet" href="/JS/JqueryUI/jquery.ui.resizable.css"/>
<link rel="stylesheet" href="/dist/css/bootstrap.min.css"/>
<link rel="stylesheet" href="/dist/css/font-awesome.min.css"/>
<link rel="stylesheet" href="/Design/JS/Plugs/jqueryUI/css/custom-theme/jquery-ui-1.10.0.custom.css"/>
<link rel="stylesheet" href="/Design/res/css/comp.css" />
<link rel="stylesheet" href="/Design/res/css/design.css" />

<script src="/JS/jquery-1.11.1.min.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/dist/js/bootstrap.min.js"></script>

<script src="/Design/JS/Plugs/jqueryUI/jquery-ui-1.9.2.custom.min.js"></script>
<script src="/Design/JS/Plugs/menu/bootstrap-contextmenu.js"></script>
<script src="/Design/JS/Plugs/covervid.js"></script>

<script src="/JS/jquery-ui.min.js"></script>
<asp:Literal runat="server" ID="Resource_L" EnableViewState="false"></asp:Literal>
<title><asp:Literal runat="server" ID="Title_L" EnableViewState="false"></asp:Literal></title>
</head>
<body>
    <form id="form1" runat="server">
        <div ng-app="app" class="editor">
            <div id="editorBody" ng-controller="appController">
                <div id="midBody" class="compbody container" style="position: relative; padding: 0px;">
                    <div class="designLine"></div>
                    <div class="designLine" style="margin-left: 1170px;"></div>
                </div>
                <div id="mainBody" class="compbody"></div>
            </div>
        </div>
        <div id="tools">
            <div id="compinfo_div" class="drag_alignLine"></div>
            <div id="dragLine_top" class="drag_alignLine" style="width: 100%; border-bottom:1px solid #0094ff;"></div>
            <div id="dragLine_left" class="drag_alignLine" style="width: 1px; border-left:1px solid #0094ff;"></div>
            <div id="dragLine_right" class="drag_alignLine" style="width: 1px; border-right:1px solid #0094ff;"></div>
            <div id="dragLine_bottom" class="drag_alignLine" style="width: 100%; border-bottom:1px solid #0094ff;"></div>
            <div id="context-menu">
                <ul id="context-ul" class="dropdown-menu" role="menu">
                    <li data-cmd="edit"><a><i class="fa fa-pencil"></i> 修改</a></li>
   <%--             <li data-cmd="cut"><a><i class="fa fa-cut"></i> 剪切</a></li>
                    <li data-cmd="paste"><a><i class="fa fa-paste"></i> 粘贴</a></li>--%>
                    <li data-cmd="copy"><a><i class="fa fa-copy"></i> 复制</a></li>
                    <li class="divider"></li>
                    <li data-cmd="common"><a tabindex="-1"><i class="fa fa-th"></i> 通用属性</a></li>
                    <li class="divider" style="height:1px;"></li>
                    <li data-cmd="remove"><a><i class="fa fa-trash"></i> 移除</a></li>
                </ul>
            </div>
            <div id="toolsmenu_div" class="btn-group" style="position: absolute;display:none;">
                <a href="javascript:;" class="btn btn-default" title="增加层级" onclick="tools.up();"><i class="fa fa-arrow-up"></i></a>
                <a href="javascript:;" class="btn btn-default" title="降低导级" onclick="tools.down();"><i class="fa fa-arrow-down"></i></a>
                <a href="javascript:;" class="btn btn-default" title="绑定行为" onclick="tools.bind();"><i class="fa fa-link"></i></a>
                <a href="javascript:;" class="btn btn-danger"  title="移除组件" onclick="tools.del();"><i class="fa fa-trash"></i></a>
            </div>
            <div id="diagBody">
                <iframe id="diagIfr" style="border:none;width:100%;min-height:350px;"></iframe>
            </div>
        </div>
        <script>
            //------------------------
            var editor = { app: null, scope: null, compile: null, diag: null };
            editor.ShowDiag = function (url, diagParam) {
                diag = editor.diag;
                if (!diagParam || diagParam == "") { diagParam = { autoOpen: true, width: 700, height: 500 }; }
                $("#diagIfr").height(1);//修复长度过长Bug
                $("#diagIfr").attr("src", url);
                //$("#diagIfr").height(diagParam.height + "px");
                $("#diagIfr").unbind();
                $("#diagIfr").load(function () {
                    $("#diagIfr").height($("#diagIfr").contents().height());
                    diag.dialog(diagParam);
                });
                diag.dialog(diagParam);
            }
            editor.app = angular.module("app", [], function ($compileProvider) { })
                .controller("appController", function ($scope, $compile) {
                    editor.scope = $scope;
                    $scope.list = {};
                    //添加前检测同名元素,有同名元素存在且不为null,则取消添加
                    $scope.addDom = function (compObj) {
                        if ($scope.list[compObj.id]) { console.log("[" + compObj.id + "]取消添加,有重名元素存在"); return; }
                        $scope.list[compObj.id] = compObj;
                        var html = compObj.AnalyToHtml({ mode: "design" });
                        var template = angular.element(html);
                        compObj.SetInstance($compile(template)($scope), document);
                        //-----确定加入哪一个body中,默认居中
                        var bodyid = "midBody";
                        if (compObj.config.bodyid && compObj.config.bodyid != "") { bodyid = compObj.config.bodyid; }
                        //-----
                        angular.element(document.getElementById(bodyid)).append(compObj.instance);
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
                editor.diag = $('#diagBody').dialog({
                    autoOpen: false,
                    width: 500,
                    //buttons: { "确定": function () { $(this).dialog("close"); }, "关闭": function () { $(this).dialog("close"); } }
                });
            });
            //------------------------
            var alignLine = { domHeight: 0, width: 0, height: 0 };
            function EventBind() {
                $(".compbody a").each(function () { $(this).click(function () { window.event.returnValue = false; }); });
                $(".comp_wrap").unbind("click");
                $(".candrag,.onlydrag").draggable({
                    scroll: true,
                    iframeFix: true,
                    start: function (e, ui) { tools.clear(); alignLine.domHeight = $(window).height() + $(window).scrollTop(); alignLine.width = $(e.target).width(); alignLine.height = $(e.target).height() },
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
                        $('#compinfo_div').css("left", x);
                        $("#compinfo_div").css("top", (y - 25));
                        $("#compinfo_div").html("X:" + Convert.ToInt(x) + ",Y:" + Convert.ToInt(y));
                    },
                    stop: function (e, ui) { $(e.target).click(); $('.drag_alignLine').css("display", "none"); }
                });
                $(".dragy").draggable({ axis: "y" });
                $(".candrag,.onlyresize").resizable({
                    handles: "all",
                    resize: function (e, ui) {
                        var x = ui.position.left;
                        var y = ui.position.top;
                        $('#compinfo_div').css("left", x);
                        $("#compinfo_div").css("top", (y - 25));
                        $("#compinfo_div").html("W:" + Convert.ToInt(ui.size.width) + ",H:" + Convert.ToInt(ui.size.height));
                        if (tools.comp.resize) { tools.comp.resize({ width: ui.size.width, height: ui.size.height }); }
                    },
                    start: function (e, ui) { $('#compinfo_div').show(); },
                    stop: function (e, ui) {
                        $(ui).click();
                        $('#compinfo_div').hide();
                    }
                });

                //单出后增加边框,绑定事件
                $(".comp_wrap").click(function (e) {
                    var $obj = $(this);
                    $(".comp_wrap").removeClass("active");
                    $obj.addClass("active");
                    //通知更新右侧边框
                    var comp = parent.page.compList.GetByID($obj.attr("id"));
                    if (comp) { comp.UpdateRootPanel(); }
                    //显示Tools(如果支持的话)
                    tools.comp = comp;
                    tools.menu.show();
                    tools.menu.css("top", $obj.offset().top - 35).css("left", $obj.offset().left);
                    //阻止冒泡
                    var e = event || window.event;
                    if (e && e.stopPropagation) { e.stopPropagation(); }
                    else { e.cancelBubble = true; }
                    return false;
                });
                //每个控件自实现自己的菜单,并写好功能
                //未选中控件则为当前page,否则为控件
                $(".comp_wrap").contextmenu({
                    target: "#context-menu",//根据不同元素,载入不同html后再加载右键
                    before: function (e, context) {
                        //e.preventDefault();
                        //var id = context[0].id;
                        //var comp = parent.page.compList.GetByID(id);
                        //console.log(comp.config.type);
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
                            var labelid=$dom.attr("labelid");
                            if (labelid && labelid!="") {
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
                                editor.ShowDiag("/Design/Diag/Common/Edit.aspx?id=" + id);
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
                editor.diag.dialog("close");
                
            }
            //-------------------------浮动工具栏菜单
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
                tools.comp.instance.remove();
                tools.comp.RemoveSelf(parent.page.compList);
                tools.clear();
            }
            tools.clear = function () {
                tools.comp = null;
                tools.menu.hide();
            }
        </script>
    </form>
</body>
</html>
