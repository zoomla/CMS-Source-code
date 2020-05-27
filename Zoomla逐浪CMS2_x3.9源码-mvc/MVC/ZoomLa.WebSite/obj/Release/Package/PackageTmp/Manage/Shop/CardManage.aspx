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
        <asp:Repeater ID="gvCard" runat="server" >
            <ItemTemplate>
                <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td height="22"  align="center">
                        <input name="Item" type="checkbox" value='<%# Eval("Card_ID")%>' />
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
        </asp:Repeater>
        <tr class="tdbg">
            <td style="text-align:center"><asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
            <td colspan="7" align="center">
                共
                <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                个商品
                <asp:Label ID="Toppage" runat="server" Text="" />
                <asp:Label ID="Nextpage" runat="server" Text="" />
                <asp:Label ID="Downpage" runat="server" Text="" />
                <asp:Label ID="Endpage" runat="server" Text="" />
                页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server"
                    Text="" />页
                <asp:Label ID="pagess" runat="server" Text="" />个商品/页 转到第<asp:DropDownList ID="DropDownList1"
                    runat="server" AutoPostBack="True">
                </asp:DropDownList>
                页
            </td>
        </tr>
    </table>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" OnClick="Button3_Click" />
        <asp:Button ID="Button4" class="btn btn-primary" runat="server" Text="开启卡片" OnClick="Button4_Click" />
        <asp:Button ID="Button5" class="btn btn-primary" runat="server" Text="关闭卡片" OnClick="Button5_Click" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
    </script>
</asp:Content>