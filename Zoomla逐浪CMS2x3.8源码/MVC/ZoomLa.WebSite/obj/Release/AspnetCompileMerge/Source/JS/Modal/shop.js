//多价格处理逻辑,需引用Array,Regex
var MultiPrice = {
    trTlp: "",
    config: { hid: "ChildPro_Hid", body: "child_tb", num: 1 },
    GetModel: function () { return { code: "", Proname: "", LinPrice: "", ShiPrice: "" }; },
    Init: function () {
        var ref = this;
        ref.trTlp = $("#" + ref.config.body).html();
        var val = $("#" + ref.config.hid).val();
        if (val && val != "" && val.indexOf("|") >2) {
            var list = JSON.parse(val);
            ref.AddTrByData(list);
        }
        else { ref.AddTrByNum(ref.config.num); }
        $($("#" + ref.config.body + " .delchild")[0]).remove();
    },
    BindEvent: function () {
        var ref = this;
        $(".addchild").unbind("click");
        $(".delchild").unbind("click");
        $(".addchild").click(function () {
            ref.AddTrByNum(1, "append");
        });
        $(".delchild").click(function () {
            $(this).closest(".childtr").remove();
        });
    },
    PreSubmit: function () {
        //提交前的处理
        var ref = this;
        var $trs = $(".childtr");
        var list = [];
        for (var i = 0; i < $trs.length; i++) {
            var model = ref.GetModel();
            var $tr = $($trs[i]);
            model.code = $tr.find("[name=C_code_hid]").val();
            model.Proname = $tr.find("[name=C_Proname_T]").val();
            model.LinPrice = $tr.find("[name=C_LinPrice_T]").val();
            model.ShiPrice = $tr.find("[name=C_ShiPrice_T]").val();
            if (model.code == "") { model.code = GetRanPass(6); }
            if (ZL_Regex.isNum(model.LinPrice, model.ShiPrice) && !ZL_Regex.isEmpty(model.Proname)) {
                list.push(model);
            }
        }//for end;
        if (list.length > 0) { $("#" + ref.config.hid).val(JSON.stringify(list)); }
        return true;
    },
    //--------以上为必须有,后期增加继承属性
    AddTrByNum: function (num, method) {
        //创建指定个数的行
        var ref = this;
        if (!num || num < 1) { num = 1; }
        var list = [];
        for (var i = 0; i < num; i++) {
            list.push(ref.GetModel());
        }
        ref.AddTrByData(list, method);
    },
    AddTrByData: function (list, method) {
        //根据数据先清除再填充,这样模板可以直接写在Html中
        var ref = this;
        var html = JsonHelper.FillData(ref.trTlp, list);
        if (method == "append") {
            $("#" + ref.config.body).append(html)
        }
        else { $("#" + ref.config.body).html(html) }
        ref.BindEvent();
    }
};
//-------商品类型
var proclass = {};
proclass.$hid = $("#ProClass_Hid");
proclass.$rads = $("input[name=proclass_rad]");
proclass.switch = function (val) {
    var ref = this;
    if (!val || val == "") { val = "1"; }
    $(".proclass_tab").hide();
    $("#proclass_tab" + val).show();
    ref.$hid.val(val);
    //部分商品类别再做特殊处理
}
var idc = { $hid: $("#IDC_Hid") };
idc.tlp = '<tr><td><input type="text" class="form-control time" value="@time" /></td><td><input type="text" class="form-control price"  value="@price" /></td><td><a href="javascript:;" class="btn btn-info" onclick="idc.del(this);"><i class="fa fa-minus"></i></a></td></tr>';
idc.presave = function () {
    var list = [];
    $("#idc_list tr").each(function () {
        var model = { name: "", time: "", price: "" };
        var $this = $(this);
        model.time = $this.find(".time").val();
        model.price = $this.find(".price").val();
        model.name = model.time + " ¥" + parseFloat(model.price).toFixed(2);
        if (!model.time || model.time == "") { return; }
        list.push(model);
    });
    idc.$hid.val(JSON.stringify(list));
}
idc.init = function () {
    var val = idc.$hid.val();
    if (val == "" || val == "[]") { return; }
    var list = JSON.parse(val);
    //根据数据重新生成tr,便于扩展更多天数支持
    var $items = JsonHelper.FillItem(idc.tlp, list);
    $("#idc_list").html("").append($items);
}
idc.addrow = function () {
    var $item = JsonHelper.FillItem(idc.tlp, { name: "", time: "", price: "", remind: "" });
    $("#idc_list").append($item);
}
idc.del = function (btn) {
    $tr = $(btn).closest("tr");
    $tr.remove();
}
//-------
$(function () {
    MultiPrice.Init();
    ZL_Regex.B_Num(".num");
    $("#Unitd").find("button").click(function () { $("#ProUnit").val($(this).text()); });
    //----商品类型
    if (proclass.$hid.val() == "") { proclass.$hid.val("1"); }
    proclass.$rads.each(function () { if ($(this).val() == proclass.$hid.val()) { $(this).click(); } });
    idc.init();
    //----模板信息
    Tlp_initTemp();
    BindPro();//旅游订单不显示
    //售后设置属性
    var revalues = $("#restate_hid").val().split(',');
    if (revalues.length > 0) {
        $("input[name='GuessXML']").each(function () { this.checked = false; })
        for (var i = 0; i < revalues.length; i++) {
            if (!ZL_Regex.isEmpty(revalues[i])) {
                var chk = $("input[name=GuessXML][value=" + revalues[i] + "]")[0];
                if (chk) { chk.checked = true; }
            }
        }
    }
})
//提交前处理
function PreSubmit() {
    if (!MultiPrice.PreSubmit()) return false;
    if (proclass.$hid.val() == "6") { idc.presave(); }
    //会员价,购买数量限定
    setToHid(".Price_Group_T", "Price_Group_Hid");
    setToHid(".Quota_Group_T", "Quota_Group_Hid");
    setToHid(".DownQuota_Group_T", "DownQuota_Group_Hid");
    $("#EBtnSubmit").click();
}
//将RPT中的值存为json {gid:"",value:""}
function setToHid(filter, hidid) {
    var $texts = $(filter);
    var $hid = $("#" + hidid);
    var result = [];
    if ($texts.length < 1) { $hid.val(""); return; }
    for (var i = 0; i < $texts.length; i++) {
        var $t = $($texts[i]);
        result.push({ gid: $t.data("gid"), value: $t.val() });
    }
    $hid.val(JSON.stringify(result));
}