<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZoneStyleAdd.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.ZoneStyleAdd" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head"><title>模板管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="td_l tdleft">
                <strong>模板名称：</strong>
            </td>
            <td>
                <asp:TextBox ID="StyleName_T" class="form-control text_300" runat="server" MaxLength="20" />
            </td>
        </tr>
        <tr>
            <td class="tdleft">
                <strong>模板缩略图：</strong>
            </td>
            <td>
                <asp:TextBox ID="StylePic_T" class="form-control text_300" runat="server" Width="300px" />
                <span>填写模板图片路径</span>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>首页模板：</strong></td>
            <td>
                <asp:TextBox ID="UserIndexStyle_T" class="form-control text_300" runat="server" />
                <input type="button" value="选择模板" class="btn btn-primary" onclick="opentitle('../Template/TemplateList.aspx?OpenerText=' + escape('UserIndexStyle_T') + '&FilesDir=')" />
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="EBtnSubmit" Text="保存模板" class="btn btn-primary" runat="server" OnClick="EBtnSubmit_Click" />
                <a href="ZoneStyleManage.aspx" class="btn btn-primary">取消保存</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        function GV(id, value) { $("#" + id).val(value); }
        var TemDiag = new ZL_Dialog();
        function opentitle(url) {
            TemDiag.title = "选择模板";
            TemDiag.maxbtn = false;
            TemDiag.url = url;
            TemDiag.ShowModal();
        }
        function CloseDiag() { TemDiag.CloseModal(); }
        function Tlp_SetValByName(name, val) { $("#" + name).val(val); }
    </script>
</asp:Content>
