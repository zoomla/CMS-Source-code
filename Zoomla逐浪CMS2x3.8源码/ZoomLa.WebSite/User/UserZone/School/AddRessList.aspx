<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="AddRessList.aspx.cs" Inherits="User_UserZone_School_AddRessList" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<title>会员中心 >> 我的班级 </title>
<link href="../../../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script src="/JS/DatePicker/WdatePicker.js" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/UserFriend/Default.aspx">我的好友</a></li>
        <li class="active">班级信息</li>
    </ol>
</div> 
<div class="container btn_green"> 
<uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
</div>
<div class="container btn_green u_cnt">
<div class="us_topinfo">
	<div>
		<a href="mySchoolList.aspx">我的班级</a> &gt;&gt; 通讯录列表
		<hr />
	</div>
	<table width="100%" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td>
				<a href="addRess.aspx">添加通讯录</a>
			</td>
		</tr>
		<tr>
			<td id="tdcontext" runat="server">
                 <ZL:ExGridView runat="server" ID="EGV" DataKeyNames="ID" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowDeleting="GridView1_RowDeleting">
					<Columns>
						<asp:TemplateField HeaderText="姓名">
							<ItemTemplate>
								<a href='AddRess.aspx?AID=<%# DataBinder.Eval(Container.DataItem, "ID")%>'>
									<%# Eval("UserName","{0}")%></a>
							</ItemTemplate>
						</asp:TemplateField>

						<asp:TemplateField HeaderText="电话">
							<ItemTemplate>
									<%# Eval("UserPhone", "{0}")%>
							</ItemTemplate>
						</asp:TemplateField>

						<asp:TemplateField HeaderText="QQ">
							<ItemTemplate>
									<%# Eval("UserQQ", "{0}")%>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="MSN">
							<ItemTemplate>
									<%# Eval("UserMSN", "{0}")%>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="E_Mail">
							<ItemTemplate>
									<%# Eval("UserMail", "{0}")%>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="地址">
							<ItemTemplate>
									<%# Eval("UserAdd", "{0}")%>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="简介">
							<ItemTemplate>
								<%#GetContext(Eval("UserContext","{0}"))%>
							</ItemTemplate>
						</asp:TemplateField>
						<asp:CommandField ShowDeleteButton="True" />
					</Columns>
				 <PagerStyle HorizontalAlign="Center" />
                <RowStyle Height="24px" HorizontalAlign="Center"  />
        </ZL:ExGridView>
			</td>
		</tr>
		 
	</table>
</div> 
</div> 
</asp:Content>