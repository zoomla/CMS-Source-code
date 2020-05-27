<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopScutcheon.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.ShopScutcheon"  MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>品牌管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr class="text-center">
            <td style="width:105px;"></td>
            <td style="width:253px;">品牌名称</td>
            <td style="width:81px;">品牌分类</td>
            <td style="width:51px;">已启用</td>
            <td style="width:75px;"><span>属性</span></td>
            <td style="width:304px;"><span></span><span class="tdbgleft">操作</span></td>
        </tr>
        <ZL:ExRepeater ID="RPT" PageSize="10" runat="server" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='5' class='text-center'>" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr ondblclick="editScutcheon('<%# Eval("id") %>')">
                    <td>
                        <input type="checkbox" name="idchk" value="<%#Eval("id") %>" /></td>
                    <td><%#Eval("Trname") %></td>
                    <td><%#Eval("TrClass")%></td>
                    <td><%#showstop2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
                    <td><%#showtop2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%> <%#showjian2(DataBinder.Eval(Container, "DataItem.id", "{0}"))%></td>
                    <td>&nbsp; <a href="AddShopBrand.aspx?menu=edit&id=<%#Eval("id") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                        <a href="?menu=delete&id=<%#Eval("id") %>" onclick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" class="option_style"><i class="fa fa-trash-o" title="删除"></i></a>
                         <%#showstop(DataBinder.Eval(Container, "DataItem.id", "{0}"))%> 
                        <%#showtop(DataBinder.Eval(Container, "DataItem.id", "{0}"))%> 
                        <%#showjian(DataBinder.Eval(Container, "DataItem.id", "{0}"))%> 
                    </td>
                </tr>
            </ItemTemplate>
        </ZL:ExRepeater>
        <tr>
            <td colspan="6">
                <asp:Button ID="Button1" class="btn btn-primary" Text="删除选中品牌" runat="server" OnClick="Button1_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "Item");
            });
        })
        function editScutcheon(id) {
            location.href = "AddShopBrand.aspx?menu=edit&id=" + id;
        }
    </script>
</asp:Content>
