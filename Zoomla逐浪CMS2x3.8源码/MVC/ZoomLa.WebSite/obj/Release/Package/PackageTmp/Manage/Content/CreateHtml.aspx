<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateHtml.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.CreateHtml"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>发布操作</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
<div class="text-left">
    <div class="alert alert-info" style="background: rgba(216, 236, 246, 0.5)">
        <p style="font-size: 1.5em;"><span class="fa-stack fa-lg">
            <i class="fa fa-circle fa-stack-2x"></i>
            <i class="fa fa-print fa-stack-1x fa-inverse"></i></span>面向静态发布提升站点效率!
            <a href="CreateHtmlContent.aspx" class="btn btn-info">&lt;返回生成页</a>
        </p>
        <p style="padding-left: 100px;"></p>
        <p style="line-height: 28px; margin-bottom: 10px; border-bottom: 1px dashed #808080;">
            网站首页的后缀需在<a href="<%=CustomerPageAction.customPath2%>Config/SiteInfo.aspx">[系统设置]</a>-<a href="<%=CustomerPageAction.customPath2%>Config/SiteInfo.aspx">[网站配置]</a>-<a href="<%=CustomerPageAction.customPath2%>Config/SiteOption.aspx">[进阶信息]</a>中进行设置,.aspx为不生成静态页<br />
            对应栏目页需在<a style="color: #428bca;" href="<%=CustomerPageAction.customPath2%>Config/SiteInfo.aspx">[系统设置]</a>-<a style="color: #428bca;" href="<%=CustomerPageAction.customPath2%>Content/NodeManage.aspx">[节点管理]</a>对相应节点生成选项进行设置,.aspx为不生成静态页<br />
            发布内容页，需对内容所在节点的生成选项的中，对内容页扩展名进行设置，.aspx为不生成静态页。
        </p>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:Timer ID="Timer1" runat="server" Interval="1000" OnTick="Timer1_Tick"></asp:Timer>
        <ul>
            <asp:Literal ID="infoHtml" runat="server"></asp:Literal>
        </ul>
    </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>