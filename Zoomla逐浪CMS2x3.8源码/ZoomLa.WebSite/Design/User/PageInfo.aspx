<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PageInfo.aspx.cs" Inherits="Design_User_PageInfo" MasterPageFile="~/Design/Master/User.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<link href="/Design/res/css/user.css" rel="stylesheet" />
<title>页面配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="siteinfo container">
    <ol class="breadcrumb">
        <li><a href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/Design/User/">动力模块</a></li>
        <li class="active">站点配置</li>
    </ol>
    <div class="itemBody">
        <div class="item">
            <span class="left_sp">标题</span>
            <span class="mid_sp">
                <asp:TextBox runat="server" ID="Title_T" CssClass="form-control text_300" />
                <asp:RequiredFieldValidator runat="server" ID="R1" ForeColor="Red" ControlToValidate="Title_T" ErrorMessage="标题不能为空" Display="Dynamic" />
            </span>
        </div>
        <div class="item">
            <span class="left_sp">路径</span>
            <span class="mid_sp">
                <asp:TextBox runat="server" ID="Path_T" CssClass="form-control text_300" />
                <a href="/Design/?ID=<%:Mid %>" target="_blank" title="设计"><i class="fa fa-paint-brush"></i></a>
                <asp:RequiredFieldValidator runat="server" ID="R2" ForeColor="Red" ControlToValidate="Path_T" ErrorMessage="路径不能为空" Display="Dynamic" />
            </span>
        </div>
        <div class="item">
            <span class="left_sp">创建时间</span>
            <span class="mid_sp"><asp:Label runat="server" ID="CDate_L"></asp:Label></span>
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