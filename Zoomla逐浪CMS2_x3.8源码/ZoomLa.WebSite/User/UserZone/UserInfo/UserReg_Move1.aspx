<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="UserReg_Move1.aspx.cs" Inherits="User_UserInfo_UserReg_Move1" EnableViewState="false" %>
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
        <li class='active'>个人信息管理</li>
    </ol>
</div> 
<div class="container btn_green"> 
    <uc2:WebUserControlTop ID="WebUserControlTop1" runat="server" />
    <uc1:WebUserControlUserInfoTop ID="WebUserControlUserInfoTop1" runat="server" />
</div> 
<div class="container btn_green"> 
    <div class="us_topinfo" style="overflow: hidden;">
        <table class="table table-striped table-bordered table-hover">
            <tr valign="top">
                <td width="17%" align="right">用户名： </td>
                <td width="41%">
                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label></td>
                <td width="42%"></td>
            </tr>
            <tr valign="top">
                <td width="17%" align="right">昵 称： </td>
                <td width="41%">
                    <asp:TextBox ID="lblhoney" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" style="height: 26px">真实姓名： </td>
                <td style="height: 26px">
                    <asp:TextBox ID="txtrealname" CssClass="form-control text_md" runat="server"></asp:TextBox></td>
                <td style="height: 26px"></td>
            </tr>
            <tr>
                <td align="right" style="height: 24px">性 别： </td>
                <td style="height: 24px">
                    <asp:Label ID="lblSex" runat="server" Text=""></asp:Label></td>
                <td style="height: 24px"></td>
            </tr>
            <tr>
                <td align="right">出生日期： </td>
                <td>
                    <asp:TextBox ID="txtbir" runat="server" CssClass="form-control text_md" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' });" ></asp:TextBox>
                    <br />如：2008-08-08
                    生肖：
                    <asp:Label ID="shengxiao" runat="server"><%=GetTree(Convert.ToDateTime(txtbir.Text))%></asp:Label>
                    &nbsp
                    星座：
                    <asp:Label ID="xingzuo" runat="server" Text=""><%=GetConstellation(Convert.ToDateTime(txtbir.Text))%></asp:Label></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtbir" ErrorMessage="出生日期不能为空"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="请输入正确的出生日期" ValidationExpression="^\d{4}-\d{2}-\d{2}$" ControlToValidate="txtbir"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td align="right" style="height: 22px">身份证号码： </td>
                <td style="height: 22px">
                    <asp:DropDownList ID="CardType" CssClass="form-control text_md" runat="server">
                        <asp:ListItem Value="身份证" />
                        <asp:ListItem Value="军官证" />
                        <asp:ListItem Value="护照" />
                    </asp:DropDownList>
                    <asp:TextBox ID="txtNum" runat="server" CssClass="form-control text_md"></asp:TextBox></td>
                <td style="height: 22px"></td>
            </tr>
            <tr>
                <td align="right">户籍所在地省： </td>
                <td style="height: 22px">
                    <asp:DropDownList ID="ddlProvince" CssClass="form-control text_md" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="height: 22px">
                    市： 
                    <asp:DropDownList ID="ddlCity" CssClass="form-control text_md" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right">对他人隐藏信息： </td>
                <td colspan="2">
                    <asp:RadioButtonList ID="RadioButtonList1" name="RadioButtonList1" runat="server"
                        RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Value="0">是</asp:ListItem>
                        <asp:ListItem Value="1">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="height: 19px"></td>
                <td colspan="2" style="height: 19px">
                    <asp:Label ID="Label1" runat="server" Text="其他所有信息填写完整可获得积分哦" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td colspan="2">
                    <asp:Button ID="nextButton" CssClass="btn btn-primary" runat="server" Text="提交" OnClick="nextButton_Click" /></td>
            </tr>
        </table>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
