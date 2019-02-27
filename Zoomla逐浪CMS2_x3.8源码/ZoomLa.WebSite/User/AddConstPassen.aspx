<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="AddConstPassen.aspx.cs" Inherits="User_AddConstPassen" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>常用客户管理</title>
<script src="/JS/ICMS/area.js"></script>
<script src="/JS/ZL_Regex.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="ConstPassen"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="ConstPassen.aspx">客户管理</a></li>
        <li class="active"><asp:Label ID="Label1" runat="server" Text="添加客户" ></asp:Label></li> 
    </ol>
</div>
<div class="container u_cnt btn_green">
    <div class="guide" id="routine" style="float: left; width: 100%">
            <ul class="nav nav-tabs">
                <li class="active"><a href="#tab0" data-toggle="tab" onclick="ShowTabs(0)">基本信息</a></li>
                <li><a href="#tab1" data-toggle="tab" onclick="ShowTabs(1)">联络信息</a></li>  
            </ul>
        <table  class="table table-striped table-bordered table-hover">
            <tr class="tdbg">
                <td style="height: 100px;" valign="top">
                    <table id="Tabs0"  class="table table-striped table-bordered table-hover">
                        <tr>
                            <td align="right" class="tdbgleft">
                                客户类别：
                            </td>
                            <td>
                                <table id="RadlClientType" border="0">
                                    <tr>
                                        <td>
                                            <input id="RadlClientType_0" type="radio" checked name="RadlClientType"  runat="server" onclick="isPerson('Enterprise_Add')" />
                                            <label for="RadlClientType_0">
                                                企业客户</label>
                                        </td>
                                        <td>
                                            <input id="RadlClientType_1" type="radio" name="RadlClientType" runat="server" onclick="isPerson('Person_Add')" />
                                            <label for="RadlClientType_1">
                                                个人客户</label>
                                        </td>
                                    </tr>
                                </table> 
                            </td>
                            <td align="right" class="tdbgleft">
                                客户组别：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropGroupID" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                                <span style="color: Red;">*</span> <span id="ctl00_CphContent_ValxGroupID" style="color: Red;
                                    visibility: hidden;">请选择客户组别！</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="DropGroupID" ErrorMessage="客户组别不能为空！"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" align="right" class="tdbgleft">
                                客户名称：
                            </td>
                            <td style="width: 35%">
                                <input type="text" maxlength="100" id="TxtClientName" class="form-control text_300" runat="server" />
                                <span id="TxtClientName_span" class="color_red">*</span>
                            </td>
                            <td style="width: 15%" align="right" class="tdbgleft">
                                客户编号：
                            </td>
                            <td style="width: 35%">
                                <asp:Label runat="server" ID="TxtClientNum" maxlength="20"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                助记名称：
                            </td>
                            <td>
                                <input type="text" maxlength="20" runat="server" id="TxtShortedForm" class="form-control text_300" />
                                <span id="TxtShortedForm_span" class="color_red">*</span> 
                            </td>
                            <td align="right" class="tdbgleft">
                                证件号码：
                            </td>
                            <td>
                                <input name="TxtIDCard" type="text" id="TxtIDCard" class="form-control text_300" runat="server" />
                                <span id="TxtIDCard_span" class="color_red"></span> 
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                区域：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropArea" runat="server" CssClass="form-control text_300" AutoPostBack="false" EnableViewState="true">
                                </asp:DropDownList>
                            </td>
                            <td align="right" class="tdbgleft">
                                行业：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropClientField" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                价值评估：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropValueLevel" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                            <td align="right" class="tdbgleft">
                                信用等级：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropCreditLevel" runat="server" CssClass="form-control text_300" AutoPostBack="false" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                重要程度：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropImportance" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                            <td align="right" class="tdbgleft">
                                关系等级：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropConnectionLevel" runat="server" CssClass="form-control text_300">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                客户来源：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropSourceType" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                                <span style="color: Red;">*</span> <span id="ctl00_CphContent_ValxSourceType" style="color: Red;
                                    visibility: hidden;">请选择客户来源！</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="DropSourceType" ErrorMessage="客户来源不能为空！"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" class="tdbgleft">
                                阶段：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropPhaseType" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                                <span style="color: Red;">*</span> <span id="ctl00_CphContent_ValxPhaseType" style="color: Red;
                                    visibility: hidden;">请选择阶段！</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="DropPhaseType" ErrorMessage="阶段不能为空！"></asp:RequiredFieldValidator>
                            </td>
                        </tr> 
                        <asp:Literal runat="server" ID="htmlStr"></asp:Literal>
                    </table>
                    <table id="Tabs1" cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;
                       display:none;">
                        <tbody id="infoE">
                            <tr> 
                                <td colspan="3" style="height: 23px">
                                    <div id="ctl00_CphContent_Region_UpnlRegion">
                                        <table class="table table-striped table-bordered table-hover" border="0" cellpadding="2" cellspacing="1">
                                            <tr>
                                                <td style="width: 15%" align="right" class="tdbgleft">
                                                    国家/地区： 
                                                </td>
                                                <td colspan="2" align="left">
                                                    <select name="country" id="country" runat="server" class="form-control text_300">
                                                        <option value="">请选择</option>
                                                        <option value="中华人民共和国" selected>中华人民共和国</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  align="right" class="tdbgleft">
                                                    省/市/自治区：
                                                </td>
                                                <td align="left">
                                                    <select name="province" id="province" onchange="province_onchange(province,city)" class="form-control text_300">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="tdbgleft">
                                                    市/县/区/旗：
                                                </td>
                                                <td align="left">
                                                    <select name="city" id="city"  class="form-control text_300">
                                                    </select>
                                                    <select id="tbCounty" style="display:none;"></select>
                                                    <asp:HiddenField ID="Adrees_Hid" runat="server" />
                                                </td>
                                            </tr>
                                             <tr class="tdbg">
                                            <td  align="right" class="tdbgleft">
                                                联系地址：
                                            </td>
                                            <td>
                                                <input name="TxtAddress" type="text" runat="server" maxlength="255" id="TxtAddress" class="form-control text_300"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="tdbgleft">
                                                邮政编码：
                                            </td>
                                            <td>
                                                <input name="TxtZipCodeW" type="text" id="TxtZipCodeW" class="form-control text_300" runat="server" />
                                                <span id="TxtZipCodeW_span" class="color_red"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:15%" align="right" class="tdbgleft">
                                                联系电话：
                                            </td>
                                            <td style="width: 38%">
                                                <input name="TxtPhone" type="text" maxlength="30" id="TxtPhone" class="form-control text_300" runat="server" />
                                                <span id="TxtPhone_span" class="color_red"></span> 
                                            </td>
                                        </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>     
                        </tbody>
                    </table>   
                </td>
            </tr>
            <tr>
                <td colspan="2" class="text-center">
                    <asp:Button ID="ctl00_CphContent_BtnSave" runat="server" Text="保存" OnClick="ctl00_CphContent_BtnSave_Click" OnClientClick="return CheckForm()" class="btn btn-primary" />  
                    <a href="ConstPassen.aspx" class="btn btn-primary">返回</a> 
                </td>
            </tr>
        </table> 
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" src="/JS/Controls/ZL_PCC.js"></script>
<script>
function ShowTabs(ID) {
    for (i = 0; i < 2; i++) {
        if (i == ID) {
            document.getElementById("Tabs" + i).style.display = "";
        }
        else {
            document.getElementById("Tabs" + i).style.display = "none";
        }
    }
}
$().ready(function () {
    var pcc = new ZL_PCC("province", "city", "tbCounty");
    if ($("#Adrees_Hid").val() != "") {
        var attr = $("#Adrees_Hid").val().split(',');
        pcc.SetDef(attr[0], attr[1]);
    }
    pcc.ProvinceInit();
});
function CheckForm() {
    var flag = false;
    console.log("check");
    $(".color_red").text(""); $("#TxtClientName_span").text("*"); $("#TxtShortedForm_span").text("*");
    if ($("#TxtClientName").val() == "") {
        $("#TxtClientName_span").text("客户名不能为空！");
    } 
    else if ($("#TxtShortedForm").val() == "") {
        $("#TxtShortedForm_span").text("助计名不能为空！");
    }
    else if (!ZL_Regex.isIDCard($("#TxtIDCard").val()) && !ZL_Regex.isEmpty($("#TxtIDCard").val())) {
        $("#TxtIDCard_span").text("证件号码格式不正确！")
    }
    else if(!ZL_Regex.isZipCode($("#TxtZipCodeW").val()) && !ZL_Regex.isEmpty($("#TxtZipCodeW").val())) {
        $("#TxtZipCodeW_span").text("邮政编码格式不正确！");
    } 
    else if (!ZL_Regex.isMobilePhone($("#TxtPhone").val()) && !ZL_Regex.isEmpty($("#TxtPhone").val())) {
        $("#TxtPhone_span").text("手机号码格式不正确!");
    }
    else { flag = true; }
    return flag;
}
</script>
</asp:Content>
