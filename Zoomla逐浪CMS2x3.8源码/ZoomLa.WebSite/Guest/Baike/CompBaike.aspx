<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Guest/Baike/Baike.master" CodeFile="CompBaike.aspx.cs" Inherits="Guest_Baike_CompBaike" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>词条版本</title>
<style>
    .compbaike{padding:10px 150px;}
    .compbaike table{border-bottom:1px solid #ddd;}
    .compbaike .table th,td{padding:8px 20px!important; color:#666; font-weight:normal; border-bottom:none;}
    .compbaike h4{color:#111}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="compbaike">
        <div class="container-fluid">
            <div class="col-md-6 col-lg-6">
                <table class="table table-condensed">
                    <thead>
                        <tr>
                            <th>更新时间</th>
                            <th>版本</th>
                            <th>贡献者</th>
                            <th>修改原因</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td><asp:Label ID="PreDate_L" runat="server"></asp:Label></td>
                            <td><asp:Literal ID="PreSelBaike_Li" EnableViewState="false" runat="server"></asp:Literal></td>
                            <td><asp:Label ID="PreUserName_L" runat="server"></asp:Label></td>
                            <td><asp:Label ID="PreWhy_L" runat="server"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
                <div><h4>正文</h4></div>
                <div>
                     <div runat="server" id="precode"></div>
                </div>
            </div>
            <div class="col-md-6 col-lg-6">
                <table class="table table-condensed">
                    <thead>
                        <tr>
                            <th>更新时间</th>
                            <th>版本</th>
                            <th>贡献者</th>
                            <th>修改原因</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td><asp:Label ID="CurDate_L" runat="server"></asp:Label></td>
                            <td><asp:Literal ID="CurSelBaike_Li" EnableViewState="false" runat="server"></asp:Literal></td>
                            <td><asp:Label ID="CurUserName_L" runat="server"></asp:Label></td>
                            <td><asp:Label ID="CurWhy_L" runat="server"></asp:Label></td>
                        </tr>
                    </tbody>
                </table>
                <div><h4>正文</h4></div>
                <div>
                     <div runat="server" id="curcode"></div>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

