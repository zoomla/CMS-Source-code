<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchJob.aspx.cs" Inherits="ZoomLaCMS.Search.SearchJob" %>

<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
<title>职位详情搜索</title>
<link href="../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</head>
<body>
<form id="form1" runat="server">    
<div class="us_seta">   
    <h1 style="text-align: center">
            <asp:Label ID="LblModelName" runat="server" Text="Label"></asp:Label></h1>
    <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0" class="border">            
        <asp:Literal ID="ModelSearchHtml" runat="server"></asp:Literal><tr class="tdbgbottom">
            <td colspan="2" align="center">
                <asp:Button ID="Button1" runat="server" Text="搜索" OnClick="Button1_Click"/><asp:HiddenField ID="HdnModel" runat="server" />
            </td>
        </tr>
    </table>        
</div>
<div id="nodata" runat="server" style="border:1px solid #b7b7b7; height:80px; text-align:center; margin-top:5px; line-height:60px;">
    没有指定搜索条件，或没有搜索到符合条件的数据
</div>
</form>
</body>
</html>
