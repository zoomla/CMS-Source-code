<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Flight_Order_Management.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.OtherOrder.Flight_Order_Management" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>航班订单管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-striped table-bordered table-hover">
    <tr>
      <td><span class="pull-left"> 快速查找：
        <asp:DropDownList ID="quicksouch" CssClass="form-control" Width="200" runat="server" AutoPostBack="True">
          <asp:ListItem Selected="True">请选择查找方式</asp:ListItem>
          <asp:ListItem Value="1">我负责跟踪的订单</asp:ListItem>
          <asp:ListItem Value="2">今天的新订单</asp:ListItem>
          <asp:ListItem Value="3">所有订单</asp:ListItem>
          <asp:ListItem Value="4">最近10天内的新订单</asp:ListItem>
          <asp:ListItem Value="5">最近一个月内的新订单</asp:ListItem>
        </asp:DropDownList>
        </span > <span class="pull-left" style="margin-left:10px;margin-right:10px;"> 高级查询：
        <asp:DropDownList CssClass="form-control" Width="150" ID="souchtable" runat="server">
          <asp:ListItem Selected="True" Value="1">订单编号</asp:ListItem>
          <asp:ListItem Value="2">客户名称</asp:ListItem>
          <asp:ListItem Value="3">用户名</asp:ListItem>
          <asp:ListItem Value="5">联系地址</asp:ListItem>
        </asp:DropDownList>
        </span>
        <div class="input-group pull-left" style="width:300px;">
          <asp:TextBox runat="server" ID="souchkey" class="form-control" placeholder="请输入需要搜索的内容" />
          <span class="input-group-btn">
          <asp:LinkButton runat="server" CssClass="btn btn-default" ID="souchok" OnClick="souchok_Click"><span class="fa fa-search"></span></asp:LinkButton>
          </span> </div></td>
    </tr>
  </table>
  <table id="EGV" class="table table-striped table-bordered table-hover">
    <tr>
      <td><asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
      <td>订单编号</td>
      <td>客户名称</td>
      <td>用户名</td>
      <td>起飞时间</td>
      <td>实际金额</td>
      <td>收款金额</td>
      <td>已开发票</td>
      <td>订单状态</td>
      <td>付款状态</td>
      <td>其他费用</td>
    </tr>
    <asp:Repeater ID="Orderlisttable" runat="server">
      <ItemTemplate>
        <tr class="tdbg" id="<%#Eval("id") %>" ondblclick="getinfo(this.id)">
          <td><%#Getclickbotton(DataBinder.Eval(Container,"DataItem.id","{0}"))%></td>
          <td><%#getorderno(Eval("id","{0}"))%></td>
          <td><%#GetUser(DataBinder.Eval(Container, "DataItem.Reuser", "{0}"))%></td>
          <td><a onclick="opentitle('../../User/Userinfo.aspx?id=<%#Eval("userId") %>','查看会员')" href="javascript:;" title="查看会员"><%#GetUsers(DataBinder.Eval(Container, "DataItem.userId", "{0}"))%></a></td>
          <td><%#Eval("AddTime") %></td>
          <td><%#getshijiage(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
          <td><%#formatcs(DataBinder.Eval(Container,"DataItem.Receivablesamount","{0:N2}"))%><%#getMoney_sign(Eval("Money_code").ToString())%></td>
          <td><%#fapiao(DataBinder.Eval(Container, "DataItem.Developedvotes","{0}")) %></td>
          <td><%#formatzt(DataBinder.Eval(Container, "DataItem.OrderStatus", "{0}"),"0")%></td>
          <td><%#formatzt(DataBinder.Eval(Container, "DataItem.Paymentstatus", "{0}"),"1")%></td>
          <td><a href="###" onclick='javascript:window.open("/manage/Common/Extra.aspx?OrderID=<%#Eval("id")%>","","width=600,height=450,resizable=0,scrollbars=yes");'>其他费用</a></td>
        </tr>
      </ItemTemplate>
    </asp:Repeater>
    <tr>
      <td colspan="6">本次查询合计：<br />
        总计金额： </td>
      <td><asp:Label ID="thisall" runat="server"></asp:Label>
        <br />
        <asp:Label ID="allall" runat="server"></asp:Label></td>
      <td>&nbsp;</td>
    </tr>
    <tr class="tdbg">
      <td colspan="11"><span style="text-align: center">共
        <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
        条数据
        <asp:Label ID="Toppage" runat="server" Text="" />
        <asp:Label ID="Nextpage" runat="server" Text="" />
        <asp:Label ID="Downpage" runat="server" Text="" />
        <asp:Label ID="Endpage" runat="server" Text="" />
        页次：
        <asp:Label ID="Nowpage" runat="server" Text="" />
        /
        <asp:Label ID="PageSize" runat="server" Text="" />
        页
        <asp:Label ID="pagess" runat="server" Text="" />
        <asp:TextBox ID="txtPage" runat="server" AutoPostBack="true" OnTextChanged="txtPage_TextChanged"></asp:TextBox>
        条数据/页 转到第
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"></asp:DropDownList>
        页
        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtPage"
                        ErrorMessage="只能输入数字" Type="Integer" MaximumValue="100000" MinimumValue="0"></asp:RangeValidator>
        </span></td>
    </tr>
    <tr>
      <td colspan="11">说明：“已结清”与“已付款”的订单不允许删除,当订单号码成“灰色”代表此订单已“作废” </td>
    </tr>
    <tr>
      <td colspan="11"><asp:Button ID="Button1" class="btn btn-primary" Text="删除选中的订单" runat="server"  OnClick="Button1_Click1" OnClientClick="if(!IsSelectedId()){alert('请选择内容');return false;}else{return confirm('不可恢复性删除数据,你确定将该数据删除吗？')}" /></td>
    </tr>
  </table>
    <div class="modal" id="userinfo_div">
        <div class="modal-dialog" style="width: 1024px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong id="title">用户信息</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="user_ifr" style="width:100%;height:600px;border:none;" src=""></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript" src="/js/Drag.js"></script>
<script type="text/javascript" src="/js/Dialog.js"></script>
<script>
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

function getinfo(id) {
	location.href = 'Flight_OrderInfo.aspx?id=' + id;
}

function opentitle(url, title) {
	$("#title").text(title);
	$("#user_ifr").attr("src", url);
	$("#userinfo_div").modal({});
}
</script>
</asp:Content>