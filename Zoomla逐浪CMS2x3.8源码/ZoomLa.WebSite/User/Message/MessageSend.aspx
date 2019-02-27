<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="MessageSend.aspx.cs" Inherits="ZoomLa.WebSite.User.MessageSend" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>发送短消息</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="pub"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li><a href="Message.aspx">消息中心</a></li>
            <li class="active">发送短消息</li>
        </ol>
    </div>
    <div class="container">
        <div class="btn-group btn_green">
            <a href="MessageSend.aspx" class="btn btn-primary">新消息</a>
            <a href="Message.aspx" class="btn btn-primary">收件箱</a>
            <a href="MessageOutbox.aspx" class="btn btn-primary">发件箱</a>
            <a href="MessageDraftbox.aspx" class="btn btn-primary">草稿箱</a>
            <a href="MessageGarbagebox.aspx" class="btn btn-primary">垃圾箱</a>
            <a href="Mobile.aspx" class="btn btn-primary">手机短信</a>
        </div>
        <div class="us_seta btn_green">
            <table class="table_li table-border">
                <tr>
                    <td class="text-right td_m">收件人</td>
                    <td class="tdRight">
                        <div class="input-group" style="width: 380px;">
                            <asp:TextBox CssClass="form-control text_300" ID="TxtInceptUser" runat="server" />
                            <asp:HiddenField ID="TxtInceptUser_Hid" runat="server" />
                            <span class="input-group-btn">
                                <button type="button" onclick="ShowSelUser(1)" class="btn btn-primary">选择用户</button>
                            </span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="text-right">抄送人</td>
                    <td class="tdRight">
                        <div class="input-group" style="width: 380px;">
                            <asp:TextBox CssClass="form-control text_300" ID="ccUser_T" runat="server" />
                            <span class="input-group-btn">
                                <button type="button" onclick="ShowSelUser(2)" class="btn btn-primary">选择用户</button>
                            </span>
                        </div>
                        <asp:HiddenField ID="ccUser_Hid" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="text-right">邮件标题</td>
                    <td class="tdRight">
                        <asp:TextBox ID="TxtTitle" CssClass="form-control text_300" runat="server" /></td>
                </tr>
                <tr>
                    <td class="text-right">内容</td>
                    <td>
                        <asp:TextBox ID="EditorContent" runat="server" TextMode="MultiLine" style="height:240px;width:100%;"/>
                    </td>
                </tr>
                <tr id="hasFileTR" runat="server" visible="true">
                    <td class="text-right">已上传文件<asp:HiddenField runat="server" ID="hasFileData" />
                    </td>
                    <td id="hasFileTD" runat="server"></td>
                </tr>
                <tr>
                    <td class="text-right">附件</td>
                    <td>
                        <input type="button" id="upfile_btn" class="btn btn-primary" value="选择文件" />
                        <div style="margin-top: 10px;" id="uploader" class="uploader">
                            <ul class="filelist"></ul>
                        </div>
                        <asp:HiddenField runat="server" ID="Attach_Hid" />
                    </td>
                </tr> 
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="Send_Btn" runat="server" Text="发送" OnClick="Send_Btn_Click" OnClientClick="return SendConfirm();" class="btn btn-primary" />
                        <asp:Button ID="Draft_Btn" runat="server" Text="存草稿" OnClick="Draft_Btn_Click" class="btn btn-primary" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="alert alert-success">
            <i class="fa fa-lightbulb-o"></i>
            提示：系统支持以MessageSend.aspx?name=[用户名]&content=[内容]方式get将站内信发送,如:MessageSend.aspx?name=admin&content=站内信
        </div>
    </div>

</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
<script src="/Plugins/Ueditor/ueditor.config.js"  charset="utf-8"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js" charset="utf-8"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<%=Call.GetUEditor("EditorContent",2) %>
<script>
$(function () {
    $("#upfile_btn").click(ZL_Webup.ShowFileUP);
})
function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }
function ShowSelUser(n) {
    var url = "";
    if (n == 1)
        url = "/Mis/OA/Mail/SelGroup.aspx?Type=AllInfo#ReferUser";
    else
        url = "/Mis/OA/Mail/SelGroup.aspx?Type=AllInfo#CCUser";
    comdiag.maxbtn = false;
    ShowComDiag(url, "选择用户");
}
function UserFunc(json, select) {
    var uname = "";
    var uid = "";
    for (var i = 0; i < json.length; i++) {
        uname += json[i].UserName + ",";
        uid += json[i].UserID + ",";
    }
    if (uid) uid = uid.substring(0, uid.length - 1);
    if (select == "ReferUser") {
        $("#TxtInceptUser").val(uname);
        $("#TxtInceptUser_Hid").val(uid);
    }
    if (select == "CCUser") {
        $("#ccUser_T").val(uname);
        $("#ccUser_Hid").val(uid);
    }
    CloseComDiag();
}
function SendConfirm() {
    rece = $("#TxtInceptUser").val();
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
