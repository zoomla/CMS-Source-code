<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddDeliverType.aspx.cs" Inherits="User_Shop312_AddDeliverType" MasterPageFile="~/User/Default.master"%>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>运费管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="shop" data-ban="store"></div> 
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li><a href="DeliverType.aspx">运费模板</a></li>
            <li class="active">运费管理</li>
            <div class="clearfix"></div>
        </ol>
    </div>
    <div class="container">
    <div class="btn_green"><uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" /></div>
    <table class="table table-bordered table-striped">
        <tr><td class="td_l">模板名称:</td><td>
            <asp:TextBox runat="server" ID="TlpName_T" CssClass="form-control text_300" MaxLength="100" /></td></tr>
        <tr><td>计价方式:</td><td>
           <label><input type="radio" value="1" name="pricemod_rad" checked="checked" />按件数</label></td>
        </tr>
        <tr><td>设置运费:</td><td>
            <div>
                <label>
                    <input runat="server" type="checkbox" id="exp_chk" />快递</label>
                <span>运费：<input type="text" class="text_xs num" id="exp_price_t" />元,每超过一件需加运费<input type="text" class="text_xs num" id="exp_plus_t" />元</span>
                <select id="exp_free_sel" data-exdiv="exp_free_sp" onchange="Showexdiv(this);">
                    <option value="0">不包邮</option>
                    <option value="1">计件包邮</option>
                    <option value="2">金额包邮</option>
                    <option value="3">件数+金额</option>
                </select>
                <span class="exdiv" id="exp_free_sp1">满<input type="text" class="text_xs" id="exp_free_num"/>件</span>
                <span class="exdiv" id="exp_free_sp2">满<input type="text" class="text_xs" id="exp_free_money"/>金额</span>
            </div>
            <div>
                <label><input runat="server" type="checkbox" id="ems_chk" />EMS</label>
                <span>运费：<input type="text" class="text_xs num" id="ems_price_t" />元,每超过一件需加运费<input type="text" class="text_xs num" id="ems_plus_t" />元</span>
                <select id="ems_free_sel" data-exdiv="ems_free_sp" onchange="Showexdiv(this);">
                    <option value="0">不包邮</option>
                    <option value="1">计件包邮</option>
                    <option value="2">金额包邮</option>
                    <option value="3">件数+金额</option>
                </select>
                <span class="exdiv" id="ems_free_sp1">满<input type="text" class="text_xs" id="ems_free_num" />件</span>
                <span class="exdiv" id="ems_free_sp2">满<input type="text" class="text_xs" id="ems_free_money" />金额</span>
            </div>
            <div>
                <label><input runat="server" type="checkbox" id="mail_chk" />平邮</label>
                <span>运费：<input type="text" class="text_xs num" id="mail_price_t" />元,每超过一件需加运费<input type="text" class="text_xs num" id="mail_plus_t" />元</span>
                 <select id="mail_free_sel" data-exdiv="mail_free_sp" onchange="Showexdiv(this);">
                    <option value="0">不包邮</option>
                    <option value="1">计件包邮</option>
                    <option value="2">金额包邮</option>
                    <option value="3">件数+金额</option>
                </select>
                <span class="exdiv" id="mail_free_sp1">满<input type="text" class="text_xs" id="mail_free_num" />件</span>
                <span class="exdiv" id="mail_free_sp2">满<input type="text" class="text_xs" id="mail_free_money" />金额</span>
            </div></td></tr>
        <tr><td>备注(买家可见):</td><td><asp:TextBox runat="server" ID="Remind_T" TextMode="MultiLine" CssClass="form-control text_300" /></td></tr>
        <tr><td>备注(仅卖家可见):</td><td><asp:TextBox runat="server" ID="Remind2_T" TextMode="MultiLine" CssClass="form-control text_300"/></td></tr>
        <tr><td></td><td><asp:Button runat="server" ID="Save_Btn"  Text="保存" OnClientClick="return BeginSave();" OnClick="Save_Btn_Click" CssClass="btn btn-primary"/></td></tr>
    </table>
    <asp:HiddenField runat="server" ID="Fare_Hid" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <style type="text/css">
     .exdiv{display:none;}
    </style>
    <script type="text/javascript">
        var fareArr = [];
        var fareStr = "exp,ems,mail".split(',');
        function BeginSave() {
            fareArr = [];
            var priceMode = $("[name=pricemod_rad]").val();
            for (var i = 0; i < fareStr.length; i++) {
                //快递名,计价方式,初始价,续价,包邮方式,多少件包邮,多少钱包邮
                var fareMod = { name: "", mode: "", enabled: "", price: 0, plus: 0, free_sel: "", free_num: "", free_money: "" };
                fareMod.name = fareStr[i];
                fareMod.mode = priceMode;
                fareMod.enabled = $("#" + fareMod.name + "_chk")[0].checked;
                fareMod.price = $("#" + fareMod.name + "_price_t").val();
                fareMod.plus = $("#" + fareMod.name + "_plus_t").val();
                fareMod.free_sel = $("#" + fareMod.name + "_free_sel").val();
                fareMod.free_num = $("#" + fareMod.name + "_free_num").val();
                fareMod.free_money = $("#" + fareMod.name + "_free_money").val();
                fareArr.push(fareMod);
            }
            $("#Fare_Hid").val(JSON.stringify(fareArr));
            return true;
        }
        function ShowPrice() {
            fareArr = JSON.parse($("#Fare_Hid").val());
            for (var i = 0; i < fareArr.length; i++) {
                var model = fareArr[i];
                $("#" + model.name + "_chk")[0].checked = model.enabled;
                $("#" + model.name + "_price_t").val(model.price);
                $("#" + model.name + "_plus_t").val(model.plus);
                $("#" + model.name + "_free_sel").val(model.free_sel);
                $("#" + model.name + "_free_num").val(model.free_num);
                $("#" + model.name + "_free_money").val(model.free_money);
                Showexdiv($("#" + model.name + "_free_sel"));
            }
        }
        //-----Tools
        function Showexdiv(obj) {//显示包邮条件
            var $obj = $(obj);
            switch ($obj.val()) {
                case "3":
                    $obj.closest("div").find(".exdiv").show();
                    break;
                default:
                    $obj.closest("div").find(".exdiv").hide();
                    $("#" + $obj.data("exdiv") + $obj.val()).show();
                    break;
            }
        }
    </script>
</asp:Content>