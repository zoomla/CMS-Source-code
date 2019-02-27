var editor;
var diagLabel = new ZL_Dialog();
function hotkey() {
    var a = window.event.keyCode;
    if (window.event.altKey && a == 88) {
        opentitle("LabelPage.aspx", "选择标签[按ESC键关闭窗口]");
    }
} // end hotkey 
function cit(obj) {
    var code = GetCode(obj);
    PasteValue(code);
}
function cit2(type, code) {
    var code = GetCode(type, code);
    PasteValue(code);
}
function setdiagTitle(title) {
    $("#" + diagLabel.id).find(".modal-title").text(title);
}
function GetCode(obj) {
    var labeltype, code;
    if (arguments.length == 2) {
        labeltype = arguments[0]; code = arguments[1];
    }
    else { var $obj = $(obj); labeltype = $obj.attr("outtype"); code = $obj.attr("code"); }
    switch (labeltype) {
        case "1":
            code = "{ZL.Label id=\"" + code + "\"/}";
            break;
        case "2":
        case "4":
            var link = "Insertlabel.aspx?n=" + escape(code);
            diagLabel.width = "modal-sm";
            diagLabel.height = 212;
            diagLabel.maxbtn = false;
            diagLabel.isBigClose = false;
            diagLabel.foot = "<input type='button' value='Add' class='btn btn-primary' onclick=\"$('#" + diagLabel.id + "').find('iframe')[0].contentWindow.submitdate();\" />&nbsp;<input type='button' class='btn btn-default' value='Close' onclick='closeCuModal();' />"
            //diagLabel.title = "引用标签";
            diagLabel.url = link;
            diagLabel.ShowModal();
            //var ret = window.open(link, window, 'modal=yes,width=500,height=300,menubar=no,toolbar=no,location=no,resizable=no,status=no,scrollbars=no');
            code = "";
            //code = "{ZL.Label id=\"" + code + "\"/}";
            break;
        case "3":
            code = "{ZL.Source id=\"" + code + "\"/}";
            break;
        
        case "5":
            code = "{ZL.Page id=\"" + code + "\"/}"
            break;
        case "6":
            code = "{ZL.Page id=\"" + code + "\" num=\"500\"/}"
            break;
        default:
            break;
    }
    return code;
}
function PasteValue(code) {
    if (!editor || !editor.setOption) { setTimeout(function () { PasteValue(code); }, 500); return;}
    if (code && code != "")
        editor.replaceSelection(code, null, "paste");
}
//--------------
$(function () {
    InitLabelDrag();
    InitEditor();
});
//如果是直接跳转IE下只需要执行一次InitEditor即可,Chrome等仍可两次
function InitEditor() {
    if ($(".CodeMirror").length > 0) { return; }
    editor = CodeMirror.fromTextArea(document.getElementById("textContent"), {
        mode: "text/html",
        tabMode: "indent",
        lineNumbers: true,
        styleActiveLine: true,
        matchBrackets: true,
        lineWrapping: true
    });
    if (editor)
    { editor.setOption("theme", "eclipse"); }
}
function InitLabelDrag() {
    $(".spanfixdiv,.spanfixdivchechk").attr("draggable", true);
    //$(".spanfixdiv,.spanfixdivchechk").bind("dragstart", function (event) { drag(event); })
    //兼容Firefox
    $(".spanfixdiv,.spanfixdivchechk").each(function () {
        var obj = this;
        this.ondragstart = function (e) {
            var code = GetCode(obj);
            e.dataTransfer.setData("Text", code);
        }
    });
    //document.ondragover = function (event) { event.preventDefault(); };
}
function addubb(code) { PasteValue(code); }
//----素材选择与上传
function SelPic(pval) {
    comdiag.maxbtn = false;
    ShowComDiag("/Common/SelFiles.aspx?pval=" + JSON.stringify(pval), "选择图片");
}
function PageCallBack(action, vals, pval) {
    var val = vals.split('|')[0];
    addubb(val);
    CloseComDiag();
}
//----更新标签列表
//根据自定义标签,填充
function GetCustom(obj) {
    var stlp = '<div outtype="@LabelType" code="@LabelName" onclick="cit(this)" class="list-group-item spanfixdivchechk text-left" draggable="true"><a onclick=\'opentitle(\"LabelSql.aspx?LabelName=@LabelN2\",\"修改标签\");\' href="javascript:;" title="修改标签"><span class="fa fa-edit"></span></a><span outtype="@LabelType" code="@LabelName">@LabelName</span></div>';
    $.post("/Design/Diag/Label/LabelCall.aspx", { action: "custom", "cate": obj.value }, function (list) {
        for (var i = 0; i < list.length; i++) {
            list[i].LabelN2 = encodeURI(list[i].LabelName);
        }
        var html = JsonHelper.FillData(stlp, list);
        $("#CustomLabel_div").html(html);
        InitLabelDrag();
    }, "json");
}
function GetField(obj) {
    $.post("/Design/Diag/Label/LabelCall.aspx", { action: "field", "labelid": obj.value }, function (html) {
        html = base64.decode(html);
        $("#Field_div").html(html);
        InitLabelDrag();
    })
}