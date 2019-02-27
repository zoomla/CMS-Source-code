<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageProjects.aspx.cs" Inherits="manage_AddOn_ManageProjects" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>项目管理</title>
</head>
<script type="text/javascript" src="/js/Drag.js"></script>
    <script type="text/javascript" src="/js/Dialog.js"></script>
    <script type="text/javascript">
        var diag = new Dialog();
        function openurls(url) {
            Dialog.open({ URL: url });
        }
        function open_title(ModelID, NodeID) {
            diag.Width = 800;
            diag.Height = 600;
            //diag.Modal = false;
            diag.Title = "添加项目<span style='font-weight:normal'>[ESC键退出当前操作]</span>";
            diag.URL = "AddProjects.aspx";
            diag.show();
        }

        function opentitle(url, title) {
            diag.Width = 800;
            diag.Height = 600;
            diag.Title = title;
            //diag.Modal = false;
            diag.URL = url;
            diag.show();
        }
        function closdlg() {
            Dialog.close();
        }
    </script>
<frameset cols="180,*" frameborder="no" border="0" framespacing="0">
<frame src="ProjectClass.aspx" name="leftFrame" scrolling="No" noresize="noresize" id="leftFrame" />
<frame src="Projects.aspx" name="mainFrame" id="mainFrame" />
</frameset>
<noframes><body>
</body></noframes>
</html>