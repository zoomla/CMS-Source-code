
//统一小写判断
function ascxInit(name) {
    switch (name)
    {
        case "nodetree.ascx"://内容节点
        case "shopnodetree.ascx"://商城内容节点
        case "recyclenodetree.ascx"://商城内容节点
        case "shoprecycle.ascx"://商城内容节点
        case "specialtree.ascx"://商城内容节点
        case "usershopnodetree.ascx"://店铺内容节点
            NodeTree.init();
            break;
        case "structtree.ascx"://组织结构
            NodeTree.init();
            NodeTree.showInfo();
            break;
        default:
            NodeTree.init();
            BindLayerUL();
            break;
    }
}

var NodeTree = {
    init: function () {
        $(".tvNav a.list1").click(function () { NodeTree.showList(this); });
        $(".tvNav  a.list1 .NodeP_Span").click(function () {
            window.event.cancelBubble = true;//停止冒泡
            window.event.returnValue = false;//阻止事件的默认行为
            //windows.event.stopPropagation();//Edge以下不支持
            NodeTree.showList($(this).parent());
            return false;//Edge处理
        });
        $("#left_Div").attr("");
    },
    showList: function (obj) {
        //$(obj).parent().parent().find("a").removeClass("SelectedA");//a-->li-->ul
        //$(obj).parent().children("a").addClass("SelectedA");//li
        $(obj).parent().siblings("li").find("ul").slideUp();
        $(obj).parent().children("ul").slideToggle();
    },
    showInfo: function (obj)//组织桔构
    {
        $(".NodeInfo").click(function () {
            window.event.cancelBubble = true;
            window.event.returnValue = false;
            ShowMain("", "{$path}I/AddOn/StructAnalysis.aspx?sid="+$(obj).attr("sid"));
        });
    },
    activeSelf: function (obj)//节点,自身选中
    {
        $("#nodeNav .SelectedA").each(function () { $(this).removeClass("SelectedA"); });
        $(obj).addClass("SelectedA");
    }
}
/*
 *ShowMain()为default.aspx中方法
 */
//-------ASCX中公用方法
var ASCX = {
    Search: function (url,id)
    {
        var key = "";
        if (typeof (id) == "string") {
            key = $("#" + id).val();
        }
        else {
        key = $(id).val();}
        ShowMain('', url + key);
    },
    OnEnterSearch: function (url, obj) { if (event.keyCode == 13) { ASCX.Search(url, obj); return false;  } }
}