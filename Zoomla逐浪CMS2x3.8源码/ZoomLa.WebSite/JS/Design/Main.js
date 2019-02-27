$cur = null;//$()当前元素
//根据控件数据,填充设计栏
function EditA(obj) {
    $cur = $(obj);
    //margin与padding
    var directArr = "top,bottom,left,right".split(',');
    for (var i = 0; i < directArr.length; i++) {
        var val = ConverToInt($cur.css("margin-" + directArr[i]));
        $(".label.margin." + directArr[i]).text(val);
    }
    for (var i = 0; i < directArr.length; i++) {
        var val = ConverToInt($cur.css("padding-" + directArr[i]));
        $(".label.padding." + directArr[i]).text(val);
    }
    //-------------
    $("#width_t").val($cur.width());
    $("#height_t").val($cur.height());
    //都可以通过其取到值
    $("#bkcolor_t").val(RGBTo16($cur.css("background-color")));
    $("#bkimg_t").val($cur.css("background-image"));
    $("#border_width_t").val($cur.css("border-width"));
    $("#font_size_t").val($cur.css("font-size"));
    //对齐方式
    $("#text_align_div *").removeClass('active');
    var align = $cur.css("text-align") == "start" ? "left" : $cur.css("text-align");//过滤start
    align = $cur.css("text-align") == "end" ? "right" : align;//过滤end
    $("#text_align_div [data-param='" + align + "']").addClass('active');
    //文本样式
    $("#color_t").val(RGBTo16($cur.css('color')));
    $("#color_t").next().children().css('background-color', $cur.css('color'));
    $("#text_decoration_div *").removeClass('active');
    $("#text_decoration_div").find('.text-decoration-' + $cur.css('text-decoration')).addClass('active');
    //字体样式
    $("#font_style_div *").removeClass('active');
    $("#font_style_div").find('.font-style-' + $cur.css('font-style')).addClass('active');
    //链接属性
    $("#a_decoration_div *").removeClass('active');
    if ($cur.find('a').css('color')) {
        $("#a_color_t").val(RGBTo16($cur.find('a').css('color')));
        $("#a_color_t").next().children().css('background-color', $cur.find('a').css('color'));
        //链接文本样式
        $("#a_decoration_div").find('.text-decoration-' + $cur.find('a').css('text-decoration')).addClass('active');
    }
    //背景图片
    $("#background_repeat *").removeClass('active');
    $("#bkimg").css('background', $cur.css('background'));
    $("#background_repeat .background-" + $cur.css('background-repeat')).addClass('active');
    //window.getComputedStyle();
    //$("#line_height_t").val($cur.css("line-height"));
}
//字体,字体大小,文本对齐,边框,
//-----------事件JS,用于绑定右边栏与控件事件
$(function () {
    //边框线
    $(".sides>span").click(function () {
        $(this).siblings().removeClass("active");
        $(this).addClass("active");
    });
    //边框线,文字等各种选项
    //1,依据样式,2,依据data-hint(这个应该仅用于提示)
    $(".options>.option").click(function () {
        $obj = $(this);
        $obj.siblings().removeClass("active");
        $obj.addClass("active");
        //根据data-bind和根据样式来做不同的处理吧
        switch ($obj.data("bind")) {
            case "border":
                {
                    //获取边框
                    var side = $(".border .sides .active").data("side");
                    if (side == "all") { side = "border"; }
                    else { side = "border-" + side; }
                    //获取线的样式
                    var line_width = ConverToInt($("#border_width_t").val()) + "px";
                    var line_color = $("#border_color_t").val() != "" ? $("#border_color_t").val() : "#ddd";
                    var style = line_width + " " + $obj.data("param") + " " + line_color;
                    $cur.css(side, style);
                }
                break;
            case "text"://文字左右对齐等
                {
                    var align = $obj.data("param");
                    $cur.css("text-align", align);
                }
                break;
            case "text-decoration"://文本下划线
                var decoration = $obj.data("param");
                $cur.css("text-decoration", decoration);
                break;
            case "font-style"://文字样式(斜体)
                {
                    var style = $obj.data("param");
                    $cur.css('font-style', style);
                }
                break;
            case "a-decoration"://链接下划线样式
                {
                    var decoration = $obj.data('param');
                    $cur.find('a').css('text-decoration', decoration);
                }
                break;
            case "background-repeat"://背景重复样式
                {
                    var repeat = $obj.data('param');
                    $cur.css('background-repeat', repeat);
                }
                break;
            default:
                console.log($obj.data("bind"));
                break;
        }
    });
    //直接绑定,用于某些较简单的事件,如文字大小等
    $("#font_size_t").change(function () {
        $cur.css("font-size", ConverToInt(this.value) + "px");
    });
    $("#font_family_dp").change(function () {
        $cur.css("font-family", $("#font_family_dp option:checked").text());
    });
    //选择颜色按钮点击事件
    $(".diy-control.color .preview").click(function () {
        $curImgText = $(this).prev();
        ShowDiag("/Common/SetColor.htm", "选择颜色");
    });
    //label用于显示文字与值,handle绑定事件
    $(function () {
        $(".box>.handle[data-direct]").each(function () {
            MouseBind_UD(this, "margin");//只用上下
        });
        $(".padding>.handle[data-direct]").each(function () {
            MouseBind_UD(this, "padding");//只用上下
        });
    });
    //收缩属性
    $(".title").click(function () {
        $(this).next().toggle();
        if ($(this).next().is(":visible"))
            $(this).removeClass("acitve");
        else
            $(this).addClass("acitve");
    });
    $(".preset a").mousedown(function () { $(this).css('background-color', '#2c2c2c') });
    $(".preset a").mouseup(function () { $(this).css('background-color', '#474747') });
})
//清空背景样式
function CleanBGImg() {
    $cur.css('background', 'none');
    $("#background_repeat *").removeClass('active');
    $("#bkimg").removeAttr('style');
}
var $curImgText;//当前图片字段
function setColor(color) {
    diag.CloseModal();
    $curImgText.val(color);
    $curImgText.next().children().css('background-color', color);
    switch ($curImgText.attr('id')) {
        case "color_t"://文本颜色
            $cur.css('color', color);
            break;
        case "a_color_t"://链接颜色
            $cur.find('a').css('color', color);
            break;
        case "bkcolor_t"://背景色
            $cur.css('background-color', color);
            $("#bkimg").css('background-color', color);
            break;

    }
}
//-----------值获取或转换辅助方法
function RGBTo16(rgb) {
    rgb = rgb.split('(')[1];
    var str = [3];
    for (var k = 0; k < 3; k++) {
        str[k] = parseInt(rgb.split(',')[k]).toString(16);//str 数组保存拆分后的数据 
    }
    var colsr = '#' + str[0] + str[1] + str[2];
    return colsr;
}
//默认返回1
function ConverToInt(val, suf) {
    if (!val || val == "") { val = "1"; }
    val = val.replace(/ /g, "").replace("px", "").replace("em", "");
    val = parseInt(val);
    if (isNaN(val)) { val = 1; }
    return val;
}
//-----------
var diag = new ZL_Dialog();
function ShowDiag(url, title) {
    diag.maxbtn = false;
    diag.backdrop = true;
    diag.width = "dialog";
    diag.url = url;//"/common/SelFiles.aspx?pval=" + JSON.stringify({ "name": name });
    diag.title = title;// "选择图片";
    diag.ShowModal();
}
function CloseDialog() { diag.CloseModal(); tlpdiag.CloseModal(); }
//选择在线图片,也可上传新的在线图片
function SelImg(name) {
    ShowDiag("/common/SelFiles.aspx?pval=" + JSON.stringify({ "name": name }), "选择图片");
}
function PageCallBack(action, url, pval) {
    $("#" + pval.name).css('background', 'url("' + url.split('|')[0] + '") center center no-repeat');
    $cur.css('background', 'url("' + url.split('|')[0] + '") center center no-repeat');
    diag.CloseModal();
}
//上下
function MouseBind_UD(oLine, type) {
    oLine.onmousedown = function (e) {
        if ($cur == null) { return; }
        var disY = (e || event).clientY;
        var direct = $(oLine).data("direct");
        var $span = $(".label." + type + "." + direct);
        //oLine.top = ConverToInt($span.text());
        var initTop = ConverToInt($span.text());
        document.onmousemove = function (e) {
            var val = initTop + ((e || event).clientY - disY);
            $span.text(val);
            $cur.css(type + "-" + direct, val + "px");
            return false
        };
        document.onmouseup = function () {
            document.onmousemove = null;
            document.onmouseup = null;
            oLine.releaseCapture && oLine.releaseCapture();
        };
        oLine.setCapture && oLine.setCapture();
        return false
    };
}
//左右
function MouseBind_LR(oLine) {
    oLine.onmousedown = function (e) {
        var disX = (e || event).clientX;
        oLine.left = oLine.offsetLeft;
        document.onmousemove = function (e) {
            var val = oLine.left + ((e || event).clientX - disX);
            var direct = $(oLine).data("direct");
            $(".label[data-direct=" + direct + "]").text(val);
            //将得到的值给予目标
            return false
        };
        document.onmouseup = function () {
            document.onmousemove = null;
            document.onmouseup = null;
            oLine.releaseCapture && oLine.releaseCapture()
        };
        oLine.setCapture && oLine.setCapture();
        return false
    };
}
//更换模板
function Tlp_SetValByName(name, val) {
    location = "Design.aspx?VPath=" + config.tlpdir + "/" + val;
}
var tlpdiag = new ZL_Dialog();
function SetTlpName() {
    tlpdiag.title = "设置模板名称";
    tlpdiag.content = "tlpname_div";
    tlpdiag.width = "default";
    tlpdiag.ShowModal();;
}
//----------保存
function SaveHead(html) {
    $("#Head_Hid").val(html);
}
//----------define
function define(url, callback, sett) {//modelName,callback,setting
    var scripts = document.getElementsByTagName('script');
    for (var i = 0; i < scripts.length; i++) {//是否已加载
        if (scripts[i].src.indexOf(url) > -1 && callback && (callback.constructor === Function)) {
            //已创建script
            if (scripts[i].className === 'loaded') {//已加载
                callback();
            } else {//加载中
                onloaded(scripts[i], callback);
            }
            return;
        }
    }
    var script = document.createElement('script');
    script.type = "text/javascript";
    script.src = url;
    document.body.appendChild(script);
    onloaded(script, callback);
}
function onloaded(script, callback) {//绑定加载完的回调函数
    //var isJs = /\/.+\.js($|\?)/i.test(url) ? true : false;
    if (script.readyState) { //ie
        script.attachEvent('onreadystatechange', function () {
            if (script.readyState == 'loaded' || script.readyState == 'complete') {
                script.className = 'loaded';
                callback && callback.constructor === Function && callback();
            }
        });
    } else {
        script.addEventListener('load', function () {
            script.className = "loaded";
            callback && callback.constructor === Function && callback();
        }, false);
    }
}