<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WebUserControlSetCenterTop.ascx.cs"
    Inherits="User_SetCenter_WebUserControlSetCenterTop" %>
<div class="us_topinfo" style="margin-top: 10px;">
    <a href='<%=ResolveUrl("~/User/UserZone/SetCenter/SC_ANotification.aspx") %>'>活动通知</a> |
    <a href='<%=ResolveUrl("~/User/UserZone/SetCenter/SC_blacklist.aspx") %>'>黑名单</a> |
    <%--<a href='<%=ResolveUrl("~/User/UserZone/SetCenter/SC_Picture.aspx") %>'>照片公开模式</a> |--%>
    <a href='<%=ResolveUrl("~/User/UserZone/SetCenter/SC_Friendfactor.aspx") %>'>征友条件</a> |
    <a href='<%=ResolveUrl("~/User/UserZone/SetCenter/SC_FriendStatus.aspx") %>'>征友状态</a> |    
    <a href='<%=ResolveUrl("~/ColumnList.aspx?NodeID=30") %>'>谁能联系我</a>
</div>
