define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    _self.prototype.wrapper = '<div ng-model="list.@id" id="@id" class="onlydrag" style="{{list.@id.config.style}}">{html}</div>';
    _self.prototype.Init = function (model, extend) {
        var ref = this;
        ref._init(model, extend);
        ref.editurl = "/Design/Diag/Gallery/Edit.aspx?id=" + ref.id;
        ref.defaults = {
            current: 0,	// 当前项
            autoplay: false,// 是否自动播放
            interval: 2000  // 播放间隔
        }
    };
    _self.prototype.SetInstance_After = function () {
        var ref = this;
        ref.$el = ref.instance;
        ref.editurl = "/Design/Diag/Gallery/Edit.aspx?id=" + ref.id;
        setTimeout(function () { ref._galleryInit(ref); }, 200);
    }
    _self.prototype.Render = function () {
        var ref = this;
        ref._galleryInit(ref);
    }
    var html = "<section id=\"comp_photo_container\" class=\"comp_photo_container\">"
+ "		<div class=\"comp_photo_wrapper\">"
+ "			<a ng-repeat=\"item in list.@id.dataMod.list|orderBy:'orderid'\" class=\"comp_photo_transition\" href=\"#\"><img src=\"{{item.url}}\" alt=\"{{item.name}}\"><div ng-bind=\"item.name\"></div></a>"
+ "		</div>"
+ "		<nav style=\"width:100%;\">	"
+ "			<a class=\"comp_photo_prev btn btn-primary\" href=\"javascript:;\"><i class=\"fa fa-chevron-left\"></i></a>"
+ "			<a class=\"comp_photo_next btn btn-primary\" href=\"javascript:;\"><i class=\"fa fa-chevron-right\"></i></a>"
+ "		</nav>"
+ "	</section>";
    _self.prototype.htmlTlp = html;
    _self.prototype._cssinit = function (ref) {//整合css
        ref.$el.find(".comp_photo_container").css({
            "width": "1000px",
            "height": "450px",
            "position": "relative"
        });
        ref.$el.find(".comp_photo_container img").css({
            "width": "100%",
            "height":"100%"
        });
        ref.$el.find(".comp_photo_wrapper").css({
            "width": "481px",
            "height": "316px",
            "margin": "0 auto",
            "position": "relative",
            "-webkit-transform-style": "preserve-3d",
            "-moz-transform-style": "preserve-3d",
            "-o-transform-style": "preserve-3d",
            "-ms-transform-style": "preserve-3d",
            "transform-style": "preserve-3d",
            "-webkit-perspective": "1000px",
            " -moz-perspective": "1000px",
            "-o-perspective": "1000px",
            "-ms-perspective": "1000px",
            "perspective": "1000px"
        });
        ref.$el.find(".comp_photo_wrapper a").css({
            "width": "482px",
            "height": "316px",
            "display": "block",
            "position": "absolute",
            "left": "0",
            "top": "0",
            "background": "transparent url(../images/browser.png) no-repeat top left",
            "box-shadow": "0px 10px 20px rgba(0,0,0,0.3)"
        });
        ref.$el.find(".comp_photo_wrapper a.comp_photo_transition").css({
            "-webkit-transition": "all 0.5s ease-in-out",
            "-moz-transition": "all 0.5s ease-in-out",
            "-o-transition": "all 0.5s ease-in-out",
            "-ms-transition": "all 0.5s ease-in-out",
            "transition": "all 0.5s ease-in-out"
        });
        ref.$el.find(".comp_photo_wrapper a img").css({
            "display": "block",
            "padding": "41px 0px 0px 1px"
        });
        ref.$el.find(".comp_photo_wrapper a div").css({
            "font-style": "italic",
            "text-align": "center",
            "line-height": "50px",
            "text-shadow": "1px 1px 1px rgba(255,255,255,0.5)",
            "color": "#333",
            "font-size": "16px",
            "width": "100%",
            "bottom": "-55px",
            "display": "none",
            "position": "absolute"
        });
        ref.$el.find(".comp_photo_wrapper a.comp_photo_center div").css({ "display": "block" });
        ref.$el.find(".comp_photo_container nav").css({
            "position": "absolute",
            "z-index": "1000",
            "bottom": "40px",
            "left": "50%",
            "margin-left": "-29px"
        });
        ref.$el.find(".comp_photo_container nav span").css({
            "text-indent": "-9000px",
            "float": "left",
            "cursor": "pointer",
            "width": "24px",
            "height": "25px",
            "opacity": "0.8"
        });
    };
    _self.prototype._galleryInit = function (ref, options) {
        ref._cssinit(ref);
        ref.options = $.extend(true, {}, ref.defaults, options);
        //配置是否启用3d动画
        var isAnimal = ref.compatibleAnimal(ref);
        ref.support3d = isAnimal;
        ref.support2d = isAnimal;
        ref.supportTrans = isAnimal;
        //----------------------------
        ref.$wrapper = ref.$el.find('.comp_photo_wrapper');
        ref.$items = ref.$wrapper.children();
        ref.itemsCount = ref.$items.length;
        ref.$nav = ref.$el.find('nav');
        ref.$navPrev = ref.$nav.find('.comp_photo_prev');
        ref.$navNext = ref.$nav.find('.comp_photo_next');
        // minimum of 3 items
        if (ref.itemsCount < 3) {
            ref.$nav.remove();
            return false;
        }
        ref.current = ref.options.current;
        ref.isAnim = false;
        ref.$items.css({
            'opacity': 0,
            'visibility': 'hidden'
        });
        ref._validate(ref);
        ref._layout(ref);
        // load the events
        ref._loadEvents(ref);
        // slideshow
        if (ref.options.autoplay) {
            ref._startSlideshow(ref);
        }
    }
    _self.prototype._validate = function (ref) {
        if (ref.options.current < 0 || ref.options.current > ref.itemsCount - 1) {
            ref.current = 0;
        }
    }
    _self.prototype._layout = function (ref) {
        // current, left and right items
        ref._setItems(ref);
        // current item is not changed
        // left and right one are rotated and translated
        var leftCSS, rightCSS, currentCSS;
        if (ref.support3d && ref.supportTrans) {
            leftCSS = {
                '-webkit-transform': 'translateX(-350px) translateZ(-200px) rotateY(45deg)',
                '-moz-transform': 'translateX(-350px) translateZ(-200px) rotateY(45deg)',
                '-o-transform': 'translateX(-350px) translateZ(-200px) rotateY(45deg)',
                '-ms-transform': 'translateX(-350px) translateZ(-200px) rotateY(45deg)',
                'transform': 'translateX(-350px) translateZ(-200px) rotateY(45deg)'
            };

            rightCSS = {
                '-webkit-transform': 'translateX(350px) translateZ(-200px) rotateY(-45deg)',
                '-moz-transform': 'translateX(350px) translateZ(-200px) rotateY(-45deg)',
                '-o-transform': 'translateX(350px) translateZ(-200px) rotateY(-45deg)',
                '-ms-transform': 'translateX(350px) translateZ(-200px) rotateY(-45deg)',
                'transform': 'translateX(350px) translateZ(-200px) rotateY(-45deg)'
            };

            leftCSS.opacity = 1;
            leftCSS.visibility = 'visible';
            rightCSS.opacity = 1;
            rightCSS.visibility = 'visible';

        }
        else if (ref.support2d && ref.supportTrans) {
            leftCSS = {
                '-webkit-transform': 'translate(-350px) scale(0.8)',
                '-moz-transform': 'translate(-350px) scale(0.8)',
                '-o-transform': 'translate(-350px) scale(0.8)',
                '-ms-transform': 'translate(-350px) scale(0.8)',
                'transform': 'translate(-350px) scale(0.8)'
            };
            rightCSS = {
                '-webkit-transform': 'translate(350px) scale(0.8)',
                '-moz-transform': 'translate(350px) scale(0.8)',
                '-o-transform': 'translate(350px) scale(0.8)',
                '-ms-transform': 'translate(350px) scale(0.8)',
                'transform': 'translate(350px) scale(0.8)'
            };
            currentCSS = {
                'z-index': 999
            };
            leftCSS.opacity = 1;
            leftCSS.visibility = 'visible';
            rightCSS.opacity = 1;
            rightCSS.visibility = 'visible';
        }
        ref.$leftItm.css(leftCSS || {});
        ref.$rightItm.css(rightCSS || {});
        ref.$currentItm.css(currentCSS || {}).css({
            'opacity': 1,
            'visibility': 'visible',
            'z-index': 999
        }).addClass('comp_photo_center');
    }
    _self.prototype._setItems = function (ref) {
        ref.$items.removeClass('comp_photo_center');
        ref.$currentItm = ref.$items.eq(ref.current);
        ref.$leftItm = (ref.current === 0) ? ref.$items.eq(ref.itemsCount - 1) : ref.$items.eq(ref.current - 1);
        ref.$rightItm = (ref.current === ref.itemsCount - 1) ? ref.$items.eq(0) : ref.$items.eq(ref.current + 1);
        if (ref.support3d && ref.support2d && ref.supportTrans) {
            ref.$items.css('z-index', 1);
            ref.$currentItm.css('z-index', 999);
        }
        // next & previous items
        if (ref.itemsCount > 3) {
            // next item
            ref.$nextItm = (ref.$rightItm.index() === ref.itemsCount - 1) ? ref.$items.eq(0) : ref.$rightItm.next();
            ref.$nextItm.css(ref._getCoordinates(ref, 'outright'));

            // previous item
            ref.$prevItm = (ref.$leftItm.index() === 0) ? ref.$items.eq(ref.itemsCount - 1) : ref.$leftItm.prev();
            ref.$prevItm.css(ref._getCoordinates(ref, 'outleft'));
        }
    }
    _self.prototype._loadEvents = function (ref) {
        ref.$navPrev.on('click.gallery', function (event) {
            if (ref.options.autoplay) {
                clearTimeout(ref.slideshow);
                ref.options.autoplay = false;
            }
            ref._navigate(ref, 'prev');
            return false;
        });
        ref.$navNext.on('click.gallery', function (event) {
            if (ref.options.autoplay) {
                clearTimeout(ref.slideshow);
                ref.options.autoplay = false;

            }
            ref._navigate(ref, 'next');
            return false;
        });
        ref.$wrapper.on('webkitTransitionEnd.gallery transitionend.gallery OTransitionEnd.gallery', function (event) {
            ref.$currentItm.addClass('comp_photo_center');
            ref.$items.removeClass('rcomp_photo_transition');
            ref.isAnim = false;
            ref._cssinit(ref);
        });
    }
    _self.prototype._getCoordinates = function (ref, position) {
        if (ref.support3d && ref.supportTrans) {
            switch (position) {
                case 'outleft':
                    return {
                        '-webkit-transform': 'translateX(-450px) translateZ(-300px) rotateY(45deg)',
                        '-moz-transform': 'translateX(-450px) translateZ(-300px) rotateY(45deg)',
                        '-o-transform': 'translateX(-450px) translateZ(-300px) rotateY(45deg)',
                        '-ms-transform': 'translateX(-450px) translateZ(-300px) rotateY(45deg)',
                        'transform': 'translateX(-450px) translateZ(-300px) rotateY(45deg)',
                        'opacity': 0,
                        'visibility': 'hidden'
                    };
                    break;
                case 'outright':
                    return {
                        '-webkit-transform': 'translateX(450px) translateZ(-300px) rotateY(-45deg)',
                        '-moz-transform': 'translateX(450px) translateZ(-300px) rotateY(-45deg)',
                        '-o-transform': 'translateX(450px) translateZ(-300px) rotateY(-45deg)',
                        '-ms-transform': 'translateX(450px) translateZ(-300px) rotateY(-45deg)',
                        'transform': 'translateX(450px) translateZ(-300px) rotateY(-45deg)',
                        'opacity': 0,
                        'visibility': 'hidden'
                    };
                    break;
                case 'left':
                    return {
                        '-webkit-transform': 'translateX(-350px) translateZ(-200px) rotateY(45deg)',
                        '-moz-transform': 'translateX(-350px) translateZ(-200px) rotateY(45deg)',
                        '-o-transform': 'translateX(-350px) translateZ(-200px) rotateY(45deg)',
                        '-ms-transform': 'translateX(-350px) translateZ(-200px) rotateY(45deg)',
                        'transform': 'translateX(-350px) translateZ(-200px) rotateY(45deg)',
                        'opacity': 1,
                        'visibility': 'visible'
                    };
                    break;
                case 'right':
                    return {
                        '-webkit-transform': 'translateX(350px) translateZ(-200px) rotateY(-45deg)',
                        '-moz-transform': 'translateX(350px) translateZ(-200px) rotateY(-45deg)',
                        '-o-transform': 'translateX(350px) translateZ(-200px) rotateY(-45deg)',
                        '-ms-transform': 'translateX(350px) translateZ(-200px) rotateY(-45deg)',
                        'transform': 'translateX(350px) translateZ(-200px) rotateY(-45deg)',
                        'opacity': 1,
                        'visibility': 'visible'
                    };
                    break;
                case 'center':
                    return {
                        '-webkit-transform': 'translateX(0px) translateZ(0px) rotateY(0deg)',
                        '-moz-transform': 'translateX(0px) translateZ(0px) rotateY(0deg)',
                        '-o-transform': 'translateX(0px) translateZ(0px) rotateY(0deg)',
                        '-ms-transform': 'translateX(0px) translateZ(0px) rotateY(0deg)',
                        'transform': 'translateX(0px) translateZ(0px) rotateY(0deg)',
                        'opacity': 1,
                        'visibility': 'visible'
                    };
                    break;
            };

        }
        else if (ref.support2d && ref.supportTrans) {
            switch (position) {
                case 'outleft':
                    return {
                        '-webkit-transform': 'translate(-450px) scale(0.7)',
                        '-moz-transform': 'translate(-450px) scale(0.7)',
                        '-o-transform': 'translate(-450px) scale(0.7)',
                        '-ms-transform': 'translate(-450px) scale(0.7)',
                        'transform': 'translate(-450px) scale(0.7)',
                        'opacity': 0,
                        'visibility': 'hidden'
                    };
                    break;
                case 'outright':
                    return {
                        '-webkit-transform': 'translate(450px) scale(0.7)',
                        '-moz-transform': 'translate(450px) scale(0.7)',
                        '-o-transform': 'translate(450px) scale(0.7)',
                        '-ms-transform': 'translate(450px) scale(0.7)',
                        'transform': 'translate(450px) scale(0.7)',
                        'opacity': 0,
                        'visibility': 'hidden'
                    };
                    break;
                case 'left':
                    return {
                        '-webkit-transform': 'translate(-350px) scale(0.8)',
                        '-moz-transform': 'translate(-350px) scale(0.8)',
                        '-o-transform': 'translate(-350px) scale(0.8)',
                        '-ms-transform': 'translate(-350px) scale(0.8)',
                        'transform': 'translate(-350px) scale(0.8)',
                        'opacity': 1,
                        'visibility': 'visible'
                    };
                    break;
                case 'right':
                    return {
                        '-webkit-transform': 'translate(350px) scale(0.8)',
                        '-moz-transform': 'translate(350px) scale(0.8)',
                        '-o-transform': 'translate(350px) scale(0.8)',
                        '-ms-transform': 'translate(350px) scale(0.8)',
                        'transform': 'translate(350px) scale(0.8)',
                        'opacity': 1,
                        'visibility': 'visible'
                    };
                    break;
                case 'center':
                    return {
                        '-webkit-transform': 'translate(0px) scale(1)',
                        '-moz-transform': 'translate(0px) scale(1)',
                        '-o-transform': 'translate(0px) scale(1)',
                        '-ms-transform': 'translate(0px) scale(1)',
                        'transform': 'translate(0px) scale(1)',
                        'opacity': 1,
                        'visibility': 'visible'
                    };
                    break;
            };

        }
        else {
            switch (position) {
                case 'outleft':
                case 'outright':
                case 'left':
                case 'right':
                    return {
                        'opacity': 0,
                        'visibility': 'hidden'
                    };
                    break;
                case 'center':
                    return {
                        'opacity': 1,
                        'visibility': 'visible'
                    };
                    break;
            };
        }
    }
    _self.prototype._navigate = function (ref, dir) {
        if (ref.supportTrans && ref.isAnim)
            return false;
        ref.isAnim = true;
        switch (dir) {
            case 'next':
                ref.current = ref.$rightItm.index();
                // current item moves left
                ref.$currentItm.addClass('comp_photo_transition').css(ref._getCoordinates(ref, 'left'));
                // right item moves to the center
                ref.$rightItm.addClass('comp_photo_transition').css(ref._getCoordinates(ref, 'center'));
                // next item moves to the right
                if (ref.$nextItm) {
                    // left item moves out
                    ref.$leftItm.addClass('comp_photo_transition').css(ref._getCoordinates(ref, 'outleft'));
                    ref.$nextItm.addClass('comp_photo_transition').css(ref._getCoordinates(ref, 'right'));
                }
                else {

                    // left item moves right
                    ref.$leftItm.addClass('comp_photo_transition').css(ref._getCoordinates(ref, 'right'));
                }
                break;

            case 'prev':
                ref.current = ref.$leftItm.index();
                // current item moves right
                ref.$currentItm.addClass('comp_photo_transition').css(ref._getCoordinates(ref, 'right'));
                // left item moves to the center
                ref.$leftItm.addClass('comp_photo_transition').css(ref._getCoordinates(ref, 'center'));
                // prev item moves to the left
                if (ref.$prevItm) {

                    // right item moves out
                    ref.$rightItm.addClass('comp_photo_transition').css(ref._getCoordinates(ref, 'outright'));

                    ref.$prevItm.addClass('comp_photo_transition').css(ref._getCoordinates(ref, 'left'));
                }
                else {

                    // right item moves left
                    ref.$rightItm.addClass('comp_photo_transition').css(ref._getCoordinates(ref, 'left'));
                }
                break;
        };
        ref._setItems(ref);
        if (!ref.supportTrans)
            ref.$currentItm.addClass('comp_photo_center');
        ref._cssinit(ref);
    }
    _self.prototype._startSlideshow = function (ref) {
        ref.slideshow = setTimeout(function () {
            ref._navigate(ref, 'next');

            if (ref.options.autoplay) {

                ref._startSlideshow(ref);

            }
        }, ref.options.interval);

    }
    _self.prototype.compatibleAnimal = function (ref) {//检测浏览器是否支持动画
        var names = ["", "ms", "webkit", "moz", "o"];
        var stylelist = $("<div></div>")[0].style;
        if (stylelist["transition"] != undefined) { return true; }
        for (var i = 0; i < names.length; i++) {
            if (stylelist[names + "Transition"] != undefined) { return true; }
        }
        return false;
    }
    _self.prototype.destroy = function (ref) {
        ref.$navPrev.off('.gallery');
        ref.$navNext.off('.gallery');
        ref.$wrapper.off('.gallery');

    }
    module.exports = function () { return _self; }
});
