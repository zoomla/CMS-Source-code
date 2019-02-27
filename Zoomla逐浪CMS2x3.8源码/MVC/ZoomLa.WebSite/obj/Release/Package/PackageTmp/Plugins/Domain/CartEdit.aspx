<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CartEdit.aspx.cs" Inherits="ZoomLaCMS.Plugins.Domain.CartEdit" MasterPageFile="~/Common/Common.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
 <title>注册信息编辑</title>
 <script type="text/javascript" src="Site.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<%--<div id="m_site"><p style="float:left;"> 站群中心 >> 域名管理</p></div>--%>
<div id="site_main">
<div id="tempInfoDiv">
	<table id="tempInfoTable">
	<tr><td>域名:</td><td><asp:Label runat="server" ID="domNameT" style="margin-left:12px;"/></td></tr>
	<tr><td>参数来源:</td><td><asp:DropDownList runat="server" ID="tempListDP" style="margin-left:12px;" AutoPostBack="true" OnSelectedIndexChanged="tempListDP_SelectedIndexChanged"></asp:DropDownList></td></tr>
	<tr style="display:none;"><td>模板名：</td><td><span class="redStar">*&nbsp;</span><input id="tempName" type="text" class="site_input" size="30" name="tempName" /></td></tr>
	<tr><td>单位名称（中文名）：</td><td><span class="redStar">*&nbsp;</span><input id="uname1" type="text" class="site_input" size="30" name="uname1" /></td></tr>
	<tr><td>单位名称（英文名）：</td><td><span class="redStar">*&nbsp;</span><input id="uname2" type="text" class="site_input" size="30" name="uname2"  /></td></tr>
	<tr><td></td><td style="color: red;">联系人中文名中至少含有1个中文字符，英文名信息中名和姓必须以空格分开。</td></tr>
	<tr> <td>联系人（中文名）：</td><td><span class="redStar">*&nbsp;</span><input id="rname1" type="text" class="site_input" size="30" name="rname1" /></td></tr>
	<tr><td>联系人（英文名）：</td><td><span class="redStar">*&nbsp;</span><input id="rname2" type="text" class="site_input" size="30" name="rname2" /></td></tr>
	<%--<tr class="CNAddr">
		<td>URL指向:</td><td><span class="redStar">*</span><input id="urlId" type="text" class="site_input" size="30" name="url" value="http://www." /></td>
		</tr>--%>
	<tr><td>电子邮箱：</td><td><span class="redStar">*&nbsp;</span><input id="aemail" type="text" class="site_input" size="30" name="aemail"  /></td></tr>
	<tr>
		<td>所属区域：</td>
		<td><span class="redStar">*</span>
			<select><option value="01">中国</option></select><br />
			 <span class="redStar">*</span>
			<asp:DropDownList runat="server" ID="prvinceDP"  class="dpclass" ClientIDMode="Static"></asp:DropDownList><br />
			 <span class="redStar">*</span>
			 <input type="text" id="cityText" name="cityText" class="site_input"/>
		</td>
	</tr>
	<tr><td>省份城市（英文）：</td><td><span class="redStar">*&nbsp;</span><input id="ucity2" name="ucity2" type="text" class="site_input" size="30"  /></td></tr>
	<tr><td></td><td style="color: red;">通迅地址（中文）信息中必须至少含有1个中文字符</td></tr>
	<tr><td>通迅地址（中文）：</td>
		<td><span class="redStar" style="position:relative;bottom:70px;">*</span>
			<textarea id="uaddr1" rows="4" cols="28" name="uaddr1" class="site_input" style="height:150px; margin-bottom:5px;" ></textarea></td></tr>
	<tr><td>通迅地址（英文）：</td>
		<td><span class="redStar" style="position:relative;bottom:70px;">*</span><textarea id="uaddr2" rows="4" cols="28" name="uaddr2" class="site_input" style="height:150px;" ></textarea></td></tr>
	<tr><td>邮编：</td><td><span class="redStar">*</span><input id="uzip" type="text" name="uzip" class="site_input" size="30"  /></td></tr>
	<tr><td>手机：</td><td><span class="redStar">*</span><input id="uteln" type="text" class="site_input" name="uteln" /></td></tr>
	<tr><td>传真：</td><td><span class="redStar">*</span>
		<input id="ufaxa" type="text" class="site_input" size="6" name="ufaxa"  style="width:60px;"/>--
		<input id="ufaxn" type="text" class="site_input" size="12" name="ufaxn" style="width:114px;"  /></td></tr>
	<tr><td>操作：</td><td>
		<asp:Button runat="server" ID="submitBtn" Text="确认"  class="site_button" style="margin-left:12px;" 
			OnClientClick="return checkValue();" OnClick="submitBtn_Click"/>
		</td></tr>
	<tr><td colspan="2"></td></tr>
</table>
	</div>
</div>
</asp:Content>