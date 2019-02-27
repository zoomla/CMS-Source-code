var wwwSite = "";
function buildWWW(relativePath)
{ return wwwSite + "/" + relativePath; }
var $cookie = {
    set: function (name, value, options) {
        var cv = "";
        options = options || {};
        value = value || null;
        if (value == null) {
            options = $.extend({}, options);
            options.expires = -1;
        }
        if (value != null && typeof (value) == "string")
        { cv = escape(value).replace(/\+/g, "%2b"); }
        else {
            if (value != null && typeof (value) == "object") {
                var jsonv = $cookie.ToJson($cookie.get(name));
                if (jsonv == false) { jsonv = {} }
                for (var k in value)
                { eval("jsonv." + k + '="' + value[k] + '"'); }
                for (var k in jsonv) {
                    cv += k + "=" + escape(jsonv[k]).replace(/\+/g, "%2b") + "&";
                }
                cv = cv.substring(0, cv.length - 1);
            }
        }
        var expires = "";
        if (options.expires && (typeof options.expires == "number" || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == "number") {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000))
            }
            else {
                date = options.expires
            }
            expires = "; expires=" + date.toUTCString()
        }
        var path = options.path ? "; path=" + (options.path) : "; path=/";
        var domain = options.domain ? "; domain=" + (options.domain) : "";
        if (options.topdomain) {
            var host = location.hostname;
            hostindex = host.indexOf(".");
            if (hostindex > 0) {
                host = host.substring(hostindex);
                domain = "; domain=" + host
            }
        }
        var secure = options.secure ? "; secure" : "";
        document.cookie = [name, "=", cv, expires, path, domain, secure].join("")
    }
    , get: function (n, k) {
        var reg = new RegExp("(^| )" + n + "=([^;]*)(;|$)");
        var arr = document.cookie.match(reg);
        if (arguments.length == 2) {
            if (arr != null) {
                var kArr, kReg = new RegExp("(^| |&)" + k + "=([^&]*)(&|$)");
                var c = arr[2];
                var c = c ? c : document.cookie;
                if (kArr = c.match(kReg))
                { return unescape(kArr[2].replace(/\+/g, "%20")) }
                else
                { return "" }
            }
            else
            { return "" }
        }
        else {
            if (arguments.length == 1) {
                if (arr != null)
                { return unescape(arr[2].replace(/\+/g, "%20")) }
                else { return "" }
            }
        }
    }
    , ToJson: function (cv) {
        var cv = cv.replace(new RegExp("=", "gi"), ":'").replace(new RegExp("&", "gi"), "',").replace(new RegExp(";\\s", "gi"), "',");
        return eval("({" + cv + (cv.length > 0 ? "'" : "") + "})")
    }
    , clear: function (name, options) {
        var expires = ";expires=Thu, 01-Jan-1900 00:00:01 GMT";
        var path = options.path ? "; path=" + (options.path) : "; path=/";
        var domain = options.domain ? "; domain=" + (options.domain) : "";
        if (options.topdomain) {
            var host = location.hostname;
            hostindex = host.indexOf(".");
            if (hostindex > 0) {
                host = host.substring(hostindex);
                domain = "; domain=" + host;
            }
        }
        var secure = options.secure ? "; secure" : "";
        document.cookie = [name, "=", expires, path, domain, secure].join("")
    }
}

var openWinMode =
{
    setBlank: function (N) {
        N = N == "N" ? "_blank" : "";
        var currentUrl = window.location.href;
        var HrefArray = document.getElementsByTagName("a");
        var ProductUrl = /Product\/ProductUI\.aspx/i;
        var NewsUrl = /NewsUI\.aspx/i;
        var BaiduUrl = /http:\/\/www\.baidu\.com/i;
        var IsDefault = false;
        if (location.href.toLowerCase() == buildWWW("default.aspx").toLowerCase()
            || location.href.toLowerCase() == buildWWW("").toLowerCase())
        { IsDefault = true; }
        var M, J;
        var I = HrefArray.length;
        for (var V = I; V--; ) {
            M = HrefArray[V];
            var H = $(M).attr("href");
            var B = $(M).attr("ref");
            if (!H) { H = "" }
            if (!B) { B = "" }
            J = H.indexOf("#") == -1 ? false : true;
            if (IsDefault) {//对首页处理
                if (H != "" && H.indexOf("javascript") == -1
                    && H != currentUrl && B != "noBlank"
                    && J == false)
                { M.target = N; }
            }
            else {
                if (B != "noBlank") {
                    if (B == "blank") {
                        if (H == currentUrl) {
                            M.target = ""; //特定页面设置为原窗口打开
                        }
                        else {
                            M.target = N;
                        }
                    }
                    else {
                        M.target = N;
//                        if (ProductUrl.test(M) && J == false && !ProductUrl.test(currentUrl)) {
//                            M.target = N;
//                        }
//                        if (NewsUrl.test(M) && !NewsUrl.test(currentUrl) && J == false) {
//                            M.target = N;
//                        }

                    }
                }
            }
        }

    }
    , updateState: function (A) {
        var C = $cookie.get("Blank");

        if (C == A) { return; }
        if (A) {
            $cookie.set("Blank", A, { topdomain: true, expires: 9999 });
            openWinMode.setBlank(A);
        }
        else {
            openWinMode.setBlank(C);
        }
    },
    updateButtonState: function (A) {
               if (A == "N") {
                    $("#btnNoBlank").removeClass("curr");
                     $("#btnNewBlank").addClass("curr") ;
                }
               else {
                    if (A == "C") {
                     $("#btnNewBlank").removeClass("curr");
                     $("#btnNoBlank").addClass("curr") 						 
					 
                }
            }
    }
    , init: function () { 
        var currentSetValue = $cookie.get("Blank");
        $("#btnNewBlank").click
             (
                function () {
                    if ($cookie.get("Blank") == "N") { return; }
					$("#setOk").show();$("#setOk").fadeOut(3000);//显示提示信息
                    openWinMode.updateButtonState('N'); //修改按钮状态
                    openWinMode.updateState('N'); //修改cookie值
				 
                }
            );
			
            $("#btnNoBlank").click
            (
                function () {
                    if ($cookie.get("Blank") == "C") { return; }
					 $("#setOk").show();$("#setOk").fadeOut(3000);//显示提示信息
                    openWinMode.updateButtonState('C'); //修改按钮状态
                    openWinMode.updateState('C'); //修改cookie值 
					 
                }
            );
            if (currentSetValue == "N" || currentSetValue == "C") {
                openWinMode.updateButtonState(currentSetValue)
                openWinMode.setBlank(currentSetValue);
            }
            else {
                openWinMode.updateButtonState('N')
                openWinMode.setBlank('N');
            }
    }
}
openWinMode.init();