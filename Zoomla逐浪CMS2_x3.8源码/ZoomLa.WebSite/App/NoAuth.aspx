<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NoAuth.aspx.cs" Inherits="App_NoAuth" MasterPageFile="~/Common/Master/Commenu.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>APP授权</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <div class="container" style="margin-top:80px;">
        <ul class="list-unstyled" id="optionul">
        <li>
        <div class="user_menu" style="background: rgb(153, 153, 255);" title="点击授权">
            <a target="_blank" href="http://www.z01.com/APP/AuthApply.aspx">
                <i class="fa fa-copyright"></i>
                <br />获取授权<br /><span class="font16">获取授权,本地布署生成APP</span></a>
        </div></li>
        <li><div class="user_menu" style="background: rgb(102, 153, 51);" title="点击访问">
            <a target="_blank" href="http://app.z01.com/APP/Default.aspx">
                <i class="fa fa-tasks"></i>
                <br />免费生成<br /><span class="font16">访问微逐浪,在线免费生成APP</span></a>
        </div></li>
        <li><div class="user_menu" style="background:rgb(39, 169, 227);" title="点击下载">
            <a target="_blank" href="http://www.z01.com/APP/APPManual.docx">
                <i class="fa fa-list-alt"></i>
                <br />部署手册<br /><span class="font16">获取本地布署和使用手册</span></a>
        </div></li>
    </ul>
 </div>
 <div class="alert alert-danger">站点未授权,无法本地生成APP,你可以申请授权或在线生成APP,如果你已有授权码,<a href="<%=CustomerPageAction.customPath2+"Config/SiteOption.aspx?Tab=2" %>">点此配置</a></div>
 <div class="alert alert-info remind" runat="server" id="auth_sp"></div>
    <style type="text/css">
       * {font-family:'Microsoft YaHei';}
       #optionul li{width:33%;float:left;}
       .user_menu{ margin:auto; margin-bottom:10px; width:220px; height:220px; border-radius:10px; background:rgba(23,126,1,1);}
       .user_menu a { display: block; padding-top:20px; height: 220px; text-align: center; color: #fff; font-size: 20px; margin-bottom: 10px; border-radius:10px; box-shadow:0 0 3px 1px rgba(0,0,0,0.3);}
       .user_menu a i{ font-size:6em;}
       .font16 {font-size:16px;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>