<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false" CodeFile="WaterMarkConfig.aspx.cs" Inherits="manage_Config_WaterMarkConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>SiteInfo</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label3" runat="server" Text="网站水印配置"></asp:Label><br />
        <br />
        <table border="1">
            <tr>
                        <td style="width: 300px">
                            <strong>文本：</strong></td>
                        <td style="width: 500px">
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                                <asp:ListItem>类型1</asp:ListItem>
                                <asp:ListItem>类型2</asp:ListItem>
                                <asp:ListItem>类型3</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
        </table>
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="保存文本" OnClick="Button1_Click1" /><br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="水印文本配置"></asp:Label><br />
        <br />
                <table border="1" >
                    <tr>
                        <td style="width: 300px">
                            <strong>字体的文本：</strong></td>
                        <td style="width: 500px">
                            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 300px">
                            <strong>字体的类型：</strong></td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 300px; ">
                            <strong>字体的大小：</strong></td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 300px; height: 23px;">
                            <strong>字体的颜色：</strong></td>
                        <td style="width: 100px; height: 23px;">
                            <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 300px">
                            <strong>字体样式：</strong></td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextBox6" runat="server" ></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 300px">
                            <strong>边框颜色：</strong></td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 300px">
                            <strong>是否显示边框：</strong></td>
                        <td style="width: 100px">
                            <asp:RadioButtonList ID="RadioButtonList2" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem>是</asp:ListItem>
                                <asp:ListItem>否</asp:ListItem>
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td style="width: 300px">
                            <strong>水印文本位置：</strong></td>
                        <td style="width: 100px">
                            <asp:RadioButtonList ID="RadioButtonList3" runat="server">
                                <asp:ListItem>WM_TOP_LEFT</asp:ListItem>
                                <asp:ListItem>WM_TOP_CENTER</asp:ListItem>
                                <asp:ListItem>WM_TOP_RIGHT</asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                    </tr>
                    <tr>
                        <td style="width: 300px">
                            <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; X 坐标：</strong></td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 300px">
                            <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; Y 坐标：</strong></td>
                        <td style="width: 100px">
                            <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox></td>
                    </tr>
                    
                </table>
        <br />
            
        <asp:Button ID="Button2" runat="server" Text="保存水印文本设置" OnClick="Button2_Click" /><br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="水印图片配置"></asp:Label><br />
        <br />
        <table border="1">
            <tr>
                <td style="width: 300px">
                    <strong>图片路径：</strong></td>
                <td style="width: 500px">
                    <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 300px">
                    <strong>水印文本位置：</strong></td>
                <td style="width: 100px">
                    <asp:RadioButtonList ID="RadioButtonList4" runat="server">
                        <asp:ListItem>WM_TOP_LEFT</asp:ListItem>
                        <asp:ListItem>WM_TOP_CENTER</asp:ListItem>
                        <asp:ListItem>WM_TOP_RIGHT</asp:ListItem>
                    </asp:RadioButtonList></td>
            </tr>
            <tr>
                <td style="width: 300px">
                    <strong>&nbsp; &nbsp; &nbsp; &nbsp; X 坐标：</strong></td>
                <td style="width: 100px">
                    <asp:TextBox ID="TextBox14" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 300px; height: 23px">
                    <strong>&nbsp; &nbsp; &nbsp; &nbsp; Y 坐标：</strong></td>
                <td style="width: 100px; height: 23px">
                    <asp:TextBox ID="TextBox15" runat="server"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <asp:Button ID="Button3" runat="server" Text="保存水印图片配置" OnClick="Button3_Click" /><br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;<br />
        <br />
        &nbsp;</div>
    </form>
</body>
</html>
