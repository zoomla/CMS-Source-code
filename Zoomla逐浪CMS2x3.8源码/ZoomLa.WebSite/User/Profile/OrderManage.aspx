<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="OrderManage.aspx.cs" Inherits="User_Profile_OrderManage" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的订单</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/UserShop/Default.aspx">商城管理</a></li>
        <li class="active">我的订单</li>
    </ol>
    <div runat="server" id="Login" visible="false">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" class="text-center"><font color="red">本页需支付密码才能登录请输入支付密码</font></td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox runat="server" ID="Second" CssClass="form-control pull-right" TextMode="Password"></asp:TextBox>
                </td>
                <td style="width: 50%;">
                    <asp:Button runat="server" ID="sure" CssClass="btn btn-primary" Text="确定" OnClick="sure_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div runat="server" id="DV_show">
        <div class="btn-group" style="margin-bottom:10px;">
            <a href="OrderManage.aspx?state=0" class="btn btn-primary">待返订单</a><a href="OrderManage.aspx?state=1" class="btn btn-primary">已返订单</a><a href="OrderManage.aspx?state=2" class="btn btn-primary">失效订单</a>
        </div>
        <table class="table table-striped table-bordered">
            <tr>
                <td class="text-center">订单详情
                </td>
            </tr>
            <tr>
                <td>
                    <div id="orderlist" runat="server" style="float: left; margin-top: 5px"></div>
                    <div id="order" runat="server" style="margin-left: 15px; margin-top: 5px; float: right">如果您刚下的单不在列表中，<a id="look" href="javascript:void(0)" onclick="Show()">点此查看↓</a> </div>
                </td>
            </tr>
            <tr id="hedui" style="display: none;">
                <td>
                    <ul class="item_list">
                        <li><strong>请核对您的下单时间：</strong></li>
                        <li style="color: Gray">如果不到30分钟，请耐心等待，30分钟后<a href="?state=0">点此刷新</a></li>
                        <li style="color: Gray">如果超过30分钟，建议您去商家页面取消订单后重新下单；或者将您的订单编号、下单时间提交给在线客服，我们会在24小时内帮您核对。 <a href="###">提交给在线客服&gt;&gt;</a></li>
                    </ul>
                </td>
            </tr>
        </table>
        <div>
            <asp:HiddenField ID="count" runat="server" />

            <div align="center">
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td colspan="8" class="text-center">
                            <div id="tips" runat="server"></div>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="14%">订单状态</td>
                        <td align="center" width="12%">订单反馈日期</td>
                        <td align="center" width="12%">商家</td>
                        <td align="center" width="12%">订单号</td>
                        <td align="center" width="12%">订单金额</td>
                        <td align="center" width="12%">预计可返金额</td>
                        <td align="center" width="14%">预计返利时间</td>
                        <td align="center" width="12%">实返金额</td>
                    </tr>
                    <tr>
                        <td colspan="8" class="text-center">
                            <div id="lblOrderTip" runat="server" style="color: Red"></div>
                        </td>
                    </tr>
                    <ZL:ExRepeater ID="repf" runat="server" PagePre="<tr><td colspan='8' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>" OnItemDataBound="repf_ItemDataBound">
                        <ItemTemplate>
                            <tr>
                                <td align="center" width="14%">
                                    <asp:Label ID="lblState" runat="server"></asp:Label></td>
                                <td align="center" width="12%">
                                    <asp:Label ID="lblProData" runat="server"></asp:Label></td>
                                <td align="center" width="12%">
                                    <asp:Label ID="lblShop" runat="server"></asp:Label></td>
                                <td align="center" width="12%">
                                    <asp:HiddenField ID="hfId" runat="server" Value='<%#Eval("id") %>' />
                                    <asp:Label ID="lblOrderNo" runat="server" Text='<%#Eval("OrderNo") %>'></asp:Label></td>
                                <td align="center" width="12%">
                                    <asp:Label ID="lblOrderMoney" runat="server"
                                        Text='<%#DataBinder.Eval(Container, "DataItem.OrderMoney", "{0:N2}")%>'></asp:Label></td>
                                <td align="center" width="12%">
                                    <asp:Label ID="ProfileMoney" runat="server"
                                        Text='<%#DataBinder.Eval(Container, "DataItem.profileMoney", "{0:N2}")%>'> </asp:Label></td>
                                <td align="center" width="14%">
                                    <asp:Label ID="lblTime" runat="server"> </asp:Label></td>
                                <td align="center" width="12%">
                                    <asp:Label ID="lblFM" runat="server"></asp:Label></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate></FooterTemplate>
                    </ZL:ExRepeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" language="javascript">
        function Show() {
            if (document.getElementById("hedui").style.display == "none") {
                document.getElementById("look").innerText = "点此查看↑";
                document.getElementById("hedui").style.display = "";
            } else {
                document.getElementById("hedui").style.display = "none";
                document.getElementById("look").innerText = "点此查看↓";
            }
        }

        function change() {

        }
    </script>
</asp:Content>
