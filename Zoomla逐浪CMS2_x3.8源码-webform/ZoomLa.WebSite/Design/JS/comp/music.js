define(function (require, exports, module) {
    //同步加载html,加载其他JS与CSS文件
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    _base.utils.inherits(_self, _base.Control);
    _self.prototype.SetInstance_After = function () {
        var ref = this;
        _base.utils.LoadCSS(["/Design/Diag/Music/barui/css/bar-ui.css"], ref.doc);
        ref.instance.load("/Design/Diag/Music/barui/index.html", function () {
            _base.utils.LoadJS(["/Design/Diag/Music/barui/soundmanager2.js", "/Design/Diag/Music/barui/script/bar-ui.js"], ref.doc);
        });
    };
    module.exports = function () { return _self; }
});