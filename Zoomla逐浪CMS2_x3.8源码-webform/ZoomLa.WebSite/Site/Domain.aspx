<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Domain.aspx.cs" Inherits="Manage_Site_DomReg" MasterPageFile="~/Manage/Site/SiteMaster2.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>域名注册</title>
    <link rel="stylesheet" href="/Plugins/Domain/css/style.css" type="text/css" media="all" />
    <link rel="stylesheet" href="/Plugins/Domain/css/css.css" type="text/css" />
    <script type="text/javascript" src="/Plugins/Domain/Site.js"></script>
    <style type="text/css">
        .dpclass { width: 200px;}
        .allChk {margin-left: 2px;position: relative;bottom: 2px;}
        .leftnone {margin-left: 0px;}
         body, button, input, select, textarea {font: 14px/1.6 Arial,"Microsoft YaHei";color: #444;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="site_main">
        <div id="stepOneDiv"  runat="server" visible="false" >
           <div class="domainContont">
		<div class="cartSteps">
			<dl >
				<dd class="stepLeft_hover"></dd>
				<dd class="stepOne_hover">1.查询域名并加入购物车<span class="step_one"></span></dd>
				<dd class="stepTwo">2.购物车<span class="step_one"></span></dd>
				<dd class="stepThree">开始支付</dd>
				<dd class="stepRight"></dd>
			</dl>
		</div>
	       </div>
           <div class="domainlist">
        <table style="width:540px;" border="0" class="allDomain"><tbody>
             <asp:Repeater runat="server" ID="domRepeater">
                 <ItemTemplate>
               <td style="width:30px;">
                   <input type="checkbox" name="domainChk" <%#GetCheck(Eval("isRegAble"),Eval("DomPrice"))%> class="cb" value='<%#Eval("domName") %>'></td>
               <td style="width:160px;"><span class="domainResult"><%#Eval("DomName")%></span></td>
               <td style="width:160px;"><span class="domainResult"><%#Eval("DomPrice")%></span></td>
                   <td style="width:200px;">
                   <%#GetRegAble(Eval("isRegAble"))%> 
                   </td></tr>
                 </ItemTemplate>
             </asp:Repeater>
           </tbody></table>
           <table style="width:100%;" border="0" class="allDomain">
           <tbody>
           <tr><td colspan="3" align="center"><asp:Button runat="server" ID="stepOneBtn" Text="加入购物车" CssClass="site_button" OnClick="stepOneBtn_Click" /></td></tr>
           </tbody>
           </table>
           </div>
        </div>
        <div id="stepTwoDiv" runat="server" visible="false" >
            <div class="cartSteps">
			<dl>
				<dd class="stepLeft"></dd>
				<dd class="stepOne">1.查询域名并加入购物车<span></span></dd>
				<dd class="stepTwo hover">2.购物车结算<span></span></dd>
				<dd class="stepThree">开始支付</dd>
				<dd class="stepRight"></dd>
			</dl>
		</div>
            <table style="width:540px;" border="0" id="twoStepTable">
                <tr><td></td><td>域名</td><td>年限</td><td>单价</td><td>金额</td><td>操作</td></tr>
                <tbody>
             <asp:Repeater runat="server" ID="twoRepeater">
                 <ItemTemplate>
                     <tr>
                         <td><input type="checkbox" name="twoStepChk" value="<%#Eval("DomName") %>" checked="checked"></td>
                         <td><%#Eval("DomName") %></td>
                         <td><select name="twoYearSelect" onchange="UpdateMoney(this);">
                             <option value="1">1年</option>
                             <option value="2">2年</option>
                             <option value="3">3年</option>
                             <option value="4">4年</option>
                             <option value="5">5年</option>
                             </select>
                             <input type="hidden" name="twoHid" value="<%#Eval("DomName") %>" />
                         </td>
                         <td name="unitPrice"><%#Eval("DomPrice") %></td>
                         <td name="totalPrice"><%#Eval("Money") %></td>
                         <td>
                             <a href="CartEdit.aspx?Index=<%#Eval("Index") %>" target="_blank" title="编辑"><img src="/Images/ModelIcon/Edit.gif" /></a>
                             <!--disDiv('tempInfoDiv');getTempValue(this);-->
                             <a href="javascript:;" onclick="delTr(this);" title="删除"><img src="/App_Themes/AdminDefaultTheme/images/del.png" /></a>
                         </td>
                     </tr>
                 </ItemTemplate>
             </asp:Repeater>    
           </tbody></table>
            <table style="width:100%;" border="0" class="allDomain">
           <tbody>
            <tr><td colspan="3" align="center">
                <asp:Button runat="server" ID="stepTwoBtn" Text="结算" CssClass="site_button" OnClick="stepTwoBtn_Click"/>
                <asp:Button runat="server" ID="clearCartBtn" Text="清空购物车" CssClass="site_button" OnClick="clearCartBtn_Click" />
                </td>
            </tr>
           </tbody>
           </table>
            <div class="cart_info">
			<div class="head"><p>我的购物车</p><span class="cart-btn"><a href="https://www.ename.net/cart/deleteall" onclick="return confirmDelCart()">清空购物车</a></span></div>
			<div class="left">
				<div><p>可用余额：<span>￥</span><asp:Label runat="server" ID="purseL"></asp:Label><span class="recharge">
                    <a href="/PayOnline/OrderPay.aspx?Money=200" target="_blank">充值</a></span></p>
				 </div>
				<div><p>本次总消费：<span>￥</span><span id="allMoney"><asp:Label runat="server" ID="allMoneyL"></asp:Label></span></p>
				<p>本次需支付：<span>￥</span><span class="resultMoney"><asp:Label runat="server" ID="resultMoneyL" /></span></p></div>
			</div>
		</div>
        </div>
        <div id="stepFourDiv" runat="server" class="allDomain" visible="false" >
  <div class="cartSteps">
		   <dl>
                <dd class="stepLeft"></dd>
				<dd class="stepTwo" style="width:33%;">1.查询域名并加入购物车<span></span></dd>
				<dd class="stepTwo">2.购物车结算<span class="step_one"></span></dd>
				<dd class="stepThree hover">开始支付<span></span></dd>
				<dd class="stepRight"></dd>
			</dl>
      <table style="width:540px;" border="0">
           <tr><td>注册信息</td><td>域名</td><td>年限</td><td>总计</td><td>操作</td></tr><tbody>
      <asp:Repeater runat="server" ID="fourthRepeater">
          <ItemTemplate><tr>
                  <td><%#GetIsOK(Eval("Isok")) %></td><!--用于注册的信息是否完整-->
                  <td><%#Eval("DomName") %><input type="hidden" name="fourthHid" value="<%#Eval("DomName") %>" /></td>
                  <td><%#Eval("Year") %></td>
                  <td><%#Eval("Money") %></td>
                  <td><a href="javascript:;" onclick="editFrame(<%#Eval("Index") %>);" title="编辑"><img src="/Images/ModelIcon/Edit.gif" /></a>
                      <a href="javascript:;" onclick="delTr(this);" title="删除"><img src="/App_Themes/AdminDefaultTheme/images/del.png" /></a></td>
              </tr></ItemTemplate>
       </asp:Repeater>
       </tbody></table>
      <table style="width:100%;" border="0" class="allDomain">
           <tbody>
            <tr><td colspan="3" align="center"><asp:Button runat="server" ID="fourthBtn" Text="确定购买" CssClass="site_button" OnClick="FourthBtn_Click"/></td></tr>
           </tbody>
           </table>
      <asp:Button runat="server" ID="checkFuncBtn" ClientIDMode="Static" OnClick="CheckFuncBtn_Click" style="display:none;" />
      <div id="lastEditDiv">
          <iframe id="lastEditFrame" height="1000"  src="CartEdit.aspx?Index=0" style="width:98%;" frameborder=0 scrolling=no></iframe>
      </div>
     </div>
    </div>
        <div runat="server" id="regDiv" class="domain_register">
		<div class="domain_left">www.</div>
		<div class="domain_middle">
			<textarea  name="domainBody" id="Textarea1" class="medium small domain_textarea" style="color:#DDD;" onfocus="empty(this);" onblur="fill(this);"><%=prompt %></textarea>
			<br />
            <asp:Button runat="server" ID="Button1" Text="查询" CssClass="btn btn-primary" OnClick="checkBtn_Click"/>
		</div>
		<div class="domain_right">
			<div class="tab">
				<div class="tab_menu">
					<ul class="r_tit">
						<li class="selected"><a href="javascript:void(0)">域名</a></li>
					</ul>
					<p class="checkall"><span id="Span1">&nbsp;</span></p>
				</div>
				<div class="tab_box">
    <div id="Div1" style="display: block;">
        <p class="checkall0 allcheck"><label for="allChk">全选</label><input type="checkbox" id="Checkbox1" class="allChk" onclick="selectAllByName(this, 'ext');"/></p>
        <ul id="Ul1" runat="server"></ul>
    </div>
    </div>
    </div>
    </div>
    </div>
     </div><br />
    <input type="hidden" id="tempID" />
    <script type="text/javascript">
        function checkFunc()
        {
            $("#<%=checkFuncBtn.ClientID%>").trigger("click");
        }
        function editFrame(index)
        {
            //$("#lastEditDiv").show();
            $("#lastEditFrame").attr("src", "CartEdit.aspx?Index="+index);
        }
        //-----------------第二步
        function UpdateMoney(obj) {
            ye = $(obj).select().val();//年限
            up = $(obj).parent().siblings("[name='unitPrice']").text();//单价
            tp = up * ye;
            $(obj).parent().siblings("[name='totalPrice']").text(tp + ".00");
            //更新本行的价格,再汇总所有的money更新总计
            TotalMoney();
        }
        //统计所有金额
        function TotalMoney() {
            arr = $("#twoStepTable tr>td[name='totalPrice']");
            total = 0;
            for (var i = 0; i < arr.length; i++) {
                total += parseInt(arr[i].innerText);
            }
            $("#<%=allMoneyL.ClientID%>").text(total);
                    $("#<%=resultMoneyL.ClientID%>").text(total);
        }
        function disDiv(id) {
            $("#" + id).show();
        }
        function getTempValue(obj)
        {
            a="getTempValue";
            v = $(obj).parent().parent().find("[name='twoTempSelect']").select().val();
            $("#tempID").val(v);//存最近一次的更改
            $.ajax({
                type: "Post",
                url: "Default.aspx",
                //dataType: "json",
                data: { action:a, value: v },
                success: function (data)
                {
                    setValue(data);
                },
                error: function (data) { alert("失败"); }
            });
            // return result;
        }
        //--------------
        function clearSelect(cn)
        {
          $("select:[name='" + cn + "']").empty();
        }
        function setSelectValue(cn, dpValue) {
            var temp, arr = dpValue.split(",");
            for (var i = 0; i < arr.length; i++) {
                temp = arr[i].split(":");
                $("select:[name='" + cn + "']").append("<option value='" + temp[0] + "'>" + temp[1] + "</option>");
            }
        }
        //更改选中
        function setSelectOption(cn) {
            $("select:[name='" + cn + "'] option:[value='-1']").attr('selected', 'true')
        }
        function delTr(obj) {
            if(confirm("你确定要删除吗!!"))
            $(obj).parent().parent().remove();
        }
        function setDefaultCheck(v) {
            a = v.split(',');
            for (var i = 0; i < a.length; i++) {
                $("input:[value='" + a[i] + "']").attr("checked", "checked");
            }
        }
        //------
        function empty(obj) {
            if ($(obj).val() == "<%=prompt %>") {
                $(obj).css("color", "black");
                $(obj).val("");
            }
        }
        function fill(obj) {
            if ($(obj).val() == "") {
                $(obj).css("color", "#DDD");
                $(obj).val("<%=prompt %>");
            }
        }
    </script>
</asp:Content>

