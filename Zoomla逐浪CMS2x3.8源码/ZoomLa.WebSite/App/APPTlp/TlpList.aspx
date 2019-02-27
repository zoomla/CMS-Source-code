<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TlpList.aspx.cs" Inherits="App_APPTlp_TlpList" MasterPageFile="~/Common/Master/Commenu.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>模板选择</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="height:60px;"></div>
        <div class="template tlplist">
            <ul class="list-unstyled">
                <ZL:ExRepeater runat="server" ID="RPT" PageSize="14" PagePre="<div class='clearfix'></div><div class='text-center'>" PageEnd="</div>">
                    <ItemTemplate>
                        <li class="padding5">
                        </li>
                    </ItemTemplate>
                    <FooterTemplate></FooterTemplate>
                </ZL:ExRepeater>
            </ul>
        </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <link href="/App_Themes/V3.css" rel="stylesheet" />
    <style type="text/css">
        .Template_box {padding:0px;}
        .temp_foot {border-top:1px solid #ddd;background:#fafafa;height:2em;line-height:2em;padding-left:5px;}
        .tlplist li{width:auto;height:auto;}

    </style>
</asp:Content>