define(function (require, exports, module) {
    var $ = require("jquery");
    var ZLArray = require("array");
    var APIResult = require("APIResult");
    var page = {
        apiurl: "/design/scence.ashx",
        instance: null,//指向页面实例,iframe.load中-->$(this).contents();
        guid: "",
        scence: { list: null },
        //mode: "new",//new,edit,preview(disuse)
        compList: [],//组件实体,作为最终保存对象,初始化时对其完成赋值(main)
        compData: [],//组件数据(仅用于初始化时解析)
        comp_global: [],//全局组件,不保存(或单独保存???)
        pageData: {},
        labelArr: "",
        extendData: [],//现用于存标签模块的数据
        GetCompObj: function (model, callback) {
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
                case "pub_button":
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
                case "pub_input":
                    compObj = new (new require("pub_input")())();
                    break;
                case "pub_select":
                    compObj = new (new require("pub_select")())();
                    break;
                default:
                    alert(model.config.type + "未命中");
                    break;
            }
            model.mode = page.mode;
            compObj.Init(model);//{dataMod:{},config:{}}
            return compObj;
        },
        init: function () {
            var ref = this;
            //加载数据
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
                plug.data = ref.pageData[plug.name];
                if (plug.data) { plug.data = JSON.parse(plug.data); }
                plug.init();
            }
        },
        PreSave: function (callback) {
            var ref = this;
            var compArr = [];
            for (var i = 0; i < page.compList.length; i++) {
                var comp = page.compList[i];
                if (comp.config._conf && comp.config._conf.type == "global") { continue; }
                var data = page.compList[i].PreSave(page);
                compArr.push(angular.toJson(data));
            }
            ref.pageData = {};
            //存储插件数据
            for (var i = 0; i < ref.plugs.length; i++) {
                var plug = ref.plugs[i];
                ref.pageData[plug.name] = plug.presave();
            }
            //为兼容,数据存入pageData中
            ref.pageData.scence_conf = JSON.stringify(scence.conf);
            //整合存储模型
            var saveMod = { guid: page.guid, "labelArr": ref.labelArr, "page": JSON.stringify(ref.pageData), "comp": JSON.stringify(compArr), "scence": JSON.stringify(scence.list) };
            $.post(ref.apiurl + "?action=save", { "model": JSON.stringify(saveMod) }, function (data) {
                var model = APIResult.getModel(data);
                if (APIResult.isok(model)) { callback(model.result); }
                else { console.log("保存报错:" + model.retmsg); }
            });
        }
    };
    //统一从page中加载数据,根所拥有的对其填充(后期改为原型方式)
    page.plugs = [];
    //挂载音乐插件(pc上可自动播,手机上需要点击后触发)
    page.music = { data: { src: "" }, name: "music", $btn: $('<div class="bgm-btn pause" id="music_btn"></div>'), $audio: $('<audio id="bgm" src="" loop="" style="display: none; width: 0; height: 0;"></audio>') };
    page.music.init = function () {
        var ref = this;
        if (ref.data && ref.data.src != "") { ref.set(ref.data); }
    }
    //添加后默认启动播放
    page.music.set = function (data) {
        var ref = this;
        if (page.instance.find("#music_btn").length < 1) {
            page.instance.find("#editorBody").after(ref.$btn);
            //page.instance.find("#editorBody").after(ref.$audio);
            ref.$btn[0].onclick = function () {
                //播放或停止音乐
                if (ref.$btn.hasClass("pause")) {
                    ref.$btn.removeClass("pause");
                    ref.$audio[0].play();
                }
                else { ref.$btn.addClass("pause"); ref.$audio[0].pause(); }
            };
        }
        page.music.data = data;
        page.music.$audio.attr("src", data.src);
        //手机下必须手动触发
        //if (ref.$btn.hasClass("pause")) { ref.$btn.trigger("click"); }
    }
    page.music.clear = function () {
        page.instance.find("#music_btn").remove();
        page.music.data = null;
        page.music.$audio.attr("src", "");
        //page.music.$audio.removeAttr("autoplay");
    }
    page.music.presave = function () { var ref = this; return JSON.stringify(ref.data); }
    //挂载背景插件
    page.bk = { data: [], name: "bk" };
    page.bk.init = function () {
        var ref = this;
        if (ref.data && ref.data.length > 0) {
            for (var i = 0; i < ref.data.length; i++) {
                ref.set(ref.data[i]);
            }
        } else { ref.data = []; }
    }
    page.bk.set = function (data) {
        if (!data || !data.type || !data.pageid) { console.log("背景参数不正确,取消设定", data); return; }
        var ref = this;
        //根据pageid定位与切换数据
        var has = false;
        for (var i = 0; i < ref.data.length; i++) {
            if (ref.data[i].pageid == data.pageid) { ref.data[i] = data; has = true; }
        }
        if (has == false) { ref.data.push(data); }
        var $se = page.instance.find("#section_" + data.pageid);
        switch (data.type) {
            case "image":
                if (data.url != "") {
                    $se.css("background", "url(" + data.url + ")");//$(this).css("background-image","url(on.jpg)");
                    $se.css("background-repeat", "no-repeat")
                    $se.css("background-size", "cover");
                }
                else {
                    $se.css("background", "");
                }
                break;
            case "color":
                if (data.color != "") {
                    $se.css("background-image", "");
                    $se.css("background-color", data.color)
                }
                else {
                    $se.css("background-color", "");
                }
                break;
            case "clear":
                $se.css("background", "");
                $se.css("background-color", "");
                break;
            default:
                console.log("[" + data.type + "]类型错误");
                break;
        }

    }
    page.bk.clear = function (data) {
        var ref = this;
        ref.set(data);
    }
    //获取当前的背景图片URL
    page.bk.geturl = function (pageid) {
        var $se = page.instance.find("#section_" + pageid);
        var bk = $se.css("background");
        if (bk.indexOf("url(") < 0) { return ""; }
        var url = bk.split("url(")[1].split(")")[0];
        //取出来的可能是http://路径,转为虚拟路径
        url = url.replace("://", "").replace(/\"/g, "");
        url = url.substring(url.indexOf("/"), url.length);
        return url;
    }
    //指定的page是否有设定背景
    page.bk.has = function (pageid) {
        //var $se = page.instance.find("#section_" + pageid);
        //var bk = $se.css("background");
        //return bk.indexOf("url(") > 0;
        var data = page.bk.data.GetByID(pageid, "pageid");
        return (data && data.type != "clear");
    }
    page.bk.presave = function () { var ref = this; return JSON.stringify(ref.data); }
    //挂载抹除插件
    page.eraser = { data: { src: "", prog: 0, opacity: 1, able: false }, name: "eraser", $img: $('<img id="eraser">') }
    page.eraser.init = function (data) {
        var ref = this;
        if (ref.data && ref.data.able) { ref.set(ref.data); }
    }
    page.eraser.set = function (data) {
        var ref = this;
        if (page.instance.find('#eraser').length < 1) {
            page.instance.find('.swiper-container.scence:not(.editor)').before(ref.$img);
            ref.$img.attr("src", data.src);
            $("#eraser").eraser({
                opacity: data.opacity, progressFunction: function (p) {
                    if ((p * 100) > data.prog) { $("#eraser").remove(); }
                }
            });
        }
        ref.data = data;
    }
    page.eraser.presave = function () { var ref = this; return JSON.stringify(ref.data); }
    //----------------------
    page.plugs.push(page.bk);
    page.plugs.push(page.music);
    page.plugs.push(page.eraser);
    module.exports = page;
});