<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoveSpec.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="Manage_Content_MoveSpec" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>专题迁移</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="3" class="text-center">批量迁移专题</td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="Specs_List" runat="server" CssClass="form-control text_300" DataTextField="SpecName" DataValueField="SpecID" Height="282px" SelectionMode="Multiple"></asp:ListBox>
            </td>
            <td></td>
            <td>
                <asp:ListBox ID="TagetSpecs_List" runat="server" CssClass="form-control text_300" DataTextField="SpecName" DataValueField="SpecID" Height="282px"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td colspan="3"><span style="color:green;"><i class="fa fa-lightbulb-o"></i> 专题与文章互为内容的经纬关系，专题合并后，并不会更改原有专题关系，请谨慎操作。</span></td>
        </tr>
        <tr>
            <td class="text-center" colspan="3">
                <asp:Button ID="MoveSpecs_Btn" runat="server" CssClass="btn btn-primary" Text="迁移专题" OnClick="MoveSpecs_Btn_Click"/>
                <a href="SpecialManage.aspx" class="btn btn-primary">返回专题</a>
            </td>
        </tr>
    </table>
</asp:Content>


