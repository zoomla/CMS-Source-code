<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Manage.Sentiment.Default" MasterPageFile="~/Manage/I/Default.master"%>
 <asp:Content ID="Content1" runat="server" ContentPlaceHolderID="head">
<title>舆情监测</title> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="ani_icon text-center"> 
<div><i class="fa fa-rocket animated homeb201503"></i></div> 
<h1 class="animated homeb201505">舆情监测-做信息时代的主人</h1>
</div>
<div class="ani_func text-center">
<ul class="list-inline">
<li><a href="SenConfig.aspx" class="btn btn-primary">系统配置</a></li>
<li><a href="DataList.aspx" class="btn btn-primary">监测报表</a></li>
<li><a href="DataList.aspx" class="btn btn-primary">监测报告</a></li>
</ul>
</div> 
<script>
    $().ready(function () {
        setInterval("$('.ani_func').fadeIn()", 3000);
    })
</script>        

</asp:Content> 
