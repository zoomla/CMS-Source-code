<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="StoreApply.aspx.cs" Inherits="User_UserShop_StoreApply" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的店铺</title>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="store"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="ProductList.aspx">我的店铺</a></li>
        <li class="active">店铺申请</li> 
    </ol>
</div>
<div class="container btn_green">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" class="text-center">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-center">商铺名称</td>
            <td>
                <asp:TextBox ID="Nametxt" runat="server" Text='' CssClass="form-control m715-50"></asp:TextBox>
                <span><font color="red">*</font></span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="Nametxt">名称必填</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="text-center">商铺类型</td>
            <td>
                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control m715-50"  OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                <span><font color="red">*</font></span>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="DropDownList1">类型必填</asp:RequiredFieldValidator>
            </td>
        </tr>
        <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
        <tr>
            <td colspan="2" class="text-center">
                <asp:Button ID="EBtnSubmit" Text="信息提交" runat="server" CssClass="btn btn-primary" OnClick="EBtnSubmit_Click" />
                <input id="Button1" type="button" value="返回 " class="btn btn-primary" onclick="javascript: history.go(-1)" />
            </td>
        </tr>
    </table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/js/Common.js"></script>
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script src="/js/ZL_Content.js"></script>
</asp:Content>
