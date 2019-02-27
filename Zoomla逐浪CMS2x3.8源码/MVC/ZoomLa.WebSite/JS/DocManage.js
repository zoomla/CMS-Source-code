/*
 *写作助手的JS方法,与WebOffice沟通，生成文档，并对页面的值做检测
 */

//值检测,使用参数ID，不为空
function docCheck() {
}
//参数隐藏字段的ID(DisUse)
function generate(docTitleID, docParamID) {
    docTitle = document.getElementById(docTitleID).value;
    docParam = document.getElementById(docParamID).value;
    if (docTitle == "" || docParam == "") { alert("请先保存，再生成文档"); return false; }
    if (confirm("你确定要生成文档吗!")) {
        wordManage.window.batGenerateDoc(docTitle, docParam);
        location.href = "/Edit/Edit.aspx";
        return false;
    }
}
//用户中心使用,不跳转
function generate2(docTitleID, docParamID) {
    docTitle = document.getElementById(docTitleID).value;
    docParam = document.getElementById(docParamID).value;
    if (docTitle == "" || docParam == "") { alert("请先保存，再生成文档"); return false; }
    if (confirm("你确定要生成文档吗!")) {
        if (!wordManage) { alert("没有引用WebOffice"); }
             wordManage.window.batGenerateDoc2(docTitle, docParam);
            return true;
    }
    return false;
}

//生成的同时显示等待图,DivID为等待图所在的Div
function generate3(docTitleID, docParamID, DivID) {
    docTitle = document.getElementById(docTitleID).value;
    docParam = document.getElementById(docParamID).value;
    if (docTitle == "" || docParam == "") { alert("请先保存，再生成文档"); return false; }
    setCenter(DivID, '')
    if (confirm("你确定要生成文档吗!")) {
        if (!wordManage) { alert("没有引用WebOffice"); }
        wordManage.window.batGenerateDoc2(docTitle, docParam);
        setCenter('preview', 'none');
        return true;
    }
    setCenter(DivID, 'none')//如果点否则不生成
    return false;
}

//单文档生成
function singleDocGen(docTitleID, docParamID, docName) {
    docParam = document.getElementById(docParamID).value.replace(/[\S\s]*?(?=;)/, docName);
    docTitle = document.getElementById(docTitleID).value;
    if (confirm("你确定要生成[" + docName.split('.')[0] + "]模板吗!")) {
        if (!wordManage) { alert("没有引用WebOffice"); }
        wordManage.window.batGenerateDoc2(docTitle, docParam);
        alert("生成完成");
    }
    location = location;
}
//-----------------JS使其在屏幕中央显示
function setCenter(DivID, status) {
    var waitDiv = document.getElementById(DivID);
    if (waitDiv) {
        waitDiv.style.top = (document.documentElement.scrollTop + (document.documentElement.clientHeight - waitDiv.offsetHeight) / 2) + "px";
        waitDiv.style.left = (document.documentElement.scrollLeft + (document.documentElement.clientWidth - waitDiv.offsetWidth) / 2) + "px";
        waitDiv.style.display = status;
        return true;
    }
}//setCenter End;

//全选，全清(用于User/FileFactory)
var flag = true;
function allCheck(name) {
    var checkBoxarr = document.getElementsByName(name);
    for (i = 0; i < checkBoxarr.length; i++) {
        checkBoxarr[i].checked = flag;
    }
    document.getElementById("taBtn").value = document.getElementById("taBtn").value == "全选" ? "全清" : "全选";
    flag = !flag;
}
