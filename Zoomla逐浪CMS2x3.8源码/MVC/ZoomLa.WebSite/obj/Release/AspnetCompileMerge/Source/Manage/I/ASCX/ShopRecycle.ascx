<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ShopRecycle.ascx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.ShopRecycle" %>
<div class="menu_tit"><span class="fa fa-chevron-down"></span> 商品管理</div>
<div id="nodeNav" style="padding: 0 0 0 0;">
    <div class="input-group">
        <input type="text" ID="keyWord" class="form-control" onkeydown="return ASCX.OnEnterSearch('<%:CustomerPageAction.customPath2+"Shop/ShopRecycler.aspx?keyWord=" %>',this);" placeholder="请输入商品名称或ID" />
        <span class="input-group-btn">
            <button type="button" id="searchBtn" class="btn btn-default" onclick="ASCX.Search('<%:CustomerPageAction.customPath2+"Shop/ShopRecycler.aspx?keyWord=" %>','keyWord');"><span class="fa fa-search"></span></button>
        </span>
    </div>
    <div>
        <div class="tvNavDiv">
            <div class="left_ul">
                <asp:Literal runat="server" ID="nodeHtml" EnableViewState="false"></asp:Literal>
            </div>
        </div>
        <span style="color: green;" runat="server" id="Span1" visible="false" />
    </div>
</div>
