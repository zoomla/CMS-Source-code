<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="UserReg_Move5.aspx.cs" Inherits="UserReg_Move5" EnableViewState="false" %>

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
        <li class='active'>我的生活状态</li>
    </ol>
</div> 
<div class="container btn_green"> 
        <uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
        <uc1:WebUserControlUserInfoTop ID="WebUserControlUserInfoTop1" runat="server" />
    </div>
</div> 
<div class="container btn_green"> 
    <div class="us_topinfo" style="overflow: hidden;">
        <table class="table table-bordered table-hover table-striped">
            <tr>
                <td align="right" width="17%">是否吸烟： </td>
                <td width="41%">
                    <asp:DropDownList ID="SmokeDropDownList" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td width="42%"></td>
            </tr>
            <tr>
                <td align="right">是否喝酒： </td>
                <td>
                    <asp:DropDownList ID="DrinkDropDownList" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right" style="height: 22px">职业类别： </td>
                <td style="height: 22px">
                    <asp:DropDownList ID="ddlWork" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td style="height: 22px"></td>
            </tr>
            <tr>
                <td align="right" style="height: 22px">是否购车： </td>
                <td style="height: 22px">
                    <asp:DropDownList ID="CarDropDownList" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td style="height: 22px"></td>
            </tr>
            <tr>
                <td align="right">公司类别： </td>
                <td>
                    <asp:TextBox ID="Comtxt" runat="server" CssClass="form-control text_md"></asp:TextBox></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">是否想要孩子： </td>
                <td>
                    <asp:DropDownList ID="NeedchildDropDownList" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">我的内心独白： </td>
                <td colspan="2" valign="top">
                    <textarea id="TextArea1" runat="server" class="form-control text_md" style="height: 89px"></textarea></td>
            </tr>
            <tr>
                <td align="right">我的爱情宣言： </td>
                <td valign="top">
                    <textarea id="Textarea2" runat="server" class="form-control text_md" style="height: 89px"></textarea></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Textarea2"
                        ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
            </tr>
            <tr>
                <td style="width: 158px; height: 24px;"></td>
                <td colspan="2" >
                    <asp:Button ID="nextButton" CssClass="btn btn-primary" runat="server" Text="提交" OnClick="nextButton_Click" /></td>
            </tr>
        </table>
    </div>
	</div> 
</asp:Content>
