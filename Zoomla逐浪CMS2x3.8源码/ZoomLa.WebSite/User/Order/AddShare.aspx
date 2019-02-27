<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddShare.aspx.cs" Inherits="User_Order_AddShare" MasterPageFile="~/User/Default.master"%>
<asp:Content ContentPlaceHolderID="head" Runat="Server"><title>晒单评价</title></asp:Content>
<asp:Content  ContentPlaceHolderID="Content" Runat="Server">
    <div id="pageflag" data-nav="shop" data-ban="shop"></div> 
    <div class="container shareorder">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Order/OrderList.aspx">我的订单</a></li>
        <li class="active"><a href="<%=Request.RawUrl %>">评价晒单</a></li>
    </ol>
        <table class="table table-border table-hover table-striped" id="EGV">
            <tr><th class="td_s"></th><th class="text-center">商品信息</th><th>购买数量</th><th>金额</th><th>购买时间</th></tr>
            <asp:Repeater runat="server" ID="RPT">
                <ItemTemplate>
                    <tr>
                        <td><label class="filltd"><input type="radio" value="<%#Eval("ID") %>" name="cart_rad" /></label></td>
                        <td>
                            <a href="<%#GetShopUrl() %>" target="_blank">
                                <img src="<%#GetImg() %>" class="img50" />
                                 <%#Eval("ProName") %></a></span>
                        </td>
                        <td class="td_m">x<%#Eval("ProNum","{0:f0}") %></td>
                        <td class="td_m"><%#Eval("AllMoney","{0:f2}") %></td>
                        <td class="td_m"><%#Eval("AddTime","{0:yyyy-MM-dd}") %></td></tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <div class="row">
            <div class="col-md-3 td_left text-right">标题：</div>
            <div class="col-md-9">
                <asp:TextBox ID="Title_T" placeholder="描述一下评价的主要内容" runat="server" CssClass="form-control text_300"></asp:TextBox>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 td_left text-right"><span class="color_red">*</span> 评分：</div>
            <div class="col-md-9" id="star_div">
                <i class="staricon fa fa-star-o" data-val="1"></i>
                <i class="staricon fa fa-star-o" data-val="2"></i>
                <i class="staricon fa fa-star-o" data-val="3"></i>
                <i class="staricon fa fa-star-o" data-val="4"></i>
                <i class="staricon fa fa-star-o" data-val="5"></i>
                <asp:HiddenField runat="server" id="star_hid" value="0"></asp:HiddenField>
            </div>
        </div>
     <%--   <div class="row">
            <div class="col-md-3 td_left text-right"><span class="color_red">*</span> 标签：</div>
            <div class="col-md-9"><div class="type_div"><span class="fa fa-pencil"></span> 自定义</div></div>
        </div>--%>
        <div class="row">
            <div class="col-md-3 td_left text-right"><span class="color_red">*</span> 心得：</div>
            <div class="col-md-9">
                <asp:TextBox ID="MsgContent_T" placeholder="商品是否给力?快分享你的购买心得吧" runat="server" CssClass="form-control text_300 msgcon" MaxLength="500" TextMode="MultiLine" Height="120" />
                <div class="text-right text_300 tips">10-500字</div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3 td_left text-right">晒单：</div>
            <div class="col-md-9">
                <input type="button" id="upfile_btn" class="btn btn-info" value="添加图片" />
                <div style="margin-top: 10px;" id="uploader" class="uploader">
                    <ul class="filelist"></ul>
                </div>
                <asp:HiddenField runat="server" id="Attach_Hid"></asp:HiddenField>
            </div>
        </div>
        <div class="row">
            <div class="col-md-3"></div>
            <div class="col-md-9">
                <asp:Button ID="Save_Btn" runat="server" CssClass="btn btn-primary" OnClick="Save_Btn_Click" OnClientClick="return SubCheck();" Text="评价并继续" /> 
                <asp:CheckBox ID="IsHideName" runat="server" Text="匿名评价" style="margin-left:30px;" /> 
            </div>
        </div>
    </div>
    <div style="height:20px;"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style>
        #EGV td {line-height:50px;}
        .required {color:#666;}
        .shareorder #star_div{line-height:30px;}
        .shareorder .staricon{font-size:20px; color:#ccc; cursor:pointer;}
        .staricon.fa-star {color:#FBA507;}
        /*其他*/
        .shareorder .type_div{padding:0 10px;cursor:pointer; margin:0 5px 5px 0;border:1px solid #ddd; float:left; line-height:30px; font-size:13px;}
        .shareorder .row{margin-top:10px;}
        .shareorder .td_left{line-height:30px;}
        .shareorder .addimg_a{display:inline-block; border:1px solid #ddd;text-align:center; padding:25px 10px;color:#999;}
        .shareorder .tips{font-size:12px;color:#999;}
    </style>
    <link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
    <script src="/JS/jquery.validate.min.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/Controls/ZL_Webup.js"></script>
    <script>
        $(function () {
            //jQuery.validator.addMethod("IDCards", function (value) {
            //    return ZL_Regex.isIDCard(value);
            //}, "请输入正确的证件号码！");
            $("form").validate({});
            jQuery.validator.addMethod("msgcon", function (value) {
                return (value.length > 9);
            }, "心得最少需要10个字符!");
            ZL_Webup.config.json.ashx = "";
            ZL_Webup.config.json.accept = "img";
            $("#upfile_btn").click(ZL_Webup.ShowFileUP);
            StarInit();
            //---
            $("input[name=cart_rad]")[0].checked = true;
        })
        function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }
        function reloadPage() { }
        //评星
        function StarInit() {
            $(".staricon").hover(function () {
                //fa-star-o空心,
                $(this).removeClass("fa-star-o").addClass("fa-star");
                $(this).prevAll(".staricon").removeClass("fa-star-o").addClass("fa-star");
            }, function () {
                StarByVal($("#star_hid").val());
            }).click(function () {
                $("#star_hid").val($(this).data("val"));
                StarByVal($(this).data("val"));
            });
            //移出div块,除非已click,否则清除值
            $("#star_div").mouseleave(function () {
                var val = $("#star_hid").val();
                StarByVal(val);
            });
            //根据val点亮或熄灭评星
            function StarByVal(val) {
                if (val == "" || val == 0 || val == "0") { $(".staricon").removeClass("fa-star").addClass("fa-star-o"); }
                else
                {
                    var ref = $(".staricon[data-val=" + val + "]"); ref.removeClass("fa-star-o").addClass("fa-star");
                    ref.prevAll().removeClass("fa-star-o").addClass("fa-star");
                    ref.nextAll().removeClass("fa-star").addClass("fa-star-o");
                }
            }
        }
        //-----
        function SubCheck()
        {
            var score = parseInt($("#star_hid").val());
            if (score < 1 || score > 5) { alert("请选定评分"); return false; }
            return true;
        }
    </script>
</asp:Content>
