<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.MIS.Daily.Default" MasterPageFile="~/Common/Master/Empty.master" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>日志</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" /> 
<script src="/js/calendar-brown.js"></script>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
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
    var myDate = new Date();
    loadPage("jouRight", "/Mis/Daily/AddDaily.aspx?Date=" + myDate.getFullYear() + "-" + (myDate.getMonth() + 1) + "-" + myDate.getDate());
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="calendar-container" class="jouLeft">
<script type="text/javascript">
function dateChanged(calendar) {
    if (calendar.dateClicked) {
        var y = calendar.date.getFullYear();
        var m = calendar.date.getMonth(); // integer, 0..11
        var d = calendar.date.getDate(); // integer, 1..31
      //  alert(y + "-" + (m + 1) + "-" + d)
        loadPage("jouRight", "/Mis/Daily/AddDaily.aspx?Date=" + y + "-" + (m + 1) + "-" + d);
      
        //  window.open("/Class_70/NodeHot.aspx?name=admin&Data=" + y + "-" + (m + 1) + "-" + d, "jouRight");
    }
};
Calendar.setup(
{
    flat: "calendar-container", // ID of the parent element 
    flatCallback: dateChanged  // our callback function
} 
);
</script>
</div>
</asp:Content>
