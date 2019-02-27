<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Common/Master/Empty.master" CodeFile="Default.aspx.cs" Inherits="MIS_Target" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>目标</title>
<%Call.Label("{ZL:Boot()/}"); %>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script type="text/javascript">
function loadPage(id, url) {
    $("#" + id).addClass("loader");
    $("#" + id).append("Loading......");
    $.ajax({
        type: "get",
        url: url,
        cache: false,
        error: function () { alert('加载页面' + url + '时出错！'); },
        success: function (msg) {
            $("#" + id).empty().append(msg);
                   
            $("#" + id).removeClass("loader");
        }
    });
}

function Target(id) {
    if (document.getElementById("Z_type0" + id).style.display == "none") {
        document.getElementById("Z_type0" + id).style.display = "block";
    }
    else {
        document.getElementById("Z_type0" + id).style.display = "none";
    }

    loadPage("ProP" + id, "ProList.aspx?id=" + id + "&types=7");
    loadPage("ProM" + id, "memoList.aspx?id=" + id + "&types=4");
    loadPage("ProC" + id, "planList.aspx?id=" + id + "&types=5");
    loadPage("ProE" + id, "mailList.aspx?id=" + id + "&types=8");
    loadPage("ProAp" + id, "ApprovalList.aspx?id=" + id + "&types=9");

    }
function Prolist(id, types) {
     
        loadPage("Newli", "ProList.aspx?id=" + id + "&types=" + types);
        loadPage("ProMli", "memoList.aspx?id=" + id + "&types=" + types);
        loadPage("ProEli", "mailList.aspx?id=" + id + "&types=" + types);
        loadPage("ProPlan", "planList.aspx?id=" + id + "&types=" + types);
        loadPage("ProApproval", "ApprovalList.aspx?id=" + id + "&types=" + types);
        document.getElementById("Newli").style.display = "block";
        document.getElementById("ProMli").style.display = "block";
        document.getElementById("ProEli").style.display = "block";
        document.getElementById("ProPlan").style.display = "block";
        document.getElementById("ProApproval").style.display = "block";
    }
function setEmpty(obj) {
    if (obj.value == "请输入关键字") {
        obj.value = "";
    }
}
function settxt(obj) {
    if (obj.value == "") {
        obj.value = "请输入关键字";
    }
}
           
//function ProTypes(obj) {
//    document.getElementById("ProID").value = obj.value;
//    alert(document.getElementById("ProID").value);
//}
     
function SetImg(o, maxW, maxH) {
    var obj = document.getElementById(o);
    var imgH = obj.height;
    var imgW = obj.width;
    if (obj.height > maxH) {
        obj.height = maxH;
        obj.widht = (obj.width * (maxH / imgH)); 
    } 
    else if (obj.width > maxW) {
        obj.width = maxW;
        obj.height = (maxW / imgW) * imgH;
        imgW = maxW;
        imgH = obj.height;
    }
    obj.style.marginTop = (maxH - obj.height) / 2;
    obj.style.marginLeft = (maxW - obj.width) / 2;
}
</script> 
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="Meno">
<div id="pro_left"> 
    <span>梦想未来</span>
    <div class="new_tar"><a href="AddTarget.aspx">新建目标</a></div>
    <div class="search" >
        <div class="pull-left">
            <asp:DropDownList ID="drType" CssClass="form-control" runat="server" data-container="body" Width="100">
                <asp:ListItem Value="">全部</asp:ListItem>
                <asp:ListItem Value="0">事业</asp:ListItem>
                <asp:ListItem Value="1">财富</asp:ListItem>
                <asp:ListItem Value="2">家庭</asp:ListItem>
                <asp:ListItem Value="3">休闲</asp:ListItem>
                <asp:ListItem Value="4">学习</asp:ListItem>
            </asp:DropDownList> 
        </div>
        <div class="input-group pull-right" style="width:180px;">
            <asp:TextBox ID="TxtKey" CssClass="form-control" runat="server" Text="请输入关键字" Width="140"  onclick="setEmpty(this)" onblur="settxt(this)"></asp:TextBox>
            <span class="input-group-btn">
                <asp:Button ID="Button1" runat="server" Text="搜索" CssClass="btn btn-primary"  OnClick="Button_Click" />
            </span>
        </div><!-- /input-group -->
        <div class="clearfix"></div>
        <div class="Target_list"> 
            <ul>
                <asp:Repeater ID="Repeater2" runat="server">
                    <ItemTemplate>
                        <li><a href="Default.aspx?ID=<%#Eval("ID") %>"><%#Eval("Title") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>
</div>
   
<div id="pro_right">
    <h1><asp:Label ID="TxtTitle" runat="server" Text="Label"></asp:Label> <asp:Label ID="TxtTitleEdt" runat="server" class="font12" ></asp:Label>  </h1>
    <div class="Schedule"><a id="line"  runat="server" title="项目进度"></a></div><asp:Label ID="Schedule" runat="server" Text=""></asp:Label>
    <div class="clear"></div>
     <div class="target_tit"><span class='greyl'>创建人：</span><asp:Label ID="TxtInputer" runat="server" Text="Label"></asp:Label> <span class='greyl'>( <asp:Label ID="lblStartTime" runat="server"></asp:Label> ∽ <asp:Label ID="lblEndTime" runat="server"></asp:Label> )</span>  <span class='greyl'>参与人：</span><asp:Label ID="TxtJoiner" runat="server" Text="Label"></asp:Label></div>
     
    <div  class="DreamePic"> <asp:Image ID="TxtPic" runat="server" ImageUrl=""   /></div> 
	<div class="tarcon"> <strong>详细:</strong> <br><asp:Label ID="TxtCotent" runat="server" Text="Label"></asp:Label></div>
    <div class="Z_Target">
   <asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
     <div class="Z_Tar" id="Z_Tar<%#Eval("ID") %>"> 
        <div class="Z_Targ"><a href="javascript:void(0)" onclick="Target('<%#Eval("ID") %>')"><strong></strong><img src="<%# GetPic(Eval("Pic").ToString()) %>" id="pic" /><%#Eval("Title")%> <br /><span><%#Eval("CreateTime") %></span></a><br />
            <a href="AddTarget.aspx?ParentID=<%=Request["ID"]%>&ID=<%#Eval("ID") %>">修改</a> 
        </div>
        <div class="Z_else"><a href="javascript:void(0)" onclick="Telse('<%#Eval("ID") %>')"><img src="/App_Themes/UserThem/images/Mis/jia.jpg" /></a></div>
         <div id="TarDiv<%#Eval("ID") %>"  class="Quote" style="display:none"></div>
        <div class="clear"></div>
    </div>
    <div id="Z_type0<%#Eval("ID") %>" class="Z_type" style="display:none ">
        <ul><li id="ProP<%#Eval("ID") %>" class="Prolists"> </li>
            <li id="ProJo<%#Eval("ID")%>" class="Prolists"></li>
            <li id="ProM<%#Eval("ID") %>" class="Prolists"></li>
            <li id="ProC<%#Eval("ID") %>" class="Prolists"></li>
            <li id="ProT<%#Eval("ID") %>" class="Prolists"></li>
            <li id="ProPd<%#Eval("ID") %>" class="Prolists"></li>
            <li id="ProA<%#Eval("ID") %>" class="Prolists"></li>
            <li id="ProE<%#Eval("ID") %>" class="Prolists"></li>
            <li id="ProAp<%#Eval("ID")%>" class="Prolists"></li>
        </ul>
        <div class="clear"></div>
    </div>
        </ItemTemplate>
    </asp:Repeater> 
        <div class="Add_Tar">
            <asp:Label ID="NewBar" runat="server"></asp:Label>
        </div>
    </div> 
</div>
<div class="clear"></div> 
    <div class="Quote" id="Quote" style="display:none">
        <div class="left_ico"><img src="../../App_Themes/UserThem/images/Mis/jian.jpg" /></div>
    <ul>
    <li id="MeoIn"><a href="javascript:void(0)" onclick="project('<%=Request["ID"] %>','4')"><img src="../../App_Themes/UserThem/images/Mis/j_ico_memo.png" /><br />备忘</a></li>
    <li id="EmlIn"><a href="javascript:void(0)" onclick="project('<%=Request["ID"] %>','8')"><img src="../../App_Themes/UserThem/images/Mis/j_ico_email.png" /><br />邮件</a></li>
    <li id="Pid"><a href="javascript:void(0)" onclick="project('<%=Request["ID"] %>','7')"><img src="../../App_Themes/UserThem/images/Mis/j_ico_project.png" /><br />项目</a></li>
    <li id="PlanIn"><a href="javascript:void(0)" onclick="project('<%=Request["ID"] %>','5')"><img src="../../App_Themes/UserThem/images/Mis/j_ico_plan.png" /><br />计划</a></li>
    <li id="WordIn"><a href="#"><img src="../../App_Themes/UserThem/images/Mis/j_ico_docu.png" /><br />文档</a></li>
    <li id="TalIn"><a href="#"><img src="../../App_Themes/UserThem/images/Mis/j_ico_comm.png" /><br />沟通</a></li>
    <li id="AnnIn"><a href="#"><img src="../../App_Themes/UserThem/images/Mis/j_ico_notice.png" /><br />公告</a></li>
    <li id="DisIn"><a href="#"><img src="../../App_Themes/UserThem/images/Mis/j_ico_bbs.png" /><br />论坛</a></li>
    <li id="QueIn"><a href="#"><img src="../../App_Themes/UserThem/images/Mis/j_ico_konw.png" /><br />知识</a></li>
    <li id="CheckIn"><a href="javascript:void(0)" onclick="project('<%=Request["ID"]%>','5')"><img src="../../App_Themes/UserThem/images/Mis/j_ico_approve.png" /><br />审批</a></li>
</ul>
</div>
    <div class="Newli" id="Newli" style="display:none"></div>
    <div class="ProMli" id="ProMli" style="display:none"></div>
    <div class="ProEli" id="ProEli" style="display:none"></div>
    <div class="ProPlan" id="ProPlan" style="display:none"></div>
    <div class="ProApproval" id="ProApproval" style="display:none"></div>
<div id="QuoteContent"><iframe id="pifrm" src="" width="500" height="300" frameborder="0" scrolling="no"></iframe></div>
</div>  
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script>
    function Telse(id) {
    if (document.getElementById("TarDiv" + id).style.display != "none")
    {
        document.getElementById("Quote").style.display = "none";
        document.getElementById("TarDiv" + id).style.display = "none";
       document.getElementById("TarDiv" + id).innerHTML = "";}
    else {
        $(".Quote").css("display", "none");
        document.getElementById("TarDiv" + id).style.display = "block";
       // document.getElementById("Quote").style.display = "block";
        document.getElementById("TarDiv" + id).innerHTML = document.getElementById("Quote").innerHTML;
        var t = document.getElementById('Z_Tar' + id).offsetTop - 25;
        document.getElementById("Quote").style.top = t;
        document.getElementById("MeoIn").innerHTML = '<a href="javascript:void(0)"  onclick=project("' + id + '","4")><img src="../../App_Themes/UserThem/images/Mis/j_ico_project.png" /><br />备忘</a>';
        document.getElementById("EmlIn").innerHTML = '<a href="javascript:void(0)"  onclick=project("' + id + '","8")><img src="../../App_Themes/UserThem/images/Mis/j_ico_project.png" /><br />邮件</a>';
        document.getElementById("Pid").innerHTML = '<a href="javascript:void(0)"  onclick=project("' + id + '","7")><img src="../../App_Themes/UserThem/images/Mis/j_ico_project.png" /><br />项目</a>';
        document.getElementById("PlanIn").innerHTML = '<a href="javascript:void(0)"  onclick=project("' + id + '","5")><img src="../../App_Themes/UserThem/images/Mis/j_ico_plan.png" /><br />计划</a>';
        document.getElementById("CheckIn").innerHTML = '<a href="javascript:void(0)"  onclick=project("' + id + '","9")><img src="../../App_Themes/UserThem/images/Mis/j_ico_approve.png" /><br />审批</a>';
    }
}

function project(id, t) {
    document.getElementById("Quote").style.display = "none";
    document.getElementById("QuoteContent").style.display = "block";

    if (t == 7) {
        document.getElementById("pifrm").src = "ProQuote.aspx?ParentID=<%=Request["ID"]%>&id=" + id + "&types=" + t;
        //loadPage("QuoteContent", "ProQuote.aspx?ParentID=<%=Request["ID"]%>&id=" + id + "&types=" + t);
        }
    if (t == 4) {
        document.getElementById("pifrm").src = "memoQuote.aspx?ParentID=<%=Request["ID"]%>&id=" + id + "&types=" + t;
        //loadPage("QuoteContent", "memoQuote.aspx?ParentID=<%=Request["ID"]%>&id=" + id + "&types=" + t);
    }
    if (t == 5) {
        document.getElementById("pifrm").src = "planQuote.aspx?ParentID=<%=Request["ID"]%>&id=" + id + "&types=" + t;
        //loadPage("QuoteContent", "planQuote.aspx?ParentID=<%=Request["ID"]%>&id=" + id + "&types=" + t);
    }
    if (t == 8) {
        document.getElementById("pifrm").src = "mailQuote.aspx?ParentID=<%=Request["ID"]%>&id=" + id + "&types=" + t;
        //loadPage("QuoteContent", "mailQuote.aspx?ParentID=<%=Request["ID"]%>&id=" + id + "&types=" + t);
    }
    if (t == 9) {
        document.getElementById("pifrm").src = "ApprovalQuote.aspx?ParentID=<%=Request["ID"]%>&id=" + id + "&types=" + t;
            //loadPage("QuoteContent", "mailQuote.aspx?ParentID=<%=Request["ID"]%>&id=" + id + "&types=" + t);
        }
} 
</script>
</asp:Content>
