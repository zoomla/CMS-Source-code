<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Success.aspx.cs" MasterPageFile="~/Guest/Ask/Ask.master" Inherits="Guest_AskSuccess" EnableViewStateMac="false" %> 
<asp:Content runat="server" ContentPlaceHolderID="head">
<title> 成功提示-<%Call.Label("{$SiteName/}"); %>问答</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

<div class="container">
<ul class="breadcrumb">
<li>您的位置：<a href="/">网站首页</a></li>
<li><a href="/Ask">问答中心</a></li>
<li class="active">提问成功</li>
</ul> 
</div>

<div class="container">
    <ul class="list-inline">
    <li class="on"><a title="问答首页" href="#" class="btn btn-primary btn_guest">问答首页</a> </li>
    <li><a title="问答之星" href="Guest/Ask/Star.aspx" class="btn btn-primary btn_guest">问答之星</a></li>
    <li><a title="分类大全" href="Guest/Ask/Classification.aspx" class="btn btn-primary btn_guest">分类大全</a></li>
    <li><a href="guest/ask/List.aspx?strWhere=<%=Server.HtmlEncode(Request["strWhere"]) %>&QueType=<%=Server.HtmlEncode(Request["QueType"]) %>" class="btn btn-primary btn_guest">待完善问题</a></li>
    <!-- <li><a title="知识专题" href="Topic.aspx class="btn btn-primary btn_guest"">知识专题</a></li>-->
    </ul>
</div>

<div class="container">
    <div class="header">
        <div class="h_top">
            <div class="clr"></div>
        </div>
        <div class="h_mid">
            <div class="h_mid_l"></div>
            <div id="tdh" class="h_mid_m">
                <span class="pull-left" style="<%=getstyle()%>"><a href="MyAskList.aspx?QueType=">我的提问</a> <a href="MyAnswerlist.aspx">我的回答</a></span>

            </div>
            <div class="h_mid_r"></div>
        </div>
        <div class="h_b">
            <div class="h_b_l"></div>
            <div class="h_b_m">
                <div class="question_count">
                    <span class="count">最佳回答采纳率:</span><span class="adopt"><%=getAdoption() %></span><br />
                    <span class="count">已解决问题数:</span><span class="countques"><% =getSolvedCount() %></span><br />
                    <span class="count">待解决问题数:</span><span class="countques"><% =getSolvingCount() %></span>
                </div>
                <div class="h_b_input">
                </div>
                <div class="sybz">
                    <a href="http://help.z01.com/?index/help.html" title="帮助" target="_blank">使用帮助</a>
                </div>
                
                <div class="clr"></div>
            </div>
            <div class="h_b_r"></div>
        </div>
        <div class="clr"></div>
    </div>

    <div class="rg_inout msg" id="main" runat="server">
        <h2 class="f-yahei">您的问题我们已经收到！请耐心等待网友的回答：）</h2>
        <div class="submit-fallback">
            <h3 class="f-yahei">知恩图报，别忘了及时推荐为满意回答哦~</h3>
            <a href="list.aspx" title="查看问题">好，一定</a>
        </div>
    </div>
    <div id="NoUser_div" runat="server" visible="false">
        <h3 class="f-yahei">提示：当前为[匿名提问模式]，系统总共注册会员数<%=getUserCount() %>人。</h3>
        <div class="submit-fallback">
            <h3 class="f-yahei">知恩图报，别忘了及时推荐为满意回答哦~</h3>
            <a href="list.aspx" title="查看问题">好，一定</a>
        </div>
    </div>
</div>
<div class="ask_bottom">
<p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
<p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
</div>
<script type="text/javascript">
    function show() {
        var div = document.getElementById("show");
        if (div.style.display == "none") {
            div.style.display = "block";
        }
    }
</script>
</asp:Content>

