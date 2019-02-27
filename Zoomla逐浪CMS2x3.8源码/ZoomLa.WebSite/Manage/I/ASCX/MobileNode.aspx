<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MobileNode.aspx.cs" Inherits="Manage_I_ASCX_MobileNode" EnableViewState="false"%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="modal fade modal_row" id="M_NodeModel" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="tvNavDiv">
                        <div class="left_ul">
                            <asp:Literal runat="server" ID="nodeHtml" EnableViewState="false"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <style type="text/css">
            .modal_row .modal-content ul li {border-bottom: 1px solid #ffffff;}
            .modal_row .modal-content ul li a {display: inline-block;}
            .fa{margin-left:20px;font-size:2em}
        </style>
        <script type="text/javascript">
            $("#M_NodeModel .tvNav  a.list1 .NodeP_Span").click(function () {
                window.event.cancelBubble = true;
                window.event.returnValue = false;
                showList($(this).parent());
                return false;
            });
            function showList(obj) {
                //$(obj).parent().parent().find("a").removeClass("SelectedA");//a-->li-->ul
                //$(obj).parent().children("a").addClass("SelectedA");//li
                $(obj).parent().siblings("li").find("ul").slideUp();
                $(obj).parent().children("ul").slideToggle();
            }
        </script>
    </form>
</body>
</html>