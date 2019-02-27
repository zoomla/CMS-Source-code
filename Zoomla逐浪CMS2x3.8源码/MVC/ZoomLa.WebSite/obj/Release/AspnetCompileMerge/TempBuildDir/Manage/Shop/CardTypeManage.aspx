<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CardTypeManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.CardTypeManage" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>VIP卡管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td width="10%" class="title">
                <asp:CheckBox ID="Checkall" onclick="javascript:CheckAll(this);" runat="server" />
            </td>
            <td width="10%" class="title">
                ID
            </td>
            <td width="40%" class="title">
                卡介绍
            </td>
            <td width="10%" class="title">
                折扣概率
            </td>
            <td width="30%" class="title">
                操作
            </td>
        </tr>
        <ZL:ExRepeater ID="gvCard" runat="server"  PageSize="10" PagePre="<tr id='page_tr'><td><input type='checkbox' id='chkAll'/></td><td colspan='5' id='page_td'>" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'" id='<%# Eval("c_id")%>' ondblclick="getinfo(this.id)">
                    <td height="22" align="center">
                        <input name="idchk" type="checkbox" value='<%# Eval("c_id")%>' />
                    </td>
                    <td height="22" align="center">
                        <%# Eval("c_id")%>
                    </td>
                    <td height="22" align="left">
                        <%# Eval("typename")%>
                    </td>
                    <td height="22" align="center">
                        <%#Eval("iscount")%>
                        <td height="22" align="center">
                            <a href="AddCardType.aspx?menu=edit&id=<%#Eval("c_id") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i>修改</a>
                            <a href="CardTypeManage.aspx?menu=del&id=<%#Eval("c_id") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a>
                        </td>
                </tr>
            </ItemTemplate>
            <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
    <div>
        <asp:Button ID="Button3" class="btn btn-primary" runat="server" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" Text="批量删除" OnClick="Button3_Click1" /></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
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
        function getinfo(id) {
            location.href = "AddCardType.aspx?menu=edit&id=" + id + "";
        }
        $().ready(function () {
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "idchk");
            });
        });
    </script>
</asp:Content>