define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    //上传至本站的在线视频(默认)
    _self.prototype.onlineTlp = "<video width='{{list.@id.dataMod.videoStyle.width}}' height='{{list.@id.dataMod.videoStyle.height}}' class='edui-upload-video  vjs-default-skin video-js' src='@src' poster='{{list.@id.dataMod.poster}}' preload='none' data-setup='{}' @autoplay  @loop controls=''></video>";
    _self.prototype.Init = function (model) {
        switch (model.config.compType) {
            case "file"://视频文件,本站或其他站点视频文件
                _self.prototype.htmlTlp += "<div>" + _self.prototype.onlineTlp + "</div>";
                break;
            case "online"://在线视频 embed
                _self.prototype.htmlTlp += "<div class='diy_comp'>" + decodeURI(model.config.htmlTlp) + "</div>";
                break;
        }
        //属性替换
        var loop = "", autoplay = "";
        if (model.config.loop == true) { loop = 'loop="loop"'; }
        if (model.config.autoplay == true) { autoplay = 'autoplay="autoplay"'; }
        this.htmlTlp = this.htmlTlp.replace(/@loop/ig, loop).replace(/@autoplay/ig, autoplay);
        this._init(model);
    }
    module.exports = function () { return _self; }
});