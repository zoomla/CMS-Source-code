var weui = { diag: {}, pop: {}, toast: {} };
//用于弹文本,指定div,iframe
weui.diag.opts = { title: "提示", content: "", confirm: { text: "确定", click: null }, cancel: { text: "取消", click: null } };
weui.diag.show = function (opts) {
    var html = "<div class=\"weui_dialog_confirm\">"
             + "<div class=\"weui_mask\"></div>"
             + "<div class=\"weui_dialog\">"
             + "<div class=\"weui_dialog_hd\"><strong class=\"weui_dialog_title\">" + opts.title + "</strong></div>"
             + "<div class=\"weui_dialog_bd\">" + opts.title + "</div>"
             + "<div class=\"weui_dialog_ft\">"
             + "<a href=\"javascript:;\" class=\"weui_btn_dialog cancel  default\">" + opts.cancel.text + "</a>"
             + "<a href=\"javascript:;\" class=\"weui_btn_dialog confirm primary\">" + opts.confirm.text + "</a>"
             + "</div>"
             + "</div>"
             + "</div>";
    var $diag = $(html);
    $diag.find(".confirm").click(function () {
        if (opts.confirm.click) { opts.confirm.click(); }
        $diag.remove();
    });
    $diag.find(".cancel").click(function () {
        if (opts.cancel.click) { opts.cancel.click(); }
        $diag.remove();
    });
    $("body").append($diag);
}
weui.toast.wait = function () {
    var html = "<div id=\"loadingToast\" class=\"weui_loading_toast\">"
            + "<div class=\"weui_mask_transparent\"></div>"
            + "<div class=\"weui_toast\">"
            + "<div class=\"weui_loading\">"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_0\"></div>"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_1\"></div>"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_2\"></div>"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_3\"></div>"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_4\"></div>"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_5\"></div>"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_6\"></div>"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_7\"></div>"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_8\"></div>"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_9\"></div>"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_10\"></div>"
            + "<div class=\"weui_loading_leaf weui_loading_leaf_11\"></div>"
            + "</div>"
            + "<p class=\"weui_toast_content\">数据加载中</p>"
            + "</div>"
            + "</div>";
    $("body").append(html);
}
weui.toast.close = function () { $("#loadingToast").remove(); }
//弹出窗口iframe加载对应页面,进行文章修改等操作
weui.pop.show = function (url) {
    //不在此处附加,方便页面自定义样式
    var ref = weui.pop;
    var $ifr = $("#pop_ifr");
    weui.toast.wait();
    $ifr.attr("src", url);
    $ifr[0].onload = function () { weui.toast.close(); $("#pop_div").show(); }
}
weui.pop.close = function () {
    $("#pop_div").hide();
}
weui.pop.opts = { title: "提示", content: "", confirm: { text: "确定", click: null }, cancel: { text: "取消", click: null } };