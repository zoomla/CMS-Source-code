//依赖ZL_Dialog.js
var iconSelctor = function (id,opt) {
    this.id = id;
    this.btnobj = null;
    this.imgobj = null;
    this.textobj = null;
    this.opt = null;
    this.diag = new ZL_Dialog();
    this.Init();
};
iconSelctor.prototype.tlp = '<input type="text" id="icon_text_@id" class="form-control text_300" /> <button type="button" id="icon_btn_@id" class="btn btn-primary">图片地址或奥森图标</button> <span id="icon_span_@id"></span>';
iconSelctor.prototype.Init = function () {
    var ref = this;
    $("#" + ref.id).hide();
    var html = ref.tlp.replace(/@id/g, ref.id).replace(/@remark/g,ref.remark);
    $(html).insertBefore($("#" + ref.id));
    ref.btnobj = $("#icon_btn_" + ref.id);
    ref.imgobj = $("#icon_span_" + ref.id);
    ref.textobj = $("#icon_text_" + ref.id);
    ref.textobj.val($("#" + ref.id).val());
    ref.UpdateIcon();
    ref.InitEvent();
    $("form").submit(function () {
        ref.UpdateIcon();
    });
}
iconSelctor.prototype.InitEvent = function () {
    var ref = this;
    ref.btnobj.click(function () {
        ref.ShowDiag();
    });
    ref.textobj.bind("keyup cut paste", function () {
        setTimeout(function () { ref.UpdateIcon(); }, 100);
    });
}
iconSelctor.prototype.ShowDiag = function () {
    var ref = this;
    ref.diag.title = "选择图标";
    ref.diag.ajaxurl = "/Common/icon.html?ver=2";
    ref.diag.ajaxcallback = function () {
        $("#iconlist").attr('id', 'icon_sel_' + ref.id);
        $("#icon_sel_" + ref.id + " div").click(function () {
            ref.textobj.val($(this).find("input[name=glyphicon]").val());
            ref.UpdateIcon();
            ref.diag.CloseModal();
            //$(".SystemIcon div").click(function () {
            //    $(this).find("input[name=glyphicon]").click();
            //});
        });
    };
    ref.diag.ShowModal();
}
iconSelctor.prototype.UpdateIcon = function () {//根据输入框的值改变图标
    var ref = this;
    var iconstr = ref.textobj.val();
    if (iconstr.indexOf("/") > -1) {
        ref.imgobj.html("<img src='" + iconstr + "' style='width:30px; height:30px;' onerror=\"this.src='/Images/nopic.gif'\" />");
    } else {
        ref.imgobj.html("<i class='" + iconstr + "' style='font-size:20px;'></i>");
    }
    $("#" + ref.id).val(ref.textobj.val());
}