<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleAuth.aspx.cs" Inherits="Plat_Admin_RoleAuth" MasterPageFile="~/Plat/Main.master" EnableViewState="false" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<title>角色权限</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <table class="table table-striped table-bordered">
    <tr>
      <td style="width:120px;">角 色 名：</td>
      <td><asp:Label runat="server" ID="RoleName_L" /></td>
    </tr>
    <tr>
      <td>角色描述：</td>
      <td><asp:Label runat="server" ID="RoleDesc_L" /></td>
    </tr>
    <tr>
      <td colspan="2" style="text-align:center;"><strong>权限配置</strong></td>
    </tr>
    <tr>
      <td>文档管理：</td>
      <td><input type="checkbox" name="Article" value="P_Article_Send" />
        发文
        <input type="checkbox" name="Article" value="P_Article_Del" />
        删文
        <input type="checkbox" name="Article" value="P_Article_View" />
        查看 </td>
    </tr>
    <tr>
      <td>OA公文流程：</td>
      <td><input type="checkbox" name="Article" value="P_OA_Send" />
        发送公文
        <input type="checkbox" name="Article" value="P_OA_Audit" />
        公文审核
        <input type="checkbox" name="Article" value="P_OA_View" />
        公文浏览 </td>
    </tr>
    <tr>
      <td>操作：</td>
      <td><asp:Button runat="server" ID="Save_Btn" Text="保存" CssClass="btn btn-primary" OnClick="Save_Btn_Click" /></td>
    </tr>
  </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
  <script type="text/javascript">
      $(function () {
          $("#top_nav_ul li[title='管理']").addClass("active");
      })
function InitValue(v) {
	$("input[type='checkbox']").each(function () {
		var cv = $(this).val();
		this.checked = v.indexOf("," + cv + ",") >= 0;
	});
}
</script>
</asp:Content>