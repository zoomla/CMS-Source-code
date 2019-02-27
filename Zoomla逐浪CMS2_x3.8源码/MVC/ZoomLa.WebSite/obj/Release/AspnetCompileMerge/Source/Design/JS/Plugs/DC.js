define(function (require, exports, module) {
    var $ = require("jquery");
    var APIResult = require("APIResult");
    var dc = {
        node: {},
        content: {},
        site: {},//站点信息
    };
    dc.post = function (json, callback) {
        var url = sitecfg.api + "?siteid=" + sitecfg.siteid;
        $.post(url, json, function (data) {
            var model = APIResult.getModel(data);
            if (APIResult.isok(model)) {
                if (callback) { callback(model); }
            }
            else { console.log(json.action + "失败,原因:" + model.retmsg); }
        });
    }
    //-----------------
    //获取节点列表,为空则取全部
    dc.node.sel = function (callback, nid) {
        dc.post({ action: "node_sel", "nid": nid }, callback);
    }
    //根据节点,筛选其下的文章,支持IDS,为空则取全部
    dc.content.selbynid = function (callback, nid, cpage, psize) {
        dc.post({ action: "content_SelByNid", "nid": nid, "cpage": cpage, "psize": psize }, callback)
    }
    //返回内容详情
    dc.content.selbyid = function (callback, gid) {
        dc.post({ action: "content_SelByid", "gid": gid }, callback);
    }
    //返回当前站点的信息
    dc.site.sel = function (callback) {
        dc.post({ action: "site_sel" }, callback);
    }
    //-------------------单文件上传
    dc.SFileUP = {
        AjaxUpFile: function (fid, callback) {
            var fileup = document.getElementById(fid);
            if (!fileup || fileup.files.length < 1) { console.log("[" + fid + "]上传控件不存在,或值为空"); return; }
            var formdata = new FormData();
            formdata.append("file", fileup.files[0]);
            $.ajax({
                type: 'POST',
                url: "/Plugins/Uploadify/UploadFileHandler.ashx?action=design",
                data: formdata,
                processData: false,
                contentType: false,
                success: callback
            });
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
                return (ext == "jpg" || ext == "png" || ext == "gif" || ext == "jpeg")
            }
            else { return false; }
        }
    }
    //-------------------
    module.exports = dc;
});