<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="AdPlanAdd.aspx.cs" Inherits="User_Info_AdPlanAdd" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>申请广告</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="AdPlan"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li><a href="AdPlan.aspx">广告计划</a></li>
<li class="active">申请广告</li>
</ol>
</div>
<div class="container btn_green">
<div class="us_seta" id="AddMain" runat="server">
<table class="ad_info table table-striped table-bordered table-hover">
	<tr>
		<td colspan="2" class="text-center">
			<asp:Label ID="Label1" runat="server" Text="申请广告"></asp:Label>
			<asp:Label ID="Label2" Visible="false" runat="server" Text="修改广告"></asp:Label>
		</td>
	</tr>
	<tr>
		<td align="right">版位名称&nbsp;: </td>
		<td>
			<div id="nocontent" runat="server" style="color: #f00;" visible="false">无版位信息</div>
			<asp:RadioButtonList ID="ADID" runat="server" DataSourceID="RedioDataSource" DataTextField="ZoneName" DataValueField="ZoneID" RepeatDirection="Horizontal"></asp:RadioButtonList>
			<asp:ObjectDataSource ID="RedioDataSource" runat="server" SelectMethod="ADZone_ID" TypeName="B_ADZone"></asp:ObjectDataSource>
		</td>
	</tr>
	<tr>
		<td align="right">广告权重: </td>
		<td>
			<asp:DropDownList ID="Scale" CssClass=" form-control text_md" runat="server">
				<asp:ListItem Value="10"></asp:ListItem>
				<asp:ListItem Value="9"></asp:ListItem>
				<asp:ListItem Value="8"></asp:ListItem>
				<asp:ListItem Value="7"></asp:ListItem>
				<asp:ListItem Value="6"></asp:ListItem>
				<asp:ListItem Value="5"></asp:ListItem>
				<asp:ListItem Value="4"></asp:ListItem>
				<asp:ListItem Value="3"></asp:ListItem>
				<asp:ListItem Value="2"></asp:ListItem>
				<asp:ListItem Value="1"></asp:ListItem>
			</asp:DropDownList>
			*如要购买完整广告请填10
		</td>
	</tr>
	<tr>
		<td align="right">购买天数&nbsp;: </td>
		<td>
			<asp:TextBox ID="ShowTime" class=" form-control text_md" runat="server" Text="广告投放时间数（单位：天）" onclick="setEmpty(this)" onblur="settxt(this)"></asp:TextBox>
			<span><b>*</b>广告投放时间数（单位：天）</span>
			<asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="ShowTime" ErrorMessage="申请天数必须在1-365天之间" MaximumValue="365" MinimumValue="1" SetFocusOnError="True" Display="Dynamic" Type="Integer"></asp:RangeValidator></td>
	</tr>
	<tr>
		<td align="right">计划费用&nbsp;: </td>
		<td class="style9">
			<asp:TextBox ID="Price" Class=" form-control" runat="server" Width="200px"></asp:TextBox>
			<span><b>*</b>单位：人民币。</span>
			<asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="Price" Display="Dynamic" ErrorMessage="不能小于0或负数" MaximumValue="999999999" MinimumValue="1" SetFocusOnError="True" Type="Double"></asp:RangeValidator>
		</td>
	</tr>
	<tr>
		<td align="right">广告内容&nbsp;: </td>
		<td>
			<asp:TextBox ID="Content" runat="server" Class="form-control text_md" Height="100px" TextMode="MultiLine"></asp:TextBox>
			<span><b>*</b>可以填写您对于广告投放的其它要求。</span>
		</td>
	</tr>
	<tr>
		<td align="right">广告附件&nbsp;: </td>
		<td>
			<asp:TextBox ID="txt" runat="server" Width="200px" Class="form-control"></asp:TextBox><br />
			<iframe id="Upload_Pic" src="/Common/Upload.aspx" marginheight="0" marginwidth="0" frameborder="0" height="30" scrolling="no"></iframe>
			<asp:Label ID="LabPicPath" runat="server" Text="请选择上传路径！" Visible="False"></asp:Label>
			<span>*推荐使用rar|zip|jpg|docx等格式上传您的广告详细附件</span>
		</td>
	</tr>
	<tr>
		<td align="right">投放时间&nbsp;: </td>
		<td>
			<span id="Span2">
				<asp:TextBox ID="Ptime" class="form-control" runat="server" Width="200px" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })"></asp:TextBox>
				<span><b>*</b>选择计划广告投放的时间</span>
			</span>
			<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Ptime" Display="Dynamic" ErrorMessage="时间格式为yyyy-MM-dd" SetFocusOnError="True" ValidationExpression="\d{4}-\d{2}-\d{2}"></asp:RegularExpressionValidator>
		</td>
	</tr>
	<tr>
		<td colspan="2" align="center" style="text-align: center;">
			<asp:Button ID="Button2" runat="server" Text="修改" OnClick="Button2_Click" class="btn btn-primary" OnClientClick="javascript:if(check()) return true;else return false;" />
			<asp:Button ID="Button1" runat="server" Text="提交申请" OnClick="Button1_Click" class="btn btn-primary" />
			<input id="Reset1" class="btn btn-primary" type="reset" value="重置" />
		</td>
	</tr>
</table>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Common.js" type="text/javascript"></script>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript">
function setEmpty(obj) {
	if (obj.value == "广告投放时间数（单位：天）") {
		obj.value = "";
		document.getElementById("ShowTime").style.color = "#000";
	}
}
function settxt(obj) {
	if (obj.value == "") {
		obj.value = "广告投放时间数（单位：天）";
		document.getElementById("ShowTime").style.color = "#aaa";
	}
}
</script>
</asp:Content>