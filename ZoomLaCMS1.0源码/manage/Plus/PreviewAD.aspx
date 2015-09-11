<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreviewAD.aspx.cs" Inherits="ZoomLa.WebSite.Manage.AddOn.PreviewAD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>预览版位JS效果</title>
</head>
<body>
    <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1" class="border">
        <tr class="title">
            <td colspan="2" align="center">
                <strong>预览版位JS效果</strong></td>
        </tr>
        <tr class="tdbg2">
            <td style="height: 25px" align="center">
                <a href="javascript:this.location.reload();">刷新页面</a>&nbsp;&nbsp;&nbsp;&nbsp; <a
                    href="ADZoneManage.aspx">返回上页</a>
            </td>
        </tr>
        <tr valign="top">
            <td>
                <div style="height: 400px" id="ShowJS" runat="server">
                </div>
            </td>
        </tr>
    </table>
</body>
</html>
