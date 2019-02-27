<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="carinfo.aspx.cs" Inherits="User_Info_carinfo" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
  <title>订单提交</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <table class="table table-striped table-bordered table-hover">
    <tbody id="Tbody1">
      <tr>
        <td colspan="5" align="center" class="title"><asp:Label ID="Label1" runat="server" Text=""></asp:Label>
          &nbsp;</td>
      </tr>
      <tr>
        <td align="left" style="width: 13%; height: 23px">客户名称：
          <asp:Label ID="Reuser" runat="server" Text=""></asp:Label></td>
        <td align="left" style="width: 14%; height: 23px">用 户 名：
          <asp:Label ID="Rename" runat="server" Text=""></asp:Label></td>
        <td align="left" style="width: 13%; height: 23px">购买日期：
          <asp:Label ID="adddate" runat="server"></asp:Label></td>
        <td width="20%" align="left" style="height: 23px">下单日期：
          <asp:Label ID="addtime" runat="server"></asp:Label></td>
      </tr>
      <tr>
        <td align="left" style="width: 13%; height: 12px">需开发票：
          <asp:Label ID="Invoiceneeds" runat="server"></asp:Label></td>
        <td align="left" style="width: 14%; height: 12px">已开发票：
          <asp:Label ID="Developedvotes" runat="server"></asp:Label></td>
        <td align="left" style="width: 13%; height: 12px">付款状态：
          <asp:Label ID="Paymentstatus" runat="server"></asp:Label></td>
        <td width="20%" align="left" style="height: 12px">物流状态：
          <asp:Label ID="StateLogistics" runat="server"></asp:Label></td>
      </tr>
    </tbody>
  </table>
  <br />
  <table class="table table-striped table-bordered table-hover">
    <tbody id="Tbody3">
      <tr>
        <td width="50%" align="center"><table class="table table-striped table-bordered table-hover">
            <tr>
              <td width="28%" align="right">收货人姓名：</td>
              <td width="72%" align="left">&nbsp;
                <asp:Label ID="Reusers" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right">收货人地址：</td>
              <td align="left">&nbsp;
                <asp:Label ID="Jiedao" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right">收货人邮箱：</td>
              <td align="left">&nbsp;
                <asp:Label ID="Email" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right" >付款方式：</td>
              <td align="left" >&nbsp;
                <asp:Label ID="Payment" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right">发票信息：</td>
              <td align="left">&nbsp;
                <asp:Label ID="Invoice" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right">缺货处理：</td>
              <td align="left">&nbsp;
                <asp:Label ID="Outstock" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right">订单类型：</td>
              <td align="left">&nbsp;
                <asp:Label ID="Ordertype" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right" >要求送货时间：</td>
              <td align="left" >&nbsp;
                <asp:Label ID="Deliverytime" runat="server" Text=""></asp:Label></td>
            </tr>
          </table></td>
        <td width="50%" align="center"><table width="100%" border="0" cellspacing="1" cellpadding="0">
            <tr>
              <td width="28%" align="right">联系电话：</td>
              <td width="72%" align="left">&nbsp;
                <asp:Label ID="Phone" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right" >邮政编码：</td>
              <td align="left" >&nbsp;
                <asp:Label ID="ZipCode" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right">收货人手机：</td>
              <td align="left">&nbsp;
                <asp:Label ID="Mobile" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right">送货方式：</td>
              <td align="left">&nbsp;
                <asp:Label ID="Delivery" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right">跟单员：</td>
              <td align="left">&nbsp;
                <asp:Label ID="AddUser" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right">内部记录：</td>
              <td align="left">&nbsp;
                <asp:Label ID="Internalrecords" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right">备注/留言：</td>
              <td align="left">&nbsp;
                <asp:Label ID="Ordermessage" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
              <td align="right">订单状态：&nbsp;</td>
              <td align="left">&nbsp;
                <asp:Label ID="OrderStatus" runat="server"></asp:Label></td>
            </tr>
          </table></td>
      </tr>
    </tbody>
  </table>
  <br />
  <table class="table table-striped table-bordered table-hover">
    <tbody id="Tbody2">
      <tr>
        <td width="10%" align="center" class="title">图片</td>
        <td width="18%" align="center" class="title">商品名称</td>
        <td width="6%" align="center" class="title">单位</td>
        <td width="6%" align="center" class="title">数量</td>
        <td width="6%" align="center" class="title">市场价</td>
        <td width="6%" align="center" class="title">实价</td>
        <td width="6%" align="center" class="title">指定价</td>
        <td width="6%" align="center" class="title">金额</td>
        <td width="8%" align="center" class="title">服务期限</td>
        <td width="12%" align="center" class="title">备注</td>
      </tr>
      <asp:Repeater ID="procart" runat="server" OnItemDataBound="cartinfo_ItemDataBound">
        <ItemTemplate>
          <tr>
            <td align="center"><%#getproimg(DataBinder.Eval(Container, "DataItem.ProID", "{0}"))%></td>
            <td align="center"><%#Getprotype(Eval("ProID","{0}"))%> <%#Eval("proname")%></td>
            <td width="6%" align="center"><%#Eval("Danwei") %></td>
            <td width="6%" align="center"><%#Eval("pronum") %></td>
            <td width="6%" align="center"><%#getjiage("1", DataBinder.Eval(Container, "DataItem.ProID", "{0}"))%></td>
            <td width="6%" align="center"><%#getshichangjiage(DataBinder.Eval(Container, "DataItem.ProID", "{0}"))%></td>
            <td width="6%" align="center"><%#shijia(DataBinder.Eval(Container, "DataItem.Shijia", "{0}"))%></td>
            <td width="6%" align="center"><%#getprojia(DataBinder.Eval(Container, "DataItem.ID", "{0}"))%></td>
            <td width="8%" align="center"><%#qixian(DataBinder.Eval(Container, "DataItem.ProID", "{0}"))%></td>
            <td align="center"><%#beizhu(DataBinder.Eval(Container, "DataItem.ProID", "{0}"))%></td>
          </tr>
          <asp:Repeater ID="Repeater1" runat="server">
            <ItemTemplate>
              <tr>
                <td align="center"><%#getproimg(Eval("id","{0}"))%></td>
                <td align="center"><%#Getprotype(DataBinder.Eval(Container, "DataItem.id", "{0}"))%><%#Eval("proname")%></td>
                <td width="6%" align="center"><%#getProUnit(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
                <td width="6%" align="center">1</td>
                <td width="6%" align="center"><%#getjiage("1", DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
                <td width="6%" align="center"><%#getjiage("2", DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
                <td width="6%" align="center">-</td>
                <td width="6%" align="center">-</td>
                <td width="8%" align="center">-</td>
                <td align="center">-</td>
              </tr>
            </ItemTemplate>
          </asp:Repeater>
        </ItemTemplate>
      </asp:Repeater>
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td align="center">合计：</td>
        <td align="left">&nbsp;
          <asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
      </tr>
      <tr>
        <td colspan="10" >&nbsp;运费：
          <asp:Label ID="Label32" runat="server" Text=""></asp:Label>
          元 </td>
      </tr>
      <tr>
        <td colspan="10"><table class="table table-bordered">
            <tr>
              <td width="50%" align="left" style="height: 19px">&nbsp;实际金额：
                <asp:Label ID="Label29" runat="server" Text=""></asp:Label>
                +
                <asp:Label ID="Label30" runat="server" Text=""></asp:Label>
                ＝
                <asp:Label ID="Label31"
						  runat="server" Text=""></asp:Label>
                元</td>
              <td width="50%" align="right" style="height: 19px">&nbsp;已付款：
                <asp:Label ID="Label28" runat="server" Text=""></asp:Label>
                元 </td>
            </tr>
          </table></td>
      </tr>
      <tr>
        <td colspan="10"><font color="red">
          <asp:Label ID="Label33" runat="server" Text=""></asp:Label>
          </font></td>
      </tr>
    </tbody>
  </table>
  <br />
  <!--endprint-->
  <table class="table table-striped table-bordered table-hover" id="TABLE1">
    <tbody id="Tbody4">
      <asp:Repeater ID="procart2" runat="server">
        <ItemTemplate></ItemTemplate>
      </asp:Repeater>
      <tr> </tr>
      <tr> </tr>
    </tbody>
  </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
  <script language="javascript"> 
	function preview()
	{
		var ido=<%=Request.QueryString["id"]%>;
		window.open('Orderlistinfo.aspx?id='+ido+'&menu=print','打印预览','','');
	}

	function pageload()
	{
<%
	if (Request.QueryString["menu"] == "print")
	{
 %>
	bdhtml=window.document.body.innerHTML;
	sprnstr="<!--startprint-->";
	eprnstr="<!--endprint-->";
	prnhtml=bdhtml.substr(bdhtml.indexOf(sprnstr)+17);
	prnhtml=prnhtml.substring(0,prnhtml.indexOf(eprnstr));
	window.document.body.innerHTML=prnhtml;
	window.print();
 <%
 } 
 %>
}
</script>
  <style type="text/css">
@media print {
.Noprn { display: none; }
}
</style>
</asp:Content>