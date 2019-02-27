<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateHtmlManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.CreateHtmlManage" %>

<!DOCTYPE HTML>
<html>
<head id="Head1" runat="server">
    <title>VIP卡管理</title>
    <style type="text/css">
        .style3 { background: #e0f7e5; line-height: 120%; }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="r_navigation">
            <span>后台管理</span> &gt;&gt; 
        </div>
        <div class="clearbox"></div>
        <div class="r_navigation">
            &nbsp; &nbsp;选择节点内容生成：
            <asp:DropDownList ID="DropDownList2" runat="server" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged"></asp:DropDownList>
        </div>
        <div class="clearbox">
        </div>
        <div style="text-align: center">
        </div>
        <table class="border" style="margin: 0 auto;">
            <tr class="text-center">
                <td style="width:10%" class="title"></td>
                <td style="width:50%" class="title">文件名</td>
                <td style="width:40%" class="title">操作</td>
            </tr>
            <ZL:ExRepeater ID="gvCard" runat="server" PageSize="10" PagePre="<tr id='page_tr'><td><input type='checkbox' id='chkAll'/></td><td colspan='2' id='page_td'>" PageEnd="</td></tr>">
                <ItemTemplate>
                    <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                        <td class="text-center"><input name="idchk" type="checkbox" value='<%#Eval("ID")%>' /></td>
                        <td class="text-left"><%# Eval("Title")%></td>
                        <td class="text-center"><a href="<%# Eval("url")%>" target="_blank">查看文件</a>&nbsp;&nbsp;&nbsp;<a href="/" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');">删除文件</a></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate></FooterTemplate>
            </ZL:ExRepeater>
        </table>
        <div>
            <asp:Button ID="Button3" class="C_input" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" OnClick="Button3_Click" />
        </div>
    </form>
</body>
</html>
