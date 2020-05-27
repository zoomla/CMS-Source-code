<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Month.aspx.cs" Inherits="ZoomLaCMS.Manage.Counter.Month" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>日统计报表</title>
    <style>
        .allchk_l {
            display: none;
        }
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="container-fluid">
        <h3 style="margin-top: 0px;">
            <small>
                <a style="float: right;" href="counter.aspx">[返回]</a>
                统计日期：<asp:Label ID="CDate_L" runat="server"></asp:Label>
                本月累计：<asp:Label ID="SumCount_L" runat="server"></asp:Label>
                [<a href="month.aspx">当前月</a>] 
                      [<a href="month.aspx?cdate=<%=Server.UrlEncode(CDate.AddMonths(-1).ToString()) %>">上月</a>] 
                      [<a href="month.aspx?cdate=<%=Server.UrlEncode(CDate.AddMonths(1).ToString()) %>">下月</a>]
            </small>
        </h3>
        <div class="clearfix"></div>
        <div class="col-md-2 col-lg-2 padding0">
            <div class="panel panel-info">
                <div class="panel-heading">
                    <h3 class="panel-title"><i class="fa fa-calendar"></i><%:CDate.Month %>月统计</h3>
                </div>
                <ul class="list-group">
                    <asp:Repeater ID="CountRPT" runat="server">
                        <ItemTemplate>
                            <li class="list-group-item"><i class="fa fa-calendar-o"></i><%#Eval("DAY")%>日(<%#GetDayOfWeek() %>)<span class="badge" title="访问量"><%#Eval("NUM") %></span></li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
        </div>
        <div class="col-md-10 col-lg-10">
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" PageSize="15" AllowPaging="true" EnableTheming="False"
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!"
                OnPageIndexChanging="EGV_PageIndexChanging">
                <Columns>
                    <asp:BoundField ControlStyle-CssClass="td_s" HeaderText="ID" DataField="ID" />
                    <asp:TemplateField HeaderText="来源">
                        <ItemTemplate>
                            <%#Eval("Source") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="用户">
                        <ItemTemplate>
                            <%#Eval("UserName") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="IP">
                        <ItemTemplate>
                            <%#Eval("IP") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作系统">
                        <ItemTemplate>
                            <%#Eval("OSVersion") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="设备">
                        <ItemTemplate>
                            <%#Eval("Device") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="浏览器版本">
                        <ItemTemplate>
                            <%#Eval("BrowerVersion") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="创建时间">
                        <ItemTemplate>
                            <%#Eval("CDate") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </ZL:ExGridView>
        </div>
    </div>
    <%--<table  class="table table-striped table-bordered table-hover">
            <tr class="title_link">
                <td class="title" style="height: 22px" colspan="7" >
                 
                   
                </td>
            </tr>
            <tr>
                <td width="100%" style="vertical-align: top; height: 100%;">
                    <div align="center">
                        <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" >
                            <tr>
                                <td height="100%" align="center" valign="top" bgcolor="#ffffff">
                                    <% 
                                        if (pmonth == 0)
                                        {
                                    %>
                                    <table width="100%"处理style="border-collapse: collapse" align="center" cellpadding="0" cellspacing="0" >
                                        <tr>
                                            <td>
                                                <div align="center">
                                                    没有任何数据</div>
                                            </td>
                                        </tr>
                                    </table>
                                    <%
                                        }
                                        else
                                        {
                                    %><div class="user_t">
                                    <table width="100%" border="0" align="center" class="border" style="background-color: transparent">
                                        <tr align="center" bgcolor="#e2e9ff">
                                            <td width="15%">
                                                日期
                                            </td>
                                            <td width="10%">
                                                星期
                                            </td>
                                            <td width="10%">
                                                访问量
                                            </td>
                                            <td width="60%">
                                                比例
                                            </td>
                                        </tr>
                                        <%
                                            for (int ii = 1; ii < LastdayL + 1; ii++)
                                            {
                                                DateTime aboutdate1 = Convert.ToDateTime(aboutdate + "-" + ii);	
                                        %>
                                        <tr   bgcolor="#ffffff">
                                            <td align="center" >
                                                <%=ii%>&nbsp;
                                            </td>
                                            <td align="center">
                                                <%
                                                    if ((aboutdate1.DayOfWeek.ToString() == "Saturday") || (aboutdate1.DayOfWeek.ToString() == "Sunday"))
                                                    { Response.Write("<font color=red>" + GetDayOfWeek(aboutdate1.DayOfWeek.ToString()) + "</font>"); }
                                                    else
                                                    { Response.Write(GetDayOfWeek(aboutdate1.DayOfWeek.ToString())); }
                                                %>&nbsp;
                                            </td>
                                            <td align="center">
                                                <%=dsadmin.Tables[0].Rows[0][ii].ToString()%>
                                            </td>
                                            <td class="divCall"  id="tdPx<%=ii %>" onmouseover="this.firstChild.style.backgroundColor='#0953C4';" onmouseout="this.firstChild.style.backgroundColor='#4197E2';"  title="访问量：<%=dsadmin.Tables[0].Rows[0][ii].ToString() %>"  style="text-align: left;">
                                                <div onmouseover="this.style.backgroundColor='#0953C4';" onmouseout="this.style.backgroundColor='#4197E2';" id="divPx<%=ii %>" style='height: 10px; background-color: #4197E2;display:none;'>
                                                </div>
                                                <script type="text/javascript">
                                                <%
                                                    if (MaxCount == 0)
                                                    {
                                                        Response.Write("var leftPx" + ii + "=0;");
                                                    }
                                                    else
                                                    {
                                                        LeftPx = Convert.ToInt32(250 * Convert.ToInt32(dsadmin.Tables[0].Rows[0][ii].ToString()) / MaxCount);
                                                        Response.Write("var leftPx" + ii + "="+"document.getElementById('tdPx"+ii+"').offsetWidth*"+Convert.ToInt32(dsadmin.Tables[0].Rows[0][ii].ToString())+"/"+MaxCount+";");
                                                        Response.Write("document.getElementById('divPx"+ii+"').setAttribute('rel1',"+Convert.ToInt32(dsadmin.Tables[0].Rows[0][ii].ToString())+");");
                                                        Response.Write("document.getElementById('divPx"+ii+"').setAttribute('rel2',"+MaxCount+");");
                                                    }
                                                %>
                                                    document.getElementById("divPx<%=ii %>").style.width = leftPx<%=ii%> + 1;
                                                    
                                                </script>
                                            </td>
                                        </tr>
                                        <%}%>
                                    </table></div>
                                    <%}%>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>--%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Counter.js" type="text/javascript"></script>
</asp:Content>
