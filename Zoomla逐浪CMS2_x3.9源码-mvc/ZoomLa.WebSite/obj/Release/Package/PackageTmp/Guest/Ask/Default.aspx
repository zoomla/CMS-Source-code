<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Guest.Ask.Default"  MasterPageFile="~/Guest/Ask/Ask.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>问答中心首页</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
<ul class="breadcrumb">
<li>您的位置：<a href="/">网站首页</a></li>
<li>问答中心</li>
<li class="active">首页</a></li>
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
<div class="row">
<div class="padding5">
<div class="col-lg-4 col-md-4 col-sm-4 col-xs-12 padding2 margin_top20">
<div class="ask_class">
<div class="ask_class_t"><a href="/Guest/Ask/Classification.aspx">更多>></a>问题分类</div>
<table class="table table-bordered" id="ask_class_table">
<asp:Repeater ID="Repeater1" runat="server"  OnItemDataBound="Repeater1_ItemDataBound">
<ItemTemplate>
<tr>
<td class="ask_class_tl"><a href="/Guest/Ask/Classification.aspx?GradeID=<%#Eval("GradeID") %>" title="<%#Eval("GradeName") %>"><%#Eval("GradeName") %></a></td>
<td class="ask_class_tr">
<asp:Repeater ID="Repeater2" runat="server">
<ItemTemplate><a href="/Guest/Ask/Classification.aspx?ParentID=<%#Eval("ParentID") %>&GradeID=<%#Eval("GradeID") %>" title="<%#Eval("GradeName") %>"><%#Eval("GradeName") %></a></ItemTemplate>
</asp:Repeater>
</td>
</tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>
</div>
<div class="col-lg-8 col-md-8 col-sm-8 col-xs-12 padding2 margin_top20">
<div class="ask_Resolved">
<div class="ask_Resolved_t"><span><i class="fa fa-heart"></i>我们向您承诺：10分钟内给您满意的答复</span>已解决问题</div>
<div class="ask_Resolved_c">
<div class="row">
<div class="padding10">
<div>
<div class="ask_Resolved_cr">
<ul class="list-unstyled">
<asp:Repeater runat="server" ID="Repeater6"><ItemTemplate>                                            
<li class="col-lg-6 col-md-6 col-sm-6 col-xs-12 padding5">
<strong><%#getanswers(Eval("ID", "{0}"))%>回答</strong>
<a target="_self" href="/Guest/Ask/SearchDetails.aspx?ID=<%#Eval("ID")%>"><%#GetLeftString(Eval("Qcontent").ToString(),12) %></a>
<span>[<a target="_self" href="/Guest/Ask/List.aspx?QueType=<%#Eval("QueType")%>&strwhere="><%#gettype(Eval("QueType","{0}"))%></a>]</span>
</li>
</ItemTemplate></asp:Repeater>
<div class="clearfix"></div>
</ul>
</div>
</div>
</div>
</div>
</div>
</div>
<div class="ask_hotsearch">
<div class="ask_hotsearch_t">热门搜索</div>
<div id="know">
<asp:Repeater runat="server" ID="Repeater11">
<ItemTemplate>
<a class="btn btn-warning btn_hot" target="_self" href="/Guest/Ask/List.aspx?QueType=<%#Eval("QueType")%>&strwhere="><%#gettype(Eval("QueType","{0}"))%><span></span></a>
</ItemTemplate>
</asp:Repeater>
</div>
<div class="ask_hotsearch_c">
<div class="row">
<ul class="list-unstyled">
<li class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
<div class="ask_hotsearch_ct"><span>电脑装机</span></div>
<div class="clearfix"></div>
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出指定问题分类最新问题\" ShowNum=\"4\" TitleNum=\"30\" QueType=\"402\" /}");%>
<div class="clearfix"></div>
</ul>
</li>
<li class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
<div class="ask_hotsearch_ct"><span>体育/运动</span></div>
<div class="clearfix"></div>
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出指定问题分类最新问题\" ShowNum=\"4\" TitleNum=\"30\" QueType=\"392\" /}");%>
<div class="clearfix"></div>
</ul>
</li>
<li class="col-lg-4 col-md-4 col-sm-4 col-xs-12">
<div class="ask_hotsearch_ct"><span>商业/理财</span></div>
<div class="clearfix"></div>
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出指定问题分类最新问题\" ShowNum=\"4\" TitleNum=\"30\" QueType=\"394\" /}");%>
<div class="clearfix"></div>
</ul>
</li>
</ul>
</div>
</div>
</div>
<div class="ask_Wait">
<div class="ask_Wait_t"><a href="/Guest/ask/MoreProblems.aspx?type=1">更多<i class="fa fa-caret-right"></i></a>待解决问题</div>
<div class="ask_wait_c">
<!-- Nav tabs -->
<ul class="nav nav-tabs" role="tablist">
<li class="active"><a href="#ask_wait_c1" role="tab" data-toggle="tab">电脑装机</a></li>
<li><a href="#ask_wait_c2" role="tab" data-toggle="tab">硬件</a></li>
<li><a href="#ask_wait_c3" role="tab" data-toggle="tab">软件</a></li>
<li><a href="#ask_wait_c4" role="tab" data-toggle="tab">反病毒</a></li>
<li><a href="#ask_wait_c5" role="tab" data-toggle="tab">互联网</a></li>
</ul>
<!-- Tab panes -->
<div class="tab-content">
<div class="tab-pane active" id="ask_wait_c1">
<div class="ask_wait_cr">
<div class="row">
<div class="padding10">
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出待解决问题\" TitleNum=\"38\" ShowNum=\"12\" Type=\"402\" /}");%>
<div class="clearfix"></div>
</ul>
</div>
</div>
</div>
</div>
<div class="tab-pane" id="ask_wait_c2">
<div class="ask_wait_cr">
<div class="row">
<div class="padding10">
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出待解决问题\" TitleNum=\"38\" ShowNum=\"6\" Type=\"403\" /}");%>
<div class="clearfix"></div>
</ul>
</div>
</div>
</div>
</div>
<div class="tab-pane" id="ask_wait_c3">
<div class="ask_wait_cr">
<div class="row">
<div class="padding10">
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出待解决问题\" TitleNum=\"38\" ShowNum=\"6\" Type=\"404\" /}");%>
<div class="clearfix"></div>
</ul>
</div>
</div>
</div>
</div>
<div class="tab-pane" id="ask_wait_c4">
<div class="ask_wait_cr">
<div class="row">
<div class="padding10">
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出待解决问题\" TitleNum=\"38\" ShowNum=\"6\" Type=\"405\" /}");%>
<div class="clearfix"></div>
</ul>
</div>
</div>
</div>
</div>
<div class="tab-pane" id="ask_wait_c5">
<div class="ask_wait_cr">
<div class="row">
<div class="padding10">
<ul class="list-unstyled">
<%Call.Label("{ZL.Label id=\"输出待解决问题\" TitleNum=\"38\" ShowNum=\"6\" Type=\"406\" /}");%>
<div class="clearfix"></div>
</ul>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
</div>
</div>


<div class="hidden">
<div>最佳回答采纳率:<%=getAdoption() %> 已解决问题数:<% =getSolvedCount() %> 待解决问题数:<% =getSolvingCount() %></div>
<div>
积分排行榜
<asp:Repeater ID="Repeater10" runat="server"  OnItemDataBound="Repeater10_ItemDataBound">
<ItemTemplate>
<img align="absmiddle" src="/App_Themes/Guest/images/num1.gif" id="ph" runat="server"><a href="/ShowList.aspx?id=<%#Eval("UserID") %>"><%#Eval("UserName") %></a><img align="absmiddle" src="/App_Themes/Guest/images/up.gif"><%#Eval("GuestScore") %>
</ItemTemplate>
</asp:Repeater>
<asp:Repeater ID="Repeater9" runat="server" OnItemDataBound="Repeater9_ItemDataBound">
<ItemTemplate>
<img align="absmiddle" src="/App_Themes/Guest/images/num1.gif" id="Img1" runat="server"> <a href="/ShowList.aspx?id=<%#Eval("UserID") %>"><%#Eval("UserName") %></a></span><img align="absmiddle" src="/App_Themes/Guest/images/up.gif"><%#Eval("GuestScore") %>
</ItemTemplate>
</asp:Repeater>
</div>
<div>
精彩推荐的问题
<asp:Repeater ID="Repeater3" runat="server"><ItemTemplate><a target="_self" href="SearchDetails.aspx?ID=<%#Eval("ID")%>"><%#GetLeftString(Eval("Qcontent").ToString(),25) %></a><span class="lei">[<a target="_self" href="AskList.aspx?QueType=<%#Eval("QueType")%>&strwhere="><%#gettype(Eval("QueType","{0}"))%></a>]<%#getcount(Eval("ID", "{0}"))%>回答</span></ItemTemplate></asp:Repeater>
大家都在问什么
<asp:Repeater ID="Repeater4" runat="server"><ItemTemplate><a target="_self" href="MyAnswer.aspx?ID=<%#Eval("ID")%>"><%#GetLeftString(Eval("Qcontent").ToString(),25) %></a>&nbsp;[<a target="_self" href="List.aspx?QueType=<%#Eval("QueType")%>&strwhere="><%#gettype(Eval("QueType","{0}"))%></a>]<%#getanswers(Eval("ID", "{0}"))%>回答</ItemTemplate></asp:Repeater>                       
悬赏求答案的问题
<asp:Repeater runat="server" ID="Repeater5"><ItemTemplate><img  alt=" " src="/App_Themes/Guest/images/TB2.gif" /><%#Eval("Score")%><a target="_self"  href="SearchDetails.aspx?ID=<%#Eval("ID")%>"><%#GetLeftString(Eval("Qcontent").ToString(),25) %>&nbsp;</a>[<a target="_self" href="AskList.aspx?QueType=<%#Eval("QueType")%>&strwhere="><%#gettype(Eval("QueType","{0}"))%>]</a><%#getanswers(Eval("ID", "{0}"))%>回答</ItemTemplate></asp:Repeater>
<asp:Repeater runat="server" ID="Repeater7" OnItemDataBound="Repeater7_ItemDataBound">
<ItemTemplate> 
<div class="rb_zj_mm">
<div class="rb_zj_mmc">
<div class='zjimg'><img src="/Images/InitHead.gif" width='50px' height='50px' /></div>
<div class='jbzl'>
<a href="/ShowList.aspx?id=<%#Eval("UserID")%>" target='_blank'><%#Eval("UserName")%></a><br/>
回答数:<%#getanswer(Eval("UserID","{0}"))%><br />
提问数:<%#getask(Eval("UserID","{0}"))%>
</div>
精选解答:
<a class="button1" id="fix" runat="server">向TA提问</a>
<asp:Repeater runat="server" ID="Repeater8"><ItemTemplate>                         
<ul>
<li><a href="SearchDetails.aspx?ID=<%#Eval("ID")%>" target='_blank' title=" "><%#Eval("Qcontent")%></a></li>
</ul></ItemTemplate></asp:Repeater>
</div>
</div>
</ItemTemplate>
</asp:Repeater>                 
</div>
</div>

<div class="ask_bottom">
<p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
<p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
</div>
<script>
    $(function () {
        $("#top_nav_ul li[title='问答首页']").addClass("active");
    })
function showtop(type) {
    if (type == 'week') {
        document.getElementById("weektab").className = "on";
        document.getElementById("alltab").className = "";
        document.getElementById("weektop").style.display = "";
        document.getElementById("alltop").style.display = "none";
    } else {
        document.getElementById("weektab").className = "";
        document.getElementById("alltab").className = "on";
        document.getElementById("weektop").style.display = "none";
        document.getElementById("alltop").style.display = "";
    }
}
</script>  
<asp:HiddenField runat="server" ID="hfid" />
    <script type="text/javascript">
        function supplyment() {
            var div = document.getElementById("divSupplyment");
            if (div.style.display == "none") {
                div.style.display = "block";
            }
            else {
                div.style.display = "none";
            }
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
</script>
</asp:Content>
