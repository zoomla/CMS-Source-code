//-----------------KeyWord
function InitKeyWord(value) {
    tabarr = [];
    $("#Examkeyword").html('');
    if ($("#Examkeyword").length > 0) {
        $("#Examkeyword").tabControl({
            tabW: 80,
            onAddTab: function (value) {
                tabarr.push(value);
            },
            onRemoveTab: function (removeval) {
                for (var i = 0; i < tabarr.length; i++) {
                    if (tabarr[i] == removeval) {
                        tabarr.splice(i, 1);
                    }
                }
            }
        }, value);
    }//关键词
}
function ShowKeyWords() {
    comdiag.reload = true;
    comdiag.maxbtn = false;
    comdiag.width = "none";
    ShowComDiag("/Common/SelKeyWords.aspx?type=2", "选择关键字");
}
function GetKeyWords(keystr) {
    tabarr = tabarr.concat(keystr.split(','));
    var values = "";
    var length = tabarr.length <= 5 ? tabarr.length : 5;
    for (var i = 0; i < length; i++) {
        values += tabarr[i] + ",";
    }
    InitKeyWord(values);
    CloseComDiag();
}
//知识点
function SelKnow() {
    if ($("#NodeID_Hid").val() == "") { alert('请选择试题类别!'); return; }
    var $curli = $(".Template_files li a[data-id='" + $("#NodeID_Hid").val() + "']").parent();
    var nodeid = 0;
    if ($curli.data("pid") == 0) {//判断是否为根元素
        nodeid = $curli.children().data("id");
    } else {
        nodeid = $curli.prevAll('li[data-pid=0]').children().data("id");
    }
    ShowKnows(nodeid);
}
function ShowKnows(nodeid) {
    comdiag.maxbtn = false;
    ShowComDiag("/User/Exam/SelKnowledge.aspx?nid=" + nodeid + "&isread=1", "选择知识点");
}
function GetKnows(knowarr) {
    //{id:"",name:""}
    var keyArr = [];
    var vals = "";
    $(".tabinput").each(function () {
        var val = $(this).val();
        if (!ZL_Regex.isEmpty(val)) { keyArr.push(val); }
    });

    for (var i = 0; i < knowarr.length; i++) {
        var val = knowarr[i].name;
        if (!ZL_Regex.isEmpty(val)) { keyArr.push(val); }
    }
    keyArr.unique();
    for (var i = 0; i < keyArr.length; i++) {
        vals += keyArr[i] + ",";
    }
    vals = vals.substring(0, vals.length - 1);
    InitKeyWord(vals);
    CloseComDiag();
}