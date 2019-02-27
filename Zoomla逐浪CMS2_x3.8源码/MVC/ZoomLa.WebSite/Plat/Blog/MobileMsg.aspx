<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MobileMsg.aspx.cs" Inherits="ZoomLaCMS.Plat.Blog.MobileMsg" MasterPageFile="~/Plat/Empty.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<title>简洁分享</title>
<link type="text/css" rel="stylesheet" href="/App_Themes/Guest.css" /> 
<script src="/JS/Mobile/ResizeImg/mobileFix.mini.js"></script>
<script src="/JS/Mobile/ResizeImg/exif.js"></script>
<script src="/JS/Mobile/ResizeImg/lrz.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container-fluid">
<div class="row">
  <div>
    <asp:TextBox TextMode="MultiLine" placeholder="今天说点什么?" Width="100%" Height="150" Style="border-radius: 0px; max-width: 100%;" Rows="5" ID="Content_T" runat="server"></asp:TextBox>
    <div class="msg_opdiv" style="top: -5px; border-right: 1px solid #dadada;"></div>
    <div id="img_div" style="text-align:center;max-width:960px;margin:auto;"> 
        <img id="img1" src="/Images/userface/noface.png" data-id="1" class="img_pre col-lg-4 col-md-4 col-sm-4 col-xs-12" onclick="ShowFile(1)" /> 
        <img src="/Images/userface/noface.png" data-id="2" class="img_pre col-lg-4 col-md-4 col-sm-4 col-xs-12" onclick="ShowFile(2)" />
        <img id="img3" src="/Images/userface/noface.png" data-id="3" class="img_pre col-lg-4 col-md-4 col-sm-4 col-xs-12" onclick="ShowFile(3)" /> </div>
    <asp:LinkButton runat="server" CssClass="btn btn-primary one" Style="width: 100%; padding-top: 15px; padding-bottom: 15px; margin-top: 10px;" OnClientClick="return CheckEmpy()" OnClick="SaveContent_Click"> <span class="fa fa-globe" style="font-size:1.2em;margin-right:5px;"></span>分享</asp:LinkButton>
  </div>
</div>
<div id="upfile_div" style="display: none;">
  <input data-id="1" type="file" accept="image/*" class="upfile" />
  <input data-id="2" type="file" accept="image/*" class="upfile" />
  <input data-id="3" type="file" accept="image/*" class="upfile" />
  <asp:HiddenField runat="server" ID="imgurl_hid" />
</div>
</div>
<script>
$(function () {
    $("#top_nav_ul li[title='主页']").addClass("active");
    $(".upfile").change(function () {
        diag.ShowModal();
        lrz(this.files[0], { width: 400 }, function (results) {
            $("#img_div").find("img[data-id=" + curid + "]").attr("src", results.blob);
            //上传base64
            $.post("MobileMsg.aspx", { base64: results.base64 }, function (result) {
                var mod = JSON.parse(result);
                var v = $('#imgurl_hid').val();
                $('#imgurl_hid').val(v + "|" + mod.imgurl);
                diag.CloseModal();
            });
        });
    });
})//ready end;
var curid = 1;
function ShowFile(id) {
    $("#upfile_div").find("input[data-id=" + id + "]").click();
    curid = id;
}
function CheckEmpy() {
    if ($("#file").val()==""&&($("#Content_T").val()=="")) {
        alert("发送图片或内容不能为空！");
        return false;
    }
    return true;
}
var diag = new ZL_Dialog();
diag.title = "图片上传";
diag.body = "上传中,请稍等片刻";
</script> 
</asp:Content>