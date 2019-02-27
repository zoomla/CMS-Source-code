/*
* 新建一个标签：ZL_FrameTab.AddNew()
* 关闭当前标签：ZL_FrameTab.CloseCurrentTab()
* 从当前标签切换到其他标签时触发：BeforeSwitch(); 如果该函数存在并返回false则不切换标签.该函数直接写在当前页面中.
* 从其他标签切换到当前标签时触发：SwitchInto(); 如果该函数存在则执行.该函数直接写在要切换到的页面.
* 在关闭当前标签页的时候触发：OnCloseTab(); 如果该函数存在存在则执行.该函数直接写在当前页面中.
*/

var lastFrameTabId = 1; //最后新建的FrameTabId，用于新建FrameTab
var currentFrameTabId = 1; //当前显示的FrameTab
var frameTabCount = 1;

var ZL_FrameTab = {
    //新建一个标签
    AddNew: function() {
        jQuery("#newFrameTab").click();
    },
    //关闭当前标签
    CloseCurrentTab: function(){
        jQuery("#iFrameTab" + currentFrameTabId).find(".closeTab").click();
    }
};

///给tab标签注册切换和关闭tab事件
jQuery.fn.iFrameTab = function() {
    jQuery(this).each(function() {
        var cr = jQuery(this);
        var tabId = cr.attr("id").replace("iFrameTab", "");
        cr.click(function() {//切换FrameTab
            SwitchIframe(this);
        }).find(".closeTab").click(function() {//关闭FrameTab
            if (frameTabCount > 1) {
                var mainRightFrame = jQuery("#main_right_frame iframe[tabid='" + tabId + "']");
                var bClose = mainRightFrame[0].contentWindow.OnCloseTab ? mainRightFrame[0].contentWindow.OnCloseTab() : true;
                if (bClose) {
                    if (cr.attr("class") == "current") {//如果关闭的标签是当前标签，则切换到前一标签，如果前一标签不存在，则切换到后一标签
                        var nextIframe = cr.prev("li[id^='iFrameTab']");
                        if (nextIframe.length <= 0) { nextIframe = cr.next("li[id^='iFrameTab']"); }
                        SwitchIframe(nextIframe[0]);
                    }
                    //清理
                    cr.remove();
                    jQuery("#frmTitle iframe[tabid='" + tabId + "']").remove();
                    mainRightFrame.remove();
                    frameTabCount--;
                    CheckFramesScroll();
                }
            }
        }).end().dblclick(function() {
            jQuery(this).find(".closeTab").click();
        });
    });
    return jQuery(this);
}
///切换tab
function SwitchIframe(iFrameTab) {
    var tabId = jQuery(iFrameTab).attr("id").replace("iFrameTab", "");  //鼠标点击的tab的id
    if (currentFrameTabId == tabId) { return false; }
    //判断是否允许切换Tab document.documentElement.clientWidth-205;
    var switchFunc = jQuery("#main_right")[0].contentWindow.window.BeforeSwitch;
	 
    var bSwitch = (switchFunc) ? switchFunc() : true;
    if (!bSwitch) { return false; }

    var currentGuideSrc = jQuery("#frmTitle iframe[tabid='" + currentFrameTabId + "']").attr("src");
    SetCurrentFrameTab(iFrameTab);
    var guideFrames = jQuery("#frmTitle > iframe").hide().attr({ "id": "", "name": "" });
	var mainFrames = jQuery("#main_right_frame > iframe").hide().attr({ "id": "", "name": "" });
	var newGuideFrame = jQuery("#frmTitle iframe[tabid='" + tabId + "']");
	var newMainFrame = jQuery("#main_right_frame iframe[tabid='" + tabId + "']");
    //将iframe的window.name设为空，使<a target="main_right" />只对当前iframe有效
    mainFrames.each(function() { this.contentWindow.window.name = ""; }); 
    guideFrames.each(function() { this.contentWindow.window.name = ""; });
	
    if (newGuideFrame.length <= 0) {
        newGuideFrame = jQuery("#frmTitle").append(jQuery("#iframeGuideTemplate").html())
				.find("[tabid=0]").attr({ "tabid": tabId, "src": currentGuideSrc || "about:blank", "id": "left", "name": "left" })
				.css("display", "block");
    } else {
        newGuideFrame = jQuery("#frmTitle iframe[tabid='" + tabId + "']")
            .attr("id", "left").attr("name", "left").show();
    }
    newGuideFrame[0].contentWindow.window.name = "left";
    frames["left"] = newGuideFrame[0].contentWindow.window;
    if (newMainFrame.length <= 0) {//是否新建标签
        newMainFrame = jQuery("#main_right_frame").prepend(jQuery("#iframeMainTemplate").html())
				.find("[tabid=0]").attr({ "tabid": tabId, "src": "Main.aspx", "id": "main_right", "name": "main_right" })
				.css("display", "block");
    } else {
        newMainFrame = jQuery("#main_right_frame iframe[tabid='" + tabId + "']")
            .attr("id", "main_right").attr("name", "main_right").show();
    }
    //指定iframe的window.name，使<a target="main_right" />有效
    newMainFrame[0].contentWindow.window.name = "main_right";
    frames["main_right"] = newMainFrame[0].contentWindow.window;

    currentFrameTabId = tabId;
    resizeFrame();
 
    var switchInto = jQuery("#main_right")[0].contentWindow.window.SwitchInto;
    if(switchInto){ switchInto(); }
}

///初始化新建标签
function InitNewFrameTab() {
    jQuery("#newFrameTab").click(function() {
        //jQuery("#FrameTabs .current").removeClass("current");
        jQuery('<li id="iFrameTab' + (++lastFrameTabId) + '" ><a href="javascript:"><span id="frameTabTitle">新选项卡</span><a class="closeTab"><img border="0" src="/images/tab-close.gif"/></a></a></li>')
				.insertBefore(this).iFrameTab();
        frameTabCount++;
        SwitchIframe(jQuery("#iFrameTab" + lastFrameTabId)[0]);
        if (CheckFramesScroll()) { jQuery("#FrameTabs ul:eq(0)").css("margin-left", cW - fW - 40); }
    });
}
//新建一个标签
function NewFrameTab() { 
    jQuery("#newFrameTab").click();
}

function SetCurrentFrameTab(selector) {
    jQuery("#FrameTabs .current").removeClass("current");
    jQuery(selector).addClass("current");
}
///检查是否需要滚动
function CheckFramesScroll() {
    var ft = jQuery("#FrameTabs");
    window.cW = ft.width(); //包含Tabs的容器宽度
    window.fW = ft.find("ul:eq(0)").width();
    ft.unbind("DOMMouseScroll").unbind("mousewheel");
    if (fW > cW) {
        if (jQuery.browser.mozilla) {
            ft.bind("DOMMouseScroll", function(e) {
                ScrollFrames(cW, fW, e);
            });
        } else {
            ft.bind("mousewheel", function(e) {
                ScrollFrames(cW, fW, e);
            });
        }
        jQuery("#FrameTabs .tab-strip-wrap").addClass("tab-strip-margin");
        jQuery("#FrameTabs .tab-right, #FrameTabs .tab-left").css("display", "block");
        return true;
    } else {
        jQuery("#FrameTabs ul:eq(0)").css("margin-left", 0);
        jQuery("#FrameTabs .tab-right, #FrameTabs .tab-left").css("display", "none");
        jQuery("#FrameTabs .tab-strip-wrap").removeClass("tab-strip-margin");
        return false;
    }
}
///Tab滚动。
///cW为包含Tabs的容器宽度；fW为全部Tabs的宽度；y为指定的位移，如果不指定y，则使用event中的位移。
function ScrollFrames(cW, fW, event, y) {
    if (!y) {
        if (event.wheelDelta) {
            y = event.wheelDelta / 5;
        } else if (event.detail) {
            y = -event.detail * 8;
        }
    }
    var jList = jQuery("#FrameTabs ul:eq(0)");
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
    jQuery("#FrameTabs .tab-right").click(function() { 
        ScrollFrames(window.cW,window.fW,null,-50);
    });
    jQuery("#FrameTabs .tab-left").click(function() { 
        ScrollFrames(window.cW,window.fW,null,50);
    });
}
//设置标签的标题
//tarFrame参数为目标iframe
function SetTabTitle(tarFrame){ 
    var title = "";
    try { title = tarFrame.contentWindow.document.title; } catch (e) { }
    var subTitle = title = title || "新选项卡";
    if (title.length > 6) { subTitle = title.substr(0, 5) + ".." }
    jQuery("#iFrameTab" + jQuery(tarFrame).attr("tabid")).find("#frameTabTitle").html(subTitle).attr("title", title);
}

function resizeFrame() { 
//document.getElementById("main_right_frame").style.width=document.documentElement.clientWidth-205;
//document.getElementById("main_right").height=document.documentElement.clientHeight - 130;
var lHeight = document.documentElement.clientHeight - 100;
var rHeight = lHeight - (jQuery("#FrameTabs").height() || 0);
    var obj = document.getElementById("switchPoint");
    if (obj.alt == "打开左栏") {
        var width, height;
        width = document.body.clientWidth - 12;
       // height = document.body.clientHeight - 78; 
        document.getElementById("main_right").style.width = width;
        document.getElementById("FrameTabs").style.width = width;
    }
    else {
        var width = document.documentElement.clientWidth - 205; 
//        document.getElementById("main_right_frame").style.width = width > 0 ? width : 0;
//        document.getElementById("main_right").style.height = lHeight > 0 ? lHeight : 0;
        document.getElementById("main_right").style.width = width;
        document.getElementById("FrameTabs").style.width = width;
    }
// var width = document.body.clientWidth - 207;
// var lHeight = document.body.clientHeight - 78;
 //var rHeight = lHeight - (jQuery("#FrameTabs").height() || 0) ;
 // document.getElementById("main_right").style.width = width > 0 ? width : 0;
//document.getElementById("main_right").style.height = rHeight > 0 ? rHeight : 0;
//document.getElementById("left").style.height = lHeight > 0 ? lHeight : 0;
 //jQuery("#FrameTabs").width(width);
document.getElementById("left").height = lHeight;  
document.getElementById("main_right").height=rHeight;
}

jQuery(function() {
    jQuery("#FrameTabs li[id^='iFrameTab']").iFrameTab();
    InitNewFrameTab(); //初始化新建标签页
    RegScrollFramesBtn();
});
