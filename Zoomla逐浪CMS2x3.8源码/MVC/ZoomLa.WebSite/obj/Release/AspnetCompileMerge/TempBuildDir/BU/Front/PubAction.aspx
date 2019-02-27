<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PubAction.aspx.cs" Inherits="ZoomLaCMS.PubAction" EnableViewStateMac="false" %><!DOCTYPE HTML>
<html>
<head runat="server">
    <title>互动提交</title>
    <script>
        function ActionSec(obj, url) {
            console.log(obj, url);
            if (obj == -1) { alert("提交失败!"); }
            if (obj == 1) { alert("提交成功!"); }
            window.location.href = url;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server"></form>
</body>
</html>
