<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileBuyManage.aspx.cs" Inherits="User_Content_FileBuyManage" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>文件记录</title>
<style>
#AllID_Chk{display:none;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="content" data-ban="cnt"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a href='/User/'>会员中心</a></li>
<li class='active'>文件记录</li>
<div class="clearfix"></div>
</ol>
<ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False"  AllowPaging="true" AllowSorting="true" 
CssClass="table table-striped table-bordered table-hover" EnableTheming="False" GridLines="None"
OnPageIndexChanging="EGV_PageIndexChanging" EmptyDataText="没有相关记录!">
<Columns>
<asp:TemplateField HeaderText="文章标题">
<ItemTemplate>
<a href="/Item/<%#Eval("Gid") %>.aspx" target="_blank"><%#Eval("Title") %></a>
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField HeaderText="文件名" DataField="FName" />
<asp:BoundField HeaderText="用户名" DataField="UserName" />
<asp:BoundField HeaderText="价格" DataField="BuyPrice" />
<asp:BoundField HeaderText="购买时间" DataField="CDate" />
<asp:BoundField HeaderText="到期时间" DataField="EndDate" />
<asp:TemplateField HeaderText="操作">
<ItemTemplate>
<a target="_blank" href="/Common/Label/DownFile.aspx?ranstr=<%#Eval("Ranstr") %>&Gid=<%#Eval("Gid") %>&Field=<%#Eval("Field") %>"><span class="fa fa-download"></span>下载</a>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</ZL:ExGridView>
</div>
</asp:Content>