<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DomName.aspx.cs" Inherits="ZoomLaCMS.Manage.Site.DomName" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">
</style>
<link rel="stylesheet" href="/Plugins/Domain/css/style.css" type="text/css" media="all" />
<link rel="stylesheet" href="/Plugins/Domain/css/css.css" type="text/css" />
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/JS/Common.js"></script>
<script type="text/javascript" src="/Plugins/Domain/Site.js"></script>
<script type="text/javascript">
    $().ready(function () {
        $("div[class='twoOption']").mousemove(function () { $(this).addClass("optionChoose"); }).mouseout(function () { $(this).removeClass("optionChoose"); });
    }); 
    function ShowTabs(obj, id) {//Div切换
        $("#" + id).show().siblings().hide();
        $("#<%=dataField.ClientID%>").val(obj.id);
    }
    $().ready(function () {
        id = $("#<%=dataField.ClientID%>").val();
        if (id != "") {
            $("#" + id).trigger("click");
        }
    });
    function openFrame(url)
    {
        $("#tab1Frame").attr("src",url);
    }
</script>
<title>域名注册</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="n_site_bread">
<ul class="breadcrumb">
<li><a href="<%= CustomerPageAction.customPath2 %>/Main.aspx">工作台</a></li>
<li><a href="Default.aspx">站群中心</a></li>
<li class="active">域名注册</li>
</ul>
</div> 
<div class="n_site_main"> 
<iframe id="tab1Frame" onload="frameInit(this,'tab1Frame');" src="DomTempMange.aspx" style="width:98%; height:700px;" frameborder=0 scrolling=no></iframe>
<asp:HiddenField runat="server" ID="addUrl" />
<br />
<asp:HiddenField runat="server" ID="dataField" />
</div> 
</asp:Content>