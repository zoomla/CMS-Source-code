<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Pub.aspx.cs" Inherits="User_Pub" ClientIDMode="Static" EnableViewStateMac="false" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>互动页面</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="content" data-ban="cnt"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li><a href="ManagePub.aspx">互动管理</a></li>
            <li class="active">我发布的互动</li>
        </ol>
    </div>
    <div class="container">
        <div class="margin_t10">
            <asp:TextBox ID="TxtSearchTitle" CssClass="form-control text_300" runat="server"></asp:TextBox>
            <asp:Button ID="Btn_Search" runat="server" Text="搜索" CssClass="btn btn-primary" OnClick="Btn_Search_Click" />
        </div>
        <div class="margin_t10">
            <ZL:ExGridView runat="server" ID="EGV" DataKeyNames="GeneralID" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False"
            CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!"
            OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
            <Columns>
                <asp:BoundField DataField="GeneralID" HeaderText="ID"></asp:BoundField>
                <asp:TemplateField HeaderText="标题">
                    <HeaderStyle Width="50%" />
                    <ItemTemplate>
                        <a href='ShowPubList.aspx?ID=<%# Eval("GeneralID") %>'><%# Eval("Title")%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <%#ZoomLa.BLL.B_Content.GetStatusStr(Convert.ToInt32(Eval("Status"))) %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href='ShowPubList.aspx?ID=<%#Eval("GeneralID") %>'>预览</a>
                        <a href="/Item/<%#Eval("GeneralID") %>.aspx" target="_blank">访问</a>
                        <a href="EditContent.aspx?GeneralID=<%#Eval("GeneralID") %>">修改</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
        </div>
    </div>
</asp:Content>
