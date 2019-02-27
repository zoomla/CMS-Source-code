<%@ Page Language="C#" AutoEventWireup="true"  ValidateRequest="false" CodeFile="ShowJSCode.aspx.cs" Inherits="ZoomLa.WebSite.Manage.AddOn.ShowJSCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>预览JS代码</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" /> 
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
	<div class="r_n_pic"></div>
	<span>后台管理</span>&gt;&gt;<span>附件管理</span> &gt;&gt;<span>广告管理</span>
	</div> 
    <div class="clearbox"></div>
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border">
        <tr class="title">
            <td colspan="2" align="center">
                <strong>版位JS调用代码</strong></td>
        </tr>
        <tr class="tdbgleft">
            <td align="center">
            <span style="color:Green">调用方法：将下面的代码插入到网页中预定的广告位置</span></td>
        </tr>
        <tr class="tdbg">
            <td align="center" style="height: 27px">
                <asp:TextBox ID="TxtZoneCode" runat="server" Height="102px" TextMode="MultiLine"
                    Width="578px"></asp:TextBox>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="center">
                <a href="ADZoneManage.aspx">返回</a></td>
        </tr>
    </table>
    </form>
</body>
</html>
