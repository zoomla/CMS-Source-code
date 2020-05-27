<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MyFavoFriend.aspx.cs" Inherits="ZoomLa.WebSite.User.Content.MyFavori" EnableViewState="false" %>
<%@ Register Src="../UserZone/WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<%@ Register Src="WebUserControlMessage.ascx" TagName="WebUserControlMessage" TagPrefix="uc1" %>
<%@ Register Src="../UserFriend/WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc3" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的收藏</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">我的收藏</li> 
    </ol>
</div>
<div class="container btn_green">
    <uc2:WebUserControlTop ID="WebUserControlTop2" runat="server" />
    <uc3:WebUserControlTop ID="WebUserControlTop1" runat="server" />
    <uc1:WebUserControlMessage ID="WebUserControlMessage1" runat="server" />
</div>
<div class="container btn_green">
    <ZL:ExGridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover"
        DataKeyNames="FavoriteID" PageSize="20" OnPageIndexChanging="Egv_PageIndexChanging"
        OnRowCommand="Lnk_Click" Width="100%" CellPadding="4" GridLines="None">
        <Columns>
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSel" runat="server" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="会员形象照">
                <HeaderStyle Width="25%" />
                <ItemTemplate>
                    <a href="../../ShowList.aspx?id=<%#Eval("InfoID", "{0}")%>">
                        <asp:Image ID="Image1" runat="server" Height="68px" Width="72px" ImageUrl='<%#GetUrl(Eval("InfoID", "{0}"))%>' /></a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="会员信息">
                <HeaderStyle Width="40%" />
                <ItemTemplate>
                    <a href="../../ShowList.aspx?id=<%#Eval("InfoID", "{0}")%>">
                        <%# GetInfo(Eval("InfoID", "{0}"))%>
                    </a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="收藏时间">
                <HeaderStyle Width="20%" />
                <ItemTemplate>
                    <%# Eval("FavoriteDate", "{0}")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <PagerSettings FirstPageText="第一页" LastPageText="最后页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
    </ZL:ExGridView>
    <div style="padding-top: 5px;">
        <div class="us_seta" style="height: auto;">
            <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="CheckBox2_CheckedChanged" Text="选中本页显示的所有项目" />
            <asp:Button ID="Button2" runat="server" Text="批量删除" OnClick="btnDeleteAll_Click" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项从收藏夹删除吗？')}" CssClass="btn btn-primary" UseSubmitBehavior="true" />
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <style>
        #Egv td{vertical-align:middle;}
    </style>
</asp:Content>
