<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductExamine.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="manage_UserShopManage_ProductExamine" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>商品列表</title>
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
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="divline">	    
	<ul style="cursor:pointer;">
		<li><a href="ProductManage.aspx?NodeID=<%=Request.QueryString["NodeID"] %>">商品列表</a></li>
		<li><a href="ProductManage.aspx?NodeID=<%=Request.QueryString["NodeID"] %>&flag=Elite">推荐商品</a></li> 
	</ul>
</div>
<table class="table table-striped table-bordered table-hover">

  <tbody id="Tabs" >

	<tr class="tdbg">
	  <td width="34%" height="24" class="title">快速查找：
	  <asp:DropDownList ID="quicksouch" runat="server" AutoPostBack="True">
		<asp:ListItem value="1">所有商品</asp:ListItem>
		<asp:ListItem value="2">正在销售的商品</asp:ListItem>
		<asp:ListItem value="3">未销售的商品</asp:ListItem>
		<asp:ListItem value="4">正常销售的商品</asp:ListItem>
		<asp:ListItem value="5">特价处理的商品</asp:ListItem>
		<asp:ListItem value="6">所有热销的商品</asp:ListItem>
		<asp:ListItem value="7">所有新品</asp:ListItem>
		<asp:ListItem value="8">所有精品商品</asp:ListItem>
		<asp:ListItem value="9">有促销活动的商品</asp:ListItem>
		<asp:ListItem value="10">实际库存报警的商品</asp:ListItem>
		<asp:ListItem value="11">预定库存报警的商品</asp:ListItem>
		<asp:ListItem value="12">已售完的商品</asp:ListItem>
		<asp:ListItem value="13">所有批发商品</asp:ListItem>
	  </asp:DropDownList></td>
	  <td width="66%" align="center" class="title">高级查询：
	  <asp:DropDownList ID="souchtable" runat="server">
		<asp:ListItem value="0" Selected="True">请选择</asp:ListItem>
		<asp:ListItem value="1">商品名称</asp:ListItem>
		<asp:ListItem value="2">商品简介</asp:ListItem>
		<asp:ListItem value="3">商品介绍</asp:ListItem>
		<asp:ListItem value="4">厂商</asp:ListItem>
		<asp:ListItem value="5">品牌/商标</asp:ListItem>
		<asp:ListItem value="6">条形码</asp:ListItem>
	  </asp:DropDownList>
	  <asp:TextBox ID="souchkey" CssClass="form-control" runat="server" />
	  <asp:Button ID="souchok" CssClass="btn btn-primary" runat="server" Text=" 搜索 " OnClick="souchok_Click" /></td>
	</tr>
  </tbody>
</table>
<br />
<table class="table table-striped table-bordered table-hover">
  <tbody id="Tabss">
		   <tr class="tdbg">
	  <td width="3%" height="24" align="center" class="title"><asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
	  <td width="12%" align="center" class="title"><span class="tdbgleft">商品图片</span></td>
	  <td width="20%" align="center" class="title"><span class="tdbgleft">商品名称</span></td>
	  <td width="7%" align="center" class="title"><span class="tdbgleft">发布商品人</span></td>
	  <td width="7%" align="center" class="title"><span class="tdbgleft">所属店铺</span></td>
	  <td width="7%" align="center" class="title"><span class="tdbgleft">价格</span></td>
	  <td width="6%" align="center" class="title"><span class="tdbgleft">商品状态</span></td>
	  <td width="8%" align="center" class="title"><span class="tdbgleft">推荐级别</span></td>
	  <td width="8%" align="center" class="title"><span class="tdbgleft">商品属性</span></td>
	  <td width="6%" align="center" class="title"><span class="tdbgleft">销售中</span></td>
	  <td width="8%" align="center" class="title"><span class="tdbgleft">操作</span></td>
	</tr>
	<asp:Repeater ID="Productlist" runat="server">
	<ItemTemplate>
	 <tr class="tdbg">
	  <td height="24" align="center">
		  <input name="Item" type="checkbox" value='<%# Eval("id")%>'/>
		  </td>
	  <td height="24" align="center"><%#getproimg(DataBinder.Eval(Container,"DataItem.Thumbnails","{0}"))%></td>
	  <td height="24" align="center"><%#DataBinder.Eval(Container.DataItem,"ProName")%></td>
	  <td height="24" align="center"><%#GetUsername(DataBinder.Eval(Container.DataItem,"UserID").ToString())%></td>
	  <td height="24" align="center"><%#GetUserStore(DataBinder.Eval(Container.DataItem, "UserID").ToString())%></td>
	  <td height="24" align="center"><%#formatcs(DataBinder.Eval(Container,"DataItem.LinPrice","{0}"))%></td>
	  <td height="24" align="center"><%#formatnewstype(DataBinder.Eval(Container,"DataItem.ProClass","{0}"))%></td>
	  <td height="24" align="center"><%#Eval("Dengji")%></td>
	  <td height="24" align="center"><%#forisbest(DataBinder.Eval(Container,"DataItem.isbest","{0}"))%> <%#forishot(DataBinder.Eval(Container,"DataItem.ishot","{0}"))%> <%#forisnew(DataBinder.Eval(Container,"DataItem.isnew","{0}"))%></td>
	  <td height="24" align="center"><%#formattype(DataBinder.Eval(Container,"DataItem.Sales","{0}"))%></td>
	  <td height="24" align="center"><%--<a href="AddProduct.aspx?menu=edit&ModelID=<%#Eval("ModelID") %>&NodeID=<%#Eval("Nodeid") %>&id=<%#Eval("id")%>">修改</a>--%> <a href="ProductManage.aspx?menu=delete&NodeID=<%#Eval("Nodeid") %>&id=<%#Eval("id")%>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" >删除</a></td>
	</tr>
	</ItemTemplate>
	</asp:Repeater>
			 <tr class="tdbg">
	  <td height="24" colspan="12" align="center" class="tdbgleft">共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label> 个商品  <asp:Label ID="Toppage" runat="server" Text="" /> <asp:Label ID="Nextpage" runat="server" Text="" /> <asp:Label ID="Downpage" runat="server" Text="" /> <asp:Label ID="Endpage" runat="server" Text="" />  页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server" Text="" />页  <asp:Label ID="pagess" runat="server" Text="" />个商品/页  转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
		  </asp:DropDownList>页</td>
	</tr>
 </tbody>
</table> <table>
  <tr>
	<td style="height: 21px">&nbsp;
		<asp:Button ID="Button3" runat="server" Text="批量审核" CssClass="btn btn-primary" OnClick="Button3_Click1"  /></td>
  </tr>
</table>
</asp:Content>