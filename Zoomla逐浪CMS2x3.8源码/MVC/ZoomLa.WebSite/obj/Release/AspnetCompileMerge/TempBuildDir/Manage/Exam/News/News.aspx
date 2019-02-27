<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="News.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.News.News" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>报纸管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<%@ Register TagPrefix="uc"TagName="NodeList"Src="~/Manage/I/ASCX/PublishNodeTree.ascx"%>
<div id="nodelist" class="col-lg-2 col-md-2 col-xs-3 padding_l_r0">
<uc:NodeList runat="server" />
</div>
  <iframe id="Publist_IF" src="PublishDesc.aspx?Nid=0" class="col-lg-10 col-md-10 col-xs-9 padding_l2px" style="border:none;min-height:750px;"></iframe>
  <asp:HiddenField runat="server" ID="CurID_Hid" />
  <div class="modal" id="add_div">
    <div class="modal-dialog" style="width:1024px;">
      <div class="modal-content">
        <div class="modal-header">
          <button type="button" class="close" data-dismiss="modal"><span class="fa fa-remove-sign">Close</span></button>
          <span class="modal-title"><strong>添加报纸</strong></span> </div>
        <div class="modal-body">
          <table class="table table-bordered table-hover">
            <tr>
              <td>报纸名：</td>
              <td><asp:TextBox runat="server" ID="NewsName_T" CssClass="form-control text_md" />
                <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="NewsName_T" ForeColor="Red" ErrorMessage="不能为空" ValidationGroup="Add" /></td>
            </tr>
            <tr>
              <td>分类：</td>
              <td><asp:DropDownList ID="NodeList_D" CssClass="form-control text_md" runat="server">
                  <asp:ListItem Text="请选择分类" Value="0" />
                </asp:DropDownList></td>
            </tr>
            <tr>
              <td>操作：</td>
              <td><asp:Button runat="server" ID="Add_Btn" OnClick="Add_Btn_Click" Text="添加" ValidationGroup="Add" CssClass="btn btn-primary" /></td>
            </tr>
          </table>
        </div>
        <div class="modal-footer" style="text-align: center;"> </div>
      </div>
    </div>
  </div>
</div>
</div>
<div class="modal" id="add_node">
<div class="modal-dialog" style="width: 1024px;">
  <div class="modal-content">
    <div class="modal-header">
      <button type="button" class="close" data-dismiss="modal"><span class="fa fa-times-circle-o">Close</span></button>
      <span class="modal-title"><strong>添加报纸</strong></span> </div>
    <div class="modal-body">
      <table class="table table-bordered table-hover">
        <tr>
          <td>分类名：</td>
          <td><asp:TextBox runat="server" ID="NodeName_T" CssClass="form-control text_md" />
            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="NodeName_T" ForeColor="Red" ErrorMessage="不能为空" ValidationGroup="Add_Node" /></td>
        </tr>
        <tr>
          <td>报纸分类：</td>
          <td><asp:DropDownList ID="ParentIDS_D" CssClass="form-control text_md" runat="server">
              <asp:ListItem Text="请选择父分类" Value="0" />
            </asp:DropDownList></td>
        </tr>
        <tr>
          <td>操作：</td>
          <td><asp:Button runat="server" ID="AddNode_B" OnClick="AddNode_Btn_Click" Text="添加分类" ValidationGroup="Add_Node" CssClass="btn btn-primary" /></td>
        </tr>
      </table>
    </div>
    <div class="modal-footer" style="text-align: center;"> </div>
  </div>
</div>
</div>
<input id="edit_href" data-toggle='modal' data-target='#add_div' type="button" style="display:none;" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
    function ShowModal() {
        $("#edit_href").click();
    }
    function ClearHid() {
        $("#CurID_Hid").val("");
        $("#NewsName_T").val("");
        $("#Add_Btn").val("添加");
    }
    function ShowInfo(id) {
        $("#Publist_IF").attr("src", "PublishDesc.aspx?Nid=" + id);
    }
</script>
</asp:Content>
