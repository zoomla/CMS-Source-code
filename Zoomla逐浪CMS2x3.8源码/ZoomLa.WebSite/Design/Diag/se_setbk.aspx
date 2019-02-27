<%@ Page Language="C#" AutoEventWireup="true" CodeFile="se_setbk.aspx.cs" Inherits="Design_Diag_SelRes" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link href="/Design/res/css/se_design.css" rel="stylesheet" />
    <title>资源选择</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="respane">
    <div class="lpane">
        <ul class="list-unstyled" id="op_ul">
            <li class="type_sysimg" onclick="location.href='se_setbk.aspx?type=sysimg&pageid=<%:Request["pageid"]%>';">图片库</li>
            <li class="type_myimg" onclick="location.href='se_setbk.aspx?type=myimg&pageid=<%:Request["pageid"]%>';">我的图片</li>
        </ul>
        <div class="color_div" title="选择背景色<a href='javascript:;' class='btn btn-info btn-xs pull-right color_btn' onclick='setbkcolor();'>确定</a>" ondblclick="setbkcolor();">
            <span class="color_btn"><i class="fa fa-paint-brush" style="font-size:20px;"></i> 纯色背景</span>
        </div>
        <div class="up_div" onclick="pic.sel();">
            <div class="wait"><i class="fa fa-cloud-upload" style="font-size:25px;"></i> 上传</div>
            <div class="work"><i class="fa fa-spinner fa-spin" style="font-size:25px;"></i> 正在上传</div>
        </div>
       <input type="file" style="display:none;" id="pic_up" />
    </div>
    <div id="sysimg_div" runat="server" class="mpane">
        <div class="header">
            <ul runat="server" class="list-unstyled" id="style_ul"></ul>
        </div>
        <div class="main">
            <div runat="server" id="empty_div" class="alert alert-info margin_t10" visible="false">没有匹配的资源数据</div>
            <ul class="list-unstyled img_ul">
                <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
                    <ItemTemplate>
                        <li class="img_li" title="<%#Eval("name") %>" onclick="setbk('<%#Eval("VPath") %>');">
                            <img src="<%#Eval("PreViewImg") %>" />
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="clearfix"></div>
            <div>
                <asp:Literal runat="server" ID="Page_Lit" EnableViewState="false"></asp:Literal>
            </div>
        </div>
    </div>
    <div id="myimg_div" runat="server" visible="false" class="mpane">
        <div class="main">
            <div runat="server" id="emptyimg" class="alert alert-info margin_t10" visible="false">您还没有上传过图片</div>
            <ul class="list-unstyled img_ul">
                <asp:Repeater runat="server" ID="Myimg_RPT">
                    <ItemTemplate>
                        <li class="img_li" title="<%#Eval("name") %>" onclick="setbk('<%#Eval("Path") %>')">
                            <img src="<%#Eval("Path") %>" />
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
            <div class="clearfix"></div>
            <div>
                <asp:Literal runat="server" ID="ImgPage_Lit" EnableViewState="false"></asp:Literal>
            </div>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link rel="stylesheet" type="text/css" href="/Design/JS/Plugs/color/bootstrap.colorpickersliders.min.css"  />
<script src="/Design/JS/Plugs/color/tinycolor-min.js"></script>
<script src="/Design/JS/Plugs/color/bootstrap.colorpickersliders.min.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<style>
body{background-color:#fff;}
#op_ul li{cursor:pointer;}
</style>
<script>
var data = { type: "", url: "",color:"", post: "", pageid: "<%:Request["pageid"]%>" };
function getto(style) { location.href = "se_setbk.aspx?style=" + style + "&pageid=" + data.pageid; }
function setbk(url) {
    data.type = "image";
    data.url = url;
    top.page.bk.set(data);
    top.CloseDiag();
}
function setbkcolor() {
    data.type = "color";
    top.page.bk.set(data);
    top.CloseDiag();
}
var pic = { id: "pic_up", txtid: null };
pic.sel = function (id) { $("#" + pic.id).val(""); $("#" + pic.id).click(); }
pic.upload = function () {
    var fname = $("#" + pic.id).val();
    if (!SFileUP.isWebImg(fname)) { alert("请选择图片文件"); return false; }
    $(".up_div").addClass("working");
    SFileUP.AjaxUpFile(pic.id, function (url) { setbk(url); $(".up_div").removeClass(".working"); });
}
$("#" + pic.id).change(function (e) {
    if (e.target.files.length < 1) { return; }
    pic.upload();
});
$(".color_div").ColorPickerSliders({
    size: 'sm', placement: 'top', swatches: false, sliders: false, hsvpanel: true, previewformat: "hex",
    onchange: function (container, color) {
        var color = color.tiny.toHexString()
        $(".color_div").css("background-color", color);
        data.color = color;
    }
});

$(function () {
    var type = '<%:Request.QueryString["type"] %>';
    if(type==""){type="sysimg";}
    $(".type_"+type).addClass("active");
})
</script>
</asp:Content>