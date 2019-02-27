<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReqRepair.aspx.cs" MasterPageFile="~/User/Default.master" Inherits="User_Order_ReqRepair" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>申请返修</title>
<style>
body{ font-family:"Microsoft Yahei", SimSun, Tahoma, Helvetica, Arial, sans-serif; height:100%;}
.reqrepair .line30{line-height:30px;}
.typesel_div .active_div{border:2px solid red!important;}
.reqrepair .type_div{float:left;margin-right:10px;border:1px solid #ddd; padding:0 10px; cursor:pointer;}
.reqrepair .row{margin-top:10px;}
.reqrepair .tip_div{color:#999;}
.reqrepair .protable td{text-align:center;}
.protable .title_td{text-align:left;}
.reqrepair .proinfo_td div{padding:5px;}
</style>
<script src="/JS/ICMS/area.js"></script>
<script src="/JS/Controls/ZL_PCC.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="shop" data-ban="shop"></div> 
    <div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">申请返修</li>
    </ol>
    </div>
    <div class="container reqrepair">
        <table class="table table-striped protable">
            <tr style="background-color:#f5f5f5;">
                <td>商品名称</td><td>购买数量</td><td>商品单价</td><td>购买时间</td>
            </tr>
            <tr>
                <td class="proinfo_td">
                    <div class="col-md-2"><img src="" onerror="shownopic(this);" runat="server" id="ProImgSrc" style="width:100%;height:80px;" /></div>
                    <div class="col-md-10 title_td">
                        <asp:Label ID="ProName_L" runat="server"></asp:Label>
                    </div>
                </td>
                <td><asp:Literal ID="ProNum_Li" runat="server"></asp:Literal></td>
                <td>￥<asp:Literal ID="ProPrice_L" runat="server"></asp:Literal></td>
                <td><asp:Literal ID="ProDate" runat="server"></asp:Literal></td>
            </tr>
        </table>
        <div class="panel panel-default">
          <div class="panel-heading">
            <h3 class="panel-title">申请返修</h3>
          </div>
          <div class="panel-body">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-2 line30 text-right"><span class="color_red">*</span>服务类型：</div>
                    <div class="col-md-10 line30 typesel_div">
                        <asp:Literal ID="ServiceType_Li" runat="server"></asp:Literal>
                        <asp:HiddenField ID="ServicesType_Hid" Value="1" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 line30 text-right"><span class="color_red">*</span>提交数量：</div>
                    <div class="col-md-10 line30"><asp:TextBox ID="Num_T" runat="server" Text="1" CssClass="form-control text_xs text-center notnull num"></asp:TextBox></div>
                </div>
                <div class="row">
                    <div class="col-md-2 line30 text-right"><span class="color_red">*</span>问题描述：</div>
                    <div class="col-md-10 line30">
                        <div><asp:TextBox MaxLength="500" ID="Deatil_T" runat="server" TextMode="MultiLine" CssClass="form-control notnull" Width="500px" Height="120"></asp:TextBox></div>
                        <div class="text-right tip_div" style="width:500px;">10-500字</div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 line30 text-right">图片信息：</div>
                    <div class="col-md-10 line30">
                        <div>
                            <input type="button" id="upfile_btn" class="btn btn-info" value="添加图片" />
                            <div style="margin-top: 10px;" id="uploader" class="uploader">
                                <ul class="filelist"></ul>
                            </div>
                            <asp:HiddenField runat="server" id="Attach_Hid"></asp:HiddenField>
                        </div>
                        <div>为了帮助我们更好的解决问题，请您上传图片</div>
                        <div class="tip_div">最多可上传5张图片，每张图片大小不超过5M，支持bmp,gif,jpg,png,jpeg格式文件</div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 line30 text-right">申请凭据：</div>
                    <div class="col-md-10 line30">
                        <label><input type="checkbox" value="1" checked="checked" name="cret" /> 有发票</label>
                        <label><input type="checkbox" value="2" name="cret" /> 有检测报告</label>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 line30 text-right"><span class="color_red">*</span>返回方式：</div>
                    <div class="col-md-10 line30 typesel_div">
                        <div data-value="1" class="type_div active_div">上门取件</div><div data-value="2" class="type_div">送货到自提点</div>
                        <asp:HiddenField ID="ReProType_Hid" Value="1" runat="server" />
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 line30 text-right"><span class="color_red">*</span>取货地点：</div>
                    <div class="col-md-10 line30">
                        <div>
                            <select id="province_dp" name="province_dp" class="form-control td_m"></select>
                            <select id="city_dp" name="city_dp" class="form-control td_m"></select>
                            <select id="county_dp" name="county_dp" class="form-control" style="width:92px;"></select>
                            <asp:HiddenField ID="take_hid" runat="server" />
                        </div>
                        <div class="margin_t5"><asp:TextBox ID="TakeAddress_T" placeholder="详细地址" runat="server" CssClass="form-control text_600"></asp:TextBox></div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 line30 text-right"><span class="color_red">*</span>取件时间：</div>
                    <div class="col-md-10 line30"><asp:TextBox ID="TakeDate_T" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' })" runat="server" CssClass="form-control text_300 notnull"></asp:TextBox></div>
                </div>
                <div class="row">
                    <div class="col-md-2 line30 text-right"><span class="color_red">*</span>收货地点：</div>
                    <div class="col-md-10 line30">
                        <div>
                            <select id="reprovince_dp" name="reprovince_dp" class="form-control td_m"></select>
                            <select id="recity_dp" name="recity_dp" class="form-control td_m"></select>
                            <select id="recounty_dp" name="recounty_dp" class="form-control" style="width:92px;"></select>
                            <asp:HiddenField ID="reurn_hid" runat="server" />
                        </div>
                        <div><asp:TextBox ID="ReqAddRess_T" placeholder="详细地址" runat="server" CssClass="form-control text_600 margin_t5"></asp:TextBox></div>
                        <div><input type="checkbox" id="setTakeDate" /> 与取货地址相同</div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 line30 text-right"><span class="color_red">*</span>客户姓名：</div>
                    <div class="col-md-10 line30"><asp:TextBox ID="UserName_T" runat="server" CssClass="form-control text_300 notnull"></asp:TextBox></div>
                </div>
                <div class="row">
                    <div class="col-md-2 line30 text-right"><span class="color_red">*</span>手机号码：</div>
                    <div class="col-md-10 line30"><asp:TextBox ID="Phone_T" runat="server" CssClass="form-control text_300 phone"></asp:TextBox> <label><input type="checkbox" id="setOrderPhone" /> 与订单中手机号相同</label></div>
                </div>
                
                <div class="row">
                    <div class="col-md-2 line30"></div>
                    <div class="col-md-10 line30"><asp:Button ID="Save_Btn" runat="server" CssClass="btn btn-primary" OnClientClick="return CheckFormData();" OnClick="Save_Btn_Click" Text="提交" /></div>
                </div>
            </div>
          </div>
        </div>
    </div>
    <asp:HiddenField ID="IsReadOnly_Hid" runat="server" />
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Controls/ZL_Webup.js"></script>
    <script src="/JS/ZL_Regex.js"></script>
    <script>
        $().ready(function () {
            //省市县
            var pcc = new ZL_PCC("province_dp", "city_dp", "county_dp");
            if ($("#take_hid").val() == "") {
                pcc.ProvinceInit();
            }
            else {
                var attr = $("#take_hid").val().split(' ');
                pcc.SetDef(attr[0], attr[1], attr[2]);
                pcc.ProvinceInit();
            }
            var pcc = new ZL_PCC("reprovince_dp", "recity_dp", "recounty_dp");
            if ($("#reurn_hid").val() == "") {
                pcc.ProvinceInit();
            }
            else {
                var attr = $("#reurn_hid").val().split(' ');
                pcc.SetDef(attr[0], attr[1], attr[2]);
                pcc.ProvinceInit();
            }
            //初始化返回方式
            var $types = $("#ReProType_Hid").parent();
            $types.find('.type_div').removeClass('active_div');
            $types.find('[data-value="' + $("#ReProType_Hid").val() + '"]').addClass('active_div');
            //初始化服务类型
            var $services = $("#ServicesType_Hid").parent();
            $services.find('.type_div').removeClass('active_div');
            $services.find('[data-value="' + $("#ServicesType_Hid").val() + '"]').addClass('active_div');
            //初始化图片
            var imgs = $("#Attach_Hid").val();
            if (imgs != "") { ZL_Webup.AddReadOnlyLi(imgs); }
            if ($("#IsReadOnly_Hid").val() == "1") {
                $(".reqrepair *").attr('disabled', 'disabled');
                return;
            }
            //上传
            ZL_Webup.config.json.ashx = "action=OAattach";
            $("#upfile_btn").click(ZL_Webup.ShowFileUP);

            //返回方式选择
            var $types = $("#ReProType_Hid").parent();
            $types.find('.type_div').click(function () {
                $types.find('.type_div').removeClass('active_div');
                $(this).addClass('active_div');
                $("#ReProType_Hid").val($(this).data('value'));
            });
            //服务类型选择
            var $services = $("#ServicesType_Hid").parent();
            $services.find('.type_div').click(function () {
                $services.find('.type_div').removeClass('active_div');
                $(this).addClass('active_div');
                $("#ServicesType_Hid").val($(this).data('value'));
            });
        });
        function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }

        function CheckFormData() {
            var notnulls = $('.notnull');
            var nums = $(".num");
            var phones = $(".phone");
            return CheckData(notnulls, "notnull") && CheckData(nums, "num") && CheckData(phones, "phone");
        }
        function CheckData(array,type) {
            for (var i = 0; i < array.length; i++) {
                var data=$(array[i]);
                var text = data.closest('.row').find('.col-md-2').text().replace('：', "");
                if (type == "notnull" && data.val().trim() == "") {
                    alert(text + "不能为空!");
                    return false;
                }
                if (type == "num" && !ZL_Regex.isNum(data.val().trim())) {
                    alert(text + "只能为数字!");
                    return false;
                }
                if (type == "phone" && !ZL_Regex.isMobilePhone(data.val().trim())) {
                    alert(text + "格式不正确!")
                    return false;
                }
            }
            return true;
        }
    </script>
</asp:Content>