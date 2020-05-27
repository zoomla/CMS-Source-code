<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemFinger.aspx.cs" Inherits="ZoomLaCMS.Manage.Common.SystemFinger" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>系统指针</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="condiv">
        <table class="table table-striped table-bordered table-hover" id="systb">
            <tr>
                <td class="td_l"><strong>CMS系统版本：</strong></td>
                <td>
                    <asp:Label ID="lbUser" name="lbUser" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>页面网址：</strong></td>
                <td>
                    <asp:Label ID="lbServerName" name="lbServerName" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>主程序请求地址：</strong></td>
                <td>
                    <asp:Label ID="lbIp" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>数据库请求地址：</strong></td>
                <td>
                    <asp:Label runat="server" ID="DBIP_L"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>站点域名：</strong></td>
                <td>
                    <asp:Label ID="lbDomain" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>请求端口：</strong></td>
                <td>
                    <asp:Label ID="lbPort" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>服务器IIS版本：</strong></td>
                <td>
                    <asp:Label ID="lbIISVer" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>本文件所在文件夹：</strong></td>
                <td>
                    <asp:Label ID="lbPhPath" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>服务器操作系统：</strong></td>
                <td>
                    <asp:Label ID="lbOperat" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>系统所在文件夹：</strong></td>
                <td>
                    <asp:Label ID="lbSystemPath" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>服务器脚本超时时间：</strong></td>
                <td>
                    <asp:Label ID="lbTimeOut" name="lbTimeOut" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>服务器的语言种类：</strong></td>
                <td>
                    <asp:Label ID="lbLan" name="lbLan" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>.NET Framework 版本：</strong></td>
                <td>
                    <asp:Label ID="lbAspnetVer" name="lbAspnetVer" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>服务器当前时间：</strong></td>
                <td>
                    <asp:Label ID="lbCurrentTime" name="lbCurrentTime" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>服务器IE版本：</strong></td>
                <td>
                    <asp:Label ID="lbIEVer" name="lbIEVer" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>本次开机以来系统连续运行的时间：</strong></td>
                <td>
                    <asp:Label ID="lbServerLastStartToNow" name="lbServerLastStartToNow" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>服务器硬盘分区：</strong></td>
                <td>
                    <asp:Label ID="lbLogicDriver" name="lbLogicDriver" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>CPU 总数：</strong></td>
                <td>
                    <asp:Label ID="lbCpuNum" name="lbCpuNum" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>CPU 类型：</strong></td>
                <td>
                    <asp:Label ID="lbCpuType" name="lbCpuType" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>虚拟内存：</strong></td>
                <td>
                    <asp:Label ID="lbMemory" name="lbMemory" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>当前程序占用内存：</strong></td>
                <td>
                    <asp:Label ID="lbMemoryPro" name="lbMemoryPro" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>Asp.net所占内存：</strong></td>
                <td>
                    <asp:Label ID="lbMemoryNet" name="lbMemoryNet" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>Asp.net所占CPU：</strong></td>
                <td>
                    <asp:Label ID="lbCpuNet" name="lbCpuNet" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>当前Session数量：</strong></td>
                <td>
                    <asp:Label ID="lbSessionNum" name="lbSessionNum" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>当前Session的数量与ID：</strong></td>
                <td>
                    <asp:Label ID="lbSession" name="lbSession" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td><strong>操作系统版本:</strong></td>
                <td>
                    <asp:Label ID="SystemVersion_L" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="button" name="GetExc" value="保存信息为Excel" id="GetExc" class="btn btn-primary" onclick="OutToExcel();" />
                    <input type="button" id="back" name="back" value="返回" class="btn btn-primary" onclick="javascript: history.back();" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Label/ZLHelper.js"></script>
    <script>
        function OutToExcel() {
            var $html = $(document.getElementById("condiv").outerHTML);
            $html.find("td").css("border", "1px solid #666");
            $html.find("tr:last").remove();
            ZLHelper.OutToExcel($html.html(), "服务器信息总览");
        }
    </script>
</asp:Content>
