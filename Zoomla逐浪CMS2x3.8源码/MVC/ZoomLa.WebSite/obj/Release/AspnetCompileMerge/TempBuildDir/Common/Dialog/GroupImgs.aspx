<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GroupImgs.aspx.cs" Inherits="ZoomLaCMS.Common.Dialog.GroupImgs" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>选择文件</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container-fluid">
        <ul id="formul" class="list-unstyled">
            
        </ul>
    </div>
    <div class="container-fluid text-center margin_t10">
        <button class="btn btn-primary" type="button" onclick="SortImgData()">确定</button>
        <button class="btn btn-primary" type="button" onclick="parent.CloseDiag()">取消</button>
        
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <link rel="stylesheet" href="/App_Themes/V3.css" />
    <style id="styles">
        img{width:100%; height:150px;}
        #formul li{padding:5px;}
        #formul li .boders{padding:10px; border:1px solid #ccc;margin-left:10px; border-radius:5px; position:relative;}
        .img_title{background:rgba(0,0,0,0.5);line-height:30px;position:absolute;top:0;left:0;z-index:300;overflow:hidden; color:#fff;border-radius:5px; width:100%; text-align:center; height:0;}
        .textbox{width:100%; margin-top:5px;}
        
    </style>
    <script type="text/javascript" src="/JS/jquery-ui.min.js"></script>
    <script type="text/javascript" src="/JS/ICMS/ZL_Common.js"></script>
    <script type="text/javascript" src="/JS/ICMS/alt.js"></script>
    <script>
        var json = null;
        $().ready(function () {
            json = JSON.parse('<%=Request.QueryString["json"] %>');
                InitImgs();
                //初始化拖动
                $("#styles").append(".highlight{background-color:#ddd; border-radius:5px; margin:5px; width:" + ($("#formul li .boders").width() + 30) + "px; height:" + ($("#formul li .boders").height() + 20)
                                    + "px;float:left;}");
                $("#formul").sortable({
                    placeholder: "highlight",
                    cursor: 'crosshair'
                });
                //图片图标操作
                $("#formul li .boders").mouseenter(function () {
                    $btns = $(this).find(".img_title");
                    $btns.stop().animate({ height: 30 });
                }).mouseleave(function () {
                    $btns = $(this).find(".img_title");
                    $btns.stop().animate({ height: 0 });
                });
            });

            function InitImgs() {
                var imgs = json.imgs;
                var tlp = "<li class='col-lg-3 col-md-3 col-sm-3'><div class='boders'><div class='img_title'>@fname</div>"
                        + "<img src='@imgurl' /><div><textarea class='textbox form-control' placeholder='图片备注'>@desc</textarea></div></div></li>";
                for (var i = 0; i < imgs.length; i++) {
                    $("#formul").append(tlp.replace(/@imgurl/, json.pval.uploaddir + imgs[i].url).replace(/@desc/, imgs[i].desc).replace(/@fname/, GetFname(imgs[i].url)));
                }
            }
            function SortImgData() {
                var ret = {};
                ret._raw = "";
                json.pval.descs = [];
                $("#formul li img").each(function (i, v) {
                    ret._raw += $(v).attr('src') + ($("#formul li img").length - 1 != i ? "|" : "");
                    json.pval.descs.push($(v).parent().find('.textbox').val());
                });
                json.pval.isGroup = true;
                parent.AddAttach(null, ret, json.pval);
                parent.CloseDiag();
            }

        </script>
</asp:Content>

