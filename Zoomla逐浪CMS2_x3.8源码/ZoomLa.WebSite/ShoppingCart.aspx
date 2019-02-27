<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="ShoppingCart" EnableViewStateMac="false" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>加入购物车</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="main" class="rg_inout">
    <h1>第一步:加入购物车<span>[<asp:Label ID="Label1" runat="server" BorderWidth="0px" ForeColor="Red"></asp:Label>]</span><img alt="" src="/user/images/regl1.gif" width="242" height="14" /></h1>
    <div class="cart_lei">
        <ul>
          <li class="i0">图片</li>
          <li class="i1">商品名称</li>
          <li class="i2">商品属性</li>
          <li class="i3">单位</li>
          <li class="i4">数量</li>
           <li class="i5">积分</li>
          <li class="i7">折扣</li>
          <li class="i8">总计</li>
          <li class="i9">操作</li>
        </ul>
     </div>
     
 <div class="cart_con">
    <asp:Repeater ID="cartinfo" runat="server" OnItemDataBound="cartinfo_ItemDataBound">
      <ItemTemplate>
        <ul <%#(Eval("Bindpro","{0}")=="")?"":"style=background-color:#E6E6E6"%>>
          <li class="i0"> <%#getproimg(Eval("proid","{0}"))%></li>
          <li class="i1"> <%#GetProtype(DataBinder.Eval(Container, "DataItem.proid", "{0}"))%><a href='Shop.aspx?ItemID=<%#Eval("proid")%>' target="_blank"><%#Eval("proname")%></a></li>
          <li class="i2">&nbsp; <%#Eval("Attribute")%></li>
          <li class="i3"> <%#getProUnit(DataBinder.Eval(Container, "DataItem.proid", "{0}"))%></li>
          <li class="i4"><input onkeydown="if(event.keyCode==13){ keydo('<%#Eval("id") %>');return false;}" id='num<%#Eval("id") %>' value='<%#DataBinder.Eval(Container, "DataItem.pronum", "{0}")%>' style="width: 30px; height:20px" onblur="keydo('<%#Eval("id") %>')" /></li>
            <li class="i5"> <%#getjiage(Eval("proclass", "{0}"), "1", DataBinder.Eval(Container, "DataItem.proid", "{0}"), DataBinder.Eval(Container, "DataItem.pronum", "{0}"))%></li>
          <li class="i7"> <%#getproscheme(Eval("ID","{0}"))%></li>
          <li class="i8"><span id='price<%#Eval("id") %>'> <%#getprojia(Eval("ID", "{0}"), DataBinder.Eval(Container, "DataItem.pronum", "{0}"))%></span></li>
          <li class="i9"><a href="ShoppingCart.aspx?menu=del&cid=<%#Eval("id")%>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除</a></li>
          <div class="clear"></div>
        </ul>
          
         
 
      </ItemTemplate>
    </asp:Repeater>
     </div>
    <asp:HiddenField ID="prolists" runat="server" />
    
    <div style="clear: both; margin-top: 3px; margin-left: 12px; color: #ccc;">
      <asp:Label ID="yhq" runat="server" Text="优惠券:" ForeColor="Red" Font-Size="12px"></asp:Label>
      &nbsp&nbsp
      <asp:TextBox ID="yhqtext" runat="server" BorderColor="Red" BorderStyle="Solid" Height="18px" Width="90px" Style="margin-bottom: -2px;"></asp:TextBox>
      <span style="margin-top: 3px; margin-left: 12px;"></span>
      <asp:Label ID="lebel" runat="server" Text="密&nbsp;&nbsp;码:" ForeColor="Red" Font-Size="12px"></asp:Label>
      &nbsp;&nbsp;
      <asp:TextBox ID="yhqpwd" runat="server" BorderColor="Red" BorderStyle="Solid" Height="18px" Width="90px" Style="margin-bottom: -2px;"></asp:TextBox>
      提示：只要填入合法的优惠券，系统会在下一步结算时计折。</div>
    <ul style="margin-left: 170px;">
      <li style="width: 100%;">共
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
        个商品/页 转到第
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"></asp:DropDownList>
        页</li>
    </ul>

    <ul>
      <li style="width: 250px; text-align: left;">合计： <asp:Label ID="alljiage" runat="server" Text=""></asp:Label>
      </li>
      <li>&nbsp;</li>
      <li style="width: 250px; text-align: left;">
        <asp:Button ID="Button1" runat="server" Text="去收银台结帐" CssClass="i_bottom" OnClick="Button1_Click" />
        <asp:HiddenField ID="project" runat="server" />
        <asp:HiddenField ID="jifen" runat="server" />
        <asp:HiddenField ID="hfproclass" runat="server" />
      </li>
    </ul>


    <div id="Div1" runat="server">促销内容:</div>
    <div id="Label2" runat="server"></div>
    <div id="Label3" runat="server" style="padding-top: 10px">
    <div class="cart_lei">
      <ul>
        <li class="i0">选择</li>
        <li class="i1">ID</li>
        <li class="i2">图片</li>
        <li class="i3">商品名称</li>
        <li class="i4">单位</li>
        <li class="i5">数量</li>
        <li class="i6">市场价</li>
        <li class="i7">零售价</li>
        <li class="i8">优惠价</li>
      </ul>
      </div>
      
 <div class="cart_con">
      <asp:Repeater ID="Repeater1" runat="server">
        <ItemTemplate>
          <ul>
            <li class="i0"><input type="radio" name="projuct" value="<%#Eval("ID") %>" /></li>
            <li class="i0"><%#Eval("ID") %></li>
            <li class="i0"><%#getproimg(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></li>
            <li class="i0"><%#GetProtype(Eval("id", "{0}"))%><%#Eval("proname")%></li>
            <li class="i0"><%#getProUnit(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></li>
            <li class="i0"></li>
            <li class="i0"><%#shijia(DataBinder.Eval(Container, "DataItem.ShiPrice", "{0}"))%></li>
            <li class="i0"><%#shijia(DataBinder.Eval(Container, "DataItem.LinPrice", "{0}"))%></li>
            <li class="i0"><%#Getprojectjia(Eval("ID","{0}")) %></li>
            <div class="clear"></div>
          </ul>
        </ItemTemplate>
      </asp:Repeater>
    </div>


  </div>

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
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <script type="text/javascript">
        var ajax = new AJAXRequest();
        function keydo(ids) {
            var num = document.getElementById("num" + ids).value;
            var prolists = document.getElementById("prolists").value;
            ajax.get(
        "/prompt/ShopCart/UpdateShopCar.aspx?cid=" + ids + "&num=" + num + "&menu=update&prolist=" + prolists,
        function (obj) {
            console.log( obj.responseText);
            var pri = obj.responseText;
            if (pri != null) {
                var prics = pri.split('|');
                if (prics != null && prics.length > 1) {
                    document.getElementById("alljiage").innerText = prics[4];
                    document.getElementById("price" + ids).innerText = prics[1];
                    document.getElementById("currentpri" + ids).innerText = prics[2];
                    document.getElementById("num" + ids).innerText = prics[3];
                }
            }
        }
        );
        }
</script>
</asp:Content>