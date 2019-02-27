<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DatabaseProfile.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.DatabaseProfile" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <script type="text/javascript" src="/js/Common.js"></script>
    <title>运行库概况</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs">
        <li class="active"><a href="#Tabs0" data-toggle="tab">整体概况</a></li>
        <li><a href="#Tabs1" data-toggle="tab">数据详情</a></li>
    </ul>
    <div class="tab-content panel-body padding0">
        <div class="tab-pane active" id="Tabs0">
            <table class='table table-striped table-bordered table-hover' id="crt1" name="crt1">
                <tr>
                    <td>
                        <div id="Div1" runat="server" style="text-align: center; font-size: 15px; font-weight: bold;">
                            <asp:Label ID="tableTotal" runat="server"></asp:Label></div>
                    </td>
                </tr>
            </table>
        </div>
        <div class="tab-pane" id="Tabs1">
            <table class="table table-striped table-bordered table-hover" id="crt2" name="crt2" style="display: none;">
                <tr>
                    <td>
                        <div id="Top" runat="server" style="text-align: center; font-size: 15px; font-weight: bold;">
                            <asp:Label ID="Label1" Text="当前数据库所有表空间详情（按表占用量从大到小排序）" runat="server"></asp:Label><br />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="RunOK" runat="server">
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    
    <div id="Div2" runat="server"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">

    </script>
</asp:Content>





