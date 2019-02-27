<%@ Page Language="C#" AutoEventWireup="true" CodeFile="se_bk.aspx.cs" Inherits="Plugins_cropper_se_bk" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>背景裁剪</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<img id="image" src="<%=HttpUtility.UrlDecode(Request["Url"])%>" alt="Picture">
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<link href="cropper.css" rel="stylesheet" />
<script src="cropper.js"></script>
<div style="position:fixed;top:10px;left:10px;">
    <asp:LinkButton runat="server" ID="Cut_Btn" CssClass="btn btn-info" OnClick="Cut_Btn_Click" OnClientClick="return setcut();"><i class="fa fa-cut"></i> 裁剪</asp:LinkButton>
    <a href="javascript:;" class="btn btn-info" onclick="top.CloseDiag();"><i class="fa fa-close"></i> 关闭</a>
    <asp:HiddenField runat="server" ID="Cut_Hid" />
</div>
<script>
var $image = $('#image');
$(function () {
    //限定最小高度,可限定最小,不可调整,缩放为裁剪框比率0.01
        $image.cropper({
            autoCropArea: 0.1,
            cropBoxResizable: false,
            zoomOnWheel: false,
            minCropBoxWidth: 320,
            minCropBoxHeight:570,
            built: function () { $image.cropper("zoom", -0.5); }//自动缩小0.5
    });
});
function setcut() {
    var data = $image.cropper("getData");
    $("#Cut_Hid").val(JSON.stringify(data));
    return true;
}
</script>
</asp:Content>