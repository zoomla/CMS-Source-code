<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="AddJsPic.aspx.cs" Inherits="Manage_I_Content_AddJsPic" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>文章JS文件管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
<div>
<table width="100%" cellpadding="2" cellspacing="1" border="0" class="table table-striped table-bordered table-hover" align="center">
	<tr align="center">
		<td colspan="2" class="spacingtitle">
			<asp:Label ID="TxtTitle" runat="server" Text="添加新的JS文件（图片列表方式）"></asp:Label>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>代码名称：</b>
		</td>
		<td>
			<asp:TextBox ID="Jsname" runat="server" Width="270px" CssClass="form-control pull-left"></asp:TextBox>
			&nbsp;<font color="red">*</font><asp:RequiredFieldValidator ID="RequiredFieldValidator1"
				runat="server" ErrorMessage="代码名称不能为空" ControlToValidate="Jsname"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>简介：</b>
		</td>
		<td>
			<asp:TextBox ID="JsReadme" runat="server" Rows="5" Width="270px" CssClass="form-control pull-left"></asp:TextBox>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>内容代码格式：</b>
		</td>
		<td>
			<asp:RadioButtonList ID="ContentType" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Selected="True">JS</asp:ListItem>
				<asp:ListItem>HTML</asp:ListItem>
			</asp:RadioButtonList>
			<font color="blue">注意：频道选择生成Shtml方式时可选用此项，可以在扩展名为.shtml的文件中使用<br />
				&lt;!--#include file=&quot;aaaa.html&quot;--&gt;这样的指令包含其他文件，这样对搜索引擎比使用JS代码调用会更友好。
			</font>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>文件名：</b>
		</td>
		<td>
			<asp:TextBox ID="JsFileName" runat="server" Width="270px" CssClass="form-control pull-left" onfocus="AddJs('JsName','JsFileName','ContentType')"></asp:TextBox>
			<font color="#ff0000">*</font> <span id="jstype"><font color="red">以.js为扩展名</font></span>
		</td>
	</tr>
	<tr>
		<td align="center" colspan="2">
			<b>参数设置</b>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>所属栏目：</b>
		</td>
		<td>
			<asp:DropDownList ID="ClassID" runat="server" Width="270" CssClass="form-control pull-left">
			</asp:DropDownList>
			<asp:CheckBox ID="IncludeChild" runat="server" Text="包含子栏目" />
			<font color="red"><b>&nbsp;注意：</b></font>不能指定为外部栏目
		</td>
	</tr>
	
	<tr>
		<td align="right" style="width: 24%">
			<strong>文章数目：</strong>
		</td>
		<td height='25'>
			<asp:TextBox ID="ArticleNum" runat="server" Width="50px" CssClass="form-control pull-left">8</asp:TextBox>
			<font color='#FF0000'>*</font>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>文章属性：</b>
		</td>
		<td>
			<asp:CheckBoxList ID="ContentProperty" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="IsHot" Text="热门文章"></asp:ListItem>
				<asp:ListItem Value="IsElite" Text="推荐文章"></asp:ListItem>
			</asp:CheckBoxList>
			<font color="#ff0000">如果都不选，将显示所有文章</font>&nbsp;
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>日期范围：</b>
		</td>
		<td>
			<span class="pull-left" style="line-height:30px; margin-right:5px;">只显示最近</span>
			<asp:TextBox ID="DateNum" runat="server" Width="50px" CssClass=" form-control pull-left" Text="30"></asp:TextBox>
			<span class="pull-left" style="line-height:30px; margin-left:5px;">天内更新的文章 <font color="#ff0000">&nbsp;如果为空或0，则显示所有天数的文章</font></span>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>排序方法：</b>
		</td>
		<td>
			<asp:DropDownList ID="OrderType" runat="server" CssClass="form-control" Width="150">
				<asp:ListItem Value="GIDDesc">文章ID(降序)</asp:ListItem>
				<asp:ListItem Value="GID">文章ID(升序)</asp:ListItem>
				<asp:ListItem Value="UpdateDesc">更新时间(降序)</asp:ListItem>
				<asp:ListItem Value="Update">更新时间(升序)</asp:ListItem>
				<asp:ListItem Value="HitsDesc">点击次数(降序)</asp:ListItem>
				<asp:ListItem Value="Hits">点击次数(升序)</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>显示样式：</b>
		</td>
		<td>
			<asp:RadioButtonList ID="ShowType" runat="server">
				<asp:ListItem Selected="True" Value="1" Text="图片+标题：上下排列"></asp:ListItem>
				<asp:ListItem Value="2" Text="图片+标题：左右排列"></asp:ListItem>
			</asp:RadioButtonList>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>首页图片设置：</b>
		</td>
		<td>
			<span class="pull-left" style="">宽度：</span><asp:TextBox ID="ImgWidth" runat="server" Width="70px" CssClass="form-control pull-left">130</asp:TextBox><span class="pull-left" style="line-height:30px; margin-left:5px;"></span>像素
			高度：<asp:TextBox ID="ImgHeight" runat="server" Width="56px" CssClass="l_input">90</asp:TextBox>像素
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>文章标题字符数：</b>
		</td>
		<td>
			<asp:TextBox ID="TitleLen" runat="server" Width="56px" CssClass=" form-control pull-left">20</asp:TextBox>
			<font color="#ff0000">&nbsp; 若为0，则不显示标题；若为-1，则显示完整标题。字母算一个字符，汉字算两个字符。</font>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>文章副标题字符数：</b>
		</td>
		<td>
			<asp:TextBox ID="ContentLen" runat="server" Width="56px" CssClass="form-control pull-left">0</asp:TextBox>
			&nbsp; <font color="#ff0000">如果大于0，则显示指定字数的文章副标题</font>
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>显示内容：</b>
		</td>
		<td>
			<asp:CheckBox ID="ShowTips" runat="server" Text="显示作者、更新时间、点击数等提示信息" />
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>每行显示文章数：</b>
		</td>
		<td>
			<asp:DropDownList ID="Cols" CssClass="form-control pull-left" Width="80" runat="server">
				<asp:ListItem>1</asp:ListItem>
				<asp:ListItem>2</asp:ListItem>
				<asp:ListItem>3</asp:ListItem>
				<asp:ListItem>4</asp:ListItem>
				<asp:ListItem>5</asp:ListItem>
				<asp:ListItem>6</asp:ListItem>
				<asp:ListItem>7</asp:ListItem>
				<asp:ListItem>8</asp:ListItem>
				<asp:ListItem>9</asp:ListItem>
				<asp:ListItem>10</asp:ListItem>
				<asp:ListItem>11</asp:ListItem>
				<asp:ListItem>12</asp:ListItem>
			</asp:DropDownList>
			&nbsp; 超过指定列数就会换行
		</td>
	</tr>
	<tr>
		<td align="right" style="width: 24%">
			<b>链接地址选项：</b>
		</td>
		<td>
			<asp:DropDownList ID="UrlType" CssClass="form-control" Width="200" runat="server">
				<asp:ListItem Value="Relative">使用相对路径</asp:ListItem>
				<asp:ListItem Value="Absolute">使用包含完整网址的绝对路径</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
</table>
</div>
    <asp:HiddenField ID="JSID_Hid" runat="server" />
<div style="margin-bottom:10px;">
<center>
<asp:Button ID="Save_Btn" runat="server" Text="修改保存结果" CssClass="btn btn-primary" OnClick="Button1_Click" />
<asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="返回" CausesValidation="false" onclick="Button2_Click" />
</center>
</div>
</asp:Content>