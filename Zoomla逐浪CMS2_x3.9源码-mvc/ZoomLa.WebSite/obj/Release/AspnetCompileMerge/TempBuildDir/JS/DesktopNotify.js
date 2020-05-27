/**
 * Copyright 2012 Tsvetan Tsvetkov
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * Author: Tsvetan Tsvetkov (tsekach@gmail.com)
 * https://github.com/ttsvetko/HTML5-Desktop-Notifications
 */
(function (win) {
    /*
     Safari native methods required for Notifications do NOT run in strict mode.
     */
    //"use strict";
    var PERMISSION_DEFAULT = "default",
        PERMISSION_GRANTED = "granted",
        PERMISSION_DENIED = "denied",
        PERMISSION = [PERMISSION_GRANTED, PERMISSION_DEFAULT, PERMISSION_DENIED],
        defaultSetting = {
            pageVisibility: false,
            autoClose: true
        },
        empty = {},
        emptyString = "",
        isSupported = (function () {
            var isSupported = false;
            /*
             * Use try {} catch() {} because the check for IE may throws an exception
             * if the code is run on browser that is not Safar/Chrome/IE or
             * Firefox with html5notifications plugin.
             *
             * Also, we can NOT detect if msIsSiteMode method exists, as it is
             * a method of host object. In IE check for existing method of host
             * object returns undefined. So, we try to run it - if it runs 
             * successfully - then it is IE9+, if not - an exceptions is thrown.
             */
            try {
                isSupported = !!(/* Safari, Chrome */win.Notification || /* Chrome & ff-html5notifications plugin */win.webkitNotifications || /* Firefox Mobile */navigator.mozNotification || /* IE9+ */(win.external && win.external.msIsSiteMode() !== undefined));
            } catch (e) { }

            return isSupported;
        }()),
        ieVerification = Math.floor((Math.random() * 10) + 1),
        isFunction = function (value) { return (value && (value).constructor === Function); },
        isString = function (value) { return (value && (value).constructor === String); },
        isObject = function (value) { return (value && (value).constructor === Object); },

        /**
         * Dojo Mixin
         */
        mixin = function (target, source) {
            var name, s;
            for (name in source) {
                s = source[name];
                if (!(name in target) || (target[name] !== s && (!(name in empty) || empty[name] !== s))) {
                    target[name] = s;
                }
            }
            return target; // Object
        },
        noop = function () { },
        settings = defaultSetting;

    //获取不同浏览器的桌面通知对象
    function getNotification(title, options) {
        var notification;
        if (win.Notification) { /* Safari 6, Chrome (23+) */
            notification = new win.Notification(title, {
                /* The notification's icon - For Chrome in Windows, Linux & Chrome OS */
                icon: isString(options.icon) ? options.icon : options.icon.x32,
                /* The notification’s subtitle. */
                body: options.body || emptyString,
                /*
                    The notification’s unique identifier.
                    This prevents duplicate entries from appearing if the user has multiple instances of your website open at once.
                */
                tag: options.tag || emptyString
            });
        } else if (win.webkitNotifications) { /* FF with html5Notifications plugin installed */
            notification = win.webkitNotifications.createNotification(options.icon, title, options.body);
            notification.show();
        } else if (navigator.mozNotification) { /* Firefox Mobile */
            notification = navigator.mozNotification.createNotification(title, options.body, options.icon);
            notification.show();
        } else if (win.external && win.external.msIsSiteMode()) { /* IE9+ */
            //Clear any previous notifications
            win.external.msSiteModeClearIconOverlay();
            win.external.msSiteModeSetIconOverlay((isString(options.icon) ? options.icon : options.icon.x16), title);
            win.external.msSiteModeActivate();
            notification = {
                "ieVerification": ieVerification + 1
            };
        }

        return notification;
    }

    //为桌面通知增加关闭的方法
    function getWrapper(notification) {
        return {
            close: function () {
                if (notification) {
                    if (notification.close) {
                        //http://code.google.com/p/ff-html5notifications/issues/detail?id=58
                        notification.close();
                    } else if (win.external && win.external.msIsSiteMode()) {
                        if (notification.ieVerification === ieVerification) {
                            win.external.msSiteModeClearIconOverlay();
                        }
                    }
                }
            }
        };
    }

    //请求浏览器权限许可
    function requestPermission(callback) {
        if (!isSupported) { return; }

        var callbackFunction = isFunction(callback) ? callback : noop;

        if (win.webkitNotifications && win.webkitNotifications.checkPermission) {
            /*
             * Chrome 23 supports win.Notification.requestPermission, but it
             * breaks the browsers, so use the old-webkit-prefixed 
             * win.webkitNotifications.checkPermission instead.
             *
             * Firefox with html5notifications plugin supports this method
             * for requesting permissions.
             */
            win.webkitNotifications.requestPermission(callbackFunction);
        } else if (win.Notification && win.Notification.requestPermission) {
            win.Notification.requestPermission(callbackFunction);
        }
    }

    //获取当前浏览器的权限许可
    function permissionLevel() {
        var permission;

        if (!isSupported) { return; }

        if (win.Notification && win.Notification.permissionLevel) {
            //Safari 6
            permission = win.Notification.permissionLevel();
        } else if (win.webkitNotifications && win.webkitNotifications.checkPermission) {
            //Chrome & Firefox with html5-notifications plugin installed
            permission = PERMISSION[win.webkitNotifications.checkPermission()];
        } else if (navigator.mozNotification) {
            //Firefox Mobile
            permission = PERMISSION_GRANTED;
        } else if (win.Notification && win.Notification.permission) {
            // Firefox 23+
            permission = win.Notification.permission;
        } else if (win.external && (win.external.msIsSiteMode() !== undefined)) { /* keep last */
            //IE9+
            permission = win.external.msIsSiteMode() ? PERMISSION_GRANTED : PERMISSION_DEFAULT;
        }

        return permission;
    }

    //重置默认设置
    function config(params) {
        if (params && isObject(params)) {
            mixin(settings, params);
        }
        return settings;
    }

    function isDocumentHidden() {
        return settings.pageVisibility ? (document.hidden || document.msHidden || document.mozHidden || document.webkitHidden) : true;
    }

    //处理事件及回调
    function handleNotificationEvent(notification, options) {
        if (notification.addEventListener) {
            notification.addEventListener('show', function (e) {
                //设置自动关闭
                if (settings.autoClose && options.timeout && notification && !notification.ieVerification && notification.addEventListener) {
                    win.setTimeout(function () {
                        if (notification.close) {
                            //http://code.google.com/p/ff-html5notifications/issues/detail?id=58
                            notification.close();
                        } else if (win.external && win.external.msIsSiteMode()) {
                            if (notification.ieVerification === ieVerification) {
                                win.external.msSiteModeClearIconOverlay();
                            }
                        }
                    }, options.timeout * 1000);
                }

                if (options.onShow && typeof options.onShow === 'function') {
                    options.onShow(e);
                }
            });

            notification.addEventListener('error', function (e) {
                if (options.onError && typeof options.onError === 'function') {
                    options.onError(e);
                }
            });

            notification.addEventListener('close', function (e) {
                if (options.onClose && typeof options.onClose === 'function') {
                    options.onClose(e);
                }
            });

            notification.addEventListener('click', function (e) {
                if (options.onClick && typeof options.onClick === 'function') {
                    options.onClick(e);
                }
            });
        }
    }

    //创建桌面通知
    function createNotification(title, options) {
        var notification, notificationWrapper;
        /*
            Return undefined if notifications are not supported.
            Return undefined if no permissions for displaying notifications.
            Title and icons are required. Return undefined if not set.
         */
        if (isSupported && isDocumentHidden() && isString(title) && (options && (isString(options.icon) || isObject(options.icon))) && (permissionLevel() === PERMISSION_GRANTED)) {
            notification = getNotification(title, options);
        }

        //处理事件及回调
        handleNotificationEvent(notification, options);
    }

    win.DesktopNotify = {
        PERMISSION_DEFAULT: PERMISSION_DEFAULT,
        PERMISSION_GRANTED: PERMISSION_GRANTED,
        PERMISSION_DENIED: PERMISSION_DENIED,
        isSupported: isSupported,
        config: config,
        createNotification: createNotification,
        permissionLevel: permissionLevel,
        requestPermission: requestPermission
    };

    if (isFunction(Object.seal)) {
        Object.seal(win.DesktopNotify);
    }
}(window));

//-----------------NotifyHelper
/*
*deskNotify:是否开启桌面提示,默认开启(Chrome等浏览器支持)
*/
var Notify = function () { };
Notify.prototype.config = { url:"", id: "notify_div", deskNotify: true, interval: (3 * 60 * 1000), interobj: null, callback: null, onclick: null };
Notify.prototype.Init = function (opts) {
    var ref = this;
    var config = this.config;
    //----------------------------必须
    config.url = opts.url;
    config.interval = opts.interval ? opts.interval : config.interval;
    config.callback = opts.callback;//获取到数据后的回调
    if (ref.isEmpty(config.url)) { console.log("配置不正确,url不能为空"); return; }
    //--------------------------------------------
    config.id = opts.id ? opts.id : config.id;
    config.onclick = opts.onclick;
    //是否使用默认弹窗
    config.deskNotify = (opts.deskNotify == true || opts.deskNotify == false) ? opts.deskNotify : config.deskNotify;
    //是否开启桌面通知
    if (config.deskNotify) { DesktopNotify.requestPermission(function () { }); }
    ref.config.interobj = setInterval(function () { ref.GetNotify(); }, config.interval);
};
//获取通知
Notify.prototype.GetNotify = function () {
    var config = this.config;
    if (!config.param) { config.param = {}; }
    $.post(config.url, config.param, function (data) { config.callback(data); });
};
//默认显示方法
Notify.prototype.DisNotify = function (notifyMod) {
    //这里this指向config
    var ref = this;
    var config = ref.config;
    if (!notifyMod || notifyMod == "") { return; }
    var img = "/App_Themes/User/at.png";
   
    if (config.deskNotify && DesktopNotify.isSupported && DesktopNotify.permissionLevel() == DesktopNotify.PERMISSION_GRANTED) {
        var contentText = $("<div />").html(notifyMod.msg).text();
        DesktopNotify.createNotification("收到新的通知", {
            body: contentText,
            icon: img,
            tag: "notify",
            onClick: function () {
                if (config.onClick) { config.onClick(notifyMod); }
            }
        });
    }
    else//如果不支持,则使用Div的形式
    {
        $("#NotifyDiv").unbind("click");
        if (config.onClick) {
            $("#NotifyDiv").click(function () {
                config.onClick(notifyMod);
            });
        }
        $("#notify_body").html(notifyMod.msg);
        $("#NotifyDiv").show();
    }
}
Notify.prototype.isEmpty = function () {
    for (var i = 0; i < arguments.length; i++) {
        if (!arguments[i] || arguments[i] == undefined) { return true; }
        var s = arguments[i].replace(/ /g, "");
        if (s == "") return true;
    }
    return false;
}
//<style type="text/css">
//.notify_div  {border: 1px solid #ddd; display: none; background-color: white; width: 300px; height: 80px; position: fixed; bottom: 20px; right: 10px;cursor:pointer;}
//.notify_left {width: 70px;height:80px; display:inline-block; padding-right:10px;padding-top:5px;border-right:1px solid #ddd;}
//.notify_right {width: 220px;height:80px; float: right; overflow-x:hidden;overflow-y:auto;}
//.notify_right .notify_title {text-align:right;padding-right:5px;height:20px;}
//.notify_right .noitfy_item {height:20px;cursor:pointer;border:1px solid #fff;}
//.notify_right .noitfy_item:hover {border:1px solid #337ab7;}
//.notify_icon_div {height:40px;line-height:40px;color:#fff;}
//</style>
//<div class="notify_div" id="notify_div"></div>
//<script src="/JS/Modal/APIResult.js"></script>
//<script src="/JS/DesktopNotify.js"></script>
//<script src="/JS/Controls/ZL_Array.js"></script>
//<script>
//    var notifyHtml = "<div class=\"notify_left\"><i class=\"fa fa-bell fa-5x\" style=\"color:#337ab7;\"></i></div>"
//        + "<div class=\"notify_right\">"
//        + "<div id=\"notify_title\" class=\"notify_title\">@Title"
//        + "<i title='关闭' class='fa fa-remove' style=\"margin-left:5px;\" onclick=\"$('#notify_div').hide();\"></i>"
//        + "</div>"
//        + "<div id=\"notify_body\">@Content</div>"
//        + "</div>"
//        + "<div class=\"clearfix\"></div>";
//function showNotify(data) {
//    var model = APIResult.getModel(data);
//    if (APIResult.isok(model)) {
//        if (model.result.length < 1) { return; }
//        console.log(model.result);
//        $div = $("#notify_div"); $div.html("");
//        var $items = JsonHelper.FillItem(notifyHtml, model.result);
//        $div.append($items);
//        $div.show();
//    }
//}

//var nfy = new Notify();
//nfy.Init({ url: "/common/api/notify.ashx?action=list&type=follow", interval: 3000, deskNotify: true, callback: showNotify });
//nfy.GetNotify();
//</script>