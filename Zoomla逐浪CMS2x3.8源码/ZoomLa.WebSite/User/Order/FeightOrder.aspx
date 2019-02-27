<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeightOrder.aspx.cs" Inherits="User_PrintServer_FeightOrder" EnableViewStateMac="false" MasterPageFile="~/User/Default.master" %>
<%@ Register Src="~/User/ASCX/OrderTop.ascx" TagPrefix="ZL" TagName="OrderTop" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>航班订单管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="shop"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">航班订单管理</li>
    </ol>
</div>
<div class="container">
           <ZL:OrderTop runat="server" />
</div>
    <div class="container margin_t10">
	<div class="us_seta btn_green">
			 <asp:Panel ID="Panel1" runat="server" Width="100%">
                 <asp:Label runat="server" ID="TableTitle_L" Visible="false"></asp:Label>
                 <ZL:ExGridView ID="EGV1" runat="server" CssClass="table table-striped table-bordered table-hover" 
                     EmptyDataText="无订单数据" AllowPaging="true" AutoGenerateColumns="false" PageSize="10">
                     <Columns>
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <input name="idchk" value="<%#Eval("id") %>" type="checkbox" />
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="订单编号">
                             <ItemTemplate>
                                 <a href="OrderProList?OrderNo=<%#Eval("OrderNo") %>">
								<%#Eval("OrderNo")%></a>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="客户名称">
                             <ItemTemplate>
                                <%#Eval("Reuser") %>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="起飞时间">
                             <ItemTemplate>
                                 <%#DataBinder.Eval(Container.DataItem, "AddTime", "{0:yyyy-MM-dd}")%>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="订单金额">
                             <ItemTemplate>
                                <%#formatcc(DataBinder.Eval(Container, "DataItem.Ordersamount", "{0:N2}"))%>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="实际金额">
                             <ItemTemplate>
                                <%#formatcc(DataBinder.Eval(Container, "DataItem.Ordersamount", "{0:N2}"))%>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="操作">
                             <ItemTemplate>
                                <%#getbotton(Eval("id","{0}")) %>
                             </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>
                 </ZL:ExGridView>
                 <asp:Button ID="Dels" CssClass="btn btn-primary" runat="server" Text="批量作废" OnClick="Dels_Click" />
		</asp:Panel>
			<asp:Panel ID="Panel3" runat="server" Width="100%" Visible="false">
                <ZL:ExGridView ID="EGV3" runat="server" CssClass="table table-striped table-bordered table-hover" EmptyDataText="无相关数据" AutoGenerateColumns="false" PageSize="10">
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
             <ZL:ExGridView ID="EGV2" runat="server" CssClass="table table-striped table-bordered table-hover" EmptyDataText="无相关数据" AutoGenerateColumns="false" PageSize="10">
                 <Columns>
                     <asp:TemplateField HeaderText="航班号">
                         <ItemTemplate>
                             <%#DataBinder.Eval(Container, "DataItem.proname", "{0}")%>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="起飞">
                         <ItemTemplate>
                             <%#DataBinder.Eval(Container.DataItem, "AddTime", "{0:yyyy-MM-dd}")%>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="价格">
                         <ItemTemplate>
                             <%# DataBinder.Eval(Container, "DataItem.Shijia", "{0:N2}")%>
                         </ItemTemplate>
                     </asp:TemplateField>
                     <asp:TemplateField HeaderText="机建/燃油">
                         <ItemTemplate>
                             <%#Eval("Proinfo")%>
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
             </ZL:ExGridView>
			<br />
			<ul >
				<li style="width:45%; float: none; line-height: 24px; text-align: left;">
					保险数量：<asp:Label ID="lblIns" runat="server" Text=""></asp:Label> 份 </li>
				<li style="width:90%; float: none; line-height: 24px; text-align: left;">
					合计：<asp:Label ID="preojiage" runat="server" Text=""></asp:Label> 元 </li>
			   
			</ul>
		 </asp:Panel>
	</div>
    </div>
</asp:Content>
