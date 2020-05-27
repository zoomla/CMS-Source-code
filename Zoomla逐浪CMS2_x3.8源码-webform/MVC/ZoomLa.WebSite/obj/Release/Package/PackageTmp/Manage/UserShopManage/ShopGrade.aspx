<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShopGrade.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.ShopGrade" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>等级管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td style="width:3%"></td>
            <td style="width:10%"><span>等级名称</span></td>
            <td style="width:10%"><span>等级别名</span></td>
            <td style="width:25%"><span>等级图片</span></td>
            <td style="width:21%"><span>积分</span></td>
            <td style="width:10%"><span>启用状态</span></td>
            <td style="width:10%"><span>等级类型</span></td>
            <td style="width:12%"><span>操作</span></td>

        </tr>
        <ZL:ExRepeater ID="RPT" PageSize="10" runat="server" PagePre="<tr><td><input type='checkbox' id='chkAll'/></td><td colspan='7' class='text-center'>" PageEnd="</td></tr>">
            <ItemTemplate>
                <tr class="text-center" ondblclick="editGrade('<%# Eval("ID") %>')">
                    <td>
                        <input name="idchk" type="checkbox" value='<%#Eval("ID") %>' />
                    </td>
                    <td>
                        <%#Eval("GradeName")%></td>
                    <td>
                        <%#Eval("OtherName")%></td>
                    <td>
                        <%#GetIcon(DataBinder.Eval(Container, "DataItem.Gradeimg", "{0}"),Eval("Imgnum","{0}"))%>
                    </td>
                    <td>
                        <%#Eval("CommentNum")%></td>
                    <td>
                        <%#Getture(Eval("Istrue","{0}"))%></td>
                    <td>
                        <%#Gettpye(Eval("GradeType","{0}"))%></td>
                    <td>
                        <a href="AddShopGrades.aspx?menu=edit&id=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>　
                        <a href="?menu=del&id=<%#Eval("ID") %>" onclick="return confirm('不可恢复性删除数据,确定将该数据删除?');" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a></td>
                </tr>
                </tr>
            </ItemTemplate>
        </ZL:ExRepeater>
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

