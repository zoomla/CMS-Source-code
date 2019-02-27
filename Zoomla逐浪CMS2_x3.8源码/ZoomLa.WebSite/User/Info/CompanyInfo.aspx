<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="CompanyInfo.aspx.cs" Inherits="User_Info_CompanyInfo" ClientIDMode="Static" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title><%Call.Label("{$SiteName/}"); %>会议中心</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="home" data-ban="UserInfo"></div>
<div class="container margin_t5"> 
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li class="active">商铺信息设置</li>
<div class="clearfix"></div>
</ol>
</div>
<div class="container">
<div class="us_seta">
<table class="table table-striped table-bordered table-hover btn_green">
<tr>
<td colspan="2" class="text-center">商铺信息设置</td>
</tr>
<tr>
<td>公司名称： </td>
<td>
<asp:TextBox ID="txtName" runat="server" class="form-control text_md" ></asp:TextBox>
</td>
</tr>
<tr>
<td>当前状态： </td>
<td>
<asp:Label ID="lblState" ForeColor="Red" runat="server" Text=""></asp:Label>
<asp:Label ID="lblState_hid" runat="server" Text="" Visible="false"></asp:Label></td>
</tr>
<tr>
<td>公司介绍： </td>
<td>
<asp:TextBox ID="txtCompanyDescribe" runat="server" TextMode="MultiLine" Height="60px" class="form-control text_md" ></asp:TextBox>
</td>
</tr>
<tr>
<td>公司Logo： </td>
<td>
<ZL:SFileUp ID="SFile_Up" runat="server" FType="Img" />
</td>
</tr>
<tr>
<td>服务认证： </td>
<td>
<asp:DropDownList ID="DropDownList1" CssClass="form-control text_md" Width="150" runat="server">
<asp:ListItem>请选择</asp:ListItem>
<asp:ListItem>已通过国际认证</asp:ListItem>
<asp:ListItem>产品实行三包</asp:ListItem>
<asp:ListItem Value="按客商要求生产制造">按客商要求生产制造</asp:ListItem>
<asp:ListItem>未设置</asp:ListItem>
</asp:DropDownList>
(说明: 选择国际认证则需要您添加证书并通过后才能显示) 
</td>
</tr>
<tr>
<td colspan="2" class="text-center">
<asp:Button ID="BtnSubmit" runat="server" Text="确认" class="btn btn-primary" OnClick="BtnSubmit_Click" />
<asp:Button ID="BtnCancle" runat="server" Text="取消" class="btn btn-primary" OnClick="BtnCancle_Click" />
</td>
</tr>
</table>
</div>
</div>
</asp:Content>