<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteInfo.aspx.cs" Inherits="ZoomLaCMS.Design.User.SiteInfo" MasterPageFile="~/Design/Master/User.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/Design/res/css/user.css" rel="stylesheet" />
<title>站点配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="siteinfo container">
    <ol class="breadcrumb">
        <li><a href="/Design/">站点创作</a></li>
        <li><a href="/Design/User/">配置中心</a></li>
        <li class="active">站点设置</li>
    </ol>
    <div class="itemBody">
        <div class="item">
            <span class="left_sp">站点名</span>
            <span class="mid_sp">
                <asp:TextBox runat="server" ID="SiteName_T" CssClass="form-control text_300" />
                <asp:RequiredFieldValidator runat="server" ID="R1" ForeColor="Red" ControlToValidate="SiteName_T" ErrorMessage="站点名不能为空" Display="Dynamic" />
            </span>
            <span class="right_sp"></span>
        </div>
        <div class="item" style="line-height:normal;height:auto;">
            <span class="left_sp">站点Logo</span>
            <div class="mid_sp">
               <ZL:SFileUp runat="server" ID="Logo_UP" FType="Img" />
            </div>
        </div>
        <div class="item">
            <span class="left_sp">站点地址</span>
            <span class="mid_sp">
                <asp:Label runat="server" ID="Domain_L"></asp:Label></span>
            <span class="right_sp"></span>
        </div>
        <div class="item">
            <span class="left_sp">状态</span>
            <span class="mid_sp">运行中</span>
            <span class="right_sp"></span>
        </div>
        <div class="item">
            <span class="left_sp"></span>
            <div class="mid_sp">
                <asp:Button runat="server" ID="Save_Btn" Text="保存" OnClick="Save_Btn_Click" class="btn btn-primary" />
                <a href="Default.aspx" class="btn btn-default">返回</a>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
</asp:Content>