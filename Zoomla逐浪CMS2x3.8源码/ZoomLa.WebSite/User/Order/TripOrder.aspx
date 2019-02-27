<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TripOrder.aspx.cs" Inherits="User_PrintServer_TripOrder" EnableViewStateMac="false" MasterPageFile="~/User/Default.master" %>
<%@ Register Src="~/User/ASCX/OrderTop.ascx" TagPrefix="ZL" TagName="OrderTop" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>旅游订单管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="shop"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">旅游订单管理</li> 
    </ol>
</div>
<div class="container">
<ZL:OrderTop runat="server" />
</div>
<div class="container btn_green margin_t10">
    <div class="us_seta">
			 <asp:Panel ID="Panel1" runat="server" Width="100%">
                 <asp:Label runat="server" ID="Label1" Visible="false"></asp:Label>
			<ZL:ExGridView id="EGV1" runat="server" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" EmptyDataText="无订单数据" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" value="<%#Eval("id") %>" name="idchk" />
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="订单编号">
                <ItemTemplate>
                    <a href="OrderProList.aspx?OrderNo=<%#Eval("OrderNo") %>"> <%#Eval("OrderNo")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="客户名称">
                <ItemTemplate>
                    <%#Eval("Reuser") %>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="出发时间">
                <ItemTemplate>
                   <%#DataBinder.Eval(Container.DataItem, "AddTime", "{0:yyyy-MM-dd HH:mm}")%>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="订单金额">
                <ItemTemplate>
                   <%#formatcc(DataBinder.Eval(Container, "DataItem.Ordersamount", "{0:N2}"))%>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="实际金额">
                <ItemTemplate>
                  <%#getshijiage(Eval("id", "{0}"))%>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="需要发票">
                <ItemTemplate>
                 <%#fapiao(DataBinder.Eval(Container, "DataItem.Invoiceneeds", "{0}"))%>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="已开发票">
                <ItemTemplate>
                 <%#fapiao(DataBinder.Eval(Container, "DataItem.Developedvotes","{0}")) %>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="订单状态">
                <ItemTemplate>
                 <%#formatzt(DataBinder.Eval(Container, "DataItem.OrderStatus", "{0}"),"0")%>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="付款状态">
                <ItemTemplate>
                <%#formatzt(DataBinder.Eval(Container, "DataItem.Paymentstatus", "{0}"),"1")%>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="物流状态">
                <ItemTemplate>
                <%#formatzt(DataBinder.Eval(Container, "DataItem.StateLogistics", "{0}"),"2")%>
                </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <%#GetToPay() %>
                    <%#GetOrderStatus() %>
                </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
                 <asp:Button ID="Dels" CssClass="btn btn-primary" OnClick="Dels_Click" runat="server" Text="批量删除" />
		</asp:Panel>
		  <asp:Panel ID="Panel3" runat="server" Width="100%" Visible="false">
              <ZL:ExGridView ID="EGV3" CssClass="table table-striped table-bordered table-hover" EmptyDataText="无相关数据" PageSize="10" runat="server" AutoGenerateColumns="false">
                  <Columns>
                      <asp:TemplateField HeaderText="乘客名称">
                          <ItemTemplate>
                              <%#Eval("Name")%> <%#Eval("Name_EN")%>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="证件类型">
                          <ItemTemplate>
                             <%# GetCreType(GetCreID(Eval("CreID", "{0}"), 0))%>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="证件号码">
                          <ItemTemplate>
                             <%# GetCreID(Eval("CreID", "{0}"),1)%>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="联系电话">
                          <ItemTemplate>
                             <%#Eval("Mobile")%>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
              </ZL:ExGridView>
		 </asp:Panel>
		<asp:Panel ID="Panel2" runat="server" Width="100%" Visible="false">
		<h1 style="text-align: center">
			<asp:Label ID="Label10" runat="server" Text=""></asp:Label></h1>
              <ZL:ExGridView ID="EGV2" CssClass="table table-striped table-bordered table-hover" EmptyDataText="无相关数据" PageSize="10" runat="server" AutoGenerateColumns="false">
                  <Columns>
                      <asp:TemplateField HeaderText="旅游线路">
                          <ItemTemplate>
                             <%#DataBinder.Eval(Container, "DataItem.proname", "{0}")%>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="出发时间">
                          <ItemTemplate>
                             <%#DataBinder.Eval(Container.DataItem, "AddTime", "{0:yyyy-MM-dd}")%>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="成人价格">
                          <ItemTemplate>
                             <%# DataBinder.Eval(Container, "DataItem.Shijia", "{0:N2}")%>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="儿童数量">
                          <ItemTemplate>
                            <%#Eval("PerID")%>
                          </ItemTemplate>
                      </asp:TemplateField>
                      <asp:TemplateField HeaderText="成人数量">
                          <ItemTemplate>
                            <%#Eval("Pronum")%>
                          </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>
                </ZL:ExGridView>
		  
			<br />
			<ul >
			 <li style="width:45%; float: none; line-height: 24px; text-align: left; background-color: #F7F7F7">
					保险数量：<asp:Label ID="lblIns" runat="server" Text=""></asp:Label> 份 </li>
			  <li style="width:45%; float: none; line-height: 24px; text-align: left; background-color: #F7F7F7">
					合计：<asp:Label ID="preojiage" runat="server" Text=""></asp:Label> 元   &nbsp;&nbsp;
					<asp:LinkButton ID="lb" runat="server" Text="在线支付"></asp:LinkButton></li>			   
			</ul>
		 </asp:Panel>
	</div>
    </div>
</asp:Content>


	