<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MisAttendance.aspx.cs" MasterPageFile="~/Common/Master/Empty.master" Inherits="MIS_MisAttendance" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>考勤</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
    function loadPage(id, url) {
        $("#" + id).addClass("loader");
        $("#" + id).append("Loading......");
        $.ajax({
            type: "get",
            url: url,
            cache: false,
            error: function () { alert('加载页面' + url + '时出错！'); },
            success: function (msg) {
                $("#" + id).empty().append(msg);
                $("#" + id).removeClass("loader");
            }
        });
    }
    window.onload = function () {
        loadPage('ProShow', '/Mis/MisPunch.aspx');
    }
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="Meno">
<div id="pro_left">
    <ul class="AttenLis list-unstyled">
        <li onmouseover="this.style.backgroundColor='#e0e8fc'" onmouseout="this.style.backgroundColor='#f6f9fe'"><a href="javascript:void(0)" onclick="loadPage('ProShow','/Mis/MisPunch.aspx')">考勤打卡</a></li>
        <li onmouseover="this.style.backgroundColor='#e0e8fc'" onmouseout="this.style.backgroundColor='#f6f9fe'"><a href="javascript:void(0)" onclick="loadPage('ProShow','/Mis/MisAttDaily.aspx')">我的考勤</a></li>
        <li onmouseover="this.style.backgroundColor='#e0e8fc'" onmouseout="this.style.backgroundColor='#f6f9fe'"><a href="javascript:void(0)" onclick="loadPage('ProShow','/Mis/MisAttendanceInfo.aspx')">考勤统计</a></li>
        <li onmouseover="this.style.backgroundColor='#e0e8fc'" onmouseout="this.style.backgroundColor='#f6f9fe'"><a href="javascript:void(0)" onclick="loadPage('ProShow','/Mis/MisSignSet.aspx')">考勤设置</a></li>
    </ul>
</div>
<div id="pro_right" runat="server">
    <div class="RHead"><asp:Label ID="lblTit" Text="考勤" runat="server"></asp:Label></div>
    <div id="ProShow"></div>
</div>
</div>
</asp:Content>
