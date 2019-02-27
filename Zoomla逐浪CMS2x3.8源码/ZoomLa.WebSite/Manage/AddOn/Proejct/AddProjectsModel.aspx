<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddProjectsModel.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="manage_AddOn_AddProjectsModel" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加项目模型</title>
    <style type="text/css">
    .style1{	background: #e0f7e5;   padding: 2px;	width: 35%;	height: 33px;}
    .style2{	height: 33px;}
    .style3{	color: #1e860b;	font-weight: bold;	line-height: 22px;	letter-spacing: 8px;	height: 43px;	padding: 2px;	background: #dbf9d9 url('../../App_Themes/AdminDefaultTheme/Images/title.gif') repeat-x 50% top;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered table-responsive">
	<tr>
		<td class="style3" colspan="2" align="center">
			<asp:Literal ID="LTitle" runat="server" Text="添加项目模型"></asp:Literal>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="style1" >
			<strong>项目模型名称：</strong>
		</td>
		<td class="style2">
			<asp:TextBox ID="TxtModelName" class="form-control text_300" runat="server" Width="156" MaxLength="200" /><font color="red">*</font>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TxtModelName">项目模型名称不能为空</asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft">
			<strong>创建的数据表名：</strong>
		</td>
		<td>
			<asp:Label ID="LblTablePrefix" runat="server" Text="ZL_Pro_" />
			<asp:TextBox ID="TxtTableName" class="form-control text_300" runat="server" Width="120" MaxLength="50" /><font color="red">*</font>
			<asp:RegularExpressionValidator ID="ValeTableName" runat="server" ControlToValidate="TxtTableName" ErrorMessage="只允许输入字母、数字或下划线" ValidationExpression="^[\w_]+$" SetFocusOnError="true" Display="Dynamic" />
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft">
			<strong>名称：</strong>
			<br />
			例如：文章、软件、图片、商品
		</td>
		<td>
			<asp:TextBox ID="TxtItemName" runat="server" class="form-control text_300" Width="156" MaxLength="20" /><font color="red">*</font>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtItemName" ErrorMessage="RequiredFieldValidator">项目名称不能为空</asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft">
			<strong>单位：</strong>
			<br />
			例如：篇、个、张、件
		</td>
		<td>
			<asp:TextBox ID="TxtItemUnit" runat="server" class="form-control text_300" Width="156" MaxLength="20" /><font color="red">*</font>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TxtItemUnit" ErrorMessage="RequiredFieldValidator">项目单位不能为空</asp:RequiredFieldValidator>
		</td>
	</tr>
   <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft">
			<strong>是否复制：</strong>
			  <br />
			  确定模型是否可以复制
		</td>
		<td>
			<asp:RadioButtonList ID="rblCopy" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="1" Selected="True" >是</asp:ListItem>
				<asp:ListItem Value="2">否</asp:ListItem>
			</asp:RadioButtonList>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft">
			<strong>图标：</strong>
			<br />
			图标存放在~/Images/ModelIcon/目录下
		</td>
		<td>
			<asp:TextBox ID="TxtItemIcon" class="form-control text_300" Text="Default.gif" runat="server" Width="156" MaxLength="20" />
			<asp:Image ID="ImgItemIcon" runat="server" ImageUrl="~/Images/ModelIcon/Default.gif" />
			<=<asp:DropDownList ID="DrpItemIcon" runat="server" />
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft">
			<strong>模型描述：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtDescription" runat="server" class="form-control text_300" 
				TextMode="MultiLine" Width="365px" Height="77px" />
		</td>
	</tr>
	<tr class="tdbgbottom">
		<td colspan="2">
			<asp:HiddenField ID="HdnModelId" runat="server" />
			<asp:Button ID="EBtnSubmit"  Text="保 存" class="form-control text_300"  OnClick="EBtnSubmit_Click" runat="server" />
			&nbsp;&nbsp;
			<input name="Cancel" type="button"  id="Cancel" class="form-control text_300"  value="取 消" onclick="window.location.href='ProjectsModel.aspx';" />
		</td>
	</tr>
</table>
<script type="text/javascript">
function ChangeImgItemIcon(icon)
{
	document.getElementById("<%= ImgItemIcon.ClientID %>").src = "../../Images/ModelIcon/"+icon;
}
function ChangeTxtItemIcon(icon)
{
	document.getElementById("<%= TxtItemIcon.ClientID %>").value = icon;
}
</script>
</asp:Content>
