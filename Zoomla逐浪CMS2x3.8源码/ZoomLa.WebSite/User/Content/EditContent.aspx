<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="EditContent.aspx.cs" Inherits="ZoomLa.WebSite.User.Content.EditContentpage" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<title>投稿管理</title>
<style>
.tdbgleft{text-align:right;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="content" data-ban="cnt"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
	<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
	<li><a href="Mycontent.aspx?NodeID=<%= NodeID%>">投稿管理</a></li>
	<li class="active"><asp:Label runat="server" ID="NodeName_L"></asp:Label></li>
</ol>
</div>
    <div class="container btn_green u_cnt">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="text-right">所属节点</td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Label2"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right"><asp:Label ID="Title_L" runat="server">内容标题</asp:Label></td>
            <td>
                <asp:TextBox ID="txtTitle" CssClass="form-control m715-50" runat="server" Text=''></asp:TextBox>
                <span><font color="red">*</font></span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtTitle">内容标题必填</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="text-right">用户分类</td>
            <td>
                <asp:DropDownList CssClass="form-control" Width="150" ID="DropDownList1" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="text-right">关键字</td>
            <td>
                <asp:TextBox ID="TxtTagKey" CssClass="form-control m715-50" runat="server" Text=''></asp:TextBox>
                关键字请用“,”隔开<span style="color: #0000ff">[</span>
                <a href="#" onclick="SelectKey();"><span style="text-decoration: underline; color: Green;">选择关键字</span></a>
                <span style="color: #0000ff">]</span>
            </td>
        </tr>
        <tr>
            <td class="text-right">
                <asp:Label ID="Label3" runat="server" Text="发布悬赏"></asp:Label></td>
            <td>
                <asp:DropDownList ID="BidType" CssClass="form-control" Width="100" runat="server" onchange="changevalue(this.value);">
                    <asp:ListItem Value="0">选择方式</asp:ListItem>
                    <asp:ListItem Value="1">单人中标</asp:ListItem>
                    <asp:ListItem Value="2">记件工资</asp:ListItem>
                </asp:DropDownList>
                <div id="changeins" style="display: none">
                    我想要
                    <asp:TextBox ID="pronum" Width="30px" runat="server" class="l_input">0</asp:TextBox>
                    个方案，每个
                    <asp:TextBox ID="bidmoney" Width="30px" runat="server" class="l_input">0</asp:TextBox>
                    元
                </div>
            </td>
        </tr>
        <tr>
            <td class="text-right">
                <asp:Label ID="Label5" runat="server" Text="项目完成周期"></asp:Label></td>
            <td>
                <asp:TextBox ID="proweek" runat="server" CssClass="form-control" style="max-width:80px">0</asp:TextBox>
                天 
            </td>
        </tr>
        <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
        <tr>
            <td colspan="2" class="text-center">
                <asp:HiddenField ID="HdnItem" runat="server" />
                <asp:TextBox ID="FilePicPath" CssClass="form-control" runat="server" Text="fbangd" Style="display: none" />
                <asp:Button ID="EBtnSubmit" CssClass="btn btn-primary" Text="修改" OnClick="EBtnSubmit_Click" runat="server" />
                <asp:Button ID="BtnBack" CssClass="btn btn-primary" runat="server" Text="返回列表" OnClick="BtnBack_Click" UseSubmitBehavior="False" CausesValidation="False" />
            </td>
        </tr>
    </table>
</div>
    <ul>
        <li style="width: 70%; float: left; line-height: 30px; text-align: right">
            
        </li>
        <li style="width: 20%; line-height: 30px"></li>
    </ul>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript"  src="/JS/Common.js"></script>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript"  src="/JS/ZL_Content.js"></script>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>