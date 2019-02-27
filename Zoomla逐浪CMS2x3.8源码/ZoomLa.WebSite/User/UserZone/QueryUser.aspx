<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QueryUser.aspx.cs" Inherits="User_UserZone_QueryUser" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>搜索用户</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">搜索结果 [<a href="/User/UserFriend/Default.aspx">我的好友</a>]</li>
    </ol>
</div>
<div class="container btn_green">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="未搜索到符合条件的用户">
        <Columns>
            <asp:TemplateField HeaderText="头像">
                <ItemTemplate>
                      <img src="<%#Eval("salt") %>" onerror="shownoface(this);" class="img50" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="用户名" DataField="UserName" />
            <asp:BoundField HeaderText="昵称" DataField="HoneyName" />
            <asp:TemplateField HeaderText="性别">
                <ItemTemplate>
                    <%#Eval("UserSex","").Equals("0")?"女":"男" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a class="option_style" href="javascript:;" onclick="addFriend(this,<%#Eval("UserID") %>);" title="发送申请"><i class="fa fa-user-plus"></i>发送申请</a>
                    <a class="option_style" href="/User/Message/MessageSend.aspx?ToID=<%#Eval("UserID") %>" title="发送信息"><i class="fa fa-envelope"> 发送信息</i></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">.allchk_l {display:none; }</style>
<script src="/JS/Modal/APIResult.js"></script>
<script src="/JS/Controls/B_User.js"></script>
<script>
    function addFriend(obj, uid) {
        var $a = $(obj);
        if ($a.hasClass("disabled")) { return; }
        var tlp = '<i class="fa fa-user-plus"></i> 已 申 请';
        $a.html(tlp);
        $a.addClass("disabled");
        Friend.add(uid, function (model) { if (APIResult.isok(model)) { } });
    }
</script>
</asp:Content>
