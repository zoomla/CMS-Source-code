<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="camara.aspx.cs" Inherits="ZoomLaCMS.Design.Diag.Image.camara" MasterPageFile="~/Common/Common.master" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>拍摄照片</title>
<link href="/Plugins/FancyBox/jquery.fancybox.css" rel="stylesheet" />
<style>
    .photocamara .preimg{width:50px;height:50px;}
    .photocamara .phototools{background-color:#121212; float:left;width:100%;padding:5px;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container photocamara">
        <div class="panel panel-default margin_t5" id="photo_div">
          <div class="panel-body">
            <div id="photoview">
                <video id="video" autoplay="autoplay" style="width:100%;text-align:center"></video>
            </div>
            <div class="phototools">
                <div class="col-sm-4 col-xs-4 col-md-4 col-lg-4">
                    <a class="preimg_a" href="/Images/userface/noface.png"><img class="preimg" src="" onerror="shownoface(this);" /></a>
                </div>
                <div class="col-sm-4 col-xs-4 col-md-4 col-lg-4 text-center"><button type="button" id="shoot_btn" class="btn btn-default" style="border-radius:50px; width:50px; height:50px;"></button></div>
                <div class="col-sm-4 col-xs-4 col-md-4 col-lg-4 text-right">
                    <a href="javascript:;" title="确认" onclick="SaveImg()"><span class="fa fa-check-circle" style="font-size:50px;color:white;"></span></a>
                </div>
            </div>
          </div>
        </div>
        <div class="panel panel-primary margin_t5" id="err_div" style="display:none;">
            <div class="panel-heading">拍照错误!</div>
            <div class="panel-body">
                <div class="text-center"><span id="err_span"></span></div>
                <div class="text-center margin_t10">
                    <button type="button" class="btn btn-primary" onclick="location.href='camara.aspx'">刷新页面</button>
                    <button type="button" class="btn btn-primary" onclick="location.href='/Design/Diag/AddComp.aspx'">返回上一页</button>
                </div>
            </div>
        </div>
    </div>
    
    <script type="text/javascript" src="/Plugins/FancyBox/jquery.fancybox.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Webup.js"></script>
    <script src="/JS/ICMS/ZL_Common.js"></script>
    <script>
        var video = document.getElementById("video");
        var videostream;
        $(function () {
            $(".preimg_a").fancybox({
                'titlePosition': 'over',
                helpers: {
                    thumbs: {
                        width: 50,
                        height: 50
                    }
                }
            });
            $("#shoot_btn").click(function () {
                shoot();
            });
            EnableCamera();
        });
        //拍照操作
        function shoot() {
            var canvas = capture(video);
            var imgData = canvas.toDataURL("image/jpg");
            $(".preimg").attr('src', imgData);
            $(".preimg_a").attr('href', imgData);
           
        }
        //开启摄像头
        function EnableCamera() {
            navigator.getUserMedia = navigator.getUserMedia || navigator.webkitGetUserMedia || navigator.msGetUserMedia;
            if (navigator.getUserMedia) //  
            {
                if (navigator.webkitURL)//  
                {
                    navigator.getUserMedia({ video: true }, function (stream) {
                        video.src = window.webkitURL.createObjectURL(stream);
                    }, function (error) { ShowErrMsg('没有检测到摄像头!请确认摄像头已插好或是否被浏览器禁用') });
                }
                else if (navigator.msURL) {
                    navigator.getUserMedia({ video: true }, function (stream) {
                        videostream = stream;
                        video.src = window.msURL.createObjectURL(stream);

                    }, function (error) { ShowErrMsg('没有检测到摄像头!请确认摄像头已插好或是否被浏览器禁用') });
                }
                else //  
                {
                    navigator.getUserMedia({ video: true }, function (stream) {
                        videostream = stream;
                        video.src = window.URL.createObjectURL(stream);
                    }, function (error) { ShowErrMsg('没有检测到摄像头!请确认摄像头已插好或是否被浏览器禁用') });
                }
            }
            if (!navigator.getUserMedia) {
                ShowErrMsg('您的浏览器不支持在线拍照功能,请尝试切换chrome或edge浏览器以获得最佳体验!')
                return;
            }
        }
        function StopCamera() {
            if (videostream)
            { videostream.stop(); }
            video.src = '';
        }
        //從video元素抓取圖像到canvas
        function capture(video) {
            var canvas = document.createElement('canvas'); //建立canvas js DOM元素
            canvas.width = video.videoWidth;
            canvas.height = video.videoHeight;
            var ctx = canvas.getContext('2d');
            ctx.drawImage(video, 0, 0);
            return canvas;
        }
        function SaveImg() {
            if ($(".preimg").attr('src').indexOf("data:image") > -1) {
                var base64String = $(".preimg").attr('src').substr(22); //取得base64字串
                SFileUP.AjaxUpBase64(base64String, function (url) {
                    var model = {
                        dataMod: { src: url },
                        config: { type: "image", compid: "", css: "candrag", style: 'position:absolute;top:30%;left:40%;', imgstyle: "width:150px;height:150px;" }
                    };
                    parent.AddComponent(model);
                })
            }
            StopCamera();
            parent.CloseDiag();
        }
        function ShowErrMsg(msg) {
            $("#photo_div").hide();
            $("#err_div").show();
            $("#err_span").html(msg);
        }
    </script>
</asp:Content>


