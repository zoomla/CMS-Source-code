<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="UserReg_Move2.aspx.cs" Inherits="UserReg_Move2" EnableViewState="false" %>
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
        <li class='active'>我的择偶条件</li>
    </ol>

    <div class="container us_topinfo" style="width: 98%">
         <a title="会员中心" target="_parent" href='<%=ResolveUrl("~/User/Default.aspx")%>'>会员中心</a>&gt;&gt;我的择偶条件 
    </div>
</div> 
<div class="container btn_green"> 
    <uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
    <uc1:WebUserControlUserInfoTop ID="WebUserControlUserInfoTop1" runat="server" />
</div> 
<div class="container btn_green">
    <div class="us_topinfo" style="overflow: hidden;">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td align="right" valign="middle">工作地区： </td>
                <td valign="top">
                    <table>
                        <tr>
                            <td>省： </td>
                            <td>
                                <asp:DropDownList ID="DropDownList3" CssClass="form-control text_md" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList3_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>市： </td>
                            <td>
                                <asp:DropDownList ID="DropDownList4" CssClass="form-control text_md" runat="server" Visible="false"></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td></td>
            </tr>
            <tr>
                <td align="right">婚姻状况： </td>
                <td>
                    <asp:DropDownList ID="marryDropDownList" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">身高： </td>
                <td>
                    <asp:TextBox ID="Staturetxt" runat="server" CssClass="form-control text_md" Style="max-width: 50px;" Text="不限"></asp:TextBox>Cm
                </td>
                <td>
                    <span id="YourPosition0">
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="Staturetxt" ErrorMessage="请输入正确的身高信息" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator>
                    </span>
                </td>
            </tr>
            <tr>
                <td align="right">体重： </td>
                <td>
                    <asp:TextBox ID="Avoirtxt" runat="server" CssClass="form-control text_md" Style="max-width: 50px;" Text="不限"></asp:TextBox>Kg</td>
                <td>
                    <span id="YourPosition1">
                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="Avoirtxt" ErrorMessage="请输入正确的体重信息" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></span>
                </td>
            </tr>
            <tr>
                <td align="right">学历： </td>
                <td>
                    <asp:DropDownList ID="BachelorDropDownList" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">月收入： </td>
                <td>
                    <asp:DropDownList ID="monthDropDownList" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">住房条件： </td>
                <td>
                    <asp:DropDownList ID="homeDropDownList" Width="110px" runat="server"></asp:DropDownList></td>
                <td></td>
            </tr>
            <tr>
                <td align="right">其他条件： </td>
                <td>
                    <textarea id="TextArea1" class="form-control text_md" style="max-width: 236px; height: 64px" runat="server"></textarea>
                </td>
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
