<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageTree.ascx.cs" Inherits="Manage_I_ASCX_PageTree" %>
    <div id="nodeNav" style="padding:0 0 0 0;">
        <div>
            <div class="tvNavDiv">
                <div class="left_ul">
                    <asp:Literal runat="server" ID="nodeHtml" EnableViewState="false"></asp:Literal>
                </div>
            </div>
            <span style="color: green;" runat="server" id="remind" visible="false" />
        </div>
    </div>