//text   |add.html中获取其中的style给予div
//button |add.html获取超链接a中的html,将其作为模板引入
//image  |单独页面
//gallery|data-type判断
//menu   |依据compid,拼接字符串
define(function (require, exports, module) {
    var $ = require("jquery");
    //var arrHelper = require("array");
    var ZLDE = {};
    var utils = ZLDE.utils = {
        /*
        *以给定对象作为原型创建一个新对象
        */
        makeInstance: function (obj) {
            var noop = new Function();
            noop.prototype = obj;
            obj = new noop;
            noop.prototype = null;
            return obj;
        },
        /**
         * 将source对象中的属性扩展到target对象上， 根据指定的isKeepTarget值决定是否保留目标对象中与
         * 源对象属性名相同的属性值。
         **/
        extend: function (t, s, b) {
            if (s) {
                for (var k in s) {
                    if (!b || !t.hasOwnProperty(k)) {
                        t[k] = s[k];
                    }
                }
            }
            return t;
        },
        /**
         * 模拟继承机制， 使得subClass继承自superClass
         **/
        inherits: function (subClass, superClass) {
            var oldP = subClass.prototype,
                newP = utils.makeInstance(superClass.prototype);
            utils.extend(newP, oldP, true);
            subClass.prototype = newP;
            return (newP.constructor = subClass);
        },
        clone: function (source, target) {
            var tmp;
            target = target || {};
            for (var i in source) {
                if (source.hasOwnProperty(i)) {
                    tmp = source[i];
                    if (typeof tmp == 'object') {
                        target[i] = utils.isArray(tmp) ? [] : {};
                        utils.clone(source[i], target[i])
                    } else {
                        target[i] = tmp;
                    }
                }
            }
            return target;
        },
        newGuid: function () {
            var guid = "";
            for (var i = 1; i <= 32; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
                if ((i == 8) || (i == 12) || (i == 16) || (i == 20))
                    guid += "-";
            }
            return guid;
        },
        newRandom: function (len) {
            if (!len) { len = 8; }
            var guid = "";
            for (var i = 1; i <= len; i++) {
                var n = Math.floor(Math.random() * 16.0).toString(16);
                guid += n;
            }
            return guid;
        },
        ConverToInt: function (val, suf) { //默认返回1
            if (!val || val == "") { val = "1"; }
            val = val + "";
            val = val.replace(/ /g, "").replace("px", "").replace("em", "");
            val = parseInt(val);
            if (isNaN(val)) { val = 1; }
            return val;
        },
        //------------------//
        each: function (obj, iterator, context) {
            if (obj == null) return;
            if (obj.length === +obj.length) {
                for (var i = 0, l = obj.length; i < l; i++) {
                    if (iterator.call(context, obj[i], i, obj) === false)
                        return false;
                }
            } else {
                for (var key in obj) {
                    if (obj.hasOwnProperty(key)) {
                        if (iterator.call(context, obj[key], key, obj) === false)
                            return false;
                    }
                }
            }
        },
        //装载脚本进入Head,按顺序加载,过滤重复(现仅music使用),page如果是当前页则可不传
        //问:附加的JS不执行
        LoadJS: function (paths, doc) {
            for (var i = 0; i < paths.length; i++) {
                var path = paths[i];
                var item = function (path, time) {
                    setTimeout(function () {
                        var id = path.replace(/[^a-z0-9]+/gi, "");
                        var old = doc.getElementById(id);
                        if (old) { return; }// oldjs.parentNode.removeChild(old);
                        var obj = doc.createElement("script");
                        obj.id = id;
                        obj.src = path;
                        //obj.type = "text/javascript";
                        doc.getElementsByTagName("head")[0].appendChild(obj);
                    }, time);
                }(path, (i * 300));
            }
        },
        LoadCSS: function (paths, doc) {
            for (var i = 0; i < paths.length; i++) {
                var path = paths[i];
                var item = function (path) {
                    var id = path.replace(/[^a-z0-9]+/gi, "");
                    var old = doc.getElementById(id);
                    if (old) { return; }
                    var obj = doc.createElement("link");
                    obj.id = id;
                    obj.href = path;
                    obj.rel = "stylesheet";
                    doc.getElementsByTagName("head")[0].appendChild(obj);
                }(path);
            }
        }
    };
    utils.each(['String', 'Function', 'Array', 'Number', 'RegExp', 'Object', 'Date'], function (v) {
        utils['is' + v] = function (obj) {
            return Object.prototype.toString.apply(obj) == '[object ' + v + ']';
        }
    });
    /*-------------------------*/
    //以管道的形式初始化,各个事件都可接收事件注入
    //---给予所有控件继承的父类,_为不允许覆盖的
    Control = ZLDE.Control = function () { };
    //初始化前需要执行的操作,例如对模板进行判断(设计模式:浏览模式),并根据compid载入不同的html
    Control.prototype.Init_Pre = function (model, extend) { }
    /**
     * @override组件初始化,第二个extend为附加信息,用于某些控件preview时
     * model:初始化数据,具体交由子类实现
     * extend:preview时部分组件需要该数据
     * 完成数据的请求和html中的替换(id|style)
     */
    Control.prototype.Init = function (model, extend) {
        this._init(model, extend);
    };
    Control.prototype._init = function (model, extend) {
        this.Init_Pre(model, extend);
        this.dataMod = model.dataMod;
        this.config = model.config;
        this.id = this.CreateID();
        this.Init_After();
    }
    //生成完成Html后,对其进行数据填充(GalleryGrid)
    Control.prototype.Init_After = function () { }
    //@override 核心html解析,使用双绑输出,另如果分设计与展示模板,也在此处理
    Control.prototype.AnalyToHtml = function (config) {
        return this._AnalyToHtml(config);
    };
    Control.prototype._AnalyToHtml = function (config) {
        var ref = this;
        var html = "";
        switch (ref.mode) {
            case "design"://设计模式
                html = this.designTlp;
                break;
            default:      //预览模式
                break;
        }
        if (!this.config.contain_style) { this.config.contain_style = ""; }
        var wrapper = ref.wrapper;
        wrapper = wrapper.replace("@contain_style", this.config.contain_style);
        if (html == "") { html = this.htmlTlp; }

        wrapper = wrapper.replace("{html}", html);
        wrapper = wrapper.replace(/@id/g, this.id).replace(/@style/g, this.config.style).replace(/@css/g, this.config.css).replace(/@src/g, this.dataMod.src)
        if (this.config.imgstyle) { wrapper = wrapper.replace("@imgstyle", this.config.imgstyle); }
        return wrapper;
    }
    //为instance赋值,赋值后便于JS方法动态填充内容
    Control.prototype.SetInstance = function (obj, doc) { this._setInstance(obj, doc); }
    Control.prototype._setInstance = function (obj, doc) {
        var ref = this;
        ref.doc = doc;
        ref.instance = obj;
        ref.SetInstance_After();
        ref.UpdateRootPanel();
        //-----------子级继承父级部分css样式(如背景色,前景色)
        ref.instance.find(".comp_contain").children().css("background-color", ref.instance.css("background-color"));
        //-----------动画效果处理
        ref.SetAnimate();
        //-----绑定事件,暂为全支持,后期看是否只有指定组件支持
        //{type:"0",url:"",js:"",disabled:false}
        if (ref.mode != "design") {
            if (ref.dataMod.click && ref.dataMod.click.type != "0") {
                ref.instance.on("click", function () {
                    switch (ref.dataMod.click.type) {
                        case "0":
                            break;
                        case "1"://新窗口超链接
                            window.open(ref.dataMod.click.url);
                            break;
                        case "2"://打开超链接
                            location = ref.dataMod.click.url;
                            break;
                        case "3"://执行JS
                            eval(ref.dataMod.click.js)
                            break;
                        case "4"://场景跳转
                            var index = parseInt(ref.dataMod.click.index) + 1;
                            if (myani.swiper) { myani.swiper.slideTo(index, 1000, true); }
                            break;
                        case "5"://互动表单提交(后期改良)
                            {
                                if (ref.dataMod.click.disbaled) { alert("请不要重复提交"); return; }
                                var GetByID = function (comps, id) {
                                    for (var i = 0; i < comps.length; i++) {
                                        if (comps[i].id == id) { return comps[i]; }
                                    }
                                    return null;
                                }
                                //找到当前页的表单元素,并检测是否符合规范
                                var pubs = ref.instance.closest(".swiper-slide").find(".pub");
                                //sename:所属场景,fname:表单名称
                                var answer = { guid: page.guid, fname: ref.config.fname, content: [] };//name:"",value:""
                                for (var i = 0; i < pubs.length; i++) {
                                    var $input = $(pubs[i]);
                                    var comp = GetByID(page.compList, $input.data("id"));
                                    var regex = comp.config.regex;
                                    var name = comp.config.name;
                                    var value = $input.val();
                                    if (regex) {
                                        if (regex.indexOf("required") > -1 && ZL_Regex.isEmpty(value)) { alert(name + "：不能为空"); return; }
                                        if (regex.indexOf("mobile") > -1 && !ZL_Regex.isMobilePhone(value)) { alert(name + "：手机格式不正确"); return; }
                                        if (regex.indexOf("email") > -1 && !ZL_Regex.isEmail(value)) { alert(name + "：邮件格式不正确"); return; }
                                        if (regex.indexOf("qq") > -1 && !ZL_Regex.isQQ(value)) { alert(name + "：QQ格式不正确"); return; }
                                    }
                                    answer.content.push({ "name": name, "value": $input.val() })
                                }
                                answer.content = JSON.stringify(answer.content);
                                $.post("/design/pub.ashx?action=add", { "model": JSON.stringify(answer) }, function (data) {
                                    APIResult.ifok(data, function (result) { console.log("提交成功"); }, function (data) { console.log("提交失败:" + data); })
                                })
                                ref.dataMod.click.disbaled = true;
                                alert(ref.dataMod.click.prompt);
                            }
                            break;
                    }
                });
            }
        }
    }
    Control.prototype.SetInstance_After = function () { }
    //动画效果,刷新时也调用此
    Control.prototype.SetAnimate = function () {
        var ref = this;
        if (ref.config.animate && ref.config.animate.enabled != false) {
            var animate = ref.config.animate;
            ref.instance.addClass("ani");
            ref.instance.attr("swiper-animate-effect", animate.effect);
            ref.instance.attr("swiper-animate-duration", animate.duration + "s");
            ref.instance.attr("swiper-animate-delay", animate.delay + "s");
            ref.instance.attr("swiper-animate-count", (animate.count == "0" ? "infinite" : animate.count));
            //ref.instance.attr("swiper-animate-iteration-count", animate.count);
            //用于避免手机动画隐藏因为宽度问题不彻底(会导致文字无法竖排)
            //if (ref.config.type == "text") { ref.instance.css("width","100%"); }
        }
    }
    //更新右侧基本属性框(当被选中时触发)
    Control.prototype.UpdateRootPanel = function () {
        if (!this.instance) { console.log("UpdateRootPanel失败", this.instance); return; }
        $("#root_width_t").val(utils.ConverToInt(this.instance.width()));
        $("#root_height_t").val(utils.ConverToInt(this.instance.height()));
        $("#root_x_t").val(utils.ConverToInt(this.instance.css("left")));
        $("#root_y_t").val(utils.ConverToInt(this.instance.css("top")));
    }
    //产生ID,组件名_随机码
    Control.prototype.CreateID = function () {
        var id = this.config.type + "_" + ZLDE.utils.newRandom();
        return id;
    }
    //自我清除,包括dom与自己在数组中的存值
    Control.prototype.RemoveSelf = function (arr) {
        this.instance.remove();
        for (var i = 0; i < arr.length; i++) {
            if (arr[i].id == this.id) { arr.splice(i, 1); break; }
        }
    }
    //回发保存前执行,用于维护好自己 dataMod与config  (仅这两个保存)
    Control.prototype.PreSave = function (page) {
        this._presave();
        return { "dataMod": this.dataMod, "config": this.config };
    }
    Control.prototype._presave = function () {
        //dataMod的全存,config自动保存css与style,其余特殊处理,组件自实现
        if (!this.instance) { console.log("无实例,取消保存config", this); return; }
        //只保存指定的style,其他的全不存(白名单)
        var styleArr = "position top bottom left right width height margin z-index font-size color text-align text-indent opacity background-color transform font-family font-weight".split(' ');
        //var border = "border-style border-color";//暂不保存边框,后期优化(加了会影响到定位)
        var domstyle = this.instance.attr("style").toLowerCase();
        this.config.style = "";
        for (var i = 0; i < styleArr.length; i++) {
            var name = styleArr[i];
            if (domstyle.indexOf(name) < 0) { continue; }//无指定风格直接返回
            var val = this.instance.css(name);
            if (val == "auto") { continue; }
            else { this.config.style += name + ":" + val + ";"; }
        }
        //---------通用处理(黑名单方式)
        //var cssArr = "ui-draggable ui-resizable ng-pristine ng-untouched ng-valid ng-scope ng-binding active ani animated".split(" ");
        //this.config.css = this.instance.attr("class");
        //for (var i = 0; i < cssArr.length; i++) {
        //    this.config.css = this.config.css.replace(cssArr[i], "");
        //}
        this.config.css = "";
        var cleancss = function (css, igncss) {
            var arr = igncss.split(" ");
            for (var i = 0; i < arr.length; i++) {
                css = css.replace(arr[i], "");
            }
            return css;
        }
        var cssArr = cleancss(this.instance.attr("class"), "comp_wrap active ani animated").split(" ");
        //不保存angular与jq插件附加上的css样式
        for (var i = 0; i < cssArr.length; i++) {
            var css = cssArr[i];
            if (css == "" || !css || css.indexOf("ng-") == 0 || css.indexOf("ui-") == 0) { }
            else { this.config.css += css + " "; }
        }
        //暂只用于记录旋转
        this.config.contain_style = this.instance.find(".comp_contain").attr("style");
        if (!this.config.contain_style) { this.config.contain_style = ""; }
    }
    //使和数据重缓控件本身,不兼容含绑定的组件,建议有需要自实现(需要编译后再加入)
    Control.prototype.Render = function () { }
    /*--------------------------------------------------------------------------------*/
    //第一层用于拖动,改变大小,动画,样式修改
    //第二层用于旋转等于动画不兼容的效果,style="transform"
    //第三层实际的html元素
    Control.prototype.wrapper = '<div id="@id" ng-model="list.@id.dataMod" class="@css comp_wrap" style="@style"><div class="comp_contain" style="@contain_style">{html}</div></div>';
    //@override 控件自有html必须重写
    Control.prototype.htmlTlp = "";
    //@override 设计时展示,可为空,为空则自动以htmlTlp为准
    Control.prototype.designTlp = "";
    //支持的菜单cmds,根据此创建对应的条目(暂未使用)
    Control.prototype.cmds = "";
    //存储本身的配置,依此和Data用于Render(该值支持从dom中重获)
    Control.prototype.config = {};
    //需要实时根更的数据,只是暂时保存,使用时需要实时解析出来(label)
    Control.prototype.temp = {};
    //存储用于展示的数据,如图片,文字(该字段的值通过JS直接赋于模型,不通过视图重获)
    Control.prototype.dataMod = {};
    //指向html控件实例(可以跨iframe建立关联)
    Control.prototype.instance = null;
    //---------------------------------------------------------
    // 通过 exports 对外提供接口
    //exports.doSomething = function () {return "";}
    // 通过 module.exports 提供整个接口
    module.exports = ZLDE;
});
//-------自定义扩展属性
//editurl:自定义编辑页面  (gallery_group)