<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="User_UserShop_Default" ClientIDMode="Static" ValidateRequest="false" %>

<%@ Register Src="WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>店铺主页</title>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="store"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="ProductList.aspx">我的店铺</a></li>
        <li class="active">店铺主页
            <asp:Label runat="server" ID="StoreUrl_L"></asp:Label></li>
    </ol>
</div>
<div class="container">
    <div class="u_shop_body">
        <div class="btn_green">
            <uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
        </div>
        <table class="table table-striped table-bordered table-hover btn_green" style="margin-top: 10px;">
            <tr>
                <td class="text-center">商铺名称</td>
                <td>
                    <asp:TextBox ID="StoreName_T" runat="server"  CssClass="form-control text_md"></asp:TextBox>
                    <span class="r_red">*</span>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="Nametxt">名称必填</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="text-center">商铺信用积分</td>
                <td>
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="text-center">商铺状态</td>
                <td>
                    <asp:Label ID="Label4" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-center">商铺类型</td>
                <td>
                    <asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="text-center">商铺风格模板</td>
                <td>
                    <asp:DropDownList ID="SSTDownList" CssClass="form-control text_md" runat="server" AutoPostBack="True" OnSelectedIndexChanged="SSTDownList_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="text-center">模板效果预览</td>
                <td>
                    <asp:Image ID="Image1" runat="server" Height="100px" Width="150px" />
                </td>
            </tr>
            <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
            <tr>
                <td colspan="2" class="text-center">
                    <asp:Button ID="EBtnSubmit" Text="信息提交" CssClass="btn btn-primary" runat="server" OnClick="EBtnSubmit_Click" />
                    <input id="Button1" type="button" value="返回" class="btn btn-primary" onclick="javascript: history.go(-1)" />
                </td>
            </tr>
        </table>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/Common.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/ICMS/ZL_Common.js"></script>
<script>
    function opentitle(url, title) {
        comdiag.reload = true;
        comdiag.maxbtn = false;
        ShowComDiag(url, title);
    }
    function closdlg() {
        CloseComDiag();
    }
</script>
</asp:Content>
