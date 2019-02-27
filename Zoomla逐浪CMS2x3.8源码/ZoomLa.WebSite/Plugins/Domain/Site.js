//检测值是否符合规范
function checkValue() {
    //remind = "确定要注册该系列域名吗!\r\n";
    //dom = $("#").val();
    //arr = $("input:[name='domNameChk']:checked");

    //tna = $("#tempName").val();
    flag = false;
    un1 = $("#uname1").val();
    un2 = $("#uname2").val();
    rn1 = $("#rname1").val();
    rn2 = $("#rname2").val();
    aem = $("#aemail").val();
    ad1 = $("#uaddr1").val();
    ad2 = $("#uaddr2").val();
    utl = $("#uteln").val();
    zip = $("#uzip").val();
    cit = $("#cityText").val();
    cit2 = $("#ucity2").val();
    //if (isEmpty(dom)||arr.length<1)
    //{
    //    alert("未选定域名");
    //    return false;
    //}
    //if (isEmpty(tna)) {
    //    alert("模板名不能为空");
    //    return false;
    //}
    //else
    if (isEmpty(un1) || !isContainChina(un1))//为空或不包含中文
    {
        alert("单位名称(中文)填写不符合规范");
    }
    else if (isEmpty(un2) || isContainChina(un2))//包含中文
    {
        alert("单位名称(英文)不能含有中文或为空");
    }
    else if (isEmpty(rn1) || !isContainChina(rn1))//不包含中文
    {
        alert("联系人(中文)填写不符合规范");
    }
    else if (isEmpty(rn2) || !domIsEng(rn2))//为空或不符合规范
    {
        alert("联系人(英文)不能含有中文或，必须为英文，数字组合，名和姓必须用空格隔开");
    }
    else if (isEmpty(aem) || !isEmail(aem)) {
        alert("邮箱地址填写不规范");
    }
    else if (!isContainChina(cit)) {
        alert("区域地址不规范");
    }
    else if (!domIsEng(cit2))
    {
        alert("省份城市(英文)不规范,必须为英文，数字组合，名和姓必须用空格隔开");
    }
    else if (isEmpty(ad1) || !isContainChina(ad1)) {
        alert("通迅地址(中文)不规范");
    }
    else if (isEmpty(ad2) || isContainChina(ad2)) {
        alert("通迅地址(英文)不规范");
    }
    else if (isEmpty(utl) || !isMobilePhone(utl)) {
        alert("手机号码填写不规范");
    }
    else if (!isZipCode(zip)) {
        alert("邮编地址不规范");
    }
    else { flag = true; }
    return flag;

    //for (var i = 0; i < arr.length; i++) {
    //    remind += dom + $(arr[i]).val() + "\r\n";
    //}
    //if (!confirm(remind))
    //    return false;
}
function isEmpty(e) { return e = $.trim(e), e.length > 0 ? !1 : !0 }
function isContainChina(e) { var t = /[\u4E00-\u9FA5]|[\uFE30-\uFFA0]/gi; return t.exec(e) ? !0 : !1 }
function isEmail(e) { var t = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/gi; return t.exec(e) ? !0 : !1 }
function isMobilePhone(e) { var t = /^1[(0-9)]{10}$/gi; return t.exec(e) ? !0 : !1 }
function isZipCode(e) { return e = $.trim(e), isInt(e) && e.length == 6 ? !0 : !1 }
function isInt(e) { var t = /^\d+(\d+)?$/gi; return t.exec(e) ? !0 : !1 }
function domIsEng(s) {
    var patrn = /[^a-zA-Z0-9\s]+/gi;
    if (!patrn.exec(s)) {
        i = s.toString().indexOf(" ");
        return i > 0;
    }
    else {
        return false;
    }
}

//设定值格式:key:value,key2,value:::下同
function setValue(v) {
    var arr = v.split(",");
    var temp;
    for (i = 0; i < arr.length; i++)
    {
        temp = arr[i].split(":");
        $("#" + temp[0]).val(temp[1]);
    }
}
//设置默认选中,需要选中的值，divID
function setDefaultCheck(v, id) {
    a = v.split(',');
    if (id != undefined) {
        for (var i = 0; i < a.length; i++) {
            $("#" + id + " [value='" + a[i] + "']").attr("checked", "checked");
        }
    }
    else//未设置ID
    {
        for (var i = 0; i < a.length; i++) {
            $("[value='" + a[i] + "']").attr("checked", "checked");
        }
    }
}
//------------模板
//获取需要提交的数据,table下的所有
function getInfo(tableID) {
    //tempInfoTable
    dataArr = $("#" + tableID + " tr>td input");
    jsonData = ""
    for (var i = 0; i < dataArr.length - 1; i++)//不加最后一行
    {
        $obj = $(dataArr[i]);
        jsonData += $obj.attr("name") + ":" + $obj.val() + ",";
    }
    jsonData += "uaddr1:" + $("#uaddr1").val() + ",";
    jsonData += "uaddr2:" + $("#uaddr2").val();
    return jsonData;
}
