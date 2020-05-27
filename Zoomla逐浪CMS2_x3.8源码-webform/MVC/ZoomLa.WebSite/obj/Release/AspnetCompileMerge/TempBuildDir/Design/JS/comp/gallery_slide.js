define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    _self.prefix = "comp_slide";//暂不用
    _self.prototype.wrapper = '<div ng-model="list.@id" id="@id" class="onlydrag" style="{{list.@id.config.style}}">{html}</div>';
    _self.prototype.Init = function (model, extend) {
        var ref = this;
        ref._init(model, extend);
        ref.editurl = "/Design/Diag/Gallery/Edit.aspx?id=" + ref.id;
        ref.target = [];
        ref.index = 0;
        ref.timer = null;
        ref.offset = 5000;//间隔
        for (var i = 0; i < model.dataMod.list.length; i++) {
            var item = model.dataMod.list[i];
            ref.target.push(item.orderid);
        }
    }
    _self.prototype.SetInstance_After = function () {
        var ref = this;
        setTimeout(function () {
            ref.instance.find('div.comp_slide_word').css({ opacity: 0.85 });
            ref.auto(ref);
            ref.hookThumb(ref);
            ref.hookBtn(ref);
            ref.bighookBtn(ref);
        }, 1000);
    }
    //------------------------------
    var html = "<div style=\"width: 958px; height: 620px; border: #d9e0ea 1px solid;\">"
+ "    <div style=\"margin: 0px auto; width: 958px; background: #fff; height: 620px; \">"
+ "        <div style=\"margin: 0px auto; width: 774px; height: 436px; overflow: hidden\">"
+ "            <div style=\"margin: 0px auto; width: 774px; height: 436px; overflow: hidden; \" id=\"bigpicarea\">"
+ "                <p style=\"z-index: 100; position: absolute; width: 35px; height: 51px; TOP: 186px; cursor: pointer; left: 30px\"><span id=\"big_play_prev\" style=\"background-image: url(/Design/Diag/gallery/slide/img/leftbig.png); width: 35px; display: block; height: 51px; _background: none; \"></span></p>"
+ "                <div id=\"image_{{item.orderid}}\" ng-repeat=\"item in list.@id.dataMod.list|orderBy:'orderid'\">"
+ "                    <a href=\"{{item.click}}\"><img src=\"{{item.url}}\" style=\"width:772px;height:434px;\" /></a>"
+ "                    <div class=\"comp_slide_word\">{{item.name}}</div>"
+ "                </div>"
+ "                <p style=\"z-index: 100; position: absolute; width: 35px; height: 51px; top: 186px; cursor: pointer; right: 30px\"><span id=\"big_play_next\" style=\"background-image: url(/Design/Diag/gallery/slide/img/rightbig.png); width: 35px; display: block; height: 51px; _background: none; \"></span></p>"
+ "            </div>"
+ "        </div>"
+ "        <div style=\"margin: 0px auto; width: 958px; border-top: #ececec 1px solid; padding-top: 20px\">"
+ "            <div id=\"thumbs\" style=\"margin: 0px auto; width: 830px; height: 110px\">"
+ "                <ul class=\"list-unstyled\">"
+ "                    <li style=\"margin: 25px 10px 0px 15px; width: 9px; height: 16px; cursor: pointer; float: left; \"><img id=\"play_prev\" src=\"/Design/Diag/gallery/slide/img/left.png\" /></li>"
+ "                    <li ng-repeat=\"item in list.@id.dataMod.list|orderBy:'orderid'\" style=\"margin: 0px 7px; width: 90px; display: inline; float: left; height: 60px\">"
+ "                        <a id=\"thumb_{{item.orderid}}\" href=\"javascript:;\" style=\"border: #fff 2px solid; width: 90px; display: block;\"><img src=\"{{item.url}}\" style=\"width:90px;height:60px;\" /></a>"
+ "                    </li>"
+ "                    <li style=\"margin: 25px 10px 0px 15px; width: 9px; height: 16px; cursor: pointer;float:left; \"><img id=\"play_next\" src=\"/Design/Diag/gallery/slide/img/right.png\" /></li>"
+ "                </ul>"
+ "            </div>"
+ "        </div>"
+ "    </div>"
+ "</div>"
    _self.prototype.htmlTlp = html;
    //大图交替轮换
    _self.prototype.slideImage = function (ref, i) {
        var id = 'image_' + ref.target[i];
        ref.instance.find('#' + id)
            .animate({ opacity: 1 }, 800, function () {
                $(this).find('.comp_slide_word').animate({ height: 'show' }, 'slow');
            }).show()
            .siblings(':visible')
            .find('.comp_slide_word').animate({ height: 'hide' }, 'fast', function () {
                $(this).parent().animate({ opacity: 0 }, 800).hide();
            });
    }
    //bind thumb a
    _self.prototype.hookThumb = function (ref) {
        ref.instance.find('#thumbs li a')
            .bind('click', function () {
                if (ref.timer) {clearTimeout(ref.timer); }
                var id = this.id;
                ref.index = ref.getIndex(ref,id.substr(6));
                ref.rechange(ref, ref.index);
                ref.slideImage(ref, ref.index);
                ref.timer = window.setTimeout(function () { ref.auto(ref); }, ref.offset);
                this.blur();
                return false;
            });
    }
    //bind next/prev img
    _self.prototype.hookBtn = function (ref) {
        ref.instance.find('#thumbs li img').filter('#play_prev,#play_next')
            .bind('click', function () {
                if (ref.timer) {clearTimeout(ref.timer); }
                var id = this.id;
                if (id == 'play_prev') {
                    ref.index--;
                    if (ref.index < 0) ref.index = ref.target.length;
                } else {
                    ref.index++;
                    if (ref.index >= ref.target.length) ref.index = 0;
                }
                ref.rechange(ref, ref.index);
                ref.slideImage(ref,ref.index);
                ref.timer = window.setTimeout(function () { ref.auto(ref); }, ref.offset);
            });
    }
    _self.prototype.bighookBtn = function (ref) {
        ref.instance.find('#bigpicarea p span').filter('#big_play_prev,#big_play_next')
            .bind('click', function () {
                if (ref.timer) {clearTimeout(ref.timer); }
                var id = this.id;
                if (id == 'big_play_prev') {
                    ref.index--;
                    if (ref.index < 0) ref.index =ref.target.length;
                } else {
                    ref.index++;
                    if (ref.index >= ref.target.length) ref.index = 0;
                }
                ref.rechange(ref,ref.index);
                ref.slideImage(ref,ref.index);
                ref.timer = window.setTimeout(function () { ref.auto(ref); }, ref.offset);
            });
    }
    //get index
    _self.prototype.getIndex = function (ref,v) {
        for (var i = 0; i < ref.target.length; i++) {
            if (ref.target[i] == v) return i;
        }
    }
    _self.prototype.rechange = function (ref, loop) {
        var id = 'thumb_' + ref.target[loop];
        ref.instance.find('#thumbs li a.current').removeClass('current');
        ref.instance.find('#' + id).addClass('current');
    }
    _self.prototype.auto = function (ref) {
        ref.index++;
        if (ref.index >= ref.target.length) {
            ref.index = 0;
        }
        ref.rechange(ref,ref.index);
        ref.slideImage(ref, ref.index);
        timer = window.setTimeout(function () { ref.auto(ref); }, ref.offset);
    }
    //-------------------
    module.exports = function () { return _self; }
});