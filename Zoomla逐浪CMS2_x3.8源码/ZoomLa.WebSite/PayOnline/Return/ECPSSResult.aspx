<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ECPSSResult.aspx.cs" Inherits="PayOnline_Return_ECPSSResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table>
         <tr>
             <td>订单号:</td>
             <td><asp:Label ID="BillNo_L" runat="server"></asp:Label></td>
         </tr>
         <tr>
             <td>价格:</td>
             <td><asp:Label ID="Amount_L" runat="server"></asp:Label></td>
         </tr>
         <tr>
             <td>返回状态:</td>
             <td><asp:Label ID="Result_L" runat="server"></asp:Label></td>
         </tr>
     </table>
    </div>
    </form>
</body>
</html>
