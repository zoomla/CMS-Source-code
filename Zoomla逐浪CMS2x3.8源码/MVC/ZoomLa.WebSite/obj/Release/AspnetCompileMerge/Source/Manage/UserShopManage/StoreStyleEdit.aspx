<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StoreStyleEdit.aspx.cs" Inherits="ZoomLaCMS.Manage.UserShopMannger.StoreStyleEdit" MasterPageFile="~/Manage/I/Default.master" %>
<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDown" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>修改模板</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <table class="table table-striped table-bordered table-hover">
    <tr>
      <td colspan="2" align="center"><asp:Literal ID="LTitle" runat="server" Text="修改模板"></asp:Literal></td>
    </tr>
    <tr>
      <td class="td_l text-right"><strong>模板名称：</strong></td>
      <td><asp:TextBox ID="TxtModelName" CssClass="form-control text_300" runat="server"  MaxLength="200" />
        <font color="red">*</font>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="TxtModelName">模板名称不能为空</asp:RequiredFieldValidator></td>
    </tr>
    <tr>
      <td class="text-right"><strong>所属模型：</strong></td>
      <td><asp:DropDownList ID="DropDownList1" CssClass="form-control text_300" runat="server"></asp:DropDownList></td>
    </tr>
    <tr>
      <td class="text-right"><strong>模板缩略图：</strong></td>
      <td><asp:TextBox ID="txt_Thumbnails" runat="server" CssClass="form-control" Width="300px" />
        <iframe id="bigimgs" style="top: 2px;width:100%;height:25px;" src="/Common/fileupload.aspx?FieldName=Thumbnails&ModelID=4&NodeID=1" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe></td>
    </tr>
    <tr>
      <td class="text-right"><strong>店铺主页模板选择：</strong></td>
      <td>
          <%=PageCommon.GetTlpDP("ModeTemplate") %>
          <asp:HiddenField ID="ModeTemplate_hid" runat="server" />
    </tr>
    <tr>
      <td class="text-right"><strong>店铺商品列表模板：</strong></td>
      <td>
          <%=PageCommon.GetTlpDP("ListModeTemplate") %>
          <asp:HiddenField ID="ListModeTemplate_hid" runat="server" />
    </tr>
    <tr>
      <td class="text-right"><strong>店铺商品信息模板：</strong></td>
      <td>
        <%=PageCommon.GetTlpDP("ContentModeTemplate") %>  
          <asp:HiddenField ID="ContentModeTemplate_hid" runat="server" />        
    </tr>
    <tr>
      <td class="text-right"><strong>模板状态：</strong></td>
      <td><asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
          <asp:ListItem Value="1" Selected="True">启用</asp:ListItem>
          <asp:ListItem Value="0">停用</asp:ListItem>
        </asp:RadioButtonList></td>
    </tr>
    <tr>
      <td colspan="2" class="text-center"><asp:HiddenField ID="HdnModelId" runat="server" />
        <asp:Button ID="EBtnSubmit" Text="保存" CssClass="btn btn-primary" runat="server" OnClick="EBtnSubmit_Click" />
        <input name="Cancel" type="button" class="btn btn-primary" id="Cancel" value="取消" onclick="javescript: history.go(-1)" /></td>
    </tr>
  </table>
<ZL:TlpDown runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/Common.js" type="text/javascript"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script>
    $().ready(function () {
        Tlp_initTemp();
    });
    var diag = new ZL_Dialog();
    function ShowTemplist(url) {
        diag.title = "选择模板";
        diag.url = url;
        diag.maxbtn = false;//不显示全屏按钮
        diag.ShowModal();
    }
    function CloseDialog() {
        diag.CloseModal();
    }
</script>
</asp:Content>