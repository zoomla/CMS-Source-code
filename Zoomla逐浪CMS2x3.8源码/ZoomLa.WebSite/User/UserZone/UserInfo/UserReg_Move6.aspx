<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="UserReg_Move6.aspx.cs" Inherits="User_UserInfo_UserReg_Move6" EnableViewState="false" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc2" %>
<%@ Register Src="WebUserControlUserInfoTop.ascx" TagName="WebUserControlUserInfoTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>个人信息管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li class='active'>联系方式</li>
    </ol>
</div> 
<div class="container btn_green"> 
    <uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
    <uc1:WebUserControlUserInfoTop ID="WebUserControlUserInfoTop1" runat="server" />
</div> 
<div class="container btn_green"> 
    <div style="overflow: hidden;">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td width="17%" align="right">联系地址： </td>
                <td width="41%">
                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control text_md" style="max-width:300px;"></asp:TextBox></td>
                <td width="42%"></td>
            </tr>
            <tr>
                <td align="right">邮政编码： </td>
                <td>
                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control text_md"></asp:TextBox></td>
                <td>
                    <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtZipCode" ErrorMessage="请输入正确的邮政编码" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="right">办公室电话： </td>
                <td>
                    <asp:TextBox ID="txtOfficePhone" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">家用电话： </td>
                <td>
                    <asp:TextBox ID="txtHomePhone" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">传真： </td>
                <td>
                    <asp:TextBox ID="txtFax" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">小灵通： </td>
                <td>
                    <asp:TextBox ID="txtPHS" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">移动电话： </td>
                <td>
                    <asp:TextBox ID="txtMobile" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">QQ： </td>
                <td>
                    <asp:TextBox ID="txtQQ" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                <td>
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtQQ" ErrorMessage="请输入正确的QQ号" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td align="right">MSN： </td>
                <td>
                    <asp:TextBox ID="txtMSN" CssClass="form-control text_md" runat="server"></asp:TextBox>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="right">ICQ： </td>
                <td>
                    <asp:TextBox ID="txtICQ" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">UC： </td>
                <td>
                    <asp:TextBox ID="txtUC" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                <td>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtUC" ErrorMessage="请输入正确的UC号" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></td>
            </tr>
            <tr>
                <td align="right">YaHoo： </td>
                <td>
                    <asp:TextBox ID="txtYaHoo" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">个人主页： </td>
                <td>
                    <asp:TextBox ID="txtHomePage" runat="server" CssClass="form-control text_md" TextMode="MultiLine" Columns="2" Width="300px"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="nextButton" CssClass="btn btn-primary" runat="server" Text="提交" OnClick="nextButton_Click" /></td>
                <td></td>
            </tr>
        </table>
    </div>
	</div> 
</asp:Content>
