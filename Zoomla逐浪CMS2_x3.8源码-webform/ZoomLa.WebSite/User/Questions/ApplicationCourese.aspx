<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationCourese.aspx.cs" Inherits="User_Questions_ApplicationCourese" EnableViewStateMac="false" MasterPageFile="~/User/Default.master" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
<title>申请课程</title>
<style>
</style>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx" target="_parent">会员中心</a></li>
        <li class="active"><a href="ApplicationCourese.aspx">申请课程</a></li>
		<div class="clearfix"></div>
    </ol>
  <div class="s_body" style="width: 100%">
    <div class="s_bleft" style="width: 20%; float: left;">
      <iframe frameborder="0" width="100%" height="800px" src="TreeNode.aspx?Type=2" scrolling="auto" id="I1" name="I1"></iframe>
    </div>
    <div style="width: 80%; float: left">
	  <div class="us_seta" style="margin-top: 5px;">
    <h1 align="center"> 申请课程</h1>
  </div>
      <div class="divbox" id="nocontent" runat="server"> 暂无课程信息</div>
      <ZL:ExGridView ID="EGV" CssClass="table table-bordered table-hover" runat="server" AutoGenerateColumns="False"  AllowPaging="true" OnPageIndexChanging="EGV_PageIndexChanging"  AllowSorting="False" EnableTheming="False"  EnableModelValidation="True">
        <HeaderStyle Height="28px"></HeaderStyle>
        <RowStyle  Height="35px"></RowStyle>
        <Columns>
        <asp:TemplateField HeaderText="课程名称" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="50%" >
          <ItemTemplate> <%#Eval("CourseName")%> </ItemTemplate>
          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="50%"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="课时" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" ItemStyle-Width="25%" >
          <ItemTemplate> <%#GetClass(Eval("ID", "{0}"))%> </ItemTemplate>
          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%"></ItemStyle>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle" ItemStyle-Width="25%" >
          <ItemTemplate> <%# getStr(Convert.ToInt32(Eval("ID").ToString()))%> </ItemTemplate>
          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="25%"></ItemStyle>
        </asp:TemplateField>
        </Columns>
      </ZL:ExGridView>
    </div>
  </div>
</asp:Content>
