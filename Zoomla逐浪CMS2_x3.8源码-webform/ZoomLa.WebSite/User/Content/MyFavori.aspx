<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MyFavori.aspx.cs" Inherits="ZoomLa.WebSite.User.Content.MyFavori" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>内容收藏</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="group" data-ban="fav"></div>
    <div class="container margin_t10">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li class="active">我的收藏</li>
        </ol>
    </div>
    <div class="container u_cnt btn_green">
        <ZL:ExGridView ID="Egv" runat="server" CssClass="table table-striped table-bordered table-hover" EmptyDataText="您没有任何收藏" AllowPaging="true" OnPageIndexChanging="Egv_PageIndexChanging" AutoGenerateColumns="False" DataKeyNames="FavoriteID" OnRowCommand="Egv_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" value="<%#Eval("FavoriteID") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标题" ItemStyle-CssClass="td_lg">
                    <ItemTemplate>
                        <a href="<%#Eval("FavUrl")%>" target="_blank"><%#GetTitle() %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="收藏类型">
                    <ItemTemplate>
                        <%#ZoomLa.BLL.B_Favorite.GetFavType(ZoomLa.SQLDAL.DataConvert.CLng(Eval("FavoriType"))) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="收藏时间" DataField="FavoriteDate" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="Del" CommandArgument='<%# Eval("FavoriteID") %>' OnClientClick="return confirm('你确定将该数据从收藏夹删除吗？')">移出收藏夹</asp:LinkButton>
                        <%--<a href='MyFavori.aspx?type=1&method=Del&ID=<%# Eval("FavoriteID") %>'>移除</a>--%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
        <asp:Button ID="BtnDel_B" runat="server" Text="批量删除" OnClick="BtnDel_B_Click" OnClientClick="return confirm('你确定要将所有选择项从收藏夹删除吗？')" CssClass="btn btn-primary" UseSubmitBehavior="true" />
    </div>
</asp:Content>
