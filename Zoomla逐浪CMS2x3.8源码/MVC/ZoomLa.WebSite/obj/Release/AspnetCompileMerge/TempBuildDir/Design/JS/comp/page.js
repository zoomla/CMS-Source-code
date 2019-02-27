define(function (require, exports, module) {
    var $ = require("jquery");
    var ZLArray = require("array");
    var APIResult = require("APIResult");
    var page = {
        instance: null,//指向页面实例
        guid: "",
        compList: [],//组件实体,作为最终保存对象,初始化时对其完成赋值(main)
        compData: [],//组件数据(仅用于初始化时解析)
        comp_global: [],//全局组件,不保存(或单独保存???)
        pageData: {},
        labelArr: "",
        extendData: [],//现用于存标签模块的数据
        init: function () {
            var ref = this;
            ZLArray.MyArr = ref.extendData;
            //解析组件,支持传字符串或模型
            var parseComp = function (comp) {
                try {
                    if (typeof (comp) == "string") { comp = JSON.parse(comp); }
                    //如果该组件为标签,则特殊处理(找到对应的解析后的html)
                    if (comp.config.type == "label") {
                        var labelMod = ZLArray.GetByID(data.dataMod.guid, "guid");
                        if (labelMod) { data.htmlTlp = labelMod.htmlTlp; }
                    }
                    //构造组件
                    var compObj = ref.GetCompObj(comp);
                    return compObj;
                } catch (ex) { console.log("parseComp error,原因:" + ex.message, comp); }
            }
            //处理页面组件
            for (var i = 0; i < ref.compData.length; i++) {
                var comp = ref.compData[i];
                ref.compList.push(parseComp(comp));
            }
            //处理全局组件
            for (var i = 0; i < ref.comp_global.length; i++) {
                var model = ref.comp_global[i];
                var compData = JSON.parse(model.comp);
                //将全局页的组件解析
                for (var j = 0; j < compData.length; j++) {
                    var comp = JSON.parse(compData[j]);
                    //保存时将其移除并额外处理
                    comp.config._conf = { type: "global", path: model.path };
                    ref.compList.push(parseComp(comp));
                }
            }
            //加载插件数据,处理插件
            for (var i = 0; i < ref.plugs.length; i++) {
                var plug = ref.plugs[i];
                //hasOwnProperty
                plug.data = ref.pageData[plug.name];
                if (plug.data) { plug.data = JSON.parse(plug.data); }
                plug.init();
            }
        },
        GetCompObj: function (model,callback) {
            //依靠其中参数,类型,初始化为对应的对象并返回
            var compObj = null;
            //必须用静态字符串,需要解决方案
            switch (model.config.type) {
                case "div":
                    compObj = new (new require("div")())();
                    break;
                case "text":
                    compObj = new (new require("text")())();
                    break;
                case "image":
                    compObj = new (new require("image")())();
                    break;
                case "ueditor":
                    compObj = new (new require("ueditor")())();
                    break;
                case "button":
                    compObj = new (new require("button")())();
                    break;
                case "list":
                    compObj = new (new require("list")())();
                    break;
                case "content":
                    compObj = new (new require("content")())();
                    break;
                case "menu":
                    compObj = new (new require("menu")())();
                    break;
                case "gallery":
                    compObj = new (new require("gallery")())();
                    break;
                case "gallery_group":
                    compObj = new (new require("gallery_group")())();
                    break;
                case "gallery_slide":
                    compObj = new (new require("gallery_slide")())();
                    break;
                case "galleryGrid":
                    compObj = new (new require("galleryGrid")());
                    break;
                case "label":
                    compObj = new (new require("label")())();
                    break;
                case "video":
                    compObj = new (new require("video")())();
                    break;
                case "music":
                    compObj = new (new require("music")())();
                    break;
                case "social":
                    compObj = new (new require("social")())();
                    break;
                case "map":
                    compObj = new (new require("map")())();
                    break;
                case "progress":
                    compObj = new (new require("progress")())();
                    break;
                case "gallery_photo":
                    compObj = new (new require("gallery_photo")())();
                    break;
                case "shape":
                    compObj = new (new require("shape")())();
                    break;
                default:
                    alert(model.config.type + "未命中");
                    break;
            }
            compObj.Init(model);//{dataMod:{},config:{}}
            return compObj;
        },
        PreSave: function (callback) {
            var ref = this;
            //Converting circular structure to JSON,有递归互引
            //处理:PreSave后返回json即可
            var compArr = [];
            for (var i = 0; i < page.compList.length; i++) {
                var comp = page.compList[i];
                if (comp.config._conf && comp.config._conf.type == "global") { continue; }
                var data = page.compList[i].PreSave(page);
                compArr.push(angular.toJson(data));
            }
            //改为for var 拷贝
            ref.pageData = { title: page.title };
            //存储插件数据
            for (var i = 0; i < ref.plugs.length; i++) {
                var plug = ref.plugs[i];
                ref.pageData[plug.name] = plug.presave();
            }
            //为避免递归循环,浅拷贝
            var saveMod = { guid: page.guid, "labelArr": page.labelArr, "page": JSON.stringify(ref.pageData), "comp": JSON.stringify(compArr) };
            $.post("/design/design.ashx?action=save", { "model": JSON.stringify(saveMod) }, function (data) {
                var model = APIResult.getModel(data);
                if (APIResult.isok(model)) { callback(model.result); }
                else { console.log("保存报错:" + model.retmsg); }
            });
        }
    };
    page.plugs = [];
    //挂载背景插件
    page.bk = { data: null, name: "bk" };
    page.bk.init = function () {
        var ref = this;
        if (ref.data) { ref.set(ref.data); }
    }
    page.bk.set = function (data) {
        var ref = this;
        if (!data) { data = { type: "", url: "", post: "" }; }
        ref.data = data;
        page.instance.find("#page_bkdiv").remove(); //移除旧背景
        var $div = $('<div id="page_bkdiv"></div>');
        switch (data.type) {
            case "minimg":
                {
                    $div.attr("style", 'position:fixed;top:0px;height:100%;width:100%;background-image:url(' + data.url + ');background-size:auto;background-repeat:repeat;z-index:-2;');
                }
                break;
            case "image":
                {
                    $div.attr("style", 'background:url(' + data.url + ');background-position: center;left:0;top:0;right:0;bottom:0;width:100%;height:100%; position: fixed; background-repeat:no-repeat;background-size:cover;z-index:-2;');
                }
                break;
            case "video":
                {
                    $div.attr("style", "position:fixed;top:0;left:0;z-index:-2;background-color:#fff;");
                    var html = '<video class="covervid-video" autoplay loop poster="' + data.post + '">';
                    html += '<source src="' + data.url + '" type="video/mp4">';
                    $div.append(html);
                }
                break;
            case ""://不使用背景
            default:
                break;
        }
        if (data.type != "") { page.instance.find("#editorBody").after($div); }
    }
    page.bk.clear = function () {
        var ref = this;
        ref.set();
    }
    page.bk.presave = function () { var ref = this; return JSON.stringify(ref.data); }
    //----------------------
    page.plugs.push(page.bk);
    module.exports = page;
});