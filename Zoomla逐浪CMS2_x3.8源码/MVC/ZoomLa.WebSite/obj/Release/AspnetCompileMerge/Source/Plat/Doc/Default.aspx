<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ZoomLaCMS.Plat.Doc.Default" MasterPageFile="~/Plat/Main.master"   ValidateRequest="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>公司文库</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="container platcontainer">
<div style="margin-top: 10px; margin-bottom: 10px;"><span>文件路径：</span><span><a href="<%=Request.Url %>">公司文库</a>></span>
<asp:Literal runat="server" ID="PathNav_L"></asp:Literal>
</div>
<div>
<input type="button" onclick="$('#article_div').show();" value="撰写文章" class="btn btn-info"/>
<input type="button" id="upfile_btn" value="上传文件" class="btn btn-info"/>
<input type="button" data-toggle="modal" data-target="#dir_div" value="新建文件夹" class="btn btn-info"/>
</div>
<table class="table">
<thead>
  <tr>
	<td style="width:60%;">名称</td>
	<td style="width:20%;">大小</td>
	<td style="width:20%;">创建时间</td>
  </tr>
</thead>
<tbody id="FListBody">
  <asp:Repeater runat="server" ID="File_Rep">
	<ItemTemplate>
	  <tr id="file_tr_<%#Eval("ID") %>" onmouseover="$(this).find('.hoverDiv').show();" onmouseout="$(this).find('.hoverDiv').hide();">
		<td><div style="position:relative;bottom:-10px;display:inline-block;"> <%#GroupPic.GetExtNameMini(GetExt()) %> </div>
		  <%#GetFileName() %> <span class="hoverDiv" style="display:none;">
		  <input type="text" id="fname_<%#Eval("ID") %>" value="<%#Eval("FileName") %>" onkeydown="return PreReName(<%#Eval("ID")+",'"+Eval("FileName")+"'" %>);" class="form-control rename" />
		  <%#GetOP() %> <a><span class='fa fa-refresh' title='重命名' onclick="ShowReName(<%#Eval("ID") %>);"></span></a> <a><span class='fa fa-trash-o' title='删除' onclick="PostDel(<%#Eval("ID")+",'"+Eval("FileName")+"'" %>);"></span></a> </span></td>
		<td><span style="line-height:45px;"><%#GetSize(Eval("FileSize").ToString())%></span></td>
		<td><span style="line-height:45px;"><%#Convert.ToDateTime(Eval("CDate")).ToString("yyyy年MM月dd日 HH:mm") %></span></td>
	  </tr>
	</ItemTemplate>
  </asp:Repeater>
</tbody>
<tr runat="server" id="Page_tr">
  <td colspan="3"><asp:Literal runat="server" ID="Page_Lit" EnableViewState="false"></asp:Literal></td>
</tr>
</table>
<!--模态框-->
<div style="display:none;"> <a href="javascript:;" data-toggle="modal" data-target="#preview_div" id="PreView_A"></a> </div>
<div class="modal" id="article_div" style="position:fixed;top:35px;z-index:5;">
<div class="modal-dialog" style="width:1024px;">
  <div class="modal-content">
	<div class="modal-header">
	  <button type="button" class="close" data-dismiss="modal" onclick="$('#article_div').hide();"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
	  <span class="modal-title"><strong>撰写文章</strong></span> </div>
	<div class="modal-body" style="height:460px;overflow-y:auto;">
	  <ul>
		<li>
		  <asp:TextBox runat="server" ID="ArtTitle_T" CssClass="form-control" placeholder="请输入文章标题"></asp:TextBox>
		</li>
		<li class="margin_t5">
		  <asp:TextBox runat="server" ID="Article_T" TextMode="MultiLine" Height="300px"></asp:TextBox>
		</li>
	  </ul>
	</div>
	<div class="modal-footer" style="text-align:center;">
        <input type="button" class="btn btn-info"    value="保存" onclick="htmlchk();" />
        <input type="button" class="btn btn-default" value="关闭" onclick="$('#article_div').hide();">
	</div>
  </div>
</div>
</div>
<div class="modal" id="dir_div" style="position:fixed;top:20%;">
<div class="modal-dialog">
  <div class="modal-content">
	<div class="modal-header">
	  <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
	  <span class="modal-title"><i class="fa fa-folder"></i> 新建文件夹</span> </div>
      <div class="modal-body" style="padding-bottom:50px;">
          <div class="input-group">
              <span class="input-group-addon">文件夹名</span>
              <asp:TextBox runat="server" ID="DirName_T" CssClass="form-control"></asp:TextBox>
              <span class="input-group-btn">
                  <asp:Button runat="server" ID="NewFolder_Btn" Text="新建" OnClick="NewFolder_Btn_Click" CssClass="btn btn-info" ValidationGroup="newdir" />
                  <button type="button" class="btn btn-default" data-dismiss="modal">关闭</button>
              </span>
          </div>
          <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="DirName_T" ForeColor="Red" ErrorMessage="文件夹名不能为空" ValidationGroup="newdir" />
      </div>
  </div>
</div>
</div>
<div class="modal fade" id="preview_div" style="position:fixed;top:5%;"><!--Auth Not Limit-->
<div class="modal-dialog" style="width:1100px;">
  <div class="modal-content">
	<div class="modal-header">
	  <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
	  <span class="modal-title">文件预览</span> <a href="#" id="preview_down_a" title="下载该文档">下载</a> <a href="#" title="全屏浏览">全屏</a> </div>
	<div class="modal-body">
	  <iframe id="preview_if"  style="border:none;width:100%;height:650px;overflow-y:hidden;"></iframe>
	</div>
	<div class="modal-footer">
	  <button type="button" class="btn btn-info" data-dismiss="modal">关闭</button>
	</div>
  </div>
</div>
</div>
</div>
<asp:Button runat="server" ID="ArtSave_Btn" Text="保存" OnClick="ArtSave_Btn_Click" style="display:none;"/>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style>
.table tr{border-bottom:1px solid #ddd;height:45px;line-height:45px;}
.table thead tr td{font-size:15px;font-weight:bold;}
#FListBody tr:hover{background-color:#ebebeb;}
#FListBody .hoverDiv span{margin-left:10px;margin-right:10px;}
#FListBody a{line-height:45px;}
#FListBody .rename{display:none;}
</style>
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<script>
var api = "<%:Request.RawUrl%>";
$(function () {
    ZL_Webup.config.json.ashx = "action=Plat_Doc_Common%26dir=<%:Request["dir"]%>";
    $("#upfile_btn").click(ZL_Webup.ShowFileUP);
    setactive("公司");
});
function webup_finish() { location = location; }
function PreView(vpath) {
    $("#PreView_A").click();
    if (vpath != $("#preview_down_a").attr("href"))//如果预览的文件变更，则重新加载
    {
        $("#preview_down_a").attr("href", vpath);
        $("#preview_if").attr("src", "/PreView.aspx?vpath=" + escape(vpath));
    }
}
function ShowReName(id) {
	$('.rename').hide(); $("#fname_" + id).show();
}
function PreReName(id, oldname) {
	if (event.keyCode == 13)
	{ PostReName(id, oldname); $('.rename').hide(); return false; }
	else { return true; }
}
function PostReName(id, oldname) {
    var newname = $("#fname_" + id).val();
    if (oldname != newname) {
        $("#fname_now_" + id).text(newname);
        $.post(api, { action: "ReName", value: id + "|" + newname }, function (data) { console.log(data); });
    }
}
function PostDel(id, fname) {
	if (confirm("你确定要删除该文件吗!!")) {
	    $.post(api, { action: "Del", value: id }, function (data) {
	        if (data == "0") { alert("目录不为空,或文件不存在!!"); }
	        else { $("#file_tr_" + id).remove(); }
	    })
	}
}
function htmlchk() {
    if (ZL_Regex.isEmpty($("#ArtTitle_T").val())) { alert("标题不能为空"); return; }
    var txt = UE.getEditor("Article_T").getContentTxt();
    if (ZL_Regex.isEmpty(txt)) { alert("内容不能为空"); return; }
    if (!confirm("确定要保存吗?")) { return; }
    $("#ArtSave_Btn").trigger("click");
}
</script>
<%=Call.GetUEditor("Article_T",2)%>
</asp:Content>
