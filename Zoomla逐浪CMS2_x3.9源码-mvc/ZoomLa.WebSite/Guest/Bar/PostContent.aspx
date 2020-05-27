<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostContent.aspx.cs" Inherits="ZoomLaCMS.Guest.Bar.PostContent" MasterPageFile="~/Guest/Guest.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<link href="/Plugins/Ueditor/third-party/video-js/video-js.min.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="/Plugins/FancyBox/jquery.fancybox.css" />
<link type="text/css" rel="stylesheet" href="/Plugins/FancyBox/jquery.fancybox-thumbs.css" />
<script src="/Plugins/Ueditor/bar.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.js"></script>
<script type="text/javascript" src="/JS/jquery-ui.min.js"></script>
<script type="text/javascript" src="/Plugins/FancyBox/jquery.fancybox.js"></script>
<script type="text/javascript" src="/Plugins/FancyBox/jquery.fancybox-thumbs.js"></script>
<script type="text/javascript" src="/JS/ICMS/MainBlog.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<title><asp:Literal runat="server" ID="Title_L"></asp:Literal></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
    <div class="post_div">
        <div id="contentDiv">
            <div class="post_div_num">
                <div class="title"><asp:Label runat="server" ID="PostName_L" /></div>
                <a href="javascript:;" id="copyhref" class="opition hidden-xs" title="复制链接">[复制链接]</a>
                <div class="pull-right">                    
                    <asp:HyperLink runat="server" ID="ReturnBar_a"></asp:HyperLink>  
                    <div class="share_box"><a href="javascript:;" id="bar_share"><i class="fa fa-share-alt"></i>分享</a>   
                    <div class="bdsharebuttonbox pull-right"><a href="#" class="bds_renren" data-cmd="renren" title="分享到人人网"></a><a href="#" class="bds_qzone" data-cmd="qzone" title="分享到QQ空间"></a><a href="#" class="bds_tqq" data-cmd="tqq" title="分享到腾讯微博"></a><a href="#" class="bds_tsina" data-cmd="tsina" title="分享到新浪微博"></a><a href="#" class="bds_more" data-cmd="more"></a></div> 
                        <script>window._bd_share_config={"common":{"bdSnsKey":{},"bdText":$('#PostName_L').text()+document.URL,"bdUrl":document.URL,"bdMini":"2","bdMiniList":false,"bdPic":"","bdStyle":"2","bdSize":"32"},"share":{}};with(document)0[(getElementsByTagName('head')[0]||body).appendChild(createElement('script')).src='http://bdimg.share.baidu.com/static/api/js/share.js?v=89860593.js?cdnversion='+~(-new Date()/36e5)];</script>   
                    </div>               
                </div>
                <div class="clearboth"></div>
            </div>
            <div class="post_div_title">
                <div class="pull-left reply_title"><span class="card_menNum" runat="server" id="replynum_span1"></span>回复贴，共<span class="card_menNum" runat="server" id="pagenum_span1"></span>页，点击数:<span class="card_menNum" runat="server" id="hitcount_span"></span></div>
                <div class="pull-right">
                    <div class="dropdown" id="EditBtn_DIV" runat="server" visible="false" style="display:inline-block;">
                      <button class="btn btn-xs btn-primary dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">编辑<span class="caret"></span></button>
                      <ul class="dropdown-menu editdrop" aria-labelledby="dropdownMenu1">
                        <li><a href="/EditContent?ID=<%=Pid %>"><span class="fa fa-pencil"></span> 编辑</a></li>
                        <li><a href="javascript:;" onclick="PostDelMsg('<%=Pid %>',1)"><span class="fa fa-trash"></span> 删除</a></li>
                      </ul>
                    </div>
                    <a href="/PItem?id=<%:Pid %>&Filter=OnlyLayer" class="btn btn-xs btn-primary">只看楼主</a>
                    <asp:Literal ID="LikeBtn_Li" runat="server"></asp:Literal>
                    <input type="button" value="回复" class="btn btn-xs btn-primary" onclick="FocusMsg();" />
                </div>
                <div class="clearboth"></div>
            </div>
            <asp:Repeater runat="server" ID="MsgRepeater"  OnItemDataBound="MsgRepeater_ItemDataBound" EnableViewState="false">
                <ItemTemplate>
                    <div class="msgmain_div stickem-container" id="msgmain_div_<%#Eval("ID") %>">

                        <div class="msg_content_div padding0 col-lg-10 col-md-10 col-sm-10 col-xs-12 col-lg-push-2">
                            <div class="post_msg bar_conent_img">
                                <div class="msg_content_article_div thumbnail_div"><%#GetMsg()%></div>
                            </div>
                            <div runat="server" id="plat_div" visible="false" class="reply_exinfo_div">
                                <div><strong><i class="fa fa-share-alt-square"></i> 能力中心</strong></div>
                                <asp:Repeater runat="server" ID="Plat_RPT" EnableViewState="false">
                                    <ItemTemplate>
                                        <div style="padding-top: 5px; border-bottom: 1px dashed #ddd;">
                                            <div style="float: left;">
                                                <img class="replyface" src="<%#Eval("UserFace") %>" onerror="shownoface(this);" />
                                            </div>
                                            <div style="float: left; overflow: hidden; text-overflow: ellipsis; padding-left: 5px;">
                                                <a href="javascript:;"><%#Eval("CUName") %>：</a>
                                            </div>
                                            <div style="float: left;"><%#Eval("MsgContent") %></div>
                                            <div class="clearfix"></div>
                                            <div class="text-right" style="font-size: 12px;"><%#Eval("CDate","{0:yyyy年MM月dd日 HH:mm}") %></div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <div class="post_msg_foot">
                                <div id="like_div_<%#Eval("ID") %>">
                                    <ul class="likeids_div_ul">
                                        <%#GetMedalBtn() %>
                                        <li style="width: auto;"><a href="javascript:;" onclick="OPLike(<%#Eval("ID") %>);" class="likeids_div_a">
                                            <i class="fa fa-thumbs-up"></i><span class="likenum_span likenum_text">(<%#GetLikeNum() %>)</span></a>
                                        </li>
                                        <%#ShowLikeUser() %>
                                    </ul>
                                </div>
                                <div id="medals_div_<%#Eval("ID") %>" class="medal_list">
                                    <%#ShowMedalList() %>
                                </div>
                                  <span class="layer"><%#GetDel() %></span><span><%#Eval("Layer") %>楼</span>
                                  <%#Convert.ToDateTime(Eval("CDate","{0:yyyy年MM月dd日 HH:mm}")) %>
                                  <%#GetReply() %>
                            </div>
                            <div id='reply_<%#Eval("ID") %>' class="post_reply">
                                <div id="reply_content_div_<%#Eval("ID") %>">
                                    <asp:Literal runat="server" ID="ReplyList_L" EnableViewState="false"></asp:Literal>
                                </div>
                                <div class="post_reply_msg" id="reply_main_bottom_<%#Eval("ID") %>">
                                    <div class="text-right">
                                        <input type="button" name="sayword_btn" data-id="<%#Eval("ID") %>" value="我也说一句" class="btn btn-xs btn-primary" onclick="DisReplyul(<%#Eval("ID")%>);" /></div>
                                   <% if(!HidePost){%>
                                     <ul style="display: none;" class="list-unstyled" id="reply_bottom_ul_<%#Eval("ID") %>">
                                        <li>
                                            <textarea id="reply_ul_txt_<%#Eval("ID") %>" class="form-control" placeholder="说一句吧..."></textarea></li>
                                        <li class="reply_send">
                                            <input type="button" value="发表" class="btn btn-xs btn-primary" onclick="AddMessage(<%#Eval("ID")%>);" />
                                        </li>
                                    </ul>
                                    <%} %>
                                </div>
                            </div>
                        </div>

					
					
					
					      <div class="msg_left_div padding0 col-lg-2 col-md-2 col-sm-2 col-xs-12 col-lg-pull-10">
                            <div class="stickem">
                                <a <%#GetHref() %>>
                                    <img class="imgface_mid center-block" src='<%#GetUserFace() %>' data-uid="<%#Eval("CUser") %>" onerror="shownoface(this);" /></a>
                                <div class="postName text-center"><a <%#GetHref() %>><%#GetUName() %></a></div>
                                <div class="groupName"><%#GetDel(1) %></div>
                                <div class="msg_rel_div">
                                    <div class="msg_rel_border"></div>
                                    <div class="msg_rel_main">
                                        <div class="col-md-4 col-xs-5 padding5">
                                            <a <%#GetHref() %>>
                                                <img class="img-responsive center-block" src='<%#GetUserFace() %>' onerror="shownoface(this);" /></a>
                                        </div>
                                        <div class="col-md-8 col-xs-7 padding5">
                                            <ul class="list-unstyled">
                                                <li><span>积分：</span>@UserExp</li>
                                                <li><span>性别：</span>@UserSex</li>
                                                <li>@GroupName</li>
                                                <li><span>生日：@UserBirth</li>
                                                <li><span>注册时间：</span>@RegTime</li>
                                                <div class="clearfix"></div>
                                                <li><a href="javascript:;" class="btn btn-xs btn-primary" onclick="PrivateOpen('@UserID','@UserName');"><i class="fa fa-envelope-o"></i>发送站内信</a></li>
                                                <li><a href="javascript:;" class="btn btn-xs btn-success" onclick="AddFriend('@UserID')"><i class="fa fa-compress"></i>加为好友</a></li>
                                                <li><span class="medalnum_span">加载中...</span></li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>   
                        </div>
						
						
						 <div class="clearfix"></div>
						
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="post_foot_num">
            <asp:Literal runat="server" ID="MsgPage_L" EnableViewState="false"></asp:Literal>
            <span class="card_menNum" runat="server" id="Span1"></span>回复贴，共<span class="card_menNum" runat="server" id="Span2"></span>页
        </div>
        <div runat="server" class="send_div" id="send_div"><!--回复主贴-->
            <div>
                <div class="text-left"><span class="fa fa-comment padding_l_5"></span><strong>发表回复</strong></div>
            </div>
            <div class="clearboth"></div>
            <div class="padding_b_5"><asp:TextBox runat="server" ID="MsgContent_T" TextMode="MultiLine" style="height:200px;" /></div>
            <asp:TextBox ID="VCode" placeholder="验证码" MaxLength="6" runat="server" CssClass="form-control text_x" autocomplete="off"/>
            <img id="VCode_img" class="codeimg" title="点击刷新验证码" />
            <input type="hidden" id="VCode_hid" name="VCode_hid" />
			<asp:Button runat="server" ID="PostMsg_Btn" Text="发表主题" OnClick="PostMsg_Btn_Click" OnClientClick="return CheckData();" CssClass="btn btn-primary" />
        </div>
<div id="noauth_div" runat="server" visible="false">您未登录,没有发贴权限<span><a href="/User/Login.aspx?returnUrl=<%=Request.RawUrl %>">[点此登录]</a></span></div>
        <%=Call.GetUEditor("MsgContent_T",4) %>
       </div>
</div>
<div class="container footer">
Copyright &copy;<script>
var year = ""; mydate = new Date(); myyear = mydate.getYear(); year = (myyear > 200) ? myyear : 1900 + myyear; document.write(year);
</script>
<a href="/" target="_blank"><%:Call.SiteName %></a>版权所有
</div>
<div class="floattool">
    <ul>
        <!--<a href="javascript:;"><span class="txtSpan">回 到 顶 部</span></a>-->
        <li title="回到顶部" onclick="returnTop()" onmouseout="hideTxt(this)" onmouseover="showTxt(this)"><a href="javascript:;"><span class="fa fa-arrow-up"></span></a><span class="txtSpan">回 到 顶 部</span></li>
        <li title="发表帖子" style="position: relative;" onclick="returnDown()" onmouseout="hideTxt(this)" onmouseover="showTxt(this)"><a href="<%:Request.RawUrl %>"><span class="fa fa-edit"></span></a><span class="txtSpan">发 表 帖 子</span></li>
        <li title="贴吧列表" onclick="returnPost()" onmouseout="hideTxt(this)" onmouseover="showTxt(this)"><a href="<%=GetRouteUrl("BarIndex", new { })%>" class="last"><span class="fa fa-th"></span></a><span class="txtSpan">回 到 社 区</span></li>
    </ul>
</div>
<div runat="server" id="barowner_div" visible="false"  class="zIndex9">
    <div id="funcdiv" class="panel panel-primary candrag">         
        <div class="panel-body">
                 <div style="padding: 5px;">
                    <div class="func_item"><asp:LinkButton runat="server" ID="Dels" OnClick="Bar_Btn_Click" CommandArgument="Del"><i class="fa fa-close"><span> 删除</span></i>真的不要了吗</asp:LinkButton></div>
                    <div class="func_item"><asp:LinkButton runat="server" OnClick="Bar_Btn_Click" CommandArgument="Hidden"><i class="fa fa-eye-slash"><span> 屏蔽</span></i> 折戟沉沙</asp:LinkButton></div>
                    <div class="func_item"><asp:LinkButton runat="server" OnClick="Bar_Btn_Click" Text="取消屏蔽" CommandArgument="CancelHidden" ><i class="fa fa-eye"><span> 取消屏蔽</span></i>重见天日</asp:LinkButton></div>
                    <div class="func_item"><asp:LinkButton runat="server" ID="SetJing" OnClick="Bar_Btn_Click" CommandArgument="AddRecom" ><i class="fa fa-eye"><span> 精华</span></i>很好的帖子</asp:LinkButton></div>
                    <div class="func_item"><asp:LinkButton runat="server" ID="CancelJing" OnClick="Bar_Btn_Click" CommandArgument="RemoveRecom" ><i class="fa fa-close"><span> 取消精华</span></i>换其他的看看</asp:LinkButton></div>
                </div>
                <div style="padding: 5px;">
                    <div class="func_item"><asp:LinkButton runat="server" ID="SetAllTop" OnClick="Bar_Btn_Click" CommandArgument="AddAllTop" ><i class="fa fa-arrow-circle-up"><span> 全局置顶</span></i>置为全局帖子</asp:LinkButton></div>
                    <div class="func_item"><asp:LinkButton runat="server" ID="SetTop" OnClick="Bar_Btn_Click" CommandArgument="AddTop" ><i class="fa fa-level-up"><span> 版块置顶</span></i>置为头条帖子</asp:LinkButton></div>
                    <div class="func_item"><asp:LinkButton runat="server" ID="CancelTop" OnClick="Bar_Btn_Click" CommandArgument="RemoveTop" ><i class="fa fa-backward"><span> 取消置顶</span></i>有更好的啦</asp:LinkButton></div>
                    <div class="func_item"><asp:LinkButton runat="server" ID="SetDown" OnClick="Bar_Btn_Click" CommandArgument="AddBottom" ><i class="fa  fa-level-down"><span> 沉底</span></i>放在最底下</asp:LinkButton></div>
                    <div class="func_item"><asp:LinkButton runat="server" ID="CancelDown" OnClick="Bar_Btn_Click" CommandArgument="RemoveBottom" ><i class="fa fa-forward"><span> 取消沉底</span></i>放上来再晒晒</asp:LinkButton></div>
                </div>
        </div>
    </div>
</div>
<div id="privatediv" class="modal site_mail_div">
  <div class="modal-dialog">
	<div class="modal-content">
	  <div class="modal-body padding0">
		<div class="site_mail_title"><span class="pull-left"><i class="fa fa-edit"></i>发送站内信</span><span class="pull-right fa fa-remove grayremind" title="关闭" onclick="PrivateClose();"></span> </div>
		<table id="privatetable">		  
		  <tr>
			<td class="centertd"><span class="grayremind">收信人：  </span></td>
			<td><div class="text_s">
				<input type="text" id="ReceUser" class="form-control text_s"/>
				<input type="hidden" id="ReceUser_Hid"/></div></td>
		  </tr>
		  <tr>
			<td class="centertd"><span class="grayremind">内容：  </span></td>
			<td><asp:TextBox CssClass="site_mail_content" runat="server" ID="MailContent" TextMode="MultiLine"/></td>
		  </tr>
		  <tr class="text-center"> 
			<td  colspan="2"><input type="button" id="prvatesend_btn" value="发送" onclick="PrivateSend();" class="btn btn-primary" /></td>
		  </tr>
		</table>
	  </div>
	</div> 
  </div>
</div>
<%=Call.GetUEditor("MailContent",1) %>
<div id="metals_div">
    <div class="panel panel-default">
      <div class="panel-heading">
        <h3 class="panel-title"><span id="metals_title"></span> <span class="pull-right" style="cursor:pointer;" onclick="$('#metals_div').hide();"><i class="fa fa-remove"></i></span></h3>
      </div>
      <div class="panel-body" id="metals_body">
      </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style>
    .padding0{ padding-left:0; padding-right:0;} 
    #privatetable{ padding:20px; width:90%;} 
    #privatetable td{ margin-bottom:5px; min-width:80px;}
    #metals_div{position:fixed;top:30%;left:40%; width:500px;display:none;z-index:999;}
    .site_mail_title{ padding-left:15px; padding-right:15px; margin-bottom:20px; width:100%; height:50px; line-height:50px; border-bottom:1px solid #eee; box-shadow:0 2px 5px #eee;}
    .site_mail_title .pull-left{ font-size:18px;}
    .site_mail_title .pull-left i{ margin-right:10px; font-size:24px; color:#ff00dc;}
    #edui65{ min-height:150px;}
    .centertd{ text-align:right;}
    .editdrop{min-width:100px;} 
    .stickit{position:fixed;top:50px; z-index:1000;}
    /*点赞*/
    .likeids_div_ul {text-align:left;padding-left:2px;}
    .likeids_div_ul .likeids_div_a {color:#fff;}
    .likeids_div_ul li img{width:25px;height:25px;}
    /*勋章*/

</style>
<script src="/JS/ZL_ValidateCode.js"></script>
<script src="/JS/Controls/B_User.js"></script>
<script type="text/javascript">
    var buser=new B_User();
    $().ready(function(){
        $("input[name=sayword_btn]").each(function(){
            var id=$(this).attr("data-id");
            var len=$("#reply_content_div_"+id).find("div").length;
            if(len==0)//回复区下有内容
            {
                $(this).hide();
                $("#msg_toggle_a_"+id).text("回复");
            }
        });
        $(".header_index_login_run").attr("href", '/User/Login.aspx?returnUrl=<%=Request.RawUrl %>');
        $("#VCode").ValidateCode();
        $.post("/API/UserSinIn.ashx",{action: "sinstatu", localtion: "1"},function(data){
            if (data != "none") {
                $("#sinin_bu").val("已签到").attr("disabled", "disabled");
                $(".sinDays").text("连续" + data + "天");
            }
        },"json");
        $('#copyhref').zclip({
            path: '/JS/ZeroClipboard.swf',
            copy: function () { 
                return $("#PostName_L").text()+"\n"+location.href; },
            afterCopy: function () { alert("复制完成"); }
        });
        $(":checkbox[name=idchk]").click(function () {
            if ($(":checkbox[name=idchk]:checked").length > 0){
                var e = event || window.event;
                $("#barowner_div").css({
                    top: e.clientY-150,
                    left:e.clientX-300,
                })
                $("#barowner_div").show();
            }           
            else $("#barowner_div").hide();
        });
        $(".candrag").draggable
           ({
               addClasses: false,
               axis: false,
               cursor: 'crosshair',
               containment: 'document'
           });
        $(".msg_content_div img").each(function(){
            var src=$(this).attr("src").toLowerCase();
            if(src.indexOf("/attachment/")>0||src.indexOf("/emotion/")>0||src=="")return;
            $(this).wrap($("<a class='athumbnail_img' rel='group' href='"+$(this).attr("src")+"'></a>"));
            $(this).addClass($("thumbnail_img"));
        })
        $(".athumbnail_img").fancybox({
            'titlePosition': 'over',
            helpers: {
                thumbs: {
                    width: 50,
                    height: 50
                }
            }
        });//end;
        
    });
    
    function showTxt(data){
        $(data).children().first().hide();
    }
    function hideTxt(data){
        $(data).children().first().show();
    }
    function returnPost(){
        window.location="<%=GetRouteUrl("BarIndex", new { })%>";
    }
    function returnTop() {
        setTimeout(setScollTop, 1);
    }
    function setScollTop() {
        if ($(window).scrollTop()>1) {
            $(window).scrollTop($(window).scrollTop() - 30);
            setTimeout(setScollTop,1);
        }
    }
    function returnDown(){
        window.location="/EditContent?ID=-1&Cid=<%=Cid %>";
    }
    function DisReply(id) {
        var t=$(event.srcElement);
        if(t.text()=="回复"){t.text("↓收起回复");$("#reply_"+id).show("middle"); $("#reply_bottom_ul_"+id).show();}
        else{t.text("回复");$("#reply_"+id).hide("middle");}
    }
    function DisReplyul(id)
    {
        $("#reply_bottom_ul_"+id).toggle("middle");
    }
    function CheckData()
    {
        var msg=UE.getEditor("MsgContent_T").getContent();
        if(msg.replace(" ","")==""){alert("内容不能为空");return false;}
        if($("#TxtValidateCode").val()==""){alert("验证码不能为空");return false;}
    }
    function FocusMsg()
    {
        $(window).scrollTop($(document).height());
        $("MsgContent_T").focus();
    }
    function SinIn(e) {
        $.post("/API/UserSinIn.ashx?action=sinin",{localtion:"1"},function(data){
            if (data!=""&&d!="-1") {
                $(e).val("已签到");
                $(e).attr("disabled", "disabled");
                $("#sinDays").text("连续" + data + "天");
            }else{ alert("您尚未登录！！");}    
        });   
    }
	function GetMsgMainID(id)
	{
		return "msgmain_div_"+id;
	}
	function ShowDiv(id) {
		if ($("#" + id).is(":visible"))
			$("#" + id).hide();
		else
		{
			switch(id)
			{
				case "ImgFace_Div":
					if($("#ImgFace_if").attr("src")==""){
						$("#ImgFace_if").attr("src","/Plugins/Ueditor/dialogs/emotion/ImgFace.html");
					}
					break;
			}
			$("#" + id).show();
		}
	}
	function ReplyHer(mid,uname)
	{
		$("#reply_bottom_ul_"+mid).show();
		$("#reply_ul_txt_"+mid).val("回复 "+uname+" :").focus();
	}
    //-------
	function GetSource() { return "bar"; }
	function LoadReply(pid,pageSize,pageIndex)
	{
		$("#reply_content_div_" + pid).load("/Guest/Bar/ReplyList.aspx?code="+Math.random()+"&pid=" + pid + "&PageSize=" + pageSize + "&PageIndex=" + pageIndex + " start");
	}
	var pageSize =<%=replyPageSize%>
	//----------------AJAX区
	function PostDelMsg(msgid,reload)
	{
		if(confirm("确定要删除该条信息吗!!"))
		{
			$("#reply_div_"+msgid).remove();
			PostToCS("DeleteMsg",msgid,function(data){if(data=="-1")alert("删除失败,当前用户无权限!");});
			if(reload==1)  location="/PClass?id=<%:Cid%>";
			else if(reload==2)
			{
				location=location;
			}
		}
	}
    function ColledBtn(obj,id){
        var action="AddColl";
        if($(obj).hasClass("collbtn"))
        {
            action="ReColl";
        }
        PostToCS(action,id,function(){
            switch(action)
            {
                case "AddColl":
                    $(obj).addClass("collbtn");
                    $(obj).val("取消收藏");
                    break;
                case "ReColl":
                    $(obj).removeClass("collbtn");
                    $(obj).val("收藏");
                    break;
            }
        });
    }
	function AddReply(id) {//回复主信息
		var msg = $("#MsgContent_" + id).val();
		if (msg == "") { alert('信息不能为空!!'); return; }
		var value = id + ":::" + msg;
		PostToCS("AddReply", value, function () { LoadReply(id, pageSize, 1); });
	}
	function AddMessage(pid) {//对回复者回复
		var msg = $("#reply_ul_txt_"+pid).val();
		if (msg == "") { alert('信息不能为空!!'); return; }
		if(pid==""){alert('未指定回复对象!!'); return;}
		var value = pid + ":::" + msg;
		PostToCS("AddReply2", value, function () { LoadReply(pid, pageSize, 1); });
		$("#reply_bottom_ul_"+pid).hide();
		$("#reply_ul_txt_"+pid).val("");

	}
	var isbarowner='<%=IsBarOwner %>';
    //添加勋章
	function AddMedal(obj,id){
	    if($(obj).hasClass("medal_btn_dis")){return;}
	    if(!confirm("是否确定颁发勋章"+(isbarowner=='True'?"":"(将扣除积分1点)")+"?")){return;}
	    PostToCS("AddMedal",id,function(data){
	        if(data==-2){alert("您已经颁发过此贴了!");return;}
	        if(data==-3){alert("不能给自己颁发勋章!");return;}
	        if(data==-1){alert("用户积分不足!");return;}
	        $(obj).addClass("medal_btn_dis");
	        $("#medals_div_"+id).append(GetMedalIcon(data));
	        $("#like_div_"+id).find('.medalnum_btn').text("("+$("#medals_div_"+id).children().length+")");
	    });
	}
    //获得勋章图标
	function GetMedalIcon(type){
	    var htmltlp="<span class='fa-stack fa-lg'><i class='fa fa-sun-o fa-stack-2x'></i><i class='fa fa-stack-1x '>@type</i></span>";
	    var iconstr="";
	    switch(type+"")
	    {
	        case "1":
	            iconstr="网";
	            break;
	        case "2":
	            iconstr="版";
	            break;
	        case "3":
	            iconstr="系";
	            break;
	    }
	    return htmltlp.replace(/@type/g,iconstr);
	}
	function PostToCS(a, v, CallBack) {
		PostToCS2("/Guest/Bar/PostContent.aspx?Pid=<%:Pid%>",a,v,CallBack);
	}//Post To CS end;
	function PostToCS2(u,a,v,CallBack)
	{
		$.ajax({
			type: "Post",
			url:u,
			data: { action: a, value: v },
			success: function (data) {
				CallBack(data);
			},
			error: function (data) {
			}
		});
	}
	function AddFriend(fid) {
	    $(".msg_rel_div").hide();
	    buser.AddFriend(fid,function(data){AddFriendCall(data);});
	}
	function CloseUserInfo(){
	    $(".msg_rel_div").hide();
	}
	//添加好友成功UI
	function AddFriendCall(data){
	    var mesg="<div class=\'addinfo\'></div>"
	    $("body").append(mesg);
	    $(".addinfo").css({
	        display:'none',
	        position:'fixed',
	        top:'50%',
	        left:'50%',
	        marginLeft:'-75px',
	        marginTop:'-20px',
	        width:'150px',
	        height:'40px',
	        lineHeight:'40px',
	        background:'rgba(0,0,0,0.6)',
	        boxShadow:'0 0 10px 1px rgba(0,0,0,0.3)',
	        color:'#fff',
	        borderRadius:'20px',
	        textAlign:'center', 
	    }) 
	    if ( data == "-1") {  
	        $(".addinfo").html("添加好友成功");	           
	    }
	    else{
	        $(".addinfo").html("你们已经是好友啦");
	    }
	    $(".addinfo").fadeIn(1000);
	    setTimeout(function(){$('.addinfo').fadeOut(1000);},2000);  	
	}
	
	var uinfointer;
    $(".imgface_mid").hover(function(){
        obj=this;
        uinfointer= setTimeout(function(){UInfoShow(obj)},300);
    },function(){
        clearTimeout(uinfointer);
    });
    var uinfoArr=[];
    function UInfoShow(obj){
        var uid=$(obj).data("uid");
        $uinfodiv=$(obj).parent().parent().find(".msg_rel_div");
        $(".msg_rel_div").hide();
        $uinfodiv.show();
        buser.GetBarUInfo(uid,function (data) {
            UInfoCallBack(data,$uinfodiv);
        });
     }    
    function UInfoCallBack(data,$uinfodiv)
    {
        UInfoTlpDeal($uinfodiv,data);
        uinfoArr.push(data);
    }
    function UInfoTlpDeal($uinfodiv,data){   
        var tlp = $uinfodiv.html().replace("/Images/ajax-loader.gif", data.UserFace).replace(/@UserExp/g, data.UserExp).replace("@Count", data.Count).replace(/@UserSex/g, data.UserSex).replace("@GroupName", data.GroupName).replace("@UserBirth", data.UserBirth).replace("@RegTime", data.RegTime).replace("@UserID", data.UserID).replace("@UserName", data.UserName);
        $uinfodiv.html(tlp);
        $uinfodiv.find(".loading").hide();
        $uinfodiv.find(".loadok").show();
        if(data.UserID<=0){$uinfodiv.find(".medalnum_span").html("<span style='color:gray'>匿名用户</span>");return;}
        //加载勋章
        PostToCS("GetMedalNum",data.UserID,function(result){
            if(data!="-1"){
                $uinfodiv.find(".medalnum_span").html("<a href='javascript:;' onclick=\"ShowUserMetals("+data.UserID+",'"+data.UserName+"')\"><span class='fa fa-sun-o'></span>勋章总数:["+result+"]</a>");
            }
        })
    }
    //显示用户勋章
    function ShowUserMetals(uid,uname){
        PostToCS("GetUserMedal",uid,function(data){
            var medalarr=JSON.parse(data);
            $("#metals_title").html(uname+"的勋章");
            $("#metals_body").html('');
            for (var i = 0; i < medalarr.length; i++) {
                $("#metals_body").append(GetMedalIcon(medalarr[i].medalid));
            }
            $("#metals_div").show();
        });
    }
    function GetSource() { return "<%=Request.RawUrl%>"; }
    //分享设置
    $().ready(function(){
        $(".share_box").mouseover(function(e) {
            $(this).find(".bdsharebuttonbox").show();
        }).mouseout(function(e) {
            $(this).find(".bdsharebuttonbox").hide();
        });;
    })    
    var timer;
    $(".msg_left_div ").mouseenter(function(){
        clearTimeout(timer);    
    }).mouseleave(function(){
        timer=setTimeout(function(){
            CloseUserInfo();
        },1000);
    });
    //用于收藏或点赞,用户头像等信息后台获取并输出
    var LikeComp = {};
    LikeComp.config = <%=GetCurUser()%>;
    LikeComp.liTlp = '<li title="@UserName" class="likeids_li" data-uid="@UserID"><a href="javascript:;"><img src="@UserFace" onerror=\"this.src=\'/Images/UserFace/noface.png\'"></a></li>';
    //贴子ID
    LikeComp.AddLike = function (id) {
        var action = "AddLike";
        LikeComp.UpdateUI(action,id);
        $.post("/Guest/Bar/PostContent.aspx", { "action": action, "value": id }, function (data) {});
    }
    LikeComp.ReLike = function (id) {
        var action = "ReLike";
        LikeComp.UpdateUI(action,id);
        $.post("/Guest/Bar/PostContent.aspx", { "action": action, "value": id }, function (data) {});
    }
    //更新UI与数量
    LikeComp.UpdateUI=function(action,id){
        var config=LikeComp.config;
        var $main=$("#like_div_"+id);
        switch(action)
        {
            case "AddLike":
                var html = LikeComp.liTlp.replace("@UserName", config.UserName).replace("@UserFace", config.UserFace).replace("@UserID", config.UserID);
                $main.find(".likeids_div_ul").append(html);
                break;
            case "ReLike":
                $main.find(".likeids_div_ul li[data-uid="+LikeComp.config.UserID+"]").remove();
                break;
        }
        $main.find(".likenum_text").text($main.find(".likeids_div_ul li[data-uid]").length);
    }
    function OPLike(id)
    {
        var filter="#like_div_"+id+" .likeids_div_ul li[data-uid="+LikeComp.config.UserID+"]";
        var config=LikeComp.config;
        if ($(filter).length > 0) {
            LikeComp.ReLike(id);
        }
        else {
            LikeComp.AddLike(id);
        }
    }
</script> 
<script type="text/javascript" src="/JS/jquery.zclip.min.js"></script>
</asp:Content>