<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SeachDirectory.aspx.cs" Inherits="manage_Search_SeachDirectory" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>全文目录</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
      <tr align="left">
        <td width="10%" class="tdbg">欢迎您使用全文检索系统；本系统将帮助您为数据库创建全文索引目录。
        <br />在此位置您可以对索引目录进行观察位置、大小、删除目录及管理目录的索引文件。
        </td>
      </tr>
      </table>
    <table class="table table-striped table-bordered table-hover">
      <tr align="center" class="title">
        <td width="5%" class="title"></td>
        <td width="10%" class="title">目录名称</td>
        <td class="style1"width="55%" >地址路径</td>
        <td width="10%" class="title">当前容量</td>
        <td width="10%" class="title">创建日期</td>
        <td width="10%" class="title"> 操作</td>
      </tr>
        <ZL:ExRepeater ID="gvCard" runat="server" PagePre="<tr><td colspan='6' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">       
        <ItemTemplate>
      <tr >
        <td height="22" align="center"></td>
        <td height="22" align="center"></td>
         <td height="22" align="center"></td>
        <td height="22" align="center"></td>
        <td height="22" align="center"></td>
        <td height="22" align="center"></td>
      </tr>
        </ItemTemplate>
        <FooterTemplate></FooterTemplate>
        </ZL:ExRepeater>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
	function CheckAll(spanChk)//CheckBox全选
	{
		var oItem = spanChk.children;
		var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
		xState = theBox.checked;
		elm = theBox.form.elements;
		for (i = 0; i < elm.length; i++)
			if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
				if (elm[i].checked != xState)
					elm[i].click();
			}
	}
</script>
</asp:Content>