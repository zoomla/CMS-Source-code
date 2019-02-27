<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSchool.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.AddSchool" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>添加学校信息</title>
<script src="/JS/ICMS/area.js"></script>
<script src="/JS/Controls/ZL_PCC.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
	<tr>
		<td class="td_l tdleft"><strong>学校名称:</strong></td>
		<td>
			<asp:TextBox ID="txtSchoolName" class="form-control" runat="server" Width="340px"></asp:TextBox>
			<span style="color: Red">*</span>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSchoolName"
				Display="Dynamic" ErrorMessage="学校名称不能为空!"></asp:RequiredFieldValidator></td>
	</tr>
	<%--<tr>
		<td class="tdleft"><strong>所属国家:</strong></td>
		<td>
			<asp:DropDownList ID="txtCountry" CssClass="form-control" Width="150" runat="server" AutoPostBack="True" OnSelectedIndexChanged="txtCountry_SelectedIndexChanged"></asp:DropDownList>
			<span style="color: Red">*</span>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCountry" ErrorMessage="所属国家不能为空!"></asp:RequiredFieldValidator>
		</td>
	</tr>--%>
	<tr>
		<td class="tdleft"><strong>所属省市:</strong></td>
		<td>
            <select name="province_dp" class="form-control text_s" id="province_dp"></select>
            <select name="city_dp" class="form-control text_s" id="city_dp"></select>
            <select name="county_dp" class="form-control text_s" id="county_dp"></select>
		</td>
	</tr>
	<tr>
		<td class="tdleft"><strong>学校类型:</strong></td>
		<td>
			<asp:DropDownList CssClass="form-control" Width="150" ID="txtSchoolType" runat="server">
				<asp:ListItem Value="1">小学</asp:ListItem>
				<asp:ListItem Value="2">中学</asp:ListItem>
				<asp:ListItem Value="3">大学</asp:ListItem>
				<asp:ListItem Value="4">其他</asp:ListItem>
			</asp:DropDownList></td>
	</tr>
	<tr>
		<td class="tdleft"><strong>学校性质:</strong></td>
		<td>
			<asp:DropDownList CssClass="form-control" Width="150" ID="txtVisage" runat="server">
				<asp:ListItem Value="1">公办</asp:ListItem>
				<asp:ListItem Value="2">私立</asp:ListItem>
			</asp:DropDownList></td>
	</tr>
    <tr>
        <td class="tdleft"><strong>学校微标:</strong></td>
        <td>
            <asp:TextBox ID="SchoolIcon_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
        </td>
    </tr>
	<tr>
		<td class="tdleft"><strong>学校信息:</strong></td>
		<td>
			<asp:TextBox ID="txtSchoolInfo" TextMode="MultiLine" class="form-control" runat="server" Height="148px" Width="451px"></asp:TextBox></td>
	</tr>
	<tr>
		<td colspan="2" class="text-center">
			<asp:HiddenField ID="txtID" runat="server" />
			<asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="确认添加" OnClick="Button1_Click" />
			<asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="取消返回" OnClientClick="location.href='SnsSchool.aspx';return false;" />
		</td>
	</tr>
</table>
<asp:HiddenField ID="pro_hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" src="/JS/Plugs/IconSelector.js"></script>
<script>
    $(function () {
        var pcc = new ZL_PCC("province_dp", "city_dp", "county_dp");
        if ($("#pro_hid").val() == "") {
            pcc.ProvinceInit();
        }
        else {
            var attr = $("#pro_hid").val().split(' ');
            pcc.SetDef(attr[0], attr[1], attr[2]);
            pcc.ProvinceInit();
        }
        var iconselecter = new iconSelctor("SchoolIcon_T");
    })
</script>
</asp:Content>