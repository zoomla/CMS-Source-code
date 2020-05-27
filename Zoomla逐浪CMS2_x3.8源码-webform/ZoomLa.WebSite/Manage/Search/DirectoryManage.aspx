<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DirectoryManage.aspx.cs" Inherits="manage_Search_DirectoryManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>定义全文检索</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
<tr align="left">
<td>欢迎您使用<%=SiteName %>系统全文检索功能模块；本系统将帮助您为数据库创建全文索引目录。使用本功能您可以：
<ul><li>创建待索引的表</li><li>选择待索引的列</li><li>在现有目录中添加数据库表</li></ul>
</td>
</tr>
</table>
<table class="table table-striped table-bordered table-hover">
  <tr class="title" align="center">
	<td width="5%" class="title"></td>
	<td width="20%" class="title">检索名称</td>
	<td width="15%" >表名</td>
	<td width="15%" class="title">列</td>
	<td width="20%" class="title">全文目录名</td>
	<td width="10%" class="title"> 创建时间</td>
	<td width="15%" class="title"> 操作</td>
  </tr>
	<ZL:ExRepeater ID="gvCard" runat="server" PagePre="<tr><td colspan='7' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">       
		<ItemTemplate>
		  <tr>
			<td height="22" align="center"></td>
			<td height="22" align="center"></td>
			 <td height="22" align="center"></td>
			<td height="22" align="left"></td>
			<td height="22" align="center"></td>
			<td height="22" align="center"></td>
			<td height="22" align="center">添加 修改 删除</td>
		  </tr>
		</ItemTemplate>
        <FooterTemplate></FooterTemplate>
	</ZL:ExRepeater>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
	function CheckAll(spanChk)//CheckBox全选
	{
		var oItem = spanChk.children;
		var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
		xState = theBox.checked;
		elm = theBox.form.elements;
		for (i = 0; i < elm.length; i++)
			if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
				if (elm[i].checked != xState)
					elm[i].click();
			}
	}
</script>
</asp:Content>