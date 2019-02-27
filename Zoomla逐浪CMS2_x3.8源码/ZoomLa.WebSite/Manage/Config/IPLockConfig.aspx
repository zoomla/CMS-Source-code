<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IPLockConfig.aspx.cs" Inherits="Manage_I_Config_IPLockConfig" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" ClientIDMode="AutoID" %>
<%@ Register TagPrefix="ZL" TagName="IPWeb" Src="~/Manage/Config/IPWeb.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>IP访问限定</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
      <ul class="nav nav-tabs">
        <li class="active"><a href="#tab0" data-toggle="tab" onclick="ShowTabs(0)">IP访问限定</a></li>
        <li><a href="#tab1" data-toggle="tab" onclick="ShowTabs(1)">目录安全防护</a></li>
    </ul>
    <table class="table table-striped table-bordered table-hover" id="tab0">
    <tr align="center">
        <td colspan="2" class="spacingtitle">
            <strong><%:lang.LF("IP访问限定") %></strong>
        </td>
    </tr>
    <tr>
        <td style="width:200px">
            <strong><%:lang.LF("全站来访限定方式") %>：</strong></td>
        <td>
            <asp:RadioButtonList ID="LockIPType" runat="server">
                <asp:ListItem Value="0">不启用来访限定功能，任何IP都可以访问本站。</asp:ListItem>
                <asp:ListItem Value="1">仅仅启用白名单，只允许白名单中的IP访问本站。</asp:ListItem>
                <asp:ListItem Value="2">仅仅启用黑名单，只禁止黑名单中的IP访问本站。</asp:ListItem>
                <asp:ListItem Value="3">同时启用白名单与黑名单，先判断IP是否在白名单中，如果不在，则禁止访问；如果在则再判断是否在黑名单中，如果IP在黑名单中则禁止访问，否则允许访问。</asp:ListItem>
                <asp:ListItem Value="4">同时启用白名单与黑名单，先判断IP是否在黑名单中，如果不在，则允许访问；如果在则再判断是否在白名单中，如果IP在白名单中则允许访问，否则禁止访问。</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td style="width: 200px">
            <strong><%:lang.LF("全站IP段白名单") %>：</strong>
        </td>
        <td>
            <ZL:IPWeb ID="IPLockWhite" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width:200px">
            <strong><%:lang.LF("全站IP段黑名单") %>：</strong>
        </td>
        <td>
            <ZL:IPWeb ID="IPLockBlack" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width:200px">
            <strong><%:lang.LF("后台来访限定方式") %>：</strong>
        </td>
        <td>
            <asp:RadioButtonList ID="AdminLockIPType" runat="server">
                <asp:ListItem Value="0">不启用来访限定功能，任何IP都可以访问本站。</asp:ListItem>
                <asp:ListItem Value="1">仅仅启用白名单，只允许白名单中的IP访问本站。</asp:ListItem>
                <asp:ListItem Value="2">仅仅启用黑名单，只禁止黑名单中的IP访问本站。</asp:ListItem>
                <asp:ListItem Value="3">同时启用白名单与黑名单，先判断IP是否在白名单中，如果不在，则禁止访问；如果在则再判断是否在黑名单中，如果IP在黑名单中则禁止访问，否则允许访问。</asp:ListItem>
                <asp:ListItem Value="4">同时启用白名单与黑名单，先判断IP是否在黑名单中，如果不在，则允许访问；如果在则再判断是否在白名单中，如果IP在白名单中则允许访问，否则禁止访问。</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td style="width:200px">
            <strong><%:lang.LF("后台IP段白名单") %>：</strong>
        </td>
        <td>
            <ZL:IPWeb ID="IPLockAdminWhite" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width:200px">
            <strong><%:lang.LF("后台IP段黑名单") %>：</strong>
        </td>
        <td>
            <ZL:IPWeb ID="IPLockAdminBlack" runat="server" />
        </td>
    </tr>        
</table>
    <table class="table table-striped table-bordered table-hover" id="tab1">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <strong>目录安全防护</strong>
            </td>
        </tr>
       <tr >
            <td style="width: 200px;text-align:right;line-height:100px;"><strong><%:lang.LF("安全目录") %>：</strong></td>
            <td><asp:TextBox ID="NeedCheckRefer" runat="server" Width="300px" Height="100px" class="form-control pull-left" TextMode="MultiLine"/>
              <span style="color:#1e860b;line-height:100px;"> 例如:/User/指定目录下的页面将禁止直接访问,支持多个</span>
                <asp:CustomValidator runat="server" ControlToValidate="NeedCheckRefer" ID="c1" ErrorMessage="格式不正确,示例:/User/,/QMail/" ForeColor="Red" ClientValidationFunction="ValidFunc" ValidationGroup="cc" />
          </td>
        </tr>
    </table>        
    <asp:Button ID="Button1" runat="server" Text="保存设置" class="btn btn-primary" OnClick="Button1_Click" ValidationGroup="cc" /><br />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function ShowTabs(id) {
            $("#tab" + id).show().siblings("table").hide();
        }
        function ValidFunc(sender, args) {
            var s = $("#<%:NeedCheckRefer.ClientID%>").val().replace(" ", "");
            if (s.indexOf(",") > -1) {
                for (var i = 0; i < s.split(',').length; i++) {
                    if (!StrCheck(s.split(',')[i])) { args.IsValid = false; break; }
                }
            }
            else
            {
                args.IsValid = StrCheck(s);
            }
        }
        function StrCheck(s)
        {
            var flag = false;
            if (s.indexOf("/") == 0 && s.lastIndexOf("/") == (s.length - 1))
                flag = true;
            return flag;
        }
    </script>
</asp:Content>