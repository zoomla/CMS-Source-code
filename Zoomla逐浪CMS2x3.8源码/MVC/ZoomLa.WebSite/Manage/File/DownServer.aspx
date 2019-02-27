<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DownServer.aspx.cs" Inherits="ZoomLaCMS.Manage.FtpFile.DownServer" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>下载服务器</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>
                    <asp:Label ID="LblTitle" runat="server" Text="添加服务器" Font-Bold="True"></asp:Label>
                </strong>
            </td>
        </tr>
        <tr>
            <td align="left">
                <strong>服务器名称：</strong><br />
                在此输入在前台显示的镜像服务器名，如广东下载、上海下载等。
            </td>
            <td  style="text-align: left; width: 60%;">
                <asp:TextBox ID="TxtServerName" class="form-control" runat="server" Width="290px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ValrServerName" runat="server" ErrorMessage="下载服务器名称不能为空"
                    ControlToValidate="TxtServerName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td align="left">
                <strong>服务器LOGO：</strong><br />
                输入服务器LOGO的绝对地址，如http://www.z01.com/Images/ServerLogo.gif
            </td>
            <td  style="text-align: left; width: 60%;">
                <asp:TextBox ID="TxtServerLogo" class="form-control" runat="server" Width="290px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left">
                <strong>服务器地址：</strong><br />
                请认真输入正确的服务器地址。<br />
                如http://www.z01.com/这样的地址
            </td>
            <td  style="text-align: left; width: 60%; height: 49px;">
                <asp:TextBox ID="TxtServerUrl" class="form-control" runat="server" Width="290px"></asp:TextBox>
                <asp:RequiredFieldValidator ID="ValrServerUrl" runat="server" ErrorMessage="下载服务器地址不能为空"
                    ControlToValidate="TxtServerUrl"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr >
            <td align="left">
                <strong>链接地址加密方式：</strong>
            </td>
            <td  style="text-align: left; width: 60%;">
                <asp:DropDownList ID="Encrypttype" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Encrypttype_SelectedIndexChanged">
                    <asp:ListItem Value="0">不加密</asp:ListItem>
                    <asp:ListItem Value="1">Base64加密</asp:ListItem>
                    <asp:ListItem Value="2">DES加密</asp:ListItem>
                    <asp:ListItem Value="3">RSA加密</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="Label2" runat="server"></asp:Label>
            </td>
        </tr>

        <tr  id="Encrypt" runat="server" visible="false" >
            <td align="left">
                <strong>链接地址加密密钥：</strong>
            </td>
            <td  style="text-align: left; width: 60%;">
                <asp:TextBox ID="EncryptKey" class="form-control" runat="server" Height="58px" TextMode="MultiLine"  Width="407px"></asp:TextBox>
            </td>
        </tr>

        <tr  id="Tr1" >
            <td align="left">
                <strong>附加时间戳加密：</strong>
            </td>
            <td  style="text-align: left; width: 60%;">
                <asp:CheckBox ID="TimeEncrypt" runat="server" Text="使用" AutoPostBack="true" />
                <font color="red">说明:此时间戳是经过了MD5+SHA1加密,在设定时间内更新一次</font>
            </td>
        </tr>
        <tr  id="Tr2" runat="server">
            <td align="left">
                <strong>更新时间戳间隔时间：</strong><br />
                单位：分钟, 0 为不更新</td>
            <td  style="text-align: left; width: 60%;">
                <asp:TextBox ID="UpTimeuti" CssClass="form-control" runat="server">0</asp:TextBox>
                <asp:DropDownList ID="UpTimeutiList" CssClass="form-control" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem Value="1">每分钟</asp:ListItem>
                    <asp:ListItem Value="10">每十分钟</asp:ListItem>
                    <asp:ListItem Value="30">每三十分钟</asp:ListItem>
                    <asp:ListItem Value="60">每小时</asp:ListItem>
                    <asp:ListItem Value="360">每六小时</asp:ListItem>
                    <asp:ListItem Value="720">每十二小时</asp:ListItem>
                    <asp:ListItem Value="1440">每天</asp:ListItem>
                    <asp:ListItem Value="2880">每二天</asp:ListItem>
                    <asp:ListItem Value="7200">每五天</asp:ListItem>
                    <asp:ListItem Value="10080">每七天</asp:ListItem>
                    <asp:ListItem Value="44640">每月</asp:ListItem>
                    <asp:ListItem Value="133920">每季度</asp:ListItem>
                    <asp:ListItem Value="535680">每年</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr >
            <td align="left">
                <strong>显示方式：</strong>
            </td>
            <td  style="text-align: left; width: 60%;">
                <asp:DropDownList ID="DropShowType" CssClass="form-control" runat="server">
                    <asp:ListItem Value="0">显示名称</asp:ListItem>
                    <asp:ListItem Value="1">显示LOGO</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr >
            <td align="left">
                <strong>允许访问用户组(权限设置)：</strong>
            </td>
            <td  style="text-align: left; width: 60%;">
                <asp:CheckBoxList ID="ReadRoot" runat="server">
                </asp:CheckBoxList>
            </td>
        </tr>
        <tr >
            <td style="text-align: center" colspan="2">
                <asp:Button ID="EBtnModify" class="btn btn-primary" Text="修改" OnClick="EBtnModify_Click" runat="server" Visible="false" />
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp;&nbsp;
                <input name="BtnCancel" type="button" class="btn btn-primary" onclick="javascript: window.location.href = 'DownServerManage.aspx'" value=" 取消 " />
            </td>
        </tr>
    </table>
</asp:Content>
