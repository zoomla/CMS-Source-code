<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TreeTlpDP.ascx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.TreeTlpDP" %>
<div id="<%=ClientID %>" class="btn-group treetlp">
    <button type="button" style="text-align: left;" class="btn btn-default dropdown-toggle text-left text_300">
        <span class="treetext">
            <span class="gray_9"><i class="fa fa-warning"></i><%=EmpyDataText %></span></span>
        <span class="pull-right"><span class="caret"></span></span>
    </button>
    <ul class="dropdown-menu Template_files text_300" role="menu">
        <asp:Literal ID="TreeTlp_Li" runat="server" EnableViewState="false"></asp:Literal>
    </ul>
</div>