<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="ZoomLa.WebSite.User.Message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>会员中心 >> 短消息</title>
    <link href="/App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="/App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="../css/user.css" rel="stylesheet" type="text/css" />
    <link href="../css/default1.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
<div>
    <div class="r_navigation">
    <div class="r_n_pic"></div>
    您现在的位置：<span id="YourPosition"><span><a title="网站首页" href="/"><asp:Label ID="LblSiteName" runat="server" Text="Label"></asp:Label></a></span><span> &gt;&gt; </span><span><a title="会员中心" href="/User/Default.aspx">会员中心</a></span><span> &gt;&gt; </span><span><a title="短消息管理" href="/User/Message/Message.aspx">收件箱</a></span></span></div>   
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing"
                  Width="100%" AllowPaging="True" DataKeyNames="MsgID" PageSize="10" OnRowCommand="Row_Command" OnPageIndexChanging="GridView1_PageIndexChanging">
        <Columns>
        　　<asp:TemplateField HeaderText="选择" HeaderStyle-Width="5%" ItemStyle-HorizontalAlign="center">
        　　　<ItemTemplate>
        　　　   <asp:CheckBox ID="cheCk" runat="server" />
        　　　</ItemTemplate>
        　　</asp:TemplateField>
            <asp:BoundField DataField="Title" HeaderText="短消息主题" HeaderStyle-Width="25%" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField DataField="Incept" HeaderText="收件人" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField  DataField="status" HeaderText="收件人状态" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField  DataField="Sender" HeaderText="发件人" HeaderStyle-Width="10%" ItemStyle-HorizontalAlign="center" />
            <asp:BoundField DataField="PostDate" HeaderText="发送日期" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="center" />
            <asp:TemplateField HeaderText="操作" HeaderStyle-Width="20%" ItemStyle-HorizontalAlign="center">
              <ItemTemplate>
                 <asp:LinkButton ID="btnDel" runat="server" CommandName="DeleteMsg" OnClientClick="if(!this.disabled) return confirm('确实要删除此信息吗？');"
                        CommandArgument='<%# Eval("MsgID")%>' Text="删除"></asp:LinkButton>
                  <asp:LinkButton ID="lbRead" runat="server" CommandName="ReadMsg" CommandArgument='<%# Eval("MsgID")%>'>阅读信息</asp:LinkButton>
              </ItemTemplate>
            </asp:TemplateField>
        </Columns>
          <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
          <RowStyle ForeColor="Black" BackColor="#DEDFDE" Height="25px" />
          <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
          <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
          <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" BorderStyle="None" Height="30px" Font-Overline="False" />
          <PagerSettings FirstPageText="第一页" LastPageText="最后页" Mode="NextPreviousFirstLast" NextPageText="下一页" PreviousPageText="上一页" />
    </asp:GridView>
    <asp:CheckBox ID="CheckBox2" runat="server" AutoPostBack="True" Font-Size="9pt" OnCheckedChanged="CheckBox2_CheckedChanged" Text="全选" />
    <asp:Button ID="Button1" runat="server" Font-Size="9pt" Text="取消" OnClick="Button1_Click" />
    <asp:Button ID="Button2" runat="server" Font-Size="9pt" Text="批量删除" OnClick="Button2_Click" />
    </div>
  </form>
</body>
</html>
