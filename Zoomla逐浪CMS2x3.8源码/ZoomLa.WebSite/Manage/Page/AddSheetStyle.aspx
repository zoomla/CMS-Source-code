<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSheetStyle.aspx.cs" Inherits="manage_Page_AddSheetStyle" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>

<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>添加样式</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField runat="server" ID="Label1_Hid" />
    <table class="table table-striped table-bordered table-hover">
  <tbody id="Tbody1">
    <tr class="tdbg">
        <td colspan="2" align="center" class="title">
            <span>添加样式</span></td>
    </tr>
    <tr class="tdbg" id="1" >
      <td width="26%" height="24" align="left" class="tdbgleft"><strong>样式标题(别名)：</strong></td>
      <td width="74%" align="left">
          <asp:TextBox ID="Alias" name="Alias" class="form-control" runat="server" ></asp:TextBox>
          <font color=red>*
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Alias" ErrorMessage="样式标题不能为空!"></asp:RequiredFieldValidator></font></td>
    </tr>
    <tr class="tdbg" id="Tr2"  >
      <td height="24" align="left" class="tdbgleft"><strong>标签名：</strong></td>
      <td width="74%" align="left">
          <asp:TextBox ID="Lname" name="Lname" class="form-control" runat="server" ></asp:TextBox>
          <font color=red>*
             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="Lname" ErrorMessage="标签名不能为空!"></asp:RequiredFieldValidator></font></td>
    </tr>
    <tr class="tdbg" id="Tr4"  >
      <td height="24" align="left" class="tdbgleft"><strong>标签分类：</strong></td>
      <td width="74%" align="left">
            <asp:DropDownList runat="server" ID="Ltype" CssClass="form-control"  name="Ltype">
            <asp:ListItem Value="1" Text="商品列表"></asp:ListItem>
            <asp:ListItem Value="2" Text="文章列表"></asp:ListItem>
            <asp:ListItem Value="3" Text="新相册"></asp:ListItem>
            <asp:ListItem Value="4" Text="布局"></asp:ListItem>
            <asp:ListItem Value="5" Text="天气预报"></asp:ListItem>
            <asp:ListItem Value="6" Text="图层"></asp:ListItem>
            <asp:ListItem Value="7" Text="版式"></asp:ListItem>
            <asp:ListItem Value="8" Text="背景"></asp:ListItem>
            <asp:ListItem Value="9" Text="在线地图"></asp:ListItem>
            <asp:ListItem Value="10" Text="模板"></asp:ListItem>
            <asp:ListItem Value="11" Text="系统"></asp:ListItem>
            </asp:DropDownList></td>
    </tr>
    <tr class="tdbg" id="Tr1">
      <td height="24" align="left" class="tdbgleft"><strong>标签价格：</strong></td>
      <td width="74%" align="left">
          <asp:TextBox ID="Price" name="Price" class="form-control" runat="server">0</asp:TextBox>元
          <font color=red>*
         <asp:RegularExpressionValidator ID="span1" runat="server" Display="Dynamic" ControlToValidate="Price" ErrorMessage="请输入正确的金额格式XXXX.XX" ValidationExpression="^(([1-9]\d{0,9})|0)(\.\d{1,2})?$"></asp:RegularExpressionValidator>
             <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="Price" ErrorMessage="标签名不能为空!"></asp:RequiredFieldValidator></font></td>
    </tr>
      <tr class="tdbg" id="Tr3" >
      <td height="24" align="left" class="tdbgleft">
          <strong>上传缩略图：</strong></td>
<td align="left">
          <asp:TextBox ID="ShowImg" class="form-control" runat="server"></asp:TextBox>
           <font color="red">*
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ShowImg" ErrorMessage="缩略图不能为空!"></asp:RequiredFieldValidator></font>
                  <iframe id="bigimgs" style="top: 2px" src="../../shop/fileupload.aspx?menu=ShowImg" width="100%" height="25px" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe>
          </td>
    </tr>
    <tr class="tdbg" id="Tr18">
      <td height="24" colspan="2" align="center">
          <asp:HiddenField ID="lblid" runat="server" Value="0"/>
          <asp:Button ID="Button1" class="btn btn-primary"  runat="server" Text="添加" OnClick="Button1_Click" />
          <asp:Button ID="Button2" class="btn btn-primary"  runat="server" Text="取消" OnClick="Button2_Click" /></td>
    </tr>
  </tbody>
</table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Common.js" type="text/javascript"></script>
</asp:Content>   