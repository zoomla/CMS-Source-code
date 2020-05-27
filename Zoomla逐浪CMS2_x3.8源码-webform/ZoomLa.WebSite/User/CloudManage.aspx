<%@ Page Title="" Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="CloudManage.aspx.cs" Inherits="User_Cloud_CloudManage" ClientIDMode="Static" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<style>
.cloud_dirdiag { width: 450px; }
.cloudmanage .hoverDiv a { display: inline-block; margin-left: 10px; }
</style>
<link href="/Plugins/Uploadify/style.css" rel="stylesheet" type="text/css" />
<title>网络云盘</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="cloud" data-ban="cloud"></div>
<div class="container u_cnt">
    <span runat="server" id="OpenCloud" visible="false"><i class="fa fa-warning"></i>您还没有云盘，请先
        <asp:LinkButton ID="Open" runat="server" OnClick="OpenCloud_Click" ForeColor="Red">开通云盘</asp:LinkButton>
    </span>
    <div class="cloudmanage margin_t10">
        <div id="Cloud" runat="server" visible="false">
            <div class="url_title" style="border: 1px solid #ddd; background-color: #f7f7f7; border-radius: 4px; padding: 8px;">
                <span id="navv" runat="server"></span>
            </div>
            <div class="clearfix"></div>
            <table class="table">
                <thead>
                    <tr>
                        <td style="width: 50%;">名称</td>
                        <td style="width: 15%;">大小</td>
                        <td style="width: 15%;">创建时间</td>
                        <td style="width: 20%;">操作</td>
                    </tr>
                </thead>
                <asp:Repeater ID="RptFiles" runat="server" OnItemCommand="RptFiles_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td class="text-left">
                                <div style="position: relative; bottom: -10px; display: inline-block;"><%# GetUrl()%></div>
                                <%#GetLink(Eval("FileType").ToString(),Eval("FileName").ToString()) %>
                            </td>
                            <td><%#GetSize(Eval("FileSize").ToString())%></td>
                            <td><%#Eval("CDate")%></td>
                            <td>
                                <%#GetDownUrl() %>
                                <asp:LinkButton CommandName='DelFile' CommandArgument='<%# Eval("Guid")%>' class="btn btn-sm btn-info" OnClientClick="return confirm('确定要删除吗');" runat="server"><i class='fa fa-trash-o' title='删除'></i></asp:LinkButton>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <asp:Literal runat="server" ID="MsgPage_L" EnableViewState="false"></asp:Literal>
        </div>
<%--        <div><%=CurrentDir %></div>
        <div><%=ZoomLa.Common.function.VToP(CurrentDir) %></div>--%>
        <div class="us_seta btn_green text-left margin_t10" id="us_seta" runat="server">
            <input type="button" class="btn btn-primary" name="demoCode04-3" value="上传文件" onclick="ShowFileDiag()" />
            <input type="button" class="btn btn-primary" id="newFile" value="新建文件夹" onclick="ShowDirDiag()" />
        </div>
        <div style="display: none;" id="NewDir_div">
            <input type="text" id="DirName_T" name="DirName_T" class="form-control text_300"/>
            <asp:Button ID="CreateDiv" runat="server" CssClass="btn btn-primary" OnClick="CreateDiv_Click" Text="确定" />
        </div>
        <div style="display: none;" id="File_div"><input type="file" id="files" /></div>
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/Plugins/Uploadify/jquery.uploadify.js"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    var DirDiag = new ZL_Dialog();
    function ShowDirDiag() {
        DirDiag.content = "NewDir_div";
        DirDiag.title = "新建目录";
        DirDiag.width = "cloud_dirdiag";
        DirDiag.ShowModal();
    }
    var FileDiag = new ZL_Dialog();
    function ShowFileDiag() {
        FileDiag.title = "文件上传";
        FileDiag.content = "File_div";
        FileDiag.width = "dirdiag";
        FileDiag.ShowModal();
        $("#files").uploadify({
            //按钮宽高
            width: 120,
            height: 35,
            auto: true,
            swf: '/Plugins/Uploadify/uploadify.swf',
            uploader: '/Plugins/Uploadify/UploadFileHandler.ashx',
            buttonText: "上传附件",
            buttonCursor: 'hand',
            fileTypeExts: "*.*",
            fileTypeDesc: "请选择文件",
            fileSizeLimit: "50000KB",
            formData: { "action": "Cloud_Doc", "value": "<%:VDir%>", "Type": "<%=Request["Type"] %>" },
            queueSizeLimit: 3,
            removeTimeout: 2,
            multi: true,
            onUploadStart: function (file) { },
            onUploadSuccess: function (file, data, response) {//json,result,tru||false
            },
            onQueueComplete: function (queueData) { location = location; },
            onUploadError: function (file) { }
        });
    }
    var prediag = new ZL_Dialog();
    function prefile(guid) {
        prediag.title = "预览文件";
        prediag.url = "/PreView.aspx?CloudFile=" + guid;
        prediag.maxbtn = false;
        prediag.ShowModal();
        $('.modal').css('top', '100px');
        $('.modal-body').css('height', '600px');
    }
</script>
</asp:Content>
