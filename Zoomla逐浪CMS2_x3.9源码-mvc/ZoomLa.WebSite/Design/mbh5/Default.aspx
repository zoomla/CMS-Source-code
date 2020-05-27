<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Design.mbh5.Default"  MasterPageFile="~/Common/Master/Empty.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<script>
    var phoneWidth = parseInt(window.screen.width),phoneHeight = parseInt(window.screen.height);
    var phoneScale = phoneWidth / 640;
    var ua = navigator.userAgent;
    document.write('<meta name="viewport" content="width=640, minimum-scale = ' + phoneScale + ', maximum-scale = ' + phoneScale + '">');
</script>
<style>
</style>
<title>
    <asp:Literal runat="server" ID="Title_L" /></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div style="display: none;">
    <asp:Image ID="Wx_Img" runat="server" />
</div>
<a href="javascript:;" onclick="showmenu();" class="header back link_btn">
    <i class="fa fa-align-justify"></i>
    <ul id="titmenu" class="list-unstyled menu_vertical">
        <li class="item link_btn" onclick="location.href='https://v.z01.com/design/user/default2.aspx';">我的<br />
            场景</li>
        <li class="item link_btn" onclick="mbh5.pop.show('edit');">修改<br />
            参数</li>
    </ul>
</a>
<a href="javascript:;" class="header view link_btn" onclick="mbh5.preview();" style="width: 80px; height: 80px; padding-top: 10px;">保存<br />
    预览</a>
<a href="javascript:;" class="header view link_btn disabled" style="top: 100px;" onclick="scence.btnclick(this);"><i class="fa fa-lock fa-2x"></i></a>
<!--Tools-->
<div id="mask_div" onclick="mask.hide(1);"></div>
<div id="mask_wait_div"><i class="fa fa-spinner fa-spin" style="font-size: 5em; color: #fff;"></i></div>
<div id="pop_div">
    <div class="popdiag" style="display: block;">
        <a href="javascript:;" class="header back link_btn" onclick="mbh5.backToEdit();"><i class="fa fa-chevron-left"></i></a>
        <iframe id="music_ifr" class="popifr"></iframe>
        <iframe id="pop_ifr" class="popifr"></iframe>
        <iframe id="edit_ifr" class="popifr"></iframe>
    </div>
</div>
<div id="toolsmenu_div" class="btn-group" style="position: absolute; display: none; transform: scale(2); z-index: 400;">
    <a href="javascript:;" class="btn btn-default" title="绑定动画" onclick="tools.animate();"><i class="fa fa-send"></i></a>
    <a href="javascript:;" class="btn btn-default" title="修改组件" onclick="tools.edit();"><i class="fa fa-pencil"></i></a>
    <a href="javascript:;" class="btn btn-danger" title="移除组件" onclick="tools.del();"><i class="fa fa-trash"></i></a>
</div>
<div ng-app="app">
    <div class="swiper-container editor scence" style="height:1040px;">
        <div id="editorBody" class="swiper-wrapper" ng-controller="appCtrl"></div>
        <img src="/design/h5/images/arrow.png" id="array" class="resize">
        <div class="swiper-pagination"></div>
    </div>
    <div ng-controller="footCtrl">
        <div class="footer">
            <a href="javascript:;" onclick="mbh5.foot.show(170,'addpage')">
                <i class="fa fa-file-o"></i>
                <span>页面</span>
            </a>
            <a href="javascript:;" onclick="mbh5.tlp.showadd();">
                <i class="fa fa-th-large"></i>
                <span>模板</span>
            </a>
            <a href="javascript:;">
                <div class="add" onclick="mbh5.comp.showadd();">
                    <i class="fa fa-plus"></i>
                </div>
            </a>
            <a href="javascript:;" ng-click="ele.show();">
                <i class="fa fa-th"></i>
                <span>元素</span>
            </a>
            <a href="javascript:;" onclick="mbh5.music.showadd();">
                <i class="fa fa-music"></i>
                <span>音乐</span>
            </a>
        </div>
        <div class="footcmd addcomp text-center" id="addcomp_div">
            <a href="javascript:;" ng-click="scence.add();">
                <i class="fa fa-file-o"></i>
                <span>页面</span>
            </a>
            <a href="javascript:;" onclick="mbh5.comp.addtext();">
                <i class="fa fa-text-height"></i>
                <span>文字</span>
            </a>
            <a href="javascript:;" onclick="mbh5.comp.addimg();">
                <i class="fa fa-image"></i>
                <span>图片</span>
            </a>
            <%--      <a href="javascript:;" onclick="mbh5.comp.addbtn();">
            <i class="fa fa-th"></i>
            <span>按钮</span>
        </a>--%>
        </div>
        <div class="footcmd addani" id="addani_div">
            <div class="footcmd_head">
                <div class="head_tab_wrap"></div>
                <div class="head_yes">
                    <a onclick="CloseDiag();" class="btn btn-default"><i class="fa fa-remove"></i></a>
                </div>
            </div>
            <div class="footcmd_ul_wrap">
                <ul class="footcmd_ul">
                    <li ng-repeat="item in ani.list" ng-class="ani.isactive(item)?'active':''" ng-click="ani.set(item);">
                        <img class="img" src="/design/res/mbh5/aniimg/ani_clear_animation.png" />
                        <div class="txt" ng-bind="item.name"></div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="footcmd addpage" id="addpage_div">
            <div class="footcmd_head">
                <div class="head_tab_wrap">
                    <input type="button" value="删除" class="btn btn-danger opbtn" ng-click="scence.del();" />
                    <button type="button" class="btn btn-info opbtn" ng-click="scence.move('pre');" style="margin-left: 46px; margin-right: 5px;" ng-disabled="scence.canmove('pre')"><i class="fa fa-chevron-left"></i>前移</button>
                    <button type="button" class="btn btn-info opbtn" ng-click="scence.move('next');" ng-disabled="scence.canmove('next')">后移 <i class="fa fa-chevron-right"></i></button>
                </div>
                <div class="head_yes">
                    <a onclick="CloseDiag();" class="btn btn-default"><i class="fa fa-remove"></i></a>
                </div>
            </div>
            <div class="footcmd_ul_wrap">
                <ul class="footcmd_ul">
                    <li ng-repeat="item in scence.list|orderBy:'order' track by $index " ng-class="scence.isactive(item)?'active':''" ng-click="scence.switch(item);">
                        <img class="img" ng-src="{{scence.getbk(item)}}" />
                        <div class="txt hid">当前页</div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="footcmd setbk" id="elelist_div">
            <div class="footcmd_head">
                <div class="head_tab_wrap">
                    <input type="button" value="全部" class="btn btn-info opbtn" ng-click="ele.show();" />
                    <input type="button" value="图片" class="btn btn-info opbtn" ng-click="ele.show('image');" />
                    <input type="button" value="文字" class="btn btn-info opbtn" ng-click="ele.show('text');" />
                </div>
                <div class="head_yes">
                    <a onclick="CloseDiag();" class="btn btn-default"><i class="fa fa-remove"></i></a>
                </div>
            </div>
            <div class="footcmd_ul_wrap">
                <%--元素列表--%>
                <ul class="footcmd_ul">
                    <li ng-repeat="item in ele.list" ng-class="ele.isactive(item)?'active':''" ng-click="ele.sel(item);">
                        <div ng-if="item.showimg"><img class="img" src="{{item.src}}" /></div>
                        <div class="con" ng-if="item.showtxt"><span ng-bind="item.txt"></span></div>
                        <div class="txt eletxt" ng-bind="item.name"></div>
                        <div ng-if="item.showdel" class="del"><div ng-click="ele.del(item,$index);" class="d_btn"><i class="fa fa-minus"></i></div></div>
                    </li>
                </ul>
            </div>
        </div>
        <div class="footcmd setbk" id="setbk_div">
            <div class="footcmd_head">
                <div class="head_tab_wrap">
                    <input type="button" value="清除" class="btn btn-danger opbtn" onclick="diy_bk.del();" />
                </div>
                <div class="head_yes">
                    <a onclick="CloseDiag();" class="btn btn-default"><i class="fa fa-remove"></i></a>
                </div>
            </div>
            <div class="footcmd_ul_wrap">
                <%--背景--%>
                <ul class="footcmd_ul">
                    <asp:Repeater runat="server" ID="BKRPT" EnableViewState="false">
                        <ItemTemplate>
                            <li class="img_li" onclick="diy_bk.setbk('<%#Eval("VPath") %>');">
                                <img src="<%#Eval("PreViewImg") %>" class="img" />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
        </div>
    </div>
    <div class="footcmd addani" id="addtlp_div">
        <div class="footcmd_head">
            <div class="head_tab_wrap"></div>
            <div class="head_yes">
                <a onclick="CloseDiag();" class="btn btn-default"><i class="fa fa-remove"></i></a>
            </div>
        </div>
        <div class="footcmd_ul_wrap">
            <ul class="footcmd_ul">
                <ZL:Repeater ID="TlpRPT" runat="server" EnableViewState="false">
                    <ItemTemplate>
                        <li onclick="scence.loadtlp(<%#Eval("ID") %>);">
                            <img class="img" onerror="shownopic(this);" src="<%#Eval("PreviewImg") %>" />
                            <div class="txt"><%#Eval("TlpName") %></div>
                        </li>
                    </ItemTemplate>
                </ZL:Repeater>

            </ul>
        </div>
    </div>
</div>
<div id="txt_div">
    <textarea id="txt_editor"></textarea>
    <div class="footwrap">
        <input type="button" value="关闭窗口" class="btn btn-lg btn-default" style="margin-right: 10px;" onclick="mbh5.comp.text.hide();" />
        <input type="button" value="保存修改" class="btn btn-lg btn-info" onclick="mbh5.comp.text.save();" />
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link rel="stylesheet" href="/design/res/css/comp.css" />
<link rel="stylesheet" href="/design/res/css/mbh5_design.css?v=<%:Version %>" />
<link rel="stylesheet" href="/design/h5/css/swiper.min.css">
<link rel="stylesheet" href="/design/h5/css/animate.min.css">
<script src="/JS/ZL_Regex.js"></script>
<script src="/JS/Plugs/angular.min.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Modal/APIResult.js"></script>
<script src="/JS/Controls/ZL_Webup.js?v=<%:Version %>"></script>
<%--<script src="/design/h5/js/swiper.min.js"></script>--%>
<script src="/design/h5/js/swiper.js"></script>
<script src="/design/h5/js/swiper.animate.min.js"></script>
<script src="/design/h5/js/animap.js"></script>
<script src="/design/mbh5/js/fastclick.js"></script>
<script src="/design/mbh5/js/hammer.min.js?v=<%:Version %>"></script>
<script src="/design/mbh5/js/drag.js"></script>
<script src="/design/js/sea.js"></script>
<script>
    $(function () {
        seajs.use(["/design/js/se_comp/page"], function (instance) {
            page = instance;
            ZLDE=seajs.require("base");
            page.guid = "<%:pageMod.guid%>";
            page.pageData=<%=pageMod.page%>
	        page.compData = <%=pageMod.comp%>;
            page.comp_global=<%=comp_global%>
	        page.extendData = <%=extendData%>;
            scence.list=<%=pageMod.scence%>;
            //---------------------------------修改弹窗
            scence.init();//先初始化场景再开始加载元素
            page.instance = $(document).contents();
            page.init();
            for (var i = 0; i < page.compList.length; i++) {editor.scope.addDom(page.compList[i]);}
            editor.footscope.scence.list=scence.list;
        });
    });
	var showmenu = function(){
	    if($("#titmenu").hasClass("menu_active")){ $("#titmenu").removeClass("menu_active"); }
	    else{ $("#titmenu").addClass("menu_active"); }
	}
	var refresh = function(title,url){
	    mbh5.pop.hide();
	    document.title = title;
	    $(".wxpic").remove();
	    showwxpic(url);
	}
</script>
<script src="js/mbh5.js?v=<%:Version %>"></script>
</asp:Content>
