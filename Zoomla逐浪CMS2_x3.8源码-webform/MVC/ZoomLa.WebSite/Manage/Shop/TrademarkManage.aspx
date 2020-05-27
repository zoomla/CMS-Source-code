<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrademarkManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.TrademarkManage"  MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>品牌管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tbody id="Tbody1">
            <tr class="tdbg text-center">
                <td width="105">
                </td>
                <td width="253" >
                    品牌名称
                </td>
                <td width="81" >
                    品牌分类
                </td>
                <td width="51" >
                    已启用
                </td>
                <td width="75" >
                    <span class="tdbgleft">属性</span>
                </td>
                <td width="304" >
                    <span class="tdbgleft"></span><span class="tdbgleft">操作</span>
                </td>
            </tr>
            <ZL:ExRepeater ID="RPT" PageSize="10" runat="server" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='5' class='text-center'>" PageEnd="</td></tr>">
                <ItemTemplate>
                    <tr class="tdbg text-center" id="<%#Eval("id") %>" ondblclick="getinfo(this.id)">
                        <td>
                            <input type="checkbox" name="idchk" value="<%#Eval("id") %>" />
                        </td>
                        <td>
                            <%#Eval("Trname") %>
                        </td>
                        <td>
                            <%#Eval("TrClass")%>
                        </td>
                        <td class="segtext">
                            <%#showstop2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                        </td>
                        <td class="segtext">
                            <%#showtop2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                            <%#showjian2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                        </td>
                        <td>
                            &nbsp; <a href="Trademark.aspx?menu=edit&id=<%#Eval("id") %>" class="option_style"> <i class="fa fa-pencil" title="修改"></i></a>
                            <a href="?menu=delete&id=<%#Eval("id") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" class="option_style"><i class="fa fa-trash-o" title="删除"></i></a>
                            <%#showstop(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                            <%#showtop(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                            <%#showjian(DataBinder.Eval(Container, "DataItem.id", "{0}"))%>
                        </td>
                    </tr>
                </ItemTemplate>
            </ZL:ExRepeater>
            <tr>
                <tdcolspan="6">
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
    </script>
</asp:Content>