<%@ Page Language="C#" AutoEventWireup="true" CodeFile="address.aspx.cs" Inherits="test_Cart_address" MasterPageFile="~/Common/Master/Empty.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>地址管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
    <table class="table table-bordered">
        <tr>
            <td class="text-right td_m">地区：</td>
            <td>
                <select id="province_dp" name="province_dp" class="form-control td_m" style="display:inline-block;"></select>
                <select id="city_dp" class="form-control td_m" style="display:inline-block;"></select>
                <select id="county_dp" class="form-control" style="width: 92px;display:inline-block;"></select>
                <asp:HiddenField ID="pro_hid" runat="server" />
            </td>
        </tr>
        <tr>
            <td class="text-right">地址：</td>
            <td>
                <asp:TextBox runat="server" class="form-control" ID="Street_T" TextMode="MultiLine" /></td>
        </tr>
        <tr>
            <td class="text-right">邮编：</td>
            <td>
                <asp:TextBox runat="server" class="form-control num" ID="ZipCode_T" MaxLength="6" /></td>
        </tr>
        <tr>
            <td class="text-right">收货人：</td>
            <td>
                <asp:TextBox runat="server" class="form-control" ID="ReceName_T" /></td>
        </tr>
        <tr>
            <td class="text-right">手机：</td>
            <td>
                <asp:TextBox runat="server" class="form-control num" ID="MobileNum_T" MaxLength="13" /></td>
        </tr>
        <tr class="hidden-xs">
            <td class="text-right">电话：</td>
            <td>
                <asp:TextBox runat="server" Style="width: 80px;" class="form-control pull-left num" ID="Area_T" placeholder="区号" MaxLength="4" />
                <asp:TextBox runat="server" Style="width: 210px; margin-left: 10px;" class="form-control pull-left num" ID="Phone_T" placeholder="号码" MaxLength="8" /></td>
        </tr>
        <tr id="#def_tr">
            <td class="text-right">默认：</td>
            <td><input type="checkbox" class="switchChk" runat="server" id="Def_chk" /></td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button runat="server" ID="SaveBtn" Text="保存" OnClientClick="return CheckForm()" OnClick="SaveBtn_Click" CssClass="btn btn-primary" /></td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link href="/App_Themes/V3.css" rel="stylesheet" />
<link href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
<script src="/dist/js/bootstrap-switch.js"></script>
<script src="/JS/ICMS/area.js"></script>
<script src="/JS/Controls/ZL_PCC.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script type="text/javascript">
    $().ready(function () {
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
    });
    function CheckForm() {
        var flag = false;
        if ($("#Street_T").val().length < 5) {
            $("#Street_T").focus();
            alert("详细地址字数必须大于5个字！");
        }
        else if (!ZL_Regex.isZipCode($("#ZipCode_T").val())) {
            alert("邮政编码格式不正确！");
            $("#ZipCode_T").focus();
        }
        else if (ZL_Regex.isEmpty($("#ReceName_T").val())) {
            alert("收货人姓名不能为空！");
            $("#ReceName_T").focus();
        }
        else if (!ZL_Regex.isMobilePhone($("#MobileNum_T").val()) && ZL_Regex.isEmpty($("#Phone_T").val())) {
            alert("请输入正确的手机号码或电话！");
            $("#MobileNum_T").focus();
        }
        else { flag = true; }
        $("#pro_hid").val($("#province_dp option:selected").text() + " " + $("#city_dp option:selected").text() + " " + $("#county_dp option:selected").text());
        return flag;
    }
    function Refresh() {
        if (parent && parent.Refresh) parent.Refresh();
        parent.location = parent.window.location;
    }
    function hidedef() {
        $("#def_tr").hide();
    }
</script>
</asp:Content>