var global = { lock: false };
var scence = {
    list: [],//场景的数据,order与id均从1开始
    conf: {},//存储场景的总配置,场景的分配置另存(图标,切换方式)
    $body: $("#page_ul"),
    stlp: '<li data-id="@id" id="li_@id" onclick="scence.switch(@id);"><span class="page_num"><span>@_index</span></span><span class="page_handler"><i class="fa fa-arrows-alt"></i></span><input class="page_name" type="text" value="@title" onchange="scence.change(@id,this);"></li>',
    setlp: '<section class="swiper-slide" id="section_@id"><div id="mainBody@id" class="compbody"></div></section>',
    editor: null,
    myani: null,
    getbody: function (id) { if (!id) { id = this.pageid; } return "mainBody" + id; },//当前的body计算
    pageid: "",
    tempcheck: true,
};
//删除当前页
scence.del = function () {
    var ref = this;
    //如果只有一个页面,则不可移除
    if (ref.list.length <= 1) { alert("起始页不可移除"); return false; }
    if (!confirm("确定要移除该页吗?")) { return false; }
    var id = ref.pageid;
    ref.comp_clear(id);
    //移除指定的场景
    ref.editor.find("#section_" + id).remove();
    //移除页面,并跳至首页
    ref.list.RemoveByID(id, "id");
    ref.$body.find("#li_" + id).remove();
    ref.reorder();
    ref.switch(ref.list[0].id)
}
//清除单个页面的所有组件
scence.comp_clear = function (pageid) {
    var bodyid = "mainBody" + pageid;
    //移除组件
    for (var i = 0; i < page.compList.length; i++) {
        var comp = page.compList[i];
        if (comp.config.bodyid == bodyid) { comp.RemoveSelf(page.compList); }
    }
}
//切换section
scence.switch = function (id) {
    var ref = this;
    var curMod = ref.list.GetByID(ref.pageid);
    var tarMod = null;//需要跳转至的页面
    switch (id) {
        case "pre":
            if (curMod.order <= 1) { return; }
            tarMod = ref.list.GetByID((curMod.order - 1), "order");
            break;
        case "next":
            if (curMod.order >= ref.list.length) { return; }
            tarMod = ref.list.GetByID((curMod.order + 1), "order");
            break;
        default://按id切换
            tarMod = ref.list.GetByID(id);
            break;
    }
    id = tarMod.id;
    ref.$body.find("li").removeClass("active");
    ref.$body.find("#li_" + id).addClass("active");
    ref.pageid = id;
    tools.clear();
    //------场景切换到指定页面
    if (myani.swiper) { myani.swiper.slideTo(tarMod.swipe_index, 1000, true); }
}
//仅重绘右边栏右码,不涉及section,order小的置前
scence.render = function () {
    var ref = this;
    for (var i = 0; i < ref.list.length; i++) {
        var item = ref.list[i];
    }
    ref.list.sort(function (a, b) { return a.order > b.order ? 1 : -1; });//1表示前进一位
    var $items = JsonHelper.FillItem(ref.stlp, ref.list, null);
    ref.$body.html("").append($items);
    ref.$body.find("#li_" + ref.pageid).addClass("active");
}
//在iframe加载完成后--组件加载前调用,用于加载section,并初始化动画JS
scence.init = function () {
    var ref = page.scence = this;
    ref.editor = $(editor.id);
    ref.conf = page.pageData.scence_conf ? JSON.parse(page.pageData.scence_conf) : { effect: "slide", direction: "vertical", autoplay: 0, loop: false };
    if (ref.list.length < 1) { ref.list.push(ref.new()); }
    ref.render();
    //----------添加区域
    for (var i = 0; i < ref.list.length; i++) {
        ref.list[i].swipe_index = i;
    }
    var $se = JsonHelper.FillItem(ref.setlp, ref.list, null);
    $("#editorBody").append($se);
    ref.switch(ref.list[0].id);
    //----------绑定排序事件(修改右边栏页码,section排序)
    //$("#page_ul").sortable({
    //    placeholder: "highlight",
    //    handle: ".page_handler",
    //    cancel: ".ui-state-disabled",
    //    update: function (e,v) {
    //        ref.reorder();
    //    }
    //}).disableSelection();
    //调用绑定动画效果
    setTimeout(function () { myani.init(); }, 200);//在元素展示之后再附加动画效果
}
//修改标题
scence.change = function (id, obj) {
    var ref = this;
    var model = ref.list.GetByID(id);
    model.title = $(obj).val();
}
//将当前页替换为模板页
scence.loadtlp = function (tlpid) {
    var ref = this;
    if (!confirm("确定要加载该模板吗,当前页面的数据将会丢失")) { return false; }
    $.post("/design/scence.ashx?action=loadtlp", { "tlpid": tlpid }, function (data) {
        var model = APIResult.getModel(data);
        if (APIResult.isok(model)) {
            //清空当前页的数据,然后将组件置入
            //删除页面,将页面数据保留,然后重生成一个新的页面
            scence.comp_clear(ref.pageid);
            //载入组件与设置背景
            var _page = JSON.parse(model.result.page);
            _page.bk = JSON.parse(_page.bk);
            var _comp = JSON.parse(model.result.comp);
            for (var i = 0; i < _comp.length ; i++) {
                _comp[i] = JSON.parse(_comp[i]);
                //兼容,只保留第一频的组件
                if (_comp[i].config.bodyid.indexOf("ody1") < 0) { continue; }
                AddComponent(_comp[i]);
            }
            _page.bk[0].pageid = ref.pageid;
            page.bk.set(_page.bk[0]);
        }
        else { console.log("loadtlp", model.retmsg); }
        CloseDiag();
    })
}
//----------Tools
scence.new = function () {
    var ref = this;
    var model = { id: 1, title: "第1页", order: (ref.list.length + 1), swipe_index: ref.list.length };
    var max = ref.list.GetByMax("id");
    if (max != null) {
        model.id = max.id + 1;
        model.title = "第" + model.order + "页";
    }
    return model;
}
//预览动画
scence.preview = function () {
    //去除掉当前section中的动画效果,然后重新再附加上去
    var ref = this;
    var $se = ref.editor.find("#section_" + ref.pageid);
    //重除css风格后再一段时间再加上
    $se.find(".ani").each(function () {
        scence.play(this);
    });
}
//预览单个元素动画效果
scence.play = function (dom) {
    var $dom = $(dom);
    var css = $dom.attr("swiper-animate-effect");
    $dom.removeClass("animated").removeClass(css);
    setTimeout(function () { $dom.addClass("animated").addClass(css); }, 50);
}
//修改当前页面的元素
scence.domonpage = function () {
    comdiag.maxbtn = false;
    comdiag.ShowModal("domonpage.aspx", "组件列表");
}
//根据当前li的顺序,重新生成orderid,删除|添加|拖动后调用
scence.reorder = function () {
    var $lis = $("#page_ul li");
    for (var i = 0; i < $lis.length; i++) {
        var id = $($lis[i]).data("id");
        scence.list.GetByID(id).order = (i + 1);
    }
}
scence.btnclick = function (btn) {
    var $btn = $(btn);
    if ($btn.hasClass("disabled")) {
        console.log("lock");
        $btn.removeClass("disabled");
        scence.lock();
        global.lock = true;
    }
    else {
        console.log("unlock");
        $btn.addClass("disabled");
        scence.unlock();
        global.lock = false;
    }
}
scence.lock = function () {
    myani.swiper.lockSwipes();
    myani.swiper.lockSwipeToNext();
    myani.swiper.lockSwipeToPrev();
}
scence.unlock = function () {
    myani.swiper.unlockSwipes();
    myani.swiper.unlockSwipeToNext();
    myani.swiper.unlockSwipeToPrev();
}
//----------------------------背景方面操作
var diy_bk = { $body: $("#diy_bk_body") };
//设置背景,如无的话,即显示弹窗
diy_bk.set = function () {
    if (page.bk.has(scence.pageid)) { console.log("12"); diy_bk.$body.show(); }
    else { diy_bk.diag(); }
}
diy_bk.setbk = function (url) {
    var data = { type: "image", url: url, post: "", pageid: scence.pageid };
    page.bk.set(data);
    CloseDiag();
    diy_bk.close();
}
diy_bk.diag = function () { CloseDiag(); var url = "/design/diag/se_setbk.aspx?pageid=" + scence.pageid; ShowComDiag(url, "设置背景"); diy_bk.close(); }
diy_bk.del = function () {
    var data = { type: "clear", url: "", post: "", pageid: scence.pageid };
    page.bk.clear(data);
    diy_bk.close();
}
diy_bk.cut = function () {
    var url = page.bk.geturl(scence.pageid);
    if (!url) { alert("未指定背景图片"); }
    else { ShowComDiag("/plugins/cropper/se_bk.aspx?url=" + url, "背景裁剪"); }
}
diy_bk.close = function () { diy_bk.$body.hide(); }
//----------------------------组件操作(文本,图片)
function PreSave(callback) {
    page.PreSave(function (data) {
        page.guid = data;
        if (callback) { callback(data); }
    });
}
function CloseDiag() {
    $(".scence").css({ "transform": 'scale(1)', "transform-origin": '', "-webkit-transform": 'scale(1)' });
    mbh5.pop.hide(); mask.hide(); mbh5.foot.hide(); mbh5.comp.text.hide();
}
function ShowEditDiag(url, param) { mbh5.pop.show(url); }
function AddComponent(model) {
    mbh5.comp.add(model);
}
function CopyComp(comp) {
    var model = ZLDE.utils.clone(comp, model);
    var compObj = page.GetCompObj({ "dataMod": model.dataMod, "config": model.config });
    editor.scope.addDom(compObj);
    page.compList.push(compObj);
}
function hideDragEl() {
    $(editor.id).find('.el-menu').remove();
    $(editor.id).find('.press-menu').remove();
    dragEl.hide();
}
//========================================editor
var editor = { id: "#editorBody", app: null, scope: null, footscope: null, compile: null, diag: null };
var dragEl, targetEl;  //拖拽元素 、 拖拽的目标元素
editor.ShowDiag = function (url, diagParam) { ShowEditDiag(url, diagParam); }
//------------------------
editor.app = angular.module("app", [], function ($compileProvider) { })
    .controller("appCtrl", function ($scope, $compile) {
        editor.scope = $scope;
        $scope.list = {};
        //添加前检测同名元素,有同名元素存在且不为null,则取消添加
        $scope.addDom = function (compObj) {
            if ($scope.list[compObj.id]) { console.log("[" + compObj.id + "]取消添加,有重名元素存在"); return; }
            $scope.list[compObj.id] = compObj;
            var html = compObj.AnalyToHtml({ mode: "design" });
            var template = angular.element(html);
            compObj.SetInstance($compile(template)($scope), document);
            angular.element(document.getElementById(compObj.config.bodyid)).append(compObj.instance);
            $(".comp_wrap").removeClass("active");
            compObj.instance.addClass("active");
            compObj.instance.css("position", "absolute");
        }
        //元素需要另外清零,或元素一个指向其
        $scope.delDom = function (name) {
            if ($scope.list[name]) {
                $scope.list[name].instance.remove();
                delete $scope.list[name];
            }
        }
    })
    .controller("footCtrl", function ($scope, $http) {
        //------------animate
        //animap.list.forEach(function (a, v, t) {
        //    a.img = "/design/res/mbh5/aniimg/ani_clear_animation.png"
        //});
        editor.footscope = $scope;
        $scope.ani = {};
        animap.list.unshift({ effect: "", name: "无动画" });
        $scope.ani.list = animap.list;
        $scope.ani.isactive = function (item) {
            var effect = "";
            if (!tools.comp || !tools.comp.config.animate) { }
            else { effect = tools.comp.config.animate.effect; }
            return (effect == item.effect)
        }
        $scope.ani.set = function (item) {
            var comp = tools.comp;
            var animate = comp.config.animate;
            if (!animate) {
                animate = { enabled: true, duration: 1, delay: 0, effect: "", count: 1 };
            }
            comp.config.animate = animate;
            //--清除已动画效果
            var css = comp.instance.attr("swiper-animate-effect");
            comp.instance.removeClass("animated").removeClass(css);
            //--保存新动画效果
            comp.config.animate.effect = item.effect;
            comp.SetAnimate();
            scence.play(comp.instance);
        }
        //------------pagelist
        //$scope.scence.list = scence.list;
        $scope.scence = {};
        $scope.scence.isactive = function (item) { return scence.pageid == item.id; }
        $scope.scence.switch = function (item) { scence.switch(item.id); }
        $scope.scence.getbk = function (item) {
            return $scope.scence.getbkbyid(item.id);
            //if (page.bk.has(item.id)) { return page.bk.geturl(item.id); }
            //else { return ""; }
            //else { return "/Images/nopic.gif"; }
            //根据item返回pk
        }
        $scope.scence.getbkbyid = function (id) {
            if (page.bk.has(id)) { return page.bk.geturl(id); }
            else { return ""; }
        }
        $scope.scence.add = function () {
            var ref = scence;
            var model = ref.new();
            var $se = JsonHelper.FillItem(ref.setlp, model);
            myani.swiper.appendSlide($se);
            $scope.scence.list.push(model);
            CloseDiag();
        }
        $scope.scence.del = function () { scence.del(); }
        //--场景排序 pre|next
        $scope.scence.move = function (dir) {
            var curMod = scence.list.GetByID(scence.pageid);
            if (!curMod) return;
            //获取最接近指定元素的对象,目标必须带order,值相等则忽略
            var getnear = function (arr, order, dir) {
                var result = null;
                if (arr.length < 2) { return result; }
                switch (dir) {
                    case "pre":
                        for (var i = 0; i < arr.length; i++) {
                            var cur = parseInt(arr[i].order);//当前要对比的order
                            if (cur < order && (!result || cur > result.order)) {
                                result = arr[i];
                            }
                        }
                        break;
                    case "next":
                        for (var i = 0; i < arr.length; i++) {
                            var cur = parseInt(arr[i].order);//当前要对比的order
                            if (cur > order && (!result || cur < result.order)) {
                                result = arr[i];
                            }
                        }
                        break;
                    default:
                        console.log("getnear:" + dir + "错误");
                        break;
                }
                return result;
            }
            var nearMod = getnear(scence.list, curMod.order, dir);
            var temp = nearMod.order;
            nearMod.order = curMod.order;
            curMod.order = temp;
        }
        $scope.scence.canmove = function (dir) {
            var curMod = scence.list.GetByID(scence.pageid);
            if (!curMod) return true;
            switch (dir) {
                case "pre":
                    return curMod.order <= 1;
                case "next":
                default:
                    var max = scence.list.GetByMax("order");
                    return curMod.order >= max.order;
            }
        }
        //------------domlist|bklist

        //------------ele元素列表
        $scope.ele = {};
        $scope.ele.list = [];
        $scope.ele.show = function (type) {
            scence.tempcheck = false;
            mbh5.foot.show(170, 'elelist')
            $scope.ele.list = [];
            $scope.ele.list.unshift({ src: $scope.scence.getbkbyid(scence.pageid), name: "背景", showname: true, showimg: true, type: "bk" });
            var img_index = 1;
            var txt_index = 1;
            //遍历场景元素
            for (var i = 0; i < page.compList.length; i++) {
                var comp = page.compList[i];
                //只获取当前页元素
                if (comp.config.bodyid == scence.getbody()) {
                    //筛选，全部|图片|文字
                    if (type && type != comp.config.type) { continue; }
                    //根据不同的元素类型，加入list
                    switch (comp.config.type) {
                        case "image":
                            //showdel:是否显示删除按钮 , showimg|showtxt:显示图片|文字
                            $scope.ele.list.push({ src: comp.dataMod.src, name: "图片" + img_index, showdel: true,  showimg: true, type: "ele", id: comp.id });
                            img_index++;
                            break;
                        case "text":
                            $scope.ele.list.push({ name: "文字" + txt_index, showdel: true, showtxt: true,  type: "ele", txt: comp.dataMod.text, id: comp.id });
                            txt_index++;
                            break;
                    }
                }
            }
        };
        $scope.ele.isactive = function (item) {
            if (item.type == "bk") { return !$(".comp_wrap").hasClass("active"); }
            else { return $("#" + item.id).hasClass("active"); }
        }
        //选中事件
        $scope.ele.sel = function (item) {
            scence.tempcheck = false;
            switch (item.type) {
                case "bk":
                    mbh5.foot.show(170, 'setbk');
                    break;
                case "ele":
                    //根据ID找到对应元素触发点击事件
                    $("#" + item.id).click();
                    break;
            }
        }
        //删除元素
        $scope.ele.del = function (item, $index) {
            $scope.ele.sel(item);
            //同时删除list内数据
            if (tools.del()) { console.log("isdel"); $scope.ele.list.splice($index, 1); }

        }
        //------------
        FastClick.attach(document.body);
        EventBind();
    })
    .filter("html", ["$sce", function ($sce) { return function (text) { return $sce.trustAsHtml(text); } }]);
//----------------集成动画效果
var myani = { swiper: null };
myani.init = function () {
    myani.swiper = new Swiper('.swiper-container', {
        direction: 'vertical',
        loop: false,
        mousewheelControl: true,
        simulateTouch: true,
        //pagination: '.swiper-pagination',
        onInit: function (swiper) {
            swiperAnimateCache(swiper);
            swiperAnimate(swiper);
        },
        onSlideChangeStart: function () { },
        onSlideChangeEnd: function (swiper) {
            clearSwiperAnimate();//兼容iphone
            swiperAnimateCache(swiper);//必须加这句,否则修改过的style无效
            swiperAnimate(swiper);
            //更新pageid
            var model = scence.list.GetByID(swiper.activeIndex + "", "swipe_index");
            scence.pageid = model.id;
            tools.clear();
        },
        onTransitionEnd: function (swiper) {

        }
    });
}
//------------------------
function EventBind() {
    $(".compbody a").each(function () { $(this).click(function () { window.event.returnValue = false; }); });
    $(editor.id).on('click', '.comp_wrap', function (event, istrigger) {
        if (scence.tempcheck) { CloseDiag(); }
        else { scence.tempcheck = true; }
        var _this = $(this),
            _ani = _this.find('.comp_contain');
        $(editor.id).find('.el-menu').remove();
        $(editor.id).find('.press-menu').remove();

        var getTransform = function (style, name) {
            if (!style || style.indexOf(name) < 0) { return ""; }
            //开始获取其中的值transform: name();
            var str = style.split(name + "(")[1];
            str = str.substr(0, str.indexOf(")"));//第一个)
            str = str.replace("deg", "");
            return str;
        }
        rotdegZ = getTransform(_ani.attr("style"), "rotateZ") || 0;

        dragEl.css({
            "width": _this.width(),
            "height": _this.height(),
            "top": _this[0].offsetTop,
            "left": _this[0].offsetLeft,
            "transform": 'rotateX(0deg) rotateY(0deg) rotateZ(' + rotdegZ + 'deg)',
            "-webkit-transform": 'rotateX(0deg) rotateY(0deg) rotateZ(' + rotdegZ + 'deg)',
        });
        _this.after(dragEl);
        dragEl.show();
        targetEl = _this;
        if (istrigger != 1) { dragEl[0].dragElement.onTap(); }

        $(".comp_wrap").removeClass("active");
        _this.addClass("active");
        tools.comp = page.compList.GetByID(_this.attr("id"));
        tools.show();
        event.stopPropagation();
    }).on('click', '.dragel', function (event) {
        event.stopPropagation();
    })
}
//-------------------------浮动工具栏菜单(统一读其选定的comp)
var tools = { comp: null, menu: $("#toolsmenu_div") };
tools.up = function () {
    var zindex = Convert.ToInt(tools.comp.instance.css("z-index"), 0);
    tools.comp.instance.css("z-index", (zindex + 1) + "");
}
tools.down = function () {
    var zindex = Convert.ToInt(tools.comp.instance.css("z-index"), 0);
    tools.comp.instance.css("z-index", (zindex - 1));
}
tools.bind = function () {
    //弹出窗口,绑定超链接或JS,或pop弹出
    editor.ShowDiag("/design/diag/BindEvent.aspx?id=" + tools.comp.id);
}
tools.del = function () {
    if (!confirm("确定要移除吗?")) { return false; }
    if (!tools.comp) { return false; }
    tools.comp.RemoveSelf(page.compList);
    tools.clear();
    hideDragEl();
    return true;
}
tools.animate = function () {
    //mbh5.pop.show("/design/diag/common/animate.aspx?id=" + tools.comp.id);
    mbh5.foot.show(170, "addani");
}
tools.edit = function () {
    mbh5.comp.edit();
}
tools.common = function () { editor.ShowDiag("/design/diag/common/edit.aspx?id=" + tools.comp.id); }
tools.clear = function () {
    tools.comp = null;
    tools.menu.hide();
    hideDragEl();
}
//显示工具条
tools.show = function () {
    if (!tools.comp) { tools.menu.hide(); }
    var dom = tools.comp.instance;
    tools.menu.show();
    var left = dom.offset().left + 70;
    var top = dom.offset().top - 50;
    if (left < 60) { left = 60; }
    if (left > 465) { left = 465; }
    if (top < 18) { top = 18; }
    if (top > 970) { top = 970; }
    tools.menu.css("top", top).css("left", left);
}
//------------------------------上传
tools.up = { $up: $('<input id="pic_up" type="file" onchange="tools.up.change();" accept="image/*" style="display:none;">') };
tools.up.sel = function () { tools.up.$up.click(); }
tools.up.change = function (callback) {
    var ref = tools.up;
    tools.comp.instance.find("img").attr("src", "/design/res/img/spinner.jpg");
    if (ZL_Regex.isEmpty(ref.$up.val())) { return; }
    SFileUP.AjaxUpFile(ref.$up[0], function (data) {
        tools.comp.dataMod.src = data;
        editor.scope.$digest();
    });
}
//------------------------------
var mbh5 = { scence: {}, comp: {}, music: {}, pop: {}, foot: {}, tlp: {} };
mbh5.preview = function () {
    mask.wait();
    PreSave(function () {
        mbh5.pop.show("/design/h5/preview.aspx?id=" + page.guid);
        setTimeout(mask.hide, 500);
    });
}
mbh5.backToEdit = function () {
    $("#pop_div").slideUp("middle");
}
mbh5.backToList = function () {
    location.href = "TlpShop.aspx";
}
//------------------------------
mbh5.pop.show = function (url) {
    mask.wait();
    var after = function ($ifr) { mask.hide(); $(".popifr").hide(); $ifr.show(); $("#pop_div").show(); }
    switch (url) {
        case "music":
            var $ifr = $("#music_ifr");
            var src = $ifr.attr("src");
            if (!src || src == "") {
                $ifr[0].onload = function () { after($ifr); }
                $ifr.attr("src", "/design/diag/mb_music.aspx");
            } else { after($ifr); }
            break;
        case "edit":
            var $ifr = $("#edit_ifr");
            $ifr[0].onload = function () { after($ifr); }
            $ifr.attr("src", "/design/mbh5/EditScence.aspx?id=" + page.guid);
            break;
        default:
            var $ifr = $("#pop_ifr");
            $ifr[0].onload = function () { after($ifr); };
            $ifr.attr("src", url);
            break;
    }
}
mbh5.pop.hide = function () {
    $("#pop_div").slideUp("middle");
}
//------------------------------底部菜单栏,缩放界面
mbh5.foot.show = function (bot, type) {
    CloseDiag();
    var _height = phoneHeight - bot;
    var _scale = _height / 570;
    $(".compbody").css({ "transform": 'scale(' + _scale + ')', "-webkit-transform": 'scale(' + _scale + ')' });
    //var _scale = 530 / _height;
    //$(".scence").css({ "transform": 'scale(' + _scale + ')',"transform-origin": 'center 5px', "-webkit-transform": 'scale(' + _scale + ')' });
    if (type != 'elelist') { mask.show(); }
    switch (type) {
        case "addpage":
            $("#addpage_div").show();
            break;
        case "addani":
            $("#addani_div").show();
            break;
        case "addtlp":
            $("#addtlp_div").show();
            break;
        case "setbk":
            $("#setbk_div").show();
            break;
        case "addcomp":
            $("#addcomp_div").show();
            break;
        case "elelist":
            $("#elelist_div").show();
            break;
        default:
            console.log("[" + type + "]footcmd未命中");
            break;
    }
    if (scence.tempcheck) { editor.footscope.$digest(); }
    else { scence.tempcheck = true; }
}
mbh5.foot.hide = function () {
    $(".compbody").css({ "transform": 'scale(1)', "-webkit-transform": 'scale(1)', });
    $(".footcmd").hide();
}
mbh5.tlp.showadd = function () {
    mbh5.foot.show(170, "addtlp");
}
//------------------------------对公用逻辑的再封装,仅用于移动端
var mask = {};
mask.show = function () { $("#mask_div").show(); }
mask.wait = function () { $("#mask_wait_div").show(); }
mask.hide = function () {
    $("#mask_div").hide();
    $("#mask_wait_div").hide();
    $(".footcmd").hide();
}
//------------------------------
//显示底部添加组件栏
mbh5.comp.showadd = function () {
    mbh5.foot.show(170, "addcomp");
    mask.show();
}
mbh5.comp.add = function (model) {
    var compObj = page.GetCompObj(model);
    compObj.config.bodyid = scence.getbody();
    editor.scope.addDom(compObj);
    page.compList.push(compObj);
}
mbh5.comp.addtext = function () {
    var ref = this;
    var model = { "id": "", "dataMod": { "text": "网站标题" }, "config": { "type": "text", "compid": "comp1", "css": "candrag", "style": "font-size:60px;top:420px;left:200px;", "bodyid": "" } };
    ref.add(model);
    mask.hide();
}
mbh5.comp.addimg = function () {
    var ref = this;
    var model = { "dataMod": { "src": "/UploadFiles/timg.jpg" }, "config": { "type": "image", "compid": "image", "css": "candrag", "style": "top:420px;left:200px;", "imgstyle": "width:300px;height:300px;" } };
    ref.add(model);
    mask.hide();
}
mbh5.comp.addbtn = function () {
    var ref = this;
    var model = { "dataMod": { "value": "操作按钮", "html": "" }, "config": { "type": "button", "compid": "", "css": "candrag btn btn-info", "style": "top:420px;left:200px;width:100px;height:50px;" } };
    ref.add(model);
    mask.hide();
}
mbh5.comp.edit = function () {
    var ref = mbh5.comp;
    switch (tools.comp.config.type) {
        case "image":
            tools.up.sel();
            break;
        case "text":
            ref.text.show();
            break;
    }
}
mbh5.comp.text = {};
mbh5.comp.text.show = function () { $("#txt_div").show(); $("#txt_editor").val(tools.comp.dataMod.text); $("#txt_editor").focus(); }
mbh5.comp.text.hide = function () { $("#txt_div").hide(); }
mbh5.comp.text.save = function () {
    tools.comp.dataMod.text = $("#txt_editor").val();
    editor.scope.$digest();
    this.hide();
}
//------------------------------
//显示弹窗,让用户试听和选择音乐
mbh5.music.showadd = function () {
    mbh5.pop.show("music");
}
//----------------------------手势拖动
//重置dragEl的宽高
function resetDragEl() {
    var _this = $("#editorBody").find('.active'),
        _ani = _this.find('.comp_wrap'),//内再置一层comp_rot用于避免与动画冲突
        rotdegZ = _ani.attr("rotdegz") || 0;

    dragEl.css({
        "width": _this.width(),
        "height": _this.height(),
        "top": _this[0].offsetTop,
        "left": _this[0].offsetLeft,
        "transform": 'rotateX(0deg) rotateY(0deg) rotateZ(' + rotdegZ + 'deg)',
        "-webkit-transform": 'rotateX(0deg) rotateY(0deg) rotateZ(' + rotdegZ + 'deg)',
    });

}
dragEl = $('<div class="dragel"><div class="dragel_inner"></div></div>');
dragEl.dragElement();