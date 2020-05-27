<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.MIS.Approval.Default"  MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>审批</title>
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
function putid(id) {
document.getElementById("TitComType").innerText = "添加类型";
document.getElementById("HidCommTxt").value = "0";
document.getElementById("TxtComment").value = "";
document.getElementById("CommentDiv").style.display = "block";
var h = document.getElementById("divcons").getBoundingClientRect().top;
document.getElementById("CommentDiv").style.top = (h - 104) + "px";
document.getElementById("CommentDiv").style.background = "#fdfce6";
}

function Punids(id, Cont) {
document.getElementById("TitComType").innerText = "修改类型";
document.getElementById("CommentDiv").style.display = "block";
var h = document.getElementById("divcon" + id).getBoundingClientRect().top;
document.getElementById("HidCommTxt").value = id;
document.getElementById("TxtComment").value = Cont;
document.getElementById("CommentDiv").style.top = (h-126) + "px";
document.getElementById("CommentDiv").style.background = "#fdfce6";
}

function HideDiv(div_id) {
$("#" + div_id).animate({ opacity: "hide" }, 300);
}

function HideAndShow(div1,div2,Li1,Li2) {
document.getElementById(div1).style.display = "block";
document.getElementById(div2).style.display = "none";
document.getElementById(Li1).style.background = "#eaeae8";
document.getElementById(Li2).style.background = "#ccc";
}

</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="Meno">
<div id="pro_left">
<div class="new_tar">
<a href="AddApproval.aspx">新建申请单</a></div>
<div class="pro_li">
<ul>
<li><a href="Default.aspx?type=1">我的申请</a></li>
<li><a href="Default.aspx?type=2">我的审批</a></li>
<li><a href="Default.aspx?type=3">已归档</a></li>
<li><a href="Default.aspx?type=8">抄送给我的</a></li>
<li><a href="Default.aspx?type=7">审批设置</a></li>
</ul>
</div>
</div>
<div id="pro_right" runat="server">
<div class="Mis_Title"><asp:Label ID="tit" runat="server" Text="我的申请"></asp:Label></div>
<div class="search">
<div id="MyApplication" runat="server">
<a href="Default.aspx?type=1">所有(<asp:label ID="lblAllOf" runat="server"></asp:label>)</a>
<a href="Default.aspx?type=4">审批中(<asp:Label ID="lblInApps" runat="server"></asp:Label>)</a>
<a href="Default.aspx?type=5">不同意(<asp:Label runat="server" ID="lblonApps"></asp:Label>)</a>
</div>
<div id="MyApps" runat="server">
<a href="Default.aspx?type=2">所有(<asp:Label ID="lblAlls" runat="server"></asp:Label>)</a>
<a href="Default.aspx?type=6">未处理(<asp:Label ID="lblNosave" runat="server"></asp:Label>)</a>
</div>
<div id="SaveOn" runat="server">
<a href="#">所有(<asp:Label ID="lalAllss" runat="server"></asp:Label>)</a>
<a href="#">未处理(<asp:Label ID="lblNoDell" runat="server"></asp:Label>)</a>
</div>
<div id="SendTome" runat="server">
<a href="Default.aspx?type=8">所有(<asp:label ID="lblAllMe" runat="server"></asp:label>)</a>
<a href="Default.aspx?type=9">不同意(<asp:label ID="lblNoPass" runat="server"></asp:label>)</a>
</div>
<div class="search_r" style="display:none;">
<asp:TextBox ID="TxtKey" CssClass="b_input" runat="server" Text="请输入关键字" Width="120" onclick="setEmpty(this)" onblur="settxt(this)"></asp:TextBox>
<asp:Button ID="Button1" runat="server" Text="" CssClass="bottom_bg"   />
</div>
</div>
<table  class="boder ApproverTd" width="100%">
<tr>
<th class="AppTitle"><asp:DropDownList ID="DrpType" OnSelectedIndexChanged="DrpType_SelectedIndexChanged" AutoPostBack="true" runat="server"></asp:DropDownList>申请内容</th>
<th class="AppPeson"> 审批人</th>
<th class="AppResult" align="left"> 审批结果 </th>
</tr>
<ZL:ExRepeater ID="RPT" runat="server" PagePre="<tr><td colspan='3' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
<ItemTemplate>
<tr class="Approval" onmouseover="this.style.backgroundColor='#eaeae8'" onmouseout="this.style.backgroundColor='#fff'">
    <td class="AppTitle"><a href="ApproverView.aspx?ID=<%#Eval("ID") %>"><%#Eval("content") %></a></td><td class="AppPeson"><%#Eval("Approver") %></td><td class="AppResult"><%#GetResults(Convert.ToInt32(Eval("Results"))) %></td>
</tr>
</ItemTemplate>
<FooterTemplate></FooterTemplate>
</ZL:ExRepeater>
<tr><td colspan="3"><table width="100%" align="center">
</table></td></tr>
</table>
         
<div class="Approval_list">
<ul>
<asp:Repeater ID="Repeater2" runat="server">    
<ItemTemplate>
<li><a href="#">asa</a></li>
</ItemTemplate>
</asp:Repeater></ul>
</div>

</div>

<div id="SetUpType" runat="server">
<div style="overflow:hidden; background-color:#F4F8FB;">
<ul>
<li id="SetProLis" style="background-color:#eaeae8;"><a href="#" onclick="HideAndShow('Setprocedure','SetType','SetProLis','SetTypeLis')">流程设置</a></li>
<li id="SetTypeLis"><a href="#" onclick="HideAndShow('SetType','Setprocedure','SetTypeLis','SetProLis')">类型设置</a></li>
</ul>
</div>
<div id="Setprocedure">
<div style="float:right;"><a href="EditProcedure.aspx" title="新建审批流程">新建审批流程</a></div>
<div id="SetPHead">
<div id="ProContent"><asp:DropDownList ID="DpList" runat="server" OnSelectedIndexChanged="DpList_SelectedIndexChanged"></asp:DropDownList>
</div>
</div>
<div id="SetPro" style="overflow:hidden;">
<asp:Repeater ID="RepProcedure" runat="server">
<HeaderTemplate>
<ul>
</HeaderTemplate>
<ItemTemplate>
<li onmouseover="this.style.backgroundColor='#eaeae8'" onmouseout="this.style.backgroundColor='#fff'"><a href="EditProcedure.aspx?ProID=<%#Eval("ID")%>"><%#Eval("ProcedureName")%></a></li>
</ItemTemplate>
<FooterTemplate>
</ul>
</FooterTemplate>
</asp:Repeater>
</div>
</div>
<div id="SetType" style="display:none;">
<div style="float:right;"><a href="#" onclick="putid('SetUpType')">添加类型</a></div>
<div id="divcons"></div>
<table style="width:705px;" border="0">
<tr class="TrHead">
<th class="TdID">序号</th>
<th class="TdType">类型</th>
<th class="TdUse">操作</th>
</tr>
<asp:Repeater ID="RepType" runat="server" DataMember="ID" OnItemCommand="RepType_ItemCommand">
<ItemTemplate>
    <tr class="TBodys" onmouseover="this.style.backgroundColor='#eaeae8'" onmouseout="this.style.backgroundColor='#FFF'">
    <td class="TdID"><asp:Label ID="lblNum" runat="server"></asp:Label></td>
    <td class="TdType"><%#Eval("TypeName")%></td>
    <td class="TdUse">
    <a href="#" onclick="Punids('<%#Eval("ID")%>','<%#Eval("TypeName")%>')">修改</a>&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="linkBtnDele" runat="server" CommandName="Delete" OnClientClick="return confirm('确定要删除该条记录么？')" CommandArgument='<%#Eval("ID")%>'>删除</asp:LinkButton>
        <div id='divcon<%#Eval("ID")%>'></div>
    </td>
    </tr>
</ItemTemplate>
</asp:Repeater>
</table>
</div>
<div id="CommentDiv" style=" background-color:#fdfce6; position:absolute; padding:8px;border:1px solid #ccc;display:none;">
<div style="float:left; width:680px;"><asp:Label ID="TitComType" runat="server"></asp:Label></div>
<div style="text-align:right; float:left;">
<span class="closex"><a href="javascript:void(0)" onclick="HideDiv('CommentDiv')" >×</a></span>
</div>
<asp:TextBox ID="TxtComment" Text="" runat="server" Width="686px"></asp:TextBox>
<br />
<input ID="HidCommTxt" type="hidden" value="" runat="server" />
<span style=" float:right;">
<asp:Button ID="BtnComment" Text="确定" runat="server" OnClick="BtnComment_Click"/>
<input type="button" value="取消" id="conbtn" onclick="HideDiv('CommentDiv')"/> 
</span>
</div>
</div>
</asp:Content>

