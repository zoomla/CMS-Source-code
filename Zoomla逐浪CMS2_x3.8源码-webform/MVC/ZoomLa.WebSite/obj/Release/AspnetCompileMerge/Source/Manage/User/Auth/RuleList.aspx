<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RuleList.aspx.cs" Inherits="ZoomLaCMS.Manage.User.RuleList"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>选择角色</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
     <table class="table table-striped table-bordered table-hover">
    <tr class="title">
      <td align="left"><b>角色列表：</b></td>
      <td align="right"><asp:TextBox ID="TxtKeyWord" runat="server" class="form-control" style="max-width:150px; display:inline;"></asp:TextBox>
        &nbsp;&nbsp;
        <asp:Button ID="BtnSearch" runat="server" Text="查找" OnClick="BtnSearch_Click" class="btn btn-primary" /></td>
    </tr>
    <tr>
      <td valign="top" colspan="2"><table>
          <tr class="tdbgleft">
            <td width="5%" height="24" align="center"><strong>ID</strong></td>
            <td width="5%" height="24" align="center"><asp:CheckBox ID="CheckBox1" runat="server" onclick="CheckAll(this);" /></td>
            <td width="15%" height="24" align="center"><strong>角色名称</strong></td>
            <td width="35%" height="24" align="center"><strong>角色说明</strong></td>
            <td width="10%" height="24" align="center"><strong>优先级别</strong></td>
            <td width="10%" height="24" align="center"><strong>是否启用</strong></td>
          </tr>
          <ZL:ExRepeater ID="Pagetable" runat="server" PagePre="<tr><td colspan='6' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
            <ItemTemplate>
              <tr>
                <td height="24" align="center"><%#Eval("ID") %></td>
                <td height="24" align="center"><input name="Item" type="checkbox" value=<%#Eval("ID") %>></td>
                <td height="24" align="center"><a target="_blank" href="Permissionadd.aspx?menu=edit&id=<%#Eval("ID") %>"><%#Eval("RoleName") %></a></td>
                <td height="24" align="center"><%#Eval("info")%></td>
                <td height="24" align="center"><%#Eval("Precedence")%></td>
                <td height="24" align="center"><%#Eval("IsTrue","{0}")=="True"?"<font color=green>启用</font>":"<font color=red>停用</font>"%></td>
              </tr>
            </ItemTemplate>
              <FooterTemplate></FooterTemplate>
          </ZL:ExRepeater>
        </table></td>
    </tr>
    <tr>
      <td colspan="2" align="center"><asp:Button ID="Button1" runat="server" Text="添加角色" OnClick="Button1_Click" class="btn btn-primary" />
        <asp:Button ID="Button2" runat="server" Text="取消添加" OnClick="Button2_Click" class="btn btn-primary" /></td>
    </tr>
  </table>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script language="javascript">
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


