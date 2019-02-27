define(function (require, exports, module) {
    var _self = function () { }, _base = require("base"), $ = require('jquery');
    var JsonHelper = require("JsonHelper");
    _base.utils.inherits(_self, _base.Control);
    //----------------------
    //用于图片布局,传入参数后,在指定位置输出布局好的图片
    //1,使用何种模式布局,每列数量或自定义数量?
    var GalleryGrid = function () { };
    //0为不限定行与列的数量,否则有最大数量限定
    //mode:需要展示的模式
    GalleryGrid.prototype.config = { id: "", mode: "flow", row: 1, col: 3 };
    GalleryGrid.prototype.instance = null;
    GalleryGrid.prototype.items = [];//存储图片模型数据
    //GalleryGrid.prototype.imgTlp = '<a href="javascript:;"><div style="width:@width;height:@height;display:inline-block;background:url(@url) center no-repeat;" /></a>';
    GalleryGrid.prototype.imgTlp = '<a href="javascript:;"><img src="@url" style="width:@width;height:@height;" /></a>';
    //click后是字符串,则跳转,是指定字符则预览或新窗口打开,是方法则执行
    GalleryGrid.prototype.GetImgModel = function () { return { name: "", tip: "", orderid: 0, url: "", target: "", click: null } };
    GalleryGrid.prototype.Init = function () {
        var ref = this;
        var config = ref.config;
        var $body = $("#" + ref.config.id);
        if (ref.instance) { $body = ref.instance; }
        var width = (100 / config.col) + "%";
        var height = (100 / config.row) + "%";
        for (var i = 0; i < ref.items.length; i++) {
            ref.items[i].width = width;
            ref.items[i].height = height;
        }
        //根据需要,绑定单击,或预览,或跳转
        var $result = JsonHelper.FillItem(ref.imgTlp, ref.items, function ($item, mod) {
            if (!mod.click || mod.click == "") { return; }
            var type = typeof (mod.click);// string function object
            switch (type) {
                case "string":
                    if (mod.click == "view") { }
                    else {
                        $item.attr("href", mod.click);
                        $item.attr("target", mod.target);
                    }
                    break;
                case "function":
                    $item.click = function () { mod.click($item, mod); }
                    break;
                default:
                    break;
            }
        });
        $body.append($result);
    }
    //----------------------
    _self.prototype.SetInstance_After = function () {
        var grid = new GalleryGrid();
        grid.instance = this.instance;
        grid.items = this.dataMod.list;//?
        grid.Init();
    }
    module.exports = function () { return _self; }
});