<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Design_mbh5_album_Default" MasterPageFile="~/Common/Master/Empty.master" %>
<%@ Import Namespace="ZoomLa.BLL.Design" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<style type="text/css">
.photo_add {position:relative;border:1px dashed #ddd;display:inline-block;overflow:hidden;width:150px;height:150px;text-align:center;line-height:150px;}
.photo_item {position:relative;display:inline-block;list-style:none;text-align:center;margin-right:10px; width:150px;height:150px;overflow:hidden;}
.photo_item img{max-width:100%;max-height:100%;}
.photo_item .opspan {display:block;position:absolute;bottom:0px;width:100%;padding-left:15px;padding-right:15px;}
.photo_item .opspan button {width:100%;border-radius:0px;}
.tlp_item {position:relative;display:inline-block;list-style:none;margin-right:10px; width:140px; height:176px;
           border-left:1px solid #ddd;border-top:1px solid #ddd;box-shadow:2px 2px 3px #ddd;padding:5px;}
.tlp_item img {width:100%;}
.tlp_item span {width:100%;line-height:50px;text-align:center;font-size:15pt;display:block;height:50px;}
.tlp_item.active {border-left:1px solid #0094ff;border-top:1px solid #0094ff;box-shadow:2px 2px 3px #0094ff;}
#music_ul {max-height:200px;overflow-y:auto;}
.music_item {border-bottom: 1px solid #ddd;}
.music_item .wrap {width: 100%;line-height: 30px;font-size:16px; font-weight: normal;}
.music_item .name {float: left; margin-left: 20px; }
.music_item .ischoose {float: right; margin-right: 40px;display:none;font-size:26px;}
.music_item.active .ischoose {display:block;}
.footbtn {position:fixed;bottom:0px;width:100%;border-radius:0px;}
</style>
<title>相册管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table class="table table-bordered table-striped" style="border-bottom:none;">
    <tr><td colspan="2" class="text-center">相册信息</td></tr>
    <tr><td colspan="2"><asp:TextBox runat="server" ID="AlbumName_T" class="form-control" placeholder="请输入相册名称"/></td></tr>
<%--    <tr><td class="td_m">相册描述</td><td><asp:TextBox runat="server" ID="AlbumDesc_T" class="form-control" /></td></tr>--%>
    <tr><td colspan="2" class="text-center">图片管理</td></tr>
    <tr><td colspan="2">
        <div style="overflow-x:auto;height:155px;" class="auto_width">
            <ul class="list-unstyled" id="photo_ul" style="white-space:nowrap;">
                <li class="photo_add" id="upfile_btn">
                    <i class="fa fa-image" style="font-size:60px;color:#ddd;"></i>
                </li>
            </ul>
        </div>
        </td></tr>
    <tr><td colspan="2" class="text-center">模板选择</td></tr>
    <tr><td colspan="2">
        <div style="overflow-x:auto;" class="auto_width">
            <ul class="list-unstyled" id="tlp_ul" style="white-space:nowrap;display:none;">
                <li class="tlp_item" data-id="1">
                    <img src="<%=B_Design_Album.TlpDir+"1/thumb.jpg" %>" />
                    <span>心心相印</span>
                </li>
                <li class="tlp_item" data-id="2">
                    <img src="<%=B_Design_Album.TlpDir+"2/thumb.jpg" %>" />
                    <span>校园记忆</span>
                </li>
                <li class="tlp_item" data-id="3">
                    <img src="<%=B_Design_Album.TlpDir+"3/thumb.jpg" %>" />
                    <span>气球旅行</span>
                </li>
                <li class="tlp_item" data-id="4">
                    <img src="<%=B_Design_Album.TlpDir+"4/thumb.jpg" %>" />
                    <span>个人旅程</span>
                </li>
            </ul>
        </div>
        </td></tr>
    <tr><td colspan="2" class="text-center">背景音乐</td></tr>
    <tr><td colspan="2">
        <ul class="list-unstyled" id="music_ul">
            <asp:Repeater runat="server" ID="Music_RPT" EnableViewState="false">
                <ItemTemplate>
                    <li class="music_item">
                        <label class="wrap">
                            <div style="float: left;">
                                <i class="fa fa-music" style="color: #0094ff;"></i>
                            </div>
                            <div class="name"><%#ZoomLa.Common.StringHelper.SubStr(Eval("Name",""),15) %></div>
                            <div class="ischoose">
                                <i class="fa fa-check" style="color: green;"></i>
                            </div>
                            <input type="radio" name="music_rad" value="<%#Eval("VPath") %>" style="display: none;" />
                        </label>
                    </li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
        </td></tr>
</table>
<asp:HiddenField runat="server" ID="Photos_Hid" />
<asp:HiddenField runat="server" ID="UseTlp_Hid" />
<div style="height:50px;"></div>
<asp:Button runat="server" ID="Save_Btn" class="btn btn-lg btn-primary footbtn" OnClick="Save_Btn_Click" Text="生成相册" OnClientClick="return subcheck();" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<script>
    var $photo_ul = $("#photo_ul");
    $(function () {
        ZL_Webup.config.json.ashx = "action=OAattach";
        ZL_Webup.config.json.accept = "img";
        ZL_Webup.config.json.compress = { width: 400, height: 400, compressSize: 100 * 1024 };
        $("#upfile_btn").click(ZL_Webup.ShowFileUP);
        $(".music_item").click(function () {
            $(".music_item").removeClass("active");
            $(this).addClass("active");
        });
        $(".tlp_item").click(function () {
            $(".tlp_item").removeClass("active");
            $(this).addClass("active");
            $("#UseTlp_Hid").val($(this).data("id"));
        });
      
        $(".auto_width").css("width", $(document).width());
        $("#tlp_ul,#photo_ul").show();
        //$(window).resize(function () {
        //    $(".auto_width").css("width", $(document).width());
        //});
        //------------------
        if ("<%:Mid%>" != "0")
        {
            $(".tlp_item").each(function () {
                if ($(this).data("id") == $("#UseTlp_Hid").val()) { $(this).addClass("active"); }
            });
            $("#Photos_Hid").val().split("|").forEach(function (a) {
                if (a != "") {
                    var tlp = '<li class="photo_item"><img src="' + a + '" /><span class="opspan"><button type="button" class="btn btn-danger" onclick="delPhoto(this);">删除</button></span></li>';
                    $photo_ul.append(tlp);
                }
            });
        }
    })
    function setMusic(music) {
        $("input[name='music_rad']").closest(".music_item").click();
    }
    function AddAttach(file, ret, pval) {
        //------更新显示图片
        var tlp = '<li class="photo_item"><img src="' + ret._raw + '" /><span class="opspan"><button type="button" class="btn btn-danger" onclick="delPhoto(this);">删除</button></span></li>';
        $photo_ul.append(tlp);
        ZL_Webup.attachDiag.CloseModal();
    }
    function delPhoto(obj) { $(obj).closest(".photo_item").remove();}
    function subcheck() {
        if ($(".photo_item").length < 1) { alert("必须上传图片"); return false; }
        if (ZL_Regex.isEmpty($("#UseTlp_Hid").val())) { alert("尚未选择模板"); return false; }
        if (ZL_Regex.isEmpty($("#AlbumName_T").val())) { alert("相册名称不能为空"); return false; }
        var photos = "";
        $(".photo_item").each(function () { photos += $(this).find("img").attr("src") + "|"; });
        $("#Photos_Hid").val(photos)
        return true;
    }
    //{
    //    width: 1600,
    //    height: 1600,

    //    // 图片质量，只有type为`image/jpeg`的时候才有效。
    //    quality: 90,

    //    // 是否允许放大，如果想要生成小图的时候不失真，此选项应该设置为false.
    //    allowMagnify: false,

    //    // 是否允许裁剪。
    //    crop: false,

    //    // 是否保留头部meta信息。
    //    preserveHeaders: true,

    //    // 如果发现压缩后文件大小比原来还大，则使用原来图片
    //    // 此属性可能会影响图片自动纠正功能
    //    noCompressIfLarger: false,

    //    // 单位字节，如果图片大小小于此值，不会采用压缩。
    //    compressSize: 0
    //}
</script>
</asp:Content>