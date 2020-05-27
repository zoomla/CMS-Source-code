<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddToSpec.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.AddToSpec" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加到其他专题</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="r_navigation">后台管理 &gt;&gt; 系统设置 &gt;&gt; 专题内容管理 &gt;&gt; 添加到其他专题</div>
    <table class="table table-striped table-bordered table-hover">
        <tr align="center">
            <td colspan="2" class="spacingtitle">添加内容到专题
            </td>
        </tr>
        <tr>
            <td>
                <strong>选定的内容ID：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtGeneralID" CssClass="form-control" runat="server" Width="280px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <strong>添加到目标专题：</strong>
                <br />
                <span style="color: Red">提示：</span>可以按住“Shift”<br />
                或“Ctrl”键进行多个专题的选择
            </td>
            <td>
                <asp:ListBox ID="LstSpec" CssClass="form-control" runat="server" Rows="10" SelectionMode="Multiple" Width="280px"></asp:ListBox>
                <br />
                <input id="Button1" onclick="SelectAll()" type="button" class="btn btn-primary" style="width: 134px;" value="  选定所有专题  " />
                <input id="Button2" onclick="UnSelectAll()" type="button" class="btn btn-primary" style="width: 135px;" value="取消选定所有专题" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <asp:Button ID="BtnBacthSet" class="btn btn-primary" Style="width: 110px;" runat="server" Text="执行批处理" OnClick="BtnBacthSet_Click" />&nbsp;&nbsp;
			<asp:Button ID="BtnCancel" class="btn btn-primary" Style="width: 110px;" runat="server" Text="取消" OnClick="BtnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function SelectAll() {
            for (var i = 0; i < document.getElementById('<%=LstSpec.ClientID%>').length; i++) {
                document.getElementById('<%=LstSpec.ClientID%>').options[i].selected = true;
            }
        }
        function UnSelectAll() {
            for (var i = 0; i < document.getElementById('<%=LstSpec.ClientID%>').length; i++) {
                document.getElementById('<%=LstSpec.ClientID%>').options[i].selected = false;
            }
        }
    </script>
</asp:Content>
