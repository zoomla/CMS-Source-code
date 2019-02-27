<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MsgInfo.aspx.cs" Inherits="Manage_User_MsgInfo" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>详情信息</title>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>ID</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label ID="ID_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>发送人</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Literal ID="CUser_LB" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>接收人</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Literal ID="ReceUser_LB" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center; vertical-align: middle;"><strong>内容</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label runat="server" ID="MsgContent_L" data-show="onlyread"></asp:Label>
                <div style="display: none;" data-show="edit">
                    <asp:TextBox ID="MsgContent_T" runat="server" TextMode="MultiLine" Style="width: 90%; height: 200px;" />
                </div>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="width: 100px; text-align: center;"><strong>创建时间</strong></td>
            <td class="tdbg" style="width: 85%;">
                <asp:Label ID="CDate_L" runat="server" data-show="onlyread"></asp:Label>
                <asp:TextBox ID="CDate_T" runat="server" data-show="edit" Style="display:none" onfocus="WdatePicker({dateFmt:'yyyy/MM/dd HH:mm'});"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdbgleft" style="text-align: center;"><strong>操作</strong></td>
            <td class="tdbg">
                <input type="button" id="edit_btn" onclick="EditFunc('edit');" data-show="onlyread" value="修改" class="btn btn-primary" />
                <asp:Button ID="Save_Btn" Style="display:none" CssClass="btn btn-primary" data-show="edit" runat="server" OnClick="Save_Btn_Click" Text="保存" />
                <input type="button" id="onlyread_btn" onclick="EditFunc('onlyread');" value="取消修改" class="btn btn-primary" style="display:none;" data-show="edit" />
                <a href="MsgEx.aspx" class="btn btn-primary">返回上一页</a>
            </td>
        </tr>
    </table>
    <%=Call.GetUEditor("MsgContent_T", 4) %>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        function EditFunc(flag) {
            $("[data-show=onlyread]").toggle();
            $("[data-show=edit]").toggle();
        }
        var userDiag = new ZL_Dialog();
        function ShowUInfo(uid) {
            userDiag.title = "用户信息";
            userDiag.reload = true;
            userDiag.url = "Userinfo.aspx?id=" + uid;
            userDiag.ShowModal();
        }
    </script>
</asp:Content>
