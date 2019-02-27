<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="StoreEdit.aspx.cs" Inherits="User_UserShop_StoreEdit" ClientIDMode="Static" ValidateRequest="false" %>
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
            <li class="active">修改申请信息</li>
        </ol>
    </div>
    <div class="container">
    <div style="width: 100%">
        <div class="text_600 text-center r_gray" style="margin: 0 auto;" id="Auditing" runat="server" visible="false">
            <i class="fa fa-bank margin_t10" style="font-size: 120px;"></i>
            <div class="margin_t10" style="font-size: 18px;">店铺正在审核,请等待管理员开通您的店铺功能!</div>
            <div class="margin_t10"><a href="StoreApply.aspx" class="btn btn-info">修改申请信息</a></div>
        </div>
        <div id="add" runat="server">
            <table class="table table-striped table-bordered">
                <tr>
                    <td colspan="2" class="text-center">
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="text-center">商铺名称</td>
                    <td>
                        <asp:TextBox ID="Nametxt" runat="server" Text="" CssClass="form-control m715-50"></asp:TextBox>
                        <span><font color="red">*</font></span>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="Nametxt">名称必填</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="text-center">商铺类型</td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" CssClass="form-control m715-50" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        <span><font color="red">*</font></span>&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="类型必填" ControlToValidate="DropDownList1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
                <tr>
                    <td colspan="2" class="text-center">
                        <asp:Button ID="EBtnSubmit" CssClass="btn btn-primary" Text="信息提交" runat="server" OnClick="EBtnSubmit_Click" />
                        <input id="Button1" type="button" class="btn btn-primary" value="返  回" onclick="javascript: history.go(-1)" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/Common/Common.js"></script>
    <script src="/js/Common.js"></script>
    <script src="/JS/calendar.js"></script>
    <script src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
