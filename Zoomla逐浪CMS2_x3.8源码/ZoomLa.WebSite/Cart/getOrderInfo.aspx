<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getOrderInfo.aspx.cs" Inherits="Cart_getOrderInfo" MasterPageFile="~/Cart/order.master" EnableViewStateMac="true"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/ICMS/ZL_Common.js"></script>
    <title>订单结算</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="head_div hidden-xs">
        <a href="/"><img src="<%=Call.LogoUrl %>" /></a>
    </div>
    <div class="row gray_border">
        <div class="bordered address_div"><span class="address_head">填写并核对订单信息</span><img class="pull-right" src="/App_Themes/User/step2.png" /></div>
        <div class="bordered" runat="server" id="Address_Div">
            <p><i class="fa fa-pencil-square-o strong"> 收货人信息</i></p>
            <ul class="addresssul indent">
                <asp:Repeater runat="server" ID="AddressRPT">
                    <ItemTemplate>
                        <li id="addli_<%#Eval("ID") %>">
                            <label class="normalw"><input type="radio" name="address_rad" value="<%#Eval("ID") %>" /><%#GetAddress() %></label>
                            <span>
                                <%#GetIsDef() %>
                                <a href="javascript:;" onclick="EditAddress(<%#Eval("ID") %>);">修改</a>
                                <a href="javascript:;" onclick="DelAddress(<%#Eval("ID") %>);">删除</a>
                            </span>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div runat="server" id="EmptyDiv" class="r_red" visible="false">你没有收货地址,请先填写收货地址</div>
            <input type="button" value="添加新地址" class="btn btn-primary btn-xs margin_l20" onclick="AddAddress();"/>
        </div>
        <div class="bordered">
            <p><i class="fa fa-th strong"> 发票信息</i></p>
            <div class="indent">
                <label>
                    <input type="radio" name="invoice_rad" value="0" onclick="$('#invoice_div').hide();" checked="checked"/>不开发票</label>
                    <label><input type="radio" name="invoice_rad" value="1" onclick="$('#invoice_div').show();" />需要发票</label><br />
                <div id="invoice_div">
                    <ul class="addresssul indent padding0">
                        <asp:Repeater ID="Invoice_RPT" runat="server">
                            <ItemTemplate>
                                <li>
                                    <label class="normalw"><input class="invoice_item_rad" name="invoice_item_rad" type="radio" value='<%#Eval("Detail") %>' data-head="<%#Eval("Head") %>" /><%#Eval("Head") %></label>
                                </li>
                            </ItemTemplate>
                            <FooterTemplate> 
                                 <li><label class="normalw"><input class="invoice_item_rad" name="invoice_item_rad" type="radio" value='' data-head=""/>使用新发票</label></li>
                            </FooterTemplate>
                        </asp:Repeater>
                    </ul>
                    <div>
                        <asp:TextBox runat="server" ID="InvoTitle_T" CssClass="form-control text_400 margin_t5" MaxLength="50" placeholder="发票抬头（个人或公司名称）"/> 
                        <asp:TextBox runat="server" ID="Invoice_T" TextMode="MultiLine" class="form-control invoice_t" MaxLength="180" placeholder="在此输入发票开具科目明细
" />
                    </div>
                </div>
            </div>
        </div>
        <div class="bordered">
            <p><i class="fa fa-cubes strong">商品清单</i>
                <a runat="server" id="ReUrl_A1" visible="false" title="返回购物车" class="pull-right padding_r10">返回修改购物车</a>
                <a runat="server" id="ReUrl_A2" visible="false" class="pull-right padding_r10">返回修改信息</a>
            </p>
            <div class="indent">
                <table class="table text-center">
                <tr style="background:#eeeeee;"><td>商品</td><td>金额</td><td>优惠</td><td>数量</td><td>状态</td></tr>
                    <asp:Repeater runat="server" ID="Store_RPT" OnItemDataBound="Store_RPT_ItemDataBound" EnableViewState="false">
                        <ItemTemplate>
                            <tbody style="border: none;">
                                <tr>
                                    <td colspan="6" class="storename text-left"><label><%#Eval("StoreName") %></label></td></tr>
                                     <asp:Repeater runat="server" ID="ProRPT" EnableViewState="false">
                                                <ItemTemplate>
                                                    <tr>
                                                        <td class="text-left">
                                                            <div class="pull-left"><a href="<%#GetShopUrl() %>" target="_blank" title="浏览商品">
                                                                <img src="<%#GetImgUrl(Eval("Thumbnails"))%>" onerror="shownopic(this);" class="preimg_m" /></a>
                                                            </div>
                                                            <div class="padding_l55"> <a href="<%#GetShopUrl() %>" target="_blank" title="浏览商品"><span><%#Eval("ProName") %></span></a>
                                                            <p class="grayremind"><%#GetAddition() %></p></div>
                                                        </td>
                                                        <td class="tdtext">
                                                            <i id="payPriceId" class="fa fa-rmb"><%#Eval("AllMoney","{0:F2}") %></i>
                                                           <%#GetAllMoney_Json() %>
                                                        </td>
                                                        <td class="r_red"><%#GetDisCount()%></td>
                                                        <td>x <%#Eval("Pronum") %></td>
                                                        <td><%#GetStockStatus() %></td>
                                                    </tr>
                                                </ItemTemplate>
                                        </asp:Repeater>
                                   <tr><td colspan="6" class="text-right">
                                           <span>配送方式:</span>
                                       <asp:Literal runat="server" ID="FareType_L" EnableViewState="false"></asp:Literal>
                                       </td></tr>
                                 </tbody>
                        </ItemTemplate>
                    </asp:Repeater>
                <tr>
                    <td colspan="6">
                        <div class="text-right total_count_div">
                            <div><span><span runat="server" id="itemnum_span" class="r_red_x">1</span>件商品,总商品金额：</span><i class="fa fa-rmb" runat="server" id="totalmoney_span1">0.00</i>
                                <span>余额:<span id="totalPurse_sp"></span></span>
                                <span>银币:<span id="totalSicon_sp"></span></span>
                                <span>积分:<span id="totalPoint_sp"></span></span>
                            </div>
                            <div><span>优惠卷：</span><i class="fa fa-rmb" id="arrive_money_sp">0.00</i></div>
                            <div><span>积分折扣：</span><i class="fa fa-rmb" id="point_money_sp">0.00</i></div>
                            <div><span>运费：</span><i class="fa fa-rmb" id="fare_span">0.00</i></div>
                            <div class="pay_moneyAll"><span>应付总额：</span><i class="fa fa-rmb" runat="server" id="totalmoney_span2">0.00</i></div>
                        </div>
                    </td>
                </tr>
            </table>
                <ul class="extend_ul">
                    <li runat="server" visible="false" id="userli">
                         <p><a href="javascript:;" onclick="$('#userlist_div').toggle();" title="显示顾客详情"><i class="fa fa-users"> 顾客与联系人列表</i></a></p>
                        <div id="userlist_div" class="extenddiv" style="display:block;">
                            <table class="table table-bordered">
                                <tr>
                                    <td class="td_m">姓名</td><td>证件号</td><td>手机</td>
                                </tr>
                                <asp:Repeater runat="server" ID="UserRPT" EnableViewState="false">
                                    <ItemTemplate><tr><td><%#Eval("Name") %></td><td><%#Eval("CertCode") %></td><td><%#Eval("Mobile") %></td></tr></ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                    </li>
                    <li>
                        <div><a href="javascript:;" onclick="$('#arrive_div').toggle();"><i class="fa fa-plus-circle"> 优惠券</i></a></div>
                        <div runat="server" id="arrive_div" class="extenddiv">
                            <asp:HiddenField runat="server" ID="Arrive_Hid" />
                            <div runat="server" visible="false" id="arrive_empty_div">
                                <div class="alert alert-info">没有可用的优惠券</div>
                            </div>
                            <div runat="server" visible="false" id="arrive_data_div">
                                                            <ul id="arrive_active_ul" class="list-unstyled arrive_o">
                                <asp:Repeater runat="server" ID="Arrive_Active_RPT" EnableViewState="false">
                                   <ItemTemplate>
                                       <li class="item" data-flow="<%#Eval("flow") %>">
                                           <div class="c_top"></div>
                                           <div class="detail">
                                               <div class="c_msg">
                                                   <div class="c_price">
                                                       <i class="fa fa-rmb"></i> <%#Eval("AMount","{0:f0}") %>
                                                       <span class="c_limit"><%#Arrive_GetMoneyRegion() %></span>
                                                   </div>
                                                   <div class="c_time"><%#"有效期至"+Eval("EndTime","{0:yyyy-MM-dd}") %></div>
                                               </div>
                                               <div class="c_type">
                                                   <span>[<%#Arrive_GetTypeStr() %>]</span>
                                               </div>
                                           </div>
                                           <div class="info"></div>
                                           <span class="c_choose"></span>
                                       </li>
                                   </ItemTemplate>
                                </asp:Repeater>
                                <li class="clearfix"></li>
                            </ul>
                            <hr/>
                            <ul class="list-unstyled arrive_o">
                            <asp:Repeater runat="server" ID="Arrive_Disable_RPT" EnableViewState="false">
                                   <ItemTemplate>
                                       <li class="item disable">
                                           <div class="c_top"></div>
                                           <div class="detail">
                                               <div class="c_msg">
                                                   <div class="c_price">
                                                       <i class="fa fa-rmb"></i> <%#Eval("AMount","{0:f0}") %>
                                                       <span class="c_limit"><%#Arrive_GetMoneyRegion() %></span>
                                                   </div>
                                                   <div class="c_time"><%#"有效期至"+Eval("EndTime","{0:yyyy-MM-dd}") %></div>
                                               </div>
                                               <div class="c_type"><span>[<%#Arrive_GetTypeStr() %>]</span></div>
                                               <div class="r_gray margin_t5"><i class="fa fa-exclamation-circle"></i> <%#Eval("err") %></div>
                                           </div>
                                           <div class="info"></div>
                                           <span class="c_choose"></span>
                                       </li>
                                   </ItemTemplate>
                                </asp:Repeater>
                                <li class="clearfix"></li>
                            </ul>
                            </div>
                        </div>
                    </li>
                    <li id="point_li">
                        <div><a href="javascript:;" onclick="$('.point_div').toggle();"><i class="fa fa-plus-circle"> 积分抵扣</i></a></div>
                        <div id="point_body" runat="server" visible="false" class="extenddiv point_div">
                            共<asp:Label ID="Point_L" runat="server"></asp:Label>个积分,本次可用<span id="usepoint_span" class="r_red"></span>个积分,<asp:TextBox runat="server" ID="Point_T" Text="0" onkeyup="checkMyPoint(this);" CssClass="form-control text_150 num"/>
                        </div>
                        <div id="point_tips" runat="server" visible="false" class="alert alert-warning point_div extenddiv" role="alert">
                            <i class="fa fa-exclamation-circle"></i> 积分抵扣已关闭!
                        </div>
                    </li>
                    <li>
                       <div><a href="javascript:;" onclick="$('#oremind_div').toggle();"><i class="fa fa-plus-circle"> 订单备注</i></a></div>
                        <div id="oremind_div" class="extenddiv">
                            <p>提示：请勿填写有关支付、收货、发票方面的信息</p>
                            <asp:TextBox runat="server" ID="ORemind_T" CssClass="form-control max" MaxLength="100" placeholder="限100个字" />
                        </div>
                   </li>
                </ul>
            </div>
        </div>
        <div class="total_div">
            <span class="total">应付总额：<i runat="server" id="totalmoney_i" class="fa fa-rmb">0.00</i></span><asp:Button runat="server" CssClass="btn btn-danger" ID="AddOrder_Btn" Text="提交订单" OnClientClick="disBtn(this,5000);" OnClick="AddOrder_Btn_Click" />
        </div>
    </div>
    <asp:HiddenField ID="PointRate_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/Modal/APIResult.js"></script>
<script>
    $(function () {
        //地址
        if ($(".addresssul li").length > 0) {
            $(".addresssul li").click(function () {
                $(this).siblings().removeClass("active");
                $(this).addClass("active");
            });
            $(".addresssul li:first").click(); $(":radio[name=address_rad]")[0].checked = true;
        }
        //付款方式
        $(".methodul li").click(function () {
            $(this).siblings().removeClass("active");
            $(this).addClass("active");
            $(this).find(":radio")[0].checked = true;
        })
        $(".methodul li:first").click();
        //发票
        $(".invoice_item_rad").click(function () {
            $("#InvoTitle_T").val($(this).data("head"));
            $("#Invoice_T").val($(this).val());
        });
        $(".invoice_item_rad:first").click();
        //运费
        $(".fare_select").change(function () {
            UpdateTotalPrice();
        });
        //优惠券
        arrive.init();
        UpdateTotalPrice();
        IsDisBtn();
        ZL_Regex.B_Num(".num")
    })
    var diag = new ZL_Dialog();
    diag.width = "minwidth";
    diag.maxbtn = false;
    function AddAddress() {
        diag.title = "添加新地址";
        diag.url = "address.aspx";
        diag.ShowModal();
    }
    function EditAddress(id) {
        diag.title = "修改地址";
        diag.url = "address.aspx?id=" + id;
        diag.ShowModal();
    }
    function DelAddress(myid) {
        if (confirm("确定要删除吗")) {
            $("#addli_" + myid).remove();
            $.post("ordercom.ashx", { action: "deladdress", id: myid }, function () {
            });
        }
    }
    function SelInvo(dp) {
        if ($(dp).val() != "") {
            $("#InvoTitle_T").val($(dp).find(":selected").text());
            $("#Invoice_T").val($(dp).val());
        }
    }
    //价格统计
    function UpdateTotalPrice() {
        var total = parseFloat($("#totalmoney_span1").text());
        var arrive = parseFloat($("#arrive_money_sp").text());
        var point = parseFloat($("#point_money_sp").text());
        var fare = 0;
        //根据所选,计算运费
        $(".fare_select").each(function () {
            fare += parseFloat($(this).find("option:selected").data("price"));
        });
        total = (total + arrive + fare - point);
        total = total > 0 ? total : 0;
        $("#fare_span").text(fare.toFixed(2));
        $("#totalmoney_span2").text(total.toFixed(2));
        $("#totalmoney_i").text(total.toFixed(2));
        $("#totalPurse_sp").text(GetSumByFilter(".purse_sp"));
        $("#totalSicon_sp").text(GetSumByFilter(".sicon_sp"));
        $("#totalPoint_sp").text(GetSumByFilter(".point_sp"));
    }
    //计算可用积分抵扣
    function SumByPoint(usepoint) {
        var point = parseFloat($("#Point_L").text());
        if (usepoint > point) { usepoint = point; };
        $("#usepoint_span").text(usepoint);
        ZL_Regex.B_Value("#Point_T", {
            min: 0, max: usepoint, overmin: null, overmax: null
        });
    }
    function GetSumByFilter(filter) {
        var total = 0.00;
        $(filter).each(function () {
            var price = parseFloat($(this).text());
            if (price) total += price;
        });
        return total.toFixed(2);
    }
    //是否禁用按钮
    function IsDisBtn() {
        var flag = false;
        if ($("#Address_Div").length > 0 && $(".addresssul li").length < 1) { flag = true; }
        if ($(".stockout").length > 0) { flag = true; }
        if (flag)
        { disBtn(document.getElementById("AddOrder_Btn")); }
    }
    function Refresh() { diag.CloseModal(); location = location; }
    function checkMyPoint(obj) {
        if (isNaN(parseFloat($(obj).val()))) { $(obj).val("0"); }
        var val = parseFloat($(obj).val());
        var usepoint = parseFloat($("#usepoint_span").text());//可用积分
        if (usepoint <= val) { val = usepoint; $(obj).val(usepoint); };
        var pointrate = parseFloat($("#PointRate_Hid").val());
        $("#point_money_sp").text((val * pointrate).toFixed(2));
        UpdateTotalPrice();
    }
    //--------------
    var arrive = {};
    arrive.init = function () {
        $("#arrive_active_ul .item").click(function () {
            $(".arrive_o .item").removeClass("choose");
            $(this).addClass("choose");
            arrive.use($(this).data("flow"));
        });
    }
    arrive.use = function (flow) {
        var model = { action: "arrive", "flow": flow, money: $("#totalmoney_span1").text() };
        $.post("ordercom.ashx", model, function (data) {
            var model = APIResult.getModel(data);
            if (APIResult.isok(model)) {
                $("#arrive_money_sp").text("-" + parseFloat(model.result.amount).toFixed(2));
            }
            else { $("#arrive_money_sp").text("-0.00"); alert(model.retmsg); }
            $("#Arrive_Hid").val(flow);
            UpdateTotalPrice();
        });
    }
</script>
</asp:Content>