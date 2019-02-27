var ImgHelper = {};
//base64或jpg,png,gif格式返回true
ImgHelper.isWebImg = function (fname) {
    if (fname.indexOf("data:image/") > -1) { return true; }
    return ImgHelper.isAllow(fname, "jpg,png,gif");
}
ImgHelper.isPng = function (fname) {
    return ImgHelper.isAllow(fname, "png");
}
//获取后缀名(小写,不含.)
ImgHelper.getExt = function (fname) {
    if (!fname || fname == "" || fname.indexOf(".") < 0) { return ""; }
    fname = fname.replace(/ /g, "");
    fname = fname.toLowerCase();
    var start = fname.lastIndexOf(".");
    var ext = fname.substring((start + 1), fname.length);
    return ext;
}
//文件的后缀名是否允许以,切割
ImgHelper.isAllow = function (fname, extArr) {
    var ext = ImgHelper.getExt(fname);
    if (ext == "") { return false; }
    extArr = extArr.split(",");
    for (var i = 0; i < extArr.length; i++) {
        if (extArr[i] == ext) { return true; }
    }
    return false;
}
//返回图片信息json,width,height等
ImgHelper.getImgInfo = function (imgid) {
    //必须动态生成,并且load之后再返回数据
    //isError是否为一个有效的链接
    //未完成,图像需要onload后才能获取到值
    var model = { id: imgid, width: 0, height: 0, width_css: 0, height_css: 0, src: "" };
    var $img = $("#" + imgid);
    if ($img.length < 1) { return null; }
    model.src = $img.attr("src");
    model.width = $("#" + imgid)[0].naturalWidth;//仅数字,ie9
    model.height = $("#" + imgid)[0].naturalHeight;
    model.width_css = $img.css("width");//带px
    model.height_css = $img.css("height");
    return model;
}