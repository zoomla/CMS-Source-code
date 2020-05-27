<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoteManage.aspx.cs" Inherits="ZoomLaCMS.Plugins.VoteManage" %>
<!DOCTYPE html>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>报名系统_状态查询</title>
<link href="../App_Themes/UserThem/Survey.css" rel="stylesheet" type="text/css" />
</head> 
<body style=" background:none;">
<form id="form2" runat="server">
<div id="Votetop">
<strong>网上报名与查询系统 </strong>
</div>
<div id="VoteMain">
<div id="VoteL">
<ul>
<li><a href="/Plugins/UserVote.aspx?SID=3">考试报名信息</a></li> 
<li><a href="/Plugins/VoteManage.aspx?SID=3">状态查询</a></li> 
<li><a href="/User/logout.aspx">退出</a></li>   
</ul>  
<div class="voteL_t"><strong>联系方式</strong></div> 
招生热线:<br />
 0393-4677777、4677100<br />
 E-mail:mxp@vip.163.com<br />
传真: 0393-4677222 
</div>
<div id="VoteR">
    <strong>录取结果查询</strong><br />
    <asp:Label ID="lbStatus" runat="server" Style="color:#f00"></asp:Label>
</div> 
</div>
</form>
</body>
</html>
