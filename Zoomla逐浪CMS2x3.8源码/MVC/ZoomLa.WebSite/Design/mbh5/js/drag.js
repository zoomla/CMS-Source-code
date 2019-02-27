//# sourceMappingURL=hammer.min.js.map
// 封装
/*  press(长按)，
    pan(拖动、平移),
    rotate(旋转),
    tap(单击),
    doubleTap(双击),
    pinch(缩放),
    swipe(滑动)
*/
(function ($) {
    var reqAnimationFrame = (function () {
        return window[Hammer.prefixed(window, 'requestAnimationFrame')] ||
        function (callback) {
            window.setTimeout(callback, 1000 / 60);
        };
    })();
    //element为需要初始化的div
    var dragElement = function (element, option) {
        var funthis = this;
        this.element = element;
        this.initialize = function () {
            element.addClass("dragElement").css("overflow", "");
            element.find('.dragElement-btn').remove();
            element.find('.rotateElement-btn').remove();

            element.append('<div class="rotateElement-btn"></div>'
                            + '<div class="dragElement-btn"></div>');

            var el = element[0],
                START_X = el.offsetLeft,
                START_Y = el.offsetTop,
                ticking = false,
                transform = {
                    width: element.width(),
                    height: element.height(),
                    translate: { x: START_X, y: START_Y },
                    scale: 1,
                    angle: 0,
                    rx: 0,
                    ry: 0,
                    rz: 0,
                    type: '',
                    rotate: 0
                };

            //通过Manager设置自己的识别器的实例。
            var mc = new Hammer.Manager(element.find('.dragel_inner')[0]);
            //触摸手势对象绑定到hammer.js对象
            mc.add(new Hammer.Pan({ direction: Hammer.DIRECTION_ALL, threshold: 0, pointers: 1 }));
            mc.add(new Hammer.Pinch({ threshold: 0 })).recognizeWith([mc.get('pan')]);
            // mc.add(new Hammer.Tap({ event: 'doubletap', taps: 2 }));
            mc.add(new Hammer.Tap());
            mc.add(new Hammer.Press({ time: 500 }));

            //绑定手势事件
            mc.on("panstart panmove panend", onPan);
            mc.on("pinchstart pinchmove", onPinch);//缩放
            mc.on("tap", onTap);//单击
            // mc.on("doubletap", onDoubleTap);//双击
            mc.on("press", onPress);//长按

            funthis.mc = mc;

            var mc2 = new Hammer(element.find('.dragElement-btn')[0]);
            mc2.get('pan').set({ direction: Hammer.DIRECTION_ALL });
            mc2.on('panstart panmove panend', onTap2);

            var mc3 = new Hammer(element.find('.rotateElement-btn')[0]);
            mc3.get('pan').set({ direction: Hammer.DIRECTION_ALL });
            mc3.on('panstart panmove panend', onTap3);


            //重置
            function resetElement() {
                START_X = el.offsetLeft;//四舍五入
                START_Y = el.offsetTop;

                transform.width = element.width(),
                transform.height = element.height(),

                transform.translate.x = START_X;
                transform.translate.y = START_Y;

                initScale = 1;
                transform.scale = 1;

                transform.moveX = 0;
                transform.moveY = 0;

            }

            //更新transform
            function updateElementTransform() {
                var _width = transform.width * transform.scale,
                    _height = transform.height * transform.scale;

                _width < 2 ? _width = 2 : '';
                _height < 2 ? _height = 2 : '';

                if (targetEl.hasClass('btn')) {
                    _width < 99 ? _width = 99 : '';
                }

                element.css({
                    "top": transform.translate.y,
                    "left": transform.translate.x,
                    "width": _width,
                    "height": _height
                });
                targetEl.css({
                    "top": transform.translate.y,
                    "left": transform.translate.x,
                    "width": _width,
                    "height": _height
                });
                ticking = false;

            }
            function requestElementUpdate(update) {
                if (!ticking) {
                    reqAnimationFrame(update);
                    ticking = true;
                }
            }

            var iscurchange = true;
            function onPan(ev) {        //拖动
                if (ev.type == 'panstart') {
                    tools.menu.hide();
                    scence.lock(); // 当点在元素上时，锁定swiper滑动
                    iscurchange = true;
                }

                if (iscurchange) {
                    if (ev.distance > 7) {
                        $(editor.id).find('.el-menu').remove();
                        $(editor.id).find('.press-menu').remove();
                    }
                    resetElement();
                    iscurchange = false;
                }

                transform.translate = {
                    x: START_X + ev.deltaX,
                    y: START_Y + ev.deltaY
                };

                if (ev.type == 'panend') {
                    START_X = transform.translate.x;
                    START_Y = transform.translate.y;
                    if (global.lock === false) { scence.unlock(); }
                    iscurchange = true;
                    tools.show();
                }
                requestElementUpdate(updateElementTransform);
            }
            var initScale = 1;
            function onPinch(ev) {      //两指捏动
                if (ev.type == 'pinchstart') {
                    resetElement();
                    initScale = transform.scale || 1;
                }
                transform.scale = initScale * ev.scale;



                requestElementUpdate(updateElementTransform);
            }
            var initAngle = 0;
            function onRotate(ev) {     //旋转

                transform.type = ev.type;
                if (ev.type == 'rotatestart') {
                    initAngle = transform.angle || 0;
                }
                transform.rz = 1;
                transform.angle = initAngle + ev.rotation;
                if (ev.type == 'rotateend') {
                    initAngle = transform.angle;
                }
                requestElementUpdate(updateElementTransform);
            }

            var _dif = '';

            function onTap(ev) {        //单击事件
                resetElement();
            }
            funthis.onTap = onTap;

            var presshtml = $('<section class="press-menu"><span class="press-shang"></span>'
                        + '<span class="press-xia"></span><span class="press-top"></span>'
                        + '<span class="press-bottom"></span></section>');
            //按压,可关键右键菜单
            function onPress(ev) {
                resetElement();

                $(editor.id).find('.el-menu').remove();
                $(editor.id).find('.press-menu').remove();
                myani.swiper.currentPage.find(".pageshow").append(presshtml);

                var _top = transform.translate.y + transform.height / 2 - 80,
                    _left = transform.translate.x - 40;
                presshtml.css({
                    "top": _top < 0 ? 0 : (_top + 165 > 520 ? 520 - 165 : _top),
                    "left": _left < 0 ? 0 : (_left + 37 > 330 ? 330 - 37 : _left),
                });

                ev.preventDefault();
            }

            function updateElementScale() {
                var _width = transform.width + transform.moveX,
                    _height = transform.height + transform.moveY;
                if (tools.comp && tools.comp.resize) { tools.comp.resize({ width: _width, height: _height }); }

                _width < 2 ? _width = 2 : '';
                _height < 2 ? _height = 2 : '';
                if (targetEl.hasClass('btn')) {
                    _width < 99 ? _width = 99 : '';
                }

                element.css({
                    "width": _width,
                    "height": _height
                });
                targetEl.css({
                    "width": _width,
                    "height": _height
                });
                ticking = false;
            }

            var __rotation = 0,
                __initial_angle = 0,
                getAngle = function (p1, p2) {
                    return Math.atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Math.PI;
                },
                getAngle180 = function (p1, p2) {
                    var agl = Math.atan((p2.y - p1.y) * -1 / (p2.x - p1.x)) * (180 / Math.PI);
                    return (agl < 0 ? (agl + 180) : agl);
                },
                getAngleDiff = function (p1, p2) {
                    var diff = parseInt(__initial_angle - getAngle180(p1, p2), 10);
                    var count = 0;

                    while (Math.abs(diff - __rotation) > 90 && count++ < 10) {
                        if (__rotation < 0) {
                            diff -= 180;
                        } else {
                            diff += 180;
                        }
                    }
                    __rotation = parseInt(diff, 10);
                    return __rotation;
                },
                getAngle360 = function (p1, p2) {
                    var anl = Math.atan2(p2.y - p1.y, p2.x - p1.x) * 180 / Math.PI + 45;
                    anl = (anl < 0) ? anl + 360 : anl;
                    return anl;
                };
            //拖动调整大小
            function onTap2(ev) {
                if (ev.type == 'panstart') {
                    resetElement();
                    scence.lock(); // 当点在元素上时，锁定swiper滑动
                    $(editor.id).find('.el-menu').remove();
                    $(editor.id).find('.press-menu').remove();
                }
                transform.moveX = ev.deltaX;
                transform.moveY = ev.deltaY;

                requestElementUpdate(updateElementScale);
                if (ev.type == 'panend') {
                    if (global.lock === false) { scence.unlock(); } //当事件结束时，解锁swiper滑动
                }
            }
            function updateElementRotate() {
                element.css({
                    "transform-origin": "50% 50% 0",
                    "-webkit-transform-origin": "50% 50% 0",
                    "-moz-transform-origin": "50% 50% 0",
                    "transform": 'perspective(400px) rotateX(0deg) rotateY(0deg) rotateZ(' + transform.angle + 'deg)',
                    "-webkit-transform": 'perspective(400px) rotateX(0deg) rotateY(0deg) rotateZ(' + transform.angle + 'deg)',
                    "-moz-transform": 'perspective(400px) rotateX(0deg) rotateY(0deg) rotateZ(' + transform.angle + 'deg)'
                });
                targetEl.find('.comp_contain').css({
                    "transform-origin": "50% 50% 0",
                    "-webkit-transform-origin": "50% 50% 0",
                    "-moz-transform-origin": "50% 50% 0",
                    "transform": 'perspective(400px) rotateX(0deg) rotateY(0deg) rotateZ(' + transform.angle + 'deg)',
                    "-webkit-transform": 'perspective(400px) rotateX(0deg) rotateY(0deg) rotateZ(' + transform.angle + 'deg)',
                    "-moz-transform": 'perspective(400px) rotateX(0deg) rotateY(0deg) rotateZ(' + transform.angle + 'deg)'
                }).attr({
                    'rotdegx': 0, 'rotdegy': 0, 'rotdegz': transform.angle
                });

                ticking = false;
            }
            //旋转
            function onTap3(ev) {
                if (ev.type == 'panstart') {
                    resetElement();
                    scence.lock(); // 当点在元素上时，锁定swiper滑动
                    $(editor.id).find('.el-menu').remove();
                    $(editor.id).find('.press-menu').remove();

                    var ro1 = $("<div></div>").css({
                        'position': "absolute",
                        'top': 0,
                        'left': 0
                    }).appendTo(element);
                    var ro2 = $("<div></div>").css({
                        'position': "absolute",
                        'bottom': 0,
                        'right': 0
                    }).appendTo(element);
                    var o1 = ro1.offset(),
                        o2 = ro2.offset();
                    transform.st = {
                        x: o1.left + (o2.left - o1.left) / 2,
                        y: o1.top + (o2.top - o1.top) / 2
                    }

                    ro1.remove();
                    ro2.remove();
                }

                transform.af = {
                    'x': ev.pointers[0].pageX,
                    'y': ev.pointers[0].pageY
                };
                console.log(transform.st);
                console.log(transform.af);
                transform.angle = getAngle360(transform.st, transform.af);

                requestElementUpdate(updateElementRotate);

                if (ev.type == 'panend') {
                    if (global.lock === false) { scence.unlock(); }// 当事件结束时，解锁swiper滑动
                }
            }
        }
        this.destroy = function () {
            this.element.removeClass('dragElement');
            this.element.find('dragElement-btn').remove();
            this.element.find('rotateElement-btn').remove();
        }
        this.element[0].dragElement = this;

        this.initialize();
    }

    $.fn.dragElement = function (option) {
        if (!(this.hasClass('dragElement'))) {
            return new dragElement(this, option);
        }
    }

})(window.jQuery || window.Zepto);