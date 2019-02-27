<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MyProduct.aspx.cs" Inherits="User_Content_MyProduct" ClientIDMode="Static" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的商品</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="content" data-ban="cnt"></div>
<div class="container margin_t10">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">我的内容</li>
    </ol>
</div>
<div class="container btn_green">
    <div class="us_pynews">
        <div class="us_seta" style="margin-bottom:10px;">
            <asp:Label ID="lblAddContent" runat="server"></asp:Label>
            <a href="Myproduct.aspx?NodeID=<%=Request["NodeID"]%>&recycle=1">回收站</a>
        </div>
    </div>
</div>
<div class="container btn_green">
    <div class="us_pynews">
        <div class="us_seta">
            <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" OnRowDataBound="EGV_RowDataBound"
                OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
                CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有商品">
                <Columns>
                    <asp:TemplateField ItemStyle-CssClass="td_s" ItemStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="ID" HeaderText="ID">
                        <HeaderStyle Width="6%" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="商品图片">
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="ShowProduct.aspx?menu=edit&ModelID=<%#Eval("ModelID") %>&NodeID=<%#Eval("Nodeid") %>&id=<%#Eval("id")%>">
                                <%#getproimg(DataBinder.Eval(Container,"DataItem.Thumbnails","{0}"))%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="商品名称">
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <a href="<%# GetUrl(Eval("ID", "{0}"))%>" target="_blank"><%# Eval("Proname")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="单位">
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# Eval("ProUnit")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="库存">
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%# Eval("stock")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="价格">
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#formatcs(DataBinder.Eval(Container, "DataItem.LinPrice", "{0}"), Eval("ProClass", "{0}"), Eval("PointVal","{0}"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="类型">
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#formatnewstype(DataBinder.Eval(Container,"DataItem.ProClass","{0}"),Eval("id","{0}"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="推荐">
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#Eval("Dengji")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="商品属性">
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#forisbest(DataBinder.Eval(Container,"DataItem.isbest","{0}"))%>
                            <%#forishot(DataBinder.Eval(Container,"DataItem.ishot","{0}"))%>
                            <%#forisnew(DataBinder.Eval(Container,"DataItem.isnew","{0}"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="销售中">
                        <HeaderStyle Width="5%" />
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <%#formattype(DataBinder.Eval(Container,"DataItem.Sales","{0}"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="状态">
                        <ItemTemplate>
                            <%# GetStatus(Eval("istrue", "{0}")) %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="已生成">
                        <ItemTemplate>
                            <%# GetCteate(Eval("MakeHtml", "{0}"))%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="<%# GetUrl(Eval("ID", "{0}"))%>" target="_blank">预览</a> |
                            <asp:LinkButton ID="Edit_Lnk" Visible="false"  runat="server" CommandName="Edit" CommandArgument='<%# Eval("ID") %>'>修改 | </asp:LinkButton>
                            <asp:LinkButton  runat="server" CommandName="Del" CommandArgument='<%# Eval("ID") %>' Visible='<%#GetIsNRe(Eval("Recycler", "{0}")) %>'
                                OnClientClick="return confirm('你确定将该数据删除到回收站吗？')"> 删除 | </asp:LinkButton>
                            <asp:LinkButton ID="ResetBtn" runat="server" CommandName="Reset" CommandArgument='<%# Eval("ID") %>' Visible='<%#GetIsRe(Eval("Recycler", "{0}")) %>'
                                OnClientClick="return confirm('你确定将该数据还原吗？')"> 还原</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle HorizontalAlign="Center" />
            </ZL:ExGridView>
            <div style="margin-top: 5px;">
                <asp:Button ID="Button2" runat="server" Text="批量删除" OnClick="btnDeleteAll_Click"
                    OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项删除吗？')}"
                    CssClass="btn btn-primary" UseSubmitBehavior="true" />
                <asp:DropDownList ID="DropDownList1" CssClass="form-control text_md" Width="150" runat="server">
                    <asp:ListItem Value="0">按标题查找</asp:ListItem>
                    <asp:ListItem Value="1">按ID查找</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox ID="TxtSearchTitle" CssClass="form-control text_md" runat="server"></asp:TextBox>
                <asp:Button ID="Btn_Search" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="Btn_Search_Click" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="HiddenField2" runat="server" />
            </div>
        </div>
    </div>
</div>
</asp:Content>
