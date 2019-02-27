<%@ Page Language="C#" Title="发送短信息"　AutoEventWireup="true" CodeFile="Message.aspx.cs" Inherits="User.Message" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>编辑短信息</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border" align="center">
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>批量删除操作</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 28px; width: 45%;">
                <strong>批量删除会员（发件人）短消息：<br />
                </strong>可以用英文状态下的逗号将用户名隔开实现多会员同时删除</td>
            <td style="height: 28px; width: 511px;">
                <asp:TextBox ID="TxtSender" runat="server"></asp:TextBox>
                <asp:Button ID="BtnDelSender" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗？');"
                    OnClick="BtnDelSender_Click" /></td>
        </tr>
        <tr class="tdbg">
            <td align="right" style="height: 28px; width: 45%;">
                <strong>批量删除指定日期范围内的短消息：<br />
                </strong>默认为删除已读信息</td>
            <td style="height: 28px; width: 511px;">
                <asp:DropDownList ID="DropDelDate" runat="server">
                    <asp:ListItem Value="1">一天前</asp:ListItem>
                    <asp:ListItem Value="3">三天前</asp:ListItem>
                    <asp:ListItem Value="7">一星期前</asp:ListItem>
                    <asp:ListItem Value="30">一个月前</asp:ListItem>
                    <asp:ListItem Value="60">两个月前</asp:ListItem>
                    <asp:ListItem Value="180">半年前</asp:ListItem>
                    <asp:ListItem Value="0">所有短消息</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="BtnDelDate" runat="server" Text="删除" OnClientClick="return confirm('确定要删除吗？');"
                    OnClick="BtnDelDate_Click" /></td>
        </tr>
     </table>
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
    <br />
   
    </div>
    </form>
</body>
</html>
