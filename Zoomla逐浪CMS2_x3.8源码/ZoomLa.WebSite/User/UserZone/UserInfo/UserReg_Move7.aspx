<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="UserReg_Move7.aspx.cs" Inherits="User_UserInfo_UserReg_Move7" EnableViewState="false" %>
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
        <li class='active'>我的爱好</li>
    </ol>
</div> 
<div class="container btn_green"> 
        <uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
        <uc1:WebUserControlUserInfoTop ID="WebUserControlUserInfoTop1" runat="server" />
</div> 
<div class="container btn_green"> 
    <div class="us_topinfo" style="overflow: hidden;">
        <table class="table table-bordered table-striped table-hover">
            <tr>
                <td valign="top" align="right">是否愿意与父母同住： </td>
                <td colspan="2" valign="top">
                    <asp:DropDownList ID="LiveSamep" CssClass="form-control text_md" runat="server">
                        <asp:ListItem Text="愿意" Value="0" />
                        <asp:ListItem Text="欢迎常来小住" Value="1" />
                        <asp:ListItem Text="视融洽程度" Value="2" />
                        <asp:ListItem Text="不愿意" Value="3" />
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td valign="top" align="right">锻炼频次： </td>
                <td colspan="2" valign="top">
                    <asp:DropDownList ID="DuanL" CssClass="form-control text_md" runat="server">
                        <asp:ListItem Text="每天" Value="0" />
                        <asp:ListItem Text="每周" Value="1" />
                        <asp:ListItem Text="每月" Value="2" />
                        <asp:ListItem Text="很少" Value="3" />
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td valign="top" align="right">作息习惯： </td>
                <td colspan="2" valign="top">
                    <asp:DropDownList ID="Zuoxi" CssClass="form-control text_md" runat="server">
                        <asp:ListItem Text="早睡早起" Value="0" />
                        <asp:ListItem Text="较有规律" Value="1" />
                        <asp:ListItem Text="夜猫子" Value="2" />
                        <asp:ListItem Text="偶尔懒散随意" Value="3" />
                        <asp:ListItem Text="作息不定" Value="4" />
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td valign="top" align="right">个性自评： </td>
                <td colspan="2" valign="top">
                    <asp:CheckBoxList ID="Self" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td valign="top" align="right">兴趣爱好： </td>
                <td colspan="2" valign="top">
                    <asp:CheckBoxList ID="Love" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td valign="top" align="right">喜欢的活动： </td>
                <td colspan="2" valign="top">
                    <asp:CheckBoxList ID="acctiveCheckBoxList" runat="server" RepeatColumns="8" RepeatDirection="Horizontal"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td valign="top" align="right">喜欢的体育运动： </td>
                <td colspan="2" valign="top">
                    <asp:CheckBoxList ID="AthCheckBoxList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td valign="top" align="right">喜欢的音乐： </td>
                <td colspan="2" valign="top">
                    <asp:CheckBoxList ID="MusicCheckBoxList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td valign="top" align="right">喜欢的影视节目/书： </td>
                <td colspan="2" valign="top">
                    <asp:CheckBoxList ID="MovieCheckBoxList" runat="server" RepeatColumns="4" RepeatDirection="Horizontal"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td valign="top" align="right">喜欢的食物： </td>
                <td colspan="2" valign="top">
                    <asp:CheckBoxList ID="FoodCheckBoxList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td valign="top" align="right">宠物： </td>
                <td colspan="2" valign="top">
                    <asp:CheckBoxList ID="Pet" runat="server" RepeatColumns="8" RepeatDirection="Horizontal"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td valign="top" align="right">擅长的生活技能： </td>
                <td colspan="2" valign="top">
                    <asp:CheckBoxList ID="LifeCan" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td valign="top" align="right">休闲去处： </td>
                <td colspan="2" valign="top">
                    <asp:CheckBoxList ID="AreaCheckBoxList" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"></asp:CheckBoxList></td>
            </tr>
            <tr>
                <td style="width: 158px"></td>
                <td colspan="2">
                    <asp:Label ID="errLabel" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 158px; height: 24px;"></td>
                <td colspan="2" style="height: 24px">
                    <asp:Button ID="nextButton" CssClass="btn btn-primary" runat="server" Text="提交" OnClick="nextButton_Click" /></td>
            </tr>
        </table>
    </div>
	</div> 
</asp:Content>
