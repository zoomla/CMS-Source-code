$(function () {
    $(".selnum").change(function () {
        if ($(this).val() != '1')
            $(this).next().removeClass("IDCards");
        else
            $(this).next().addClass("IDCards");
    });

    $.validator.addMethod("IDCards", function (value) {
        return ZL_Regex.isIDCard(value);
    }, "请输入正确的证件号码！");
    $.validator.addMethod("phones", function (value) {
        return ZL_Regex.isMobilePhone(value);
    }, "请输入正确的手机号码！");
    FillGuest();
    FillContract();
    $("form").validate({});
});
var addTlp = "<li><table class='table table-bordered'>"
        + "<tr><td rowspan='2' class='r_green_mid min'>" + guestName + "<span class='num'>@num</span></td><td><input type='text' class='form-control text_300 required' name='name_t_@num' placeholder='姓名' /><span class='btn btn-default fa fa-minus margin_l5' onclick='RemoveUser(this);'></span></td></tr>"
        + "<tr><td><select name='certtype_sel_@num' class='form-control min selnum'>"
        + "<option value='1' selected='selected'>身份证</option><option value='2'>护照</option><option value='3'>学生证</option><option value='4'>其它证件</option></select>"
        + "<input type='text' class='form-control text_300 margin_l5 required digits IDCards' name='certcode_t_@num' placeholder='证件号' /></td></tr></table></li>";
//添加一个新旅客
function AddGuest() {
    var num = parseInt($("#user_ul li:last .num").text()) + 1;
    var obj = $(addTlp.replace(new RegExp(/@num/g), num));
    $(obj).find(".selnum").change(function () {
        if ($(this).val() != '1')
            $(this).next().removeClass("IDCards");
        else
            $(this).next().addClass("IDCards");
    });
    $("#user_ul").append(obj);
    return false;
}
function RemoveUser(obj) {
    if (confirm("确定要移除吗")) {
        $(obj).closest("li").remove();
    }
}
function AddGuests(num) {
    for (var i = 1; i < num; i++) {
        AddGuest();
    }
}
//添加crm客户
function AddCustomer(datas) {
    $("#user_ul li").each(function (i,v) {
        if (i > 0) 
            $(v).remove();
    });
    $("#Guest_Hid2").val(JSON.stringify(datas));
    AddGuests(datas.length);
    FillGuest();
}
function CheckSubmit() {
    var guestArr = [];//旅客
    var contractArr = [];//联系人
    $liarr = $("#user_ul li");
    for (var i = 0; i < $liarr.length; i++) {
        $li = $($liarr[i]);
        var model = { Name: $li.find("input[name=name_t_" + (i + 1) + "]").val(), CertType: $li.find("[name=certtype_sel_" + (i + 1) + "]").val(), CertCode: $li.find("input[name=certcode_t_" + (i + 1) + "]").val() };
        guestArr.push(model);
    }
    var model = { Name: $("input[name=c_name_t]").val(), Mobile: $("input[name=c_mobile_t]").val(), Address: $("input[name=c_address_t]").val() };
    contractArr.push(model);
    $("#Guest_Hid").val(JSON.stringify(guestArr));
    $("#Contract_Hid").val(JSON.stringify(contractArr));
    //-------------
    if (!IsDump(guestArr)) { alert("证件号码不能为空或重复!"); return false; }
    var vaild = $("form").validate({ meta: "validate" });
    return vaild.form();
}
function IsDump(gusetArr) {
    var n = {}, r = []; //n为hash表，r为临时数组
    for (var i = 0; i < gusetArr.length; i++) //遍历当前数组      
    {
        if (n[gusetArr[i].CertCode]) { return false; } //如果hash表中没有当前项            {
        n[gusetArr[i].CertCode] = true; //存入hash表
        r.push(gusetArr[i].CertCode); //把当前数组的当前项push到临时数组里面            }
    }
    return true;
}
function skey() {
    var key = $("#skey_t").val();
    window.open("/Search/SearchList?node=0&keyword=" + key);
}
function FillGuest() {
    var json = $("#Guest_Hid2").val();
    if (json) {
        var list = JSON.parse(json);
        for (var i = 0; i < list.length; i++) {
            var model = list[i];
            var index = i + 1;
            $("[name=name_t_" + index + "]").val(model.Name);
            $("[name=certcode_t_" + index + "]").val(model.CertCode);
            $("[name=certtype_sel_" + index + "]").val(model.CertType);
        }
    }
}
function FillContract() {
    var json = $("#Contract_Hid2").val();
    if (json) {
        var list = JSON.parse(json);
        for (var i = 0; i < list.length; i++) {
            var model = list[i];
            $("[name=c_name_t]").val(model.Name);
            $("[name=c_mobile_t]").val(model.Mobile);
            $("[name=c_address_t]").val(model.Address);
        }
    }
}
var diag = new ZL_Dialog();
function AjaxLogin() {
    diag.title = "用户登录";
    diag.url = "/com/login_Ajax";
    diag.maxbtn = false;
    diag.width = "width350";
    diag.closebtn = false;
    diag.backdrop = true;
    diag.ShowModal();
}
function LoginSuccess() {
    diag.CloseModal();
}