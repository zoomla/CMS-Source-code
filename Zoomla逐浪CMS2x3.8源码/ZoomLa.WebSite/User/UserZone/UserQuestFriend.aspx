<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserQuestFriend.aspx.cs" Inherits="User_Guild_UserQuestFriend" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>搜索好友</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="zone"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li class="active">用户搜索 [<a href="Default.aspx">我的好友</a>]</li>
        </ol>
    </div>
    <div class="container btn_green">
        <table style="width: 569px; height: 175px;" class="table table-hover table-bordered padding0">
            <tr>
                <td colspan="2">
                    <font color="#336699"><strong class="f14">模糊查找</strong></font>
                </td>
            </tr>
            <tr>
                <td class="text-right">性别：
                </td>
                <td>
                    <select id="sex_dp" runat="server" class="form-control text_150">
                        <option value="0">所有</option>
                        <option value="1">男</option>
                        <option value="2">女</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="text-right">年龄范围：
                </td>
                <td>
                    <select class="form-control text_150" name="age" id="age" runat="server">
                        <option value="0">全部</option>
                        <option value="0-18">18以下</option>
                        <option value="18-25">18-25</option>
                        <option value="25-33">25-33</option>
                        <option value="33-45">33-45</option>
                        <option value="45-100">45以上</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="text-right">&nbsp;
                </td>
                <td>
                    <span class="btmbian">
                        <asp:Button ID="btnSecher" runat="server" CssClass="btn btn-primary" Text="查找" OnClick="btnSecher_Click" />
                    </span>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table style="width: 569px; height: 165px;" class="table table-bordered table-hover padding0">
            <tr>
                <td colspan="2">
                    <font color="#336699"><strong>精确查找</strong></font>
                </td>
            </tr>
            <tr>
                <td class="text-right">对方昵称：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="UserName_T" size="40" MaxLength="20" class="form-control text_150" />
                    <asp:Button runat="server" CssClass="btn btn-primary" ID="UserName_Skey_Btn" Text="查找" OnClick="UserName_Skey_Btn_Click" ValidationGroup="UName" />
                    <asp:RequiredFieldValidator runat="server" ID="UR1" ForeColor="Red" ErrorMessage="用户名不能为空" ValidationGroup="UName" Display="Dynamic" ControlToValidate="UserName_T" />
                </td>
            </tr>
            <tr>
                <td class="text-right">对方会员ID号：
                </td>
                <td>
                    <asp:TextBox runat="server" ID="UserID_T" class="form-control text_150" maxlength="10" />
                    <asp:Button ID="UserID_Skey_Btn" runat="server" Text="查找" CssClass="btn btn-primary" OnClick="UserID_Skey_Btn_Click" ValidationGroup="UserID"></asp:Button>
                    <asp:RangeValidator ID="UidR1" runat="server" MaximumValue="99999" MinimumValue="1" Type="Integer" ErrorMessage="请输入数字" ControlToValidate="UserID_T" Display="Dynamic" ValidationGroup="UserID"/>
                    <asp:RequiredFieldValidator runat="server" ID="UidR2" ForeColor="Red" ErrorMessage="用户ID不能为空" ValidationGroup="UserID" ControlToValidate="UserID_T" Display="Dynamic" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
