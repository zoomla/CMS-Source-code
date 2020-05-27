<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BKEditor.aspx.cs" Inherits="ZoomLaCMS.Guest.Baike.BKEditor"  MasterPageFile="~/Common/Master/Empty.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>百科编辑器</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="content_wrapper">
<div id="header" class="header">
    <div class="pull-right" style="margin-top:20px;margin-right:20px;">
        <asp:Button runat="server" ID="Save_Btn" CssClass="btn btn-lg btn-primary" OnClick="Save_Btn_Click" Text="保存" OnClientClick="return save();" style="margin-right:15px;" />
        <a href="/Baike" onclick="return confirm('确定要退出吗?');" class="btn btn-lg btn-default">返回</a>
    </div>
</div>
<div style="height:70px;"></div>
    <div class="bkdir">
        <div class="bkdir_head">
            <i class="fa fa-list"></i><strong>词条目录</strong>
            <input type="button" value="生成目录" class="btn btn-xs btn-info" style="font-size:12px;" onclick="dirs.list();" />
        </div>
        <div class="bkdir_content">
            <ul id="baike_nav" class="dirul"></ul>
        </div>
    </div>
    <div class="btype_div">
        <div class="bkdir_head">
            <i class="fa fa-flag"></i><strong>词条标签(以,号隔开)</strong>
        </div>
        <asp:TextBox runat="server" ID="BType_T" TextMode="MultiLine" placeholder="请输入词条标签" MaxLength="255" CssClass="form-control btype_txt"></asp:TextBox>
    </div>
    <div class="bke_body">
        <div class="bke_head">
            <h1 id="bke_title" style="margin-bottom:0px;" runat="server"></h1><span style="font-size:20px;color:#666;margin-left:10px;" id="class_sp" runat="server"></span>
            <input type="button" value="修改分类" onclick="showSelClass();" class="btn btn-info"  />
            <div id="card" class="card">
                <div class="card_header">
                    <h2>概述</h2>
                    <input type="file" id="pic_up" accept="image/*" style="display: none;" onchange="pic.upload();" />
                </div>
                <div class="card_content">
                    <div id="card_pic" style="display:none;" class="card_pic" onclick="pic.sel();" title="点击上传图片">
                        <img runat="server" style="max-width:200px;" id="pic_img" />
                    </div>
                    <div id="card_nopic" style="display:none;" class="card_pic card_nopic" onclick="pic.sel();" title="点击上传图片">
                        <div><i class="fa fa-image" style="font-size: 5em;"></i></div>
                        <div>请上传概述图</div>
                    </div>
                    <div class="card_txt">
                        <asp:TextBox runat="server" id="Brief_T" TextMode="MultiLine" style="height:200px;"></asp:TextBox>
                    </div>
                    <div class="clearfix"></div>
                </div>
                <div class="card_header"><h2>基本信息栏 <span style="color:#f31a1a;font-size:12px;">(名称或值为空,将会自动忽略)</span></h2></div>
                <div class="info_body">
                    <table class="table table-bordered table-striped" id="info_tb"></table>
                    <a href="javascript:;" class="btn btn-info" onclick="info.addRow();"><i class="fa fa-plus"></i> 添加自定义项</a>
                </div>
            </div>
        </div>
    </div>
    <div class="bke_body">
        <div class="bke_head" style="border-bottom:2px solid #ccc;padding-bottom:5px;"><strong class="font20">正文</strong></div>
        <asp:TextBox runat="server" ID="Contents_T" style="height: 600px;margin-top:10px;" TextMode="MultiLine"></asp:TextBox>
        <div style="display:none;" id="code"></div>
      <%--  <%=ZoomLa.Safe.SafeC.ReadFileStr("/test/content.txt") %>--%>
    </div>
    <div class="bke_body">
        <div class="card_header">
            <strong class="font20">参考资料：</strong>
            <a id="ref_add_btn" class="opbtn" onclick="refence.showAdd();"><i class="fa fa-link"></i> 添加新参考资料</a><!--弹窗显示-->
        </div>
        <div id="ref_body" class="ref_body"></div>
    </div>
</div>
<div class="hidden">
    <asp:HiddenField runat="server" ID="info_hid" />
    <asp:HiddenField runat="server" ID="refence_hid" />
    <asp:HiddenField runat="server" ID="pic_hid" />
    <asp:HiddenField runat="server" ID="class_hid" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
body {background-color:#E8E9EB;}
a {cursor:pointer;}
.font20 {font-size:20px;}
.td_l {width:120px;text-align:center;}
.td_s {width:50px;}
.modal-content {margin-top:130px;}
.wid1100 {width:800px;}
/*-------------------------------------------*/
#content_wrapper {}
.header {position:fixed;height:70px;background-color:#F5F6F8;border-bottom:1px solid #cfd0d1; text-shadow:none;width:100%;top:0px;}
.bke_body {background-color:#fff;margin-bottom:7px;position:relative;text-align:left;padding:15px;border:1px solid #ccc;width:760px; margin:0 auto; margin-top:10px;}
.bke_head #bke_title {font-size:30px;position:relative;color:rgb(178, 178, 178);display:inline-block;}
.card {margin-top:10px;overflow:hidden;padding-bottom:15px; }
.card_header {padding-left:12px;margin-top:5px;border-bottom:2px solid #ccc;padding:6px 0;overflow:hidden;}
.card_content {margin-top:10px;border-bottom:1px dashed #ddd;padding-bottom:15px;}
.card_header h2 {font-size:20px;color:#333;float:left;}
.card .card_pic {width:200px;float:left;padding:5px;position:relative;cursor:pointer;}
.card .card_nopic {text-align:center;color:#ddd;padding-top:75px;}
.card .card_txt {float:left;line-height:22px;text-align:left;font-size:14px;width:500px; margin-left:20px;}
.card .info_body {margin-top:10px;}
.bke_body .opbtn {color:#36c;float:right;padding-left:20px;height:15px;padding-top:8px;display:inline-block;}
.ref_body {background-color:#FAFAFA;margin-top:10px;padding:15px;}
.ref_item {margin-bottom:15px;}
.ref_item .item_url_div {width:500px;overflow:hidden;white-space:nowrap;text-wrap:none; text-overflow:ellipsis;}
.ref_item .item_url {text-decoration:underline;}
/*------------------------*/
.bkdir {display:block;position:fixed;left:0;width:210px;background-color:#fff;}
.bkdir_head {border-bottom:1px solid #e3e3e6;overflow:hidden;line-height:38px;height:38px;padding-left:13px;}
.bkdir_content {display:block;height:370px;overflow-y:auto;padding:9px 0 0 10px;color:#666;}
.dirul {padding-left:10px;list-style-type:none;list-style:none;}
.dirul>li{padding-bottom:3px;list-style-type:none;list-style:none;}
.drul li ul {padding:0px;margin:0px;list-style-type:none;}
.dirul .level1 {color:#136ec2;font-size:16px;font-weight:500;text-decoration:none;}
.dirul .level2 {color:#333;line-height:16px;font-size:12px;text-decoration:none;color:#136ec2;padding-left:12px;}
.dirul .level3 {color:#333;line-height:16px;font-size:12px;text-decoration:none;padding-left:24px;}
.btype_div {display:block;position:fixed;right:0;width:210px;background-color:#fff;}
.btype_div .btype_txt {border:none;resize:none;height:300px !important;}
</style>
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script src="/JS/Plugs/Baike.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script>
    var card_ue, main_ue;
    $(function () {
        card_ue = UE.getEditor("Brief_T", {<%=ZoomLa.BLL.BLLCommon.ueditorMin%> });
        main_ue = UE.getEditor("Contents_T", {<%=ZoomLa.BLL.BLLCommon.ueditorMid%> });
        BaiKe.config.id = "code";
        //BaiKe.config.dirid = "baike_list";
        BaiKe.config.navid = "baike_nav";
        //main.addListener("afterPaste contentChange", function () {

        //});
        {
            var val = $("#info_hid").val();
            if (!ZL_Regex.isEmpty(val)) { info.data = JSON.parse(val); info.dataToEdit(); }
            else { info.addRow(); info.addRow(); }//默认给两行选填
        }
        //---------------------------------------
        var $pic = $("#pic_img");
        if (ZL_Regex.isEmpty($pic.attr("src"))) { $("#card_nopic").show(); }
        else { $("#card_pic").show(); }
        //---------------------------------------
        {
            var val = $("#refence_hid").val();
            if (!ZL_Regex.isEmpty(val)) { refence.data = JSON.parse(val);  refence.dataToEdit(); }
        }
        setTimeout(function () { dirs.init(); }, 1000);
    })
    /*-----------------------------------*/
    var dirs = {};
    dirs.list = function () {
        //在左边栏列出
        $("#code").html(main_ue.getContent());
        var $first=$("#code").children().first();
        if ($first.length > 0) { $first.before("<h2>概述</h2><h2>基本信息栏</h2>"); }
        else { $("#code").append("<h2>概述</h2><h2>基本信息栏</h2>"); }
        $("#code").append("<h2>参考资料</h2>");
        BaiKe.CreateNavUI(BaiKe.GetList());
    }
    dirs.init = function () { dirs.list();  }
    /*-----------------------------------*/
    var pic = { id: "#pic_up" };
    pic.sel = function () {
        $(pic.id).val("");
        $(pic.id).click();
    }
    pic.upload = function () {
        var fname = $(pic.id).val();
        if (!SFileUP.isWebImg(fname)) { alert("请选择图片文件"); return false; }
        SFileUP.AjaxUpFile("pic_up", function (data) {
            $("#pic_img").attr("src", data);
            $("#card_pic").show();
            $("#card_nopic").hide();
        });
    }
    pic.preSave = function () {
        var src = $("#pic_img").attr("src");
        $("#pic_hid").val(src);
    }
    //------------------------------
    function showSelClass() {
        comdiag.width = "wid1100";
        ShowComDiag("Diag/SelClass.aspx", "选择分类 <input type='button' value='确定' class='btn btn-info' onclick='setClass();'>");
    }
    function setClass() {
        var $item = $(".modal_ifr:first").contents().find(".list-group-item.active");
        if ($item.length < 1) { alert("请先选择分类"); return false; }
        $("#class_hid").val($item.text());
        $("#class_sp").text("(" + $item.text() + ")");
        CloseComDiag();
    }
    //------------------------------
    function save() {
        info.preSave();
        refence.preSave();
        pic.preSave();
        return true;
    }
</script>
</asp:Content>