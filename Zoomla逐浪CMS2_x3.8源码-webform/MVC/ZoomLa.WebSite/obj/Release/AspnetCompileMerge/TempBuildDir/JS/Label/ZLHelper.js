var ZLHelper = {};
ZLHelper.ToWordByID = function (id, name) {
    var html = document.getElementById(id).innerHTML;
    ZLHelper.OutToWord(html, name);
}
ZLHelper.ToExcelByID = function (id, name) {
    var html = document.getElementById(id).innerHTML;
    ZLHelper.OutToExcel(html, name);
}
//可对html自定义处理后,再利用其导出
ZLHelper.OutToWord = function (html, name) {
    if (name == undefined || !name) { name = ""; }
    if (name && name != "") { name = escape(name); }
    var $form = $('<form target="_blank"  method="post" action="/Common/Label/OutToWord.aspx?name=' + name + '"></form>');
    $form.append('<input type="hidden" name="html_toword_hid" value=' + encodeURI(html) + '>');
    $("body").append($form);//兼容IE
    $form.submit();
    $form.remove();
}
ZLHelper.OutToExcel = function (html, name) {
    if (name == undefined || !name) { name = ""; }
    if (name && name != "") { name = escape(name); }
    var $form = $('<form target="_blank"  method="post" action="/Common/Label/OutToExcel.aspx?name=' + name + '"></form>');
    $form.append('<input type="hidden" name="html_toword_hid" value=' + encodeURI(html) + '>');
    $("body").append($form);//兼容IE
    $form.submit();
    $form.remove();
}