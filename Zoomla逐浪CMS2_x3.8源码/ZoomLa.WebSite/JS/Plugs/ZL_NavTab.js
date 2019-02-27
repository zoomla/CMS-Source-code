(function ($) {
    $.fn.ZL_NavTab = function (option) {
        var tlphtml = "<ul class='nav nav-tabs' id='@rid_navtabs_ul'>@li</ul>"
                    + "<a id='@rid_hid_right' class='hid_right'><i class='fa fa-angle-left'></i></a>"
                    + "<a id='@rid_hid_left' class='hid_left'><i class='fa fa-angle-right'></i></a>";;
        var defconfig = {
            hid: "",//存放数据的hidden
            curid: 0,//当前选中项id
            feildid: "id",//数据字段id的名称
            feildname: "name",//数据字段name的名称
            tabclick: null//点击选项后的事件
        };
        var opts = $.extend(defconfig, option);
        var _ref = this;
        var navid = _ref.attr("id");//容器id
        var datas = JSON.parse($("#" + opts.hid).val());
        var lihtml = "";
        //加载数据
        for (var i = 0; i < datas.length; i++) {
            var litlp = "<li class='@class' role='presentation'><a class='@rid_tab_a' data-id='@id' href='javascript:;'>@name</a></li>";
            lihtml += litlp.replace(/@class/g, datas[i][opts.feildid] == opts.curid ? "active" : "").replace(/@id/g, datas[i][opts.feildid]).replace(/@name/g, datas[i][opts.feildname] ? datas[i][opts.feildname] : datas[i][opts.feildid]);
        }
        _ref.append(tlphtml.replace(/@li/g, lihtml).replace(/@rid/g, navid));
        //加载事件
        var $ul = _ref.find("#" + navid + "_navtabs_ul");
        var marleft = 0;
        var movewidth = 0;
        _ref.find("#" + navid + "_hid_right").click(function () {
            var datacount = $ul.find("li").length;
            movewidth = _ref.innerWidth() - 60;
            if (marleft + movewidth < $ul.width() && datacount > 10) {
                marleft += movewidth;
                $ul.animate({ "marginLeft": -marleft + "px" }, 300);
            }
        });
        _ref.find("#" + navid + "_hid_left").click(function () {
            movewidth = _ref.innerWidth() - 60;
            if (marleft >= movewidth) {
                marleft -= movewidth;
                $ul.animate({ "marginLeft": -marleft + "px" }, 300);
            }
            else
                $ul.animate({ "marginLeft": "0px" }, 300);
        });
        _ref.find('.' + navid + "_tab_a").click(function () {
            if (opts.tabclick) {
                opts.tabclick($(this).data("id"), this);
            }
        });
    }
})(jQuery);