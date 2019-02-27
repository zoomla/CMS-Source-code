function getPageSize() {
        var xScroll, yScroll;
        if (window.innerHeight && window.scrollMaxY) {
            xScroll = window.innerWidth + window.scrollMaxX;
            yScroll = window.innerHeight + window.scrollMaxY
        } else {
            if (document.body.scrollHeight > document.body.offsetHeight) {
                xScroll = document.body.scrollWidth;
                yScroll = document.body.scrollHeight
            } else {
                xScroll = document.body.offsetWidth;
                yScroll = document.body.offsetHeight
            }
        };
        var windowWidth, windowHeight;
        if (self.innerHeight) {
            if (document.documentElement.clientWidth) {
                windowWidth = document.documentElement.clientWidth
            } else {
                windowWidth = self.innerWidth
            };
            windowHeight = self.innerHeight
        } else {
            if (document.documentElement && document.documentElement.clientHeight) {
                windowWidth = document.documentElement.clientWidth;
                windowHeight = document.documentElement.clientHeight
            } else {
                if (document.body) {
                    windowWidth = document.body.clientWidth;
                    windowHeight = document.body.clientHeight
                }
            }
        };
        if (yScroll < windowHeight) {
            pageHeight = windowHeight
        } else {
            pageHeight = yScroll
        };
        if (xScroll < windowWidth) {
            pageWidth = xScroll
        } else {
            pageWidth = windowWidth
        };
        arrayPageSize = new Array(pageWidth, pageHeight, windowWidth, windowHeight);
        return ({
            pageWidth: pageWidth,
            pageHeight: pageHeight,
            windowWidth: windowWidth,
            windowHeight: windowHeight
        })
    };
highlight = function (el) {
        $(el).delegate('tbody tr', {
            mouseenter: function () {
                $(this).addClass('over')
            },
            mouseleave: function () {
                $(this).removeClass('over')
            }
        })
    };
$.fn.litabs = function (opts) {
        if ($.isFunction(opts)) {
            opts = {
                callBack: opts
            }
        };
        var options = $.extend({}, {
            event: click,
            index: 0,
            ajax: false,
            callBack: false
        }, opts);
        return this.each(function (i) {
            $(this).data('i', i).on(options['event'], function (e) {
                $(this).addClass('active').siblings().removeClass('active');
                if (options.callBack) {
                    options.callBack($(this))
                } else {
                    if (options.ajax) {
                        var data = $.extend({}, options.ajax['data'], $(this).data('data'));
                        $(options.ajax['target']).load(options.ajax['url'], data)
                    } else {
                        var target = $(this).data('target');
                        $(target).addClass('active').siblings().removeClass('active')
                    }
                }
            });
            if (i == options.index) {
                $(this).trigger(options['event'])
            }
        })
    };
$.cookie = function (name, value, options) {
        if (typeof value != 'undefined') {
            if (typeof options == 'number') {
                options = {
                    expires: options
                }
            };
            options = options || {};
            if (value === null) {
                value = '';
                options = $.extend({}, options);
                options.expires = -1
            };
            var expires = '';
            if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
                var date;
                if (typeof options.expires == 'number') {
                    date = new Date();
                    date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000))
                } else {
                    date = options.expires
                };
                expires = '; expires=' + date.toUTCString()
            };
            var path = options.path ? '; path=' + (options.path) : '';
            var domain = options.domain ? '; domain=' + (options.domain) : '';
            var secure = options.secure ? '; secure' : '';
            document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('')
        } else {
            var cookieValue = null;
            if (document.cookie && document.cookie != '') {
                var cookies = document.cookie.split(';');
                for (var i = 0; i < cookies.length; i++) {
                    var cookie = jQuery.trim(cookies[i]);
                    if (cookie.substring(0, name.length + 1) == (name + '=')) {
                        cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                        break
                    }
                }
            };
            return cookieValue
        }
    };

