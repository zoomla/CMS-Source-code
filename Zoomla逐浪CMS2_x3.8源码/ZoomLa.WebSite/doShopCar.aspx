<%@ Page Language="C#" AutoEventWireup="true" CodeFile="doShopCar.aspx.cs" Inherits="doShopCar" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head>
<title>加入购物车</title>
<link href="../App_Themes/UserThem/style.css"rel="stylesheet" type="text/css" />
<link href="../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script src="../JS/ajaxrequest.js"></script>
<script type="text/javascript">
    var ajax = new AJAXRequest();
    function keydo(ids) {
        var num = document.getElementById("num" + ids).value;
        ajax.get(
        "/prompt/ShopCart/UpdateShopCar.aspx?cid=" + ids + "&num=" + num + "&menu=shopcar",
        function (obj) {
            document.getElementById("alljiage").innerText = obj.responseText;
        }
        );
    }
</script>
</head>
<body>
<form id="form1" runat="server">
  <div id="main" class="rg_inout">
    <h1>第一步:加入购物车<span><asp:Label ID="Label1" runat="server" BorderWidth="0px" ForeColor="Red"></asp:Label>]</span><img src="/user/images/regl1.gif" width="242" height="14" /></h1>
    <div class="cart_lei">
    <ul>
      <li class="i1">商品名称</li>
      <li class="i2">来源</li>
      <li class="i3">单价</li>
      <li class="i4">数量</li>
      <li class="i5"><asp:Label ID="lblProinfo" runat="server" Text="备注"></asp:Label></li>
      <li class="i6">操作</li>
    </ul>
    </div>

 <div class="cart_con">
    <asp:Repeater ID="cartinfo" runat="server">
      <ItemTemplate>
        <ul <%#(Eval("Bindpro","{0}")=="")?"":"style=background-color:#E6E6E6"%>>
          <li class="i1"> <%#GetProtype(DataBinder.Eval(Container, "DataItem.proid", "{0}"))%><a href="Shop.aspx?ID=<%#Eval("proid")%>" target="_blank"><%#Eval("proname")%></a></li>
          <li class="i2"><%#Eval("proseller")%></li>
          <li class="i3"><%#getjiage(Eval("ProClass", "{0}"), DataBinder.Eval(Container, "DataItem.proid", "{0}"))%></li>
          <li class="i4"><input id='num<%#Eval("id") %>' value='<%#getShu(DataBinder.Eval(Container, "DataItem.pronum", "{0}"))%>' style="width:30px" height='20px' onblur="keydo('<%#Eval("id") %>')"/></li>
          <li class="i5"><%#getProinfo(DataBinder.Eval(Container, "DataItem.proid", "{0}"))%></li>
          <li class="i6"><a href='doShopCar.aspx?menu=del&cid=<%#Eval("id")%>&ProClass=<%#Eval("ProClass") %>' onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除</a></li>
          <div class="clear"></div>
        </ul>
      </ItemTemplate>
    </asp:Repeater>
   </div>
    <div style="margin-left:170px;">
      <li style="width:100%;">共
        <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
        个商品
        <asp:Label ID="Toppage" runat="server" Text="" />
        <asp:Label ID="Nextpage" runat="server" Text="" />
        <asp:Label ID="Downpage" runat="server" Text="" />
        <asp:Label ID="Endpage" runat="server" Text="" />
        页次：
        <asp:Label ID="Nowpage" runat="server" Text="" />
        /
        <asp:Label ID="PageSize" runat="server" Text="" />
        页
        <asp:Label ID="pagess" runat="server" Text="" />
        个商品/页  转到第
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"></asp:DropDownList>页</li>
    </div>
    <div class="juan">
    <asp:Label ID="yhq" runat="server" Text="优惠券:" ></asp:Label>
    <asp:TextBox ID="yhqtext" runat="server" BorderColor=Red BorderStyle=Solid Height=18px width=90px style="margin-bottom:-2px;"></asp:TextBox>
    <asp:Label ID="lebel" runat="server" Text="密&nbsp&nbsp  码:" ></asp:Label>
    <asp:TextBox ID="yhqpwd" runat="server" BorderColor=Red BorderStyle=Solid Height=18px width=90px style="margin-bottom:-2px;"></asp:TextBox>
    <span>提示：只要填入合法的优惠券，系统会在下一步结算时计折。</span>
	</div> 
       
    <div class="jia">
    <ul>    
      <li>合计：<asp:Label ID="alljiage" runat="server" Text=""></asp:Label></li>
      <li><asp:Button ID="Button1" runat="server" Text="去收银台结帐" OnClientClick="keydo()"  onclick="Button1_Click" />
        <asp:HiddenField ID="project" runat="server" />
        <asp:HiddenField ID="jifen" runat="server" />
        <asp:HiddenField  ID="ProClass" runat="server" />
      </li>
    </div>
    <!-- 右边结束 --> 
  </div>
  </div>
  <!--main end -->
  
  <div id="bottom"> <a href="/"><img src="<%Call.Label("{$LogoUrl/}"); %>" alt="<%Call.Label("{$SiteName/}"); %>" /></a>
    <p> 
      <script language="javascript" type="text/javascript"> 
<!-- 
var year="";
mydate=new Date();
myyear=mydate.getYear();
year=(myyear > 200) ? myyear : 1900 + myyear;
document.write(year); 
--> 
</script>&copy;&nbsp;Copyright&nbsp;
      <%Call.Label("{$SiteName/}"); %>
      All rights reserved.</p>
  </div>
  <input type="hidden"  id="projuct" name="projuct" runat="server" />
  <input type="hidden" id="Stock" name="Stock" runat="server" />
  <!--数量 -->
  <input type="hidden" id="GuestName" name="GuestName" runat="server"/>
  <!-- 客户名称 -->
  <input type="hidden" id="comedate" name="comedate" runat="server"/>
  <!-- 入住时间 -->
  <input type="hidden" id="GuestMobile" name="GuestMobile" runat="server"/>
  <!-- 客户手机 -->
  <input type="hidden" id="cityname" name="cityname" runat="server"/>
  <!-- 城市名称 -->
  <input type="hidden" id="preID" name="preID" runat="server"/>
  <!-- 证件号码 -->
  <input type="hidden" id="Type" name="Type" runat="server" />
</form>
</body>
</html>