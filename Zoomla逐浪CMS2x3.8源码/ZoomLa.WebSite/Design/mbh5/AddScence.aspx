<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddScence.aspx.cs" Inherits="Design_mbh5_AddScence" MasterPageFile="~/Common/Master/Empty.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>场景管理</title>
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0;" name="viewport" />
<link type="text/css" rel="stylesheet" href="/dist/css/weui.min.css" />  
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="mbh5_create">
<div class="mbh5_create_t">
<h1>场景管理</h1>
</div>
<div class="mbh5_create_c">
<div class="weui_cells weui_cells_form">
<div class="weui_cell">
<div class="weui_cell_hd"><label class="weui_label">名称</label></div>
<div class="weui_cell_bd weui_cell_primary">
<ZL:TextBox runat="server" ID="Title_T" CssClass="form-control input-sm" AllowEmpty="false" ValidType="String" Text="我的场景" placeholder="场景名称" />
</div>
</div>
<div class="weui_cell bgcolor_div">
<div class="weui_cell_hd"><label class="weui_label">背景</label></div>
<div class="weui_cell_bd weui_cell_primary">
<div class="con">
<ul class="bgcolor list-unstyled">
<li class="active" style="background-color:#fff;" data-bgcolor="#fff"></li>
<li style="background-color:#00e0ff;" data-bgcolor="#00e0ff"></li>
<li style="background-color:#f00;" data-bgcolor="#f00"></li>
<li style="background-color:#fff200;" data-bgcolor="#fff200"></li>
<li style="background-color:#e500ff;" data-bgcolor="#e500ff"></li>
<li style="background-color:#00ff0d;" data-bgcolor="#00ff0d"></li>
<li style="background-color:#00f;" data-bgcolor="#00f"></li>
<li style="background-color:#000;" data-bgcolor="#000"></li>
<div class="clearfix"></div>
</ul>
</div>
<asp:HiddenField runat="server" ID="BgColor_Hid" Value="#fff" />
</div>
</div>
<div class="weui_cell">
<div class="weui_cell_hd"><label class="weui_label">图标</label></div>
<div class="weui_cell_bd weui_cell_primary">
<div class="con img_con">
<span class="pull-right">
<button type="button" id="selimg_btn" class="btn btn-info">选择图片</button>
<button type="button" id="clear_btn" class="btn btn-info" style="display:none;">清除</button>
</span>
<ZL:SFileUp ID="SFile_Up" runat="server" FType="Img" MaxWidth="600" MaxHeight="600" />
</div>
</div>
</div>
</div>
</div>

<div class="mbh5_create_b">
<asp:Button runat="server" ID="Add_B" Text="创建新场景" OnClick="Add_B_Click" CssClass="btn btn-danger btn-block btn-lg" />
<a href="/Class_371/Default.aspx" class="btn btn-info btn-block mb_btn">【从已建立的场景中选择一个美化设计】</a>
<a href="/Class_371/NodePage.aspx?type=20" class="btn btn-info btn-block mb_btn">【免费模板库一键生成】</a>
<a href="/Class_371/NodePage.aspx?type=19" class="btn btn-info btn-block mb_btn">【企业超值套餐】</a>
<a href="FastCreate.aspx" class="btn btn-info btn-block mb_btn">【模拟动画】</a>
<a href="javascript:;" class="btn btn-info btn-block mb_btn">【高级DIY】</a>
</div>
</div>
<div class="fill_div"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>
body { background:#fbf9fe;}
.mbh5_create { font-family:"STHeiti","Microsoft YaHei","黑体","arial";}
.mbh5_create_t { }
.mbh5_create_t h1 { font-size:1.5em; text-align:center;}
.weui_cells { font-size:1em;}
.weui_cells .weui_label { margin-bottom:0; font-weight:normal;}
.mbh5_create_b { padding:10px 15px;}
.mbh5_create_c .con ul { margin-bottom:0;}
.mbh5_create_c .con .bgcolor li { display:inline-block; margin-right:13px; padding:1px; width:40px;height:40px; background:#000; border-radius:100%; border:solid 3px #ccc;}
.mbh5_create_c .con .bgcolor li:hover{border:solid 3px #28c3fc;}
.mbh5_create_c .con .bgcolor li.active{border:solid 3px #28c3fc;}
.mbh5_create #Add_B{ font-size:1.2em;}
.sfile_body { float:left;}
.sfile_body .sfile_updiv {display:none;}
.weui_cells .weui_cell_primary p { margin-bottom:0;}
.fill_div{height:60px;display:none; }
</style>
<script>
$(function () {
    $(".sfile_img").css({ "width": 60, "height": 60 });
    window.innerHeight = 1000;
})
$(".bgcolor li").click(function () {
    $(".bgcolor li").removeClass('active');
    $("#BgColor_Hid").val($(this).data('bgcolor'));
    $(this).addClass('active');
});
//----图片上传
$("#selimg_btn").click(function () { $(".sfile_selbtn").click(); });
$("#clear_btn").click(function () { $(".sfile_clsbtn").click(); $(this).hide(); });
$("#FileUp_File").change(function () {
    var val = $(this).val();
    if (val && val != "" &&'<%=Request.QueryString["device"] %>'!='pc') { $("#clear_btn").show(); }
    else { $("#clear_btn").hide(); }
});
//-PC下隐藏按钮
function showinpc() {
    $(".mb_btn").hide();
    $("#Title_T").removeClass("input-sm").css("width",373);
    $(".sfile_body,.sfile_updiv").show();
    $("#selimg_btn,#clear_btn").hide();
    $("#Add_B").removeClass("btn-block").removeClass("btn-lg").css("margin-left", 105)
               .removeClass("btn-danger").addClass("btn-info");
    $(".fill_div").show();
}
//--修改场景
function editscence() {
    $(".bgcolor_div").hide();
    $(".mb_btn").hide();
    if ($("#FVPath_T").val() != '') {
        $("#clear_btn").show();
    }
}
</script>
</asp:Content>
