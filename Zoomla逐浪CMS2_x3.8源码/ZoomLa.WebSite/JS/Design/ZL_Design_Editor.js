//改为引用ZL_Tlp.js
//var diag = new ZL_Dialog();
//diag.title = "区块编辑";
//diag.maxbtn = false;
//diag.backdrop = false;
//function InitContextMenu() {
//    $("div").contextMenu('rmenu',
//      {
//          bindings:
//          {
//              'edit': function (t) {
//                  StopDrag();
//                  $("div[contenteditable=true]").removeAttr("contenteditable");
//                  $(t).attr("contenteditable", true);
//                  $(t).focus();
//                  parent.EditA(t);
//              },
//              'drag': function (t) {
//                  InitDrag();
//              },
//              'enddrag': function (t) {
//                  StopDrag();
//              }
//          }
//      });
//}
////为保存做准备
//function BeginSave() {
//    //$("#editor_div").remove();
//    //$("#jqContextMenu").remove();
//    //$("div").each(function () { $(this).removeAttr("contenteditable"); });
//    return false;
//}
//$(function () {
//    //InitDrag();
//    //InitEdit();
//    //InitContextMenu();
//    //<div data-label='标签名称'></div>,后期扩展,应该写入Json,或直接分拆Json写入
//    $("[data-label]").mouseover(function () {//编辑内容
//        $(this).addClass("edit_selected");
//    }).mouseout(function () {
//        $(this).removeClass("edit_selected");
//    }).click(function () {
//        var label = $(this).data("label");
//        var ids = FindByName(labelJson, label).IDS;
//        diag.width = "width1100";
//        diag.url = "/JS/Design/RegionEdit.aspx?label=" + label + "&ids=" + ids;
//        diag.ShowModal();
//    });
//    $("a").each(function () { $(this).click(function () { window.event.returnValue = false; }); });//禁止所有的超链接点击
//})
////-------------Tools
//function FindByName(arr, label) {
//    for (var i = 0; i < arr.length; i++) {
//        if (arr[i].LabelName == label) { return arr[i]; }
//    }
//}