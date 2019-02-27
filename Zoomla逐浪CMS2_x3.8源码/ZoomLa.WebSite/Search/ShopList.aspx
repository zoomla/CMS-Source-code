<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopList.aspx.cs" Inherits="ZoomLa.WebSite.SearchList" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>搜索结果</title>
<link href="../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<script language="javascript">
    function setEmpty(obj) {
        if (obj.value == "请输入关键字") {
            obj.value = "";
        }
    }
    function settxt(obj) {
        if (obj.value == "") {
            obj.value = "请输入关键字";
        }
    }
</script>
</head>
<body>
<form id="form1" runat="server">
    <div class="us_seta">
        <ul class="us_seta">
            您现在的位置：<a title="网站首页" href="/"><asp:Label ID="sitename" runat="server" Text="Label"></asp:Label></a><span>&gt;&gt; 信息搜索</span>
        </ul>
        <div class="cleardiv" style="padding-top: 10px">
        </div>
        <h1 style="text-align: center">
            信息搜索</h1>
        <div class="us_seta">
            站内搜索
            <asp:DropDownList ID="DDLNode" runat="server">
            </asp:DropDownList>
            <asp:TextBox ID="TxtKeyword" runat="server" paceholder="请输入关键字"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="搜  索" OnClick="Button1_Click" />
        </div>
        <div class="cleardiv" style="padding-top: 10px">
        </div>
        <h1 style="text-align: center">搜索结果</h1>
        
        
        <div id="s_title">
        <span>一共为您找到标题为<%=getkeys() %>的信息<%=GetProductsCount() %>篇</span> 


    </div>
        <div class="us_seta">
            <div class="clear"></div>
            <ul>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <%--<div>
                            <a href="<%# GetUrl(Eval("GeneralID","{0}")) %>">
                                <%# Eval("Title") %>
                            </a>
                        </div>
                        <div class="clearbox">
                        </div>--%>
                        <%--<li style=" clear:left;">
                        <strong><a href="<%# GetUrl(Eval("GeneralID","{0}")) %>" style=" font-size:18px;" target="_blank" alt="<%#toRed(Eval("Title").ToString()) %>"><%#toRed(Eval("Title").ToString()) %></a></strong>
                        <p><%#toRed(Eval("TagKey").ToString())%></p>
                        <span>发表时间：<%#Eval("CreateTime") %>
                        <a href="<%# GetUrl(Eval("GeneralID","{0}")) %>" target="_blank">访问详情>></a></span>
                        </li>--%>
                        <li style=" clear:left;">
                        <strong><a href="<%# GetUrl(Eval("ItemID","{0}")) %>" style=" font-size:18px;" target="_blank" alt="<%#toRed(Eval("Proname").ToString()) %>"><%#toRed(Eval("Proname").ToString()) %></a></strong>
                        <p><%#toRed(Eval("Proinfo").ToString())%></p>
                        <span>更新时间：<%#Eval("UpdateTime") %>
                        <a href="<%# GetUrl(Eval("ItemID","{0}")) %>" target="_blank">访问详情>></a></span>
                        </li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>



            <div id="PShow" runat="server" visible="false">
            <pre><%--<span style="color:Blue"><%=key%></span>--%>
             抱歉，没有找到与<%=getkeys() %>相关的内容，
             搜索建议您： 看看输入的文字是否有误! 
             去掉可能不必要的字词:如"的","什么"等
            </pre>
            </div>
            <div id="nonemsg" runat="server" visible="false" style=" text-align:center">
                <h3>没有输入查询关键字</h3>
            </div>
        </div>

        <div id="Pager1" runat="server">
        </div>
    </div>
</form>
</body>
</html>
