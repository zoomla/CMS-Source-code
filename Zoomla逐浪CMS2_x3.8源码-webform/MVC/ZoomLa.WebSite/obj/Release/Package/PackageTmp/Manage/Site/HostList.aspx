<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HostList.aspx.cs" Inherits="ZoomLaCMS.Manage.Site.HostList" MasterPageFile="~/Manage/I/Default.Master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>主机管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
    OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
    CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
    <Columns>
        <asp:TemplateField HeaderText="站点名称"><ItemTemplate>
            <a href="HostAdd.aspx?name=<%#HttpUtility.UrlEncode(Eval("Name","")) %>"><%#Eval("SiteInfo") %></a>
                                             </ItemTemplate></asp:TemplateField>
        <asp:BoundField HeaderText="FTP用户名" DataField="Name" ItemStyle-CssClass="td_l" />
        <asp:BoundField HeaderText="FTP密码" DataField="UserPwd" ItemStyle-CssClass="td_l" />
        <asp:BoundField DataField="CDate" DataFormatString="{0:yyyy年MM月dd日}" HeaderText="创建时间" ItemStyle-CssClass="td_l" />
        <asp:BoundField DataField="EndDate" DataFormatString="{0:yyyy年MM月dd日}" HeaderText="到期时间" ItemStyle-CssClass="td_l" />
        <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="td_m">
            <ItemTemplate>
                <a href="HostAdd.aspx?name=<%#HttpUtility.UrlEncode(Eval("Name","")) %>"><i class="fa fa-pencil"></i></a>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">.allchk_l{display:none;}</style>
</asp:Content>