<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewPublish.aspx.cs" Inherits="test_user_demo" ClientIDMode="Static" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title><asp:Literal runat="server" ID="Literal1"></asp:Literal></title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content"> 
    <div style="height: 51px; background: #FFFFFF;line-height:51px;">
            <div style="width: 980px;margin:auto;padding-left:60px;padding-right:30px;">
                <img src="<%:Call.Logo %>" style="height: 51px;" />
                <div class="top_right_div">
                    <a href="#">首页</a>
                    <a href="#">新闻</a>
                    <a href="#">娱乐</a>
                    <a href="#">体育</a>
                    <a href="#">文化</a>
                    <a href="#">教育</a>
                    <a href="#">图片</a>
                    <a href="#">时尚</a>
                    <a href="#">旅游</a>
                    <a href="#">美食</a>
                    <a href="#">视频</a>
                    <a href="#">法律</a>
                    <a href="#">财经</a>
                    <a href="#">论坛</a>
                </div>
            </div>
        </div>
        <div style="height: 68px; background: #463229;"></div>
        <div style="height: 15px; background: #E6A56D;"></div>
        <div style="width:980px;top:60px;margin:0 auto;position:relative;top:-70px;">
            <div style="width: 420px; height: 600px; float: left;z-index:2;">
                <div id="maindiv" class="main">
                    <div style="border:2px #bfbfbd solid;"><!--background:url(/UploadFiles/Admin/Publish/18/A01.jpg) no-repeat;display:block;width:400px;height:600px;-->
                        <img runat="server" id="newimg" src="new1.jpg" style="width: 400px; height: 600px;" />
                    </div>
                    <div style="text-align:right;">
                        <span style="float:left;" runat="server" id="Title_Span"></span>
                        <span>
                            <asp:LinkButton runat="server" CssClass="aStyle" ID="lbDown" OnClick="lbDown_Click" Text="附件下载" />
                            <a href="#" style="font-size: 14px; font-weight: bold;">版面预览</a>
                            <asp:Button runat="server" ID="Button1" Text="上一版" OnClick="Pre_Btn_Click" />
                            <asp:Button runat="server" ID="Button2" Text="下一版" OnClick="Next_Btn_Click" />
                        </span>
                    </div>
                </div>
            </div>
            <div class="mainDetais" style="float: left; padding-left:20px;width: 530px;">
                <div class="right1">
                    <a href="#">返回首页</a><a href="#">报纸头版</a><a href="#">版面列表</a><a href="#">内容列表</a><a href="#">付费订阅</a>
                </div>
                <div class="right2">
                    <asp:Button runat="server" Text="上一期" OnClick="PrePid_Btn_Click"  ID="PrePid_Btn" />
                    <asp:Button runat="server" Text="下一期" OnClick="NextPid_Btn_Click" ID="NextPid_Btn" />
                </div>
                <div class="loading"></div>
                <div class="news">
                    <div class="newstop">
                        <input type="button" value="上一篇" disabled="disabled"/>
                        <input type="button" value="下一篇" disabled="disabled"/>
                    </div>
                    <iframe id="news_ifr" style="height:500px;width: 100%;border: none;"></iframe>
                </div>
            </div>
            <div style="clear: both;"></div>
        </div>
        <asp:HiddenField runat="server" ID="CurID_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
         *{padding:0px;margin:0px;}
         a{font-size:12px;color:#694916;text-decoration:none;cursor:pointer;}
         a:hover{color:orange;}
         .top_right_div{float: right;}
         .main{width: 405px;position:absolute;padding-left:9px;z-index:1;}
         .tipsText{margin-top:50px;text-align:center;filter:alpha(opacity:90);display:none;padding: 8px; background-color: rgb(235, 84, 93); color: rgb(69, 7, 11);font-family:'Microsoft YaHei'; }
         .items{position:absolute;z-index:1040;text-align:center;cursor:pointer;background-color:rgba(0, 0, 0, 0.00);}
         .items:hover{border:1px dashed black;}
         .right1{padding-left:30px;}
         .right1 a{ color:white;font-weight:bold;padding-left:10px;}
         .right2{padding:10px;padding-left:20px;}
         .news{border:1px #d2d2d2 solid;margin-top:15px;background:#FFFFFF;}
         .newstop{background-color:#f0f0f0;text-align:right;height:30px;line-height:30px;padding-right:30px;}  
         .aStyle{font-size: 14px; font-weight: bold;}       
    </style>
    <script type="text/javascript">
        var tlp = "<div class='items' style='left: {x}; top: {y}; width: {w}; height: {h};' data-cid='{gid}'>" +
            "<div class='tipsText'><span class='textBold'>{re}</span></div>" +
            "</div>";
        //解析Json生成
        function AnalyJson(json) {
            var jsonArr = JSON.parse(json);
            for (var i = 0; i < jsonArr.length; i++) {
                var j = jsonArr[i];
                j.w = Addpx(j.w, 13);
                j.h = Addpx(j.h, 20);
                var divt = tlp.replace("{x}", j.x).replace("{y}", j.y).replace("{re}", j.remind).replace("{w}", j.w).replace("{h}", j.h).replace("{gid}", j.gid);
                $("#maindiv").append(divt);
            }
            $(".items").hover(function () {
                $(this).find(".tipsText").show();
            }, function () { $(this).find(".tipsText").hide(); }).click(function () {
                var loadUrl = "news.aspx?id=" + $(this).attr("data-cid");
                $(".loading").show();
                $(".news").hide();
                $("#news_ifr").attr("src", loadUrl);
                $(".loading").hide();
                $(".news").show();
            });
        }
        function Addpx(v, v2) {
            return (parseInt(v.replace("px", "")) + v2) + "px";
        }
            </script>
</asp:Content>

