define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    //预览时使用
    _self.prototype.htmlTlp = '<div class="share_box">'
                            + '<div class="bdsharebuttonbox pull-right"><a href="#" class="bds_renren" data-cmd="renren" title="分享到人人网"></a><a href="#" class="bds_qzone" data-cmd="qzone" title="分享到QQ空间"></a><a href="#" class="bds_tqq" data-cmd="tqq" title="分享到腾讯微博"></a><a href="#" class="bds_tsina" data-cmd="tsina" title="分享到新浪微博"></a><a href="javascript:;" class="bds_more" data-cmd="more"></a></div>'
                            + '</div>';
    _self.prototype.htmlTlp += '<script>window._bd_share_config = { "common": { "bdSnsKey": {}, "bdText": "" + document.URL, "bdUrl": document.URL, "bdMini": "2", "bdMiniList": false, "bdPic": "", "bdStyle": "2", "bdSize": "32" }, "share": {} };';
    _self.prototype.htmlTlp += "with (document) 0[(getElementsByTagName('head')[0] || body).appendChild(createElement('script')).src = 'http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion=' + ~(-new Date() / 36e5)];";
    _self.prototype.htmlTlp += '</script>';
    //设计时使用
    _self.prototype.designTlp = '';
    _self.prototype.designTlp += '<div class="share_box" style="width:200px;"><div class="bdsharebuttonbox bdshare-button-style2-32">'
                              + '<a href="javascript:;" title="分享到人人网" style="background-position:0 -208px;"></a><a href="javascript:;" title="分享到QQ空间" style="background-position:0 -52px;"></a><a href="javascript:;" title="分享到腾讯微博" style="background-position:0 -260px;"></a><a href="javascript:;" title="分享到新浪微博" style="background-position:0 -104px;"></a><a href="javascript:;" class="bds_more" data-cmd="more" title="更多"></a>'
                              + '</div>';
    module.exports = function () { return _self; }
});