<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopSearch.aspx.cs" Inherits="Store_ShopSearch" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>商城搜索</title>
<link href="../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<body>
<form id="form1" runat="server">
    <div class="us_seta">
        <ul class="us_seta">
            您现在的位置：<a title="网站首页" href="/"><asp:Label ID="sitename" runat="server" Text="Label"></asp:Label></a><span> &gt;&gt; 信息搜索</span>
        </ul>
        <div class="cleardiv" style="padding-top: 10px">
        </div>
        <h1 style="text-align: center">
            信息搜索</h1>
        <div class="us_seta">
            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                <tr>
                    <td rowspan="3" valign="top" style="width: 40px">
                        分类：</td>
                    <td rowspan="3" valign="top" style="width: 155px">
                        <asp:ListBox ID="DDLNode" runat="server" Height="136px" SelectionMode="Multiple"
                            Width="155px"></asp:ListBox></td>
                    <td valign="top" colspan="2" >
                        查询分类：<asp:CheckBoxList ID="cblCounmType" runat="server" RepeatDirection="Horizontal">
                        </asp:CheckBoxList></td>
                </tr>
                <tr>
                <td>开始时间：<asp:TextBox ID="txtStartTime" runat="server"  OnFocus="setday(this)" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox></td>
                <td>结束时间：<asp:TextBox ID="txtEndTime" runat="server" OnFocus="setday(this)" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox></td>
                </tr>
                <tr>
                    <td valign="top" colspan="2" >关键字：&nbsp;
                        <asp:TextBox ID="txtProducer" runat="server"></asp:TextBox>
                        <asp:Button ID="Button1" runat="server" Text="搜  索" OnClick="Button1_Click" /></td>
                </tr>
            </table>
        </div>
        <asp:Panel ID="Panel1" runat="server" Height="50px" Width="100%">
        <h1 style="text-align: center">
            搜索结果</h1>
        <div class="us_seta">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td>
                                <a href='<%#ResolveUrl("~/Store/StoreShow.aspx?storeid="+Eval("ID")) %>'><asp:Image ID="Image1" runat="server" ImageUrl='<%#ResolveUrl(DataBinder.Eval(Container.DataItem ,"Thumbnails").ToString()) %>' /></a></td>
                            <td>
                            <ul>
                            <li><a href='<%#ResolveUrl("~/Store/StoreShow.aspx?storeid="+Eval("ID")) %>'><%#Eval("Proname") %></a></li>
                            <li><%#Eval("UserName") %></li>
                            </ul>
                            </td>
                            <td>
                            <%#Eval("LinPrice") %>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:Repeater>
            &nbsp;</div>
        <div id="Pager1" runat="server" style="text-align:center ">
            共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>&nbsp;
            <asp:Label ID="Toppage" runat="server" Text=""></asp:Label>
            <asp:Label ID="Nextpage" runat="server" Text=""></asp:Label>
            <asp:Label ID="Downpage" runat="server" Text=""></asp:Label>
            <asp:Label ID="Endpage" runat="server" Text=""></asp:Label>
            页次：<asp:Label ID="Nowpage" runat="server" Text=""></asp:Label>/<asp:Label ID="PageSize"
                runat="server" Text=""></asp:Label>页
            <asp:Label ID="pagess" runat="server" Text=""></asp:Label>个/页 转到第<asp:DropDownList
                            ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                        </asp:DropDownList>页
        </div>
        </asp:Panel>
    </div>
</form>
</body>
</html>
