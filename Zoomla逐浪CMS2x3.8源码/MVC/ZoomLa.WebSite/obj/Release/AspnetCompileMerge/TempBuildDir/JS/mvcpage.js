
/*
<a href="javascript:;" class="option_style" onclick="mvcpage.del({ ids: '@dr["FavoriteID"]', url: 'favour_del', confirm: '确定要移除吗' });"><i class="fa fa-trash-o"></i></a>
<input type="button" value="批量删除" onclick="mvcpage.bat({url: 'favour_del', confirm: '确定要批量移除吗',after:'reload' });" class="btn btn-info" />
after:'方法名'
<input type="hidden" id="skey" value="@ViewBag.skey">

mvcpage.load();//搜索或其他条件选项变更,重新加载页面(表名等均为默认参数)
mvcpage.load(target:"EGV1",page:"first");//回复后至第一页
*/
var mvcpage = {};
mvcpage.load = function (opts) {
    //{target:"EGV",page:1,psize:10}
    opts = mvcpage.fillopts(opts);
    var $target = $("#" + opts.target);
    var $div = $(".mvcpage[data-for='" + opts.target + "']");
    var url = $div.data("url");
    var pcount = $div.data("pcount");
    var conf = { cpage: parseInt($div.data("cpage")), psize: $div.data("psize") };
    opts.mvcparam = $div.data("mvcparam");
    if (opts.psize) { conf.psize = opts.psize; }
    $(opts.mvcparam).each(function () {
        var $this = $(this);
        conf[$this.attr("id")] = $this.val();
    });
    switch (opts.page) {
        case "first":
            conf.cpage = 1;
            break;
        case "last":
            conf.cpage = pcount;
            break;
        case "prev":
            conf.cpage--;
            break;
        case "next":
            conf.cpage++;
            break;
        case "reload"://刷新当前页(添加|删除)
            break;
        default:
            conf.cpage = parseInt(opts.page);
            break;
    }
    $.post(url, conf, function (data) { $target.replaceWith(data); })
}
//批量删除
mvcpage.bat = function (opts) {
    //{chkname:'',taraget:'',||url:'',confirm:'确定要删除吗'}
    if (!opts.chkname) { opts.chkname = "idchk"; }
    var ids = mvcpage.help.getchk(opts.chkname);
    if (ids == "") { alert("请选择需要操作的数据"); return; }
    if (!opts.after) { opts.after = "reload"; }
    if (opts.confirm) { if (!confirm(opts.confirm)) { return; } }
    $.post(opts.url, { "ids": ids }, function (data) {
        switch (opts.after) {
            case "reload":
                break;
            default://未命中则执行js方法
                mvcpage.help.call(opts.after,data);
                break;
        }
        if (data == 1) { mvcpage.load({ target: opts.target, page: "reload" }); } else { alert(data); }
    })
}
mvcpage.del = function (opts) {
    //{id:'',url:'',confirm:'确定要删除吗'}
    opts = mvcpage.fillopts(opts);
    if (opts.confirm) { if (!confirm(opts.confirm)) { return; } }
    $.post(opts.url, { "ids": opts.ids, "id": opts.id }, function (data) {
        if (data == 1) { mvcpage.load({ target: opts.target, page: "reload" }); }
        else { alert(data); }
    })
}
//填充默认值(单)
mvcpage.fillopts = function (opts) {
    if (!opts) { opts = {}; }
    if (!opts.page) { opts.page = "1";}
    if (!opts.target) { opts.target = "EGV"; }
    if (!opts.chkname) { opts.chkname = "idchk"; }
    if (!opts.mvcparam) { opts.mvcparam = ".mvcparam"; }
    return opts;
}
//-----辅助方法不仅可用于mvcpage
mvcpage.help = {};
mvcpage.help.getchk = function (name) {
    var ids = "";
    var $chks = $("input[name='" + name + "']:checked");
    if ($chks.length < 1) { return ""; }
    $chks.each(function () { ids += this.value + ","; });
    if (ids.length > 0) { ids = ids.substr(0, ids.length - 1); }
    return ids;
}
//根据方法名称,执行方法
mvcpage.help.call = function (name, arg) {
    try {
        var func = eval(name);
        func(arg);
    } catch (ex) { console.log("call failed", name, ex); }
}