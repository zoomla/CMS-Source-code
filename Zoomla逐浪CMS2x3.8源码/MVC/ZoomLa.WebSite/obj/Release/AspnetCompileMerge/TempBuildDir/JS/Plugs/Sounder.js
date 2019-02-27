//用于无障碍浏览--阅读
var ZLSounder = {};
ZLSounder.Init = function (filter) {
    var ref = this;
    //if (!filter) { filter = "a,input[type=button],.need_sounder"; }
    console.log($(filter).length);
    $(filter).mouseover(function () {
        var text = ref.GetTextFromDom(this);
        if (text != "") {
            ref.PlayText(text)
        }
    });
}
ZLSounder.sounder = $('<video autoplay="autoplay" style="display:none;"></video>');
ZLSounder.GetTextFromDom = function (obj) {
    var text = "";
    var $obj = $(obj);
    if ($obj.data("sounder")) { text = $obj.data("sounder"); return text; }
    switch (obj.tagName) {//大写
        case "INPUT":
            text = $(obj).val();
            break;
        default:
            text = $(obj).text();
            break;
    }
    return text;
}
ZLSounder.PlayText = function (text) {
    var ref = this;
    $.post("/Common/API/Sounder.ashx", { tex: text }, function (data) {
        ref.sounder.attr("src", data);
        ref.sounder[0].play();
    })
}
$(function () {
    ZLSounder.Init("a,input[type=button],button,.sounder");
})
//1,根据样式筛选
//2,目标tag上可加 data-sounder="需要阅读的语句"
//3,建议放在底部,避免其他动态元素未加载完便执行