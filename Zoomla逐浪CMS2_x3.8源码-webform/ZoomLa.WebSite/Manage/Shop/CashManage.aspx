<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashManage.aspx.cs" Inherits="manage_Shop_CashManage" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>VIP卡管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td width="10%" class="title">
                
            </td>
            <td width="10%" class="title">ID</td>
            <td width="10%" class="title">银行</td>
            <td width="30%" class="title">帐号</td>
            <td width="10%" class="title">申请金额</td>
            <td width="10%" class="title">状态</td>
            <td width="30%" class="title">操作</td>
        </tr>
        <asp:Repeater ID="gvCard" runat="server">
            <ItemTemplate>
                <tr>
                    <td height="22" align="center">
                        <input name="Item" type="checkbox" value='<%# Eval("Y_ID")%>' /></td>
                    <td height="22" align="center"><%# Eval("Y_ID")%></td>
                    <td height="22" align="center"><%# Eval("Bank")%></td>
                    <td height="22" align="center"><%# Eval("Account")%></td>
                    <td height="22" align="center"><%#showMoney(Eval("money").ToString())%></td>
                    <td height="22" align="center"><%#shoyState(Eval("yState").ToString())%></td>
                    <td height="22" align="center"><%#showuse(Eval("Y_ID").ToString()) %>
                        <a href="CashManage.aspx?menu=del&id=<%#Eval("Y_ID") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td style="text-align:center"><asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" /></td>
            <td height="22" colspan="6" align="center">
                共
                <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                个商品
                <asp:Label ID="Toppage" runat="server" Text="" />
                <asp:Label ID="Nextpage" runat="server" Text="" />
                <asp:Label ID="Downpage" runat="server" Text="" />
                <asp:Label ID="Endpage" runat="server" Text="" />
                页次：
                <asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server" Text="" />页
                <asp:Label ID="pagess" runat="server" Text="" />个商品/页 转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                页
            </td>
        </tr>
    </table>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" OnClick="Button3_Click" />
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

