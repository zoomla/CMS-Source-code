<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchClear.aspx.cs" Inherits="ZoomLaCMS.Search.SearchClear" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>清除关键字</title>
<link href="../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script src="../JS/calendar.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" class="us_seta">
      <h1 style="text-align: center">
            <asp:Label ID="LblModelName" runat="server" Text="清除关键字"></asp:Label></h1>
      <table style="width: 100%; margin: 0 auto;" cellpadding="0" cellspacing="0" class="border">    
        <td width="10%" class="tdbg" colspan="2">
               请填写下列清除条件（程序将清除小于下列值的数据）<br />
            </td>        
        <tr class="tdbg">
          <td style="width:20%; float:left;line-height:30px; text-align:right">搜索次数:</td>
          <td style="width:80%;line-height:30px; text-align:left"><asp:TextBox ID="txtCount" runat="server"></asp:TextBox></td>
        </tr>
        <tr class="tdbg">
            <td style="width:20%; float:left;line-height:30px; text-align:right">更新时间:</td>
            <td style="width:80%;line-height:30px; text-align:left"><asp:TextBox ID="txtUpdate" runat="server" onclick="calendar()" ></asp:TextBox></td>
        </tr>
       <tr class="tdbg">
            <td style="width:20%; float:left;line-height:30px; text-align:right">入库时间:</td>
            <td style="width:80%;line-height:30px; text-align:left"><asp:TextBox ID="txtStorageData" runat="server" onclick="calendar()" ></asp:TextBox></td>
       </tr>
        <tr class="tdbgbottom">
            <td colspan="2" align="center"><asp:Button ID="btnclear" runat="server" Text="清除" 
                    onclick="btnclear_Click" /></td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>

