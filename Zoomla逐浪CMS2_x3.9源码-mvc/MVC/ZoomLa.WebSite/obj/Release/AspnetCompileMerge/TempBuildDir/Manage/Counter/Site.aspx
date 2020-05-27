<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Site.aspx.cs" Inherits="ZoomLaCMS.Manage.Counter.Site" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>访问渠道统计报表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="tables" bordercolor="#6595d6" cellspacing="0" cellpadding="2" width="97%" align="center" bgcolor="#ffffff">
        <tr class="title_link">
            <td class="title" style="height: 22px" colspan="2" align="center">
                <a style="float: right;" href="counter.aspx">[返回]</a>
                访问渠道累计： 
                    <%=SumCount%>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; height: 100%;" width="100%">
                <div align="center">
                    <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td height="100%" align="center" valign="top" bgcolor="#ffffff">
                                <% 
                                    if (pcount1 == 0)
                                    {
                                %>
                                没有任何数据
                                <%
                                    }
                                    else
                                    {
                                %>
                                <div class="user_t">
                                    <table width="100%" border="0" align="center" cellpadding="2" cellspacing="1" class="border">
                                        <tr align="center" bgcolor="#e2e9ff">
                                            <td width="15%">序号
                                            </td>
                                            <td width="15%">访问渠道
                                            </td>
                                            <td width="15%">访问量
                                            </td>
                                            <td>比例
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="Repeater1" runat="server">
                                            <ItemTemplate>
                                                <tr bgcolor="#ffffff">
                                                    <td align="center">

                                                        <%# Container.ItemIndex+1%>&nbsp;
                                                    </td>
                                                    <td align="center">
                                                        <%# DataBinder.Eval(Container.DataItem,"Count_Site") %>
                                                    </td>
                                                    <td align="center">
                                                        <%# DataBinder.Eval(Container.DataItem,"Count_Count") %>
                                                    </td>
                                                    <td class="divCall" id="tdPx<%# Container.ItemIndex+1%>" onmouseover="this.firstChild.style.backgroundColor='#0953C4';"
                                                        onmouseout="this.firstChild.style.backgroundColor='#4197E2';" title="访问量：<%# DataBinder.Eval(Container.DataItem,"Count_Count") %>"
                                                        style="text-align: left;">
                                                        <div onmouseover="this.style.backgroundColor='#0953C4';" onmouseout="this.style.backgroundColor='#4197E2';"
                                                            id="divPx<%# Container.ItemIndex+1%>" style='height: 10px; background-color: #4197E2; display: none;'>
                                                        </div>
                                                        <script type="text/javascript">
                                                <% 
                                                            if (MaxCount == 0)
                                                            {%>
                                                            var leftPx<%#Container.ItemIndex+1 %> + "=0;";
                                                   <% }
                                                            else
                                                            {%>
                                                            var leftPx<%#Container.ItemIndex+1 %>=document.getElementById('tdPx<%#Container.ItemIndex+1 %>').offsetWidth* <%# DataBinder.Eval(Container.DataItem,"Count_Count") %>/<%=MaxCount %>;
                                                            document.getElementById('divPx<%#Container.ItemIndex+1 %>').setAttribute('rel1',<%# DataBinder.Eval(Container.DataItem,"Count_Count") %>);
                                                            document.getElementById('divPx<%#Container.ItemIndex+1 %>').setAttribute('rel2',<%=MaxCount %>);
                                                    <%}
                                                %>
                                                            document.getElementById("divPx<%# Container.ItemIndex+1%>").style.width=leftPx<%# Container.ItemIndex+1%>+1;
                                                    
                                                        </script>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                                <%}%>
                            </td>
                        </tr>
                    </table>
                    <br>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <link href="/App_Themes/AdminDefaultTheme/Counter.css" rel="stylesheet" type="text/css" />
    <script src="/JS/Counter.js" type="text/javascript"></script>
</asp:Content>
