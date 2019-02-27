<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyAskList.aspx.cs" MasterPageFile="~/Guest/Ask/Ask.master" Inherits="Guest_MyAskList" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>问题列表-<%Call.Label("{$SiteName/}"); %>问答</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

<div class="container">
<ul class="breadcrumb">
<li>您的位置：<a href="/">网站首页</a></li>
<li><a href="/Ask">问答中心</a></li>
<li class="active">我的问题列表</li>
</ul> 
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
    <div class="asklist">
         <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >    
      <Columns>
      <asp:TemplateField >
        <ItemTemplate> 
             <div class="qst">
                   <div class="btn btn-success btn-xs"><%# GetStatus(Convert.ToInt32( Eval("Status"))) %></div>
               <h3>
                    <a target="_self" href="Interactive.aspx?ID=<%#Eval("ID")%>"><%#GetLeftString(Eval("Qcontent").ToString(),30) %></a>
               </h3>                 
              <div class="fs">
                  <%# Getname(Eval("isNi", "{0}"),Eval("UserName", "{0}"),Eval("UserID", "{0}"))%>  - <%#Eval("AddTime", "{0:yyyy-MM-dd}")%> - <a href="Classification.aspx?GradeID=<%# Eval("QueType")%>"><%#gettype(Eval("QueType","{0}"))%></a>
               </div>
             </div>
        </ItemTemplate>
        <ItemStyle />
      </asp:TemplateField>
      </Columns>
</ZL:ExGridView>
    </div>
  </div>
<div class="ask_bottom">
<p class="text-center"><a target="_blank" title="如何提问" href="http://help.z01.com/?index/help.html#如何提问">如何提问</a> <a target="_blank" title="如何回答" href="http://help.z01.com/?index/help.html#如何回答">如何回答</a> <a target="_blank" title="如何获得积分" href="http://help.z01.com/?index/help.html#如何获得积分">如何获得积分</a> <a target="_blank" title="如何处理问题" href="http://help.z01.com/?index/help.html#如何处理问题">如何处理问题</a></p>
<p class="text-center"><%Call.Label("{$Copyright/}"); %></p>
</div>

<script  type="text/javascript">
    $(function () {
        $("#top_nav_ul li[title='我的提问']").addClass("active");
    })
    function show() {
        var div = document.getElementById("show");
        if (div.style.display == "none") {
            div.style.display = "block";
        }
    }

</script>
<script  type="text/javascript">
    function show() {
        var div = document.getElementById("show");
        if (div.style.display == "none") {
            div.style.display = "block";
        }
    }
</script>
</asp:Content>