<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FuncSearch.aspx.cs"  MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLaCMS.Manage.Config.FuncSearch" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>搜索结果</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="False" AllowPaging="true" PageSize="20" OnPageIndexChanging="EGV_PageIndexChanging"
            CssClass="table table-striped table-bordered table-hover" IsHoldState="false" OnRowDataBound="EGV_RowDataBound" >
            <Columns>
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <input  type="checkbox" name="chkSel" title="" value='<%#Eval("id") %>' />
                        <asp:HiddenField ID="hfId" runat="server" Value='<%# Eval("id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="名称" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <a href='<%#Eval("id","AddSearch.aspx?menu=edit&id={0}") %>' title="点击编辑导航"><%#Eval("Name") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblState" runat="server" Text='<%# Eval("state") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="文件或地址路径">
                    <ItemTemplate>
             <%--           <asp:HyperLink ID="hlLink" runat="server" NavigateUrl='<%#Eval("FlieUrl") %>' Text='<%#Eval("FlieUrl") %>' ToolTip='<%# Eval("Name") %>' Visible="false" />--%>
                        <a href="<%# Eval("FileUrl") %>" title="<%# Eval("Name") %>"><%#Eval("FileUrl") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="图标地址">
                    <ItemTemplate>
                        <span class="font_red"><i class="<%# Eval("ico") %>"></i></span>：<asp:Label ID="lblpic" runat="server" Text='<%# Eval("ico") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="手动排序">
                     <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <input type="number" min="1" class="text_x text-center" name="order_T" value="<%#Eval("OrderID") %>" />
                        <input type="hidden" name="order_Hid" value="<%#Eval("ID")+":"+Eval("OrderID")+":"+Eval("OrderID") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="支持移动" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                      <ItemTemplate>
                          <%#IsMobile(Eval("Mobile")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="连接类型" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="linkType" runat="server"></asp:Label> 
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate>
                        <asp:Label ID="lblTime" runat="server" Text='<%# getDate(Eval("time","{0}"))%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
</asp:Content>

