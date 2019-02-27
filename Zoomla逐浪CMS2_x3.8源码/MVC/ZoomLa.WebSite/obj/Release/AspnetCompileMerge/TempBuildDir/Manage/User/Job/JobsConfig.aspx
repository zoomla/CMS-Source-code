<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobsConfig.aspx.cs" Inherits="ZoomLaCMS.Manage.User.JobsConfig"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>招聘模块配置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <table class="table table-striped table-bordered table-hover">
    <tbody id="Tabss">
      <tr class="tdbg">
        <td width="100%" height="24" align="center" class="title" colspan="2"><asp:Label ID="Lbtitle" runat="server" Text="招聘模块配置"></asp:Label></td>
      </tr>
      <tr>
        <td class="tdleft td_m"><strong> 启用模块：</strong></td>
        <td><asp:RadioButtonList ID="Isuse" runat="server" RepeatDirection="Horizontal">
            <asp:ListItem Value="1">启用</asp:ListItem>
            <asp:ListItem Value="0">停用</asp:ListItem>
          </asp:RadioButtonList></td>
      </tr>
      <tr>
        <td class="tdleft"> <strong>个人会员组：</strong></td>
        <td>&nbsp;
          <asp:DropDownList ID="userlist" runat="server" CssClass="form-control text_md"> </asp:DropDownList></td>
      </tr>
      <tr>
        <td class="tdleft"> <strong>企业会员组：</strong></td>
        <td>&nbsp;
          <asp:DropDownList ID="comlist" runat="server" CssClass="form-control text_md"> </asp:DropDownList></td>
      </tr>
      <tr>
        <td class="tdleft"> <strong>个人会员简历模型：</strong></td>
        <td>&nbsp;
          <asp:DropDownList ID="Resume" runat="server" CssClass="form-control text_md"> </asp:DropDownList>
          &nbsp;<font color="red">用于判断用户信息是否是简历</font></td>
      </tr>
      <tr>
        <td class="tdleft"> <strong>企业信息模型：</strong></td>
        <td>&nbsp;
          <asp:DropDownList ID="Company" runat="server" OnSelectedIndexChanged="Company_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control text_md"> </asp:DropDownList>
          &nbsp;<font color="red">用于判断用户信息是否是企业信息</font></td>
      </tr>
      <tr>
        <td class="tdleft"><strong> 企业信息显示字段：</strong></td>
        <td>&nbsp;
          <asp:DropDownList ID="CompanyField" runat="server" CssClass="form-control text_md"> </asp:DropDownList>
          &nbsp;<font color="red">用于显示企业信息的名称</font></td>
      </tr>
      <tr>
        <td class="tdleft"><strong> 招聘信息模型：</strong></td>
        <td>&nbsp;
          <asp:DropDownList ID="CompanyJobs" runat="server" OnSelectedIndexChanged="CompanyJobs_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control text_md"> </asp:DropDownList>
          &nbsp;<font color="red">用于判断用户信息是否是企业信息招聘信息</font></td>
      </tr>
      <tr>
        <td class="tdleft"> <strong>招聘信息职位字段：</strong></td>
        <td>&nbsp;
          <asp:DropDownList ID="JobsField" runat="server" CssClass="form-control text_md"> </asp:DropDownList>
          &nbsp;<font color="red">用于显示工作岗位名称显示</font></td>
      </tr>
      <tr>
        <td class="tdleft"><strong> 企业会员查看简历消费点数：</strong></td>
        <td > 每次查看新的简历收费
          <asp:TextBox ID="TxtConsumePoint" runat="server" Columns="5" class="form-control text_md" >0</asp:TextBox>
          点(只在查看点数消费设置是消费点数时起作用) </td>
      </tr>
      <tr>
        <td class="tdleft"><strong> 企业会员查看简历消费设置：</strong></td>
        <td><asp:RadioButtonList ID="ConsumeType" runat="server">
            <asp:ListItem Value="0" Selected="True">不启用消费</asp:ListItem>
            <asp:ListItem Value="1">使用点数查看简历</asp:ListItem>
            <asp:ListItem Value="2">在有效期内可以查看所有简历</asp:ListItem>
            <asp:ListItem Value="3">以上2种都启用</asp:ListItem>
          </asp:RadioButtonList></td>
      </tr>
      <tr>
          <td></td>
        <td>
          <asp:Button ID="Button1" runat="server" Text="提　交" OnClick="Button1_Click" class="btn btn-primary"/></td>
      </tr>
    </tbody>
  </table>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
