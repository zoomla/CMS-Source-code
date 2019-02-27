<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FriendApply.aspx.cs" Inherits="User_UserZone_FriendApply" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>好友申请</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">搜索结果 [<a href="/User/UserFriend/Default.aspx">我的好友</a>]</li>
    </ol>
</div>
<div class="container btn_green">
    <ul id="action_ul" class="nav nav-tabs">
        <li data-action="rece"><a href="FriendApply.aspx?action=rece">我收到的申请</a></li>
        <li data-action="send"><a href="FriendApply.aspx?action=send">我发送的申请</a></li>
    </ul>
    <ZL:ExGridView ID="Rece_EGV" Visible="false" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
            OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
            CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无待处理好友申请">
            <Columns>
                <asp:TemplateField HeaderText="头像">
                    <ItemTemplate>
                        <img src="<%#Eval("salt") %>" onerror="shownoface(this);" class="img50" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="用户名" DataField="UserName" />
                <asp:BoundField HeaderText="昵称" DataField="HoneyName" />
                <asp:BoundField HeaderText="备注" DataField="Remind"></asp:BoundField>
                <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="td_l">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandName="agree" CommandArgument='<%#Eval("ID") %>' CssClass="option_style" ToolTip="同意"><i class="fa fa-user-plus"></i>同意</asp:LinkButton>
                        <asp:LinkButton runat="server" CommandName="reject" CommandArgument='<%#Eval("ID") %>' CssClass="option_style" ToolTip="拒绝"><i class="fa fa-recycle"></i>拒绝</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    <ZL:ExGridView ID="Send_EGV" Visible="false" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false"
            OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
            CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无申请数据">
            <Columns>
                <asp:TemplateField HeaderText="头像">
                    <ItemTemplate>
                        <img src="<%#Eval("salt") %>" onerror="shownoface(this);" class="img50" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="用户名" DataField="UserName" />
                <asp:BoundField HeaderText="昵称" DataField="HoneyName" />
                <asp:BoundField HeaderText="备注" DataField="Remind"></asp:BoundField>
                <asp:TemplateField HeaderText="状态" ItemStyle-CssClass="td_m">
                   <ItemTemplate>
                      <%#GetStatus() %>
                   </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/ZL_Regex.js"></script>
    <script>
        $(function () {
            var menu = "<%:Request.QueryString["action"]%>";
            if (ZL_Regex.isEmpty(menu)) { menu = "rece"; }
            $(".nav li[data-action=" + menu + "]").addClass("active");
        })
    </script>
</asp:Content>