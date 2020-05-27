<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatBaike.aspx.cs" Inherits="ZoomLaCMS.Guest.Baike.CreatBaike" MasterPageFile="~/Guest/Baike/Baike.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<title>创建词条-<%Call.Label("{$SiteName/}"); %>百科</title> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
<ol class="breadcrumb margin_top10">
<li>您的位置：<a href="/">网站首页</a></li>
<li><a href="/Baike" target="_self">百科中心</a></li>
<li class="active">创建词条</li>
</ol>
    <h1>请输入你要创建的词条：</h1>
    <div class="input-group">
        <asp:TextBox runat="server" CssClass="form-control" ID="creatbai"></asp:TextBox>
        <span class="input-group-btn">
            <asp:Button runat="server" CssClass="btn btn-default" ID="save" Text="创建词条" OnClick="save_Click" />
        </span>
    </div>
   <asp:RequiredFieldValidator runat="server" ID="R1" ForeColor="Red" ErrorMessage="名称不能为空" ControlToValidate="creatbai" />
</div>
<div class="ask_bottom">
<p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
<p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
</div>
</asp:Content>
