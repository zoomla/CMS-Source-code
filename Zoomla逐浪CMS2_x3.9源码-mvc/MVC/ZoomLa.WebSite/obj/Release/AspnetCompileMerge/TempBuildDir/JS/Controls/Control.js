/*用于放置公用的控件方法,JSON,需要control.css,zl_array.js支持*/
(function ($) {
    $.fn.extend({
        ZLSteps: function (list, current) {
            //支持以,号切割的字符串,或list[{text:"",title:""}]
            if (!list) { console.log("ZLSteps", "list的值为空"); }
            if (typeof list == "string") {
                var strArr = list.split(',');
                list = [];
                for (var i = 0; i < strArr.length; i++) {
                    list.push({ text: strArr[i], title: strArr[i] });
                }
            }
            current = current ? current : 1;
            /*---------------------------------------------------------------*/
            var $wrap = $('<div class="zl_steps"></div>');
            var tlp = '<div class="wrap" data-step="@index" title="@text"><div class="@state"><label><span class="round">@index</span> <span>@text</span></label><i class="step_right_bg"></i><i class="step_right"></i></div></div>';
            for (var i = 0; i < list.length; i++) {
                var model = list[i];
                model.index = (i + 1);
                if (model.index < current) { model.state = "finished"; }
                else if (model.index == current) { model.state = "current"; }
                else { model.state = "todo"; }
                if (model.index == list.length) { model.state += " last"; }
            }
            var $items = JsonHelper.FillItem(tlp, list, function ($item, mod) {
                if (mod.index < current) { $item.find(".round").html("").append('<i class="fa fa-check"></i>'); }
            });
            $wrap.append($items);
            this.html("").append($wrap);
        }
    });
})(jQuery)
var Control = {
    EnableEnter: function () {//回车插件,过滤不可见控件
        $("input[data-enter]").keydown(function () {
            if (event.keyCode == 13) {
                var flag = false;
                var code = $(this).data("enter");
                var $arr = $("[data-enter]:visible").sort(function (a, b) { return $(a).data("enter") - $(b).data("enter") });
                var $next = null;
                for (var i = 0; i < $arr.length; i++) {
                    if ($($arr[i]).data("enter") > code) {
                        $next = $($arr[i]); break;
                    }
                }
                if ($next == null || $next.length < 0) return false;
                switch ($next.attr("type")) {
                    case "button":
                        $next.trigger("click").focus();
                        break;
                    case "submit"://有Bug,会提交两镒
                        flag = true;
                        break;
                    default:
                        $next.focus();
                        break;
                }
                return flag;
            }
        });//EnableEnter End;
    }
}
Control.Scroll = {
    ToTop: function () {
        function setScollTop() {
            if ($(window).scrollTop() > 1) {
                $(window).scrollTop($(window).scrollTop() - 30);
                setTimeout(setScollTop, 1);
            }
        }
        setTimeout(setScollTop, 1);
    },
    ToBottom: function () {
        function setScollDown() {
            var top = $(window).scrollTop();
            $(window).scrollTop($(window).scrollTop() + 30);
            if (top != $(window).scrollTop())
                setTimeout(setScollDown, 1);
        }
        setTimeout(setScollDown, 1);
    }
}
Control.Mobile = {
    SendVaildMsg: function (btnid, codeid, hcodeid, mobile) {//发送短信验证码
        var $btnobj = $("#" + btnid);//按钮对象
        var $codeobj = $("#" + codeid);//验证码输入框对象
        var $hcodeobj = $("#" + hcodeid);//验证码隐藏值对象
        $btnobj.attr("disabled", "disabled");
        $.post("/API/Mod/Mobile.ashx", { action: "SendVailMsg", code: $codeobj.val(), hcode: $hcodeobj.val(), mobile: mobile }, function (data) {
            if (data == "1") {
                alert("验证码已发送至您的手机,请您验收!");
                var sendminute = 60;//计时
                var sendtimeflag = setInterval(function () {
                    sendminute--;
                    $btnobj.text("(" + sendminute + "秒后)重发验证码");
                    if (sendminute <= 0) {//时间到
                        $btnobj.text("重发验证码");
                        $btnobj.removeAttr("disabled");
                        sendminute = 60;
                        clearInterval(sendtimeflag);
                    }
                }, 1000);
            } else {
                alert("发送失败!详情:" + data);
                $btnobj.removeAttr("disabled");
            }
        })
    }
};
Control.btn = {
    wait: function (obj, time) {
        var ref = Control.btn;
        ref.wait_db.obj = obj;
        ref.wait_db.text = obj.value;
        setTimeout(function () { obj.disabled = true; }, 50);
        ref.wait_db.inter = setInterval(function () { time = time - 1; obj.value = "请等待(" + time + ")秒"; }, 1000);
        setTimeout(function () { ref.wait_clear();}, time * 1000);
    },
    wait_db: { obj: null, inter: null, text: "" },
    wait_clear: function () {
        var ref = Control.btn;
        clearInterval(ref.wait_db.inter);
        ref.wait_db.obj.value = ref.wait_db.text;
        setTimeout(function () { $(ref.wait_db.obj).removeAttr("disabled"); }, 100);
    }
};

