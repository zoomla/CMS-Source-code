<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SiteManage.aspx.cs" Inherits="ZoomLaCMS.Manage.User.SiteManage" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>子站点管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="selTable_Div" runat="server">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td class="td_s"></td>
                <td>站点名</td>
                <td>域名</td>
                <td>IP</td>
                <td>操作</td>
            </tr>
            <ZL:Repeater ID="Site_RPT" OnItemCommand="Site_RPT_ItemCommand" runat="server" PagePre="<tr><td><input type='checkbox' id='AllID_Chk'></td><td colspan='12'><div class='text-center'>" PageEnd="</div></td></tr>">
                <ItemTemplate>
                    <tr>
                        <td>
                            <input type="checkbox" name="idchk" value="<%#Eval("sip") %>" /></td>
                        <td><%#Eval("sname") %></td>
                        <td><%#Eval("domain") %></td>
                        <td><%#Eval("sip") %></td>
                        <td>
                            <a href="AddSite.aspx?domain=<%#Eval("domain") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                            <asp:LinkButton runat="server" CommandName="Del" CommandArgument='<%#Eval("domain") %>' OnClientClick="return confirm('是否删除!')" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </ZL:Repeater>
        </table>
    </div>
    <div id="selImage_Div" runat="server" visible="false">
        <iframe id="network_ifr" style='width: 100%; border: none; height: 450px;' src=""></iframe>
        <textarea id="code" runat="server" style="display: none;">
        </textarea>
    </div>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        function ShowImage() {
            var url = "/Plugins/ECharts/ZLEcharts.aspx";
            setTimeout(function () { $("#network_ifr").attr("src", url); }, 500);
        }
    </script>
</asp:Content>

