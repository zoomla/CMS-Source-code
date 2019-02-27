<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSeat.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Service.AddSeat"  MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css"  rel="stylesheet"/>
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <title>客服信息</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <div>
    <table class="table table-striped table-bordered table-hover">
      <tr>
        <td class="td_m ">客服名称：</td>
        <td><asp:TextBox ID="SeatName_T"  runat="server" class="form-control text_300"></asp:TextBox>
            <span class="rd_green">显示给用户的名称</span>
            <asp:RequiredFieldValidator runat="server" ID="R3" ControlToValidate="SeatName_T" ForeColor="Red" ErrorMessage="客服名称不能为空" Display="Dynamic" />
        </td>
      </tr>
      <tr>
        <td>用户名：</td>
        <td>
            <asp:TextBox ID="User_T" runat="server" CssClass="form-control text_300"></asp:TextBox> 
            <span class="rd_green">如用户不存在,则会自动新建,新客服密码默认123456</span>
            <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="User_T" ForeColor="Red" ErrorMessage="用户名不能为空" Display="Dynamic" />
        </td>
      </tr>
        <tr>
            <td>客服头像：</td>
            <td><ZL:SFileUp runat="server" id="SFileUp" /></td>
        </tr>
       <tr>
        <td>默认客服：</td>
        <td><input runat="server" type="checkbox" id="RadioButtonList1" class="switchChk"/></td>
      </tr>
      <tr>
        <td>显示位置：</td>
        <td><asp:DropDownList ID="ddlIndex" runat="server" class="btn btn-default dropdown-toggle"> </asp:DropDownList></td>
      </tr>
      <tr class="tdbg">
        <td></td>
        <td><asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="提 交" OnClick="Button1_Click" />
            <a href="ServiceSeat.aspx" class="btn btn-primary">返回</a></td>
      </tr>
    </table>
  </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>