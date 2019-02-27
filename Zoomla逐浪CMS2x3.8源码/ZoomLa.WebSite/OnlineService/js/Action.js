// 创建AJAXRequest实例

function request(paras) {
    var url = location.href;
    var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
    var paraObj = {}
    for (i = 0; j = paraString[i]; i++) {
        paraObj[j.substring(0, j.indexOf("=")).toLowerCase()] = j.substring(j.indexOf("=") + 1, j.length);
    }
    var returnValue = paraObj[paras.toLowerCase()];
    if (typeof (returnValue) == "undefined") {
        return "";
    } else {
        return returnValue;
    }
}

window.onload = function () {
    var type = request("type");
    if (type == "1") {
        var inputers = document.getElementById("inputer").innerHTML;
        var inputer = "";
        if (inputers.indexOf("<STRONG>") > -1) {
            var arrlist = inputers.split("<STRONG>");
            var content = arrlist[1];
            if (content.indexOf("</STRONG>") > -1) {
                var content2arr = content.split("</STRONG>")
                var inputer = content2arr[0];
            }
        }
        var now = new Date();
        var year = now.getYear();
        var month = now.getMonth() + 1;
        var day = now.getDate();
        var hour = now.getHours();
        var minute = now.getMinutes();
        var second = now.getSeconds();
        document.getElementById("message").innerHTML += "<b>系统信息：</b>" + " " + hour + ":" + minute + ":" + second + " <BR /><font color=red>您好，欢迎您的咨询，" + inputer + " 竭诚为您服务！</font><BR />"; doPrompt("end");
        document.getElementById('message').scrollTop = document.getElementById('message').scrollHeight + 24;
    }
    e_update_shwoform();
}

var ajax = new AJAXRequest;
// 请求提示
function doPrompt(step) {
    /*var obj=document.getElementById("reqPrompt");
    switch(step) {
    case "start":
    obj.style.display="block";
    break;
    case "end":
    obj.style.display="none";
    break;
    default:
    obj.style.display="none";
    break;
    }*/
}
/* e_onexception
// 测试异常
*/
function e_onexception() {
    ajax.onexception = function (e) {
        doPrompt("end");
        var str = "请求时出现错误：";
        str += "\n请求地址: " + e.url;
        str += "\n发送数据: " + e.content;
        str += "\n请求方式: " + e.method;
        str += "\n返回状态: " + e.status;
        alert(str);
    }
    doPrompt("start");
    ajax.get(
		"test1.aspx?c=onexception",
		function () { doPrompt("end"); }
	);
}

function e_update_name(type, sess) {
    ajax.get(
         "SendApi.aspx?c=updatename&type=" + type + "&sessionid=" + sess,
        function (obj) {
            if (type == 0) {
                document.getElementById("inputer").innerHTML = "您正在与 <strong>" + obj.responseText + "</strong> 交谈";
                document.getElementById("inputer").title = "您正在与 " + obj.responseText + " 交谈";
            }
        }
        );
}

var e_update_ts2;
function e_update_shwoform(type, sid, sess) {
    e_update_ts2 = "";
    if (!e_update_ts2) e_update_ts = ajax.update("left_main2", "SendApi.aspx?c=show", 1000);
}
function e_update_shwoform2() {
    clearInterval(e_update_ts2);
}

function e_update_content(obj, sid, type) {
    ajax.get(
    "SendApi.aspx?c=showmess&seatid=" + obj + "&sessionid=" + sid + "&type=" + type,
    function (obj) {
        var showtxt = unescape(obj.responseText.replace(/\+/g, ' '));
        document.getElementById("message").innerHTML += showtxt;
    }
    );
}

/* e_get
// 测试get方法
*/
function e_get() {
    doPrompt("start");
    ajax.get(
		"SendApi.aspx?c=get",
		function (obj) { document.getElementById("e_get_r").value = obj.responseText; doPrompt("end"); }
	);
}
/* e_post
// 测试post方法
*/
function e_post() {
    doPrompt("start");
    ajax.post(
		"SendApi.aspx?c=post",
		document.getElementById("e_post_p").value,
		function (obj) { document.getElementById("e_post_r").value = obj.responseText; doPrompt("end"); }
	);
}
/* e_postf
// 测试postf方法
*/
function e_postf() {
    doPrompt("start");
    ajax.postf(
		"e_postf_p",
		function (obj) {
		    if (document.getElementById("e_postf_r"))
		        document.getElementById("e_postf_r").innerHTML = obj.responseText; doPrompt("end");
		    if (document.getElementById("message"))
		        document.getElementById("message").innerHTML += obj.responseText + "<br />"; doPrompt("end");
		}
	);
}

function e_PostContentf() {
    var content = document.getElementById("ZoomlaEditorForm").contentWindow.document.getElementsByTagName('BODY')[0].innerHTML;
    doPrompt("start");
    ajax.setcharset("utf-8");
    ajax.post(
		"SendApi.aspx?c=PostContent&sessionid=" + document.getElementById("sessionid").value + "&seatid=" + document.getElementById("seatid").value + "&type=" + document.getElementById("type").value,
		content,
		function (obj) {
		    if (obj.responseText != "") {
		        var now = new Date();
		        var year = now.getYear();
		        var month = now.getMonth() + 1;
		        var day = now.getDate();
		        var hour = now.getHours();
		        var minute = now.getMinutes();
		        var second = now.getSeconds();
		        var content = obj.responseText; // document.getElementById("ZoomlaEditorIframe").document.frames['ZoomlaEditorForm'].document.getElementsByTagName('BODY')[0].innerHTML;
		        document.getElementById("ZoomlaEditorForm").contentWindow.document.getElementsByTagName('BODY')[0].innerHTML = "";
		        var sendcontent = deencode(content);
		        document.getElementById("message").innerHTML += "<font color=blue>我</font> " + hour + ":" + minute + ":" + second + "<BR />" + decode(sendcontent) + "<BR />"; doPrompt("end");
		        document.getElementById('message').scrollTop = document.getElementById('message').scrollHeight;
		    }
		}
	);
}

function deencode(obj) {
    return encode(unescape(obj.replace(/\\u/g, '%u').replace(/\+/g, ' ')));
}

function encoded(obj) {
    return obj.replace(/[^\u0000-\u00FF]/g, function ($0) { return escape($0).replace(/(%u)(\w{4})/gi, "\\u$2") });
}   

function encode(string) {
    string = string.replace(/\r\n/g, "\n");
    var utftext = "";

    for (var n = 0; n < string.length; n++) {

        var c = string.charCodeAt(n);

        if (c < 128) {
            utftext += String.fromCharCode(c);
        }
        else
            if ((c > 127) && (c < 2048)) {
                utftext += String.fromCharCode((c >> 6) | 192);
                utftext += String.fromCharCode((c & 63) | 128);
            }
            else {
                utftext += String.fromCharCode((c >> 12) | 224);
                utftext += String.fromCharCode(((c >> 6) & 63) | 128);
                utftext += String.fromCharCode((c & 63) | 128);
            }
    }
    return utftext;
}


function decode(utftext) {
    var string = "";
    var i = 0;
    var c = c1 = c2 = 0;

    while (i < utftext.length) {

        c = utftext.charCodeAt(i);

        if (c < 128) {
            string += String.fromCharCode(c);
            i++;
        }
        else
            if ((c > 191) && (c < 224)) {
                c2 = utftext.charCodeAt(i + 1);
                string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
                i += 2;
            }
            else {
                c2 = utftext.charCodeAt(i + 1);
                c3 = utftext.charCodeAt(i + 2);
                string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
                i += 3;
            }
    }
    return string;
}



var e_update_ts;
function e_update_1() {
    ajax.update("e_update_r1", "SendApi.aspx?c=update");
}
function e_update_2() {
    e_update_ts = "";
    if (!e_update_ts) e_update_ts = ajax.update("e_update_r2", "SendApi.aspx?c=update", 1000);
}
function e_update_2c() {
    clearInterval(e_update_ts);
}
function e_update_3() {
    ajax.update("e_update_r3", "SendApi.aspx?c=update", 1000, 3);
}