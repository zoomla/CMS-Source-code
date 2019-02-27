<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DeliverList.aspx.cs" Inherits="Zoomla.Website.manage.Shop.DeliverList" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>发退货明细</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
	<tr class="tdbg">
	  <td width="72" height="24" align="center" class="title"><span>日期</span></td>
	  <td width="35" align="center" class="title">方向</td>
	  <td width="86" align="center" class="title">客户名称</td>
	  <td width="68" align="center" class="title"><span>用户名</span></td>
	  <td width="77" align="center" class="title">收货人姓名</td>
	  <td width="102" align="center" class="title"><span>订单编号</span></td>
	  <td width="96" align="center" class="title"><span>快递公司</span></td>
	  <td width="50" align="center" class="title"><span>操作人</span></td>
	  <td width="51" align="center" class="title"><span>经手人</span></td>
	  <td width="49" align="center" class="title"><span>已签收</span></td>
	  <td width="52" align="center" class="title"><span>操作</span></td>
	</tr>
	<tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
	  <td height="24" align="center">&nbsp;&nbsp;</td>
	  <td height="24" align="center">&nbsp;</td>
	  <td height="24" align="center">&nbsp;</td>
	  <td height="24" align="center">&nbsp;</td>
	  <td height="24" align="right">&nbsp;</td>
	  <td height="24" align="right" >&nbsp;</td>
	  <td height="24" align="center">&nbsp;</td>
	  <td height="24" align="center">&nbsp;</td>
	  <td height="24" align="center">&nbsp;</td>
	  <td height="24" align="center">&nbsp;</td>
	  <td height="24" align="center"><i class="fa fa-eye" title="查看"></i></td>
	</tr>        
	<tr class="tdbg">
	  <td height="24" colspan="12" align="center" class="tdbgleft">共 1 条记录 首页 上一页 下一页 尾页  页次：1/1页  条记录/页 转到第 1页</td>
	</tr>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>