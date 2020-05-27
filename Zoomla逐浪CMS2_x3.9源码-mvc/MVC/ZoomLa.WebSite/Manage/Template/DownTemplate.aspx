<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownTemplate.aspx.cs" Inherits="ZoomLaCMS.Manage.Template.DownTemplate"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>方案列表</title>
    <style>
    .lightbox img{border:1px solid #ccc;padding:3px;border-radius:3px;margin-bottom:5px;}
    #DownTips .alert-success{height:86vh;background-color:rgba(91, 192, 222, 0.95);border-color:#5bc0de;color:#fff;margin-bottom:35px;} 
    #DownTips .alert-success h4{margin-top:30vh;}
    #DownTips .alert-success p{margin-top:35px;}
    #downbtn{background-color:rgba(255, 255, 255, 0.75)}
    #downimg{margin: 20px auto; width: 80%;background-color:rgba(234, 234, 234, 0.95);border-radius:5px;display:none;padding:35px 100px 0 100px;height: 125px;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField ID="hfIsExist" runat="server" />
    <table class="table table-striped table-bordered text-center">
        <tr align="center">
            <td colspan="2">
                <asp:Label ID="LblTitle" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server">
                    <div id="gallery">
                        <div class="">
                            <asp:Literal ID="tempimg" runat="server"></asp:Literal></div>
                        <div id="DownTips" class="bs-example navbar navbar-fixed-bottom" style="width: 80%; display: none;margin:0 auto;">
                            <div class="alert alert-success fade in">
                                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                                <h4>模板方案下载完成，您现在就可以设为默认使用新模板。</h4>
                                <p>
                                    <button id="setdef_b" type="button" onclick="setDefault()" class="btn btn-default"><i class="fa fa-square-o"></i> 设为默认</button> 
                                    设置完成后可点击这里<a id="mnbak_b" href="../Content/Addon/MNBakList.aspx">[恢复元数据]</a>（如果有）
                                </p>
                            </div>
                        </div>
                        <!-- /example -->
                    </div>
                </asp:Panel>
                <asp:Panel ID="Panel3" runat="server">
                    <asp:Literal ID="installstart" runat="server"></asp:Literal>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div id="downimg"  class="navbar navbar-fixed-bottom">
        [<asp:Literal ID="tempname" runat="server"></asp:Literal>]模版下载中请耐心等待片刻…
        <div class="progress progress-striped active" style="margin-top:5px;">
            <div id="downTempDiv" class="progress-bar" role="progressbar" aria-valuenow="60" aria-valuemin="0" aria-valuemax="100">
                <span id="downTempSpan" class="sr-only" style="position: relative;"></span>
            </div>
        </div>
    </div>
    <div id="downbtn" class="navbar navbar-fixed-bottom text-center">
        <input type="button" id="down_b" value="下载模板" onclick="beginCheck('getTempP');" class="btn btn-info" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <link href="/Plugins/JqueryUI/LightBox/css/lightbox.css" rel="stylesheet" media="screen" />
    <script src="/Plugins/JqueryUI/LightBox/jquery.lightbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            base_url = document.location.href.substring(0, document.location.href.indexOf('index.html'), 0);
            $(".lightbox").lightbox({
                fitToScreen: true,
                imageClickClose: false
            });
        });
        var wida = 0;
        var interval;
        var actionArr = "getTempP";//下载模板
        function PostToCS(a, v) {
            $.ajax({
                type: "Post",
                url: "DownTemplate.aspx",
                data: { action: a, value: v },
                success: function (data) {
                    $("#downTempDiv").css("width", data + "%");
                    $("#downTempSpan").text(data + "%");
                    if (data == 100) {
                        clearInterval(interval);
                        $("#downimg").hide();
                        $("#DownTips").show();
                    }
                },
                error: function (data) {
                }
            });
        }
        //调用其开始循环获取
        function beginCheck(request) {
            $("#downimg").show();
            $("#downbtn").hide();
            interval = setInterval(function () { PostToCS(request, '') }, 1000);
        }
        function setDefault() {
            if (!confirm("确认将该模板设为默认吗?")) { return; }
            $.ajax({
                type: "Post",
                url: "DownTemplate.aspx",
                data: { action: 'setdefault', value: '<%=Request.QueryString["dir"] %>' },
                success: function (data) {
                    $("#setdef_b").attr("disabled", "disabled").html("<i class='fa fa-check-square-o'></i> 设置成功");
                }
            });
        }
        //完成,可以开始安装
        //跳转
        function jump() {
            window.location.href = "<%= customPath2%>Config/SiteOption.aspx?prodirName=<%=prodirName%>";
        }
    </script>
</asp:Content>
