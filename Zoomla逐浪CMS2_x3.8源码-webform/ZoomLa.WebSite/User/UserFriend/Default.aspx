<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="User_UserFriend_Default" %>
<%@ Register Src="../UserZone/WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<%@ Register Src="WebUserControlMessage.ascx" TagName="WebUserControlMessage" TagPrefix="uc1" %>
<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc3" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>好友管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">我的好友</li> 
        <li><a href="/User/UserZone/UserQuestFriend.aspx">[<i class="fa fa-user-plus"></i>添加好友]</a></li>
    </ol>
</div>
<div class="container btn_green">
    <uc2:webusercontroltop ID="WebUserControlTop2" runat="server" />
<%--    <uc3:webusercontroltop ID="WebUserControlTop1" runat="server" />--%>
<%--    <uc1:webusercontrolmessage ID="WebUserControlMessage1" runat="server" />--%>
</div>
<div class="container btn_green margin_t5">
    <div class="margin_t5 input-group text_300" style="margin-bottom:5px;">
        <asp:TextBox ID="UNameSkey_T" CssClass="form-control" runat="server" placeholder="好友名称"></asp:TextBox>
        <span class="input-group-btn">
            <asp:Button ID="UNameSkey_Btn" runat="server" CssClass="btn btn-primary" Text="搜索" OnClick="UNameSkey_Btn_Click" />
        </span>
    </div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="尚无好友数据">
        <Columns>
            <asp:TemplateField HeaderText="头像">
                <ItemTemplate>
                    <img src="<%#Eval("salt") %>" onerror="shownoface(this);" class="img50" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="用户名" DataField="UserName" />
            <asp:BoundField HeaderText="昵称" DataField="HoneyName" />
            <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="td_l">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ToolTip="删除好友" CommandArgument='<%#Eval("UserID") %>' CommandName="del2" CssClass="option_style" OnClientClick="return confirm('确定删除吗?');"><i class="fa fa-recycle"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">.allchk_l {display:none; }</style>
</asp:Content>
