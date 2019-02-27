var screenTurn = {};
//传入vertical(竖屏)或cross(横屏)
screenTurn.init = function (data) {
    if (data != "vertical" && data != "cross") { return; }
    var ref = this;
    if ($("#orientLayer").length < 1) {
        $("body").append($('<div id="orientLayer" class="mod-orient-layer" onclick="$(this).hide();"><div class="mod-orient-layer__content"><i class="icon mod-orient-layer__icon-orient"></i><div id="msg" class="mod-orient-layer__desc"></div></div></div>'));
    }
    switch (data) {
        case "vertical"://竖屏
            $("#orientLayer").find("#msg").html("为了更好的体验，请使用竖屏浏览");
            break;
        case "cross"://横屏
            $("#orientLayer").find("#msg").html("为了更好的体验，请使用横屏浏览");
            break;
    }
    ref.orientNotice(data);
    window.addEventListener("onorientationchange" in window ? "orientationchange" : "resize", function () {
        setTimeout(ref.orientNotice(data), 200);
    })
}
screenTurn.orientNotice = function (data) {
    var ref = this;
    var screen = "";
    if (window.orientation === 180 || window.orientation === 0) { screen = "vertical"; }
    else if (window.orientation === 90 || window.orientation === -90) { screen = "cross"; }
    else { return; }
    if (data != screen) { $("#orientLayer").show(); }
    else { $("#orientLayer").hide(); }
}