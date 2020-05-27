<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddToSpecial.aspx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.AddToSpecial" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>批量添加内容到专题</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td colspan="2" class="text-center">批量添加内容到专题</td>
        </tr>
        <tr>
            <td style="width: 25%" align="left">
                <strong>内容ID：</strong>
            </td>
            <td class="bqright">
                <asp:TextBox ID="TxtContentID" CssClass="form-control" runat="server" Width="298px" Enabled="false"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 25%" align="left">
                <strong>目标专题：</strong><br />
                <font color="red">提示：</font>可以按住“Shift”<br />
                或“Ctrl”键进行多个专题的选择
            </td>
            <td class="bqright">
                <asp:ListBox ID="ListSpecial" CssClass="form-control" runat="server" Rows="10" Width="300px" SelectionMode="Multiple"></asp:ListBox>
                <br />
                <br />
                <input id="Button2" onclick="SelectAll()" type="button" class="btn btn-primary" style="width: 137px;" value="  选定所有专题  " />
                <input id="Button3" onclick="UnSelectAll()" type="button" class="btn btn-primary" style="width: 146px;" value="取消选定所有专题" />
            </td>
        </tr>
        <tr>
            <td class="text-center" colspan="2">
                <asp:Button ID="Button1" runat="server" class="btn btn-primary" Style="width: 100px;" Text="批量处理" OnClick="Button1_Click" />
                <input name="Cancel" type="button" id="BtnCancel" class="btn btn-primary" style="width: 100px;" value="取消" onclick="javascript: history.back()" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function SelectAll() {
            for (var i = 0; i < document.getElementById('ListSpecial').length; i++) {
                document.getElementById('ListSpecial').options[i].selected = true;
            }
        }
        function UnSelectAll() {
            for (var i = 0; i < document.getElementById('ListSpecial').length; i++) {
                document.getElementById('ListSpecial').options[i].selected = false;
            }
        }
    </script>
</asp:Content>
