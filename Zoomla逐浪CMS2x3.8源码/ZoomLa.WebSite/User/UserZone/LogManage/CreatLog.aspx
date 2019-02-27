<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="CreatLog.aspx.cs" Inherits="User_UserZone_LogManage_CreatLog" ValidateRequest="false" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>发表日志</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="index" data-ban="zone"></div>
<div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li><a href="/User/UserZone/LogManage/SelfLogManage.aspx">我的日志</a></li>
        <li class="active">发表日志</li> 
    </ol>
</div>
    <div class="container btn_green">
        <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc1:WebUserControlTop>
    </div>
<div class="container btn_green">
    <table class="table table-bordered" style="margin-top:10px;">
        <tr>
            <td colspan="2" class=" text-center">
                <span class="fa fa-edit" style="margin-right:5px;"></span>发表新日志
            </td>
        </tr>
        <tr>
            <td>
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td>标题：
                                <asp:TextBox ID="txtTitle" CssClass="form-control text_md" runat="server" Width="379px"></asp:TextBox>
                            <asp:Label ID="lblErr" Text="" runat="server"></asp:Label>
                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle"   Display="Dynamic" ErrorMessage="请填写标题" Font-Size="10pt"></asp:RequiredFieldValidator> --%>
                        </td>
                    </tr>
                    <tr>
                        <td>日期：
                            <asp:TextBox ID="CDate_T" runat="server" CssClass="form-control text_md" ReadOnly="true" /></td>
                    </tr>
                    <tr>
                        <td>地点：
                            <asp:TextBox ID="Site_T" runat="server" CssClass="form-control text_md"/></td>
                    </tr>
                    <tr>
                        <td><span id="weather_span">天气：</span>
                            <asp:RadioButtonList ID="Weather_Rads" runat="server" RepeatDirection="Horizontal"  DataValueField="ID" DataTextField="Name" />
                    </tr>
                    <tr>
                        <td><span id="mood_span">心情：</span>
                            <asp:RadioButtonList ID="Mood_Rads" runat="server" RepeatDirection="Horizontal"  DataValueField="ID" DataTextField="Name" />
                    </tr>
                    <tr>
                        <td>分类：
                                <asp:DropDownList ID="dropLogType" runat="server" CssClass="form-control text_md"></asp:DropDownList>
                            <a href="LogTypeMange.aspx?where=2">分类管理</a>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 300px">
                            <%--<asp:HiddenField ID="HiddenField1" runat="server" />
                            <textarea cols="80" id="infoeditor" style="max-width: 428px; height: 300px;" class="form-control" width="300px" height="200px" name="infoeditor" srows="10" runat="server"></textarea>--%>
                            <asp:TextBox runat="server" ID="content_t" style="height:260px;" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>日志中提到的好友：
                            <asp:TextBox ID="txtFrd" CssClass="form-control text_md" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="text-center">
                <asp:Button ID="btnPut" CssClass="btn btn-primary" runat="server" Text="确定" OnClick="btnPut_Click" />
                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" Text="取消" OnClick="btnSave_Click" />
            </td>
        </tr>
    </table>
    <div class="s_bright" id="dwindow" style="position: absolute; top: 0px; left: 0px; width: 60%; height: 400px; display: none; filter: alpha(opacity=70); background-color: Gray; padding-top: 10px; margin-top: 50px; margin-left: 100px;"
        runat="server">
        <div class="i_r_ftitle">登录会员</div>
        <div class="i_r_fbody">
            <h1>请输入正确的用户名与密码 <font color='red'>
        <label id="LbAlert" runat="server" />
        </font></h1>
            <div class="cleardiv" style="height: 30px;"></div>
            <ul>
                <li style="width: 150px; text-align: right;"><b>用户名：</b></li>
                <li>
                    <asp:TextBox ID="TxtUserName" class="input1" MaxLength="20" runat="server"></asp:TextBox>
                </li>
            </ul>
            <div class="cleardiv"></div>
            <ul>
                <li style="width: 150px; text-align: right;"><b>密码：</b></li>
                <li>
                    <asp:TextBox ID="TxtPassword" runat="server" class="input1" TextMode="Password"></asp:TextBox>
                </li>
            </ul>
            <asp:PlaceHolder ID="PhValCode" runat="server">
                <ul>
                    <li style="width: 150px; text-align: right;"><b>验证码：</b></li>
                    <li>
                        <asp:TextBox ID="TxtValidateCode" class="input1" MaxLength="6" runat="server"></asp:TextBox>
                        <asp:Image ID="VcodeLogin" runat="server" ImageUrl="~/Common/ValidateCode.aspx" Height="20px" />
                    </li>
                </ul>
            </asp:PlaceHolder>
            <ul>
                <li style="width: 150px; text-align: right;"><b>Cookie：</b></li>
                <li>
                    <asp:DropDownList ID="DropExpiration" runat="server">
                        <asp:ListItem Value="Day" Text="保存一天"></asp:ListItem>
                        <asp:ListItem Value="Month" Text="保存一月"></asp:ListItem>
                        <asp:ListItem Value="Year" Text="保存一年"></asp:ListItem>
                    </asp:DropDownList>
                </li>
            </ul>
            <div class="cleardiv"></div>
            <ul>
                <li style="width: 150px; text-align: right;"></li>
                <li>
                    <asp:ImageButton ID="IbtnEnter" ImageUrl="../../images/login.gif" runat="server" OnClick="IbtnEnter_Click" />
                    <a href="../../Register.aspx">
                        <img src="../../images/reg1.gif" alt="" /></a> </li>
            </ul>
            <div class="cleardiv"></div>
            <ul>
                <li style="width: 150px; text-align: right;"></li>
                <li><a href="/User/GetPassword.aspx">忘记密码了？ </a></li>
                <li>如果您尚未在本站注册为用户，请先<a href="../../Register.aspx">点此注册</a> 。</li>
            </ul>
        </div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
#weather_span,#mood_span{float:left;}
#Weather_Rads td label,#Mood_Rads td label{padding-right:10px;}
</style>
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<%=Call.GetUEditor("content_t",2) %>
</asp:Content>
