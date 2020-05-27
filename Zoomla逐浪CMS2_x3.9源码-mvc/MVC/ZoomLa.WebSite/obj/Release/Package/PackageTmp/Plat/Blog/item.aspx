<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="item.aspx.cs" Inherits="ZoomLaCMS.Plat.Blog.item"  MasterPageFile="~/Plat/Main.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>浏览主题</title>
<script type="text/javascript" src="/JS/JQueryAjax.js"></script>
<link type="text/css" rel="stylesheet" href="/App_Themes/Guest.css" />
<style>
.msg_left_div {padding-left:20px;min-width:100px;float:left;}
.msg_content_div {float:left;padding-top:10px;}
.createDate { color: #9a9a9a; font-size: 12px; }
.content { font-size: 15px; }
.ContentDiv{margin-top:20px;}
.ContentDiv .head_d{float:left;}
.ContentDiv .content_d{margin-left:60px;}
.content_operate { margin-top: 20px; text-align: left; color: #9a9a9a; }
.content_operate div{float: right!important;}
.content_operate div *{ margin-right:10px;}
.comment_div { margin-top: 30px; background-color: #fafafa; float: left; width: 100%; }
.comment_div div { font-weight: 500; float: left; font-size: 20px; width: 50%; color: #9a9a9a; text-align: center; padding-top: 10px; padding-bottom: 10px; }
.comment_div div:hover:not(.comment_selected) { background: rgba(153,153,153,0.1); }
.comment_selected { background: rgba(153,153,153,0.2);}
.comment_content { float: left; margin-top: 20px; border-top: 1px solid #ddd; width: 100%; }
.comment_content .noneContent { color: #9a9a9a; font-size: 15px; margin-top: 10px; display: inline-block;}
.margintop10 { margin-top: 10px; }
.userface_list { text-align: left; width: 100%; padding-left: 10px; }
.likeIcon { width: 20px; height: 20px; border-radius: 50%; cursor: pointer; }
.colorfix { color: #9a9a9a; }
.recommd { border: 1px solid #ccc; }
.likelist_ul li{margin-bottom:20px;}
.likelist_ul .likelist_img{padding:5px;}
.likelist_ul .likelist_content{padding:5px 20px;}
.likelist_ul .likelist_content .likelist_time{color:#999;font-size:12px;margin-top:5px;}
.likelist_ul .likelist_content .likelist_desc{margin-top:20px;color:#888;}
.likelist_ul .likelist_img img{width:70px;height:70px;border-radius:50px; cursor:pointer;}
.likeids_div_ul li img {width:70px;height:70px;}
.popover-content { padding: 0px;}
.uinfodiv {display: block;position: static;width: 272px;}
.uinfo_bottom a{margin-right:5px;}
.send_btn_div{margin-top:5px;border:1px solid #ddd;padding:5px;}
#forward_div{position: fixed; top: 35%;}
.send_reply{margin-top:20px;}
/*图片*/
.thumbnail_img {width:auto;max-width:90%;}
/*投票*/
#vote{border:1px solid #ccc;padding:3px;margin-top:10px;}
.replydiv {overflow:hidden;height:40px;border:1px solid #ddd;}
.replydiv .msg {height:80px;width:100%;padding:5px 8px;border:none;color:#ddd;resize:none;outline:none;}
.replydiv.active {border:1px solid #03a9f4;}
.replydiv.active .msg {color:#666; }

</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
    <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12 col-md-offset-2 col-lg-offset-2 platcontainer">
        <div class="ContentDiv">
                 <div class="head_d"><%=ZoomLa.BLL.B_User_Plat.WordFace(GetUser().UserID,"uword_s uinfo",GetUser().HoneyName,GetUser().UserName) %></div>
                 <div class="content_d">
                    <p><a href="#"><%=GetUser().UserName %></a>:</p>
                    <asp:Literal runat="server" ID="Content_Lit"></asp:Literal>
                    <div class="margin_t5">
                        <asp:Literal runat="server" ID="Literal1"></asp:Literal>
                    </div>
                    <div class="margin_t5">
                        <asp:Literal runat="server" ID="Attach_Lit"></asp:Literal>
                    </div>
                   <div class="content_operate">
                        <span class="createDate">
                            <asp:Label ID="CDate_L" runat="server"></asp:Label>
                        </span>
                        <div>
                            <div title="点赞" style="cursor: pointer;" onclick="PostLike()"><span class="fa fa-thumbs-up"></span><%=GetUserLike() %></div>
                            <span class='fa fa-send' title='转发' id='re_span' onclick="ShowForWard()"></span>
                        </div>
                    </div>
                </div>
        </div>
        <div id="vote" runat="server" visible="false">
			    <div class="paddbottom5"><strong>投票标题：<%=GetMsgMod().Title %></strong></div>
			    <div class="vote_user_div" id="vote_user_div" runat="server">
			      <ul class="vote_list_ul">
				    <%=GetVoteLI() %>
			      </ul>
			      <div>
                      <asp:Button  ID="UserVote_B" runat="server" OnClientClick="return checkvote();" Text="投票" CssClass="btn btn-info btn-sm" OnClick="UserVote_B_Click" />
				    <input type="button" value="查看结果" onclick="ShowMsgDiv('.vote_user_div','.vote_result_div');" class="btn btn-info btn-sm"/>
			      </div>
			    </div>
			    <div class="vote_result_div" id="vote_result_div" runat="server"><!--结果页-->
			      <asp:Repeater runat="server" EnableViewState="false" ID="VoteResultRep">
				    <ItemTemplate>
				      <div><%#Eval("opName") %></div>
				      <div class="progress vote_progress">
					    <div class="progress-bar progress-bar-success" role="progressbar" aria-valuemin="0" aria-valuemax="100" <%#"style='width:"+Eval("Percent")+"%;'" %>></div>
				      </div>
				      <label class="vote_person_num"><%#Eval("count")+"人" %></label>
				    </ItemTemplate>
			      </asp:Repeater>
			      <div class="clearfix"></div>
			      <div id="msg_op_btn_div" runat="server">
				    <input value="返回投票" class="btn btn-info btn-sm" onclick="ShowMsgDiv('.vote_result_div','.vote_user_div');" />
			      </div>
			    </div>
			    <div class="paddin5"><strong><%=GetVoteBottom() %></strong></div>
		    </div>
        <div class="comment_div">
            <div id="comm" onclick="showComms()" class="comment_selected">评论(<asp:Label ID="commCount_L" runat="server" />)</div>
            <div id="like" onclick="showLikes()">赞(<asp:Label ID="likeCount_L" runat="server" />)</div>
        </div>
        <div class="comment_content text-center">
            <div id="comm_div">
                <span class="noneContent" id="Empty_Span_Comm" style="padding: 3px;" visible="false" runat="server">当前没有评论！</span>
                <asp:Repeater runat="server" ID="MsgRepeater" OnItemDataBound="MsgRepeater_ItemDataBound" EnableViewState="false">
                    <ItemTemplate>
                        <div class="msgmain_div margintop10">
                            <div class="msg_left_div" style="min-width: 100px;">
                               <div>
                                    <%#ZoomLa.BLL.B_User_Plat.WordFace(Eval("CUser"),"uword_mid",Eval("HoneyName",""),Eval("UserName","")) %>
                               </div>
                            </div>
                            <div class="msg_content_div text-left" style="margin-left: 5px;">
                                <div>
                                    <a href="javascript:ShowUserDiv('<%#Eval("CUser") %>')"><%#Eval("CUName") %></a>
                                </div>
                                <div class="msg_content_article_div"><%#Eval("MsgContent")%></div>
                                <div><%#GetAttach(Eval("Attach","")) %></div>
                                <span class="content_date r_gray"><%#Convert.ToDateTime(Eval("CDate")).ToString("yyy年MM月dd日 hh:mm") %> </span>
                            </div>
                            <div class="clearfix"></div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id="likes_div" style="display: none;">
                <span class="noneContent" id="Empty_Span_Like" visible="false" runat="server">当前无人点赞！</span>
                <div class="msgmain_div">
                    <div class="userface_list">
                        <ul class="likelist_ul"></ul>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
        </div>
        <div>
            <asp:Literal runat="server" ID="MsgPage_L" EnableViewState="false"></asp:Literal>
        </div>
        <div class="clearfix"></div>
        
        <span class="fa fa-comment colorfix send_reply"></span><strong class="colorfix">发表评论</strong>
        <div class="replydiv" id="replydiv">
            <div id="send_text_div">
                <textarea id="MsgContent_T" runat="server" rows="2" cols="20" class="msg"></textarea>
            </div>
            <div class="send_btn_div">
                <asp:Button ID="sendButton" Text="评论" OnClick="sendButton_Click" CssClass="btn btn-info" runat="server"/>
            </div>
        </div>
    </div>
    <div class="right_InfoDiv" id="ShowUser_Div">
        <iframe style="width: 100%; border: 0px; height: 1000px;" id="ShowUser_if"></iframe>
    </div>
    <div style="display: none;" class="hidden_div">
        <a href="javascript:;" data-toggle="modal" data-target="#forward_div" id="Forward_Btn"></a>
    </div>
    <div class="modal" id="forward_div">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                    <span class="modal-title">内容转发</span>
                </div>
                <div class="modal-body">
                    <div id="forward_his_div"></div>
                    <div id="forward_my_div" style="margin-top: 5px;">
                        <asp:HiddenField runat="server" ID="Forward_ID_Hid" />
                        <asp:TextBox runat="server" ID="ForMsg_Text" TextMode="MultiLine" Style="width: 100%; height: 100px; border-radius: 4px; border: 1px solid #ddd; padding-top: 5px; padding-left: 8px;" placeholder="说说转发理由吧!!"></asp:TextBox>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                    <asp:Button runat="server" ID="Froward_Btn" OnClick="Froward_Btn_Click" Text="转发" class="btn btn-primary" />
                </div>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="ids_Hid" runat="server" />
<asp:HiddenField ID="UserInfo_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link type="text/css" rel="stylesheet" href="/JS/atwho/jquery.atwho.css" />
<script src="/JS/atwho/jquery.atwho.js"></script>
<script src="/JS/ICMS/MainBlog.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script>
    LoadLikes();
    $(function () {
        pop.bindEvent($(".uinfo"));
        TextReply.init();
        $.post("/Plat/Common/Common.ashx", { action: "Plat_CompUser", value: "" }, function (data) { InitAt(data); });
        $("#MsgContent_T").keydown(function () {
            if (event.ctrlKey && event.keyCode == '13') {
                $("#sendButton").trigger("click");
            }
        });
    });
    function ShowMsgDiv(args1, arsg2) {
        $(args1).hide();
        $(arsg2).show();
    }
    function checkvote() {
        var name = "vote_"+<%=Mid %>;
        var val = $("input:radio[name='" + name + "']:checked").val()
        if (!val) { return false; }
        else { return true; }
    }
    function LoadLikes() {
        var tlp = "<li>"
        + "<div class=\"likelist_img pull-left\">"
        + "<img src=\"@salt\" onerror=\"shownopic(this);\" onclick='ShowUser(@CUser)' />"
        + "</div>"
        + "<div class=\"likelist_content pull-left\">"
        + "<div class=\"likelist_desc\"><a href=\"javascript:ShowUser(@CUser);\">@UserName</a> 赞了这条信息</div>"
        + "<div class=\"likelist_time\">@CDate</div>"
        + "</div>"
        + "<div class=\"clearfix\"></div>"
        + "</li>";
        $.post('', { action: "likelist", msgid: "<%=Mid %>" }, function (data) {
            var likelist = JSON.parse(data);
            console.log(data);
            var html="";
            for (var i = 0; i < likelist.length; i++) {
                html += tlp.replace(/@salt/g, likelist[i].salt).replace(/@UserName/g, likelist[i].HoneyName).replace(/@CDate/g, likelist[i].CDate).replace(/@CUser/g, likelist[i].CUser);
            }
            $("#likeCount_L").html(likelist.length);
            $(".likelist_ul").html(html);
        });
    }
    function showComms() {
        $("#comm_div").show();
        $("#likes_div").hide();
        $("#comm").attr("class", "comment_selected")
        $("#like").attr("class", "");

    }
    function showLikes() {
        $("#comm_div").hide();
        $("#likes_div").show();
        $("#comm").attr("class", "")
        $("#like").attr("class", "comment_selected");
    }
    function ShowForWard() {
        $("#Forward_ID_Hid").val(<%:Mid%>);
    var text = " 转发内容：<br />" + $(".ContentDiv").find(".content").text();
    $("#forward_his_div").html(text);
    $("#Forward_Btn").click();
}
function PostLike()//点赞
{
    var id = "<%:Mid%>";
    var tlp = "<li title='{0}' class='likeids_li'><a href='javascript:;'><img class='imgface_mid' src='{1}' /></a></li>", a = "";
    var $main = $(".likeids_div_ul");
    var uname = $("#UserInfo_Hid").val().split(':')[0];
    var likeobj = $main.find("li[title='" + uname + "']");
    if ($("#showlike_span").attr("data-init") == "1") {
        a = "ReLike";
        likeobj.remove();
        $("#showlike_span").hide();
        $("#showlike_span").attr("data-init", "0");
    }
    else {
        a = "AddLike";
        var uface = $("#UserInfo_Hid").val().split(':')[1];
        tlp = tlp.replace("{0}", uname).replace("{1}", uface);
        $main.append(tlp);
        $("#showlike_span").show();
        $("#showlike_span").attr("data-init", "1");
    }
    var num = $main.find(".likeids_li").length;
    if (num>0) {
        $("#Empty_Span_Like").hide();
    } else {
        $("#Empty_Span_Like").show();
    }
    $("#likeCount_L").text(num);
    PostToCS2("Default.aspx", a, id, function () { LoadLikes(); });
}
function ShowUserDiv(uid) {
    $("#ShowUser_Div").show();
    $("#ShowUser_if").attr("src", "/Plat/Common/UserDetail.aspx?ID=" + uid);
}
function ShowUser(uid) {
    var diag = new ZL_Dialog();
    diag.title = "用户信息";
    diag.maxbtn = false;
    diag.url = "../Common/UserDetail.aspx?ID=" + uid;
    diag.ShowModal();
}
    //-----AT
function InitAt(json) {
    json = eval(json);
    $(".atwho").atwho({
        tpl: '<li data-value="${atwho-at}${name}${suffix}"><img src="${imageUrl}" onerror="this.src=\'/Images/userface/noface.png\'" style="width:25px;height:25px;" />&nbsp;${name}</li>',
        at: "@",
        search_key: "name",
        title: "请选择要@的同事名称",
        data: json,
        limit: 8,
        max_len: 20,
        start_with_space: false,
        //data:jsonArr,
        callbacks: {
            remote_filter: function (query, callback)//@之后的语句
            {
                //callback(json);
            }
        }//callback
    });
}
function AddAT(uname, uid) {
    var v = $("#MsgContent_T").val();
    $("#MsgContent_T").val(v + "@" + uname + "[uid:" + uid + "]");
}
//----------------------------
var TextReply = {
    $div: $("#replydiv"),
    init: function () {
        var ref = this;
        $("body").click(function () {
            ref.$div.removeClass("active").animate({ height: "40px" })
        });
        ref.$div.click(function () {
            if (!$(this).hasClass("active")) {
                $(this).animate({ height: '140px' }).addClass("active");
            }
            var e = event || window.event;
            if (e && e.stopPropagation) { e.stopPropagation(); }
            else { e.cancelBubble = true; }
            if(e.target.id=="sendButton"){return true;}
            else{return false;}
        });
    }
};


</script>
</asp:Content>