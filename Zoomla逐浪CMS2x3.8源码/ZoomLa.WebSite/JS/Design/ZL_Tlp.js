//右键菜单()
function InitContextMenu() {
    $("a,p,span,input,li,div,ul").contextMenu('rmenu',
      {
          onShowMenu: function (e, menu)
          {
              if (!$(e.toElement).hasClass("diy-block-hover"))
              {
                  $('#block', menu).remove();
              }
              return menu;
          },
          bindings:
          {
              'edit': function (t) {
                  //StopDrag();
                  var $div = $(t).closest("div");
                  $("div[contenteditable=true]").removeAttr("contenteditable");
                  $(".diy-outline-selector").removeClass("diy-outline-selector");
                  $div.addClass("diy-outline-selector");
                  $div.attr("contenteditable", true);
                  //$div.focus();
                  parent.EditA(t);
              },
              "block": function (t) {
                  var $div = $(t);//如果点击了元素素本身
                  $div.closest(".diy-block-hover");
                  $div.trigger("regionedit");
              }
              //,'drag': function (t) {
              //    InitDrag();
              //},
              //'enddrag': function (t)
              //{
              //    StopDrag();
              //}
          }
      });
}
//为保存做准备
function BeginSave() {
    $("#editor_div").remove();
    $("#design_div").remove();
    $("#jqContextMenu").remove();
    $("div").each(function () { $(this).removeAttr("contenteditable"); });
    $("div[contenteditable=true]").removeAttr("contenteditable");
    $(".diy-outline-selector").removeClass("diy-outline-selector");
    $(".diy-block-hover").removeClass("diy-block-hover");
    return true;
}
$(function () {
    //InitDrag();
    //InitEdit();
    //--------------改为通过Json搜出拥有指定元素的div或li加上事件
    //console.log(labelJson);
    for (var i = 0; i < labelJson.length; i++) {
       
        var model = labelJson[i];
        if (!model) continue;
        var $target = $("li:contains('" + model.ILabel + "')");
        if ($target.length < 1) { $target = $("ul:contains('" + model.ILabel + "')"); }
        if ($target.length < 1) { $target = $("div:contains('" + model.ILabel + "')"); }
        //model.target = $target;//关联关系直接json,不赋予标签上
        $target.mouseover(function () {//区块编辑
            $(this).addClass("diy-block-hover");
        }).mouseout(function () {
            $(this).removeClass("diy-block-hover");
        });
        //需要扩展,多次绑定的处理
        var f = function (mod) {
            console.log(mod.ILabel);
            $target.bind("regionedit", function () {
                parent.ShowDiag("/JS/Design/RegionEdit.aspx?label=" + mod.LabelName + "&ids=" + mod.IDS, "区块编辑");
            });
        }(model);
    }
    //--------------
    InitContextMenu();
    $("a").each(function () { $(this).click(function () { window.event.returnValue = false; }); });//禁止所有的超链接点击
})
////---------Tools
////根据标签名找到对应的Gids
//function FindByName(arr, label) {
//    for (var i = 0; i < arr.length; i++) {
//        if (arr[i].LabelName == label) { return arr[i]; }
//    }
//}

var ZL_Tlp = function () { }
ZL_Tlp.prototype.InitDrag = function () {
    //$("div").draggable({
    //    addClasses: false,
    //    axis: false,
    //    cursor: 'crosshair',
    //    disabled: false
    //});
}
ZL_Tlp.prototype.StopDrag = function () {
    //$("div").draggable({ disabled: true });
}
ZL_Tlp.prototype.InitContextMenu = function ()
{

}
