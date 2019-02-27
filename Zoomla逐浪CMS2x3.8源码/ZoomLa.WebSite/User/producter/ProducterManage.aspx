<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProducterManage.aspx.cs" MasterPageFile="~/User/Empty.master" Inherits="User_producter_ProducterManage" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>注册信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol class="breadcrumb">
	<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
	<li>注册信息</li>
</ol>
  <div class="us_seta" style="margin-top: 10px;" id="manageinfos" runat="server"> &nbsp;&nbsp;<a href="ProducterManage.aspx">产品管理</a> &nbsp;&nbsp;<a href="cash.aspx">申请现金</a> &nbsp;&nbsp;<a href="CashInfo.aspx">帐户查看</a> </div>
  <div class="us_seta" style="margin-top: 10px;" id="Div1" runat="server">
    <asp:Label ID="Label2" runat="server" ></asp:Label>
    &nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label3" runat="server" ></asp:Label>
    &nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label4" runat="server" ></asp:Label>
    &nbsp;&nbsp;&nbsp;
    <asp:Label ID="Label5" runat="server" ></asp:Label>
  </div>
  <div class="us_seta" style="margin-top: 10px;" id="manageinfo" runat="server">
    <h1 style="text-align: center"> 发货信息</h1>
    <div style=" text-align:center; vertical-align:middle;">
      <table class="table table-striped table-bordered table-hover">
        <tr align="center">
          <td width="10%" class="title">定单号</td>
          <td width="35%" class="title">产品名称</td>
          <td width="8%" class="title">产品数量</td>
          <td width="8%" class="title">内部价格</td>
          <td width="8%" class="title">总价格</td>
          <td width="8%" class="title">付款</td>
          <td width="8%" class="title">状态</td>
          <td width="8%" class="title">操作</td>
        </tr>
        <asp:Repeater ID="gvCard" runat="server">
          <ItemTemplate>
            <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
              <td class="text-center"><a href="carinfo.aspx?id=<%# Eval("Orderid")%>" ><%# Eval("OrderNo")%></a></td>
              <td class="text-center"><%# Eval("Proname")%></td>
              <td class="text-center"><%# Eval("Pronum")%></td>
              <td class="text-center"><%# Eval("diiprice")%>
              <td class="text-center"><%# getAllMoney(Eval("diiprice"), Eval("Pronum"))%></td>
              <td class="text-center"><%# getpay(Eval("Paymentstatus"))%></td>
              <td class="text-center"><%# getsend(Eval("sended"))%></td>
              <td class="text-center"><a href="ProducterManage.aspx?id=<%# Eval("id")%>&menu=gogo" >发货</a></td>
            </tr>
          </ItemTemplate>
        </asp:Repeater>
        <tr class="tdbg">
          <td height="22" colspan="7" align="center" class="tdbgleft"> 共
            <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
            个商品
            <asp:Label ID="Toppage" runat="server" Text="" />
            <asp:Label ID="Nextpage" runat="server" Text="" />
            <asp:Label ID="Downpage" runat="server" Text="" />
            <asp:Label ID="Endpage" runat="server" Text="" />
            页次：
            <asp:Label ID="Nowpage" runat="server" Text="" />
            /
            <asp:Label ID="PageSize" runat="server"
            Text="" />
            页
            <asp:Label ID="pagess" runat="server" Text="" />
            个商品/页 转到第
            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"> </asp:DropDownList>
            页 </td>
        </tr>
      </table>
    </div>
  </div>
</asp:Content>
