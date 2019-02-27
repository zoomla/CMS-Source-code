<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="Author.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.Author" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
<title>添加作者</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" Runat="Server">
<table width="100%" border="0" cellpadding="2" cellspacing="1" class="table table-striped table-bordered table-hover">
	<tr align="center">
		<td colspan="4" class="spacingtitle">添加作者信息</td>
	</tr>
	<tr>
		<td><strong>姓名：</strong></td>
		<td>
			<asp:TextBox ID="TxtName" onblur="checkName()" runat="server" class="form-control pull-left" Width="150"></asp:TextBox><span id="TxtNameRemind"></span>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="用户名不能为空" Display="Dynamic" ControlToValidate="TxtName" Enabled="true"></asp:RequiredFieldValidator> 
		</td>
        <td><strong>照片：</strong></td>
		<td>
			<ZL:SFileUp ID="SFile_UP" runat="server" FType="Img" />
		</td>
	</tr>
	<tr>
		<td>
			<strong>会员名：</strong>
		</td>
		<td><asp:TextBox ID="TxtUserName" runat="server" class="form-control pull-left" Width="150" /><span id="TxtUserNameRemind"></span></td>
	</tr>
	<tr>
		<td>
			<strong>性别：</strong>
		</td>
		<td>
			<asp:RadioButtonList ID="RadlSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
				<asp:ListItem Selected="True" Value="1">男</asp:ListItem>
				<asp:ListItem Value="0">女</asp:ListItem>
			</asp:RadioButtonList>
		</td>
	</tr>
	<tr >
		<td><strong>生日：</strong></td>
		<td>
			<input name="member.birth" class="form-control pull-left" style="width:150px;" id="tbUBday" runat="server" type="text" value="1982-1-1" size="14" readonly="readonly" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' })" />(单击文本框,选择年月)  
		</td>
	</tr>
	<tr>
		<td>
			<strong>地址：</strong></td>
		<td>
			<asp:TextBox ID="TxtAddress" runat="server" MaxLength="20" class="form-control pull-left" Width="150"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td>
			<strong>手机：</strong></td>
		<td>
			<asp:TextBox ID="TxtTel" runat="server" MaxLength="20" class="form-control pull-left" Width="150"></asp:TextBox>
			<asp:RegularExpressionValidator ID="REV1" Display="Dynamic" ControlToValidate="TxtTel" ValidationExpression="^1\d{10}$" runat="server" ErrorMessage="手机号码格式不正确"></asp:RegularExpressionValidator>
		</td>
	</tr>
	<tr>
		<td>
			<strong>传真：</strong></td>
		<td>
			<asp:TextBox ID="TxtFax" runat="server" MaxLength="20" class="form-control pull-left" Width="150"></asp:TextBox>
			<span id="warnTxtFax" style="color:red;display:none;">传真格式不正确</span>
		</td>
	</tr>
	<tr>
		<td><strong>单位：</strong></td>
		<td>
			<asp:TextBox ID="TxtCompany" runat="server" MaxLength="20" class="form-control pull-left" Width="150" />
		</td>
	</tr>
	<tr>
		<td>
			<strong>部门：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtDepartment" runat="server" MaxLength="20"  class="form-control pull-left" Width="150"/>
		</td>
		<td>
			<strong>主页：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtHomePage" runat="server" class="form-control pull-left" Width="150" />
		</td>
	</tr>
	<tr>
		<td>
			<strong>邮编：</strong></td>
		<td>
			<asp:TextBox ID="TxtZipCode" runat="server" MaxLength="20" class="form-control pull-left" Width="150" />
			<asp:RegularExpressionValidator ID="REV3" runat="server" ControlToValidate="TxtZipCode" Display="Dynamic" ErrorMessage="邮编格式不正确" ValidationExpression="^[1-9]\d{5}(?!\d)$" Visible="true"></asp:RegularExpressionValidator>
		</td>
		<td>
			<strong>邮件：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtEmail" runat="server" MaxLength="20" class="form-control pull-left" Width="150" />
			<asp:RegularExpressionValidator ID="REV2" runat="server" ControlToValidate="TxtEmail" Display="Dynamic" ErrorMessage="电子邮箱格式不正确" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$" Visible="true"></asp:RegularExpressionValidator>
		</td>
	</tr>
	<tr>
		<td>
			<strong>通讯：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtMail" runat="server" MaxLength="20" class="form-control pull-left" Width="150" />
		</td>
		<td>
			<strong>IM：</strong>
		</td>
		<td>
			<asp:TextBox ID="TxtIm" runat="server" MaxLength="20" class="form-control pull-left" Width="150" />
		</td>
	</tr>
	<tr>
		<td>
			<strong>分类：</strong>
		</td>
		<td colspan="3">
			<asp:RadioButtonList id="RadlAuthorType"  RepeatLayout="Flow" RepeatColumns="6" RepeatDirection="Horizontal" runat="server">
				<asp:ListItem Selected="True" Value="0">大陆作者</asp:ListItem>
				<asp:ListItem Value="1">港台作者</asp:ListItem>
				<asp:ListItem Value="2">海外作者</asp:ListItem>
				<asp:ListItem Value="3">本站特约</asp:ListItem>
				<asp:ListItem Value="4">其他作者</asp:ListItem>
			</asp:RadioButtonList>
		</td>
	</tr>
	<tr>
		<td>
			<strong>属性：</strong>
		</td>
		<td colspan="3">
			<asp:CheckBox ID="ChkElite" runat="server" Checked="true"/>推荐
			<asp:CheckBox ID="ChkOnTop" runat="server" />置顶
		</td>
	</tr>
	<tr>
		<td>
			<strong>作者简介：</strong>
		</td>
		<td colspan="3">
			<asp:TextBox ID="TxtIntro" TextMode="MultiLine" runat="server" height="300px" Width="583px" class="form-control pull-left" />
		</td>
	</tr>
	<tr>
		<td>
			<strong>是否启用：</strong>
		</td>
		<td colspan="3">
			<asp:CheckBox ID="ChkPass" runat="server" Checked="true" />
		</td>
	</tr>
	<tr>
		<td colspan="4" align="center" class="tdbg">
			<asp:Button ID="EBtnModify" Text="修改" OnClick="EBtnModify_Click" runat="server"  class="btn btn-primary" Visible="false"/>
			<asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
			<input name="Cancel" type="button" class="btn btn-primary" id="Cancel" value="取消" onclick="javascript: window.location.href = 'AuthorManage.aspx'" />
		</td>
	</tr>
</table>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script>
	function checkName()
	{
		var v = document.getElementById("TxtName").value;
		SyncpostToCS(v);
	}

	function SyncpostToCS(v) {
		$.ajax({
			type: "Post",
			url: "Author.aspx",
			//dataType: "json",
			data: { value: v },
			//async: true,
			success: function (data) {
				//result = data;
				if (data == "1") {
					$("#TxtNameRemind").html("存在名字相同，建议您使用:" + v + "1").css('color', 'red');
					isSubmit = false;
				}
				else if (data == "0") {
					$("#TxtNameRemind").html("");
					isSubmit = true;
				}
			},
			error: function (data) { alert("失败"); }
		});
	}
</script>
</asp:Content>