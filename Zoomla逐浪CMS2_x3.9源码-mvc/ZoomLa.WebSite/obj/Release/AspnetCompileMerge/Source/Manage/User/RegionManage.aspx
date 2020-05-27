<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegionManage.aspx.cs" Inherits="ZoomLaCMS.Manage.User.RegionManage" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>管理员地区设置</title>
    <style>
        .itemdiv {display:inline-block;margin-left:5px;text-align:center;height:420px;}
        .DataList{width:202px;height:400px;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:ScriptManager ID="SM1" runat="server"></asp:ScriptManager>
            <div class="itemdiv" style="padding-bottom: 200px;">
                <input type="button" onclick="selectAll('ProvinceList')" value="全选" class="btn btn-primary marginbot10" /><br />
                <asp:ListBox runat="server" ID="ProvinceList" TextMode="MultiLine" Height="400" CssClass="list DataList" OnSelectedIndexChanged="ProvinceList_SelectedIndexChanged"
                    AutoPostBack="true" SelectionMode="Multiple" />
            </div>
            <div class="itemdiv">
                <input type="button" onclick="selectAll('CityList')" value="全选" class="btn btn-primary marginbot10" /><br />
                <asp:ListBox runat="server" ID="CityList" TextMode="MultiLine" Height="400" CssClass="list DataList" OnSelectedIndexChanged="CityList_SelectedIndexChanged"
                    AutoPostBack="true" SelectionMode="Multiple" />
            </div>
            <div class="itemdiv">
                <input type="button" onclick="selectAll('CountyList')" value="全选" class="btn btn-primary marginbot10" /><br />
                <asp:ListBox runat="server" ID="CountyList" TextMode="MultiLine" CssClass="list DataList" Height="400" OnSelectedIndexChanged="CountyList_SelectedIndexChanged"
                    AutoPostBack="true" SelectionMode="Multiple" />
            </div>
            <div class="itemdiv" style="position: relative; top: -200px;">
                <asp:Button runat="server" ID="LtoR" Text="添加" OnClick="LtoR_Click" class="btn btn-primary" /><br />
                <asp:Button runat="server" ID="RtoL" Text="移除" OnClick="RtoL_Click" Style="margin-top: 20px;" class="btn btn-primary" /><br />
                <asp:Button runat="server" ID="OK" Text="保存更改" OnClick="OK_Click" Style="margin-top: 20px;" class="btn btn-primary" />
            </div>
            <div class="itemdiv">
                <input type="button" onclick="selectAll('MeAllCounty')" value="全选" class="btn btn-primary marginbot10" /><br />
                <asp:ListBox runat="server" ID="MeAllCounty" SelectionMode="Multiple" CssClass="list" Style="width: 202px; height: 400px;" />
            </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
            <script type="text/javascript">
                function selectAll(id) {
                    $("#" + id).children().each(function () { $(this).attr("selected", "selected") });
                }
        </script>
</asp:Content>
