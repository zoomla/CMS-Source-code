<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false"  CodeFile="EditJSTemplate.aspx.cs" Inherits="ZoomLa.WebSite.Manage.AddOn.EditJSTemplate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>编辑广告JS模板</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/Main.css" type="text/css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
<div class="r_navigation">
<div class="r_n_pic"></div>
<span>后台管理</span>&gt;&gt;<span>附件管理</span> &gt;&gt;<span><a href="JSTemplate.aspx">广告JS模板管理</a></span>&gt;&gt;<span>广告JS模板编辑</span>
</div>
<div class="clearbox"></div>
<table width="100%" class="border" border="0" cellpadding="2" cellspacing="1">
        <tr align="center">
            <td class="spacingtitle">
                <b>修改模板内容</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td style="height: 350px" align="center">
                <asp:TextBox ID="TxtADTemplate" runat="server" Height="326px" TextMode="MultiLine"
                    Width="582px"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td style="height: 50px" align="center">
                <asp:Button ID="EBtnSaverTemplate" runat="server" Text="保存修改结果" OnClick="EBtnSaverTemplate_Click" />
            &nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HdnZoneType" runat="server" />
</form>
</body>
</html>
