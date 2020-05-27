<%@ Page Language="C#"  MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="kiss.aspx.cs" Inherits="User_UserZone_Kiss_kiss" EnableViewState="false" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>会员中心 >> 设置中心</title> 
<script src='<%=ResolveUrl("~/JS/DatePicker/WdatePicker.js")%>' type="text/javascript"></script>
<style type="text/css">
.trr {text-align: left;}
.style1{height: 24px;}
#Upload_pic{width: 42%;}
.style2{height: 147px; }
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5"> 
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li> 
        <li class="active">我的秋波</li>
    </ol>
</div>
<div class="container btn_green">
<uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
</div>
<div class="container btn_green">
<div class="us_topinfo u_cnt">
<table width="100%" height="91" border="0" cellpadding="0" cellspacing="0">
	<tr>
		<td valign="top" style="width: 100%">
			<asp:Panel ID="Panel1" runat="server">
			<ZL:ExRepeater ID="RPT" runat="server" PagePre="<div class='text-center'>" PageEnd="</div>">
				<ItemTemplate>
					<table width="100%" border="0" cellpadding="0" cellspacing="0" style="word-break:break-all">
						<tr>
							<td align="center" rowspan="3">
								<asp:Image ID="Image1" runat="server" Height="96px" Width="96px" ImageUrl='<%# getuserpic(DataBinder.Eval(Container.DataItem, "InputerID").ToString())%>' />
								<br /><a href="rekiss.aspx?userid=<%#Eval("inputerID") %>">回发秋波</a>
							</td>
							<td width="84%">
								<a href="kiss.aspx?menu=delete&id=<%#Eval("ID") %>" onclick="return confirm('确实要删除此秋波吗？');" >[删除]</a>&nbsp;<%#Eval("title","{0}")%>内容:
							</td>
						</tr>
						<tr>
							<td valign="top" style="white-space: normal">
							   <a href="ViewSkins.aspx?id=<%#Eval("ID") %>"><%#ZoomLa.Common.BaseClass.Left(DataBinder.Eval(Container.DataItem, "content","{0}"),100)%>...</a>
							</td>
						</tr>
						<tr>
							<td>
							<%#Eval("IsRead", "{0}") == "1" ? Eval("ReadTime", "查看时间：{0}　　") : ""%>发送人：<%#Eval("Inputer")%>　　发送时间：<%#Eval("SendTime")%>
								
							</td>
						</tr>
						<tr>
							<td align="center" valign="top" style="width: 152px" class="trr">
							   <br />
							</td>
							<td class="trr">
								<br />
							</td>
						</tr>
					</table>
				</ItemTemplate>
                <FooterTemplate></FooterTemplate>
			</ZL:ExRepeater>
			</asp:Panel>
			<asp:Panel ID="Panel2" runat="server">
			<table width="100%" border="0" cellpadding="0" cellspacing="0" style="word-break:break-all">
						<tr>
							<td align="center" class="style1" style="text-align: left">
							   
								强烈建议您上传一张形象照</td>
						</tr>
						<tr>
							<td align="center" class="style1" style="text-align: left">
								*照相必须为BMP,JPG,PNG或GIF格式</td>
						</tr>
						<tr>
							<td align="center" class="style1" style="text-align:center">
								形象照片</td>
						</tr>
						<tr>
							<td align="center" class="style2" style="text-align: center">
								
								<asp:Image ID="ShowUserFace" runat="server" />
								<input id="UserFaceddd" name="UserFaceddd" type="hidden" />
								</td>
						</tr>
						<tr>
							<td align="center" class="style1" style="text-align: center">
	
							</td>
						</tr>
						<tr>
							<td align="center" class="style1" style="text-align: center">
								<asp:Button ID="Button1" runat="server" Text="提交" Width="88px" 
									onclick="Button1_Click" /></td>
						</tr>
					
					</table>
			</asp:Panel>
			
			
		</td>
	</tr>
</table>
</div>
</div>
</asp:Content>