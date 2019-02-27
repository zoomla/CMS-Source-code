<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="StockList.aspx.cs" Inherits="User_UserShop_StockList" ClientIDMode="Static" ValidateRequest="false" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>库存管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="store"></div> 
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="ProductList.aspx">我的店铺</a></li>
        <li class="active">库存管理</li>
    </ol>
</div>
<div class="container btn_green">
    <uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
</div>
<div class="container btn_green margin_t5">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
    OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
    CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="尚无出入库数据">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="单据类型">
            <ItemTemplate>
                <%#stocktype(DataBinder.Eval(Container,"DataItem.stocktype","{0}"))%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField HeaderText="单据编号" DataField="danju" />
        <asp:BoundField HeaderText="录入时间" DataField="addtime" DataFormatString="{0:yyyy年MM月dd日 HH:mm}" />
        <asp:BoundField HeaderText="录入者" DataField="adduser" />
        <asp:BoundField HeaderText="备注" DataField="content" />
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <a href="StockAdd.aspx?id=<%#Eval("id") %>">修改</a>
                <asp:LinkButton runat="server" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗?');">删除</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</ZL:ExGridView>
<div><asp:Button ID="BatDel_Btn" CssClass="btn btn-primary" runat="server" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" /></div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
