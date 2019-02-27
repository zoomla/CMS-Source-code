<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DbCardActivate.aspx.cs" Inherits="User_Info_DbCardActivate" MasterPageFile="~/User/Default.master" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>双卡激活</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="shop" data-ban="shop"></div>
    <div class="container margin_t5">
    <ol class="breadcrumb">
    <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
    <li><a href="UserInfo.aspx">账户管理</a></li><li class="active">双卡激活</li>
    </ol>
    </div>
    <div class="container btn_green">
    <ul class="nav nav-tabs">
	    <li><a href="UserInfo.aspx">注册信息</a></li>
	    <li><a href="UserBase.aspx">基本信息</a></li>
	    <li><a href="UserBase.aspx?sel=Tabs1">头像设置</a></li>
	    <li><a href="DredgeVip.aspx">VIP信息</a></li>
        <li class="active"><a href="DbCardActivate.aspx">双卡激活</a></li>
    </ul>
    <div class="panel panel-info text-center margin_t10">
    <div class="panel-heading"><i class="fa  fa-pencil-square-o"></i> 输入卡号</div>
    <div class="panel-body">
        <div style="border-bottom:1px dashed #ddd;padding-bottom:5px;">
            <span>卡 一：</span>
            <asp:TextBox ID="Card1_T" runat="server" CssClass="form-control text_md" />
            <asp:RequiredFieldValidator runat="server" ID="R1" ValidationGroup="activate" ControlToValidate="Card1_T" ForeColor="Red" ErrorMessage="卡号不能为空" Display="Dynamic" />
        </div>
        <div class=" margin_t5">
            <span>卡 二：</span>
            <asp:TextBox ID="Card2_T" runat="server" CssClass="form-control text_md" />
            <asp:RequiredFieldValidator runat="server" ID="R2" ValidationGroup="activate" ControlToValidate="Card2_T" ForeColor="Red" ErrorMessage="卡号不能为空" Display="Dynamic" />
        </div>
    </div>
    <div class="panel-footer"><asp:Button ID="Activate_B" runat="server" ValidationGroup="activate" OnClick="Activate_B_Click" Text="激活" CssClass="btn btn-primary" /></div>
    </div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" CssClass="table table-striped table-bordered table-hover" Visible="false"
        RowStyle-CssClass="tdbg" OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="true" >
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="Card_ID" />
            <asp:BoundField HeaderText="卡号" DataField="CardNum" />
            <asp:TemplateField HeaderText="发放用户">
                <ItemTemplate><%#GetUserName(DataBinder.Eval(Container.DataItem ,"ActivateUserID").ToString()) %></ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="卡片创建时间" DataField="AddTime" />
            <asp:BoundField HeaderText="激活时间" DataField="StartTime" />
        </Columns>
    </ZL:ExGridView>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
.allchk_l{display:none;}
</style>
</asp:Content>
