//专用于已有数据的分页
function ZLPage(list, row, opts) {
    this.list = list;
    this.conf = { cpage: 1, psize: 4, pnum: 9, itemCount: list.length };
    this.conf.row = row;
    this.conf.body = opts.body;// "#page_body";
    this.conf.footer = opts.footer;// "#page_footer";
    this.conf.rowEvent = opts.rowEvent;//行事件绑定
    this.conf.pageCount = this.getPageCount(list.length, this.conf.psize);
    this.showPage(this.conf.cpage);
}
ZLPage.prototype.getPageCount = function (itemCount, psize) {
    var count = itemCount / psize + ((itemCount % psize > 0) ? 1 : 0);
    if (!count || count < 1) { count = 1; }
    return parseInt(count);//避免出现小数
};
ZLPage.prototype.getcpage = function (cpage) {
    cpage = parseInt(cpage);
    if (!cpage || cpage < 1) { cpage = 1; }
    if (cpage > this.conf.pageCount) { cpage = this.conf.pageCount; }
    return parseInt(cpage);
}
ZLPage.prototype.notifyListChange = function () {
    var conf = this.conf;
    conf.itemCount = this.list.length;
    conf.pageCount = this.getPageCount(conf.itemCount, conf.psize);
}
//填充数据进入body
ZLPage.prototype.showPage = function (cpage) {
    var ref = this;
    ref.conf.cpage = ref.getcpage(cpage);
    var data = this.selpage();
    var $body = $(this.conf.body);
    var $items = JsonHelper.FillItem(this.conf.row, data, this.conf.rowClick);
    $body.html("").append($items);
    ref.createFooter();
}
//对数据进行分页处理,返回要显示的数据列
ZLPage.prototype.selpage = function () {
    var conf = this.conf;
    var start = (conf.cpage - 1) * conf.psize;
    var end = conf.psize * conf.cpage;
    if (end > this.list.length) { end = this.list.length; }
    var data = [];
    for (var i = start; i < end ; i++) {
        data.push(this.list[i]);
    }
    return data;
}
//根据数据与配置,生成底部html
ZLPage.prototype.createFooter = function () {
    var ref = this;
    var conf = this.conf;
    //计算起始页,等于则减一位
    var start = parseInt(conf.cpage / conf.pnum) * conf.pnum; 
    if (conf.cpage % conf.pnum == 0) { start = start - conf.pnum; }
    //---------------Begin
    var $ul = $('<ul class="pagination">');
    var $first = $('<li><a href="javascript:;" title="首页">«</a></li>');
    if (conf.cpage == 1) { $first.addClass("disabled"); }
    else { $first.click(function () { ref.showPage(1); }); }
    $ul.append($first);
    if (conf.cpage > conf.pnum) {
        var $pre = $('<li><a href="javascript:;">...</a></li>');
        $pre.click(function () { ref.showPage(start - conf.pnum + 1); });
        $ul.append($pre);
    }
    //底部分页列表,不能超出总大小,不能超过pnum
    for (var i = 0; i < (conf.pageCount - start) && i < conf.pnum; i++) {
        var liPage = (start + i) + 1;
        var getli = function (index) {
            var $li = $('<li><a href="javascript:;">' + index + '</a></li>');
            $li.click(function () { ref.showPage(index); });
            if (index == conf.cpage) { $li.addClass("active"); }
            $ul.append($li);
        }(liPage);
    }
    //---------------End
    if (conf.pageCount > (start + conf.pnum)) {
        $next = $('<li><a href="javascript:;">...</a></li>');
        $next.click(function () { ref.showPage(start + conf.pnum + 1); });
        $ul.append($next);
    }
    var $last = $('<li><a href="javascript:;" title="尾页">»</a></li>');
    if (conf.cpage == conf.pageCount) { $last.addClass("disabled"); }
    else { $last.click(function () { ref.showPage(conf.pageCount); }); }
    $ul.append($last);
    $(conf.footer).html("").append($ul);
}
//-----------------------------------------------
//var list = [];
//for (var i = 0; i < 300; i++) {
//    list.push({ name: "admin" + i, "pwd": "zzeee" + i });
//}
//var page = new ZLPage(list, '<div><span>@name</span><span style="color:red;">@pwd</span></div>', {
//    body: "#page_body",
//    footer: "#page_footer",
//    //psize: 10, cpage: 1,pnum:9,
//    rowEvent: null
//});