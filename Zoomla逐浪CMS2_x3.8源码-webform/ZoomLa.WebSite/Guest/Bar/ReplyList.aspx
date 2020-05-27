<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReplyList.aspx.cs" Inherits="Plat_Blog_ReplyList" EnableViewState="false" ClientIDMode="Static" %>
<form id="form1" runat="server">
<script src="/JS/ICMS/ZL_Common.js"></script>
<start>
<asp:Repeater runat="server" ID="ReplyList_RPT">
<ItemTemplate>
<div style="padding-left: 5px; padding-top: 15px;" id="reply_div_<%#Eval("ID") %>">
	<div style="float: left;">
		<img style="width: 30px; height: 30px;" src='<%#GetUserFace() %>' onerror="shownoface(this);" />
	</div>
	<div style="margin-left: 45px;">
		<div>
		   <a href="#" style="text-decoration:none;" title="用户信息"><%#GetUName()  %>:</a>
			<%#GetMSG() %>
		</div>
		<div style="padding-top: 5px;padding-right:10px;text-align:right;font-size:12px;color:#999;">
			<%#GetDel() %><%#Convert.ToDateTime(Eval("CDate")).ToString("yyyy年MM月dd日 HH:mm") %>
			<a href="javascript:;" onclick="ReplyHer(<%#Eval("pid") %>,'<%#GetUName() %>');" style="margin-left: 10px;">回复</a>
		</div>
	</div>
	<div style="border-bottom: 1px #d7d7d7 dotted; width: 98%; margin-left: 1%;margin-top:5px;"></div>
</div>
</ItemTemplate>
<FooterTemplate>
</FooterTemplate>
</asp:Repeater>
</start>
</form>