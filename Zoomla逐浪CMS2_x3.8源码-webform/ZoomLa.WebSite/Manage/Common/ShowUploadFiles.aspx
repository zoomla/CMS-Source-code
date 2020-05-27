<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ShowUploadFiles.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Common.ShowUploadFiles" MasterPageFile="~/Common/Common.master" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>选择上传文件</title>
<script src="/JS/Popup.js" language="javascript" type="text/javascript"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
	<table class="table table-striped table-bordered table-hover">
		<tr class="tdbg">                
			<td align="right">
				搜索当前目录文件：
				<asp:TextBox ID="TxtSearchKeyword" runat="server"></asp:TextBox>
				<asp:Button ID="BtnSearch" runat="server" Text="搜索" />
			</td>
		</tr>
	</table>
	<table class="table table-striped table-bordered table-hover">
		<tr>
			<td>
				当前目录：<asp:Label ID="LblCurrentDir" runat="server"></asp:Label>
			</td>
			<td align="right">
				<a href="ShowUploadFiles.aspx?ClientId=<%=Request.QueryString["ClientId"] %>&HiddenFieldId=<%=Request.QueryString["ClientID"] %>&Dir=<%= Server.UrlEncode(m_ParentDir) %>">返回上一级</a></td>
		</tr>
	</table>
	<asp:Repeater ID="RptFiles" runat="server" OnItemCommand="RptFiles_ItemCommand">
		<HeaderTemplate>
			<table class="table table-striped table-bordered table-hover">                    
				<tr class="title" align="center">
					<td>
						选择</td>
					<td>
						名称</td>
					<td>
						大小</td>
					<td>
						类型</td>
					<td>
						创建时间</td>
					<td>
						最后修改时间</td>
				</tr>                    
		</HeaderTemplate>
		<ItemTemplate>                
			<tr class="tdbg" align="center">
				<td>
					<asp:CheckBox ID="ChkFiles" runat="server" />
				</td>
				<td align="left">
					<%# System.Convert.ToInt32(Eval("type")) == 1 ? "<span class='fa fa-folder'></span>" : "<span class='fa fa-file'></span>"%>
					<%# System.Convert.ToInt32(Eval("type")) == 1 ? "<a href=\"ShowUploadFiles.aspx?ClientId=" + Request.QueryString["ClientId"] + "&HiddenFieldId="+Request.QueryString["HiddenFieldId"]+"&Dir=" + Server.UrlEncode(Request.QueryString["Dir"] + "/" + Eval("Name").ToString()) + "\">" + Eval("Name").ToString() + "</a>" : "<span onmouseover=\"ShowADPreview('" + GetFileContent(Eval("Name").ToString(), Eval("content_type").ToString()) + "')\" onmouseout=\"hideTooltip('dHTMLADPreview')\">" + "<a href='javascript:ReturnUrl(\"" + Request.QueryString["Dir"] + "/" + Eval("Name").ToString() + "\")'><img src=/uploadFiles/" +Request.QueryString["Dir"] + "/" + Eval("Name").ToString() + " width='100' /></a></span>"%>
				</td>
				<td>
					<%# GetSize(Eval("size").ToString())%>
				</td>
				<td>
					<%# System.Convert.ToInt32(Eval("type")) == 1 ? "文件夹" : Eval("content_type").ToString() + "文件" %>
				</td>
				<td>
					<%# Eval("createTime")%>
				</td>
				<td>
					<%# Eval("lastWriteTime")%>
				</td>
			</tr>                              
		</ItemTemplate>
		<FooterTemplate>
			</table></FooterTemplate>
	</asp:Repeater>
	<div id="dHTMLADPreview" style="z-index: 1000; left: 0px; visibility: hidden; width: 10px;
		position: absolute; top: 0px; height: 10px">
	</div>

	<script type="text/javascript">
<!--
function ReturnUrl(url)
{

if (url.substring(0, 1) == "/")
{
	url = url.substring(1, url.Length);
}

var isMSIE= (navigator.appName == "Microsoft Internet Explorer");
if (isMSIE)
{
	window.returnValue = url;
	window.close();
}
else
{
	var thumbClientId = '<%= Request.QueryString["ThumbClientId"] %>';
	if(thumbClientId == '')
	{
	    var obj = opener.document.getElementById('<%= Request.QueryString["ClientId"] %>');
	    var newurl = '下载地址' + (obj.length + 1) + '|' + url;
	    $(obj).append('<option value="'+newurl+'">'+newurl+'</option>')
		ChangeHiddenFieldValue();
	}
	else
	{
        alert('x')
		var obj = opener.document.getElementById(thumbClientId);
		obj.value = url;
	}
	window.close();
}
}

function ChangeHiddenFieldValue()
{
    var obj = opener.document.getElementById('<%= Request.QueryString["HiddenFieldId"] %>');
var softUrls = opener.document.getElementById('<%= Request.QueryString["ClientId"] %>');
    var value = "";
    var softobj = $(softUrls).find('option');
    for (var i = 0; i < softobj.length; i++) {
        if (value != "") {
            value = value + "$$$";
        }
        value = value + $(softobj[i]).val();
    }
    obj.value = value;
}
//-->
	</script>
</asp:Content>


