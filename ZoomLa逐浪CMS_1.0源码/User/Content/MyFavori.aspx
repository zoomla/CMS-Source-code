<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyFavori.aspx.cs" Inherits="ZoomLa.WebSite.User.Content.MyFavori" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>我的收藏</title>
    <link href="/App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
<link href="/App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
<link href="../css/user.css" rel="stylesheet" type="text/css" />
<link href="../css/default1.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="/Manage/js/SelectCheckBox.js"></script>
</head>
<body>
<div>
    <div class="r_navigation">
    <div class="r_n_pic"></div>
    您现在的位置：<span id="YourPosition"><span><a title="网站首页" href="/"><asp:Label ID="LblSiteName" runat="server" Text="Label"></asp:Label></a></span><span> &gt;&gt; </span><span><a title="会员中心" href="/User/Default.aspx">会员中心</a></span><span> &gt;&gt; </span><span>收藏的信息</span></span></div>
    <div class="clearbox"></div>
    <form id="form1" runat="server">
        <asp:GridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="FavoriteID" PageSize="20" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Lnk_Click" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                  <ItemTemplate>
                      <asp:CheckBox ID="chkSel" runat="server" />
                  </ItemTemplate>
                  <ItemStyle HorizontalAlign="Center" />                
                </asp:TemplateField>
                <asp:BoundField DataField="InfoID" HeaderText="ID">
                <HeaderStyle Width="6%" />
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="标题">
                <HeaderStyle Width="50%" />
                <ItemTemplate>  
                    <a href="<%# GetUrl(Eval("InfoID", "{0}"))%>" target="_blank"><%# Eval("Title")%></a>     
                </ItemTemplate>            
                </asp:TemplateField>                
                <asp:TemplateField HeaderText="已生成">
                <ItemTemplate>  
                    <%# GetCteate(Eval("IsCreate", "{0}"))%>     
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>            
                <asp:TemplateField HeaderText="操作" >
                <ItemTemplate>                     
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Del" CommandArgument='<%# Eval("FavoriteID") %>' OnClientClick="return confirm('你确定将该数据从收藏夹删除吗？')">移出收藏夹</asp:LinkButton>
                  </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <RowStyle BackColor="#EFF3FB" Height="25px" />
            <EditRowStyle BackColor="#2461BF" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <AlternatingRowStyle BackColor="White" />
            <PagerSettings FirstPageText="第一页" LastPageText="最后页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
        </asp:GridView>
        <div style="padding-top: 5px;">
            <table width="100%" cellpadding="5" cellspacing="0" class="border">
                <tr class="tdbg">
                    <td>
                        <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="CheckBox2_CheckedChanged" Text="选中本页显示的所有项目" />
                        <asp:Button ID="Button2" runat="server" Text="批量删除" OnClick="btnDeleteAll_Click"
            OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项从收藏夹删除吗？')}"
            CssClass="button" UseSubmitBehavior="true" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;搜索标题：<asp:TextBox ID="TxtSearchTitle" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="搜索" CssClass="button" OnClick="Button1_Click" />
                    </td>
                </tr>
            </table>
        </div>            
    </form>
</div>
</body>
</html>