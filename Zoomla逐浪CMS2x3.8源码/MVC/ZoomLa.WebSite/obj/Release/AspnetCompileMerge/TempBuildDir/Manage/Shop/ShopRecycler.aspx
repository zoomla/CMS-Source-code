<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopRecycler.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.ShopRecycler" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>商品回收站</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="id" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据！！">
        <Columns>
            <asp:TemplateField>
                <HeaderStyle Width="3%" />
                <ItemTemplate>
                    <input name="idchk" type="checkbox" value='<%# Eval("id")%>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" HeaderStyle-Width="3%" DataField="ID" />
            <asp:TemplateField HeaderText="商品图片">
                <HeaderStyle Width="12%" />
                <ItemTemplate>
                    <a href="AddProduct.aspx?menu=edit&ModelID=<%#Eval("ModelID") %>&NodeID=<%#Eval("Nodeid") %>&id=<%#Eval("id")%>"><img src="<%#getproimg() %>" style="width:60px;height:60px;" /></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="商品名称">
                <HeaderStyle Width="22%" />
                <ItemTemplate>
                    <a href="AddProduct.aspx?menu=edit&ModelID=<%#Eval("ModelID") %>&NodeID=<%#Eval("Nodeid") %>&id=<%#Eval("id")%>"> <%#(Eval("Priority", "{0}") == "1") && (Convert.ToInt32(Eval("Propeid","{0}")) > 0) ? "<font color=maroon>[绑]</font>  " : ""%><%#Eval("proname")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="单位" >
                <HeaderStyle Width="7%" />
                <ItemTemplate>
                    <%#Eval("ProUnit")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="库存">
                <HeaderStyle Width="7%" />
                <ItemTemplate>
                    <%#Stockshow(DataBinder.Eval(Container,"DataItem.id","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="价格">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <%#formatcs(DataBinder.Eval(Container,"DataItem.LinPrice","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="类型">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <%#formatnewstype(DataBinder.Eval(Container,"DataItem.ProClass","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="推荐" HeaderStyle-Width="5%" DataField="Dengji" />
            <asp:TemplateField HeaderText="商品属性">
                <HeaderStyle Width="8%" />
                <ItemTemplate>
                    <%#forisbest(DataBinder.Eval(Container,"DataItem.isbest","{0}"))%>
                    <%#forishot(DataBinder.Eval(Container,"DataItem.ishot","{0}"))%>
                    <%#forisnew(DataBinder.Eval(Container,"DataItem.isnew","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="销售中">
                <HeaderStyle Width="6%" />
                <ItemTemplate>
                    <%#formattype(DataBinder.Eval(Container,"DataItem.Sales","{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <HeaderStyle Width="7%" />
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" CommandName="ReStore" CommandArgument='<%# Eval("ID") %>' runat="server" CssClass="option_style"><i class="fa fa-recycle" title="还原"></i>还原</asp:LinkButton>
                    <asp:LinkButton ID="LinkButton2" CommandName="Del1" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" runat="server" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div class="btn-group">
        <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="批量还原" OnClick="Button1_Click" />
        <asp:Button ID="Button2" class="btn btn-primary" runat="server" Text="批量删除" OnClick="Button2_Click" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('不可恢复性删除数据,你确定将该数据删除吗？')}" />
        <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="清空回收站" OnClick="Button4_Click" OnClientClick="return confirm('你确定要清空回收站吗')" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
        <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        function IsSelectedId() {
            var checkArr = $("[name=idchk]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
    </script>
</asp:Content>