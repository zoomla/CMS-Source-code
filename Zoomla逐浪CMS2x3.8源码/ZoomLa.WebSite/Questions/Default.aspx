<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Questions_Default" EnableViewStateMac="false"  MasterPageFile="~/User/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
<title><%Call.Label("{$SiteName/}");%>-组卷系统</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="group" data-ban="sub"></div>
<div class="container margin_t10">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">组卷系统</li>
    </ol>
</div>
<div class="container u_cnt">
        <table width="90%" border="1" align="center" cellpadding="0" cellspacing="1" style="margin-bottom:10px; height:40px; line-height:40px; border:1px solid #ddd; text-align:center; background:#fafafa;">
            <tr>
                <td><a href="/User/Exam/Papers_System_Manage.aspx" target="_blank">组卷管理</a></td>
                <td><a href="/User/Exam/QuestList.aspx" target="_blank">试题中心</a></td>
                </tr>
            </table>
    <div>
        <table width="90%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC">
            <tr>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <td align="center" bgcolor="#FFFFFF" height="26px">
                            <a href="?id=<%#Server.HtmlEncode(Eval("C_Id").ToString())%>">
                                <%#Eval("C_ClassName")%></a>
                        </td>
                    </ItemTemplate>
                </asp:Repeater>
            </tr>
        </table>
    </div>
    <%
        if (Request.QueryString["id"] != null)
        {    
    %>
    <br />
    <table width="90%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC">
        <tr>
            <td align="center" bgcolor="#FFFFFF"  width="20%">
                <%=GetClassName() %>
            </td>
        </tr>
    </table>
    <br />
    <div>
        <table width="90%" border="0" align="center" cellpadding="0" cellspacing="1" bgcolor="#CCCCCC" height="1500px">
            <tr>
                <td width="20%" align="left" valign="top" bgcolor="#FFFFFF">
                    <asp:TreeView ID="tvNav" runat="server" ShowLines="True" EnableViewState="False"
                        NodeIndent="10" CollapseImageUrl="~/Images/TreeLineImages/minus.gif" 
                        ExpandImageUrl="~/Images/TreeLineImages/plus.gif" 
                        LineImagesFolder="~/Images/TreeLineImages">
                        <NodeStyle BorderStyle="None" />
                    </asp:TreeView>
                </td>
                <td align="center" bgcolor="#FFFFFF" valign="top"  height="100%">
                <iframe name="main" id="main" src="Paper.aspx?cid=<%=Server.HtmlEncode(Request.QueryString["id"]) %>" onload="Javascript:SetCwinHeight()"  width="100%" height="100%"  scrolling="no" ></iframe>   
                </td>
            </tr>
        </table>
    </div>
    <%
        }    
    %>

<script language="javascript">
    function SetCwinHeight() {
        var cwin = document.getElementById("main");
//        if (document.getElementById) {
//            if (cwin && !window.opera) {
//                if (cwin.contentDocument && cwin.contentDocument.body.offsetHeight)
//                    cwin.height = cwin.contentDocument.body.offsetHeight + 20;
//                else if (cwin.Document && cwin.Document.body.scrollHeight)
//                    cwin.height = cwin.Document.body.scrollHeight + 20;
//            }
//        }
    }
</script>
</asp:Content>
