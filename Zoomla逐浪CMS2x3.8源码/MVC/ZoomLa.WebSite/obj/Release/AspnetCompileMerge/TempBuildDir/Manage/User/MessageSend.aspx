<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MessageSend.aspx.cs" Inherits="ZoomLaCMS.Manage.User.MessageSend" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>发送信息</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="tdleft td_l"><strong>收件人：</strong></td>
            <td>
                <table id="TblAddMessage" width="100%" visible="true" runat="server">
                    <tr>
                        <td style="height: 22px;width:90px">
                            <asp:RadioButton ID="RadIncept1" runat="server" GroupName="InceptGroup"  Checked="true" Text="所有会员" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButton ID="RadIncept3" runat="server" Text="指定用户名" GroupName="InceptGroup" />
                        </td>
                        <td>
                            <div class="input-group" style="width: 290px;">
                                <asp:TextBox CssClass="form-control text_290" ID="InceptUser_T" runat="server" />
                                <span class="input-group-btn">
                                    <button type="button" onclick="user.sel('InceptUser', 'user', '')" class="btn btn-primary">选择用户</button>
                                </span>
                            </div>
                            <asp:HiddenField ID="InceptUser_Hid" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="tdleft td_l"><strong>抄送人：</strong></td>
            <td>
                <div class="input-group" style="width: 380px;">
                    <asp:TextBox CssClass="form-control text_300" ID="CCUser_T" runat="server" />
                    <span class="input-group-btn">
                        <button type="button" onclick="user.sel('CCUser', 'user', 'noplat|1')" class="btn btn-primary">选择用户</button>
                    </span>
                </div>                
                <asp:HiddenField ID="CCUser_Hid" runat="server" />
            </td>            
        </tr>
        <tr>
            <td class="tdleft"><strong>短消息主题：</strong></td>
            <td>
                <asp:TextBox class="form-control" Style="width:380px" ID="TxtTitle" runat="server" > </asp:TextBox>
                <span style="color:red;">*</span>
                <asp:RequiredFieldValidator ID="ValrTitle" runat="server" ControlToValidate="TxtTitle" ErrorMessage="短消息主题不能为空" Display="Dynamic"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>短消息内容：</strong></td>
            <td>
                <asp:TextBox ID="EditorContent" runat="server" TextMode="MultiLine" Width="700px"  Height="300px" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdleft"><strong>已上传文件：</strong><asp:HiddenField runat="server" ID="hasFileData" />
            </td>
            <td id="hasFileTD" runat="server"></td>
        </tr>
        <tr>
            <td class="tdleft"><strong>附件：</strong></td>
            <td>
                <input type="button" id="upfile_btn" class="btn btn-primary" value="选择文件" />
                <div style="margin-top: 10px;" id="uploader" class="uploader">
                    <ul class="filelist"></ul>
                </div>
                <asp:HiddenField runat="server" ID="Attach_Hid" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Send_Btn" runat="server" Text="发送" OnClick="Send_Btn_Click" OnClientClick="return SendConfirm();" class="btn btn-primary" />
                <asp:Button ID="Draft_Btn" runat="server" Text="存草稿" OnClick="Draft_Btn_Click" class="btn btn-primary" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" />
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
<script src="/Plugins/Ueditor/ueditor.config.js"  charset="utf-8"></script>
<script src="/Plugins/Ueditor/ueditor.all.js" charset="utf-8"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<%=Call.GetUEditor("EditorContent",2) %>
<script>
$(function () {
    $("#upfile_btn").click(ZL_Webup.ShowFileUP);
})
function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }
user.hook["InceptUser"] = userdeal;
user.hook["CCUser"] = userdeal;
function userdeal(list, select) {
    var names = "", ids = "";
    for (var i = 0; i < list.length; i++) {
        names += list[i].UserName + ",";
        ids += list[i].UserID + ",";
    }
    $("#" + select + "_T").val(names);
    $("#" + select + "_Hid").val(ids);
    if (comdiag != null) { CloseComDiag(); }
}
function SendConfirm() {
    rece = $("#InceptUser_T").val();
    if (rece == "") { alert('未选定收件人!'); return false; }
    title = $("#TxtTitle").val();
    if (title == "") { alert('邮件标题不能为空!'); return false; }
    if (confirm('确定要发送该邮件吗')) {
        disBtn(this, 3000); return true;
    } else {
        return false;
    }
}
</script>
</asp:Content>
