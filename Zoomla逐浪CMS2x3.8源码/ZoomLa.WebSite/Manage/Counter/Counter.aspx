<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Counter.aspx.cs" Inherits="manage_Counter_Counter" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>统计导航</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered" style="width:400px;">
        <tr>
            <td colspan="4" style="text-align:center;background-color:#428BCA;color:white;">访问统计</td>
        </tr>
                    <tr align="center">
                        <td width="80" height="66">
                            <a href="month.aspx">
                                <img src="/App_Themes/Admin/stat2.jpg" width="70" height="70" border="0" /></a>
                        </td>
                        <td width="80">
                            <a href="year.aspx">
                                <img src="/App_Themes/Admin/stat5.jpg" width="70" height="70" border="0" /></a>
                        </td>
                        <td width="80">
                            <a href="browser.aspx">
                                <img src="/App_Themes/Admin/stat9.jpg" width="70" height="70" border="0" /></a>
                        </td>
                        <td width="80">
                            <a href="os.aspx">
                                <img src="/App_Themes/Admin/stat7.jpg" width="70" height="70" border="0" /></a>
                        </td>
                    </tr>
                    <tr align="center">
                        <td width="80">每日统计
                        </td>
                        <td width="80">每月统计
                        </td>
                        <td width="80">浏览信息
                        </td>
                        <td width="80">操作系统
                        </td>
                    </tr>
                    <tr align="center">
                        <td width="80" height="66">
                            <a href="site.aspx">
                                <img src="/App_Themes/Admin/stat10.jpg" width="70" height="70" border="0"/></a>
                        </td>
                        <td width="80">
                            <a href="local.aspx">
                                <img src="/App_Themes/Admin/stat8.jpg" width="70" height="70" border="0"/></a>
                        </td>
                        <td width="80">
                            <a href="ip.aspx">
                                <img src="/App_Themes/Admin/stat0.jpg" width="70" height="70" border="0"/></a>
                        </td>
                        <td width="80">
                            <a href="<%:customPath2+"Common/SystemFinger.aspx" %>">
                                <img src="/App_Themes/Admin/stat6.jpg" width="70" height="70" border="0"/></a>
                        </td>
                    </tr>
                    <tr align="center">
                        <td width="80">访问渠道</td>
                        <td width="80">地区数据</td>
                        <td width="80">全站统计</td>
                        <td width="80">系统信息</td>
                    </tr>
                </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function OnWindowsReSize() {
            document.getElementById("divMain").style.height = document.body.clientHeight - 75;
        }
    </script>
</asp:Content>