<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.MIS.File.Default" MasterPageFile="~/Common/Master/Empty.master" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>文件</title>
<link href="../../App_Themes/User.css" type="text/css" rel="stylesheet" />
<script>
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
</script>
</asp:Content> 
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="Meno">
<table class="border" width="100%"  >
<tr height="25" class="title" style="background-color: #e7f3ff;"><th>ID</th><th>文件</th><th>用户</th><th>时间</th></tr>
<ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='3' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
<ItemTemplate>
<tr style="line-height:25px;"><td align="center" width="40"><%#Eval("ID") %></td><td><a href="/UploadFiles/<%#Eval("Content") %>"><%#Eval("Title") %></a></td><td><%#Eval("Inputer") %></td><td><%#Eval("CreateTime","{0:yy-MM-dd}") %></td></tr>
</ItemTemplate>
<FooterTemplate></FooterTemplate>
</ZL:ExRepeater>
</table>
</div>
</asp:Content>
