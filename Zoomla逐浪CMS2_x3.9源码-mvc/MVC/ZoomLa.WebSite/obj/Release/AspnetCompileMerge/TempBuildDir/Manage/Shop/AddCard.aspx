<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCard.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.AddCard" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">
.form-control { max-width: 200px; }
</style>
<title>VIP卡管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <div>
    <table class="table table-striped table-bordered table-hover">
      <tr>
        <td colspan="2" style="text-align:center;">生成VIP</td>
      </tr>
      <tr>
        <td style="width:120px;"> 生成数量： </td>
        <td>&nbsp;
          <asp:TextBox ID="num" class="form-control td_l" runat="server" Text="1"></asp:TextBox></td>
      </tr>
      <tr>
        <td> 卡号编码类型： </td>
        <td>
            <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="EcodeType">
                <asp:ListItem Value="2" Selected="True"><lable style="font-weight:normal">混淆&nbsp;&nbsp;</lable></asp:ListItem>
                <asp:ListItem Value="0"><lable style="font-weight:normal">数字&nbsp;&nbsp;  </lable></asp:ListItem>
                <asp:ListItem Value="1"><lable style="font-weight:normal">字母&nbsp;&nbsp;</lable></asp:ListItem>
            </asp:RadioButtonList>
        </td>
      </tr>
      <tr>
          <td>卡号编码长度：</td>
          <td>&nbsp;
          <asp:TextBox ID="Len_T" class="form-control td_l" runat="server" Text="6"></asp:TextBox><span class="rd_green">最少四位</span></td>
      </tr>
      <tr>
        <td> 卡片加盟商： </td>
        <td>&nbsp;
          <asp:TextBox ID="tx_user" class="form-control td_l" runat="server"></asp:TextBox></td>
      </tr>
              <tr>
        <td> 生效时间： </td>
        <td>&nbsp;
          <asp:TextBox ID="createtime_T" class="form-control td_l" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                    runat="server" ControlToValidate="createtime_T" ForeColor="red" ErrorMessage="*生效时间不能为空!"></asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td> 到期时间： </td>
        <td>&nbsp;
          <asp:TextBox ID="endtime" class="form-control td_l" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                    runat="server" ControlToValidate="createtime_T" ForeColor="red" ErrorMessage="*到期时间不能为空!"></asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td> 卡类型 ： </td>
        <td>&nbsp;<asp:DropDownList ID="DropDownList2" CssClass="form-control td_l" runat="server"> </asp:DropDownList></td>
      </tr>
      <tr>
        <td> 卡片状态 ： </td>
        <td>&nbsp;
            <label><input type="radio" name="isopen" value="1" />停用</label>
            <label><input type="radio" name="isopen" value="2" checked="checked" />启用</label></td>
      </tr>
      <tr>
        <td><strong></strong></td>
        <td><asp:Button ID="Button1" class="btn btn-primary" style="width:110px;"  runat="server" Text="开始生成" OnClick="Button1_Click" /></td>
      </tr>
    </table>
  </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>