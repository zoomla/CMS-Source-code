<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommentNodeTree.ascx.cs" Inherits="Manage_I_ASCX_CommentNodeTree" ClientIDMode="Static" %>
    <div id="nodeNav" style="padding:0 0 0 0;">
        <div class="input-group">
            <asp:TextBox runat="server" ID="keyText" class="form-control" placeholder="<%$Resources:L,请输入需要搜索的内容 %>" />
            <span class="input-group-btn">
                 <asp:LinkButton runat="server" CssClass="btn btn-default" ID="searchBtn" OnClick="searchBtn_Click"><span class="fa fa-search"></span></asp:LinkButton>
            </span>
        </div>
        <div>
            <div class="tvNavDiv">
                <div class="left_ul">
                    <asp:Literal runat="server" ID="nodeHtml" EnableViewState="false"></asp:Literal>
                </div>c
            </div>
            <span style="color: green;" runat="server" id="remind" visible="false" />
        </div>
    </div>
<style type="text/css">
    .tvNavDiv{float:left;background-color:#f3f3f3;height:100%;width:100%;margin-top:1px;}
    .tvNav_ul li{padding-left:20px;}
    .left_ul ul li {border-bottom:1px solid #ddd;}
    .left_ul ul li a{color:#1963aa;font-size:0.9em;display:block;text-decoration:none;height:35px;line-height:35px;padding-left:10px;}
    .left_ul ul li a:hover{ background:#6BBEF6; color:#fff;}
    .activeLi{ background:#6BBEF6; color:#fff;border-bottom:1px solid #ddd;}
    .list_span {margin-right:10px;}/*End*/
</style>