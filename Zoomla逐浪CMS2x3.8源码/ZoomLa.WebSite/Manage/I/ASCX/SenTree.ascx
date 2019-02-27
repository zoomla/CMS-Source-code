<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SenTree.ascx.cs" Inherits="Manage_I_ASCX_SenTree" %>
<div class="tvNavDiv">
<div class="left_ul">
<ul class="m_left_ulin" id="menu9_10">
    <li class="bg-primary"><span class="fa fa-chevron-down"></span><%=Resources.L.舆情管理 %></li>
    <li>
        <p class="menu_tit laybtn"><i class="fa fa-arrow-circle-down"></i><%=Resources.L.舆情管理 %></p>
        <ul id="menu9_10_ul1" class="menu_tit_ch active">
            <li id="menu9_10_1"><a href="javascript:;" onclick="ShowMain('','{$path}Sentiment/TaskList.aspx');"><%=Resources.L.监测任务 %></a></li>
            <li id="menu9_10_3"><a href="javascript:;" onclick="ShowMain('','{$path}Sentiment/SenConfig.aspx');"><%=Resources.L.监测维度 %></a></li>
            <li id="menu9_10_4"><a href="javascript:;" onclick="ShowMain('','{$path}Sentiment/DataList.aspx');"><%=Resources.L.数据入库 %></a></li> 
            <li id="menu9_10_6"><a href="javascript:;" onclick="ShowMain('','{$path}Sentiment/ReportList.aspx');"><%=Resources.L.报告列表 %></a></li>
        </ul>
    </li>
    <li>
        <p class="menu_tit laybtn"><i class="fa fa-plus-circle"></i><%=Resources.L.数据管理 %></p>
        <ul id="menu9_10_ul2" class="menu_tit_ch">
            <asp:Repeater runat="server" EnableViewState="false" ID="RPT">
                <ItemTemplate>
                    <li><a href="javascript:;" onclick="ShowMain('','{$path}Sentiment/GetReport.aspx?Skey=<%#HttpUtility.UrlEncode(Eval("Condition").ToString()) %>',null,'正在努力抓取数据中,请等待');"><%#Eval("Title") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </li>
</ul>
</div></div>