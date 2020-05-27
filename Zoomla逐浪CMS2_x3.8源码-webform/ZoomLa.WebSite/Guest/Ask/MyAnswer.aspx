<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyAnswer.aspx.cs" Inherits="Guestbook_MyAnswer" MasterPageFile="~/Guest/Ask/Ask.master" ValidateRequest="false"  EnableViewStateMac="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>我的问答中心-<%Call.Label("{$SiteName/}"); %>问答</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container">
<ol class="breadcrumb">  
<li>您的位置：<a href="/">网站首页</a></li>
<li><a href="/Ask">问答中心</a></li>
<li><a href="List.aspx?strWhere=&QueType=">问题列表</a></li>
<li class="active"><a href="MyAnswer.aspx?ID=<%=Request["ID"]%>">问题详情</a></li>
</ol>
<div class="alert alert-danger" role="alert">
最佳回答采纳率:<%=getAdoption() %>，已解决问题数:<% =getSolvedCount() %>，待解决问题数:<% =getSolvingCount() %>，当前在线:</span><%=getLogined() %>，注册用户:<%=getUserCount() %>
</div>
<div class="myanswer">
<div class="aacengter">
<div>
<span class="pull-right">
    <a href="javascript:;" onclick="LikeQue()">
    <span class="fa fa-star"></span> 收藏</a></span>
<span class="questions">问题：</span><span style="font-weight:bold;"><asp:Label ID="question" runat="server"></asp:Label></span><br />
<span class="questions">提问者：<a href="#"><asp:Label ID="username" runat="server"></asp:Label></a></span>
<span class="questions">提问日期：<asp:Label ID="addtime" runat="server"></asp:Label></span><br />
<span class="questions" id="supdiv" runat="server">问题补充：</span><asp:Label ID="supment" runat="server"></asp:Label>
</div>
<div class="tj_content"> 
<div class="tit"><span>您的回答:</span></div>
<div class="ask_txt">
<asp:TextBox runat="server" ID="txtContent" CssClass="form-control" TextMode="MultiLine" Height="100px"></asp:TextBox>
<span class="pull-right"><asp:CheckBox ID="CkIsNi" runat="server" />匿名<asp:Button runat="server" ID="btnSubmit" Text="提交回答" CssClass="btn btn-info margin_left10" OnClick="btnSubmit_Click" /></span>
</div>
<div class="clr"></div></div>             
<div class="show_an">
<div class="tit"><div class="bg_hd"><span>其他回答:</span></div></div>
<div class="pad10"> 
<asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound">
<ItemTemplate>
<div class="g_show_txt">
<%# Eval("Content")%> <div><%#getstatus(Eval("Status","{0}"))%></div></div> 
<div class="btm">
<span class="btmlabe ad">回答者：<asp:Label id="Isname" runat="server"><a href="../../ShowList.aspx?id=<%#Eval("UserID")%>" target='_blank'><%#Eval("IsNi").ToString()=="1"?"匿名":Eval("UserName") %></a></asp:Label></span><span class="btmlabe date">回答日期：<%# Eval("AddTime")%></span></div>
<asp:Repeater runat="server" ID="Repeater2"  OnItemDataBound="Repeater2_ItemDataBound">
<ItemTemplate>  
<div class="g_show_txt" id="content">
<div> &nbsp;&nbsp;&nbsp;追问：<%#Eval("Content")%></div>
<asp:Repeater runat="server" ID="Repeater3">
<ItemTemplate>  
<div class="g_show_txt" id="content">
&nbsp;&nbsp;  回答：<%# Eval("Content")%></div> 
</ItemTemplate>
</asp:Repeater>  
</div>  
<asp:Panel  ID="ReplyBtn"  runat="server" > 
<input type="button" style="border-width: 0px; background: none; color: #3171af;" value="回复追问" onclick="reply(<%#Eval("ID")%>)" />
</asp:Panel>  
</ItemTemplate>
</asp:Repeater> 
</ItemTemplate>
</asp:Repeater>
</div> 
</div>
</div>
</div>
</div>
<div class="ask_bottom">
<p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
<p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
</div>
<asp:HiddenField runat="server" ID="hfsid" />
<script type="text/javascript" src="/JS/Modal/APIResult.js"></script>
<script type="text/javascript">
    function show() {
        var div = document.getElementById("show");
        if (div.style.display == "none") {
            div.style.display = "block";
        }
        // else {div.style.display = "none";}

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
