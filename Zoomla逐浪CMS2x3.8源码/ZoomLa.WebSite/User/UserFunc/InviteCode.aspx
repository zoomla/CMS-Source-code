<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InviteCode.aspx.cs" Inherits="User_UserFunc_InviteCode" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>我的邀请码</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="InviteCode"></div>
<div class="container margin_t5">
      <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">邀请码管理<a href="javascript:;" style="margin-left:5px;"></a></li>
          <asp:LinkButton runat="server" ID="Create_Link" Text="[生成邀请码]" style="margin-left:5px;" OnClick="Create_Link_Click" OnClientClick="return confirm('确定要生成邀请码!');" ></asp:LinkButton>
    </ol>
</div>
<div class="container">
    <div class="input-group nav_searchDiv pull-left text_300">
        <asp:TextBox runat="server" ID="searchText" class="form-control" placeholder="请输入需要搜索的邀请码" />
        <span class="input-group-btn">
            <asp:LinkButton runat="server" CssClass="btn btn-default" ID="searchBtn" OnClick="searchBtn_Click"><span class="fa fa-search"></span></asp:LinkButton>
        </span>
    </div>
</div>
    <div class="container">
    <div class="alert alert-info">您当前有<asp:Label runat="server" ID="CodeCount_L" />个有效的邀请码,最多拥有<%:maxcount %>个</div>
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
        class="table table-striped table-bordered table-hover" EmptyDataText="当前没有邀请码!!"
        OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:BoundField HeaderText="邀请码" DataField="Str1" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%#CodeStatus() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="日期" DataField="CDate" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="Del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要移除吗!');" ToolTip="移除">
                              <span class="fa fa-trash-o" style="color:#7D98A1;"></span></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle Height="24px" HorizontalAlign="Center" />
    </ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>