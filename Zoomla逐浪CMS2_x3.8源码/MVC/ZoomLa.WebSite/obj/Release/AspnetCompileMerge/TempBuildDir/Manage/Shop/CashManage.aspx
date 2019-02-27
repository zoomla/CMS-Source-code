<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CashManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.CashManage"  MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>VIP卡管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr class="text-center">
            <td style="width:10%"></td>
            <td style="width:10%">ID</td>
            <td style="width:10%">银行</td>
            <td style="width:30%">帐号</td>
            <td style="width:10%">申请金额</td>
            <td style="width:10%">状态</td>
            <td style="width:30%">操作</td>
        </tr>
        <ZL:ExRepeater ID="Cash_RPT" PageSize="10" runat="server" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='6' class='text-center'>" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr class="text-center">
                    <td><input name="idchk" type="checkbox" value='<%# Eval("Y_ID")%>' /></td>
                    <td><%# Eval("Y_ID")%></td>
                    <td><%# Eval("Bank")%></td>
                    <td><%# Eval("Account")%></td>
                    <td><%#showMoney(Eval("money").ToString())%></td>
                    <td><%#shoyState(Eval("yState").ToString())%></td>
                    <td><%#showuse(Eval("Y_ID").ToString()) %>
                        <a href="CashManage.aspx?menu=del&id=<%#Eval("Y_ID") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                    </td>
                </tr>
            </ItemTemplate>
        </ZL:ExRepeater>
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

