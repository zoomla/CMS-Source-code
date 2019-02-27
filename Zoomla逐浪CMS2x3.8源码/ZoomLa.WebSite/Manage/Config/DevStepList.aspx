<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DevStepList.aspx.cs" Inherits="manage_Config_DevStepList" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>查看流程</title>
<style type="text/css">
.divstyle
{
background-color:White;
border-color:#9AC7F0;
border-style:solid;
width:131px; 
CURSOR: move; 
HEIGHT: 102px; 
LEFT: 6px; 
Z-INDEX: 1   
}
.moves{ width:60px; float:left; line-height:100px; text-align:center  }
</style>
<script type="text/javascript">
        var ajax = new AJAXRequest();
        var over = false; var down = false; var divleft; var divtop; var n; var mousX; var mousY; var divs; var divdown = false; var bigdivleft; var bigdivtop; var w = 0; var ismove = false;
        function closediv(x) {
            var del = document.getElementById("plane" + x);
            var delcode = x;
            if (confirm("确定要删除？")) {
                if (delcode == null) {

                    document.body.removeChild(del);
                }
                else {
                    ajax.get("ajaxreturn.aspx?code=" + delcode + "&action=del",
                function (ajb) {
                    var pri = ajb.responseText;
                    //alert(pri);
                    document.body.removeChild(del);
                });
            }
            location.reload(true);
            }
            else { }
        }
        function showdiv(act, di) {//显示编辑对话框
            var code = di;
            //alert(code);
            ajax.get("ajaxreturn.aspx?id=" + act + "&divs=" + di + "&action=edit&code=" + code,
           function (ajb) {
               var pri = ajb.responseText;
               var newdiv = document.createElement("div");
               newdiv.id = 'newdivs';
               newdiv.className = "newdivs";
               newdiv.style.position = "absolute";
               newdiv.style.top = "280px";
               newdiv.style.left = "300px";
               newdiv.innerHTML = pri;
               document.body.appendChild(newdiv);
           })
        }
        function save(j, f) {//对保存的操作
            var tablename = document.getElementById("TB_TabName").value;
            var code = f;
            ajax.get("ajaxreturn.aspx?id=" + j + "&action=save&Divid=<%=DevID %>&tablename=" + tablename + "&code=" + code,
           function (ajb) {
               var pri = ajb.responseText;
               price = pri.split('|');
               var divss = document.getElementById("plane" + f);
               var lab = document.getElementById("Lable" + f);
               lab.innerHTML = price[0];
               //divss.insertAdjacentHTML("beforeEnd", "<br/>" + price[0]);
               divss.setAttribute("code", price[1]);
           })
            var can = document.getElementById("newdivs");
            document.body.removeChild(can);
        }
        function cancel() {//对取消的操作
            var can = document.getElementById("newdivs");
            document.body.removeChild(can);
        }
        function orderbydiv(di) {//显示选择排序的的操作
            ajax.get("ajaxreturn.aspx?di=" + di + "&action=order",
        function (ajb) {
            var pri = ajb.responseText;
            var newdiv = document.createElement("div");
            newdiv.id = 'newdivs';
            newdiv.className = "newdivs";
            newdiv.style.position = "absolute";
            newdiv.style.top = "280px";
            newdiv.style.left = "300px";
            newdiv.innerHTML = pri;
            document.body.appendChild(newdiv);
        });
        }
        function saveorder(ds) {//对保存排序的操作
            var tablename = document.getElementById("TB_TabName").value;
            var divss = document.getElementById("plane" + ds);
            var code = ds;
            ajax.get("ajaxreturn.aspx?code=" + code + "&action=saveorder&Divid=<%=DevID %>&di=" + tablename,
           function (ajb) {
               var pri = ajb.responseText;
               var orderid = document.getElementById("orderid" + ds);
               orderid.innerHTML = pri;
           })
            var can = document.getElementById("newdivs");
            document.body.removeChild(can);
            location.reload(true);
        }


    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <div class="r_navigation">
    后台管理 &gt;&gt; 扩展功能 &gt;&gt; 数据维护 &gt;&gt;<a href="DevelopmentCenter.aspx">开发中心</a>&gt;&gt;查看流程</div>
    <table class="border" width="100%" cellspacing="1">
        <tr>
        <td class="spacingtitle" colspan="2" align="center">
		    查看流程
	    </td>
        </tr>
    </table>
    <table>
        <tr><td><asp:Literal runat="server" ID="ShowDiv" ></asp:Literal></td></tr>
        <tr><td><asp:LinkButton ID="LinkButton1" runat="server"  CssClass="C_input" Text="立即执行" OnClientClick="return confirm('确定要执行吗？');" onclick="LinkButton1_Click"></asp:LinkButton></td></tr>
    </table>
    
    
   <div ><font color="red">
   <asp:Literal runat="server" ID="Literal1" ></asp:Literal></font> 
   </div>
</asp:Content>
