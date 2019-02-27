<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchBody.aspx.cs" Inherits="Search_SearchBody" EnableViewState="false" %>
<form id="form1" runat="server">
    <div id="main">
        <div style="padding:3px;">搜索到<span style="color:red;"><%:count %></span>条数据</div>
        <ul>
            <asp:repeater id="RPT" runat="server">
                    <ItemTemplate>
                        <li><strong><a href="<%# GetUrl() %>" style="font-size: 18px;" target="_blank" title="点击访问">
                            <%#ToRed(Eval("Title","")) %></a></strong>
                            <p><%#ToRed(Eval("TagKey",""))%></p>
                            <span>发表时间：<%#Eval("CreateTime") %>
                            <a href="<%# GetUrl()%>" target="_blank">访问详情>></a></span>
                        </li>
                    </ItemTemplate>
                </asp:repeater>
            <asp:literal id="Page_Lit" runat="server"></asp:literal>
        </ul>
        <div id="PShow" runat="server" visible="false">
            <pre>
                抱歉，没有找到与<span class="red"><%=KeyWord %></span>相关的内容，
                搜索建议您： 看看输入的文字是否有误! 
                去掉可能不必要的字词:如"的","什么"等
            </pre>
        </div>
        <div id="nonemsg" runat="server" visible="false">
            <pre><h3>没有输入查询关键字</h3></pre>
        </div>
    </div>
</form>
