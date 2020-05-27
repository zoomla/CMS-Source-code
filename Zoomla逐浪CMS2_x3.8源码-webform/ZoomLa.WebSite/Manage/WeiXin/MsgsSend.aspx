<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MsgsSend.aspx.cs" Inherits="test_MsgsSend" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>微信群发</title>
<style>
.msgthumb .headimg{width:100%; max-height:160px;}
.wxlist li{cursor:pointer;}
.wxlist li .badge{display:none;}
.wxlist li .active{display:inline-block;}

.vediodiag{width:600px;}
</style>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="Message_Div" runat="server" visible="false" class="alert alert-danger" role="alert"><span class="fa fa-info-circle"></span> 您今天已群发过一次了!</div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-md-10 col-sm-12">
                <div class="container-fluid">
                    <div class="row">
                        <div class="appmsg multi col-lg-3 col-md-3 col-xs-3 col-sm-3">
                            <div class="msgthumb msgitemf" data-id="default">
                                <img src="" onerror="shownopic(this);" class="headimg" />
                                <div class="msgthumb_foot">
                                    <div class="title_div" style="float:left;">标题</div>
                                    <div style="float:right;"><a href="javascript:;" title="编辑" class="edit_a"><i class="fa fa-pencil"></i></a></div>
                                </div>
                            </div>
                            <div style="clear:both;"></div>
                            <div id="itemlist"></div>
                            <a onclick="return false;" class="appmsg_add" id="js_add_appmsg" href="javascript:void(0);" title="增加一个">
                                <i class="fa fa-plus icon20"></i>
                            </a>
                        </div>
                        <div class="col-lg-9 col-md-9 col-xs-9 col-sm-9">
                            <table class="table table-bordered table-striped">
                                <tr><td class="wxtextl"><i class="fa fa-text-width"></i>标题</td><td><asp:TextBox runat="server" ID="title_t" CssClass="form-control text_300" onkeyup="UpdateTitle(this);"/></td></tr>
                                <tr><td class="wxtextl"><i class="fa fa-file-picture-o"></i>图片</td><td>
                                    <asp:TextBox runat="server" ID="picurl_t" CssClass="form-control text_300" />
                                    <input type="button" value="选择图片" class="btn btn-info" onclick="SelectUppic({});" />
                                    <input type="button" value="选择内容" class="btn btn-info" onclick="SelContent()" />
                                                                                                          </td></tr>
                                <tr>
                                    <td class="wxtextl"><i class="fa fa-video-camera"></i>视频</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="Video_T" CssClass="form-control text_300" />
                                        <input type="button" value="选择视频" class="btn btn-info" onclick="SelVedio()" />
                                    </td>
                                </tr>
                                <tr><td class="wxtextl"><i class="fa fa-align-justify"></i>正文</td>
                                    <td>
                                    <asp:TextBox runat="server" ID="content_t" style="width:800px; height:300px;" TextMode="MultiLine" />
                                    <%=Call.GetUEditor("content_t",3) %>
                                    </td></tr>
                                <tr><td class="wxtextl"><i class="fa fa-link"></i>链接</td><td><asp:TextBox runat="server" ID="url_t" CssClass="form-control text_300" /><span class="wtinfo">(选填)</span></td></tr>
                            </table>
                        </div>
                    </div>
                </div>
                
                
            </div>
            <div class="col-md-2 col-sm-12">
                <div class="panel panel-primary">
                  <!-- Default panel contents -->
                  <div class="panel-heading">选择公众号 <span class="pull-right"><a href="javascript:;" style="color:white;" onclick="CheckAll()">全选</a></span></div>
                  <ul class="list-group wxlist">
                      <asp:Repeater ID="WxApp_RPT" runat="server">
                          <ItemTemplate>
                                <li class="list-group-item wx_option">
                                    <%#Eval("Alias") %> <span class="badge"><span class="fa fa-check"></span></span><input type="checkbox" name="appids" value="<%#Eval("ID") %>" style="display:none" />
                                </li>
                          </ItemTemplate>
                      </asp:Repeater>
                  </ul>
                </div>
            </div>
        </div>
        <div class="text-center">
            <button type="button" class="btn btn-primary" onclick="PreWxMsg()">预览效果</button>
            <asp:Button runat="server" ID="Save_Btn" CssClass="btn btn-primary" Text="群发图文" OnClick="Save_Btn_Click" OnClientClick="return PreSave();" />
            <asp:Button runat="server" ID="SaveNew_B" Visible="false" CssClass="btn btn-primary" Text="保存素材" OnClick="SaveNew_B_Click" OnClientClick="return PreSave();" />
            <%--<asp:Button runat="server" ID="SaveAll_Btn" CssClass="btn btn-primary" Text="推送图文" OnClick="TestMPNews_Btn_Click" OnClientClick="return PreSave()" />--%>
            <asp:Button runat="server" ID="Update_Btn" CssClass="btn btn-primary" Visible="false" Text="保存素材" OnClientClick="return UpdateData()" OnClick="Update_Btn_Click" />
            <asp:Literal ID="Return_Li" runat="server" EnableViewState="false" Visible="false"></asp:Literal>
            
        </div>
        <div class="alert alert-info alert-dismissible margin_t5" role="alert">
            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <dl id="status_dl">
                <dd><strong>发送状态:</strong></dd>
                <asp:Literal runat="server" ID="Status_Li" EnableViewState="false"></asp:Literal>
            </dl>
        </div>
    </div>    
    <div id="selvedio_div" style="display:none;">
         <table class="table table-bordered table-striped">
             <tr><td>视频地址:</td><td><input type="text" id="videosrc_t" class="form-control text_md" /> <span>只支持腾讯视频!</span></td></tr>
             <tr><td>视频宽度:</td><td><input type="text" id="vediowidth_t" class="form-control text_md" value="100" /></td></tr>
             <tr><td>视频高度:</td><td><input type="text" id="vedioheight_t" class="form-control text_md" value="100" /></td></tr>
             <tr><td colspan="2" class="text-center"><button type="button" class="btn btn-primary" onclick="SetVideoHtml()">确定</button> <button type="button" onclick="CloseVideoDiag()" class="btn btn-primary">取消</button></td></tr>
         </table>
    </div>
    <div id="seturl_div" style="display:none;">
        <table class="table table-bordered table-striped">
            <tr><td>链接名称:</td><td><input type="text" id="linkname_t" class="form-control text_md" /></td></tr>
            <tr><td>链接地址:</td><td><input type="text" id="linkurl_t" class="form-control text_md" /></td></tr>
             <tr><td colspan="2" class="text-center"><button type="button" class="btn btn-primary" onclick="SetUrlHtml()">确定</button> <button type="button" onclick="urldiag.CloseModal();" class="btn btn-primary">取消</button></td></tr>            
        </table>
        
    </div>
    <asp:HiddenField runat="server" ID="Article_Hid" />
    <asp:HiddenField runat="server" ID="News_Hid" Value="" />
    <asp:HiddenField runat="server" ID="CurNews_Hid" />

<%--    <asp:Button runat="server" ID="TestMPNews_Btn" Text="上传图文素材" OnClick="TestMPNews_Btn_Click" />--%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/ZL_Regex.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/JS/Controls/ZL_Array.js"></script>
    <style type="text/css">
		.wxtextl{ width:80px; text-align:center;}
		.wxtextl i{ margin-right:5px; font-size:16px; color:#060;}
        .msgthumb {color:#c0c0c0;text-align:center;line-height:160px;font-weight:400;font-style:normal;background-color:#ececec;font-size:16px;}
        .msgthumb_foot {position:absolute;width:91%;border-radius:3px; line-height:30px;margin-top:-32px;padding-left:5px;padding-right:5px; 
            filter:alpha(opacity=50); -moz-opacity:0.5; -khtml-opacity: 0.5; opacity: 0.5;background-color:black;font-size:14px;}
        .msgitem { margin-top:10px; margin-bottom:10px; padding:10px 14px;position:relative; height:100px; box-shadow:0 0 1px 1px #f5f5f5; background:#fcfcfc;}
		.msgopt{ position:absolute; bottom:10px; left:5px;}
        .itemleft {float:left;width:68%;}
        .itemright { float:right;width:30%; text-align:right;}
        .itemimg {max-width:80px;max-height:80px;}
        .appmsg_add {display:block;border:2px dotted #d9dadc;line-height:50px;text-align:center;margin-top:20px; margin-bottom:20px;}
        .icon20 {width:20px;height:20px;display:inline-block;line-height:50px;text-align:center;color:#c0c0c0;}
		.msgopt a{ margin-left:5px; margin-right:5px; color:#999;} 
		.msgopt a:hover{ color:#000;}
		.wtinfo{ margin-left:10px; color:#390;}
    </style>
    <script>
        //----------
        var diag = new ZL_Dialog();
        function ShowDiag(url, title) {
            diag.maxbtn = false;
            diag.title = title;
            diag.url = url;
            diag.reload = true;
            diag.ShowModal();
        }
        function SelectUppic(pval) {
            ShowDiag("/Common/SelFiles.aspx?pval=" + JSON.stringify(pval), "选择在线图片");
        }
        function SelContent() {
            ShowComDiag("/Common/Dialog/SelContent.aspx", "选择内容");
        }
        //获取选择的文章内容
        function GetContent(content,title) {
            UE.getEditor("content_t").setContent(content);
            $("#title_t").val(title);
            CloseComDiag();
        }
        function PageCallBack(action, vals, pval) {
            var url = vals.split('|')[0];
            if (url == "") return;
            url = url;
            $("#picurl_t").val(url);
            UpdateImg();
            diag.CloseModal();
        }
        //----------
        //id使用随机字符串,最大不超过十个(含主体)
        var itemTlp = '<div class="msgitem msgitemf" data-id="@id"><div class="itemleft"><div class="title_div">标题</div>'
                    + '<div class="msgopt"><a href="javascript:;" title="编辑" class="edit_a"><i class="fa fa-pencil"></i></a><a href="javascript:;" title="删除" class="del_a"><i class="fa fa-trash-o"></i></a></div>'
                    + '</div><div class="itemright"><img class="itemimg" src="" onerror="shownopic(this);" /></div></div>';
        var dataArr = [];
        var curid = "default";
        $(function () {
            var media_id = '<%=MediaID %>';
            var action = '<%=Action%>';
            $("#js_add_appmsg").click(function () {
                if (media_id != ""&&action=="") {//判断当前是否为修改模式
                    if (confirm("该操作会导致无法修改图文，确定添加一个新图文吗?"))
                        $("#Update_Btn").hide();
                    else
                        return false;
                } 
                if ($(".msgitemf").length > 9) { alert("最多只允许十条图文"); return; }
                var data = { id: "", title: "", description: "", picurl: "", url: "" ,index:0};
                data.id = GetRanPass(4);
                data.index = GetCur().index + 1;
                var tlp = itemTlp.replace("@id", data.id);
                dataArr.push(data);
                $("#itemlist").append(tlp);
                EventBind();
            });
            InitNews(itemTlp);
            //初始化微信号操作
            var appid = '<%=AppID %>';
            if (appid != "" && appid != "0") {
                $("input[value='" + appid + "']")[0].checked = true;
            }
            InitWxList();
            $(".wxlist li").click(function () {
                var checks = $(this).find("input[name='appids']")[0];
                checks.checked = !checks.checked;
                InitWxList();
            });
        })
        function CheckAll() {
            $(".wxlist li input[name='appids']").each(function (i, v) {
                $(v)[0].checked = true;
            });
            InitWxList();
        }
        //初始化微信列表状态
        function InitWxList() {
            $(".wxlist li").each(function (i, v) {
                if ($(v).find("input[name='appids']")[0].checked)
                    $(v).find('.badge').addClass("active");
                else
                    $(v).find('.badge').removeClass("active");
            });
        }
        //初始化图文数据
        function InitNews(itemTlp) {
            if ($("#News_Hid").val() != "") {
                $("#itemlist").html('');
                var news = JSON.parse($("#News_Hid").val());
                var subnews = JSON.parse(news.content);
                for (var i = 0; i < subnews.length; i++) {
                    var data = { id: GetRanPass(4), title: subnews[i].title, description: subnews[i].content, picurl: subnews[i].thumb_media_id, url: subnews[i].content_source_url,index:i }
                    if (i == 0) {//主图文显示
                        $(".msgthumb .headimg").attr('src', subnews[i].thumb_media_id);
                        $(".msgthumb .title_div").text(data.title);
                        $(".msgthumb").data('id', data.id);
                        curid = data.id;
                    } else {
                        var tlp = itemTlp.replace("@id", data.id);
                        var $tlp = $("<div>" + tlp + "</div>");
                        $tlp.find('.title_div').text(subnews[i].title);
                        $tlp.find('.itemimg').attr('src', subnews[i].thumb_media_id);
                        $("#itemlist").append($tlp.html());
                    }
                    dataArr.push(data);
                }
                ShowByData(GetCur(),true);
                EventBind();
            } else
                dataArr.push({ id: "default", title: "默认封面", description: "", picurl: "", url: "" });
        }


        function UpdateImg() {
            var cur = GetCur();
            var $div = $("div[data-id=" + cur.id + "]");
            $div.find('img').attr('src', $('#picurl_t').val());
        }
        //编辑与移除事件
        function EventBind() {
            $(".edit_a").unbind("click");
            $(".del_a").unbind("click");
            //加载内容进边辑框
            $(".edit_a").click(function () {
                SaveToData();//先保存现有数据
                var item = $(this).closest(".msgitemf");
                curid = item.data("id");
                var data = GetCur();
                ShowByData(data);
            });
            $(".del_a").click(function () {
                var item = $(this).closest(".msgitemf");
                var id = item.data("id");
                item.remove();
                DelByID(id);
            });
           
        }
        function CloseDiag() {
            CloseComDiag();
        }
        function GetByID(id) {
            return ArrCOM.GetByID(dataArr, id);
        }
        function DelByID(id) {
            ArrCOM.RemoveByID(dataArr,id);
        }
        function GetCur() {
            if (curid == "") { return null; }
            return GetByID(curid);
        }
        function ShowByData(data,isfirst) {
            if (!data || data == null || data == undefined) { data = { id: "", title: "", description: "", picurl: "", url: ""}; }
            $("#title_t").val(data.title);
            $("#content_t").val(data.description);
            $("#picurl_t").val(data.picurl);
            $("#url_t").val(data.url);
            if(!isfirst)
            UE.getEditor("content_t").setContent(data.description);
            
        }
        function SaveToData() {
            if (curid == "") { return; }
            var data = GetCur();
            data.title = $("#title_t").val();
            data.description = $("#Video_T").val()+UE.getEditor("content_t").getContent();
            data.picurl = $("#picurl_t").val();
            data.url = $("#url_t").val();
        }
        //实时更新标题
        function UpdateTitle(obj) {
            var data = GetCur();
            var $div = $("div[data-id=" + data.id + "]");
            $div.find(".title_div").text(obj.value);
        }
        //每个文章都必须有图文,标题和内容?
        function CheckVal() {
            for (var i = 0; i < dataArr.length; i++) {
                var data = dataArr[i];
                if (data.title == "" || data.description == "") { alert("标题或内容不能为空"); return false; }
                if (data.picurl == "") { alert("图片(封面)地址不能为空!"); return false;}
            }
            if (!$("[name='appids']:checked")[0]) { alert("请选择公众号!"); return false; }
            $("#status_dl").append("<dd>正在发送中....<dd>");
            return true;
        }
        function PreSave() {
            SaveToData();
            $("#Article_Hid").val(JSON.stringify(dataArr));
            console.log(dataArr);
            return CheckVal();
        }
        function UpdateData() {
            SaveToData();
            var news = JSON.parse($("#News_Hid").val());
            var data = GetCur();
            var subnews = JSON.parse(news.content);
            var subdata = subnews[data.index];
            subdata.title = data.title;
            subdata.thumb_media_id = data.picurl;
            subdata.content_source_url = data.url;
            subdata.content = data.description;
            news.content = JSON.stringify(subnews);
            news.index = data.index;
            $("#News_Hid").val(JSON.stringify(news));
            return CheckVal();
        }
        var videodiag = new ZL_Dialog();
        function SelVedio() {
            videodiag.title = "选择视频";
            videodiag.content = "selvedio_div";
            videodiag.width = "vediodiag";
            videodiag.ShowModal();
        }
        //插入视频标签
        function SetVideoHtml() {
            if (CheckVideoData()) {
                var videourl = 'http://<%=Request.Url.Authority %>' + $("#videosrc_t").val();
                //var udit = UE.getEditor("content_t");
                //var videohtml = "<img width='" + $("#vediowidth_t").val() + "' height='" + $("#vedioheight_t").val() + "' _url='" + videourl +  "' class='edui-upload-video  vjs-default-skin' src='/Plugins/Ueditor/themes/default/images/spacer.gif' style='background:url(/Plugins/Ueditor/themes/default/images/videologo.gif) no-repeat center center; border:1px solid gray;'>"
                //var videohtml = "<p><video src='" + videourl + "' width='" + $("#vediowidth_t").val() + "px' height='" + $("#vedioheight_t").val() + "px' ></video></p>";
                var param = videourl.substr(videourl.lastIndexOf('?') + 1, videourl.length - videourl.lastIndexOf('?'));
                if (videourl.lastIndexOf('?')<0) {
                    alert("不支持该地址格式!")
                    return false;
                }
                var videohtml = "<iframe class='video_iframe' style='z-index:1; ' height='" + $("#vedioheight_t").val() + "' width='" + $("#vediowidth_t").val() + "' frameborder='0' src='http://v.qq.com/iframe/player.html?" + param + "&width=" + $("#vediowidth_t").val() + "&height=" + $("#vedioheight_t").val() + "&auto=0' allowfullscreen=''></iframe>";
                $("#Video_T").val(videohtml);
                //udit.setContent(videohtml, true);
                CloseVideoDiag();
            }
            
        }
        var urldiag = new ZL_Dialog();
        function SetUrl(){
            urldiag.title = "插入链接";
            urldiag.content = "seturl_div";
            urldiag.width = "vediodiag";
            urldiag.ShowModal();
        }
        //插入链接
        function SetUrlHtml() {
            var udit = UE.getEditor("content_t");
            var linkhtml = "<a href='" + $("#linkurl_t").val() + "' target='_blank'>" + $("#linkname_t").val() + "</a>";
            udit.setContent(linkhtml, true);
            urldiag.CloseModal();
        }
        function CloseVideoDiag() {
            videodiag.CloseModal();
        }
        function CheckVideoData() {
            if ($("#Video_DD option:checked").val() == "-1") {
                alert("您还没有上传视频!!");
                return false;
            }
            if (!ZL_Regex.isNum($("#vediowidth_t").val()) || !ZL_Regex.isNum($("#vedioheight_t").val())) {
                alert("视频宽度与高度必须为数字!");
                return false;
            }
            return true;
        }
        var prediag = new ZL_Dialog();
        function PreWxMsg() {
            
            SaveToData();
            prediag.title = "预览图文";
            prediag.url = "PreWxMsg.aspx";
            prediag.reload = true;
            prediag.maxbtn = false;
            prediag.ShowModal();
        }
        function PreCheckSend() {
            ClosePreDiag();
            $("#Save_Btn").click();
        }
        function ClosePreDiag() {
            prediag.CloseModal();
        }

    </script>
</asp:Content>