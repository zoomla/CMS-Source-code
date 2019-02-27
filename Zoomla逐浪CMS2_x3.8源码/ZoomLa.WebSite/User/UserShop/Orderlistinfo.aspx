<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Orderlistinfo.aspx.cs" Inherits="User_UserShop_Orderlistinfo" MasterPageFile="~/User/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>订单信息</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<%-- <asp:ImageButton runat="server" ID="btnPre2" ImageUrl="/images/up.gif" OnClick="btnPre_Click" ToolTip="上一张" Visible="false" />
<asp:ImageButton runat="server" ID="btnNext2" ImageUrl="/images/down.gif" OnClick="btnNext_Click" ToolTip="下一张" Visible="false" />--%>
 <div id="pageflag" data-nav="shop" data-ban="store"></div> 
<div class="container margin_t5">
	<ol class="breadcrumb">
		<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
		<li><a href="ProductList.aspx">我的店铺</a></li>
		<li class="active">订单详情</li> 
	</ol>
</div>
<div class="container">
	<ul class="nav nav-tabs" role="tablist">
	<li role="presentation" class="active"><a href="#OrderState" aria-controls="OrderState" role="tab" data-toggle="tab">订单状态</a></li>
	<li role="presentation"><a href="#Logistics" aria-controls="Logistics" role="tab" data-toggle="tab">物流管理</a></li>
	<li role="presentation"><a href="#Financial" aria-controls="Financial" role="tab" data-toggle="tab">财务管理</a></li>
	<li role="presentation"><a href="#Selled" aria-controls="Selled" role="tab" data-toggle="tab">售后管理</a></li>
	</ul>
	<!--startprint-->
	<div class="tab-content">
		<div role="tabpanel" class="tab-pane active" id="OrderState">
			<table class="table table-striped table-bordered table-hover">
				<tbody id="Tbody1">
					<tr>
						<td colspan="4" align="center" class="title">
							<asp:Label ID="Label1" runat="server"></asp:Label>&nbsp;
						</td>
					</tr>
					<tr>
						<td align="left" style="width: 13%;">客户名称：<asp:Label ID="Reuser" runat="server"></asp:Label></td>
						<td align="left" style="width: 14%;">用 户 名：<span id="Rename" runat="server"></span></td>
						<td align="left" style="width: 13%;">购买日期：<asp:Label ID="adddate" runat="server"></asp:Label></td>
						<td align="left" style="width: 20%;">下单日期：<asp:Label ID="addtime" runat="server"></asp:Label></td>
					</tr>
					<tr>
						<td align="left" style="width: 13%;">需开发票：<asp:Label ID="Invoiceneeds" runat="server"></asp:Label></td>
						<td align="left" style="width: 14%;">已开发票：<asp:Label ID="Developedvotes" runat="server"></asp:Label></td>
						<td align="left" style="width: 13%;">付款状态：<asp:Label ID="Paymentstatus" runat="server"></asp:Label></td>
						<td align="left" style="width: 20%;">物流状态：<asp:Label ID="StateLogistics" runat="server"></asp:Label></td>
					</tr>
				</tbody>
			</table>
		</div>
		<div role="tabpanel" class="tab-pane" id="Logistics">
			<table class="table table-striped table-bordered table-hover">
				<tbody id="Tbody3">
					<tr>
						<td align="center" style="width: 50%;">
							<table class="table table-bordered table-striped">
								<tr>
									<td width="28%" align="right">收货人姓名：</td>
									<td width="72%" align="left">&nbsp;<asp:Label ID="Reusers" runat="server"></asp:Label></td>
								</tr>
								<tr>
									<td align="right">收货人地址：
									</td>
									<td align="left">&nbsp;<asp:Label ID="Jiedao" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td align="right">收货人邮箱：
									</td>
									<td align="left">&nbsp;<asp:Label ID="Email" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td align="right" style="height: 24px">付款方式：
									</td>
									<td align="left" style="height: 24px">&nbsp;<asp:Label ID="Payment" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td align="right">发票信息：
									</td>
									<td align="left">&nbsp;<asp:Label ID="Invoice" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td align="right">缺货处理：
									</td>
									<td align="left">&nbsp;<asp:Label ID="Outstock" runat="server"></asp:Label>
									</td>
								</tr>
								<tr>
									<td align="right">订单类型：
									</td>
									<td align="left">&nbsp;<asp:Label ID="Ordertype" runat="server"></asp:Label>
									</td>
								</tr>
					
								<% if (PayClass == 1)
								   {%>
								<tr>
									<td align="right" style="height: 24px">淘宝交易号：
									</td>
									<td align="left" style="height: 24px">&nbsp;<asp:Label ID="lbAlipayNO" runat="server"></asp:Label>
									</td>
								</tr>
								<%} %>
							</table>
						</td>
						<td width="50%" align="center">
							<table class="table table-bordered table-striped">
									<tr>
										<td width="28%" align="right">联系电话：
										</td>
										<td width="72%" align="left">&nbsp;<asp:Label ID="Phone" runat="server"></asp:Label>
										</td>
									</tr>
									<tr>
										<td align="right" style="height: 24px">邮政编码：
										</td>
										<td align="left" style="height: 24px">&nbsp;<asp:Label ID="ZipCode" runat="server"></asp:Label>
										</td>
									</tr>
									<tr>
										<td align="right">收货人手机：
										</td>
										<td align="left">&nbsp;<asp:Label ID="Mobile" runat="server"></asp:Label>
										</td>
									</tr>
									<tr>
										<td align="right">送货方式：
										</td>
										<td align="left">&nbsp;<asp:Label ID="Delivery" runat="server"></asp:Label>
										</td>
									</tr>
									<tr>
										<td align="right">跟单员：
										</td>
										<td align="left">&nbsp;<asp:Label ID="AddUser" runat="server"></asp:Label>
										</td>
									</tr>
									<tr>
										<td align="right">订单状态：&nbsp;
										</td>
										<td align="left">&nbsp;<asp:Label ID="OrderStatus" runat="server"></asp:Label>
											<asp:Button ID="Button10" runat="server" CssClass="btn btn-primary" Visible="false" Text="服务记录" />
										</td>
									</tr>
									<tr>
										<td align="right" style="height: 24px">要求送货时间：
										</td>
										<td align="left" style="height: 24px">&nbsp;<asp:Label ID="Deliverytime" runat="server"></asp:Label>
										</td>
									</tr>
					
								</table>
							</td>
						</tr>
					</tbody>
				</table>
		</div>
		<div role="tabpanel" class="tab-pane" id="Financial">
				<table class="table table-striped table-bordered table-hover">
					<tr>
						<td align="right" style="width:200px;">内部记录（<a href="javascript:;" onclick="ShowMore('more_div');" title="编辑内部记录" style="color: #1963aa;">编辑</a>）：</td>
						<td align="left">
							<asp:Label ID="Internalrecords" runat="server"></asp:Label>
						</td>
					</tr>
					<tr>
						<td align="right">备注留言（<a href="javascript:;" onclick="ShowMore('more_div2');" title="编辑备注留言" style="color: #1963aa;">编辑</a>）：</td>
						<td align="left">
							<asp:Label ID="Ordermessage" runat="server"></asp:Label>
						</td>
					</tr>
				</table>
				<div class="panel panel-primary">
					<div class="panel-heading"><span class="fa fa-th"></span><span class="margin_l5">商品信息</span></div>
					<div class="panel-body">
				   <table id="proTable" class="table table-striped table-bordered table-hover">
					<tbody id="Tbody2">
						<tr>
							<td align="center" class="title" style="width: 10%;">图片</td>
							<td align="center" class="title" style="width: 10%;">商品编号</td>
							<td align="center" class="title" style="width: 18%;">商品名称</td>
							<td align="center" class="title" style="width: 6%;">PV</td>
							<td align="center" class="title" style="width: 10%;">实价(本店价)</td>
							<td align="center" class="title" style="width: 3%;">数量</td>
							<td width="3%" align="center" class="title">单位</td>
							<td width="6%" align="center" class="title">金额</td>
							<td width="8%" align="center" class="title">服务期限</td>
							<td width="12%" align="center" class="title">备注</td>
						</tr>
						<asp:Repeater ID="procart" runat="server" OnItemDataBound="cartinfo_ItemDataBound">
							<ItemTemplate>
								<tr
									<%#(Eval("Bindpro","{0}")=="")?"":"style=background-color:#E6E6E6"%>>
									<td ><img src="<%#ZoomLa.Common.function.GetImgUrl(Eval("Thumbnails"))%>" onerror="shownopic(this);" class="img_50" /></td>
									<td style="width: 6%;"><%#Eval("ProID")%></td>
									<td><a href='<%#GetShopUrl()%>' target='_blank'><%#Eval("proname")%></a></td>
									<td><%#Eval("PointVal") %></td>
									<td><%#GetLinPrice()%></td>
									<td><%#Eval("pronum") %></td>
									<td><%#Eval("Danwei") %></td>
									<td><%#GetPrice()%></td>
									<td ><%#qixian(DataBinder.Eval(Container, "DataItem.ProID", "{0}"))%></td>
									<td><%#beizhu(DataBinder.Eval(Container, "DataItem.ProID", "{0}"))%></td>
								</tr>
								<asp:Repeater ID="Repeater1" runat="server">
									<ItemTemplate>
										<tr
											style="background-color: #F6F6F6;">
											<td align="center">
												<img src="<%#ZoomLa.Common.function.GetImgUrl(Eval("Thumbnails"))%>" onerror="shownopic(this);" class="img_50" />
											</td>
											<td align="center">
												<%#Getprotype()%><%#Eval("proname")%>
											</td>
											<td width="6%" align="center"><%#Eval("ProUnit")%></td>
											<td width="6%" align="center">1
											</td>
											<td width="6%" align="center">
												<%#GetProPrice(Eval("proclass", "{0}"), "1", DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
											</td>
											<td width="6%" align="center">
												<%#GetProPrice(Eval("proclass", "{0}"), "2", DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
											</td>
											<td width="6%" align="center">-</td>
											<td width="6%" align="center">-</td>
											<td width="8%" align="center">- </td>
										</tr>
									</ItemTemplate>
								</asp:Repeater>
							</ItemTemplate>
						</asp:Repeater>
						<tr>
							<td colspan="8">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
							</td>
							<td align="left" colspan="3">合计： &nbsp;<asp:TextBox ID="Label2" runat="server" CssClass=" form-control" Width="116px"></asp:TextBox>&nbsp;
								<asp:Button ID="Button8" runat="server" Text="修改" CssClass="btn btn-primary" OnClick="Button8_Click" />
								&nbsp;
							</td>
						</tr>
						<tr>
							<td colspan="11" style="height: 24px">&nbsp;运费：<asp:Label ID="Label32" runat="server"></asp:Label>
							</td>
						</tr>
						<tr>
							<td colspan="11" style="height: 24px">&nbsp;发票税率：<asp:Label ID="lblInv" runat="server"></asp:Label>
							</td>
						</tr>
						<tr>
							<td colspan="11">
								<table style="width:100%;">
									<tr>
										<td style="width:50%;text-align:left;">&nbsp;实际金额：
											<asp:Label ID="Label29" runat="server"></asp:Label>
											+
											<asp:Label ID="Label30" runat="server"></asp:Label>＝<asp:Label ID="Label31" runat="server"></asp:Label>

											&nbsp; &nbsp;  <span>赠送积分数</span>：<asp:Label ID="LabScore" runat="server"></asp:Label>分 <font color="blue"><asp:Label ID="ISsend" runat="server"></asp:Label></font>
										</td>
										<td style="width:50%;text-align:right;">&nbsp;已付款：<asp:Label ID="Label28" runat="server"></asp:Label></td>
									</tr>
								</table>
							</td>
						</tr>
					</tbody>
				</table>
					</div>
				</div>
		</div>
		<div role="tabpanel" class="tab-pane" id="Selled">
			<div class="panel panel-primary">
				<div class="panel-heading"><span class="fa fa-user"></span><span class="margin_l5">客户详情</span></div>
				<div class="panel-body">
					<table class="table table-bordered">
						<tr>
							<td class="td_m">姓名</td>
							<td>证件号</td>
							<td>手机</td>
						</tr>
						<asp:Repeater runat="server" ID="UserRPT" EnableViewState="false">
							<ItemTemplate>
								<tr>
									<td><%#Eval("Name") %></td>
									<td><%#Eval("CertCode") %></td>
									<td><%#Eval("Mobile") %></td>
								</tr>
							</ItemTemplate>
						</asp:Repeater>
					</table>
				</div>
			</div>
			<table class="table table-striped table-bordered table-hover">
				<tr><td colspan="4" class="text-center">退款操作</td></tr>
				<tr>
					<td style="width:10%;">用户退款理由：</td><td style="width:40%"><asp:Label ID="DrawBackStr" runat="server"></asp:Label></td>
					<td style="width:10%;"><asp:Label ID="ReturnStatu_L" runat="server"></asp:Label>退款理由：</td><td style="width:40%"><asp:Label ID="isCheckRe_L" runat="server"></asp:Label></td>
				</tr>
				<tr> 
					<td colspan="4" class="text-center">
						<asp:Button ID="CheckReturn" Enabled="false" CssClass="btn btn-primary" runat="server" Text="确认退款" OnClientClick="return ShowDrawDiag(1);" />
						<asp:Button ID="UnCheckRetrun" Enabled="false" CssClass="btn btn-primary" Width="100" runat="server" Text="拒绝退款" OnClientClick="return ShowDrawDiag(2);" />
					</td>
				</tr>
			</table>
		</div>
	</div>
	<!--endprint-->
	<table class="table table-striped table-bordered table-hover">
		<tr>
			<td width="20%">
				<asp:Button ID="SureOrder_Btn" Enabled="false" CssClass="btn btn-primary" runat="server" Text="确认订单" OnClick="SureOrder_Btn_Click" />
			</td>
			<td width="20%">
				<asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="已经支付" OnClick="Button2_Click" OnClientClick="return confirm('确定修改为已支付吗');" />
				<asp:Button ID="CompleteOrder_Btn" runat="server" Text="完结订单" CssClass="btn btn-primary" OnClick="CompleteOrder_Btn_Click"  OnClientClick="return confirm('确定完结订单吗!');" />
			</td>
			<td width="20%">
				<asp:Button ID="Button13" CssClass="btn btn-primary" runat="server" Text="暂停处理" OnClick="Button13_Click" />
			</td>
			<td width="20%">
				<asp:Button ID="Button9" CssClass="btn btn-primary" runat="server" Text="冻结订单" OnClick="Button9_Click" />
			</td>
			<td width="20%" rowspan="3">
				<asp:Button ID="BT_FreeSplit"
					runat="server" CssClass="btn btn-primary" Text="自由拆分" OnClick="BT_FreeSplit_Click" Enabled="false" /><br />
				<asp:Button ID="sendScore" Visible="false" class="btn btn-primary" runat="server"/><br />
			</td>
		</tr>
		<tr>
			<td><asp:Button ID="Button5" CssClass="btn btn-primary"  runat="server" Text="取消确认" OnClick="Button5_Click" /></td>
			<td>
				<asp:Button ID="Button7" CssClass="btn btn-primary"  runat="server" Text="客户已签收"  OnClick="Button7_Click" />
			</td>
			<td>
				<asp:Button ID="Button14" CssClass="btn btn-primary"  runat="server" Text="恢复正常" OnClick="Button14_Click" />
			</td>
			<td>
				<asp:Button ID="Button11" CssClass="btn btn-primary"  runat="server" Text="开发票" OnClick="Button11_Click" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:Button CssClass="btn btn-primary" runat="server" Text="确认发货" OnClientClick="return ShowSendGood();" id="ShowSend_Btn" />
			</td>
			<td>
				<asp:Button ID="Button6" CssClass="btn btn-primary"  runat="server" Text="订单作废" OnClick="Button6_Click" />
			</td>
			<td>
				<input type="button" name="delorder" value="删除订单" onclick="showhide()" class="btn btn-primary"  />
			</td>
			<td>
				<asp:Button ID="Button15" CssClass="btn btn-primary"  runat="server" Text="打印订单" OnClientClick="preview();return false;" />
			</td>
		
		</tr>
		<tbody id="fahuo" style="display: none">
			<tr>
				<td colspan="5">
					<table id="isAlipay">
						<tr>
							<td align="right">淘宝交易号：
							</td>
							<td>
								<asp:TextBox ID="trade_no" runat="server" CssClass="l_input"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td align="right">发货类型：
							</td>
							<td>
								<asp:DropDownList ID="DropDownList1" runat="server">
									<asp:ListItem Value="EMS">EMS</asp:ListItem>
									<asp:ListItem Value="POST">平邮</asp:ListItem>
									<asp:ListItem Value="EXPRESS">快递</asp:ListItem>
								</asp:DropDownList>
							</td>
						</tr>
						<tr>
							<td align="right">物流公司名称：</td>
							<td>
								<asp:TextBox ID="logistics_name" runat="server" CssClass="l_input"></asp:TextBox>
							</td>
						</tr>
						<tr>
							<td align="right">物流发货单号：
							</td>
							<td>
								<asp:TextBox ID="txtMS" runat="server" CssClass="l_input"></asp:TextBox>
							</td>
						</tr>
					</table>
					<table id="noAlipay">
						<tr>
							<td>物流公司：<asp:DropDownList runat="server" ID="DR_Company" AppendDataBoundItems="True" onchange="showtxt(this)">
								<asp:ListItem Value="0">请选择快递公司</asp:ListItem>
								<asp:ListItem Value="77">其他</asp:ListItem>
							</asp:DropDownList>&nbsp;&nbsp; </td>
							<td>
								<input id="kdgs" class="l_input" style="display: none;" type="text" runat="server" />
							</td>
							<td>快递单号：<asp:TextBox ID="txtMSnoAlipay" runat="server" CssClass="l_input"></asp:TextBox>
								<asp:RegularExpressionValidator ID="RV1" runat="server"
									ErrorMessage="快递单号最少为五位" ControlToValidate="txtMSnoAlipay" ValidationExpression="^.{5,}$"></asp:RegularExpressionValidator>
							</td>
						</tr>
					</table>
					<asp:Button ID="SendGoods_Btn" runat="server" OnClick="SendGoods_Btn_Click" Text="发货" CssClass="btn btn-primary"></asp:Button>
				</td>
			</tr>
		</tbody>
		<tr>
			<td colspan="5" style="text-align: center; padding-top: 5px;">&nbsp;
			   <asp:Button ID="btnPre" runat="server" Text="上一个订单" Visible="false" CssClass="btn btn-primary" />
				<asp:Button ID="btnNext" runat="server" Text="下一个订单" Visible="false"  CssClass="btn btn-primary" />
			</td>
		</tr>
	</table>
	<div style="position: absolute; display: none; width: 300px; background: #e8f5ff; margin: auto; top: 20%; left: 50%; margin-left: -150px;" id="hidediv">
		<div id="HavePay" runat="server">
			<table class="table table-striped table-bordered table-hover">
				<tbody runat="server" id="payed">
					<tr><td colspan="2" style="text-align: center;">确认删除</td></tr>
					<tr> <td colspan="2" style="text-align: center; color: red;">确认删除该订单？</td></tr>
					<tr>
						<td style="width: 80px;">支付方式：</td>
						<td>
							<asp:Label ID="PayType" runat="server" Text="Label"></asp:Label></td>
					</tr>
					<tr>
						<td>支付金额：</td>
						<td>
							<asp:Label ID="PayMoney" runat="server" Text="Label"></asp:Label></td>
					</tr>
					<tr>
						<td>退还金额：</td>
						<td>
							<asp:CheckBox ID="ReturnMoney" runat="server" /><span style="color: #F00; margin-left: 5px;">*选中后退还金额</span></td>
					</tr>
				</tbody>
				<tbody id="nopayed" runat="server" visible="false">
					<tr>
						<td colspan="2" style="text-align: center;">确认删除</td>
					</tr>
					<tr>
						<td colspan="2" style="text-align: center;">该订单尚未付款，确认删除该订单？</td>
					</tr>
				</tbody>
				<tr>
					<td colspan="2" style="text-align: center">
						<asp:Button ID="SureDelBtn" CssClass="btn btn-primary" runat="server" Text="确认删除" OnClick="SureDelBtn_Click" />&nbsp;&nbsp;
						<input type="button" name="back" value="取消" onclick='$("#hidediv").hide("fast");' class="btn btn-primary" />
					</td>
				</tr>
			</table>
		</div>
	</div>
	<div id="more_div" class="border more1">
		<div class="moreTitle"><span class="closeSpan" onclick="HideMoreF(this);">关闭</span>内部记录</div>
		<div style="width: 300px; height: 300px; margin: auto;" class="border" runat="server" id="inter_Div"></div>
		<div class="moreContent">
			<asp:TextBox runat="server" ID="inter_Text" TextMode="MultiLine" Style="height: 300px; margin: 2px 2px 2px 5px; width: 98%;"></asp:TextBox>
		</div>
		<div class="moreBottom">
			<input type="button" onclick="RefreshDiv();" value="刷新预览" class="btn btn-primary" style="margin-right: 10px;" />
			<asp:Button runat="server" ID="inter_Save_Btn" Text="修改" Visible="false" class="btn btn-primary" />
		</div>
	</div>
	<div id="more_div2" class="border more">
		<div class="moreTitle"><span class="closeSpan" onclick="HideMoreF(this);">关闭</span>备注信息</div>
		<asp:TextBox runat="server" ID="omg_Text" TextMode="MultiLine" Style="width: 99%; height: 100%;" class="l_input"></asp:TextBox>
		<div class="moreBottom">
			<asp:Button runat="server" ID="omg_Save_Btn" Text="修改" Visible="false" class="btn btn-primary" />
		</div>
	</div>
	<div class="modal" id="userinfo_div">
		<div class="modal-dialog" style="width: 600px;">
			<div class="modal-content">
				<div class="modal-header">
					<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">×</span><span class="sr-only">Close</span></button>
					<span class="modal-title"><strong id="title">用户信息</strong></span>
				</div>
				<div class="modal-body">
					  <iframe id="user_ifr" style="width:100%;height:400px;border:none;" src=""></iframe>
				</div>
			</div>
		</div>
	</div>
	<div id="chkreturn_div" style="display:none;">
		<table class="table table-bordered table-striped">
			<tr>
				<td style="width:20%;" class="text-right">订单号：</td><td><asp:Label ID="OrderNo_L" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td style="width:20%;" class="text-right">订单金额：</td><td><asp:Label ID="Orderamounts_L" runat="server"></asp:Label> </td>
			</tr>
			<tr>
				<td style="width:20%;" class="text-right">下单日期：</td><td><asp:Label ID="Cdate_L" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td style="width:20%;" class="text-right">理由：</td><td><asp:TextBox runat="server" ID="Back_T" TextMode="MultiLine" CssClass="form-control" style="height:120px;width:100%;max-width:100%;" /></td>
			</tr>
			<tr>
				<td class="text-center" colspan="2">
					<asp:Button ID="CheckReturn_B" CssClass="btn btn-primary" Text="确认" OnClick="CheckReturn_Click" runat="server" />
					<asp:Button ID="UCheckReturn_B" CssClass="btn btn-primary" Text="确认" OnClientClick="return precheck();" OnClick="UnCheckRetrun_Click" runat="server" />
					<button class="btn btn-primary" type="button" onclick="closeReturn()">取消</button>
				</td>
			</tr>
		</table>
	</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript">
	function precheck() {
		var val = $("#Back_T").val();
		if (val == "" || val.length < 5) { alert("理由最少需要5个字符"); return false; }
		return true;
	}
	function preview() {
		pageload();
	}
	//打印功能
	function pageload() {
		bdhtml = window.document.body.innerHTML;
		sprnstr = "<!--startprint-->";
		eprnstr = "<!--endprint-->";
		prnhtml = bdhtml.substr(bdhtml.indexOf(sprnstr) + 17);
		prnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));
		window.document.body.innerHTML = prnhtml;
		window.print();
	}
	//发货弹出框(后期改为独立页面)
	function ShowSendGood() {
		var fahuo = document.getElementById("fahuo").style;
		var isAlipay = document.getElementById("isAlipay").style;
		var noAlipay = document.getElementById("noAlipay").style;
		if ('<%=PayClass%>' == 1) {
			isAlipay.display = "";
			noAlipay.display = "none";
		}
		else {
			isAlipay.display = "none";
			noAlipay.display = "";
		}
		if (fahuo.display == "") {
			fahuo.display = "none";
		} else {
			fahuo.display = "";
		}
		return false;
	}
	function opentitle(url, title) {
		$("#title").text(title);
		$("#user_ifr").attr("src", url);
		$("#userinfo_div").modal({});
	}
	function showtxt(obj) {
		if (obj.options[obj.selectedIndex].value == "77") {
			var txt = document.getElementById("kdgs");
			txt.style.display = "";

		} else {
			var txt = document.getElementById("kdgs");
			txt.style.display = "none";
		}
	}
	var DrawDiag = new ZL_Dialog();
	function ShowDrawDiag(type) {
		DrawDiag.title = type == 1 ? "确认退款" : "取消退款";
		DrawDiag.content = "chkreturn_div";
		DrawDiag.ShowModal();
		if (type == 1) {
			$("#CheckReturn_B").show();
			$("#UCheckReturn_B").hide();
		}
		else {
			$("#UCheckReturn_B").show();
			$("#CheckReturn_B").hide();
		}
		return false;
	}
	function closeReturn() {
		DrawDiag.CloseModal();
	}
	function showhide() {
		$("#hidediv").toggle("fast");
	}
	function ShowMore(id) {
		$("#more_div").hide();
		$("#more_div2").hide();
		$("#" + id).show();
	}
	function HideMoreF(obj) {
		$(obj).parent().parent().hide();
	}
	function RefreshDiv() {
		v = $("#inter_Text").val();
		$("#inter_Div").html(v);
	}
</script>
<style type="text/css">
	.more1 {width: 760px;height: 680px;position: absolute;display: none;top: 100px;border-radius: 3px;left: 20%;}
	.more {width: 400px;height: 300px;position: absolute;display: none;top: 100px;border-radius: 3px;left: 30%;}
	.moreTitle {text-align: center; background-color: #08C;font-family: 'Microsoft YaHei';font-size: 14px;color: white;height: 30px;padding-top: 5px;}
	.moreBottom {text-align: center;font-family: 'Microsoft YaHei';margin-top: -3px;}
	.closeSpan {float: right;margin-right: 10px; cursor: pointer;}
</style>
</asp:Content>