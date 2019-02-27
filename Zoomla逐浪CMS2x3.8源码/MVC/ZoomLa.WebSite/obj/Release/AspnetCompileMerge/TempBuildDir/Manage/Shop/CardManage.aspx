<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.CardManage" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>VIP卡管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<%--    <div class="r_navigation">
        状态选择：<a href="CardManage.aspx">全部</a> <a href="?CardState=2">已处理</a> <a href="?CardState=1">
            未处理</a>
    </div>--%>
    <table id="EGV" class="table table-striped table-bordered table-hover">
        <tr align="center">
            <th width="10%" class="title">
                
            </th>
            <th width="10%" class="title">
                ID
            </th>
            <th width="15%" class="title">
                卡号
            </th>
            <th class="title" width="15%">
                卡密码
            </th>
            <th width="15%" class="title">
                发放用户
            </th>
            <th  width="10%" class="title">
                使用用户
            </th>
            <th  width="10%" class="title">
                卡片状态
            </th>
            <th width="15%" class="title">
                操作
            </th>
        </tr>
        <ZL:ExRepeater ID="gvCard" runat="server" PageSize="10" PagePre="<tr id='page_tr'><td><input type='checkbox' id='chkAll'/></td><td colspan='7' id='page_td'>" PageEnd="</td></tr>" >
            <ItemTemplate>
                <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td height="22"  align="center">
                        <input name="idchk" type="checkbox" value='<%# Eval("Card_ID")%>' />
                    </td>
                    <td align="center"><%# Eval("Card_ID")%></td>
                    <td align="center">
                        <%# Eval("CardNum")%>
                    </td>
                    <td align="center">
                        <%#Eval("CardPwd")%>
                    </td>
                    <td align="left">
                        <%#GetUserName(DataBinder.Eval(Container.DataItem ,"PutUserID").ToString()) %>
                    </td>
                    <td align="center">
                        <%#GetUserName(DataBinder.Eval(Container.DataItem ,"AssociateUserID").ToString()) %>
                    </td>
                    <td align="center">
                        <%#GetState(DataBinder.Eval(Container.DataItem, "CardState").ToString())%>
                        <td align="center">
                            <%#showuse(DataBinder.Eval(Container.DataItem, "Card_ID").ToString())%>
                            <a href="CardManage.aspx?menu=del&id=<%#Eval("Card_ID") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                        </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" OnClick="Button3_Click" />
        <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="开启卡片" OnClick="Button4_Click" />
        <asp:Button ID="Button5" class="btn btn-primary" runat="server" Text="关闭卡片" OnClick="Button5_Click" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "idchk");
            });
        });
    </script>
</asp:Content>