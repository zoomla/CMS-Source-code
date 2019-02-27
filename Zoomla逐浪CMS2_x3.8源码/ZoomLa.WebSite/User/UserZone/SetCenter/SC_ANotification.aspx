<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/User/Default.master" CodeFile="SC_ANotification.aspx.cs" Inherits="User_UserZone_SetCenter_SC_ANotification" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<%@ Register Src="WebUserControlSetCenterTop.ascx" TagName="WebUserControlSetCenterTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>会员中心 >> 设置中心</title>
<link href="/App_Themes/User.css" rel="stylesheet" type="text/css" />
<script src='<%=ResolveUrl("~/JS/DatePicker/WdatePicker.js")%>' type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5"> 
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li> 
        <li class="active">设置中心</li>
    </ol>
</div>

 
	<div class="container btn_green">
		<uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
		<uc1:WebUserControlSetCenterTop ID="WebUserControlSetCenterTop" runat="server" />
	</div> 
<div class="container btn_green u_cnt">
   <div class="us_topinfo" style="margin-top: 10px; width: 98%">
		<table border="0" class="us_showinfo" width="100%" align="center" cellpadding="0"
			cellspacing="0">
			<tr>
				<td>
					活动通知
				</td>
			</tr>
		</table>
</div>
    </div>
</asp:Content>
 