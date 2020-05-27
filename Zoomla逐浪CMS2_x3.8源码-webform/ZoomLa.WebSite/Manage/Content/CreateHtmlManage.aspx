<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateHtmlManage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.CreateHtmlManage" EnableViewStateMac="false"   %>
<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>VIP卡管理</title>
<style type="text/css">
.style3   {background: #e0f7e5;line-height: 120%;}
</style>
<script>
function CheckAll(spanChk)//CheckBox全选
{
var oItem = spanChk.children;
var theBox=(spanChk.type=="checkbox")?spanChk:spanChk.children.item[0];
xState=theBox.checked;
elm=theBox.form.elements;
for(i=0;i<elm.length;i++)
if(elm[i].type=="checkbox" && elm[i].id!=theBox.id)
{
	if(elm[i].checked!=xState)
	elm[i].click();
}
}
</script>
</head>
<body>
<form id="form1" runat="server">
<div class="r_navigation">
	<span>后台管理</span> &gt;&gt; 
</div>
<div class="clearbox">
</div>
<div>
</div>
  <div class="r_navigation">
 &nbsp; &nbsp;
   选择节点内容生成： <asp:DropDownList ID="DropDownList2" runat="server"  onselectedindexchanged="DropDownList2_SelectedIndexChanged">
					  </asp:DropDownList>
 
	</div>
<div class="clearbox">
</div>
<div>
</div>
<div style="text-align:center">
  
</div>
<table class="border" style="margin: 0 auto;">
  <tr align="center">
	<td width="10%" class="title"><asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
	<td width="50%" class="title">文件名</td>
	<td width="40%" class="title"> 操作</td>
  </tr>
	<asp:Repeater ID="gvCard" runat="server">       
	<ItemTemplate>
  <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
	<td height="22" align="center"><input name="Item" type="checkbox" value='<%# Eval("ID")%>'/></td>
	<td height="22" align="left"><%# Eval("Title")%></td>
	
	<td height="22" align="center"><a href="<%# Eval("url")%>"  target="_blank">查看文件</a>&nbsp;&nbsp;&nbsp;<a href="/" OnClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除文件</a></td>
  </tr>
	</ItemTemplate>
	</asp:Repeater>
	<tr class="tdbg">
	<td colspan="3" align="center" class="style3">
	共
	<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
	个商品
	<asp:Label ID="Toppage" runat="server" Text="" />
	<asp:Label ID="Nextpage" runat="server" Text="" />
	<asp:Label ID="Downpage" runat="server" Text="" />
	<asp:Label ID="Endpage" runat="server" Text="" />
	页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server"
		Text="" />页
	<asp:Label ID="pagess" runat="server" Text="" />个商品/页 转到第<asp:DropDownList ID="DropDownList1"
		runat="server" AutoPostBack="True">
	</asp:DropDownList>
	页
		  </td>
  </tr>
</table>
<div>
	<asp:Button ID="Button3" class="C_input"  runat="server"  OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');"
					Text="批量删除" onclick="Button3_Click" />
   &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; 
</div>
</form>
</body>
</html>