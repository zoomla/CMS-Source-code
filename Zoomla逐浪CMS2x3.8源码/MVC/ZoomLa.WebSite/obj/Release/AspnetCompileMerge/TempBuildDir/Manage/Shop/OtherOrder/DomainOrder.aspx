<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DomainOrder.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.OtherOrder.DomainOrder" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register TagPrefix="ZL" TagName="UserGuide" Src="~/Manage/I/ASCX/UserGuide.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>域名订单管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <table class="table table-striped table-bordered table-hover">
    <tr>
      <td><span class="pull-left"> 高级查询：
        <asp:DropDownList ID="souchtable" CssClass="form-control" style="width:150" runat="server">
          <asp:ListItem Selected="True" Value="1">订单编号</asp:ListItem>
          <asp:ListItem Value="2">客户名称</asp:ListItem>
          <asp:ListItem Value="3">用户名</asp:ListItem>
          <asp:ListItem Value="5">联系地址</asp:ListItem>
        </asp:DropDownList>
        </span>
        <div class="input-group pull-left" style="width:300px;margin-left:10px;">
          <asp:TextBox runat="server" ID="souchkey" CssClass="form-control" placeholder="请输入需要搜索的内容" />
          <span class="input-group-btn">
          <asp:LinkButton runat="server" CssClass="btn btn-default" ID="souchok" OnClick="souchok_Click"><span class="fa fa-search"></span></asp:LinkButton>
          </span> </div></td>
    </tr>
  </table>
  <table class="table table-striped table-bordered table-hover">
    <tbody id="Tbody1">
      <tr class="tdbg text-center">
        <td style="width:20px;"></td>
        <td style="width:145px;"><span>订单编号</span></td>
        <td style="width:75px;"><span>客户名称</span></td>
        <td style="width:65px;"><span>用户名</span></td>
        <td style="width:150px;"><span>下单时间</span></td>
        <td style="width: 68px;"><span>实际金额</span></td>
        <td style="width:65px;"><span>收款金额</span></td>
        <td style="width:57px;"><span>已开发票</span></td>
        <td style="width:75px;"><span>订单状态</span></td>
        <td style="width:75px;"><span>付款状态</span></td>
      </tr>
      <ZL:ExRepeater ID="Order_RPT" PageSize="10" runat="server" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='9' class='text-center'>" PageEnd="</td></tr>">
        <ItemTemplate>
          <tr class="tdbg text-center" id='<%#Eval("id") %>' onmouseover="this.className='tdbgmouseover'" ondblclick="getinfo(this.id)" onmouseout="this.className='tdbg'">
            <td><%#Getclickbotton(DataBinder.Eval(Container,"DataItem.id","{0}"))%></td>
            <td><%#getorderno(Eval("id","{0}"))%></td>
            <td><%#GetUser(DataBinder.Eval(Container, "DataItem.Reuser", "{0}"))%></td>
            <td><a onclick="opentitle('../../User/Userinfo.aspx?id=<%#Eval("userId") %>','查看会员')" href="##" title="查看会员"><%#GetUsers(DataBinder.Eval(Container, "DataItem.userId", "{0}"))%></a></td>
            <td><%#Eval("AddTime") %></td>
            <td><%#getshijiage(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
            <td><%#formatcs(DataBinder.Eval(Container,"DataItem.Receivablesamount","{0:N2}"))%><%#getMoney_sign(Eval("Money_code").ToString())%></td>
            <td><%#fapiao(DataBinder.Eval(Container, "DataItem.Developedvotes","{0}")) %></td>
            <td><%#formatzt(DataBinder.Eval(Container, "DataItem.OrderStatus", "{0}"),"0")%></td>
            <td><%#formatzt(DataBinder.Eval(Container, "DataItem.Paymentstatus", "{0}"),"1")%></td>
          </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
      </ZL:ExRepeater>
      <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
        <td colspan="6" class="text-right"> 本次查询合计：<br />
          总计金额： </td>
        <td class="text-right" style="width: 68px">
            <asp:Label ID="thisall" runat="server"></asp:Label><br />
            <asp:Label ID="allall" runat="server"></asp:Label>
        </td>
        <td colspan="4">&nbsp;</td>
      </tr>
      <tr>
        <td colspan="11"> 说明：“已结清”与“已付款”的订单不允许删除,当订单号码成“灰色”代表此订单已“作废” </td>
      </tr>
      <tr>
        <td colspan="11"><asp:Button ID="Button1" Style="width: 110px" class="btn btn-primary" Text="批量删除" runat="server" OnClick="Button1_Click1" OnClientClick="if(!IsSelectByName('Item')){alert('请选择内容');return false;}else{return confirm('不可恢复性删除数据,你确定将该数据删除吗？')}" />
          <asp:Button ID="Button2" Style="width: 110px" class="btn btn-primary" Text="设为成功" runat="server" OnClick="Button2_Click" OnClientClick="if(!IsSelectByName('Item')){alert('请选择内容');return false;}else{return confirm('你确定要将订单设为成功吗？')}" /></td>
      </tr>
    </tbody>
</table>
    <div class="modal" id="userinfo_div">
        <div class="modal-dialog" style="width: 600px;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
                    <span class="modal-title"><strong id="title">用户信息</strong></span>
                </div>
                <div class="modal-body">
                      <iframe id="user_ifr" style="width:100%;height:400px;border:none;" src=""></iframe>
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
function getinfo(id) {
    location.href = 'Travel_Orderinfo.aspx?id=' + id;
}
function opentitle(url, title) {
    $("#title").text(title);
    $("#user_ifr").attr("src", url);
    $("#userinfo_div").modal({});
}
</script>
</asp:Content>