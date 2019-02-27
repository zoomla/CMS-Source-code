<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CloudLead.aspx.cs" Inherits="Manage_I_Template_CloudLead" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/App_Themes/AdminDefaultTheme/thickbox.css" rel="stylesheet" />
    <title>网站云台</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="background: #f7f7f7; height: 100%;">
        <div class="metro_con">
            <img src="/App_Themes/Admin/tem_ban.jpg" alt="" />
        </div>
        <div>
            <span class="panel_tui"><a href="TemplateSetOfficial.aspx" class="btn btn-primary" target="_self">云端模板</a></span>
            <input type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal1" value="使用前必读" />
            <!-- Modal -->
            <div class="modal fade" id="myModal1" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" style="font-family:'Microsoft YaHei'" id="myModalLabel">免费云方案下载</h4>
                        </div>
                        <div class="modal-body">
                            <p style="line-height:24px;">
                                为了让人人都建好网站，我们免费提供模板方案。<br />
                                可从云端免费下载云方案，每个方案均是一个完整的网站，包括完整的模板、栏目结构、标签及模型等等。<br />
                                因是完整网站方案下载并需要校验版本信息，需您稍候几分钟，请耐心等待！
                            </p>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <a title="获取网站云" href="TemplateSetOfficial.aspx" class="btn btn-primary">获取网站云</a>
        </div>

        <div class="apple_overlay" id="hidCont" style="display: none; position: absolute; top: 0; z-index: 9999;">
            <div class="fancybox-bg" id="fancybox-bg-n"></div>
            <div class="fancybox-bg" id="fancybox-bg-ne"></div>
            <div class="fancybox-bg" id="fancybox-bg-e"></div>
            <div class="fancybox-bg" id="fancybox-bg-se"></div>
            <div class="fancybox-bg" id="fancybox-bg-s"></div>
            <div class="fancybox-bg" id="fancybox-bg-sw"></div>
            <div class="fancybox-bg" id="fancybox-bg-w"></div>
            <div class="fancybox-bg" id="fancybox-bg-nw"></div>
            <div id="fancybox-content">
                <h2>免费云方案下载</h2>
                <br />
                <div class="overlay-content">
                    <p>
                        为了让人人都建好网站，我们免费提供模板方案。<br />
                        可从云端免费下载云方案，每个方案均是一个完整的网站，包括完整的模板、栏目结构、标签及模型等等。<br />
                        因是完整网站方案下载并需要校验版本信息，需您稍候几分钟，请耐心等待！<br />
                    </p>
                    <!--<input type="submit" id="Login" value="&nbsp;&nbsp;Ok&nbsp;&nbsp;" onclick="tb_remove()" />-->
                </div>
            </div>
            <a id="fancybox-close" href="javascript:void(0);" onclick="tb_remove()" style="display: inline;"></a><a id="fancybox-left" href="javascript:;"><span class="fancy-ico" id="fancybox-left-ico"></span></a><a id="fancybox-right" href="javascript:;"><span class="fancy-ico" id="fancybox-right-ico"></span></a>
        </div>
        <div class="metro_wrap_1">
            <div class="metro_panel_1">
                <div class="col_3"><a href="/APP/TemplateSetOfficial.aspx" class="btn_tui2"></a></div>
            </div>
        </div>
        <div class="metro_wrap_2"></div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/thickbox-compressed.js"></script>
    <script type="text/javascript">
        var Num = 0;
        var nn = 0;
        function help_show(helpid) {
            Num++;
            var newDiv = document.createElement('div');
            var str = "<div id='help_content'  style='z-index:999;'></div><div id='help_hide'  style='z-index:999;'><a onclick='help_hide(Num)' style='width:20px;color:#666' title='关闭'></a></div> ";
            newDiv.innerHTML = str;
            newDiv.setAttribute("Id", "help_div" + Num);
            nn = Num - 1
            jQuery("#help").append(newDiv);
            if ($("#help_div" + nn))
            { $("#help_div" + nn).remove(); }
            jQuery("#help_content").load("/manage/help/" + helpid + ".html", function () { jQuery("#help").show(); });
        }
        function help_hide(Num) {
            jQuery("#help_div" + Num).remove();
        }
    </script>
    <script type="text/javascript">
        function geturl() {
            tb_remove();
        }
    </script>
    <style>
        #right {
            background: #f7f7f7;
        }
    </style>
</asp:Content>
