<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NodeTree.aspx.cs" Inherits="ZoomLa.WebSite.User.Page.NodeTree" EnableViewStateMac="false" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>黄页节点</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <div id="nodeNav" style="padding: 0 0 0 0;width:95%;">
        <div>
            <div class="tvNavDiv">
                <div class="left_ul">
                    <asp:Literal runat="server" ID="nodeHtml" EnableViewState="false"></asp:Literal>
                </div>
            </div>
            <span style="color: green;" runat="server" id="remind" visible="false" />
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
</asp:Content>