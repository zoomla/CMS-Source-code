<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Topic.aspx.cs" MasterPageFile="~/Guest/Ask/Ask.master" Inherits="Guest_AskStar" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>问答中心-知识专题</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

<div class="container">
    <div class="header">
        <div class="h_top">
            <div class="logo"><a href="/Ask" title="问答系统" target="_top"><img src="<%Call.Label("{$LogoUrl/}"); %>" alt="<%Call.Label("{$SiteName/}"); %>问答系统" /></a></div>
            <div class="clr"></div>
        </div>
        <div class="h_mid">
            <div class="h_mid_l"></div>
            <div id="tdh" class="h_mid_m">
                <span style="<%=getstyle()%>"><a href="MyAskList.aspx?QueType=">我的提问</a> <a href="MyAnswerlist.aspx">我的回答</a></span>
                <ul>
                    <li><a title="问答首页" href="/Ask">问答首页</a> </li>
                    <li class="on"><a title="知识专题" href="../Ask/Topic.aspx">知识专题</a></li>
                    <li><a title="问答之星" href="../Ask/Star.aspx">问答之星</a></li>
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
                    <span class="count">最佳回答采纳率:</span><span class="adopt"><%=getAdoption() %></span><br />
                    <span class="count">已解决问题数:</span><span class="countques"><% =getSolvedCount() %></span><br />
                    <span class="count">待解决问题数:</span><span class="countques"><% =getSolvingCount() %></span>
                </div>
                <div class="h_b_input">
                </div>
                <div class="sybz">
                    <a href="http://help.z01.com/?index/help.html" title="帮助" target="_blank">使用<br />
                        帮助</a>
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
    <div id="main" class="rg_inout">
        <h1>
            <span style="float: left; width: 397px;">您的位置：<a href="/">网站首页</a>>><a href="/Ask">问答首页</a>>><a href="Classification.aspx">知识专题</a></span>
        </h1>
        <div class="clear"></div>
        <asp:Repeater runat="server" ID="Repeater1" OnItemDataBound="Repeater1_ItemDataBound">
            <ItemTemplate>
                <div class="starR">
                    <div>
                        <div class="starRimg">
                            <img src="../../Images/InitHead.gif" width='100px' height='100px' />
                        </div>
                        <div class="starRC">
                            <ul>
                                <li style="line-height: 40px; float: none;">用户ID:<a href="../../ShowList.aspx?id=<%#Eval("UserID")%>" target='_blank'><%#Eval("UserName")%></a></li>
                                <div class="clear"></div>
                                <li class="sli1">回答数:<%#getanswer(Eval("UserID","{0}"))%></li>
                                <li class="sli1">采纳率:<%#getRat(Eval("UserID","{0}"))%></li>
                                <li class="sli1">提问数:<%#getask(Eval("UserID","{0}"))%></li>
                                <li class="sli1">问答积分:<%#Eval("GuestScore","{0}")%></li>
                            </ul>
                        </div>
                        <div class="starRW">
                            精选解答:  <a href='Add.aspx?fix=<%#Eval("UserID") %>' class="button1">向TA提问</a>
                            <asp:Repeater runat="server" ID="Repeater2">
                                <ItemTemplate>
                                    <br />
                                    <a href="SearchDetails.aspx?ID=<%#Eval("ID")%>" target='_blank' title=" "><%#Eval("Qcontent")%></a>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <div class="clr"></div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
<div class="ask_bottom">
<p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
<p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
</div>
<script type="text/javascript">
    $(function () {
        $("#top_nav_ul li[title='问答首页']").addClass("active");
    })
</script>
</asp:Content>
