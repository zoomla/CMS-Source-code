<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowSuccess.aspx.cs" Inherits="ZoomLa.WebSite.Admin.Prompt.ShowSuccess" EnableViewStateMac="false" ValidateRequest="false" MasterPageFile="~/Common/Master/Empty.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <style type="text/css">ul li{list-style-type:none;}.fa{margin-right:3px;}</style> 
    <title>成功提示</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="panel panel-primary" style="width: 400px; margin: auto; margin-top: 12%;">
            <div class="panel-heading">
                <h3 class="panel-title"><span class="fa fa-check pull-left"></span>成功信息</h3>
            </div>
            <div class="panel-body text-center">
                <p class="text-center"><asp:Literal ID="LtrSuccessMessage" runat="server"></asp:Literal></p>
            </div>
             <div class="panel-footer" style="text-align:center;">
                <asp:HyperLink ID="LnkReturnUrl" runat="server" class="btn btn-primary" ToolTip="返回上一页"><span class="fa fa-repeat"></span>返回上一页</asp:HyperLink>
            </div>
        </div>
        <script>
            function AutoReturn(url, time) { setTimeout(function () { location = url }, time); }
            function SetUrl(url) { $("#LnkReturnUrl").attr("href", url); }
        </script>
</asp:Content>