<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyAnswerlist.aspx.cs" MasterPageFile="~/Guest/Ask/Ask.master" Inherits="Guest_MyAnswerlist"  EnableViewStateMac="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>我的回答</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

<div class="container">
<ul class="breadcrumb">
<li>您的位置：<a href="/">网站首页</a></li>
<li><a href="/Ask">问答中心</a></li>
<li class="active">我的回答</li>
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

    <div class="h_mid">
        <div class="h_mid_l"></div>
        <div id="tdh" class="h_mid_m">
        <span style="<%=getstyle()%>">   <a href="MyAskList.aspx?QueType=">我的提问</a> <a href="MyAnswerlist.aspx">我的回答</a></span>
            <ul>
                <li class="on"><a title="问答首页" href="/Ask">问答首页</a> </li>
                <li><a title="问答之星" href="../Ask/Star.aspx">问答之星</a></li>
                <li><a title="分类大全" href="../Ask/Classification.aspx">分类大全</a></li>
            </ul>
        </div>
    </div>
    <div class="h_b">
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
        </div>
    </div>
</div>

<div class="container">
 <div style="border-top: 2px solid #3171af; padding: 25px 0 10px 0;">
		<asp:Repeater runat="server" ID="repSearch">
			<ItemTemplate>
				<table cellspacing="0" cellpadding="0" border="0" width="100%">
					<tbody>
						<tr>
							<td class="f">
								<div class="fs">
									<a target="_self" href="SearchDetails.aspx?ID=<%#Eval("ID")%>">
										<%#Eval("Qcontent")%></a>
								</div>
								<div class="abstract">
									<%#getanswer(Eval("ID","{0}"))%>
								</div>
								<div class="fs">
									<a href="#"><%#Eval("UserName")%></a>--<%#Eval("AddTime", "{0:yyyy-MM-dd}")%>
								</div>
							</td>
						</tr>
					</tbody>
				</table>
				<br>
			</ItemTemplate>
		</asp:Repeater>
		<div style="text-align:center">共<asp:Label ID="AllNum" runat="server" Text=""></asp:Label>条记录
		<asp:Label runat="server" ID="Toppage"></asp:Label>
		<asp:Label runat="server" ID="Nextpage"></asp:Label>
		<asp:Label runat="server" ID="Downpage"></asp:Label>
		<asp:Label runat="server" ID="Endpage"></asp:Label>
		页次：<asp:Label ID="Nowpage" runat="server"></asp:Label>/<asp:Label ID="PageSize" runat="server" ></asp:Label>页<asp:Label ID="Lable1" runat="server"></asp:Label>条记录/页 转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true"></asp:DropDownList>页
		 </div>
	</div>    
</div>
<div class="ask_bottom">
<p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
<p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
</div>
<script type="text/javascript">
    $(function () {
        $("#top_nav_ul li[title='我的问答']").addClass("active");
    })
</script>
</asp:Content>