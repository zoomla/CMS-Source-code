<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SetOrderStatus.aspx.cs" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" Inherits="Manage_Shop_SetOrderStatus" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>自定义订单状态</title>
    <link href="/Plugins/ColorPicker/css/colorpicker.css" rel="stylesheet" />
    <script src="/Plugins/ColorPicker/colorpicker.js"></script>
    <style>
        .selcolor{font-size:16px;}
        .setorderstatus .td_l{width:450px;}
        .setorderstatus .text_md{width:400px;max-width:400px;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs" role="tablist">
    <li role="presentation" class="active"><a href="#orderstatus" aria-controls="orderstatus" role="tab" data-toggle="tab">订单状态</a></li>
    <li role="presentation"><a href="#ExpStatus" aria-controls="ExpStatus" role="tab" data-toggle="tab">物流状态</a></li>
    <li role="presentation"><a href="#PayStatus" aria-controls="PayStatus" role="tab" data-toggle="tab">支付状态</a></li>
    <li role="presentation"><a href="#PayType" aria-controls="PayType" role="tab" data-toggle="tab">支付方式</a></li>

    </ul>
    <div class="tab-content setorderstatus">
    <div role="tabpanel" class="tab-pane active" id="orderstatus">
        <table class="table table-bordered table-hover margin_t5">
            <thead>
                <tr><th class="td_m">订单状态</th><th class="td_s">状态码</th><th class="td_l">编辑状态</th><th>预览效果</th></tr>
            </thead>
            <tbody>
                <tr>
                    <td class="text-center"><span class="gray_9">正常订单</span></td>
                    <td>0</td>
                    <td>
                        <asp:TextBox ID="OrderNormal_T" runat="server" data-text="正常订单" CssClass="form-control text_md colortext"></asp:TextBox> 
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="text-center"><span class="gray_9">已确认</span></td>
                    <td>1</td>
                    <td><asp:TextBox ID="OrderSured_T" runat="server" data-text="已确认" CssClass="form-control text_md colortext"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="text-center"><span class="gray_9">订单完结</span></td>
                    <td>99</td>
                    <td><asp:TextBox ID="OrderFinish_T" runat="server" data-text="订单完结" CssClass="form-control text_md colortext"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="text-center"><span class="gray_9">已分成</span></td>
                    <td>100</td>
                    <td><asp:TextBox ID="UnitFinish_T" runat="server" data-text="已分成" CssClass="form-control text_md colortext"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="text-center"><span class="gray_9">申请退款</span></td>
                    <td>-1</td>
                    <td><asp:TextBox ID="DrawBack_T" runat="server" data-text="申请退款" CssClass="form-control text_md colortext"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="text-center"><span class="gray_9">拒绝退款</span></td>
                    <td>-2</td>
                    <td><asp:TextBox ID="UnDrawBack_T" runat="server" data-text="拒绝退款" CssClass="form-control text_md colortext"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="text-center"><span class="gray_9">确认退款</span></td>
                    <td>-3</td>
                    <td><asp:TextBox ID="CheckDrawBack_T" runat="server" data-text="确认退款" CssClass="form-control text_md colortext"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="text-center"><span class="gray_9">取消订单</span></td>
                    <td>-5</td>
                    <td><asp:TextBox ID="CancelOrder_T" runat="server" data-text="取消订单" CssClass="form-control text_md colortext"></asp:TextBox></td>
                    <td></td>
                </tr>
                <tr>
                    <td class="text-center"><span class="gray_9">已删除</span></td>
                    <td>-99</td>
                    <td><asp:TextBox ID="Recycle_T" runat="server" data-text="已删除" CssClass="form-control text_md colortext"></asp:TextBox></td>
                    <td></td>
                </tr>
            </tbody>
        </table>
    </div>
    <div role="tabpanel" class="tab-pane" id="ExpStatus">
        <table class="table table-bordered table-hover margin_t5">
            <thead>
                <tr><th class="td_m">物流状态</th><th class="td_s">状态码</th><th class="td_l">编辑状态</th><th>预览效果</th></tr>
            </thead>
            <tbody>
             <tr>
                <td class="text-center"><span class="gray_9">等待配送</span></td>
                <td>0</td>
                <td><asp:TextBox ID="UnDelivery_T" runat="server" data-text="等待配送" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td class="text-center"><span class="gray_9">已配送</span></td>
                <td>1</td>
                <td><asp:TextBox ID="Delivery_T" runat="server" data-text="已配送" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td class="text-center"><span class="gray_9">已签收</span></td>
                <td>2</td>
                <td><asp:TextBox ID="Signed_T" runat="server" data-text="已签收" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td class="text-center"><span class="gray_9">拒签收</span></td>
                <td>-1</td>
                <td><asp:TextBox ID="UnSiged_T" runat="server" data-text="拒签收" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
            </tr>
            </tbody>
        </table>
    </div>
    <div class="tab-pane" id="PayStatus">
        <table class="table table-bordered table-hover margin_t5">
            <thead>
                <tr><th class="td_m">支付状态</th><th class="td_s">状态码</th><th class="td_l">编辑状态</th><th>预览效果</th></tr>
            </thead>
            <tbody>
                <tr>
                <td class="text-center"><span class="gray_9">未付款</span></td>
                <td>0</td>
                <td><asp:TextBox ID="NoPay_T" runat="server" data-text="未付款" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
                </tr>
                <tr>
                <td class="text-center"><span class="gray_9">已付款</span></td>
                <td>1</td>
                <td><asp:TextBox ID="HasPayed_T" runat="server" data-text="已付款" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
                </tr>
                <tr>
                <td class="text-center"><span class="gray_9">申请退款</span></td>
                <td>-1</td>
                <td><asp:TextBox ID="RequestRefund_T" runat="server" data-text="申请退款" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
                </tr>
                <tr>
                <td class="text-center"><span class="gray_9">已退款</span></td>
                <td>-2</td>
                <td><asp:TextBox ID="Refunded_T" runat="server" data-text="已退款" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
                </tr>
                <tr>
                <td class="text-center"><span class="gray_9">确认收款</span></td>
                <td>10</td>
                <td><asp:TextBox ID="SurePayed_T" runat="server" data-text="确认收款" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
                </tr>
            </tbody>
        </table>
    </div>
        <div class="tab-pane" id="PayType">
        <table class="table table-bordered table-hover margin_t5">
            <thead>
                <tr><th class="td_m">支付状态</th><th class="td_s">状态码</th><th class="td_l">编辑状态</th><th>预览效果</th></tr>
            </thead>
            <tbody>
                <tr>
                <td class="text-center"><span class="gray_9">普通</span></td>
                <td>0</td>
                <td><asp:TextBox ID="PayNormal_T" runat="server" data-text="普通" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
                </tr>
                <tr>
                <td class="text-center"><span class="gray_9">预付款</span></td>
                <td>1</td>
                <td><asp:TextBox ID="PrePay_T" runat="server" data-text="预付款" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
                </tr>
                <tr>
                <td class="text-center"><span class="gray_9">代收款</span></td>
                <td>2</td>
                <td><asp:TextBox ID="HelpReceive_T" runat="server" data-text="代收款" CssClass="form-control text_md colortext"></asp:TextBox></td>
                <td></td>
                </tr>
            </tbody>
        </table>
            <div class="alert alert-info" role="alert">
                <span class="fa fa-info-circle"></span> "预付款"编辑状态可使用@money表示预付款的值!
            </div>
    </div>
    <div class="text-center">
        <asp:Button ID="SaveConfig_Btn" CssClass="btn btn-primary" OnClick="SaveConfig_Btn_Click" runat="server" Text="保存配置" />
        <asp:Button ID="ReSet_Btn" CssClass="btn btn-primary" OnClick="ReSet_Btn_Click" Text="刷新配置" runat="server" />
    </div>
  </div>
    <script>
        $(function () {
            InitSelColor();
            InitPreText();
        });
        var CurColor;//当前颜色选择器
        //初始化颜色选择器
        function InitSelColor() {
            $('.colortext').each(function () {
                $(this).parent().append(' <a href="javascript:;" class="selcolor" title="颜色选择"><span class="fa fa-font"></span></a>');
            });
            $('.selcolor').ColorPicker({
                color: '#0000ff',
                onShow: function (colpkr) {
                    CurColor = this;
                    $(colpkr).fadeIn(500);
                    return false;
                },
                onHide: function (colpkr) {
                    $(colpkr).fadeOut(500);
                    return false;
                },
                onChange: function (hsb, hex, rgb) {
                    $(CurColor).css('color', '#' + hex);
                },
                onSubmit: function (hsb, hex, rgb) {
                    var $textobj = $(CurColor).prev();
                    var value = '<span style="color:#' + hex + '">' + $textobj.data("text") + '</span>';
                    $textobj.val(value);
                    PreTextColor($textobj[0]);
                }
            });
        }
        //初始化预览效果
        function InitPreText() {
            $('.colortext').each(function () {
                PreTextColor(this);
            });
            $(".colortext").blur(function () {
                PreTextColor(this);
            });
        }
        //预览
        function PreTextColor(textobj) {
            $(textobj).parent().next().html($(textobj).val());
        }
    </script>
</asp:Content>

