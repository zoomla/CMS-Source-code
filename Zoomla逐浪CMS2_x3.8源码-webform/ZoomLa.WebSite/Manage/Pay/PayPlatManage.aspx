<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PayPlatManage.aspx.cs" Inherits="ZoomLa.WebSite.Manage.I.Pay.PayPlatManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>支付平台管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" AllowPaging="True" AutoGenerateColumns="False" 
        DataKeyNames="PayPlatID" PageSize="20" OnRowDataBound="Egv_RowCreated" 
        OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Lnk_Click" 
        CssClass="table table-bordered table-hover" EmptyDataText="无支付平台信息">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="PayPlatID">
                <HeaderStyle Width="5%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="名称" DataField="PayPlatName">
                <HeaderStyle Width="20%" />
            </asp:BoundField>
            <asp:BoundField HeaderText="商户ID" DataField="AccountID">
                <HeaderStyle Width="20%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="接口类型">
                <ItemTemplate>
                    <%#GetPayClass() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="默认">
                <ItemTemplate>
                    <%# GetDefault(Eval("IsDefault", "{0}")) %>
                </ItemTemplate>
                <HeaderStyle Width="5%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="启用">
                <ItemTemplate>
                    <%# GetDisabled(Eval("IsDisabled", "{0}")) %>
                </ItemTemplate>
                <HeaderStyle Width="5%" />
            </asp:TemplateField>
            <asp:BoundField DataField="OrderID" HeaderText="排序" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                <a href="AddPayPlat.aspx?ID=<%#Eval("PayPlatID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Disabled" CommandArgument='<%# Eval("PayPlatID") %>' CssClass="option_style"></asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="SetDef" CommandArgument='<%# Eval("PayPlatID") %>' CssClass="option_style"><i class="fa fa-flag" title="设为默认"></i>设为默认</asp:LinkButton>
                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="MovePre" CommandArgument='<%# Eval("PayPlatID") %>' CssClass="option_style"><i class="fa fa-arrow-up" title="上移"></i>上移</asp:LinkButton>
                <asp:LinkButton ID="LinkButton5" runat="server" CommandName="MoveNext" CommandArgument='<%# Eval("PayPlatID") %>' CssClass="option_style"><i class="fa fa-arrow-down" title="下移"></i>下移</asp:LinkButton>
                <asp:LinkButton ID="LinkButton6" runat="server" CommandName="Delete" CommandArgument='<%# Eval("PayPlatID") %>' OnClientClick="return confirm('确定删除?')" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
.allchk_l { display:none; }
</style>
</asp:Content>
