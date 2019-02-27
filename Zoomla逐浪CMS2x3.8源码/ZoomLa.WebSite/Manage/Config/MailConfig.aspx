<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MailConfig.aspx.cs" Inherits="Manage_I_Config_MailConfig" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false"  ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>邮件参数配置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
          <td class="td_l"><strong>发件邮箱：</strong></td>
          <td><asp:TextBox ID="MailName_T" runat="server" class="form-control text_300"/></td>
        </tr>
        <tr><td><strong>邮箱密码：</strong></td><td><asp:TextBox runat="server" TextMode="Password" ID="MailPwd_T" CssClass="form-control text_300"/></td></tr>
        <tr >
          <td><strong><%:lang.LF("发送邮件服务器(SMTP)") %>：</strong></td>
        <td>
            <div class="input-group text_500">
                <asp:TextBox runat="server" ID="SMTP_T" class="form-control text_300" />
                <span id="smtp_btns" class="input-group-btn">
                    <input type="button" class="btn btn-default" data-value="smtp.exmail.qq.com" value="QQ企业邮箱">
                    <input type="button" class="btn btn-default" data-value="smtp.163.com" value="163">
                    <input type="button" class="btn btn-default" data-value="smtp.sina.cn" value="新浪">
                    <input type="button" class="btn btn-default" data-value="smtp.139.com" value="139">
<%--                    <input type="button" class="btn btn-default" data-value="smtp.tom.com" value="Tom">--%>
                </span>
            </div>
        </td>
        </tr>
        <tr>
          <td><strong>邮件协议：</strong></td>
          <td>
              <div class="input-group text_300">
                  <asp:TextBox ID="MailPort_T" runat="server" class="form-control text_300" />
                  <span id="port_btns" class="input-group-btn">
                      <input type="button" class="btn btn-default" data-value="25" value="SMTP(25)" />
                      <input type="button" class="btn btn-default" data-value="465" value="SSL(465)" />
                  </span>
              </div>
          </td>
        </tr>
       <%-- <tr >
          <td><strong><%:lang.LF("身份验证方式") %>：</strong></td>
          <td><asp:RadioButton ID="RadioButton1" runat="server" GroupName="AuthenticationType"/>
            无<br />
            <asp:RadioButton ID="RadioButton2" runat="server" GroupName="AuthenticationType"/>
            基本<br />
            如果您的电子邮件服务器要求在发送电子邮件时显式传入用户名和密码，请选择此选项。 <br />
            <span class="pull-left" style="line-height:32px;">发件人的用户名：</span>
            <asp:TextBox ID="TextBox5" runat="server" Columns="30" class="form-control pull-left" Width="200" />
            <div class="clearfix"></div>
            <span class="pull-left" style="line-height:32px;">发件人的密&nbsp;&nbsp;&nbsp;码：</span>
            <asp:TextBox ID="TextBox6" TextMode="Password" Width="200" runat="server" Columns="30" CssClass="form-control"/>
            <asp:RadioButton ID="RadioButton3" runat="server" GroupName="AuthenticationType" />
            NTLM (Windows 身份验证) <br/>
            如果您的电子邮件服务器位于局域网上，并且您使用 Windows 凭据连接到该服务器，请选择此选项。 </td>
        </tr>--%>
        <tr >
          <td><strong><%:lang.LF("系统邮件接收地址") %>：</strong></td>
          <td>
              <asp:TextBox ID="TextBox4" runat="server" class="form-control text_300" />
              <div class="rd_green">用于接收系统运行、商务等邮件，多个地址用逗号分隔。</div> 
          </td>
        </tr>
        <tr><td></td><td><asp:Button ID="Save_Btn" runat="server" Text="保存设置" OnClick="Save_Btn_Click" class="btn btn-primary" /></td></tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        $(function () {
            $("#smtp_btns .btn").click(function () { $("#SMTP_T").val($(this).data("value")); });
            $("#port_btns .btn").click(function () { $("#MailPort_T").val($(this).data("value")); });
        })
    </script>
</asp:Content>