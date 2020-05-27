<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StoreShop.aspx.cs" Inherits="StoreShop" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>商品搜索</title>
</head>
<body>
    <div id="dic1" runat="server">
    <form id="form" name="form" method="post" action="StoreCart.aspx" runat="server">
  <label>
  商品ID  
   <input name="id" type="text" id="id" value="2" />
  </label>
  <p>
    <label>商品数量
    <input name="Pronum" type="text" id="Pronum" value="1" />
    </label>
  </p>
  <p>
    <label>
        <asp:Button ID="Savebtn" runat="server" PostBackUrl="~/StoreCart.aspx" Text="提交" /></label></p>
</form>
    </div>
</body>
</html>
