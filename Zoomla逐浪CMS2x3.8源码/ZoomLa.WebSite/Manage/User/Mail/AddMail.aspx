<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddMail.aspx.cs" Inherits="manage_Qmail_AddMail" EnableViewStateMac="false" ValidateRequest="false"  MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register TagPrefix="ZL" TagName="UserGuide" Src="~/Manage/I/ASCX/UserGuide.ascx" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>邮件发送</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <div>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="tdleft td_l"><strong>收件人：</strong></td>
            <td>
                <asp:DropDownList ID="DropDownList2" class="btn btn-default dropdown-toggle" runat="server">
                    <asp:ListItem>全部</asp:ListItem>
                    <asp:ListItem>按字母查询</asp:ListItem>
                    <asp:ListItem>按数字查询</asp:ListItem>
                    <asp:ListItem>按加入日期查询</asp:ListItem>
                    <asp:ListItem>按最后发送时间查询</asp:ListItem>
                    <asp:ListItem>按邮箱后缀查询</asp:ListItem>
                    <asp:ListItem>按订阅类型</asp:ListItem>
                </asp:DropDownList>
                <asp:TextBox Visible="false" ID="txtSend" TextMode="MultiLine" Rows="4" Width="760px" runat="server" Enabled="False" class="form-control" Style="display: inline;"></asp:TextBox></td>
        </tr>
      <tr>
        <td class="tdleft" ><strong>邮件主题：</strong></td>
        <td><asp:TextBox ID="TxtSubject" runat="server"   class="form-control" Width="762px"></asp:TextBox>
          <asp:RequiredFieldValidator ID="ValrSubject" runat="server" ControlToValidate="TxtSubject" Display="None" ErrorMessage="邮件主题不能为空！"></asp:RequiredFieldValidator></td>
      </tr>
      <tr>
        <td class="tdleft" ><strong>邮件内容：</strong></td>
        <td>
            <textarea runat="server" id="HiddenField1" rows="15" style="width:760px;;height:300px;" ></textarea>
            </td>
      </tr>
      <tr>
        <td class="tdleft"  ><strong>签  名：</strong></td>
        <td><asp:DropDownList ID="ddlMailIdiograph" class="form-control" width="150px" runat="server" DataTextField="Name" DataValueField ="Context"></asp:DropDownList></td>
      </tr>
      <tr>
        <td class="tdleft" ><strong>发送方式：</strong></td>
        <td><table>
            <tr>
              <td><asp:RadioButtonList ID="rblSendType" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rblSendType_SelectedIndexChanged">
                  <asp:ListItem Selected="True">立即发送</asp:ListItem>
                  <asp:ListItem>定时发送</asp:ListItem>
                </asp:RadioButtonList></td>
              <td id="td1" runat ="server" visible="false"><asp:TextBox ID="txtSendTime" onfocus="setday(this)" Width="60px" runat="server"></asp:TextBox>
                <asp:DropDownList ID="ddlHour" runat="server"> </asp:DropDownList>
                时
                <asp:DropDownList ID="ddlMinute" runat="server"> </asp:DropDownList>
                分</td>
            </tr>
          </table></td>
      </tr >
      <tr>
        <td colspan="2" style="text-align:center;"><asp:Button ID="BtnSend" class="btn btn-primary" runat="server" Text="发送" Width="60px" OnClick="BtnSend_Click" />
          &nbsp;&nbsp; &nbsp;
          <input id="Reset1" class="btn btn-primary" type="reset" value=" 清除 " width="60px"  /></td>
      </tr>
    </table>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"  ShowSummary="False" />
  </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
     <script src="/JS/DatePicker/WdatePicker.js"></script> 
     <script charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
     <script charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script> 
     <script>UE.getEditor('HiddenField1');</script>
</asp:Content>