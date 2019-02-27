var ZL_Regex = {
    isMinLen: function (str, len) {//字符长度是否小于len
        str = str.replace(/ /g, "");
        return str.length < len;
    },
    isEmpty: function () {
        for (var i = 0; i < arguments.length; i++) {
            if (!arguments[i] || arguments[i] == undefined||!arguments[i].replace ) { return true; }//传入为undefined也为false
            var s = arguments[i].replace(/ /g, ""); 
            if (s == "") return true;
        }
        return false;
    },
    //是否为pop或smtp地址
    isPop: function (s) {
        var patrn = /^(\w+)\.(\w+)\.(\w+)/g;
        return patrn.exec(s) ? true : false;
    },
    //是否包含中文
    isContainChina: function (s) {
        var patrn = /[\u4E00-\u9FA5]|[\uFE30-\uFFA0]/gi;
        if (patrn.exec(s))
            return true;
        else
            return false;
    },
    //是否只有英文与数字
    isEngorNum: function (s) {
        var patrn = /^[A-Za-z0-9]*$/;
        return patrn.exec(s) ? true : false;
    },
    //是否为英文,数字或中文(不允许空格)
    isCharorNum: function (s) {
        var patrn = /^([\u4E00-\uFA29]|[\uE7C7-\uE7F3]|[\w])*$/;
        return patrn.exec(s) ? true : false;
    },
    //是否为c#格式虚拟路径
    isVirtualPath: function (s) {
        return (s.indexOf("~/") == 0 && s.indexOf(".") > 0);
    },
    //验证http||https格式
    isUrl:function(s)
    {
        var patrn = /^((http)|(https)):\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^<>\"\"])*$/i;
        return patrn.exec(s) ? true : false;
    },
    //邮箱格式验证
    isEmail: function (s) {
        var patrn = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/gi;
        return patrn.exec(s)?true:false;
    },
    //是否为身份证
    isIDCard: function (s) {
        var patrn = /(^\d{15}$)|(^\d{17}([0-9]|X)$)/;
        return patrn.exec(s);
    },
    //是否手机号码
    isMobilePhone:function(s){
        var patrn = /^1(?:3|4|5|7|8)\d{9}$/;
            if (patrn.exec(s)) {
                return true;
            }
            else {
                return false;
            }
    },
    //是否邮编
    isZipCode: function (s) {
        s = $.trim(s);
        return(ZL_Regex.isInt(s) && s.length == 6)
    },
    isIP: function (s) {
        var patrn = /^(\d+)\.(\d+)\.(\d+)\.(\d+)$/g;
        return patrn.exec(s) ? true : false;
    },
    //是否正或负整数
    isInt: function (s) {
        var patrn = /^\d+(\d+)?$/gi;
        if (patrn.exec(s)) {
            return true;
        }
        else {
            return false;
        }
    },
    isQQ: function (s) {
        var patrn = /^\d{5,15}$/;
        if (patrn.exec(s)) {
            return true;
        }
        else {
            return false;
        }
    },
    //是否正浮点数或正整数,0也算,true:是
    isNum: function () {
        for (var i = 0; i < arguments.length; i++) {
            var val = parseFloat(arguments[i]);
            if (!(val >= 0)) { return false; }
        }
        return true;
    },
    //后缀名检测,符合返回true  文件名, mp3,mp4,swf
    extCheck: function (fname, exts) {
        if (!fname || fname == "" || !exts || exts == "") { return false; }
        var ext = StrHelper.getExt(fname);
        var extArr = exts.toLowerCase().split(',');
        if (ext == "" || extArr.length < 1) { return false; }
        for (var i = 0; i < extArr.length; i++) {
            if (ext == extArr[i]) { return true; }
        }
        return false;
    },
    domIsEng: function (s) {
        //---域名注册使用
        //---允许英文,数字,空格，不能有中文，英文中一定要带空格,注:传入的值用trim();,用于单位(英文名)等地方
        var patrn = /[^a-zA-Z0-9\s]+/gi;
        if (!patrn.exec(s)) {
            i = s.toString().indexOf(" ");
            return i > 0;
        }
        else {
            return false;
        }
    },
    //---------------文本框块
     //只允许输入正整数
     B_Num: function (filter) {
         $(filter).bind("keyup afterpaste", function (e) {
            if (ZL_Regex._isIgnKey(e)) { return; }
            this.value = this.value.replace(/\D/g, '');
        })
     },
     //允许小数位,允许两位
     B_Float: function (filter) {
         $(filter).bind("keyup afterpaste", function (e) {
             if (ZL_Regex._isIgnKey(e)) { return; }
             this.value = this.value.replace(/[^\d.]/g, "");  //清除“数字”和“.”以外的字符  
             this.value = this.value.replace(/^\./g, "");  //验证第一个字符是数字而不是. 
             this.value = this.value.replace(/\.{2,}/g, "."); //只保留第一个. 清除多余的.   
             this.value = this.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
             this.value = this.value.replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3'); //只能输入两个小数
         })

     },
     //设定最大和最小值
     B_Value: function (filter, config) {
         //config = { min: 0, max: 200, overmin: function () { }, overmax: function () { }}
         $(filter).bind("keyup afterpaste", function (e) {
             if (ZL_Regex._isIgnKey(e)) { return; }
             var val = parseFloat($(this).val());
             if (val > config.max) { $(this).val(config.max); if (config.overmax) { $(this).blur(); config.overmax(); } }
             if (val < config.min) { $(this).val(config.min); if (config.overmin) { $(this).blur(); config.overmin(); } }
         })
     },
    //只允许输入英文
     B_English: function (filter) {
         $(filter).bind("keyup afterpaste", function (e) {
             if (ZL_Regex._isIgnKey(e)) { return; }
             this.value = this.value.replace(/[^a-z]+/gi, "");///[^a-z0-9]+/gi
         });
     },
    //英文和数字
     B_EngAndNum: function (filter) {
         $(filter).bind("keyup afterpaste", function (e) {
             if (ZL_Regex._isIgnKey(e)) { return; }
             this.value = this.value.replace(/[^a-z0-9]+/gi, "");
         });
     },
     _isIgnKey: function (e) {
         //方向键,删除,回车和其他特殊按钮忽略
         var ignKey = [37, 38, 39, 40, 13, 20, 16, 17, 18, 8];
         for (var i = 0; i < ignKey.length; i++) {
             if (e.keyCode == ignKey[i]) { return true; }
         }
         return false;
     },
    //---------------取值
}
//------------------------------
var Convert = {};
Convert.ToInt = function (str, def) {
    if (!def) { def = 0; }
    var r = parseInt(str);
    if (isNaN(r)) { r = def; }
    return r;
}
//num=小数位
Convert.ToDouble = function (str, num, def) {
    if (!def) { def = 0; }
    if (!num) { num = 2; }
    var r = parseFloat(str);
    if (isNaN(r)) { r = def; }
    return r.toFixed(num);
}
//格式化时间
Convert.ToDate = function (str) {
    str = str.split('.')[0];
    str = str.replace("T", " ");
    return str;
}
//------------------------------
var StrHelper = {};
//如未前带http,则补上
StrHelper.UrlDeal = function (url) {
    if (!url || url == "") { return ""; }
    url = url.toLowerCase();
    if (url.indexOf("http:") > -1 || url.indexOf("https:") > -1) {
    }
    else {
        url = "http://" + url;
    }
    return url;
}
//获取指定长度的字符串,区分中英文
StrHelper.getSubStr = function (s, l) {
    var i = 0, len = 0;
    for (i; i < s.length; i++) {
        if (s.charAt(i).match(/[^\x00-\xff]/g) != null) {
            len += 2;
        } else {
            len++;
        }
        if (len > l) { break; }
    }
    var r = s.substr(0, i);
    r += i > s.length ? "..." : "";
    return r;
};
StrHelper.getExt = function (str) {
    if (!str || str.indexOf(".") < 0) { return ""; }
    str = str.replace(/ /g, "");
    var sindex = str.lastIndexOf(".") + 1;
    return str.substring(sindex, str.length).toLowerCase();
}
//正则表达式使用变量var eval("/"+ch+"/ig");









