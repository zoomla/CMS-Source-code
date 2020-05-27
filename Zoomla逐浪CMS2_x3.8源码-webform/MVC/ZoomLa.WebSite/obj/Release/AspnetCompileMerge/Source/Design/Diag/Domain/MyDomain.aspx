<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyDomain.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Domain.MyDomain" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>域名管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有页面">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate><input type="checkbox" name="idchk" value="<%#Eval("ID") %>" /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="域名">
                <ItemTemplate>
                    <a href="http://<%#Eval("DomName") %>" target="_blank"><%#Eval("DomName") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="备注" DataField="Remind" />
            <asp:BoundField HeaderText="创建时间" DataField="CDate" DataFormatString="{0:yyyy年MM月dd日 hh:mm}" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="http://<%#Eval("DomName") %>" target="_blank"><i class="fa fa-eye" title="浏览"></i></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">

</asp:Content>