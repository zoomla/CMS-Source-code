<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InputUser.aspx.cs" Inherits="ZoomLaCMS.Manage.User.InputUser" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>导入用户</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>
  <div class="border">
    <table class="table table-striped table-bordered table-hover">
      <tr>
        <td colspan="4" class="spacingtitle" align="center">会员导入 </td>
      </tr>
      <tr class="tdbg">
        <td width="220" align="right" class="tdbgleft" style="width: 15%; height: 22px;"> 选择文件： </td>
        <td align="left" class="style3" colspan="3">
            <ZL:FileUpload  ID="FileUpload1" runat="server" Width="250px"/>
            <%--<asp:FileUpload ID="FileUpload1" runat="server" Width="250px" />--%></td>
      </tr>
      <tr class="tdbg">
        <td width="220" align="right" class="tdbgleft" style="width: 15%; height: 22px;"> 初始密码： </td>
        <td align="left" class="style3" colspan="3"><asp:TextBox ID="userpwd" TextMode="Password" class="form-control" style="max-width:150px;float:left;"
                        runat="server" Width="172px"></asp:TextBox>
          <font color="red"> *用于定义导入会员的初始密码，如为空则生成默认admin888密码。</font> </td>
      </tr>
    </table>
    <table class="table table-striped table-bordered table-hover">
      <tr class="tdbg">
        <td style="height: 21px" colspan="4" align="center"><asp:LinkButton ID="DownFile_L" CssClass="btn btn-primary" OnClick="DownFile_L_Click" runat="server">下载模板</asp:LinkButton> <asp:Button ID="Button1" runat="server" Text="导入用户" class="btn btn-primary" onclick="Button1_Click" />
          <asp:Button ID="btnCancel" class="btn btn-primary" runat="server" Text="返回" Width="70px" OnClientClick="parent.CloseDiaog();return false;" TabIndex="13" /></td>
      </tr>
    </table>
  </div>
  状态： <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Common.js" type="text/javascript"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>