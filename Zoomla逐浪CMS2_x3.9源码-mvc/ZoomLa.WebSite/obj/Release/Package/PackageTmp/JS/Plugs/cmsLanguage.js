var cmsLang = {};
cmsLang.addon = {};
cmsLang.init = function () {
    var ref = this;
    function getCookie(cookie_name) {
        var allcookies = document.cookie; var cookie_pos = allcookies.indexOf(cookie_name); if (cookie_pos != -1) {
            cookie_pos += cookie_name.length + 1; var cookie_end = allcookies.indexOf(";", cookie_pos); if (cookie_end == -1) { cookie_end = allcookies.length; }
            var value = unescape(allcookies.substring(cookie_pos, cookie_end));
        }
        return value;
    }
    var model = getCookie("addon");
    if (!model || model == null || !model.lang) { model = { lang: "en-us" }; }
    ref.addon = model;
}
//当前页面所加载到的语言包
cmsLang.lang = {};
//根据key返回对应的语言
cmsLang.L = function (key) {
    var ref = this;
    try {
        var val = ref.Lang[key];
        return val;
    }
    catch (ex) { return key; }
}
cmsLang.loadLang = function (lang, data) {
    //如果当前选所定的语言与其不符,则不加载
    //后设定的附盖之前设定的
    var ref = this;
    if (ref.addon.lang == lang) { ref.lang = $.extend({}, ref.lang, data); }
}
cmsLang.init();
//---并入
//cmsLang.loadLang("en-us", {
//    "登录": "login",
//    "退出": "cancel",
//});