<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditScence.aspx.cs" Inherits="Design_mbh5_EditScence" MasterPageFile="~/Common/Master/Empty.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title></title>
<meta content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0;" name="viewport" />
<link type="text/css" rel="stylesheet" href="/dist/css/weui.min.css" /> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="mbh5_create">
<div class="mbh5_create_t">
<h1><span>修改场景参数</span></h1>
</div>
<div class="mbh5_create_c">
<div class="weui_cells weui_cells_form">
<div class="weui_cell">
<div class="weui_cell_hd"><label class="weui_label">名称</label></div>
<div class="weui_cell_bd weui_cell_primary">
<ZL:TextBox runat="server" ID="Title_T" CssClass="form-control" AllowEmpty="false" ValidType="String" Text="我的场景" placeholder="场景名称" />
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
<asp:Button runat="server" ID="Edit_B" Text="保存修改" OnClick="Edit_B_Click" CssClass="btn btn-danger btn-block btn-lg" />
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>
body { background:#fbf9fe;}
.mbh5_create { font-family:"STHeiti","Microsoft YaHei","黑体","arial";font-size:30px;}
.mbh5_create .btn {height:60px;font-size:30px;} 
.mbh5_create_t { }
.mbh5_create_t h1 { text-align:center;margin:30px 0;}
.weui_cells { font-size:1em;}
.weui_cells .weui_cell{padding:20px 0 20px 20px;}
.weui_cells .weui_label { margin-bottom:0; font-weight:normal;}
.mbh5_create_b { padding:10px 15px;}
.mbh5_create_c .con ul { margin-bottom:0;}
.mbh5_create_c .con span{margin:30px 15px 0 0;}
.mbh5_create_c #Title_T{height:60px;font-size:30px;}
.mbh5_create #Add_B{ font-size:1.2em;}
.sfile_body { float:left;}
.sfile_body .sfile_updiv {display:none;}
.weui_cells .weui_cell_primary p { margin-bottom:0;}
</style>
<script>
$(function () {
    $(".sfile_img").css({ "width": 120, "height": 120 });
    if ($("#FVPath_T").val() != '') {
        $("#clear_btn").show();
    }
})
//----图片上传
$("#selimg_btn").click(function () { $(".sfile_selbtn").click(); });
$("#clear_btn").click(function () { $(".sfile_clsbtn").click(); $(this).hide(); });
$("#FileUp_File").change(function () {
    var val = $(this).val();
    if (val && val != "" &&'<%=Request.QueryString["device"] %>'!='pc') { $("#clear_btn").show(); }
    else { $("#clear_btn").hide(); }
});
</script>
</asp:Content>
