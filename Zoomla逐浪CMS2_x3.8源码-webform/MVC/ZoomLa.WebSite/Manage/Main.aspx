<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="ZoomLaCMS.Manage.Main" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<title>快速导航</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content"><!--每页14条数据-->
<div class="container-fluid">
<div class="row main_nav" runat="server" id="navDiv">
<asp:Literal runat="server" ID="model_Lit" EnableViewState="false"></asp:Literal>
</div>
<asp:Literal runat="server" ID="page_Lit" EnableViewState="false"></asp:Literal>
<div id="pageDiv" runat="server" visible="false" class="pageDiv">
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" EnableTheming="False"  AllowPaging="true" OnPageIndexChanging="EGV_PageIndexChanging" PageSize="15"
    EmptyDataText="没有匹配的页面，建议更换关键词!!"  class="table table-striped table-bordered table-hover">
  <Columns>
  <asp:BoundField DataField="Index" HeaderText="ID"/>
  <asp:BoundField DataField="DTitle" HeaderText="标题" HtmlEncode="false"/>
  <asp:TemplateField HeaderText="路径">
    <ItemTemplate> <a href="<%#Eval("Url") %>" title="点击访问"><%#Eval("Url") %></a> </ItemTemplate>
  </asp:TemplateField>
  </Columns>
</ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
.mysite{display:none;}
#navDiv>div{height:191px;overflow:hidden;}
</style>
<script>
$().ready(function () {
$(".breadcrumb").hide();
parent.setLayout();
})
</script>
</asp:Content>