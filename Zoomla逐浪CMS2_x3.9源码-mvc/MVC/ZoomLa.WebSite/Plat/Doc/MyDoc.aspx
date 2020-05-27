<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyDoc.aspx.cs" Inherits="ZoomLaCMS.Plat.Doc.MyDoc"  MasterPageFile="~/Plat/Main.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>我的文库</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="首页" href="/">首页</a></li>
        <li><a title="能力中心" href="/Plat/Blog/">能力中心</a></li>
        <li class="active">我的文库</li>
    </ol>
    <div class="container platcontainer">
        <div style="margin-top: 10px; margin-bottom: 10px;">
            <span>文件路径：</span><span><a href="MyDoc.aspx" title="我的文库">我的文库</a>></span>
            <asp:Literal runat="server" ID="PathNav_L"></asp:Literal>
        </div>
        <div>
            <input type="button" onclick="$('#article_div').show();" value="撰写文章" class="btn btn-primary" />
            <input type="button" data-toggle="modal" data-target="#fileup_div" value="上传文件" class="btn btn-primary" />
            <input type="button" data-toggle="modal" data-target="#dir_div" value="新建文件夹" class="btn btn-primary" />
        </div>
        <table class="table">
            <thead>
                <tr>
                    <td style="width: 60%;">名称</td>
                    <td style="width: 20%;">大小</td>
                    <td style="width: 20%;">创建时间</td>
                </tr>
            </thead>
            <tbody id="FListBody">
                <asp:Repeater runat="server" ID="File_Rep">
                    <ItemTemplate>
                        <tr id="file_tr_<%#Eval("ID") %>" onmouseover="$(this).find('.hoverDiv').show();" onmouseout="$(this).find('.hoverDiv').hide();">
                            <td>
                                <div style="position: relative; bottom: -10px; display: inline-block;"><%#GroupPic.GetExtNameMini(GetExt()) %> </div>
                                <%#GetFileName() %> <span class="hoverDiv" style="display: none;">
                                    <input type="text" id="fname_<%#Eval("ID") %>" value="<%#Eval("FileName") %>" onkeydown="return PreReName(<%#"'"+Eval("ID")+"','"+Eval("FileName")+"'" %>);" class="form-control rename" />
                                    <%#GetOP() %> <a><span class='fa fa-refresh' title='重命名' onclick="ShowReName(<%#Eval("ID") %>);"></span></a><a><span class='fa fa-trash-o' title='删除' onclick="PostDel(<%#Eval("ID")%>);"></span></a></span></td>
                            <td><span style="line-height: 45px;"><%#GetSize(Eval("FileSize").ToString())%></span></td>
                            <td><span style="line-height: 45px;"><%#Convert.ToDateTime(Eval("CDate")).ToString("yyyy年MM月dd日 HH:mm") %></span></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
            <tr runat="server" id="Page_tr">
                <td colspan="3">
                    <asp:Literal runat="server" ID="Page_Lit" EnableViewState="false"></asp:Literal></td>
            </tr>
        </table>
        <!--模态框-->
        <div style="display: none;"><a href="javascript:;" data-toggle="modal" data-target="#preview_div" id="PreView_A"></a></div>
        <div class="modal" id="fileup_div" style="position: fixed; top: 20%;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <span class="modal-title">文件上传(每次最多五个)</span>
                    </div>
                    <div class="modal-body" style="height: 400px;">
                        <input type="file" id="file_upload_1" />
                        <input type="hidden" id="Attach_Hid" name="Attach_Hid" />
                    </div>
                    <div class="modal-footer">
                        <%--              <button type="button" class="btn btn-primary" onclick="$('#file_upload_1').uploadifyUpload();">上传</button>--%>
                        <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="article_div" style="position: fixed; top: 10%; z-index: 5;">
            <div class="modal-dialog" style="width: 1024px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" onclick="$('#article_div').hide();"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <span class="modal-title"><strong>撰写文章</strong></span>
                    </div>
                    <div class="modal-body" style="height: 630px; overflow-y: scroll;">
                        <ul>
                            <li style="padding-bottom: 5px;">
                                <asp:TextBox runat="server" ID="ArtTitle_T" CssClass="form-control" placeholder="请输入文章标题" Style="max-width: 50%; width: 400px;"></asp:TextBox>
                            </li>
                            <li>
                                <asp:TextBox runat="server" ID="Article_T" TextMode="MultiLine" Height="450px"></asp:TextBox>
                            </li>
                        </ul>
                    </div>
                    <div class="modal-footer" style="text-align: center;">
                        <asp:Button runat="server" ID="ArtSave_Btn" Text="保存" OnClick="ArtSave_Btn_Click" CssClass="btn btn-primary" />
                        <button type="button" class="btn btn-default" onclick="$('#article_div').hide();">关闭</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal" id="dir_div" style="position: fixed; top: 20%;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <span class="modal-title"><strong>新建文件夹</strong></span>
                    </div>
                    <div class="modal-body" style="height: 50px;">
                        <span>文件夹名：</span>
                        <asp:TextBox runat="server" ID="DirName_T" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="modal-footer" style="text-align: left; padding-left: 100px;">
                        <asp:Button runat="server" ID="NewFolder_Btn" Text="新建" OnClick="NewFolder_Btn_Click" OnClientClick="return CheckFolder();" CssClass="btn btn-primary" />
                        <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="modal fade" id="preview_div" style="position: fixed; top: 5%;">
            <!--Auth Not Limit-->
            <div class="modal-dialog" style="width: 1100px;">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <span class="modal-title">文件预览</span> <a href="#" id="preview_down_a" title="下载该文档">下载</a> <a href="#" title="全屏浏览">全屏</a>
                    </div>
                    <div class="modal-body">
                        <iframe id="preview_if" style="border: none; width: 100%; height: 650px; overflow-y: hidden;"></iframe>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-primary" data-dismiss="modal">关闭</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
	 .table tr{border-bottom:1px solid #ddd;height:45px;line-height:45px;}
	 .table thead tr td{font-size:15px;font-weight:bold;}
	 #FListBody tr:hover{background-color:#ebebeb;}
	 #FListBody .hoverDiv span{margin-left:10px;margin-right:10px;}
	 #FListBody a{line-height:45px;}
	 #FListBody .rename{display:none;}
</style>
    <link href="/Plugins/Uploadify/style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
    <script type="text/javascript" src="/Plugins/Uploadify/jquery.uploadify.js"></script>
    <script type="text/javascript" src="/JS/JQueryAjax.js"></script>
    <script type="text/javascript" src="/JS/jquery.validate.min.js"></script>
    <script type="text/javascript">
    $(function () {
        $("#file_upload_1").uploadify({
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
            formData: { "action": "Plat_Doc","Dir":"<%:Request["Dir"]%>" },
		queueSizeLimit: 5,
		removeTimeout: 2,
		multi: true,
		onUploadStart: function (file) { },
		onUploadSuccess: function (file, data, response) {//json,result,tru||false
		},
		onQueueComplete: function (queueData) { location = location; },
		onUploadError: function (file) {  }
	});
});
function PreView(vpath) {
    $("#PreView_A").click();
    if (vpath != $("#preview_down_a").attr("href"))//如果预览的文件变更，则重新加载
    {
        $("#preview_down_a").attr("href", vpath);
        $("#preview_if").attr("src", "/PreView.aspx?vpath=" + escape(vpath));
    }
}
ZoomLa.AjaxUrl = "<%:Request.RawUrl%>";
function ShowReName(id)
{
	$('.rename').hide(); $("#fname_" + id).show();
}
function PreReName(id, oldname)
{
	if (event.keyCode == 13)
	{ PostReName(id, oldname); $('.rename').hide(); return false; }
	else { return true; }
}
function PostReName(id, oldname) {
	var a = "ReName";
	var newname = $("#fname_" + id).val();
	if (oldname != newname) {
	    $("#fname_now_" + id).text(newname);
	    PostToCS(a, id + "|" + newname, function (data) { console.log(data); });
	}
}
function PostDel(id)
{
	if (confirm("你确定要删除该文件吗!!")) {
	    var a = "Del";
	    var v = id;
	    PostToCS(a, v, function (data) {
	        if (data == "0") {
	            alert("目录不为空,或文件不存在!!");
	        }
	        else { $("#file_tr_" + id).remove(); }
	    });
	}
}
function CheckFolder() {
	var validator= $("#form1").validate({
	    meta: "validate",
	    rules: {
	        <%:DirName_T.UniqueID%>: "required"}
	});
    return validator.form();
}
</script>
<%=Call.GetUEditor("Article_T",2)%>
</asp:Content>
