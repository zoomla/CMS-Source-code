function showLeftMenu(url) {
    ShowMain("/Office/Menu/LeftMenu.aspx?leftul=" + url);
}
function showRightCnt(url) { 
    var rifr = top.document.getElementById("main_right");
    rifr.src = url;
}
function ShowMain(lurl, rurl) {//优先加载右边iframe
    var rifr = top.document.getElementById("main_right");
    var lifr = top.document.getElementById("main_left");
    if (rifr && rurl != "") {
        rifr.src = rurl;
    } else { location = rurl; }
    if (lifr && lurl != "") {
        if (lurl.indexOf("#") == 0) {
            lurl = lurl.substring(1, lurl.length);//页面需改为直接用#切换
            lifr.src = "/Office/Menu/LeftMenu.aspx?leftul=" + lurl;
        }
        else {
            lifr.src = lurl;
        }
    }
    AutoHeight();//后期放到default中绑定
}
function AutoHeight() {
    var ifr = top.document.getElementById("main_right");
    console.log($(ifr).contents().innerHeight());
    var iframeHeight = $(ifr).contents().innerHeight();
    $(ifr).height(iframeHeight + 'px');
}
//--------------
$(function () {
    AutoHeight();
})
