<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMailIdiograph.aspx.cs" ValidateRequest="false" Inherits="manage_Qmail_AddMailIdiograph" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register TagPrefix="ZL" TagName="UserGuide" Src="~/Manage/I/ASCX/UserGuide.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>签名管理</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <div class="col-lg-10 col-md-10 col-sm-10 col-xs-12">
    <table class="table table-striped table-bordered table-hover">
      <tr>
        <td class="tdleft td_l"><strong>签名标签：</strong></td>
        <td><asp:TextBox ID="txtName" runat="server" class="form-control m715-50"></asp:TextBox></td>
      </tr>
        <tr>
            <td class="tdleft"><strong>签名内容：</strong></td>
            <td>
                <asp:TextBox ID="txtContext" class="m715-50" style="height:200px;" runat="server" TextMode="MultiLine"></asp:TextBox>
                <%=Call.GetUEditor("txtContext",2) %>
            </td>
        </tr>
      <tr>
        <td class="tdleft"><strong>签名状态：</strong></td>
        <td><asp:RadioButtonList ID="rblState" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="True" Selected="true">启用</asp:ListItem>
            <asp:ListItem Value="False">禁用</asp:ListItem>
          </asp:RadioButtonList></td>
      </tr>
      <tr><td></td>
        <td><asp:Button ID="Save_Btn" class="btn btn-primary" runat="server" Text="提交" OnClick="Save_Btn_Click" /></td>
      </tr>
    </table>
  </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
