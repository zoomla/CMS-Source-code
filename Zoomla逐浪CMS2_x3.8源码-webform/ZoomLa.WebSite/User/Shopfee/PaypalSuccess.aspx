<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PaypalSuccess.aspx.cs" Inherits="PaypalSuccess" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>支付成功</title>
</head>
<body>
<div>
恭喜，支付成功。<br/>
<%=Request.QueryString["item_number"]%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <%=Request.QueryString["amt"]%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%=Request.QueryString["cc"]%><br/>
支付时间：<%=DateTime.Now.ToLongDateString()%>     
</div>
</body>
</html>