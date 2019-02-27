<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LocationReport.aspx.cs" Inherits="manage_Shop_LocationReport" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>省市报表</title>
    <style>
        #AllID_Chk{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" DataKeyNames="code" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="15" class="table table-striped table-bordered table-hover"
        OnRowDataBound="Gdv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Lnk_Click" OnRowEditing="Gdv_Editing" EmptyDataText="无相关数据">
        <Columns>
            <asp:BoundField DataField="name" HeaderText="省份">
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
                <HeaderStyle Width="50%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="订单总数">
                <ItemTemplate>
                    <%# GetOrderNum(Container.DataItem) %>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="订单总额">
                <ItemTemplate>
                    <%# GetOrderAmount(Container.DataItem) %>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="列表">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="DicList" CommandArgument='<%#Eval("code") %>' CssClass="option_style"><i class="fa fa-list-alt" title="地区列表"></i>地区列表</asp:LinkButton>
                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="CheckList" CommandArgument='<%# Eval("name") %>' CssClass="option_style"><i class="fa fa-list-alt" title="订单列表"></i>订单列表</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button ID="Button1" runat="server" Text="返 回" OnClick="btnBack_Click" class="btn btn-primary" />
    <asp:HiddenField ID="HdnGradeID" Value="0" runat="server" />
    <asp:HiddenField ID="HdnParentID" Value="0" runat="server" />
    <asp:HiddenField ID="HdnCateID" Value="0" runat="server" />
    <asp:HiddenField ID="HdnLastLevel" Value="0" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>