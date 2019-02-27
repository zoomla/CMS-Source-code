<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUserAddress.aspx.cs" MasterPageFile="~/User/Default.master" Inherits="User_Info_AddUserAddress" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>地址管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="shop" data-ban="shop"></div>
    <div class="container margin_t5">
    <ol class="breadcrumb">
    <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
    <li><a href="UserRecei.aspx">我的地址</a></li>
    <li class="active">地址管理</li>
    </ol>
    </div>
    <div class="container btn_green btn_green_xs">
        <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
        <table class="table table-bordered">
            <tr>
                <td style="width: 120px;" class="text-right">所在地区：</td>
                <td>
                    <select id="province_dp" name="province_dp" class="form-control td_m"></select>
                    <select id="city_dp" class="form-control td_m"></select>
                    <select id="county_dp" class="form-control" style="width: 92px;"></select>
                    <asp:HiddenField ID="pro_hid" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="text-right">详细地址：</td>
                <td>
                    <asp:TextBox runat="server" class="form-control text_300 required" ID="Street_T" autofocus="true" TextMode="MultiLine" /></td>
            </tr>
            <tr>
                <td class="text-right">邮政编码：</td>
                <td>
                    <asp:TextBox runat="server" class="form-control text_300 num zipcode" ID="ZipCode_T" MaxLength="6" /></td>
            </tr>
            <tr>
                <td class="text-right">收货人姓名：</td>
                <td>
                    <asp:TextBox runat="server" class="form-control text_300 required" ID="ReceName_T" /></td>
            </tr>
            <tr>
                <td class="text-right">手机号码：</td>
                <td>
                    <asp:TextBox runat="server" class="form-control text_300 num phones" ID="MobileNum_T" MaxLength="13" /></td>
            </tr>
            <tr>
                <td class="text-right">电话号码：</td>
                <td>
                    <asp:TextBox runat="server" Style="width: 80px;" class="form-control pull-left num" ID="Area_T" placeholder="区号" MaxLength="4" />
                    <asp:TextBox runat="server" Style="width: 210px; margin-left: 10px;" class="form-control pull-left num" ID="Phone_T" placeholder="号码" MaxLength="8" /></td>
            </tr>
            <tr>
                <td class="text-right">默认地址：</td>
                <td><input type="checkbox" class="switchChk" runat="server" id="Def_chk" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button runat="server" ID="SaveBtn" Text="保存" OnClientClick="return CheckForm()" OnClick="SaveBtn_Click" CssClass="btn btn-primary" />
                    <a href="UserRecei.aspx" class="btn btn-primary">返回</a>
                </td>
            </tr>
        </table>
    </div>
    
</asp:Content>
<asp:Content runat="server" ID="script_content" ContentPlaceHolderID="ScriptContent">
<link href="/App_Themes/V3.css" rel="stylesheet" />
<link href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
<style type="text/css">.control-sm {width: 120px;display: inline-block;}</style>
<script src="/dist/js/bootstrap-switch.js"></script>
<script src="/JS/ICMS/area.js"></script>
<script src="/JS/Controls/ZL_PCC.js"></script>
<script src="/JS/jquery.validate.min.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script>
    $(function () {
        ZL_Regex.B_Num(".num");
        var pcc = new ZL_PCC("province_dp", "city_dp", "county_dp");
        if ($("#pro_hid").val() == "") {
            pcc.ProvinceInit();
        }
        else {
            var attr = $("#pro_hid").val().split(' ');
            pcc.SetDef(attr[0], attr[1], attr[2]);
            pcc.ProvinceInit();
        }
        jQuery.validator.addMethod("phones", function (value) {
            return ZL_Regex.isMobilePhone(value);
        }, "请输入正确的手机号码!");
        jQuery.validator.addMethod("zipcode", function (value) {
            return ZL_Regex.isZipCode(value);
        }, "邮政编码格式不正确!");
        $("form").validate({});
    });
    function CheckForm() {
        $("#pro_hid").val($("#province_dp option:selected").text() + " " + $("#city_dp option:selected").text() + " " + $("#county_dp option:selected").text());
        var vaild = $("form").validate({ meta: "validate" });
        return vaild.form();

        //var flag = false;
        //if ($("#Street_T").val().length < 5) {
        //    $("#Street_T").focus();
        //    alert("详细地址字数必须大于5个字！");
        //}
        //else if (!ZL_Regex.isZipCode($("#ZipCode_T").val())) {
        //    alert("邮政编码格式不正确！");
        //    $("#ZipCode_T").focus();
        //}
        //else if (ZL_Regex.isEmpty($("#ReceName_T").val())) {
        //    alert("收货人姓名不能为空！");
        //    $("#ReceName_T").focus();
        //}
        //else if (!ZL_Regex.isMobilePhone($("#MobileNum_T").val()) && ZL_Regex.isEmpty($("#Phone_T").val())) {
        //    alert("请输入正确的手机号码或电话！");
        //    $("#MobileNum_T").focus();
        //}
        //else { flag = true; }
        //return flag;
    }
</script>
</asp:Content>
