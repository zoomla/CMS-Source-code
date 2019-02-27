<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Optimization.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.Optimization" MasterPageFile="~/Manage/I/Default.master"%>

<%@ Register Src="~/Manage/I/ASCX/SPwd.ascx" TagPrefix="uc1" TagName="SPwd" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css"  rel="stylesheet"/>
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <title>数据库优化</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <uc1:SPwd runat="server" ID="SPwd" Visible="false" />
    <div id="maindiv" runat="server" visible="false">
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" class="text-center">数据库优化</td>
            </tr>
            <tr>
                <td align="right" style="width: 150px;"><strong>请选择表：</strong></td>
                <td>
                    <asp:DropDownList ID="DbTableDownList" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="DbTableDownList_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 150px;"><strong>是否创建唯一索引</strong></td>
                <td>
                    <input runat="server" type="checkbox" id="IsUnique" class="switchChk" />
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 150px;"><strong>创建索引的类型</strong></td>
                <td>
                    <asp:DropDownList runat="server" CssClass="form-control" ID="IndexType">
                        <asp:ListItem Value="">非聚簇索引</asp:ListItem>
                        <asp:ListItem Value="clustered">聚集索引</asp:ListItem>
                        <asp:ListItem Value="nonclustered">非聚集索引</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" style="width: 150px;"><strong>请输入索引名称：</strong></td>
                <td>
                    <input type="text" id="IndexName" class="form-control" style="width: 190px;" runat="server" value="Zoomla_index" /></td>
            </tr>
            <tr>
                <td align="right" style="width: 150px;"><strong>创建索引的字段：</strong></td>
                <td>
                    <asp:ListBox ID="DbFieldDownList" runat="server" CssClass="form-control" Height="220px" Width="200px" SelectionMode="Multiple" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="CreateIndex" Text="创建索引" CssClass="btn btn-primary" runat="server" OnClick="CreateIndex_Click" />
                    <asp:Button ID="ViewIndex" Text="查看已有的索引" CssClass="btn btn-primary" runat="server" OnClick="ViewIndex_Click" />
                </td>
            </tr>
        </table>
        <br />
        <div id="Top" runat="server" visible="false" style="text-align: center; font-size: 15px; font-weight: bold;">
            <asp:Label ID="Label1" runat="server"></asp:Label>
        </div>
        <br />
        <div id="RunOK" runat="server">
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>