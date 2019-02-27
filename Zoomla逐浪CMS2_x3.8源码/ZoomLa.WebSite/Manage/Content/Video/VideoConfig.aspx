<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VideoConfig.aspx.cs" Inherits="Manage_Content_Video_VideoConfig" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>视频配置</title>
    <style> 
    .video_btn{ width:100%;}
    .video_set{ display:none; height:200px; line-height:36px;}
    .video_set.active{ display:inline-block; width:100%;}
    .video_recom{ line-height:36px;}
    .video_recom strong{ float:left; display:block; width:80px;}
    .video_recom .btn-group{ float:left; margin-right:10px; }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="video_main">    
      <div class="alert alert-info fade in margin_b2px">
      <button type="button" class="close" data-dismiss="alert" aria-hidden="true">×</button>
      <h4>提示!</h4>
          <p style="padding-left: 50px; line-height: 40px;">
              <span class="fa fa-warning" style="color: #f00; font-size: 18px;"></span>
              <span runat="server" id="remind_sp">您当前无法使用视频转换功能，必须先下载视频解码器才可正常使用该功能，是否立即进入下载</span>
                  <asp:Button runat="server" ID="getLoad" Style="margin-left: 5px;" Text="立即下载" CssClass="btn btn-default" OnClick="BDown_Click" />
          </p>
          <div class="progress margin_t5" style="display: none;">
              <div class="progress-bar" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0; min-width: 2%;">0%</div>
          </div>
    </div>
        <table class="table table-striped table-bordered">
            <tr>
                <td class="text-right td_l">视频默认目录:</td>
                <td><asp:TextBox runat="server" ID="videoPath" CssClass="form-control text_300" Text="/UploadFiles/video/"></asp:TextBox></td>
            </tr>
            <tr><td></td><td><asp:Button ID="SaveConfig" Text="保存" CssClass="btn btn-primary" runat="server" OnClick="SaveConfig_Click" /> </td></tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link type="text/css" href="/Plugins/JqueryUI/ProgressBar/css/jquery.ui.theme.css" rel="stylesheet" />
<link type="text/css" href="/Plugins/JqueryUI/ProgressBar/css/jquery.ui.progressbar.css" rel="stylesheet" />
<script type="text/javascript" src="/js/Common.js"></script>
<script type="text/javascript" src="/Plugins/JqueryUI/ProgressBar/js/jquery-ui.custom.js"></script>
<style type="text/css">
.ui-progressbar {position: relative;top:50%;}

</style>
<script>
    $(".video_btn").find("input").bind("click", function () {
        var btnId = $(this).attr("id");
        if (btnId == "next_set") {//下一步
            var classId = $(".video_main").find(".active").attr("id");
            $(".video_set").removeClass("active");
            var temp = classId.replace(/[^0-9]/ig, "");          
            temp = parseInt(temp) + 1;
            if (temp > $(".video_set").length)  
                temp = $(".video_set").length;
            if (temp == $(".video_set").length) {
                $("#next_set").hide();
                $("#SaveConfig").show();
            }
             $("#video_step" + temp).addClass("active");
            $("#prev_set").show();
        }
        else if (btnId == "prev_set") {//上一步
            var classId = $(".video_main").find(".active").attr("id");
            $(".video_set").removeClass("active");
            var temp = classId.replace(/[^0-9]/ig, "");
            temp = parseInt(temp) - 1;
            if (temp < 1)  
                temp = 1;               
            if (temp == 1)  
                $("#prev_set").hide();           
            $("#video_step" + temp).addClass("active");
            $("#next_set").show();
            $("#SaveConfig").hide();
        }
    })    
</script>
<script>
    //实时获取进度,后台用异步或线程池
    function ajaxPost(obj, a, val) {//this,actionName,要上传的值,回调方法,是否弹出回复窗(true显示,false不显示)
        $.ajax({
            type: "Post",
            url: "VideoConfig.aspx",
            data: { action: a, value: "ProgMod" },
            success: function (data) {
                $(".progress-bar").css('width', data + '%');//设置进度条的长度
                $(".progress-bar").text(data+'%');
                if (data>=100) {
                    $("#next_set").removeAttr("disabled");
                    clearInterval(interval);
                }
            },
            error: function (data) {
            }
        });
    }
    var interval;
    function beginCheck(request) {
        $(".progress").show();
        setTimeout(function () { document.getElementById('getLoad').disabled = true; }, 50);
        $("#progressbar").show();
        interval = setInterval(function () { ajaxPost(this, request, '') }, 1000);
    }//实时获取后台进度

</script>
</asp:Content>
    