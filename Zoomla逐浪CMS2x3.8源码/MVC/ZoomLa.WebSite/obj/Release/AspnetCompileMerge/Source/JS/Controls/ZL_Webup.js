/*
*需ZL_Common,ZL_Dialog
*未实现:预览功能
*使用Json方式,纯支持方式实现
*/
//附件操作JS,需ZL_Common.js,ZL_Dialog支持
//WUFile {name: "test.html", size: 76272, type: "text/html", lastModifiedDate: Thu Apr 16 2015 17:41:02 GMT+0800 (China Standard Time), id: "WU_FILE_0"…}
var MFileUP = function (option) {
    var ref = this;
    var def_config = { id: "uploader", hid: "Attach_Hid", json: { ashx: "action=General", pval: "", accept: "files", compress: false }, nametype: "all" }//divid,hiddenID,给上传控件的Json
    ref.config = $.extend(def_config, option);
    if (window.ZL_Dialog) { ref.attachDiag = new ZL_Dialog(); }
}
MFileUP.prototype.imgli = "<li data-name='@name'><p><img src='@src' /></p>"
                         + "<div class='file-panel' style='height: 0px;'><span class='cancel'>删除</span></div></li>";//图片li模板
MFileUP.prototype.divli = "<li data-name='@name'><div class='imgview'><div class='ext @ex'></div><div class='fname'>@fname</div></div><div class='file-panel' style='height: 0px;'><span class='cancel'>删除</span><a href='/PreView.aspx?vpath=@fsrc' target='_blank' title='预览'><span class='prev'></span></a></div></li>";
MFileUP.prototype.onlyimgli="<li data-name='@name'><p><img src='@src' /></p>"
                         + "<div class='file-panel' style='height: 0px;'><a href='/Plat/Doc/DownFile.aspx?FName=@fsrc' target='_blank' title='下载'><span class='down'></span></a><a href='/PreView.aspx?vpath=@fsrc' target='_blank' title='预览'><span class='prev'></span></a></div></li>";
MFileUP.prototype.onlydivli = "<li data-name='@name'><div class='imgview'><div class='ext @ex'></div><div class='fname'>@fname</div></div><div class='file-panel' style='height: 0px;'><a href='/Plat/Doc/DownFile.aspx?FName=@fsrc' target='_blank' title='下载'><span class='down'></span></a>"
                        + "<a href='/PreView.aspx?vpath=@fsrc' target='_blank' title='预览'><span class='prev'></span></a></div></li>";
MFileUP.prototype.ShowFileUP = function () {
    var ref = this.constructor == Object ?this : ZL_Webup;
    ref.attachDiag.title = "文件上传";
    ref.attachDiag.reload = true;
    ref.attachDiag.backdrop = true;
    ref.attachDiag.maxbtn = false;
    ref.attachDiag.width = "width1100";//Blog
    ref.attachDiag.url = "/Plugins/WebUploader/WebUP.aspx?json=" + JSON.stringify(ref.config.json);//{\"ashx\":\"action=Blog\",\"pval\":\"\"}
    ref.attachDiag.ShowModal();
};
MFileUP.prototype.AddAttach = function (file, ret, pval) {
    var ref = this;
    $uploader = $("#" + ref.config.id);
    $hid = $("#" + ref.config.hid);
    var src = ret._raw;
    var imgli = ref.imgli;
    var divli = ref.divli;
    $uploader.show();
    var li = "", name = src;
    if (ref.config.nametype == "fname") {
        name = GetFname(src);
    }
    if (IsImage(src)) {
        var li = imgli.replace(/@src/, src).replace(/@name/, name).replace(/@fsrc/g, encodeURI(name));
    }
    else {
        var li = divli.replace("@ex", GetExName(src)).replace("@fname", GetFname(src, 6)).replace(/@name/, name).replace(/@fsrc/g, encodeURI(name));
    }
    $uploader.find(".filelist").append(li);
    if (ref.config.nametype == "all") {
        $hid.val($hid.val() + src + "|");
    }
    else if (ref.config.nametype == "fname") {
        $hid.val($hid.val() + GetFname(src, 0) + "|");//仅存文件名,用于防止用户随意指定图片
    }
    ref.BindAttachEvent();
    //其只要返回了信息,便代表已全部传输完成,可以直接关闭.
    ref.attachDiag.CloseModal();
};
MFileUP.prototype.RemoveAttach = function (name) {
    var ref = this;
    $uploader = $("#" + ref.config.id);
    $hid = $("#" + ref.config.hid);
    var attctArr = $hid.val().split('|');
    var result = "";
    for (var i = 0; i < attctArr.length; i++) {
        if (attctArr[i] != name) {
            result += attctArr[i] + "|";
        }
    }
    result = result.replace("||", "|").trim("|");
    $hid.val(result);
    if ($uploader.find(".filelist li").length < 1) { $uploader.hide(); }
}
MFileUP.prototype.BindAttachEvent = function () {
    var ref = this;
    $uploader = $("#" + ref.config.id);
    $uploader.find(".filelist li").mouseenter(function () {
        $btns = $(this).find(".file-panel");
        $btns.stop().animate({ height: 30 });
    }).mouseleave(function () {
        $btns = $(this).find(".file-panel");
        $btns.stop().animate({ height: 0 });
    });
    $uploader.find(".filelist li .cancel").click(function () {
        $li = $(this).closest("li");
        ref.RemoveAttach($li.data("name"));
        $li.remove();
    });
};
MFileUP.prototype.AddReadOnlyLi = function (imgs) {
    if (!imgs || imgs == "") { return; }
    var ref = this;
    $uploader = $("#" + ref.config.id);
    $hid = $("#" + ref.config.hid);
    for (var i = 0; i < imgs.split('|').length; i++) {
        var src = imgs.split('|')[i];
        if (!src || src == "") { continue; }
        var imgli = ref.onlyimgli;
        var divli = ref.onlydivli;
        $uploader.show();
        var li = "", name = GetFname(src);
        if (IsImage(src)) {
            var li = imgli.replace(/@src/g, src).replace(/@name/, name).replace(/@fsrc/g, encodeURI(src));
        }
        else {
            var li = divli.replace("@ex", GetExName(src)).replace("@fname", GetFname(src, 6)).replace(/@name/, name).replace(/@fsrc/g, encodeURI(src));
        }
        $uploader.find(".filelist").append(li);
    }
    ref.BindAttachEvent("preview");
}
var ZL_Webup = new MFileUP();
var SFileUP = {
    AjaxUpFile: function (fid,callback) {
        var formdata = new FormData();
        var fileup = null;
        if (typeof (fid) == "object") { fileup = fid; }
        else { fileup = document.getElementById(fid); }
        //-------------------------------------------
        if (!fileup || fileup.files.length < 1) { console.log("[" + fid + "]上传控件不存在,或值为空"); return; }
        formdata.append("file", fileup.files[0]);
        $.ajax({
            type: 'POST',
            url: '/Plugins/Uploadify/UploadFileHandler.ashx',
            data: formdata,
            processData: false,
            contentType: false,
            success: callback
        });
    },
    AjaxUpBase64:function(base64str,callback){
        $.post('/Plugins/Uploadify/Base64.ashx', { base64: base64str }, callback);
    },
    isWebImg: function (fname) {
        if (!fname || fname == "") { return false; }
        fname = fname.toLowerCase();
        if (fname.indexOf("data:image/") > -1)//base64
        {
            return true;
        }
        else if (fname.indexOf(".") > 0) {
            var start = fname.lastIndexOf(".");
            var ext = fname.substring((start + 1), fname.length);//jpg|png|gif
            return (ext == "jpg" || ext == "png" || ext == "gif"||ext=="jpeg")
        }
        else { return false; }
    }
}
var picup = {};
picup.$up = $('<input type="file" accept="image/jpg,image/png,image/jpeg" id="pic_up" onchange="picup.upload();" />');
picup.sel = function () { picup.$up.click(); }
picup.upload = function () {
    var ref=this;
    if (ref.$up.val() == "") { console.log("未选择文件"); return; }
    if (ref.up_before) { ref.up_before(); }
    if (ref.zip.enable === true) {
        if (!window.lrz) { console.log("启用了压缩上传,但未引入lrz"); return; }
        //转为base64压缩后上传
        lrz(ref.$up[0].files[0], ref.zip, function (r) {
            SFileUP.AjaxUpBase64(r.base64, function (data) {
                if (ref.up_after) { ref.up_after(data); }
                else { console.log("未匹配到"); }
            });
        });
    }
    else {
        SFileUP.AjaxUpFile(ref.$up[0], function (data) { if (ref.up_after) { ref.up_after(data); } });
    }
}
picup.up_before = null;
picup.up_after = null;
//如果是长图,则应该客户端剪切完成后再压缩上传
picup.zip = { enable: false, width: 640, quality: 0.8 };//压缩配置,如配置了该项,则启用压缩功能
//------------------------MessageSend,MessageRead
//var pic = { id: "pic_up", txtid: null };
//pic.sel = function (id) { $("#" + pic.id).val(""); $("#" + pic.id).click(); pic.txtid = id; }
//pic.upload = function () {
//    var fname = $("#" + pic.id).val();
//    if (!SFileUP.isWebImg(fname)) { alert("请选择图片文件"); return false; }
//    SFileUP.AjaxUpFile(pic.id, function (data) {
//        var url = "<%:ZoomLa.Components.SiteConfig.SiteInfo.SiteUrl%>";
//        $("#" + pic.txtid).val(url + data);
//    });
//}
//pic.preSave = function () {var src = $("#pic_img").attr("src");$("#pic_hid").val(src);}
//------------------------Note\AddProject.aspx
//$(function () {
//    ZL_Webup.config.json.ashx = "action=OAattach";
//    $("#upfile_btn").click(ZL_Webup.ShowFileUP);
//})
//function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }