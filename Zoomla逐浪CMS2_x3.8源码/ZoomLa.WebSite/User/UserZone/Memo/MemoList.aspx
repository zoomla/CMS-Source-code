<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MemoList.aspx.cs" Inherits="MemoList" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的备忘录</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="zone"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
            <li class="active">我的备忘录<a href="EditMemo.aspx">[添加备忘录]</a></li>
            <div class="clearfix"></div>
        </ol>
    </div>
    <div class="container btn_green">
        <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" OnRowCommand="Egv_RowCommand" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无相关信息！！">
            <Columns>
                <asp:TemplateField HeaderText="标题">
                    <ItemTemplate>
                        <a href='<%#"memocontext.aspx?ID="+ DataBinder.Eval(Container.DataItem,"ID") %>'>
                            <%#DataBinder.Eval(Container.DataItem,"MemoTitle") %>
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="时间">
                    <ItemTemplate>
                        <%#DateTime.Parse(DataBinder.Eval(Container.DataItem,"MemoTime").ToString()).ToShortDateString() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href='<%#"EditMemo.aspx?ID="+ DataBinder.Eval(Container.DataItem,"ID") %>' class="option_style" title="修改"><i class="fa fa-pencil"></i></a>
                        <asp:LinkButton ID="LinkButton1" CommandName="del" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗？');" runat="server" CssClass="option_style" title="删除"><i class="fa fa-trash"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <RowStyle HorizontalAlign="Center" />
        </ZL:ExGridView>
    </div>
</asp:Content>
