<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="FollowList.aspx.cs" Inherits="User_UserFriend_FollowList" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>好友管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container margin_t5">
     <div id="pageflag" data-nav="index" data-ban="zone"></div>
	<ol class="breadcrumb">
        <li><a href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Info/UserInfo.aspx">用户信息</a></li>
        <li class="active"><a href="FollowList.aspx">我的关注</a>|<a href="FollowList.aspx?type=1">关注我的</a></li> 
    </ol>
</div>
<div class="container btn_green margin_t5">
    <ul class="nav nav-tabs" role="tablist">
        <li data-type="0" class=''><a href="FollowList.aspx">我的关注</a></li>
        <li data-type="1" class=''><a href="?type=1">关注我的</a></li>
    </ul>
    <div class="margin_t5 input-group text_300" style="margin-bottom:5px;">
        <asp:TextBox ID="Skey_T" CssClass="form-control" runat="server" placeholder="用户名称"></asp:TextBox>
        <span class="input-group-btn">
            <asp:Button ID="Skey_Btn" runat="server" CssClass="btn btn-primary" Text="搜索" OnClick="Skey_Btn_Click" />
        </span>
    </div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate><input type="checkbox" value="<%#Eval("ID") %>" name="idchk" /></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="头像">
                <ItemTemplate>
                    <img src="<%#Eval("salt") %>" onerror="shownoface(this);" class="img50" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="用户名" DataField="UserName" />
            <asp:BoundField HeaderText="昵称" DataField="HoneyName" />
            <asp:BoundField HeaderText="关注日期" DataField="CDate" />
            <asp:TemplateField  HeaderText="操作" ItemStyle-CssClass="td_l">
                <ItemTemplate>
                    <asp:LinkButton runat="server" ToolTip="取消关注" CommandArgument='<%#Eval("TUserID") %>' CommandName="del" CssClass="option_style" OnClientClick="return confirm('确定取消关注吗?');"><i class="fa fa-recycle"></i>取消关注</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button ID="Dels_Btn" runat="server" CssClass="btn btn-primary" Text="批量取消关注" OnClick="Dels_Btn_Click" OnClientClick="return confirm('是否取消关注选中用户!')" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    function CheckType(id) {
        $("[data-type='" + id + "']").addClass('active');
    }
</script>
</asp:Content>
