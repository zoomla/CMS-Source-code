<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CommentShow.aspx.cs" Inherits="ZoomLaCMS.Manage.I.ASCX.CommentShow" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>留言内容</title>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content  ContentPlaceHolderID="Content" Runat="Server">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>ID</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label ID="ID_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>发表人</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Literal ID="CUser_LB" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>所属内容ID</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label ID="CID_L" runat="server" data-show="onlyread"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>所属内容标题</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label ID="Title_L" runat="server" data-show="onlyread"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center; vertical-align: middle;"><strong>贴子内容</strong></td>
            <td class="tdbg" style="width: 85%;">
                 <asp:TextBox ID="MsgContent_T" runat="server" TextMode="MultiLine" Style="width: 90%; height: 200px;" />
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>发表时间</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:TextBox ID="CDate_T" runat="server" CssClass="form-control" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm'});"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="text-align: center;"><strong>操作</strong></td>
            <td class="tdbg">
                <asp:Button ID="Save_Btn" CssClass="btn btn-primary" data-show="edit" runat="server" OnClick="Save_Btn_Click" Text="保存" />
                <a href="CommentManage.aspx" class="btn btn-primary">返回</a>
                <input type="button" id="onlyread_btn" onclick="EditFunc('onlyread');" value="取消修改" class="btn btn-primary" style="display:none;" data-show="edit" />
            </td>
        </tr>
    </table>
    <%=Call.GetUEditor("MsgContent_T", 4) %>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        
        var userdiag = new ZL_Dialog();
        function ShowUserDiag(id) {
            userdiag.url = "../User/UserInfo.aspx?id=" + id;
            userdiag.title = "用户详情";
            userdiag.ShowModal();
        }
    </script>
</asp:Content>

