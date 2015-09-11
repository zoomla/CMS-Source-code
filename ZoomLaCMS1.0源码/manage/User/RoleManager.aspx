<%@ Page Language="C#" Title="管理员角色分配" AutoEventWireup="true" CodeFile="RoleManager.aspx.cs" Inherits="User.RoleManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>管理员角色分配</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
　　<table width="99%" cellspacing="1" cellpadding="0" class="border" align="center"> 
        <tr align="center">
            <td colspan="2" class="spacingtitle">
                <b>管理员角色设置</b>
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="width: 100px;">
                <strong>角色名：</strong></td>
            <td>
                <asp:Label ID="LblRoleName" runat="server" Text="" />
            </td>
        </tr>
        <tr class="tdbg">
            <td class="tdbgleft" align="right" style="height: 79px; width: 100px;">
                <strong>角色描述：</strong></td>
            <td>
                <asp:Label ID="LblDescription" runat="server" Text="" />
            </td>
        </tr>      
      <tr class="tdbg">
          <td align="right" class="tdbgleft" style="width: 100px;">
              权限设置：</td>
          <td>
              内容管理<br/>
              <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                  <asp:ListItem Value="ContentMange">内容管理</asp:ListItem>
                  <asp:ListItem Value="ContentEdit">内容编辑</asp:ListItem>
                  <asp:ListItem Value="ContentSpec">专题内容管理</asp:ListItem>
                  <asp:ListItem Value="ContentRecycle">回收站管理</asp:ListItem>
                  <asp:ListItem Value="ComentManage">评论管理</asp:ListItem>
                  <asp:ListItem Value="CreateHtmL">生成管理</asp:ListItem>
              </asp:CheckBoxList><br/>
              模型节点管理<br/>
              <asp:CheckBoxList ID="CheckBoxList2" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                  <asp:ListItem Value="ModelManage">内容模型管理</asp:ListItem>
                  <asp:ListItem Value="ModelEdit">内容模型编辑</asp:ListItem>
                  <asp:ListItem Value="NodeManage">节点管理</asp:ListItem>
                  <asp:ListItem Value="NodeEdit">节点编辑</asp:ListItem>
                  <asp:ListItem Value="SpecCateManage">专题类别管理</asp:ListItem>
                  <asp:ListItem Value="SpecManage">专题管理</asp:ListItem>
              </asp:CheckBoxList><br/>
              模板标签管理<br/>
              <asp:CheckBoxList ID="CheckBoxList3" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                  <asp:ListItem Value="TemplateManage">模板管理</asp:ListItem>
                  <asp:ListItem Value="TemplateEdit">模板编辑</asp:ListItem>
                  <asp:ListItem Value="CssManage">风格管理</asp:ListItem>
                  <asp:ListItem Value="CssEdit">风格编辑</asp:ListItem>
                  <asp:ListItem Value="LabelManage">标签管理</asp:ListItem>
                  <asp:ListItem Value="LabelEdit">标签编辑</asp:ListItem>
                  <asp:ListItem Value="LabelImport">标签导入</asp:ListItem>
                  <asp:ListItem Value="LabelExport">标签导出</asp:ListItem>
              </asp:CheckBoxList><br/>
              用户管理<br/>
              <asp:CheckBoxList ID="CheckBoxList4" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                  <asp:ListItem Value="AdminManage">管理员管理</asp:ListItem>
                  <asp:ListItem Value="AdminEdit">管理员编辑</asp:ListItem>
                  <asp:ListItem Value="RoleMange">角色管理</asp:ListItem>
                  <asp:ListItem Value="RoleEdit">角色设置</asp:ListItem>
                  <asp:ListItem Value="UserManage">会员管理</asp:ListItem>
                  <asp:ListItem Value="UserGroup">会员组管理</asp:ListItem>
                  <asp:ListItem Value="UserModel">会员模型管理</asp:ListItem>
                  <asp:ListItem Value="UserModelField">会员模型字段管理</asp:ListItem>
                  <asp:ListItem Value="MessManage">短消息发送</asp:ListItem>
                  <asp:ListItem Value="EmailManage">邮件列表</asp:ListItem>
              </asp:CheckBoxList><br/>
              其他管理<br/>
              <asp:CheckBoxList ID="CheckBoxList5" runat="server" RepeatDirection="Horizontal" RepeatColumns="5">
                  <asp:ListItem Value="ADManage">广告管理</asp:ListItem>
                  <asp:ListItem Value="SurveyManage">问卷管理</asp:ListItem>
                  <asp:ListItem Value="FileManage">上传文件管理</asp:ListItem>
                  <asp:ListItem Value="AuthorManage">作者管理</asp:ListItem>
                  <asp:ListItem Value="DownServerManage">下载服务器管理</asp:ListItem>
                  <asp:ListItem Value="SourceManage">来源管理</asp:ListItem>
                  <asp:ListItem Value="DicManage">数据字典管理</asp:ListItem>
              </asp:CheckBoxList>
              </td>
      </tr>
      <tr class="tdbg">
          <td align="center" class="tdbgleft" colspan="2" style="height: 79px">
              &nbsp;<asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="保存权限设置" />
              &nbsp; &nbsp;&nbsp;
              <asp:Button ID="btnBack" runat="server" Text="返回" OnClick="btnBack_Click" /></td>
      </tr>
    </table>                            
       </div>
    </form>
</body>
</html>