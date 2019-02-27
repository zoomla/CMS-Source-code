var lastFrameTabId = 1; //最后新建的FrameTabId，用于新建FrameTab
var currentFrameTabId = 1; //当前显示的FrameTab
var frameTabCount = 1;
var lastLeft = "menu1_1";

var ZL_FrameTab = {
    AddNew: function() {    //新建一个标签
        $("#newFrameTab").click();
    },
    CloseCurrentTab: function(){    //关闭当前标签
        $("#iFrameTab" + currentFrameTabId).find(".closeTab").click();
    }
};
//给tab标签注册切换和关闭tab事件
$.fn.iFrameTab = function() {
    $(this).each(function() {
        var cr = $(this);
        var tabId = cr.attr("id").replace("iFrameTab", "");
        cr.click(function () {
            //切换FrameTab
            SwitchIframe(this);
        }).find(".closeTab").click(function () {//关闭FrameTab
            if (frameTabCount > 1) {
                var mainRightFrame = $("#main_right_frame iframe[tabid='" + tabId + "']");
                var bClose = mainRightFrame[0].contentWindow.OnCloseTab ? mainRightFrame[0].contentWindow.OnCloseTab() : true;
                if (bClose) {
                    if (cr.attr("class") == "current") {//如果关闭的标签是当前标签，则切换到前一标签，如果前一标签不存在，则切换到后一标签
                        var nextIframe = cr.prev("li[id^='iFrameTab']");
                        if (nextIframe.length <= 0) { nextIframe = cr.next("li[id^='iFrameTab']"); }
                        SwitchIframe(nextIframe[0]);
                    }
                    //清理
                    cr.remove();
                    $("#frmTitle iframe[tabid='" + tabId + "']").remove();
                    mainRightFrame.remove();
                    frameTabCount--;
                    CheckFramesScroll();
                }
            }
        }).end();
    });
    return $(this);
}
function SwitchIframe(iFrameTab) {
    var tabId = $(iFrameTab).attr("id").replace("iFrameTab", "");  //鼠标点击的tab的id
    if (currentFrameTabId == tabId) { return false; }
    var switchFunc = $("#main_right")[0].contentWindow.window.BeforeSwitch;
	 
    var bSwitch = (switchFunc) ? switchFunc() : true;
    if (!bSwitch) { return false; }

    var currentGuideSrc = $("#frmTitle iframe[tabid='" + currentFrameTabId + "']").attr("src");
    SetCurrentFrameTab(iFrameTab);
    var guideFrames = $("#frmTitle > iframe").hide().attr({ "id": "", "name": "" });
	var mainFrames = $("#main_right_frame > iframe").hide().attr({ "id": "", "name": "" });
	var newGuideFrame = $("#frmTitle iframe[tabid='" + tabId + "']");
	var newMainFrame = $("#main_right_frame iframe[tabid='" + tabId + "']");
    //将iframe的window.name设为空，使<a target="main_right" />只对当前iframe有效
    mainFrames.each(function() { this.contentWindow.window.name = ""; }); 
    guideFrames.each(function() { this.contentWindow.window.name = ""; });
	
    if (newGuideFrame.length <= 0) {
        newGuideFrame = $("#frmTitle").append($("#iframeGuideTemplate").html())
				.find("[tabid=0]").attr({ "tabid": tabId, "src": currentGuideSrc || "about:blank", "id": "left", "name": "left" })
				.css("display", "block");
    } else {
        newGuideFrame = $("#frmTitle iframe[tabid='" + tabId + "']")
            .attr("id", "left").attr("name", "left").show();
    }
    if (newMainFrame.length <= 0) {//是否新建标签
        newMainFrame = $("#main_right_frame").prepend($("#iframeMainTemplate").html())
				.find("[tabid=0]").attr({ "tabid": tabId, "src": "Main.aspx", "id": "main_right", "name": "main_right" })
				.css("display", "block");
    } else {
        newMainFrame = $("#main_right_frame iframe[tabid='" + tabId + "']")
            .attr("id", "main_right").attr("name", "main_right").show();
    }
    //指定iframe的window.name，使<a target="main_right" />有效
    newMainFrame[0].contentWindow.window.name = "main_right";
    frames["main_right"] = newMainFrame[0].contentWindow.window;
    currentFrameTabId = tabId;//从1开始
    var switchInto = $("#main_right")[0].contentWindow.window.SwitchInto;
    if (switchInto) { switchInto(); }
    myFrame.SwitchLeft(currentFrameTabId);
    document.getElementById("main_right").height = document.documentElement.clientHeight - 140;
    //记忆搜索
    setTimeout(function () { myFrame.SetKeyword(currentFrameTabId); $(".ascx_key").bind("blur paste clip", function () { myFrame.SaveKeyword(currentFrameTabId, $(this).val()); }); }, 300);
}
//初始化新建标签
function InitNewFrameTab() {
    $("#newFrameTab").click(function () {
        $(".ascx_key").blur();//触发关键词忆
        //$("#FrameTabs .current").removeClass("current");
        $('<li id="iFrameTab' + (++lastFrameTabId) + '" ><a href="javascript:"><span id="frameTabTitle">新选项卡</span><a class="closeTab" title="关闭"><span class="fa fa-remove"></span></a></a></li>').insertBefore(this).iFrameTab();
        frameTabCount++;
        SwitchIframe($("#iFrameTab" + lastFrameTabId)[0]);
                if (lastLeft.indexOf(".ascx") < 0)
            showleft(lastLeft);
        else
            ShowMain(lastLeft);
    });
}
//新建一个标签
function NewFrameTab() {
    $("#newFrameTab").click();
}
function SetCurrentFrameTab(selector) {
    $("#FrameTabs .current").removeClass("current");
    $(selector).addClass("current");
}
//检查是否需要滚动
function CheckFramesScroll() {
    var ft = $("#FrameTabs");
    window.cW = ft.width(); //包含Tabs的容器宽度
    window.fW = ft.find("ul:eq(0)").width();
    ft.unbind("DOMMouseScroll").unbind("mousewheel");
}
//Tab滚动,cW为包含Tabs的容器宽度；fW为全部Tabs的宽度；y为指定的位移，如果不指定y，则使用event中的位移。
function ScrollFrames(cW, fW, event, y) {
    if (!y) {
        if (event.wheelDelta) {
            y = event.wheelDelta / 5;
        } else if (event.detail) {
            y = -event.detail * 8;
        }
    }
    var jList = $("#FrameTabs ul:eq(0)");
    var ml = jList.css("margin-left");
    ml = Number(ml.toLowerCase().replace("px", ""));
    if ((ml < 0 && y > 0) || (ml - cW > -fW - 40) && y < 0) {
        ml = ml + y;
        if (ml >= 0) { ml = 0; }
        if (ml - cW <= -fW - 40) { ml = cW - fW - 40;}
        jList.css("margin-left", ml);
    }
}
//注册Tab超出范围时左移、右移事件
function RegScrollFramesBtn() {
    $("#FrameTabs .tab-right").click(function() { 
        ScrollFrames(window.cW,window.fW,null,-50);
    });
    $("#FrameTabs .tab-left").click(function() { 
        ScrollFrames(window.cW,window.fW,null,50);
    });
}
//tarFrame参数为目标iframe
function SetTabTitle(tarFrame) {
    var title = "";
    try { title = tarFrame.contentWindow.document.title; } catch (e) { }
    var subTitle = title = title || "新选项卡";
    if (title.length > 6) { subTitle = title.substr(0, 5) + ".." }
    $("#iFrameTab" + $(tarFrame).attr("tabid")).find("#frameTabTitle").html(subTitle).attr("title", title);
}
$(function() {
    $("#FrameTabs li[id^='iFrameTab']").iFrameTab();
    InitNewFrameTab(); 
    RegScrollFramesBtn();
});