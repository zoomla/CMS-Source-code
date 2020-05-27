<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Interactive.aspx.cs" Inherits="ZoomLaCMS.Guest.Ask.Interactive" MasterPageFile="~/Guest/Ask/Ask.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>我的提问-<%Call.Label("{$SiteName/}"); %>问答</title>
<script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
<ol class="breadcrumb">  
<li>您的位置：<a href="/">网站首页</a></li>
<li><a href="/Ask">问答中心</a></li>
<li><a href="MyAskList.aspx?QueType=">我的提问</a></li>
<li class="active">我的提问互动详情</li>
</ol>
<div class="row">
<div class="padding10">
<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 padding5">

<div class="ask_detial">
<div class="ask_detial_t"><span>提问问题</span><div class="clearfix"></div></div>
<div class="ask_detial_tc">
<ul class="list-unstyled">
<li><span>问题：</span><asp:Label ID="question" runat="server"></asp:Label> <span class="pull-right">
    <a href="javascript:;" onclick="LikeQue()">
    <span class="fa fa-star"></span> 收藏</a></span></li>
<li><span>提问者：</span><asp:Label  ID="username" runat="server"></asp:Label> <span>提问日期：</span><asp:Label  ID="addtime" runat="server"></asp:Label></li>
</ul>
<div class="row">
<div class="col-lg-3 col-md-3 col-sm-3 col-xs-12">
</div>

<div class="col-lg-9 col-md-9 col-sm-9 col-xs-12">
</div>
</div>
</div>
<div class="ask_detial_t"><span>补充问题</span><div class="clearfix"></div></div>
<div class="ask_detial_tc">
<asp:Label ID="supment" runat="server"></asp:Label>
<div class="asl_detial_tb">
<asp:TextBox ID="Txtsupment" runat="server" TextMode="MultiLine" data-type="normal" Style="min-height: 200px;width:100%;"></asp:TextBox>
<asp:Button ID="Button2" OnClick="Button2_Click" Text="提交" runat="server" CssClass="btn btn-default" />
</div>
</div>

<div class="ask_detial_t"><span>问题回答</span> <i style="color:#999;">[自己发表的问题不能答复]</i><div class="clearfix"></div></div>
<div class="ask_detial_th">
<div style="min-height:50px;">
<asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound" OnItemCommand="Repeater1_ItemCommand">
<ItemTemplate>
<div class="ask_detial_tl">
<div class="ask_detial_tlt"><span class="pull-right"><%# Eval("AddTime")%></span><asp:Label id="Isname" runat="server"><a href="../../ShowList.aspx?id=<%#Eval("UserID")%>" target='_blank'><%#Eval("UserName") %></a></asp:Label></div>
<div class="ask_detial_tlc">
<div class="ask_detial_tlch"><span class="ask_detial_hui">回答：</span><%# Eval("Content")%></div>
<asp:Repeater runat="server" ID="Repeater2"  OnItemDataBound="Repeater2_ItemDataBound">
<ItemTemplate>
<div>
<div><span class="ask_detial_zhui">追问：</span><%#Eval("Content")%></div>
<asp:Repeater runat="server" ID="Repeater3">
<ItemTemplate> 
<div>回答：<%# Eval("Content")%></div>
</ItemTemplate>
</asp:Repeater>  
</div>
</ItemTemplate>
</asp:Repeater>
<div class="ask_detial_tui">
<div><%#getstatus(Eval("Status","{0}"))%></div>
<asp:Panel  ID="ReplyBtn1" CssClass="pull-left" runat="server" ><input type="button" class="btn btn-warning btn-sm" value="继续追问" onclick="supplyment(<%# Eval("ID")%>)" /></asp:Panel>
<asp:LinkButton ID="recommand" runat="server" CommandName="recomand" CssClass="btn btn-success btn-sm pull-right"  CommandArgument='<%#Eval("ID")%>'  OnClientClick="javascript:return confirm('你确认推荐吗?')">推荐为满意答案</asp:LinkButton>
</div><div class="clearfix"></div>
</div>
</div>
</ItemTemplate>
</asp:Repeater>
<div style="display:none;" id="divSupplyment">
<asp:HiddenField ID="Rid" runat="server" Value="" />
<asp:TextBox runat="server" ID="txtSupplyment" CssClass="form-control" TextMode="MultiLine" Rows="6"></asp:TextBox>
<asp:Button runat="server" ID="btnSubmit" Text="提交追问" CssClass="btn btn-default" OnClick="btnSubmit_Click" />
</div>
</div>
</div>
<span class="questions" id="supdiv" runat="server" ></span>
<div id="main" class="rg_inout">     
</div>
</div>
</div>
</div>
</div>
</div>
<div class="hidden">
<div class="topright"> 
<a href="/">返回首页</a>
<a href="javascript:void(0)" onclick="SetHomepage()">设为首页</a>
<a href="javascript:void(0)" onclick="addfavorite()">收藏本站</a></div> 
<span  style="<%=getstyle()%>"> 您好！<a href="/user/" target="_blank"><asp:Label runat="server"  ID="user"></asp:Label></a> 欢迎来<%Call.Label("{$SiteName/}"); %>问答系统！ [<a href="<%=ResolveUrl("~/User/logout.aspx") %>?url=/Guest/Default.aspx">退出登录</a>]</span> 
<span  style="<%=getstyles()%>">[<a  href="/user/Login.aspx?ReturnUrl=/guest/">请登录</a>] [<a  href="/user/register.aspx?ReturnUrl=/guest/">免费注册</a>]</span>
</div>
<div class="hidden">
<div class="h_top">
<div class="logo">
<a href="/guest/Ask/Default.aspx" title="问答系统" target="_top">
<img src="<%Call.Label("{$LogoUrl/}"); %>" alt="<%Call.Label("{$SiteName/}"); %>问答系统" />
</a>
</div>
<div class="userbar">
<div class='hyn'  style="<%=getstyle()%>"> 
<a href="MyAskList.aspx?QueType=">我的提问</a> <a href="MyAnswerlist.aspx">我的回答</a>
</div>
<hr />
</div>
<div class="clr"></div>
</div>
<div class="h_mid">
<div class="h_mid_l"></div>
<div id="tdh" class="h_mid_m"> 
<ul>
<li class="on"><a title="问答首页" href="Default.aspx">问答首页</a> </li>
 <!-- <li><a title="知识专题" href="Topic.aspx">知识专题</a></li>-->
<li><a title="问答之星" href="Star.aspx">问答之星</a></li>
<li><a title="分类大全" href="../Ask/Classification.aspx">分类大全</a></li>
</ul>
</div>
<div class="h_mid_r"></div>
</div>
<div class="clr"></div>
<div class="h_b">
<div class="h_b_l"></div>
<div class="h_b_m">
<div class="question_count">
<span class="count">最佳回答采纳率:</span><span class="adopt" ><%=getAdoption() %></span><br />
<span class="count">已解决问题数:</span><span class="countques" ><% =getSolvedCount() %></span><br />
<span class="count">待解决问题数:</span><span class="countques" ><% =getSolvingCount() %></span>
</div>
<div class="h_b_input">

</div>
<div class="sybz">
<a href="http://help.z01.com/?index/help.html" title="帮助" target="_blank">使用<br />帮助</a>
</div>
<div class="tongji">
<div id="move">
    <span class="count">当前在线:</span><%=getLogined() %><br />
    <span class="count">注册用户:</span><%=getUserCount() %><br />
</div>
</div>
<div class="clr"></div>
</div>
<div class="h_b_r"></div>
</div>
<div class="clr"></div>
</div>
<asp:HiddenField  runat="server" ID="hfsid"/>
<asp:HiddenField  runat="server" ID="hfstatus"/>
        <%=Call.GetUEditor("Txtsupment",4)%>
<script type="text/javascript" src="/JS/Modal/APIResult.js"></script>
<script type="text/javascript">
    $(function(){
        $("#top_nav_ul li[title='在线提问']").addClass("active");
    })
    function show() {
        var div = document.getElementById("show");
        if (div.style.display == "none") {
            div.style.display = "block";
        }
        // else {div.style.display = "none";} 
    }
    function ViewQes()
    {
        if($("#viewQ").css("display")=="none")
            $("#viewQ").css("display","");
        else
            $("#viewQ").css("display","none");
    }
    function supplyment(id) {
        document.getElementById("Rid").value = id; 
        var div = document.getElementById("divSupplyment");
        if (div.style.display == "none") {
            div.style.display = "block";
        }
        else {
            div.style.display = "none";
        }
    }
    function Recommend_click(id) {
        document.getElementById("hfstatus").value = id;
	
    }
    function CheckDirty() {
        var TxtTTitle = document.getElementById("TxtTTitle").value;
        var TxtValidateCode = document.getElementById("TxtValidateCode").value;

        if (value == "" || TxtTTitle == "" || TxtValidateCode == "") {
            if (value == "") {
                var obj = document.getElementById("RequiredFieldValidator1");
                obj.innerHTML = "<font color='red'>内容不能为空！</font>";
            }
            else {
                var obj = document.getElementById("RequiredFieldValidator1");
                obj.innerHTML = "";
            }
            if (TxtTTitle == "") {
                var obj2 = document.getElementById("RequiredFieldValidator2");
                obj2.innerHTML = "<font color='red'>留言标题不能为空！</font>";
            }
            else {
                var obj2 = document.getElementById("RequiredFieldValidator2");
                obj2.innerHTML = "";
            }
            if (TxtValidateCode == "") {
                var obj3 = document.getElementById("sp1");
                obj3.innerHTML = "<font color='red'>验证码不能为空！</font>";
            } else {
                var obj3 = document.getElementById("sp1");
                obj3.innerHTML = "";
            }
            return false;
        }
        else {
            var obj = document.getElementById("RequiredFieldValidator1");
            obj.innerHTML = "";
            var obj2 = document.getElementById("RequiredFieldValidator2");
            obj2.innerHTML = "";
            var obj3 = document.getElementById("sp1");
            obj3.innerHTML = "";
            document.getElementById("EBtnSubmit2").click();
        }
    }
    //收藏问题
    function LikeQue(){
        $.post('/API/mod/collect.ashx',{action:"add",favurl:"<%=Request.RawUrl %>",infoid:"<%=Request["ID"] %>",type:4,title:$("#question").text()},function(data){
            var model=JSON.parse(data);
            if (APIResult.isok(model)) {
                alert("收藏成功!");
            }else{
                alert("收藏失败!原因:"+model.retmsg);
            }
        });
        //location.href="/User/Content/AddToFav.aspx?Url=<%=Request.RawUrl %>&itemid=<%=Request["ID"] %>&type=4&title="+$("#question").text();
    }
</script>
</asp:Content>
