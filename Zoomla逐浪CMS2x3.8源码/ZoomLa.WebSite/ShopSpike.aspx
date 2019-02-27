<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopSpike.aspx.cs" Inherits="ShopSpike" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head>
<title>商品秒杀</title>
<link href="../App_Themes/UserThem/style.css" rel="stylesheet" type="text/css" />
<link href="../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" language="javascript" src="JS/ajaxrequest.js" ></script>
</head>
<body>
<form id="form1" runat="server">
  <div id="main" class="rg_inout">
    <h1>商品秒杀</h1>
    <div class="cart_lei"></div>
    <div>
      <ul style="width: 600px; height: 216px">
        <li style="text-align: center; width: 280px; margin-top:10px">
          <asp:Label ID="lblimg" runat="server" Text=""></asp:Label>
        </li>
        <li>
        <li style="width: 200px">
        <ul style="width: 200px">
          <li style="float: left; width: 200px; height: 30px">商品名称：<strong>
            <asp:Label ID="lblProName" runat="server" Text=""></asp:Label>
            </strong></li>
          <li style="float: left; width: 200px; height: 30px">秒杀价：
            <asp:Label ID="lblProState"  runat="server" Text=""></asp:Label>
          </li>
          <li style="float: left; width: 200px; height: 30px">提供商品数量：
            <asp:Label ID="lblPNum"  runat="server" Text=""></asp:Label>
          </li>
          <li style="float: left; width: 200px; height: 30px">可购买商品数量：
            <asp:Label ID="lblNum" runat="server" Text="" ForeColor="Red"></asp:Label>
            &nbsp;<%=Unit%></li>
          <li style="float: left; width: 200px; height: 30px">倒计时：
            <asp:Label ID="lblTimer"  runat="server" Text="" ForeColor="Red"></asp:Label>
          </li>
          </li>
          <asp:HiddenField ID="ddlNum" runat="server" />
          <li style="float: left; width: 200px; text-align: center">
            <asp:Button ID="Button1" runat="server" Text="秒  杀" onclick="Button1_Click" />
          </li>
        </ul>
        </li>
      </ul>
    </div>
  </div>
  </div>
  <!-- 右边结束 -->
  </div>

<div id="bottom">
<a href="/"><img src="<%Call.Label("{$LogoUrl/}"); %>" alt="<%Call.Label("{$SiteName/}"); %>" /></a>
<p>
<script language="javascript" type="text/javascript"> 
<!-- 
var year="";
mydate=new Date();
myyear=mydate.getYear();
year=(myyear > 200) ? myyear : 1900 + myyear;
document.write(year); 
--> 
</script>&copy;&nbsp;Copyright&nbsp; <%Call.Label("{$SiteName/}"); %> All rights reserved.</p>
</div>
</form>
</body>
</html>