<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IDCOrderList.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.IDC.IDCOrderList"  MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>IDC订单列表</title><style>.allchk_l{display:none;}</style></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView runat="server" ID="EGV" PageSize="10" AutoGenerateColumns="false" AllowPaging="true" CssClass="table table-bordered table-hover" OnPageIndexChanging="EGV_PageIndexChanging" >
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <a href="javascript:;" onclick="parent.opentitle('IDCOrderInfo.aspx?ID=<%#Eval("ID") %>','订单详情');" title="订单详情"><%#Eval("ID") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="绑定域名">
                <ItemTemplate>
                    <a href="<%#"http://"+Eval("Domain") %>" title="访问" target="_blank"><%#Eval("Domain") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="会员">
                <ItemTemplate>
                    <a href="javascript:;" onclick="parent.opentitle('../../User/Userinfo.aspx?id=<%#Eval("Userid") %>','查看会员')" title="查看会员" ><%#GetUsers(Eval("UserID",""))%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="商品名称">
                <ItemTemplate>
                    <a href="javascript:;" onclick="parent.opentitle('../ShowProduct.aspx?id=<%#Eval("ProID") %>','商品信息');" title="点击浏览商品详情"><%#Eval("Proname") %></a> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="生效时间" DataField="STime" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField HeaderText="到期时间" DataField="ETime" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:TemplateField HeaderText="是否到期">
                <ItemTemplate><%#IsExpire(Eval("ETime")) %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%#formatzt(Eval("OrderStatus",""),"0")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="财务">
                <ItemTemplate>
                    <%#formatzt(Eval("Paymentstatus",""),"1")%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>
