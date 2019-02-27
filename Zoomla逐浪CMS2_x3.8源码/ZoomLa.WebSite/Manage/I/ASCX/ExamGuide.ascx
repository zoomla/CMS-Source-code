<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExamGuide.ascx.cs" Inherits="Manage_I_ASCX_ExamGuide" %>
<div id="nodeNav" style="padding:0 0 0 0;">
        <div class="input-group">
        <input type="text" id="keyWord" class="form-control ascx_key" onkeydown="return ASCX.OnEnterSearch('<%:CustomerPageAction.customPath2+"Content/ContentManage.aspx?keyWord=" %>',this);" placeholder="文章标题或ID" />
        <span class="input-group-btn">
            <button class="btn btn-default" type="button" onclick="ASCX.Search('<%:CustomerPageAction.customPath2+"Content/ContentManage.aspx?keyWord=" %>','keyWord');"><span class="fa fa-search"></span></button>
        </span>
    </div>
    <div>
        <div class="tvNavDiv">
            <div class="left_ul">
                <asp:Literal runat="server" ID="nodeHtml" EnableViewState="false"></asp:Literal>
            </div>
        </div>
        <span style="color: green;" runat="server" id="remind" visible="false" />
    </div>
</div>