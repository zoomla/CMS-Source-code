<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StructTree.ascx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.StructTree" %>
<div id="nodeNav" style="padding: 0 0 0 0;">
    <div class="menu_tit"><span class="fa fa-chevron-down"></span>组织结构</div>
    <%-- <div class="input-group">
            <input type="text" id="keyWord" class="form-control" onkeydown="return ASCX.OnEnterSearch('<%:CustomerPageAction.customPath2+"Content/ContentManage.aspx?keyWord=" %>',this);" placeholder="用户名或ID" />
            <span class="input-group-btn">
                <button class="btn btn-default" type="button" onclick="ASCX.Search('<%:CustomerPageAction.customPath2+"Content/ContentManage.aspx?keyWord=" %>','keyWord');"><span class="fa fa-search"></span></button>
            </span>
        </div>--%>
    <div>
        <div class="tvNavDiv">
            <div class="left_ul">
                <asp:Literal runat="server" ID="nodeHtml" EnableViewState="false"></asp:Literal>
            </div>
        </div>
        <span style="color: green;" runat="server" id="remind" visible="false" />
    </div>
</div>
