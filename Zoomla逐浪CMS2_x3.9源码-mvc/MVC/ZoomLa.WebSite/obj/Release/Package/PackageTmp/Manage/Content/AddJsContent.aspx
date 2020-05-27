<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddJsContent.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.AddJsContent" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>文章JS文件管理</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
	<table width="100%" cellpadding="2" cellspacing="1" border="0" align="center" class="table table-striped table-bordered table-hover">
	<tr align="center">
		<td colspan="2" class="title">
			<asp:Label ID="TxtTitle" runat="server" Text="添加新的JS文件（普通列表方式）"></asp:Label>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>代码名称：</b>
		</td>
		<td>
			<asp:TextBox ID="JsName" runat="server" Width="270px" CssClass="form-control pull-left" autofocus="true"></asp:TextBox>
			&nbsp;<font color="#ff0000">*</font>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="代码名称不能为空" ControlToValidate="Jsname"></asp:RequiredFieldValidator>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>简介：</b>
		</td>
		<td>
			<asp:TextBox ID="JsReadme" runat="server" TextMode="MultiLine" Rows="5" Width="270px" CssClass="form-control pull-left"></asp:TextBox>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>内容代码格式：</b>
		</td>
		<td>
			<asp:RadioButtonList ID="ContentType" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Selected="True" Value="JS">JS</asp:ListItem>
				<asp:ListItem Value="HTML">HTML</asp:ListItem>
			</asp:RadioButtonList>
			<font color="blue">注意：频道选择生成Shtml方式时可选用此项，可以在扩展名为.shtml的文件中使用<br />
				&lt;!--#include file=&quot;aaaa.html&quot;--&gt;这样的指令包含其他文件，这样对搜索引擎比使用JS代码调用会更友好。</font>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>文件名：</b>
		</td>
		<td>
			<asp:TextBox ID="JsFileName" runat="server" Width="270px" CssClass="form-control pull-left"  onfocus="AddJs('JsName','JsFileName','ContentType')"></asp:TextBox>
			<font color="#ff0000">&nbsp;*<asp:RequiredFieldValidator  ID="RequiredFieldValidator2" runat="server" ControlToValidate="JsFileName" ErrorMessage="文件名不能为空!" Display="Dynamic"></asp:RequiredFieldValidator>
			</font> &nbsp;<span id="jstype"><font color="red"> 以.js为扩展名</font></span><span id="htmltype" style="display: none"><font color="red">以.html为扩展名</font></span>
		</td>
	</tr>
	<tr class="tdbgleft" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>参数设置</b>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>显示样式：</b>
		</td>
		<td>
			<asp:DropDownList ID="ShowType" CssClass="form-control" Width="270" runat="server">
				<asp:ListItem Value="0">DIV输出</asp:ListItem>
				<asp:ListItem Value="1">li列表</asp:ListItem>
				<asp:ListItem Value="2">普通列表</asp:ListItem>
				<asp:ListItem Value="3">表格式</asp:ListItem>
				<asp:ListItem Value="4">各项独立式</asp:ListItem>
				<asp:ListItem Value="5">智能多列式</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>作者：</b>
		</td>
		<td>
			<asp:TextBox ID="Author" runat="server" CssClass="form-control pull-left" Width="270"></asp:TextBox>
			&nbsp;<font color="#ff0000">如果不为空，则只显示指定作者的文章，用于个人文集。</font>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>文章数目：</b>
		</td>
		<td>
			<asp:TextBox ID="ArticleNum" runat="server" CssClass="form-control pull-left" Width="40px">8</asp:TextBox>
			<font color="#ff0000">&nbsp;*</font>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>所属栏目：</b>
		</td>
		<td>
			<asp:DropDownList ID="ClassID" runat="server" CssClass="form-control pull-left" Width="270">
			</asp:DropDownList>
			<asp:CheckBox ID="IncludeChild" runat="server" Text="包含子栏目" />
			&nbsp;&nbsp;<font color="red"><b>注意：</b></font>不能指定为外部栏目
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>文章属性：</b>
		</td>
		<td>
			<asp:CheckBoxList ID="ContentProperty" runat="server" RepeatDirection="Horizontal">
				<asp:ListItem Value="IsHot" Text="热门文章"></asp:ListItem>
				<asp:ListItem Value="IsElite" Text="推荐文章"></asp:ListItem>
			</asp:CheckBoxList>
			&nbsp;<font color="#ff0000">如果都不选，将显示所有文章</font>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>日期范围：</b>
		</td>
		<td>
			<span class="pull-left" style="line-height:30px;margin-right:10px;">只显示最近</span>
			<asp:TextBox ID="DateNum" runat="server" Width="50px" CssClass="form-control pull-left" Text="30"></asp:TextBox>
			<span class="pull-left" style="margin-left:10px; line-height:30px;">天内更新的文章 <font color="#ff0000">&nbsp;如果为空或0，则显示所有天数的文章</font></span>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>排序方法：</b>
		</td>
		<td>
			<asp:DropDownList ID="OrderType" runat="server" CssClass="form-control" Width="270">
				<asp:ListItem Value="GIDDesc">文章ID(降序)</asp:ListItem>
				<asp:ListItem Value="GID">文章ID(升序)</asp:ListItem>
				<asp:ListItem Value="UpdateDesc">更新时间(降序)</asp:ListItem>
				<asp:ListItem Value="Update">更新时间(升序)</asp:ListItem>
				<asp:ListItem Value="HitsDesc">点击次数(降序)</asp:ListItem>
				<asp:ListItem Value="Hits">点击次数(升序)</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>文章标题字符数：</b>
		</td>
		<td>
			<asp:DropDownList ID="ShowPropertyType" runat="server" CssClass="form-control" Width="270" Style="display: none">
				<asp:ListItem Value="None">不显示</asp:ListItem>
				<asp:ListItem Value="Symbol">符号</asp:ListItem>
				<asp:ListItem Value="css1">小图片(样式1)</asp:ListItem>
				<asp:ListItem Value="css2">小图片(样式2)</asp:ListItem>
				<asp:ListItem Value="css3">小图片(样式3)</asp:ListItem>
				<asp:ListItem Value="css4">小图片(样式4)</asp:ListItem>
				<asp:ListItem Value="css5">小图片(样式5)</asp:ListItem>
			</asp:DropDownList>
			<asp:TextBox ID="TitleLen" runat="server" Width="56px" CssClass="form-control pull-left">20</asp:TextBox>
			&nbsp;<font color="#ff0000">如果为0，则显示完整标题。字母算一个字符，汉字算两个字符。</font>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>文章副标题字符数：</b>
		</td>
		<td>
			<asp:TextBox ID="ContentLen" runat="server" Width="56px" CssClass="form-control pull-left">0</asp:TextBox>
			&nbsp;<font color="#ff0000">如果大于0，则在文章标题下方显示指定字数的文章副标题</font>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>显示内容：</b>
		</td>
		<td>
			<table width='100%' border='0' cellpadding='1' cellspacing='2'>
				<tr>
					<td>
						<asp:CheckBox ID="ShowClassName" runat="server" Text="所属栏目" />
					</td>
					<td>
						<asp:CheckBox ID="ShowIncludePic" runat="server" Text="“图文”标志" Style="display: none" />
						<asp:CheckBox ID="ShowAuthor" runat="server" Text="作者" />
					</td>
					<td>
						<span class="pull-left" style="line-height:30px;margin-right:5px;">更新时间</span>
						<asp:DropDownList ID="ShowDateType" runat="server" CssClass="form-control pull-left" Width="100">
							<asp:ListItem Value="0" Selected="True" Text="不显示"></asp:ListItem>
							<asp:ListItem Value="1" Text="年月日"></asp:ListItem>
							<asp:ListItem Value="2" Text="月日"></asp:ListItem>
							<asp:ListItem Value="3" Text="月-日"></asp:ListItem>
						</asp:DropDownList>
					</td>
					<td>
						<asp:CheckBox ID="ShowHits" runat="server" Text="点击次数" />
					</td>
				</tr>
				<tr>
					<td>
						<asp:CheckBox ID="ShowHotSign" runat="server" Text="热门文章标志" />
					</td>
					<td>
						<asp:CheckBox ID="ShowNewSign" runat="server" Text="最新文章标志" Style="display: none" />
						<asp:CheckBox ID="ShowTips" runat="server" Text="显示提示信息" />
					</td>
					<td>
						<asp:CheckBox ID="ShowCommentLink" runat="server" Text="显示评论链接" />
					</td>
					<td>
					</td>
				</tr>
			</table>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>文章打开方式：</b>
		</td>
		<td>
			<asp:DropDownList ID="OpenType" runat="server" CssClass="form-control pull-left" Width="270">
				<asp:ListItem Value="">在原窗口打开</asp:ListItem>
				<asp:ListItem Value="_blank">在新窗口打开</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>链接地址选项：</b>
		</td>
		<td>
			<asp:DropDownList ID="UrlType" runat="server" CssClass="form-control pull-left" Width="270">
				<asp:ListItem Value="Relative">使用相对路径</asp:ListItem>
				<asp:ListItem Value="Absolute">使用包含完整网址的绝对路径</asp:ListItem>
			</asp:DropDownList>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>每行标题列数：</b>
		</td>
		<td>
			<asp:TextBox ID="Cols" runat="server" Width="58px" CssClass="form-control pull-left">0</asp:TextBox>
			&nbsp;<font color="#ff0000">每行显示标题的列数</font>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>CSS风格类名：</b>
		</td>
		<td>
			<asp:TextBox ID="CssNameA" runat="server" CssClass="form-control pull-left" Width="270"></asp:TextBox>
			&nbsp;<font color="#ff0000">列表中文字链接调用的CSS类名</font>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>风格样式1：</b>
		</td>
		<td>
			<asp:TextBox ID="CssName1" runat="server" CssClass="form-control pull-left" Width="270"></asp:TextBox>
			&nbsp;<font color="#ff0000">列表中奇数行的CSS效果的类名</font>
		</td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
		<td class="tdbgleft" style="width: 20%" align="right">
			<b>风格样式2：</b>
		</td>
		<td>
			<asp:TextBox ID="CssName2" runat="server"  CssClass="form-control pull-left" Width="270"></asp:TextBox>
			&nbsp;<font color="#ff0000">列表中偶数行的CSS效果的类名</font>
		</td>
	</tr>
</table>
    <asp:HiddenField ID="JSID_Hid" runat="server" />
<center>
<asp:Button ID="Save_Btn" runat="server" Text="修改保存结果" CssClass="btn btn-primary" OnClick="Button1_Click" />
<asp:Button ID="Button2" runat="server" Text="返回" CssClass="btn btn-primary" CausesValidation="false" onclick="Button2_Click" />
</center>
</asp:Content>