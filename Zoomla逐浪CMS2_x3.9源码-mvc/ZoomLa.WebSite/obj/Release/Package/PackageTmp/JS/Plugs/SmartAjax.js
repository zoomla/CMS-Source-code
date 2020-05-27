(function (window) {
    /*
      1,发起申请-->无缓存,则发起成功后并okCheck验证通过,写入缓存,如果失败,则隔一定时间重新发起
      2,发起申请-->命中缓存,未过期,未设为强制刷新,则命中缓存即返回
      3,发起申请-->命中缓存,已过期,或设为强制刷新,则先返回缓存,然后继续获取数据逻辑,成功后更新缓存
      4,发起申请-->无网络等,则每过一段时间仍会重新尝试获取数据

      *链接验证url与传递的参数
      *数据更新,请用html(),勿用append()
        SmartAjax.post(url, {}, function (data) {}, { okCheck: function (data) { return true; }});
    */
    //链接验证data与url
    //支持自动重试机制,并开放全局配置文件
    //支持local持久化存储机制
    //支持过期时间
    //ex_store: true,ex_refresh: false,okCheck:null
    var SmartAjax = {
        ajax: function (opts) {
            var ref = this;
            var cache = ZLCache.get(opts);
            if (!ZLCache.isEmpty(cache)) {
                opts.success(cache.value);
                //如未开启强制,且缓存未开启则返回
                if (opts.refresh !== true && cache.isexpire === false) {
                    ref.log(opts.url, "命中缓存,直接返回");
                    return;
                }
                if (cache.isexpire === false) { ref.log(opts.url, "命中缓存,但已过期,继续更新"); }
                else if (opts.refresh) { ref.log(opts.url, "命中缓存,开启强制刷新,继续更新"); }
            }
            else { ref.log(opts.url, "缓存不存在,开始请求"); }
            //失败时不会入此
            //opts.dataFilter = function (data, type) {
            //    console.log(data,type);
            //    return data;
            //}
            //无法获取到数据
            //opts.complete = function (XMLHttpRequest, textStatus) {
            //    console.log(textStatus);
            //}
            if (!opts.success) { opts.success = function (data, textStatus) { }; }
            if (!opts.error) { opts.error = function (XMLHttpRequest, textStatus, errorThrown) { }; }
            var success = opts.success; var error = opts.error;
            //成功时缓存化
            opts.success = function (data, textStatus) {
                //未设为不缓存,并且获取成功,则加入缓存,okCheck为自定义判断 return  data.retcode == 1;
                if (opts.ex_store !== false && !ZLCache.isEmpty(data)) {
                    if (opts.okCheck && !opts.okCheck(data)) { console.log(opts.url, "okCheck检验不缓存"); }
                    else
                    {
                        ref.log(opts.url, "写入缓存", data);
                        ZLCache.add(opts, data);
                    }
                }
                success(data, textStatus);
            }
            //弱网络情况下自动连接
            opts.error = function (XMLHttpRequest, textStatus, errorThrown) {
                ref.log(opts.url, "获取数据失败" + AjaxConfig.autoRetry + "后重试");
                if (AjaxConfig.autoRetry >= 0) {
                    setTimeout(function () { $.ajax(opts); }, AjaxConfig.autoRetry);
                }
                error(XMLHttpRequest, textStatus, errorThrown);
            }
            //正常提交请求
            $.ajax(opts);
        },
        spost: function (url, data, callback) {
            var ref = this;
            $.ajax({
                "url": url,
                type: "POST",
                dataType: 'jsonp',
                jsonp: 'callback',
                "data": data,
                success: callback,
                error: function () { console.log("failed"); }
            })
        },
        post: function (url, data, callback, opts) {
            if (!opts) { opts = { okCheck: null }; }
            var ref = this;
            ref.ajax({
                "url": url,
                type: "POST",
                dataType: 'jsonp',
                jsonp: 'callback',
                "okCheck": opts.okCheck,
                "data": data,
                success: callback
            })
        },
        get: function (url, data, callback, opts) {
            if (!opts) { opts = { okCheck: null }; }
            var ref = this;
            ref.ajax({
                "url": url,
                type: "GET",
                dataType: 'jsonp',
                jsonp: 'callback',
                "data": data,
                "okCheck": opts.okCheck,
                success: callback
            })

        },
        log: function (url, name, data) {
            if (AjaxConfig.debug === true) {
                if (data) { console.log(url, name, data); }
                else { console.log(url, name); }
            }
        },
    };
    //--------------------------------------
    var ZLCache = {
        init: function () {
            var ref = this;
            //暂定为每次均检测,后期根据需要,改为根据上次处理的时间+间隔处理
            //localStorage["ZLCache_clearByTimeOut"]
            ref.clearByTimeOut();
        },
        add: function (opts, value) {
            var ref = this;
            var key = ref.getKey(opts);
            if (ref.isEmpty(key, value)) { return; }
            else
            {
                var model = ref.getModel();
                model.key = key;
                model.value = value;
                model.date = DateHelper.getDate();
                localStorage.setItem(key, JSON.stringify(model));
            }
        },
        get: function (opts) {
            var ref = this;
            var key = ref.getKey(opts);
            var item = localStorage.getItem(key);
            if (!ZLCache.isEmpty(item)) {
                item = JSON.parse(item);
                var second = DateHelper.getInterval(item.date, DateHelper.getDate());
                item.isexpire = second > AjaxConfig.expire;
            }
            return item;
        },
        remove: function (opts) {
            var ref = this;
            var key = ref.getKey(opts);
            localStorage.removeItem(opts);
        },
        clearByTimeOut: function () {//清除超时数据
            if (AjaxConfig.timeout < 1) { return; }
            var ref = this;
            var now = DateHelper.getDate();
            for (var i = 0; i < localStorage.length; i++) {
                var key = localStorage.key(i);
                try {
                    var model = JSON.parse(localStorage[key]);
                    if (model.date) {
                        var second = DateHelper.getInterval(model.date, now);
                        if (second > AjaxConfig.timeout) { SmartAjax.log(key, "清除超时成功"); ref.remove(key); }
                    }
                } catch (ex) { }//SmartAjax.log(key, "清除超时失败", ex.message);
            }
        },
        clearAll: function () {//清除所有,不建议
            localStorage.clear();
        },
        getKey: function (opts) {
            //以url和提交的参数,混合生成key,注意顺序
            var key = opts.url;
            if (ZLCache.isEmpty(opts.data)) { opts.data = {}; }
            key += JSON.stringify(opts.data);
            return key;
        },
        getModel: function () {
            return { key: "", value: "", date: "", isexpire: false };
        },
        isEmpty: function () {
            for (var i = 0; i < arguments.length; i++) {
                if (!arguments[i] || arguments[i] == undefined || arguments == null) { return true; }
            }
            return false;
        },
        //debug
        showAll: function () {
            for (var i = 0; i < localStorage.length; i++) {
                var key = localStorage.key(i);
                console.log(key, localStorage[key]);
            }
        },
    };
    //--------------------------------------
    var AjaxConfig = {
        autoRetry: 5000,//失败后多久自动重试,小于0则不重试,以毫秒为单位
        expire:10 * 60,//超过则视为已过期,可被新的缓存覆盖,以秒为单位
        timeout: (24 * 60 * 60),//超过此时间的缓存会被清除,以秒为单位,0则为不清除
        debug: true,//是否输出调试信息
    };
    //--------------------------------------
    var DateHelper = {};
    //转化秒为时间,返回模型
    DateHelper.SecondToTime = function (time) {
        var model = this.getModel();
        if (!time || null == time || "" == time) return model;
        model.day = parseInt(time / (60 * 60 * 24));
        if (model.day > 0) { time = time - ((60 * 60 * 24) * model.day); }
        model.hour = parseInt(time / (60 * 60));
        if (model.hour > 0) { time = time - ((60 * 60) * model.hour); }
        model.minute = parseInt(time / 60);
        if (model.minute > 0) { time = time - (60 * model.minute); }
        model.second = time;
        return model;
    }
    DateHelper.getDate = function (formatStr) {
        if (!formatStr) { formatStr = "yyyy-MM-dd HH:mm:ss"; }
        var myDate = new Date();
        var str = formatStr;
        var Week = ['日', '一', '二', '三', '四', '五', '六'];
        str = str.replace(/yyyy|YYYY/, myDate.getFullYear());
        str = str.replace(/yy|YY/, (myDate.getYear() % 100) > 9 ? (myDate.getYear() % 100).toString() : '0' + (myDate.getYear() % 100));

        var month = (myDate.getMonth() + 1); if (month < 10) { month = "0" + month; }
        str = str.replace(/MM/, month);
        str = str.replace(/M/g, month);

        str = str.replace(/w|W/g, Week[myDate.getDay()]);

        str = str.replace(/dd|DD/, myDate.getDate() > 9 ? myDate.getDate().toString() : '0' + myDate.getDate());
        str = str.replace(/d|D/g, myDate.getDate());

        str = str.replace(/hh|HH/, myDate.getHours() > 9 ? myDate.getHours().toString() : '0' + myDate.getHours());
        str = str.replace(/h|H/g, myDate.getHours());
        str = str.replace(/mm/, myDate.getMinutes() > 9 ? myDate.getMinutes().toString() : '0' + myDate.getMinutes());
        str = str.replace(/m/g, myDate.getMinutes());

        str = str.replace(/ss|SS/, myDate.getSeconds() > 9 ? myDate.getSeconds().toString() : '0' + myDate.getSeconds());
        str = str.replace(/s|S/g, myDate.getSeconds());

        return str;
    }
    DateHelper.getInterval = function (sdate, edate) {
        var ref = this;
        var startTime = new Date(Date.parse(sdate.replace(/-/g, "/"))).getTime();
        var endTime = new Date(Date.parse(edate.replace(/-/g, "/"))).getTime();
        var second = Math.abs((startTime - endTime)) / (1000);//* 60 * 60 * 24
        return second;
    }
    DateHelper.dateToModel = function (str) {
        var ref = this;
        str = str.replace(/\//ig, "-");
        var model = ref.getModel();
        var date = str.split(' ')[0];
        var time = str.split(' ')[1];
        model.year = date.split('-')[0];
        model.month = date.split('-')[1];
        model.day = date.split('-')[2];
        model.hour = time.split(':')[0];
        model.minute = time.split(':')[1];
        model.second = time.split(':')[2];
        return model;
    }
    DateHelper.getModel = function () {
        var model = { year: 0, month: 0, day: 0, hour: 0, minute: 0, second: 0 };
        model.isHasTime = function () {
            return (this.year > 0 || this.month > 0 || this.day > 0 || this.hour > 0 || this.minute > 0 || this.second > 0);
        }
        return model;
    }
    //--------------------------------------
    window.SmartAjax = SmartAjax;
    window.ZLCache = ZLCache;
    ZLCache.init();
}(window))