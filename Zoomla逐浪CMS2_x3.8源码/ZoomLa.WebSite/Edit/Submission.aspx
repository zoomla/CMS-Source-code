<%@ Page Language="C#" AutoEventWireup="true" EnableViewStateMac="false" CodeFile="Submission.aspx.cs" Inherits="Edit_Submission" %>
<!DOCTYPE html>
<html>
<head>
<title>投稿</title>
</head>
<body>
<form action="/save.aspx" method="post">
<p>填写NodeID、ModeID及URL</p>
<div>
    用户ID：<input id="UserID" runat="server" type="text" value="" /><br />
    分&nbsp;类：<input id="Type" runat="server" type="text" value="" /><br />
    内&nbsp;容：<input id="Content" name="content" runat="server" type="text" value="" /><br />
<%--        修改时间：<input  id="CreateTime" runat="server" type="text" value="" /><br />
--%>        标&nbsp;题：<input id="Title" runat="server" type="text" value="" /><br />
<%--        类&nbsp;型：<input id="Status" runat="server" type="text" value="" />
--%>    </div>
<div>
    <input name="GeneralID" value="0" />
    <p>NodeID：<input id="NodeID" runat="server" name="NodeID" type="text" value="78" /></p>
    <p>ModeID：<input id="ModelID" runat="server" name="ModeID" type="text" value="2" /></p>
    <p>URL：<input id="URL" runat="server" type="text" value="/default.aspx" /></p>
</div>
<p><input type="submit" value="提交" /></p>
</form>
</body>
</html>