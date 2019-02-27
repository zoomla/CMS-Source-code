<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DataList.aspx.cs" Inherits="ZoomLaCMS.Manage.Sentiment.DataList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>数据列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs">
        <li><a href="<%:"DataList.aspx?Skey="+HttpUtility.UrlEncode(Skey)%>">全部</a></li>
        <li><a href="<%="DataList.aspx?Skey="+HttpUtility.UrlEncode(Skey)+"&Source="+HttpUtility.UrlEncode("新闻") %>">新闻</a></li>
        <li><a href="<%="DataList.aspx?Skey="+HttpUtility.UrlEncode(Skey)+"&Source="+HttpUtility.UrlEncode("微信") %>">微信</a></li>
        <li><a href="<%="DataList.aspx?Skey="+HttpUtility.UrlEncode(Skey)+"&Source="+HttpUtility.UrlEncode("微博") %>">微博</a></li>
    </ul>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" PageSize="10" OnRowCommand="EGV_RowCommand"
        OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
        <Columns>
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <%#GetTitle() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="来源" DataField="Source" />
            <asp:BoundField HeaderText="作者" DataField="Author" />
            <asp:TemplateField HeaderText="发布日期">
                <ItemTemplate>
                    <%#Eval("CDate","{0:yyyy年MM月dd日}") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="<%#Eval("Link") %>" title="点击浏览" target="_blank" class="option_style"><i class="fa fa-globe"></i></a>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="catch" ToolTip="抓取文章" CssClass="option_style"><i class="fa fa-plus"></i>抓取文章</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
   <style type="text/css">
       em {color:red;}
       #AllID_Chk{ display:none;}
   </style>
    <script>
        $(function () {
            switch ("<%=Source%>") {
                case "新闻":
                    $(".nav-tabs li:eq(1)").addClass("active");
                    break;
                case "微信":
                    $(".nav-tabs li:eq(2)").addClass("active");
                    break;
                case "微博":
                    $(".nav-tabs li:eq(3)").addClass("active");
                    break;
                default:
                    $(".nav-tabs li:eq(0)").addClass("active");
                    break;
            }
        });
    </script>
</asp:Content>