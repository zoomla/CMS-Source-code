<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrademarkManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.TrademarkManage"  MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>品牌管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tbody id="Tbody1">
            <tr class="tdbg">
                <td width="105" height="24" align="center" class="title">
                    <span class="tdbgleft">
                        <asp:CheckBox ID="CheckBox1" onclick="javascript:CheckAll(this);" runat="server" />
                    </span>
                </td>
                <td width="253" align="center" class="title">
                    品牌名称
                </td>
                <td width="81" align="center" class="title">
                    品牌分类
                </td>
                <td width="51" align="center" class="title">
                    已启用
                </td>
                <td width="75" align="center" class="title">
                    <span class="tdbgleft">属性</span>
                </td>
                <td width="304" align="center" class="title">
                    <span class="tdbgleft"></span><span class="tdbgleft">操作</span>
                </td>
            </tr>
            <asp:Repeater ID="Trademarklist" runat="server">
                <ItemTemplate>
                    <tr class="tdbg" id="<%#Eval("id") %>" ondblclick="getinfo(this.id)">
                        <td height="24" align="center">
                            <input type="checkbox" name="Item" value="<%#Eval("id") %>" />
                        </td>
                        <td height="24" align="center">
                            <%#Eval("Trname") %>
                        </td>
                        <td height="24" align="center">
                            <%#Eval("TrClass")%>
                        </td>
                        <td height="24" align="center" class="segtext">
                            <%#showstop2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                        </td>
                        <td height="24" align="center" class="segtext">
                            <%#showtop2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                            <%#showjian2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                        </td>
                        <td height="24" align="center">
                            &nbsp; <a href="Trademark.aspx?menu=edit&id=<%#Eval("id") %>" class="option_style"> <i class="fa fa-pencil" title="修改"></i></a>
                            <a href="?menu=delete&id=<%#Eval("id") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" class="option_style"><i class="fa fa-trash-o" title="删除"></i></a>
                            <%#showstop(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                            <%#showtop(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                            <%#showjian(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr class="tdbg">
                <td height="24" colspan="6" align="center" class="tdbgleft">
                    共
                    <asp:Label ID="Allnum" runat="server" Text=""></asp:Label>
                    条数据
                    <asp:Label ID="Toppage" runat="server" Text="" />
                    <asp:Label ID="Nextpage" runat="server" Text="" />
                    <asp:Label ID="Downpage" runat="server" Text="" />
                    <asp:Label ID="Endpage" runat="server" Text="" />
                    页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server"
                        Text="" />页
                    <asp:Label ID="pagess" runat="server" Text="" />条数据/页 转到第<asp:DropDownList ID="DropDownList1"
                        runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    页
                </td>
            </tr>
            <tr>
                <td height="24" colspan="6">
                    <asp:Button ID="Button1" Text="删除选中品牌" class="btn btn-primary" Style="width: 110px;" runat="server"
                        OnClick="Button1_Click" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        function getinfo(id) {
            location.href = "Trademark.aspx?menu=edit&id=" + id + "";
        }
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