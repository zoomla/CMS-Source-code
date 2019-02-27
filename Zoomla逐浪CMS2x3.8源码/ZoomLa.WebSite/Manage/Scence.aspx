<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Scence.aspx.cs" Inherits="Manage_I_Scence" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>场景切换</title>
<style type="text/css">
.Scences{width:100%;border:none;font-size: 12px}
.Scences .title{height: 24px; background-color: #E8F4FF; font-weight: bold;}
.checkDiv { width: 55px; height: 60px; background: red; background-color: #27a9e3; border-radius: 5px; text-align: center; line-height: 25px; font-size: 1em; float: left; margin: 0 5px 5px 0; cursor: pointer;}
.checkDiv:hover { background-color: #28b779;}
.checkDiv input { margin-top:0; width: 30px; height: 30px;}
.headtip{padding:5px;margin-bottom:5px;}
</style>
<script src="/JS/ZL_Scence.js"></script>
<script type="text/javascript">
function loadScence(flag) {
    var Temp = "<tbody><tr><td class='title'><div class='alert alert-success headtip' role='alert'>@Title</div></td></tr><tr><td>@Child</td></tr></tbody>";
    var chTemp = "<div class='checkDiv'>@Name<input id='@ID' type='checkbox' checked='checked' /></div>";
    var html = "";
    for (var i = 0; i < MenuJson.length; i++) {
        var chstr = "";
        var obj = MenuJson[i];
        for (var j = 0; j < MenuJson[i].Child.length; j++) {
            var chobj = MenuJson[i].Child[j];
            if(flag=="1"||chobj.css.indexOf(flag)>-1)
            chstr += chTemp.replace(/@Name/, chobj.Name).replace(/@ID/, chobj.ID);
        }
        if (flag == "1" || obj.css.indexOf(flag) >-1)
        html += Temp.replace(/@Title/, obj.Name).replace(/@Child/, chstr);
    }
    $("#Scences").html(html);
    $(".checkDiv").click(function () {
        obj = $(this).find(":checkbox")[0];
        obj.checked = !obj.checked;
        if (!obj.checked) {//未选中则背景色改变
            $(this).css("background-color", "orange");
        }
        else
            $(this).css("background-color", "#27a9e3");
    });
    $(".checkDiv :checkbox").click(function () { $(this).parent().click(); });//取消默认单击事件
}
function GetConfig(flag) {
    for (var i = 0; i < MenuJson.length; i++) {
        MenuJson[i].Visible = flag == "1" || MenuJson[i].css.indexOf(flag) > -1;
        delete MenuJson[i].Name; delete MenuJson[i].css;
        for (var j = 0; j < MenuJson[i].Child.length; j++) {
            if($("#" + MenuJson[i].Child[j].ID)[0])
                MenuJson[i].Child[j].Visible = $("#" + MenuJson[i].Child[j].ID)[0].checked;
            delete MenuJson[i].Child[j].css; delete MenuJson[i].Child[j].Name;
        }
    }
    return JSON.stringify(MenuJson);
}
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="img">
<div id="oDIV1">
<form action="" method="get" name="form1">
<table border="0" cellspacing="0" cellpadding="0" style="width: 100%;">
<tr>
<td style="height: 422px" valign="top">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr>
<td ><div class="alert alert-success headtip" role="alert">第一步：选择场景</div>&nbsp;
</td>
</tr>
<tr>
<td style="height: 24px">
<input name="Radio1" id="Allradio" type="radio" value="1" checked="checked" style="display: none" />
<input name="Radio1" id="Contentradio" type="radio" value="content" style="display: none" />
<input name="Radio1" id="Shopradio" type="radio" value="shop" style="display: none" />
<input name="Radio1" id="Pageradio" type="radio" value="page" style="display: none" />
<input name="Radio1" id="Forumradio" type="radio" value="education" style="display: none" />
<input name="Radio1" id="OAradio" type="radio" value="office" style="display: none" />
<input name="Radio1" id="Configradio" type="radio" value="config" style="display: none" />
<input name="Radio1" id="Saferadio" type="radio" value="safe" style="display: none" />
<table width="100%" border="0" cellspacing="10" cellpadding="0">
	<tr>
		<td align="center" valign="middle">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size: 12px">
				<tr>
					<td align="center" valign="middle">
						<img alt="" id="AllIco" src="/App_Themes/Admin/AllIco/Show0.png" onclick="SelectConfig(this.id)" title="全部显示" style="cursor: pointer" />
					</td>
				</tr>
				<tr>
					<td align="center" valign="middle" style="line-height: 30px">全部显示
					</td>
				</tr>
			</table>
		</td>
		<td align="center" valign="middle">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size: 12px">
				<tr>
					<td align="center" valign="middle">
						<img alt="" id="ContentIco" src="/App_Themes/Admin/ContentIco/Show1.png"
							onclick="SelectConfig(this.id)" title="内容管理" style="cursor: pointer" />
					</td>
				</tr>
				<tr>
					<td align="center" valign="middle" style="line-height: 30px">内容管理
					</td>
				</tr>
			</table>
		</td>
		<td align="center" valign="middle">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size: 12px">
				<tr>
					<td align="center" valign="middle">
						<img alt="" id="ShopIco" src="/App_Themes/Admin/ShopIco/Show1.png"
							onclick="SelectConfig(this.id)" title="商城管理" style="cursor: pointer" />
					</td>
				</tr>
				<tr>
					<td align="center" valign="middle" style="line-height: 30px">商城管理
					</td>
				</tr>
			</table>
		</td>
		<td align="center" valign="middle">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size: 12px">
				<tr>
					<td align="center" valign="middle">
						<img alt="" id="PageIco" src="/App_Themes/Admin/PageIco/Show1.png"
							onclick="SelectConfig(this.id)" title="企业黄页" style="cursor: pointer" />
					</td>
				</tr>
				<tr>
					<td align="center" valign="middle" style="line-height: 30px">企业黄页
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr>
		<td align="center" valign="middle">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size: 12px">
				<tr>
					<td align="center" valign="middle">
						<img alt="" id="ForumIco" src="/App_Themes/Admin/ForumIco/Show1.png"
							onclick="SelectConfig(this.id)" title="教育模块" style="cursor: pointer" />
					</td>
				</tr>
				<tr>
					<td align="center" valign="middle" style="line-height: 30px">教育模块
					</td>
				</tr>
			</table>
		</td>
		<td align="center" valign="middle">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size: 12px">
				<tr>
					<td align="center" valign="middle">
						<img alt="" id="OAIco" src="/App_Themes/Admin/OAIco/Show1.png"
							onclick="SelectConfig(this.id)" title="智能办公" style="cursor: pointer" />
					</td>
				</tr>
				<tr>
					<td align="center" valign="middle" style="line-height: 30px">智能办公</td>
				</tr>
			</table>
		</td>
		<td align="center" valign="middle">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size: 12px">
				<tr>
					<td align="center" valign="middle">
						<img alt="" id="ConfigIco" src="/App_Themes/Admin/ConfigIco/Show1.png"
							onclick="SelectConfig(this.id)" title="配置场景" style="cursor: pointer" />
					</td>
				</tr>
				<tr>
					<td align="center" valign="middle" style="line-height: 30px">配置场景
					</td>
				</tr>
			</table>
		</td>
		<td align="center" valign="middle">
			<table width="100%" border="0" cellspacing="0" cellpadding="0" style="font-size: 12px">
				<tr>
					<td align="center" valign="middle">
						<img alt="" id="SafeIco" src="/App_Themes/Admin/SafeIco/Show1.png"
							onclick="SelectConfig(this.id)" title="安全维护" style="cursor: pointer" />
					</td>
				</tr>
				<tr>
					<td align="center" valign="middle" style="line-height: 30px">安全维护
					</td>
				</tr>
			</table>
		</td>
	</tr>
</table>
</td>
</tr>
</table>
</td>
</tr>
<tr>
<td style="height: 37px" align="center" valign="top">
<span style="display: none;">
<input id="noshow" name="noshow" type="checkbox" checked="checked" /><label for="noshow">以后不再显示</label>
</span>
<input id="Button0" type="button" value="下一步" class="btn btn-primary" onclick="Showtoolsup(2);" />
</td>
</tr>
</table>
</form>
</div>
<div id="oDIV2" style="height: 500px; display: none; width: 100%;">
<form action="" method="get" name="form1">
<table width="100%" border="0" cellspacing="0" cellpadding="0">
<tr>
<td style="height: 422px" valign="top">
<table class="Scences" id="Scences" cellspacing="0" cellpadding="0">
</table>
</td>
</tr>
</table>
<div style="width: 100%;margin-top:10px;">
<span style="display: none;">
<input id="noshow2" name="noshow" type="checkbox" checked="checked" /></span>
<input id="Button2" type="button" value="上一步" class="btn btn-success" onclick="Showtoolsup(1)" />
<input id="Button3" type="button" value="保存配置" class="btn btn-success" onclick="disBtn(this); SetConfig();" style="margin-left: 10px;" />
<asp:HiddenField ID="CurModel_Hid" runat="server" />
</div>
</form>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
    $().ready(function () {
        if ($("#CurModel_Hid").val() != "")
            SelectConfig($("#CurModel_Hid").val());
    });
    function SetConfig() {
        var source = "<%=Source%>";
        var config = GetConfig($("[name=Radio1]:Checked").val());
        switch (source) {
            case "admin":
                var model = $("[name=Radio1]:Checked").attr('id');
                model = model.substring(0, model.length - 5) + "Ico";
                parent.SetConfig(config,model);
                break;
            default:
                $.post("Scence.aspx", { action: "Desk", value: config }, function (data) {
                    if (data) { top.location = top.location; }
                });
                break;
        }
    }
function Showtoolsup(vasto) {
    switch (vasto) {
        case 1:
            document.getElementById("oDIV2").style.display = "none";
            document.getElementById("oDIV1").style.display = "";
            break;
        case 2:
            document.getElementById("oDIV1").style.display = "none";
            document.getElementById("oDIV2").style.display = "";
            loadScence($("[name=Radio1]:Checked").val());
            break;
    }
}
function SelectConfig(sId) {
    var arraystr = ["AllIco", "ContentIco", "ShopIco", "PageIco", "ForumIco", "OAIco", "ConfigIco", "SafeIco"];
    for (var pi = 0; pi < arraystr.length; pi++) {
        var sstop = arraystr[pi].toString().substring(0, arraystr[pi].toString().length - 3);
        //alert(sId);
        if (sId == arraystr[pi]) {
            document.getElementById(sstop + 'radio').checked = true;
            document.getElementById(arraystr[pi]).src = "/App_Themes/Admin/" + sId + "/Show0.png";
        }
        else {

            document.getElementById(sstop + 'radio').checked = false;
            document.getElementById(arraystr[pi]).src = "/App_Themes/Admin/" + arraystr[pi] + "/Show1.png";
        }
    }
}
</script>
</asp:Content>