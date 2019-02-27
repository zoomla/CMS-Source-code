var tools = { $dom: null, nav: { swiper: null, init: null }, scope: null };
tools.init = function () {
    var cfg = JSON.parse($("#sitecfg_hid").val());
    if (cfg.edit === true) {
        tools.close();
        Zepto.showPreloader("正在加载编辑模块");
        //绑定修改按钮
        $(".com").each(function () {
            $this = $(this);
            if ($this.css("position") == "static") { $this.css("position", "relative"); }
            //如果是普通定位,则改为relative
            $this.append('<div class="edit" onclick="tools.edit(\'' + $(this).attr("id") + '\')">编辑模块</div>');
        });
        //等待iframe加载完成
        $(".com_edit").show();
        $("#pop_div").hide();
        //如果在JS执行完成前即加载完成,则会无法执行该段,所以放前面绑定
        //$("#product_ifr").attr("src", "/design/mobile/pop/product.aspx");
        //document.getElementById("product_ifr").onload = function () { tools.close(); }
        //判断模式
        if (localStorage["mbdesign"] && localStorage["mbdesign"] == "view") {
            tools.changeMode();
        }
        tools.close();
    }
}
//为scope附加数据处理辅助方法
tools.extend = function ($scope) {
    tools.scope = $scope;
    $scope.getfk = function (color) { return { "color": color }; }
    $scope.getbk = function (color) { return { "background-color": color }; }
    $scope.getlink = function (item) {
        var link = "";
        if (!item.wxlink || item.wxlink == "") { link = "/design/mobile/default.aspx?p=content&gid=" + item.id + "&id=" + tools.site; }
        else { link = item.wxlink; }
        return link;
    }
    $scope.gettime = function (time) {
        return time.split(".")[0].replace("T", " ");
    }
    //--------------------
    $scope.pro = {};
    $scope.pro.getlink = function (item) {
        var link = "/design/mobile/default.aspx?p=prodetail&gid=" + item.id + "&id=" + tools.site;
        return link;
    }
}
//预览编辑模式切换
tools.changeMode = function () {
    var $this = $("#view_btn");
    if ($this.hasClass("viewmode"))//返回编辑
    {
        $(".com .edit").show();
        $this.removeClass("viewmode");
        $this.html("").append('<i class="fa fa-eye"></i>');
        localStorage["mbdesign"] = "edit";
    }
    else {
        $(".com .edit").hide();
        $("#pop_div").hide();
        $this.addClass("viewmode");
        $this.html("").append('<i class="fa fa-paint-brush"></i>');
        localStorage["mbdesign"] = "view";
    }
}
//-------------------------
tools.pop = {};
tools.pop.open = function (url) {
    Zepto.showIndicator();
    $("#pop_div").show();
    $pop = $('<iframe id="pop_ifr" class="popifr" />');
    $("#pop_content").append($pop);
    $pop[0].onload = function () {
        Zepto.popup('.popup-edit');
        Zepto.hideIndicator();
    }
    $pop.attr("src", url);
    //$("#pop_div").show();
    //Zepto.popup('.popup-edit');
}
tools.pop.close = function () {
    Zepto.closeModal();
    setTimeout(function () {
        $("#pop_content").html("");//兼容ios,有iframe则无法正常使用
        $("#pop_div").hide();//兼容android与ios,否则其仍会占位
    },500);
}
//修改com组件
tools.edit = function (id) {
    var com = tools.scope.coms[id];
    var $ifr = null;
    switch (com.type) {
        case "image":
            picup.up_after = function (data) {
                com.list.wxico = data;
                var list = []; list.push(com.list);
                tools.updatedata("mb_image", { flag: com.list.flag, "list": JSON.stringify(list) });
                tools.scope.$digest();
            }
            picup.sel();
            return;
            break;
        case "footbar":
            tools.pop.open("/design/mobile/pop/list.aspx?id=" + id);
            break;
        default:
            tools.pop.open("/design/mobile/pop/" + com.type + ".aspx?id=" + id);
            break;
    }
    //$(".popifr").hide();
    //$ifr.show();
    //tools.pop.open();
    //$ifr[0].contentWindow.cfg.bind(id);
}
//通用swiper初始化方法,如果未使用swiper则不用执行
tools.nav.init = function () {
    if ($('.swiper-slide').length <= 1) {
        tools.nav.swiper = new Swiper('.swiper-container', {
            noSwiping: true,
            noSwipingClass: 'stop-swiping',
            autoplay: 0
        });

    } else {
        tools.nav.swiper = new Swiper('.swiper-container', {
            nextButton: '.swiper-button-next',
            prevButton: '.swiper-button-prev',
            pagination: '.swiper-pagination',
            paginationClickable: true,
            /* paginationType: 'custom',*/
            autoplay: 5000,
            autoplayDisableOnInteraction: false,
            loop: true,
            grabCursor: true
        });
    }
}
//关闭所有弹出层,返回编辑界面
tools.close = function () {
    //weui.pop.close();
    //weui.toast.close();
    Zepto.closeModal();
    Zepto.hidePreloader();
    Zepto.hideIndicator();
}
//获取页面元素,绑定所需的数据
tools.getdata = function (opts, callback) {
    tools.api = "/design/design.ashx?siteid=" + tools.site + "&action=mb_getdata";
    $.post(tools.api, opts, function (data) {
        var model = APIResult.getModel(data);
        if (APIResult.isok(model)) {
            if (callback) { callback(model.result); }
        } else { alert("failed:" + model.retmsg); }
    })
}
//去除里面的bk与link字段,避免json解析失败,传入angular解析后的json字符串
tools.clearlist = function (json) {
    if (json.indexOf("{") == 0) { json = "[" + json + "]"; }//后端只接受数组形式json
    var list = JSON.parse(json);
    for (var i = 0; i < list.length; i++) {
        delete list[i].bk;
        delete list[i].link;
    }
    return JSON.stringify(list);
}
//更新服务端数据
tools.updatedata = function (action, opts, callback) {
    tools.upapi = "/design/update.ashx?siteid=" + tools.site + "&action=" + action;
    $.post(tools.upapi, opts, function (data) {
        var model = APIResult.getModel(data);
        if (APIResult.isok(model)) {
            if (callback) { callback(data); }
        }
        else { alert("update failed:" + model.retmsg); tools.close(); }
    })
}
//商品操作
tools.product = {};
tools.product.get = function (opts, callback) {//{id:1,psize:10}
    var ref = tools.product;
    var api = "/design/design.ashx?siteid=" + tools.site + "&action=mb_pro_get";
    $.post(api, opts, function (data) {
        var model = APIResult.getModel(data);
        if (APIResult.isok(model)) {
            if (callback) { callback(model.result); }
        }
        else { alert("failed:" + model.retmsg); }
    })
}
tools.product.update = function (opts,callback) {
    var api = "/design/update.ashx?siteid=" + tools.site + "&action=mb_product";
    $.post(tools.upapi, opts, function (data) {
        var model = APIResult.getModel(data);
        if (APIResult.isok(model)) {
            if (callback) { callback(data); }
        }
        else { alert("update failed", model.retmsg); tools.close(); }
    })
}
//进入购买页面
tools.product.buy = function () {
  //弹窗出现页面,让其购买付款,iframe引入,点击返回消失
}
//-------------------------
tools.route = {};
//返回首页
tools.route.index = function () {
    Zepto.router.back('default.aspx?id=' + tools.site);
}
//------------------------------------------
//var mc = new Hammer.Manager(document.getElementsByClassName("com"));
//function HammerBind() {
//    $(".com").each(function () {
//        var $this = $(this);
//        var mc = new Hammer(this);
//        if ($this.hasClass("ham_press")) {
//            mc.add(new Hammer.Press({ time: 500 }));
//            mc.on("tap", com.press);//press
//        }
//        //if ($this.hasClass("ham_tap")) {
//        //    mc.on("tap", com.tap);
//        //}
//    });
//}
//------------------------------------------组件相关处理
//var com = { image: null, text: null };
////处理事件
//com.press = function (evt) {
//    var $dom = tools.$dom = com.getdom(evt);
//    switch ($dom.data("com")) {
//        case "image":
//            picup.callback = function (data) {
//                $dom[0].src = data;
//                weui.toast.close();
//            }
//            picup.sel();
//            break;
//        case "content"://弹出添加内容页面
//            var model = tools.scope.conlist.GetByID($dom.data("id"));
//            weui.pop.show("/design/mobile/pop/content.aspx?id=" + model.id);
//            break;
//    }
//}
//com.tap = function (evt) { }
////事件可能被子级接收,如果是,则获取最近的一个父级com
//com.getdom = function (evt) {
//    var $dom = $(evt.target);
//    if (!$dom.hasClass("com")) { $dom = $dom.closest(".com"); }
//    return $dom;
//}