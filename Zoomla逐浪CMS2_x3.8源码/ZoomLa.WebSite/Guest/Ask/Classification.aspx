<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Classification.aspx.cs" ClientIDMode="Static" Inherits="Guest_AddClassification" MasterPageFile="~/Guest/Ask/Ask.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>问答中心-<%Call.Label("{$SiteName/}"); %></title>
<style>.ask_class_con i{font-size:1.2em;color:#F00;}</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
<ol class="breadcrumb">  
<li>您的位置：<a href="/">网站首页</a></li>
<li><a href="Default.aspx">问答中心</a></li>
<li class="active">分类大全</li>
</ol>
</div>

<div class="container">
    <ul class="list-inline">
    <li class="on"><a title="问答首页" href="#" class="btn btn-primary btn_guest">问答首页</a> </li>
    <li><a title="问答之星" href="Star.aspx" class="btn btn-primary btn_guest">问答之星</a></li>
    <li><a title="分类大全" href="Classification.aspx" class="btn btn-primary btn_guest">分类大全</a></li>
    <li><a href="List.aspx?strWhere=<%=Server.HtmlEncode(Request["strWhere"]) %>&QueType=<%=Server.HtmlEncode(Request["QueType"]) %>" class="btn btn-primary btn_guest">待完善问题</a></li>
    <!-- <li><a title="知识专题" href="Topic.aspx class="btn btn-primary btn_guest"">知识专题</a></li>-->
    </ul>
</div>

<div class="container">
<div class="ask_class">
<div class="ask_class_t">问题分类</div>
<div class="ask_class_c">
<div class="row">
<ul class="list-unstyled">
<asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
    <ItemTemplate><li class="col-lg-2 col-md-2 col-sm-4 col-xs-12"><span runat="server" id="spGrade"></span></li></ItemTemplate>
</asp:Repeater>
</ul>
</div>
</div>
</div>
<div class="ask_class_con">
<ul class="nav nav-tabs">
<li class="active"><a href="Classification.aspx?type=1&ParentID=<%=cateid %>&GradeID=<%=gradeid %>" id="type1">待完善问题</a></li>
<li><a href="Classification.aspx?type=2&ParentID=<%=cateid %>&GradeID=<%=gradeid %>" id="type2">已完善</a></li>
<li><a href="Classification.aspx?type=3&ParentID=<%=cateid %>&GradeID=<%=gradeid %>" id="type3">高分</a></li>
<li><a href="Classification.aspx?type=4&ParentID=<%=cateid %>&GradeID=<%=gradeid %>" id="type4">零回答</a></li>
<li><a href="Classification.aspx?type=5&ParentID=<%=cateid %>&GradeID=<%=gradeid %>" id="type5">全部</a></li>
</ul>
<div class="ask_class_conc">
<table class="table table-bordered">
<tr>
<th>积分</th>
<th>问题</th>
<th>回答</th>
<th>用户</th>
<th>时间</th>
</tr> 
<ZL:Repeater ID="RPT" PageSize="10" PagePre="<tr class='text-center'><td colspan='5'>" PageEnd="</td></tr>" runat="server" OnItemDataBound="Repeater2_ItemDataBound">
<ItemTemplate>
<tr>
<td><i class="fa fa-trophy"></i> <%#Eval("Score") %></td>
<td><a target="_self" href="SearchDetails.aspx?ID=<%#Eval("ID")%>"><%#GetLeftString(Eval("Qcontent").ToString(),20) %></a></td>
<td><label id="aCount" runat="server"></label></td>
<td><a href="/ShowList.aspx?id=<%#Eval("UserID")%>" target='_blank'><%#Eval("IsNi").ToString()=="1"?"匿名":Eval("UserName") %></a></td>
<td><%#Eval("AddTime", "{0:yyyy-MM-dd HH:mm}")%></td>
</tr>
</ItemTemplate>
<FooterTemplate></FooterTemplate>
</ZL:Repeater>
</table>
</div>
</div>
<div class="">
<div class="h_b">
<div class="h_b_m">
<div class="question_count">
<span class="count">最佳回答采纳率:</span><span class="adopt" ><%=getAdoption() %></span><br />
<span class="count">已解决问题数:</span><span class="countques" ><% =getSolvedCount() %></span><br />
<span class="count">待解决问题数:</span><span class="countques" ><% =getSolvingCount() %></span>
</div>
<div id="move">
<span class="count">当前在线:</span><%=getLogined() %><br />
<span class="count">注册用户:</span><%=getUserCount() %><br />
</div>
</div>
</div>
</div>
</div>
<div class="ask_bottom">
<p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
<p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
</div>
<script>
    $(function () {
        $("#top_nav_ul li[title='分类大全']").addClass("active");
        var type = '<%Call.Label("{$GetRequest(type)$}");%>';
        if (type != "")
        {
            $(".ask_class_con ul li").removeClass("active");
            $("#type" + type).parent().addClass("active");
        }
    })
</script>
</asp:Content>