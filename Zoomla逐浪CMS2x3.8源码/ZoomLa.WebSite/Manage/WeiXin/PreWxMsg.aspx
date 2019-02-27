<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PreWxMsg.aspx.cs" ValidateRequest="false" Inherits="Manage_WeiXin_PreWxMsg" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>预览图文</title>
<style>
    .materlist .infolist{border:1px solid #ddd; padding:0; text-align:center;}
    .materlist .infolist img {width: 100%;border-bottom: 1px solid #ddd;}
    .materlist .infolist .title{line-height:25px;border-bottom:1px solid #ddd; background:none;color:#999;word-wrap:break-word;}
    .materlist .infolist .option{color:#999; line-height:25px;}
    .materlist .newslist {width:400px;border: 1px solid #ddd;line-height:25px; border-radius:5px; padding:0px; cursor:pointer;}
     .materlist .newslist .info{padding:10px; position:relative;}
     .materlist .newslist .info .date{font-size:12px;}
    .materlist .newslist img{width:100%; height:200px;}
    .materlist .newslist .option div{float:left;width:100%; line-height:40px; padding-left:5px; padding-right:5px; border-top:1px solid #ddd;}
    .materlist .newslist .option div:first-child{border-right: 1px solid #ddd;}
    .materlist .newslist .info .title{color:white; position:absolute; bottom:10px; opacity:0.7; width:378px; background-color:#000; padding:5px; font-weight:normal;}
    .materlist .newslist .sub_info{border-top:1px solid #ddd; padding:10px;}
    .materlist .newslist .sub_info div{line-height:40px; width:20%; float:left;}
     .materlist .newslist .sub_info div:first-child{width:80%;}
    .materlist .newslist .sub_info div img{width:70px; height:70px;}
    #precon_div .tips{color:#999;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="materlist">
    </div>
    <div id="precon_div" style="display:none;">
        <div class="container">
            <h3></h3>
            <div class="tips"><span class="pre_date"></span> <span><%=new ZoomLa.BLL.B_Admin().GetAdminLogin().AdminName %></span> </div>
            <img alt="正在加载.." src="#" style="width:100%" />
            <div class="prebody">

            </div>
        </div>
    </div>
    <div class="text-center margin_t5">
        <input type="button" value="发送" onclick="parent.PreCheckSend();" class="btn btn-primary sendimage" />
        <input type="button" value="关闭" onclick="parent.ClosePreDiag();" class="btn btn-primary sendimage" />
        <input type="button" value="返回" onclick="BackView()" style="display:none;" id="back_b" class="btn btn-primary" />
    </div>
    <div id="newinfo" style="display:none;">
        <div class="container-fluid newslist singleMsg">
            <div class="info">
                @title<br />
                <span class="gray_c date">@date</span>
                <img src="@imgurl" />
                <span class="gray_9">@desc</span>
            </div>
            <div class="option">
                <div>阅读全文<span class="pull-right"><span class="fa fa-chevron-right gray_9"></span></span></div>
            </div> 
        </div>
    </div>
    <div id="subnews" style="display:none;">
        <div class="container-fluid sub_info" data-index="@index">
            <div>@title</div><div><img src="@imgurl" /></div>
        </div>
    </div>
    <div id="newsinfo" style="display:none;">
        <div class="container-fluid newslist">
            <div class="info singleMsg">
                <img src="@imgurl" />
                <div class="title">@title</div>
            </div>
            @subinfo
        </div>
    </div>
    <asp:HiddenField id="News_Hid" runat="server" Value="" />
    <script>
        var dataArr = [];
        $().ready(function () {
            InitData();
            $(".singleMsg").click(function () {//单图文点击事件
                PreContent(dataArr[0])
            });
            $(".sub_info").click(function () {
                PreContent(dataArr[$(this).data('index')]);
            });
        });
        function PreContent(data) {
            $(".materlist").hide();
            $("#precon_div").show();
            $("#back_b").show();
            $("#precon_div img").attr('src', data.picurl);
            $("#precon_div h3").text(data.title);
            $("#precon_div .pre_date").text(new Date().getFullYear() + "-" + (new Date().getMonth() + 1) + "-" + new Date().getDate());
            $("#precon_div .prebody").html(data.description);
        }
        function BackView() {
            $(".materlist").show();
            $("#precon_div").hide();
            $("#back_b").hide();
        }
        function InitData() {
            if ($("#News_Hid").val() == "")
                dataArr = parent.dataArr;
            else {
                $(".sendimage").hide();
                dataArr = InitNewData();
            }
            console.log(dataArr);
            var tlp = "";
            if (dataArr.length == 1) {//单图文
                tlp = $("#newinfo").html().replace(/@title/g, dataArr[0].title).replace(/@date/g, new Date().getMonth() + 1 + "月" + new Date().getDate() + "日")
                .replace(/@imgurl/g, dataArr[0].picurl).replace(/@desc/g, "描述");
                $(".materlist").append(tlp);
            } else {
                var subinfo = "";
                for (var i = 0; i < dataArr.length; i++) {
                    if (i == 0) {
                        tlp = $("#newsinfo").html().replace(/@title/g, dataArr[0].title).replace(/@imgurl/g, dataArr[0].picurl);
                        continue;
                    }
                    subinfo += $("#subnews").html().replace(/@title/g, dataArr[i].title).replace(/@imgurl/g, dataArr[i].picurl).replace(/@index/g,i);
                }
                tlp = tlp.replace(/@subinfo/g, subinfo);
                $(".materlist").append(tlp);
            }
        }
        function InitNewData() {
            var dataAttr = [];
            var datas = JSON.parse($("#News_Hid").val());
            var subnews = JSON.parse(datas.content);
            for (var i = 0; i < subnews.length; i++) {
                var data = { id: GetRanPass(4), title: subnews[i].title, description: subnews[i].content, picurl: subnews[i].thumb_media_id, url: subnews[i].content_source_url, index: i };
                dataAttr.push(data);
            }
            return dataAttr;
        }
    </script>
</asp:Content>

