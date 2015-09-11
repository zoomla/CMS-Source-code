<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DicManage.aspx.cs" Inherits="manage_Plus_DicManage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div id="DIV1">
        <table style="width: 394px; height: 49px" id="TABLE1">
            <tr>
                <td colspan="4" style="height: 113px">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowEditing="GridView1_RowEditing" OnRowCommand="GridView1_RowCommand" Height="136px" Width="394px" DataKeyNames="CateID" OnPageIndexChanging="GridView1_PageIndexChanging">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <asp:CheckBox ID="nameCheckBox" runat="server" Text='<%# Eval("CateID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="分类名">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server"  Text='<%# Eval("CateName") %>' CommandArgument='<%# Eval("CateID")%>'   CommandName="mylink" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        &nbsp;<asp:Button ID="xiugai" runat="server" CommandName="myupdate" Text="修改" CommandArgument='<%# Eval("CateID") %>' />&nbsp;
                        <asp:Button ID="shanchu" runat="server" CommandName="mydelete" Text="删除" CommandArgument='<%# Eval("CateID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="width: 1px; height: 37px">
                    <asp:CheckBox ID="CheckBox1" OnCheckedChanged="CheckBox1_CheckedChanged" runat="server" Text="全选" Height="1px" Width="60px" AutoPostBack="True" />
                    </td>
                <td style="width: 1px; height: 37px">
                    <asp:Button ID="Button2" runat="server" Text="删除所选" OnClick="Button2_Click" OnClientClick="return confirm('是否删除全部所选内容');" /></td>
                <td style="width: 1px; height: 37px">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                <td style="width: 100px; height: 37px">
                    &nbsp;
                    <asp:Button ID="Button1" runat="server" Text="添加分类" OnClick="Button1_Click" /></td>
            </tr>
        </table>
        <br />
        </div>
   </form>
</body>
</html>
