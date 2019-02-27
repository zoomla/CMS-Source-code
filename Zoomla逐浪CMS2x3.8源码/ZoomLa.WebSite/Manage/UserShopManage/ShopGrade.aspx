<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShopGrade.aspx.cs" Inherits="manage_UserShopManage_ShopGrade" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>等级管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td width="3%" align="center" class="title">
                <input id="chkAll" type="checkbox" />
            </td>
            <td width="10%" align="center" class="title"><span>等级名称</span></td>
            <td width="10%" align="center" class="title"><span>等级别名</span></td>
            <td width="25%" align="center" class="title"><span>等级图片</span></td>
            <td width="21%" align="center" class="title"><span>积分</span></td>
            <td width="10%" align="center" class="title"><span>启用状态</span></td>
            <td width="10%" align="center" class="title"><span>等级类型</span></td>
            <td width="12%" align="center" class="title"><span>操作</span></td>

        </tr>
        <asp:Repeater ID="GradeList" runat="server">
            <ItemTemplate>
                <tr ondblclick="editGrade('<%# Eval("ID") %>')">
                    <td align="center">
                        <input name="Item" type="checkbox" value='<%#Eval("ID") %>' />
                    </td>
                    <td align="center">
                        <%#Eval("GradeName")%></td>
                    <td align="center">
                        <%#Eval("OtherName")%></td>
                    <td align="center">
                        <%#GetIcon(DataBinder.Eval(Container, "DataItem.Gradeimg", "{0}"),Eval("Imgnum","{0}"))%>
                    </td>
                    <td align="center">
                        <%#Eval("CommentNum")%></td>
                    <td align="center">
                        <%#Getture(Eval("Istrue","{0}"))%></td>
                    <td align="center">
                        <%#Gettpye(Eval("GradeType","{0}"))%></td>
                    <td align="center">
                        <a href="AddShopGrades.aspx?menu=edit&id=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>　
                        <a href="?menu=del&id=<%#Eval("ID") %>" onclick="return confirm('不可恢复性删除数据,确定将该数据删除?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a></td>
                </tr>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="8" align="center" width="100%">共<asp:Label ID="Allnum" runat="server" Text=""></asp:Label>个&nbsp;
                    <asp:Label ID="Toppage" runat="server" Text="" />
                <asp:Label ID="Nextpage" runat="server" Text="" />
                <asp:Label ID="Downpage" runat="server" Text="" />
                <asp:Label ID="Endpage" runat="server" Text="" />
                页次：<asp:Label ID="Nowpage" runat="server" Text="" />/<asp:Label ID="PageSize" runat="server" Text="" />页 
                    <asp:Label ID="pagess" runat="server" Text="" />个/页
                    转到第<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"></asp:DropDownList>页
            </td>
        </tr>
        <tr>
            <td colspan="8" class="text-center">
                <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="设为启用" CommandName="1" OnClick="Button1_Click" />
                <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="设为停用" CommandName="0" OnClick="Button2_Click" />
                <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary" Text="批量删除" CommandName="5" OnClientClick="return confirm('不可恢复性删除数据,你确定将该数据删除吗？');" OnClick="Button3_Click" />
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
        function editGrade(id) {
            location.href = "AddShopGrades.aspx?menu=edit&id=" + id;
        }
    </script>
</asp:Content>
