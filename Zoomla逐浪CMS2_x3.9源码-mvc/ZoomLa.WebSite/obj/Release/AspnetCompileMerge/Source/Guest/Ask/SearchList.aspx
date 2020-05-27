<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchList.aspx.cs" Inherits="ZoomLaCMS.Guest.Ask.SearchList" MasterPageFile="~/Guest/Ask/Ask.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>问题列表_<%Call.Label("{$SiteName/}"); %></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">

<div class="container">
<ul class="breadcrumb">
<li>您的位置：<a href="/">网站首页</a></li>
<li><a href="/Ask">问答中心</a></li>
<li class="active">搜索列表</li>
</ul> 
</div>
 
<div class="container">
    <div class="h_mid">
        <div class="h_mid_l"></div>
        <div id="tdh" class="h_mid_m"> 
        <span style="<%=getstyle()%>">   <a href="MyAskList.aspx?QueType=">我的提问</a> <a href="MyAnswerlist.aspx">我的回答</a></span>
        <ul class="list-inline">
            <li><a title="问答首页" href="/Ask" class="btn btn-primary btn_guest">问答首页</a> </li>
            <li class="on"><a title="问答之星" href="../Ask/Star.aspx" class="btn btn-primary btn_guest">问答之星</a></li>
            <li><a title="分类大全" href="../Ask/Classification.aspx" class="btn btn-primary btn_guest">分类大全</a></li>
           <li><a href="List.aspx?strWhere=<%=Server.HtmlEncode(Request["strWhere"]) %>&QueType=<%=Server.HtmlEncode(Request["QueType"]) %>" class="btn btn-primary btn_guest">待完善问题</a></li>
            <!-- <li><a title="知识专题" href="Topic.aspx class="btn btn-primary btn_guest"">知识专题</a></li>-->
            </ul>
        </div>
    </div>
    <div class="h_b">
        <div class="h_b_l"></div>
        <div class="h_b_m">
            <div class="question_count">
                <span class="count">最佳回答采纳率:</span><span class="adopt" ><%=getAdoption() %></span><br />
                <span class="count">已解决问题数:</span><span class="countques" ><% =getSolvedCount() %></span><br />
                <span class="count">待解决问题数:</span><span class="countques" ><% =getSolvingCount() %></span>
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


<div class="asklist">
        <asp:Repeater runat="server" ID="repSearch"  OnItemDataBound="repSearch_ItemDataBound" >
            <ItemTemplate>
                <table>
                    <td>
                            <h3>
                            <a target="_self" href="SearchDetails.aspx?ID=<%#Eval("ID")%>"> <%#Eval("Qcontent")%></a>
                            </h3>
                            <div class="abstract"><%#getanswer(Eval("ID","{0}"))%></div>
                            <div class="fs" >  <asp:Label id="Isname" runat="server">  <a href="../../ShowList.aspx?id=<%#Eval("UserID")%>" target='_blank'><%#Eval("UserName") %></a>
                   </asp:Label>- <%#Eval("AddTime", "{0:yyyy-MM-dd}")%>--<a href="#">
<%#gettype(Eval("QueType","{0}"))%></a></div>
                    </td>
            </table>  
            </ItemTemplate>
        </asp:Repeater>
        <div class="text-center"">共<asp:Label ID="AllNum" runat="server" Text=""></asp:Label>条记录
        <asp:Label runat="server" ID="Toppage"></asp:Label>
        <asp:Label runat="server" ID="Nextpage"></asp:Label>
        <asp:Label runat="server" ID="Downpage"></asp:Label>
        <asp:Label runat="server" ID="Endpage"></asp:Label>
        页次：<asp:Label ID="Nowpage" runat="server"></asp:Label>/<asp:Label ID="PageSize" runat="server" ></asp:Label>页<asp:Label ID="Lable1" runat="server"></asp:Label>条记录/页 转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" ></asp:DropDownList>页
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
            // else {div.style.display = "none";}

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
