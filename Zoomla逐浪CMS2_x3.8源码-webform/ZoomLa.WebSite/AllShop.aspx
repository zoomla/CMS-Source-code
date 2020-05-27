<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AllShop.aspx.cs" Inherits="AllShop" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head>
<title>代购</title>
<link href="../App_Themes/UserThem/style.css" rel="stylesheet" type="text/css" />
<link href="../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script src="JS/ajaxrequest.js" type="text/javascript"></script>
<script type="text/javascript">
    var ajax = new AJAXRequest();
    function keydo(ids) {
        var num = document.getElementById("num" + ids).value;
        ajax.get(
        "/ShopCart/UpdateShopCar.aspx?cid=" + ids + "&num=" + num + "&menu=shopcar",
        function (obj) {
            document.getElementById("alljiage").innerText = obj.responseText;
        }
        );
    }
    function sterline(s) {
        if (s == 1) {
            document.getElementById("sterline").style.display = 'block';
            document.getElementById("other").style.display = 'none';
        } else {
            document.getElementById("other").style.display = 'block';
            document.getElementById("sterline").style.display = 'none';
        }
    }
</script>
</head>
<body>
<form id="form1" runat="server">
<div runat="server" id="JZ" runat="server" >
<div id="main" class="rg_inout">
		<h1>第一步:加入购物车<span>[<asp:Label ID="Label1" runat="server" BorderWidth="0px" ForeColor="Red"></asp:Label>]</span><img src="/user/images/regl1.gif" width="242" height="14" /></h1>
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
                    <li class="i1"><%#GetProtype(DataBinder.Eval(Container, "DataItem.proid", "{0}"))%><a href="Shop.aspx?ID=<%#Eval("proid")%>" target="_blank"><%#Eval("proname")%></a></li>
                    <li class="i2"><%#Eval("proseller")%></li>
                    <li class="i3"><%#getjiage(Eval("ProClass", "{0}"), DataBinder.Eval(Container, "DataItem.proid", "{0}"))%></li>
                    <li class="i4"><input id='num<%#Eval("id") %>' value='<%#getShu(DataBinder.Eval(Container, "DataItem.pronum", "{0}"))%>' style="width:30px" height='20px' onblur="keydo('<%#Eval("id") %>')"/></li>			            
                    <li class="i5"><%#getProinfo(DataBinder.Eval(Container, "DataItem.proid", "{0}"))%></li>			            			                            
                    <li class="i6"><a href='AllShop.aspx?menu=del&cid=<%#Eval("id")%>&ProClass=<%#Eval("ProClass") %>' onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">移除</a></li>
                    <div class="clear"></div>
              </ul>
            </ItemTemplate>
        </asp:Repeater>
        </div>
        
	  <div>
		<li style="width:100%;text-align:center;">共 <asp:Label ID="Allnum" runat="server" Text=""></asp:Label> 个商品  <asp:Label ID="Toppage" runat="server" Text="" /> <asp:Label ID="Nextpage" runat="server" Text="" /> <asp:Label ID="Downpage" runat="server" Text="" /> <asp:Label ID="Endpage" runat="server" Text="" />  页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server" Text="" />页  <asp:Label ID="pagess" runat="server" Text="" />个商品/页  转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True"></asp:DropDownList>页</li>
	</div>

		<div class="jia">
			<li style="width:250px;text-align:left;">合计：<asp:Label ID="alljiage" runat="server" Text=""></asp:Label></li>
            <asp:Button ID="Button1" runat="server" Text="去收银台结帐" OnClientClick="keydo()"  onclick="Button1_Click" />
                <asp:HiddenField ID="project" runat="server" />
                <asp:HiddenField ID="jifen" runat="server" />
                <asp:HiddenField  ID="ProClass" runat="server" />
	</div>
</div>


<input type="hidden"  id="projuct" name="projuct" runat="server" />
<input type="hidden" id="Stock" name="Stock" runat="server" /><!--数量 -->
<input type="hidden" id="GuestName" name="GuestName" runat="server"/><!-- 客户名称 -->
<input type="hidden" id="comedate" name="comedate" runat="server"/> <!-- 入住时间 -->
<input type="hidden" id="GuestMobile" name="GuestMobile" runat="server"/> <!-- 客户手机 -->
<input type="hidden" id="cityname" name="cityname" runat="server"/><!-- 城市名称 -->
<input type="hidden" id="preID" name="preID" runat="server"/><!-- 证件号码 -->
<input type="hidden" id="Type" name="Type" runat="server" /> 
</div>
<div  runat="server" id="HB">
<div>
    <table cellspacing="0" cellpadding="0" width="80%" border="0" style="float:right">
    <tbody>
        <tr>
            <td>
                <div class="toptitle">
                <asp:LinkButton ID="LinkButton1" runat="server" Text="人民币支付"  onclick="LinkButton1_Click" />
                 <a href="javascript:sterline(1)">英镑支付</a>
                 <a href="javascript:sterline(2)">美元货币</a>
                </div>
            </td>
        </tr>
    </tbody>
</table>
</div>
<div id="sterline" style="display:none">
    <asp:LinkButton ID="LinkButton2" runat="server" Text="线下汇款"  PostBackUrl="/User/Shopfee/UserOrderinfo.aspx?page=1" /><br />
    <asp:LinkButton ID="LinkButton3" runat="server" Text="Paypal支付"  PostBackUrl="/User/Shopfee/PaypalDefray.aspx?page=1" />
</div>
<div id="other" style="display: none">
    <asp:LinkButton ID="LinkButton4" runat="server" Text="Paypal支付"  PostBackUrl="/User/Shopfee/PaypalDefray.aspx?page=2" />
</div>
</div>
<div id="DD" runat="server">
<div id="main" class="rg_inout">
		<h1>第二步：确认订单&nbsp;<img src="../User/images/regl2.gif" /></h1>
        <ul style="height:70px; margin-left:65px">
        <li >
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatLayout="Flow" 
                onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Value="addre2" Selected="True"></asp:ListItem>
                <asp:ListItem Value="addre1">使用其它地址</asp:ListItem>
            </asp:RadioButtonList>
            </li>
        </ul>
		<ul>
			<li style="width:150px;text-align:right;"><b><font color="#FF0000">*</font> 收货人姓名：</b></li>
			<li>&nbsp;<asp:TextBox ID="Receiver" runat="server"></asp:TextBox></li>&nbsp;
            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="Receiver"
                ErrorMessage="收货人姓名不能为空!" SetFocusOnError="True"></asp:RequiredFieldValidator></ul>
		<div class="cleardiv"></div>
		<ul>
			<li style="width:150px;text-align:right;"><b><font color="#FF0000">*</font> 收货人地址：</b></li>
			<li>&nbsp;<asp:DropDownList ID="dddizhi" runat="server" 
                    onselectedindexchanged="dddizhi_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            <br /><asp:TextBox ID="Jiedao" runat="server" Width="409px"></asp:TextBox><asp:CheckBox ID="cbAdd" runat="server" />加入我的地址薄</li><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Jiedao"
                ErrorMessage="收货人地址不能为空!" SetFocusOnError="True"></asp:RequiredFieldValidator></ul>
		<div class="cleardiv" style="margin-top:30px"></div>
		<ul>
			<li style="width:150px;text-align:right;"><b><font color="#FF0000">*</font> 收货人邮箱：</b></li>
			<li>&nbsp;<asp:TextBox ID="Email" runat="server"></asp:TextBox></li><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="Email"
                ErrorMessage="收货人邮箱不能为空!" SetFocusOnError="True"></asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="Email"
                                        ErrorMessage="邮件地址不规范" ValidationExpression="^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$"></asp:RegularExpressionValidator></ul>
		
		<div class="cleardiv"></div>
				<ul>
				
			<li style="width:150px;text-align:right;"><b><font color="#FF0000">*</font> 联系电话：</b></li>
			<li>
                    <asp:TextBox ID="Phone" runat="server"></asp:TextBox> 格式:区号-号码
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Phone"
                        Display="Dynamic" ErrorMessage="联系电话不能为空!" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    
                        </li>
                        </ul>
				<ul>
			<li style="width:150px;text-align:right;"><b><font color="#FF0000">*</font> 邮政编码：</b></li>
			<li>&nbsp;
                    <asp:TextBox ID="ZipCode" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ZipCode"
                        Display="Dynamic" ErrorMessage="邮政编码不能为空!" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </li></ul>
<%--				<ul>
			<li style="width:150px;text-align:right;"><b><font color="#FF0000">*</font> 收货人手机：</b></li>
			<li>&nbsp;
                    <asp:TextBox ID="Mobile" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="Mobile"
                        Display="Dynamic" ErrorMessage="收货人手机不能为空!" SetFocusOnError="True"></asp:RequiredFieldValidator>
                    </li></ul>--%>
				<ul>
			<li style="width:150px;text-align:right;"><b><font color="#FF0000">*</font> 送货方式：</b></li>
			<li>&nbsp;<asp:DropDownList ID="Delivery" runat="server" AutoPostBack="True" 
                    onselectedindexchanged="Delivery_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Delivery"
                        ErrorMessage="送货方式不能为空!" SetFocusOnError="True"></asp:RequiredFieldValidator></li></ul>
         
                        
				<ul>
			<li style="width:150px;text-align:right;"><b><font color="#FF0000">*</font> 运费：</b></li>    
			<li>&nbsp;<asp:Label ID="lblYunFei" runat="server" ></asp:Label></li>
			<li style="width:150px;text-align:right;"></li>
			<li>&nbsp;</li><asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
              &nbsp;<asp:HiddenField ID="projiect" runat="server" />
                    <asp:HiddenField ID="prodectid" runat="server" />
                    <asp:HiddenField ID="projectjiage" runat="server" />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField  ID="hfproclass" runat="server" />
                </ul>
		<div class="cleardiv"></div>
		<asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
		<div class="cleardiv"></div>
		<ul>
			<li style="width:150px;text-align:right;">&nbsp;</li>
			<li><asp:Button ID="Button2" runat="server" Text="提交订单" OnClick="Button2_Click" />&nbsp;</li>
			</ul>
	</div>
</div>
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