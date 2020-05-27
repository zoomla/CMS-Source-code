<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.home" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>移动微信管理平台</title>
<style>
body{ FILTER: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#99CCFF,endColorStr=#fff); /*IE 6 7 8*/  
background: -ms-linear-gradient(top, #99CCFF,  #fff) no-repeat;        /* IE 10 */  
background:-webkit-gradient(linear, 0% 0%, 0% 100%,from(#99CCFF), to(#fff)) no-repeat;/*谷歌*/ }
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="MainApp" class="wxapp_box">
<div class="wxapp">
<a href="WxAppManage.aspx"><i class="fa fa-globe center-block" ></i>欢迎语</a>  
</div>  
<div class="wxapp">
<a href="WxAppManage.aspx"><i class="fa fa-comments-o center-block" style="background:#73B9FF;"></i>自动回复</a>
</div>  
<div class="wxapp">
<a href="WxAppManage.aspx&type=image"><i class="fa fa-newspaper-o center-block" style="background:#FFC926;"></i>素材管理</a>
</div>  
<div class="wxapp">
<a href="javascript:;" onclick="ShowSendApp()"><i class="fa fa-rss center-block" style="background:#0085B2;"></i>高级群发</a>
</div>  
<div class="wxapp">
<a href="WxAppManage.aspx"><i class="fa fa-book center-block" style="background:#00D9D9;"></i>菜单配置</a>
</div> 
<div class="wxapp">
<a href="WxConfig.aspx"><i class="fa fa-cog center-block" style="background:#FF4DFF;"></i>微信配置</a>
</div>  
<div class="wxapp">
<a href="WxAppManage.aspx"><i class="fa fa-users center-block" style="background:#686859;"></i>粉丝管理</a>
</div>  
<div class="wxapp">
<a href="SendWx.aspx"><i class="fa fa-history center-block" style="background:#ff006e;"></i>刮刮卡</a>
</div>  
<div class="wxapp">
<a href="SendWx.aspx"><i class="fa fa-futbol-o center-block" style="background:#ff006e;"></i>大转盘</a>
</div>  
<div class="wxapp">
<a href="/Admin/Template/TemplateSetOfficial.aspx"><i class="fa fa-cloud center-block" style="background:#ff006e;"></i>模板云</a>
</div>  
<div class="wxapp">
<a href="http://app.z01.com/" target="_blank" class="wxapp_a"><i class="fa fa-mobile center-block" style="background:#ff6a00;"></i>微场景</a>
</div>
<div class="wxapp">
<a href="http://www.z01.com/mtv" target="_blank"  class="wxapp_a"><i class="fa fa-film center-block" style="background:#ff6a00;"></i>云视频</a>
</div>
<div class="wxapp">
<a href="/Admin/Sentiment/Default.aspx"><i class="fa fa-dashboard center-block" style="background:#00D9A3;"></i>舆情监测</a>
</div>  
<div class="wxapp">
<a href="/Admin/Content/ECharts/AddChart.aspx"><i class="fa fa-line-chart center-block" style="background:#ff006e;"></i>智能图表</a>
</div>  
<div class="wxapp">
<a href="http://www.z01.com/other/2400.shtml" target="_blank" class="wxapp_a"><i class="fa fa-language center-block" style="background:#00698C;"></i>智能采集</a>
</div>  
</div>
<div class="wxapp_box" id="SendAll" style="display:none;">
<div class="wxapp">
<a href="WxAppManage.aspx"><i class="fa fa-comment center-block" ></i>消息群发</a>  
</div> 
<div class="wxapp">
    <a href="WxAppManage.aspx"><i class="fa fa-file-image-o center-block"></i>图文群发</a>
</div>
<div class="wxapp">
    <a href="javascript:;" onclick="ShowMainApp()"><i class="fa fa-reply center-block"></i>返回菜单</a>
</div>
</div>
<script>
    function ShowSendApp() {
        $("#MainApp").hide();
        $("#SendAll").show();
    }
    function ShowMainApp() {
        $("#MainApp").show();
        $("#SendAll").hide();
    }
</script>
</asp:Content>
