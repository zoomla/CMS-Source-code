<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StructManage.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.StructManage" %>
<!DOCTYPE html>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title>组织结构</title>  
    </head>
    <script type="text/javascript">  
        window.onload = function () {
            var str = '<%=Request["type"] %>';
           
            if (str == '1') {
                document.getElementById("mainFrame").src = 'StructView.aspx?ID=<%=Request["ID"] %>';
            }
            else {

                document.getElementById("mainFrame").src = 'StructMenber.aspx?ID=<%=Request["ID"] %>';
            }
        }
    </script>
<frameset cols="180,*" frameborder="no" border="0" framespacing="0">
<frame src="StructClass.aspx?type=<%=Request["type"] %>&pid=<%=Request["pid"] %>" name="leftFrame"  noresize="noresize" id="leftFrame" />
<frame src="" name="mainFrame" id="mainFrame" />
</frameset>
<noframes><body>
</body></noframes>

</html>
