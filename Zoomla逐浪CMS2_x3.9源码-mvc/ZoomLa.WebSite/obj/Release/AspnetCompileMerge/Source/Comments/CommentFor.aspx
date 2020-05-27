<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentFor.aspx.cs" Inherits="ZoomLaCMS.Comments.CommentFor" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>发表评论</title>
    <script src="/JS/Controls/Emotion.js"></script>
    <script>
        var InsertSmiley = function (data) {
            var $text = $(emotion.param);
            $text.val($text.val() + data.title);
        }
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="commentfor_body">
    <div style="position: relative;">
    <div id="Comment">
        <div id="Comment_List">
            <div class="alert alert-info" id="nocoment" visible="false" runat="server" role="alert">
                信息所属栏目评论功能已关闭
            </div>
            <div class="alert alert-info" id="nologin" visible="false" runat="server" role="alert">
                您必须登录才能评论，请点击<a href="javascript:;" onclick="LoginUser()">登录</a>
            </div>
            <div id="comentyes" visible="false" runat="server">
                <div class="panel panel-default">
                    <div class="panel-heading"><div class="gray_c">评论还可以输入<span id="maxcount_span">320</span>字</div>
                    </div>
                    <div class="CommentPK" style="display: none;">
                        <asp:RadioButtonList ID="RBLPK" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="1" Selected="True">我支持</asp:ListItem>
                            <asp:ListItem Value="0">我反对</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                    <textarea id="content_sender" onkeyup="upMaxLength()" runat="server" style="height: 120px;border:none;" maxlength="320" placeholder="我也来说两句" class="form-control msgtext"></textarea>
                    <div class="panel-footer">
                        <div class="hidden">
                            <span>评 分：</span><span>
                                <asp:DropDownList CssClass="form-control" Width="100" ID="DDLScore" runat="server">
                                    <asp:ListItem Value="1">1分</asp:ListItem>
                                    <asp:ListItem Value="2">2分</asp:ListItem>
                                    <asp:ListItem Value="3" Selected="True">3分</asp:ListItem>
                                    <asp:ListItem Value="4">4分</asp:ListItem>
                                    <asp:ListItem Value="5">5分</asp:ListItem>
                                </asp:DropDownList>
                            </span>
                        </div>
                        <asp:TextBox ID="VCodesender" placeholder="验证码" MaxLength="6" Width="100" runat="server" class="form-control" />
                        <img id="VCodesender_img" runat="server" title="点击刷新验证码" class="code" style="height: 32px;" />
                        <input type="hidden" id="VCodesender_hid" name="VCodesender_hid" />
                        <div class="pull-right text-right" style="width: 40%;">
                            <a href="javascript:;" title="选择表情" onclick="emotion.open(this,'#content_sender');" style="margin-right:10px;position:relative;bottom:-5px;"><i class="fa fa-smile-o" style="font-size: 30px"></i></a>
                            <asp:ValidationSummary ID="ValidationSummary1" ShowMessageBox="true" ShowSummary="false" runat="server" />
                            <span id="reinfo_sender"></span>
                            <button type="button" id="comment_sender" class="btn btn-primary" onclick="replyContent('sender')">发表评论</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="CommentInput" runat="server" style="margin-bottom: 10px; position: relative;">
            <div class="alert alert-warning" role="alert">本评论只代表网友个人观点，不代表本站观点</div>
            <div class="panel panel-default comment_list">
                <div class="panel-heading">
                    <ul>
                        <li>评论总数：<asp:Label ID="Label1" runat="server" Text="0"></asp:Label></li>
                    </ul>
                </div>
                <ul class="list-group" style="width: 100%; padding: 0;">
                    <ZL:ExRepeater ID="RPT" runat="server" PageSize="10" PagePre="<li style='width:100%' class='text-center margin_t10'>" PageEnd="</li>">
                        <ItemTemplate>
                            <li class="list-group-item" style="width: 100%;">
                                <h5 class="list-group-item-heading"><span class="padding_r2px"><%#Eval("Layer") %>楼</span>
                                    <span class="gray_9">评论人：<%# GetUserName(Eval("UserID","{0}")) %></span>  <span class="text-right comm-date">发表时间：<%# Eval("CommentTime") %></span></h5>
                                <p class="list-group-item-text">
                                    <asp:Literal ID="Commment_Lit" runat="server" EnableViewState="false"></asp:Literal>
                                   <script>
                                       document.write(emotion.strToEmotion("<%#GetContent() %>"));
                                   </script> 
                                </p>
                                <div class="text-right comment_func">
                                    <span class="comm_btns support" data-id="<%#Eval("CommentID") %>" data-flag="1" onclick="Support(this)">支持(<span class="count"><%#Eval("AgreeCount") %></span>)</span>
                                    <span class="comm_btns support" data-id="<%#Eval("CommentID") %>" data-flag="-1" onclick="Support(this)">反对(<span class="count"><%#Eval("DontCount") %></span>)</span>
                                    <%#GetReport(Eval("ReprotIDS").ToString(),Eval("CommentID").ToString()) %>
                                    <span class="comm_btns" onclick='showHuiFu(this,<%#Eval("commentID") %>)'>回复</span>
                                </div>
                            </li>
                        </ItemTemplate>
                        <FooterTemplate></FooterTemplate>
                    </ZL:ExRepeater>
                </ul>
            </div>
            <div class="Assis_Div">
                <a class="info" id="setup" title="顶" href="javascript:;" onclick="ContentAssist(1)">
                    <i class="fa fa-thumbs-o-up"></i>
                    <span class="gray_9 info_count" id="asscount" runat="server">0</span>
                </a>
                <a class="info" id="setdown" title="踩" href="javascript:;" onclick="ContentAssist(-1)">
                    <i class="fa fa-thumbs-o-down"></i>
                    <span class="gray_9 info_count">
                        <asp:Label ID="CommCount_L" runat="server"></asp:Label></span>
                </a>
            </div>
        </div>
    </div>
</div>
<div id="boxCovers" style="display: none; position: absolute; top: 0px; left: 0px; z-index: 98; background: #b2b2b2;"></div>
<div id="popupLogin" style="width: 300px; height: 200px; background: #fff; position: absolute; top: 250px; left: 350px; z-index: 99; border: #33ccff solid 3px; display: none;">
    <div id="LoginHead" class="ptitle"><span class="pspanl">用户登录</span><span id="lclose" class="pspanr">关闭</span></div>
</div>
<div id="toComment_box" class="hidden">
    <div id="toComment">
        <textarea id="content_reply" name="txtContent" class="form-control msgtext" placeholder="请输入评论内容" rows="4"></textarea>
        <span class="pull-left reply_vaild">
            <asp:TextBox ID="VCodereply" MaxLength="6" runat="server" class="td_m" placeholder="验证码" Style="margin-top: 3px;" />
            <img id="VCodereply_img" runat="server" title="点击刷新验证码" class="code" />
            <input type="hidden" id="VCodereply_hid" name="VCodereply_hid" /></span>
        <input type="hidden" name="txtpid" id="txtpid" />
        <span><span id="reinfo_reply"></span>
             <a href="javascript:;" title="选择表情" onclick="emotion.open(this,'#content_reply');" style="margin-right:10px;position:relative;bottom:-5px;"><i class="fa fa-smile-o" style="font-size: 20px"></i></a>
            <button class="btn btn-sm btn-default" type="button" id="comment_reply" onclick="replyContent('reply')">发布</button></span>
        <div class="clearfix"></div>
    </div>
</div>
<asp:Button ID="Reply_Btn" runat="server" Style="display: none;" OnClick="Reply_B_Click" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
.emotion_ifr{position:absolute;overflow:hidden;margin-top:30px;left:30%;width:700px;height:380px;border:none;z-index:1000;}
</style>
<link rel="stylesheet" href="/App_Themes/V3.css" />
<script src="/JS/ICMS/alt.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ZL_ValidateCode.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script>
    var ismove = false;
    $(function () {
        $("#VCodesender").ValidateCode();
        $("#VCodesender").keydown(function(){
            if(event.keyCode==13)
            {
                $("#comment_sender").click();
                return false;
            }
        });
        if(parent&&parent.initCommInfo){ parent.initCommInfo($(".Assis_Div").html());}
    })
    function LoginUser() {
        var url = parent.location.href.replace(location.protocol + "//", "").replace(location.host, "");
        parent.location.href = "/user/login.aspx?ReturnUrl=" + url;
    }
    function commentSubmit() {
        $("#Reply_Btn").click();
    }
    //支持反对操作
    function Support(obj) {
        $.ajax({
            type: "POST",
            data: { action: 'support', id: $(obj).data('id'), flag: $(obj).data('flag'), gid: '<%=ItemID %>' },
            success: function (data) {
                if (data == "-1") { alert("不能重复支持或反对!"); return; }
                $(obj).parent().find('.support').addClass('gray_9');
                var val = parseInt($(obj).find(".count").text()) + 1;
                if (isNaN(val)) { val = 1; }
                $(obj).find(".count").text(val);
                $(obj).parent().find('.support').removeAttr("onclick");
            }
        });
    }
    //回复操作
    function replyContent(action) {
        $("#comment_" + action).attr("disabled", "disabled");
        $("#reinfo_" + action).html("<span class='fa fa-refresh fa-spin'></span>");
        var postData = { "action": action, id: $("#txtpid").val(), content: $("#content_" + action).val(), VCode_hid: $("#VCode" + action + "_hid").val(), VCode: $("#VCode" + action).val() };
        $.post("", postData, function (data) {
            switch (data) {
                case "2":
                    location = location;
                    break;
                case "1":
                    $("#reinfo_" + action).html("<span class='comm_audited'><span class='fa fa-check-circle'></span> 感谢回复,编辑正在审核中</span>");
                    $("#VCode" + action).ValidateCode();
                    $("#VCode" + action).removeClass('codeok');
                    $("#VCode" + action).val('');
                    $("#content_" + action).val('');
                    location = location;
                    break;
                case "-1":
                    $("#reinfo_" + action).html("<span style='color:red;'><span class='fa fa-exclamation-circle'></span> 验证码不正确!</span>");
                    $("#comment_" + action).removeAttr('disabled');
                    break;
                case "-2":
                    $("#reinfo_" + action).html("未登录!点击[<a href='javascript:;' onclick='parent.checkLogin()'>登录</a>]!");
                    break;
                case "-3":
                    $("#reinfo_" + action).html("内容不能为空!");
                    $("#comment_" + action).removeAttr('disabled');
                    break;
                case "-5":
                    $("#reinfo_" + action).html("您已经评论过了!");
                    break;
                default:
                    $("#reinfo_" + action).html("<span style='color:red;'>该内容不允许发表评论!</span>");
                    $("#comment_" + action).removeAttr('disabled');
                    break;
            }
        })
    }
    //举报操作
    function Report(obj) {
        $.post("", {action:"report", cid: $(obj).data('cid') }, function (data) {
            if (data == "1") {
                $(obj).removeAttr('onclick');
                $(obj).addClass('gray_9');
                $(obj).text('已举报');
            }
        })
    }
    //顶与踩
    function ContentAssist(flag) {
        var idflag=flag==1?"setup":"setdown";
        $.ajax({
            type:"POST",
            data: { action: 'assist', gid: "<%:ItemID %>", value: flag },
            success: function (data) {
                $("#"+idflag).removeAttr("onclick");
                $("#"+idflag).find(".hand").addClass("gray_c");
                if(data=="1"){
                    $(".info").css({ color:'#666',});
                    var val=parseInt($("#"+idflag).find(".info_count").text())+1;
                    if(isNaN(val)){val=1;}
                    $("#"+idflag).find(".info_count").text(val);
                }
                    
                parent.initCommInfo($(".Assis_Div").html());
            }
        });
    }
    //回复框
    function showHuiFu(obj, pid) {
        $("#comment_reply").removeAttr("disabled");
        $("#reinfo_reply").html("");
        $("#VCodereply").ValidateCode();
        $("#txtpid").val(pid);
        $("#toComment").appendTo($(obj).parent());
    }   
    function LoginSuccess() {
        history.go(0);
    }
    //统计评论数量
    function upMaxLength(){
        $("#maxcount_span").text(320-$("#content_sender").val().length);
    }
</script>
</asp:Content>