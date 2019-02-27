<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSites.aspx.cs" Inherits="Manage_I_Content_AddSites" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" runat="server" >
<title>添加站点信息</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="server">
    <table class="table table-bordered table-hover table-striped">
        <tr>
            <td class="name">站点名称：</td>
            <td>
                <asp:TextBox ID="SiteName_T" runat="server" Style="float:left;" CssClass="form-control text_md"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Style="float:left; margin:7px;" ControlToValidate="SiteName_T"
                    ErrorMessage="站点名称不能为空!" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="name">站点链接：</td>
            <td>
                <asp:TextBox runat="server" ID="SiteUrl_T" CssClass="form-control text_md" Style="float:left;"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2"  Style="float:left; margin:7px;" runat="server" ControlToValidate="SiteUrl_T"
                    ErrorMessage="站点链接不能为空!" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
                
        </tr>
        <tr>
            <td class="name">子站备注：</td>
            <td>
                <asp:TextBox runat="server" ID="SiteDesc_T" CssClass="form-control text_md"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="name">站点密钥：</td>
            <td>
                <asp:TextBox runat="server" ID="SiteKey_T" Style="float:left;" CssClass="form-control text_md"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="float:left; margin:7px;" runat="server" ControlToValidate="SiteKey_T"  ErrorMessage="站点密钥不能为空!" SetFocusOnError="True" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="name">需获取的节点：</td>
            <td><asp:TextBox runat="server" ID="Nodes_T" CssClass="form-control text_md" style="display:inline-block;"></asp:TextBox>
                <input type="button" value="选择" onclick="ShowDiv('node_div');" class="btn btn-primary" /><abbr>为空代表所有节点</abbr>
            </td>
        </tr>
            <tr>
                <td colspan="2"><asp:Button CssClass="btn btn-primary" OnClick="Save_Btn_Click" ID="Save_Btn" runat="server" Text="添加站点" /></td>
            </tr>
    </table>
    
</asp:Content>
<asp:Content ContentPlaceHolderID="ScriptContent" runat="server">    
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<style>.name{ width:8%;}</style>
<script>
	function CloseDiv(id)
	{
		$("#" + id).hide();
	}
	var nodediag = new ZL_Dialog();
	function ShowDiv(id)
	{
		var url = $("#SiteUrl_T").val();
		if (!url || url == "") {
			alert("站点地址不能为空!");
		}
		else {
			if (url.indexOf("http://") < 0) url = "http://" + url;
			if (url.lastIndexOf("/") != url.length - 1) url = url += "/";
			nodediag.maxbtn = false;
			nodediag.title = "远程节点选择" + "来源：" + url;
			nodediag.url = "/Common/NodeList.aspx?SiteUrl=" + url;
			nodediag.ShowModal();
		}
	}
	function DealResult(nodeArr)
	{
	    var ids = "";
	    for (var i = 0; i < nodeArr.length; i++) {
	        ids += nodeArr[i].nodeid+",";
	    }
	    $("#Nodes_T").val(ids);
	    nodediag.CloseModal();
	}
</script>
</asp:Content>