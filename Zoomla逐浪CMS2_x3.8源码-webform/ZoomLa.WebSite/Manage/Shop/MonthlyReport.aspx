<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MonthlyReport.aspx.cs" Inherits="manage_Shop_MonthlyReport" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>省市报表</title>
    <style>
        #AllID_Chk{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" RowStyle-HorizontalAlign="Center" DataKeyNames="code" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" OnPageIndexChanging="EGV_PageIndexChanging" PageSize="15" class="table table-striped table-bordered table-hover" OnRowCommand="Lnk_Click" EmptyDataText="无相关数据">
    <Columns>                                                                      
        <asp:BoundField DataField="name" HeaderText="省份">
            <ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
            <HeaderStyle Width="50%" />
        </asp:BoundField>  
        <asp:TemplateField HeaderText="订单总数">
            <ItemTemplate>
                <%# GetOrderNum(Container.DataItem) %>                                
            </ItemTemplate>
            <ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="订单总额">
            <ItemTemplate>
                <%# GetOrderAmount(Container.DataItem) %>                                
            </ItemTemplate>
            <ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
        </asp:TemplateField>       
        <asp:TemplateField HeaderText="列表">                
            <ItemTemplate>
              <asp:LinkButton ID="LinkButton3" runat="server" CommandName="CheckList" CommandArgument='<%# Eval("code") %>' CssClass="option_style"><i class="fa fa-list-alt" title="订单列表"></i>订单列表</asp:LinkButton>
            </ItemTemplate>
            <ItemStyle  CssClass="tdbg" HorizontalAlign="Center" />
        </asp:TemplateField>
    </Columns>         
</ZL:ExGridView>
    <asp:Label ID="thisall" runat="server"></asp:Label>
    <asp:HiddenField ID="HdnGradeID" Value="0" runat="server" />
    <asp:HiddenField ID="HdnParentID" Value="0" runat="server" />
    <asp:HiddenField ID="HdnCateID" Value="0" runat="server" />
    <asp:HiddenField ID="HdnLastLevel" Value="0" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
