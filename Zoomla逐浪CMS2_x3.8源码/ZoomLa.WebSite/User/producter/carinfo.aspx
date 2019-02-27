<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="carinfo.aspx.cs" Inherits="User_producter_carinfo" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>订单信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">订单信息</li>
    </ol>
    <table class="table table-striped table-bordered table-hover">
        <tbody id="Tbody1">
            <tr>
                <td colspan="5" class="text-center">
                    <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="text-left" style="width: 13%;">客户名称：<asp:Label ID="Reuser" runat="server" Text=""></asp:Label></td>
                <td class="text-left" style="width: 14%;">用 户 名：<asp:Label ID="Rename" runat="server" Text=""></asp:Label></td>
                <td class="text-left" style="width: 13%;">购买日期：<asp:Label ID="adddate" runat="server"></asp:Label></td>
                <td width="20%" class="text-left" >下单日期：<asp:Label ID="addtime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-left" style="width: 13%; ">需开发票：<asp:Label ID="Invoiceneeds" runat="server"></asp:Label></td>
                <td class="text-left" style="width: 14%; ">已开发票：<asp:Label ID="Developedvotes" runat="server"></asp:Label></td>
                <td class="text-left" style="width: 13%; ">付款状态：<asp:Label ID="Paymentstatus" runat="server"></asp:Label></td>
                <td width="20%" class="text-left" style="">物流状态：<asp:Label ID="StateLogistics" runat="server"></asp:Label></td>
            </tr>
        </tbody>
    </table>
    <table class="table table-striped table-bordered table-hover">
        <tbody id="Tbody3">
            <tr>
                <td width="50%" class="text-center">
                    <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td width="28%" align="right"">收货人姓名：</td>
                            <td width="72%" class="text-left"><asp:Label ID="Reusers" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">收货人地址：</td>
                            <td class="text-left"><asp:Label ID="Jiedao" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">收货人邮箱：</td>
                            <td class="text-left"><asp:Label ID="Email" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">付款方式：</td>
                            <td class="text-left"><asp:Label ID="Payment" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">发票信息：</td>
                            <td class="text-left"><asp:Label ID="Invoice" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">缺货处理：</td>
                            <td class="text-left"><asp:Label ID="Outstock" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">订单类型：</td>
                            <td class="text-left"><asp:Label ID="Ordertype" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">要求送货时间：</td>
                            <td class="text-left"><asp:Label ID="Deliverytime" runat="server" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
                <td width="50%" class="text-center">
                    <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td width="28%" align="right"">联系电话：</td>
                            <td width="72%" class="text-left"><asp:Label ID="Phone" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right">邮政编码：</td>
                            <td class="text-left"><asp:Label ID="ZipCode" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">收货人手机：</td>
                            <td class="text-left"><asp:Label ID="Mobile" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">送货方式：</td>
                            <td class="text-left"><asp:Label ID="Delivery" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">跟单员：</td>
                            <td class="text-left"><asp:Label ID="AddUser" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">内部记录：</td>
                            <td class="text-left"><asp:Label ID="Internalrecords" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">备注/留言：</td>
                            <td class="text-left"><asp:Label ID="Ordermessage" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td align="right"">订单状态：</td>
                            <td class="text-left"><asp:Label ID="OrderStatus" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </tbody>
    </table>
    <table class="table table-striped table-bordered table-hover">
        <tbody id="Tbody2">
            <tr>
                <td width="10%" class="text-center">图片</td>
                <td width="18%" class="text-center">商品名称</td>
                <td width="6%" class="text-center">单位</td>
                <td width="6%" class="text-center">数量</td>
                <td width="6%" class="text-center">市场价</td>
                <td width="6%" class="text-center">实价</td>
                <td width="6%" class="text-center">指定价</td>
                <td width="6%" class="text-center">金额</td>
                <td width="8%" class="text-center">服务期限</td>
                <td width="12%" class="text-center">备注</td>
            </tr>
            <asp:Repeater ID="procart" runat="server" OnItemDataBound="cartinfo_ItemDataBound">
                <ItemTemplate>
                    <tr>
                        <td class="text-center"><%#getproimg(DataBinder.Eval(Container, "DataItem.ProID", "{0}"))%></td>
                        <td class="text-center"><%#Getprotype(Eval("ProID","{0}"))%> <%#Eval("proname")%></td>
                        <td width="6%" class="text-center"><%#Eval("Danwei") %></td>
                        <td width="6%" class="text-center"><%#Eval("pronum") %></td>
                        <td width="6%" class="text-center"><%#getjiage("1", DataBinder.Eval(Container, "DataItem.ProID", "{0}"))%></td>
                        <td width="6%" class="text-center"><%#getshichangjiage(DataBinder.Eval(Container, "DataItem.ProID", "{0}"))%></td>
                        <td width="6%" class="text-center"><%#shijia(DataBinder.Eval(Container, "DataItem.Shijia", "{0}"))%></td>
                        <td width="6%" class="text-center"><%#getprojia(DataBinder.Eval(Container, "DataItem.ID", "{0}"))%></td>
                        <td width="8%" class="text-center"><%#qixian(DataBinder.Eval(Container, "DataItem.ProID", "{0}"))%></td>
                        <%--<td class="text-center"><%#getsend(DataBinder.Eval(Container, "DataItem.sended", "{0}"))%></td>--%>
                    </tr>
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td class="text-center"><%#getproimg(Eval("id","{0}"))%></td>
                                <td class="text-center"><%#Getprotype(DataBinder.Eval(Container, "DataItem.id", "{0}"))%><%#Eval("proname")%></td>
                                <td width="6%" class="text-center"><%#getProUnit(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
                                <td width="6%" class="text-center">1</td>
                                <td width="6%" class="text-center"><%#getjiage("1", DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
                                <td width="6%" class="text-center"><%#getjiage("2", DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
                                <td width="6%" class="text-center">-</td>
                                <td width="6%" class="text-center">-</td>
                                <td width="8%" class="text-center">-</td>
                                <td class="text-center">-</td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td></td>
                <td class="text-center">合计：</td>
                <td class="text-left"><asp:Label ID="Label2" runat="server" Text=""></asp:Label></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="10">运费：<asp:Label ID="Label32" runat="server" Text=""></asp:Label>元</td>
            </tr>
            <tr>
                <td colspan="10">
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td width="50%" class="text-left" style="height: 19px">
                                实际金额：
                                <asp:Label ID="Label29" runat="server" Text=""></asp:Label>
                                + 
                                <asp:Label ID="Label30" runat="server" Text=""></asp:Label>
                                ＝
                                <asp:Label ID="Label31" runat="server" Text=""></asp:Label>
                                元
                            </td>
                            <td width="50%" align="right" style="height: 19px">已付款：<asp:Label ID="Label28" runat="server" Text=""></asp:Label>元</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="10">
                    <font color="red">
                        <asp:Label ID="Label33" runat="server" Text=""></asp:Label>
                    </font>
                </td>
            </tr>
        </tbody>
    </table>
    <!--endprint-->
    <table width="100%" cellpadding="2" cellspacing="1" class="border" style="background-color: white;" id="TABLE1">
        <tbody id="Tbody4">
            <asp:Repeater ID="procart2" runat="server">
                <ItemTemplate></ItemTemplate>
            </asp:Repeater>
            <tr></tr>
            <tr></tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style>
        @media print {
            .Noprn {
                display: none;
            }
        }
    </style>

    <script> 
        function preview()
        {
            var ido=<%=Request.QueryString["id"]%>;
            window.open('Orderlistinfo.aspx?id='+ido+'&menu=print','打印预览','','');
        }
        function pageload()
        {
            <%
        if (Request.QueryString["menu"] == "print")
        {
	%>
            bdhtml=window.document.body.innerHTML;
            sprnstr="<!--startprint-->";
            eprnstr="<!--endprint-->";
            prnhtml=bdhtml.substr(bdhtml.indexOf(sprnstr)+17);
            prnhtml=prnhtml.substring(0,prnhtml.indexOf(eprnstr));
            window.document.body.innerHTML=prnhtml;
            window.print();
	<%
            } 
	%>
        }
    </script>

</asp:Content>
