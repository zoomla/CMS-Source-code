<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="DredgeVip.aspx.cs" Inherits="User_Info_DredgeVip" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>VIP信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="shop"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li><a href="UserInfo.aspx">账户管理</a></li><li class="active">VIP信息</li>
</ol>
</div>
<div class="container btn_green">
<ul class="nav nav-tabs">
	<li><a href="UserInfo.aspx">注册信息</a></li>
	<li><a href="UserBase.aspx">基本信息</a></li>
	<li><a href="UserBase.aspx?sel=Tabs1">头像设置</a></li>
	<li class="active"><a href="DredgeVip.aspx">VIP卡</a></li>
    <li><a href="DbCardActivate.aspx">双卡激活</a></li>
</ul>
<div class="text-center">
<asp:MultiView ID="MultiView1" runat="server">
<asp:View ID="View1" runat="server">
    <div class="panel panel-info margin_t10">
        <div class="panel-heading"><i class="fa fa-credit-card-alt"></i> VIP卡</div>
        <div class="panel-body">
            <div style="border-bottom:1px dashed #ddd;padding-bottom:5px;">
                <span>VIP号</span>
                <asp:TextBox ID="txtVIP" runat="server" CssClass="form-control text_md" />
                <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="txtVIP" ForeColor="Red" ErrorMessage="VIP卡号不能为空" Display="Dynamic" />
            </div>
            <div class=" margin_t5">
                <span>密 码</span>
                <asp:TextBox ID="txtPas" runat="server" TextMode="Password" CssClass="form-control text_md" />
                <asp:RequiredFieldValidator runat="server" ID="R2" ControlToValidate="txtVIP" ForeColor="Red" ErrorMessage="VIP密码不能为空" Display="Dynamic" />
            </div>
        </div>
        <div class="panel-footer"><asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="开通VIP" CssClass="btn btn-primary" /></div>
    </div>
</asp:View>
<asp:View ID="View3" runat="server">
	你的VIP卡已开通号：&nbsp;
	<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
	<br />
	<br />
	<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
</asp:View>
<asp:View ID="View2" runat="server">
	<table class="table table-striped table-bordered table-hover">
		<tr>
			<td width="10%" class="title"></td>
			<td width="15%" class="title">卡号 </td>
			<td width="20%" class="title">发放用户 </td>
			<td width="10%" class="title">使用用户 </td>
			<td width="20%" class="title">卡片状态 </td>
			<td width="10%" class="title">操作 </td>
		</tr>
		<asp:Repeater ID="Card_RPT" runat="server">
			<ItemTemplate>
				<tr>
					<td><input name="idchk" type="checkbox" value='<%# Eval("Card_ID")%>' /></td>
					<td><%# Eval("CardNum")%></td>
					<td><%#GetUserName(DataBinder.Eval(Container.DataItem ,"PutUserID").ToString()) %></td>
					<td><%#GetUserName(DataBinder.Eval(Container.DataItem ,"AssociateUserID").ToString()) %></td>
					<td><%#GetState(DataBinder.Eval(Container.DataItem, "CardState").ToString())%>
					<td><a href="CardView.aspx?id=<%#Eval("Card_ID") %>">查看</a></td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:View>
</asp:MultiView>
</div>
</div>
</asp:Content>