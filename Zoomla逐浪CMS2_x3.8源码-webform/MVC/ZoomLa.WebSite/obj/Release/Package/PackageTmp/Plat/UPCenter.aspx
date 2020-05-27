<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UPCenter.aspx.cs" Inherits="ZoomLaCMS.Plat.UPCenter" MasterPageFile="~/Plat/Main.master" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>个人信息</title>
<link type="text/css" rel="stylesheet" href="/dist/css/font-awesome.min.css" />
<style type="text/css">
#Content_Body { margin-top: 15px; }
.tdname { text-align: right; width: 100px; }
.tdcontent { text-align: left; }
ul { list-style-type: none; }
#plattable tr td img { width: 30px; height: 30px; float: left; }
.ViBosDiv { float: left; margin-left: 10px; margin-right: 10px; margin-top: 8px; }
.ViBoStatu_T { margin-top: 10px; font-size: 12px; color: green; }
.ViBoStatu_F { margin-top: 10px; color: red; }
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="Content" class="container platcontainer">
<div class="child_head"><span class="child_head_span1"></span> <span class="child_head_span2">个人信息</span></div>
<ul class="nav nav-tabs">
  <li class="active"><a href="#Tabs0" data-toggle="tab">个人信息</a></li>
  <li><a href="#Tabs1" data-toggle="tab">平台绑定</a></li>
  <li><a href="/User/Change/Pwd">修改密码</a></li>
</ul>
<div class="tab-content">
  <div class="tab-pane active" id="Tabs0">
    <div id="admin_div">
      <table class="table table-bordered table-hover table-striped">
        <tr>
          <td class="tdname">用户名：</td>
          <td class="tdcontent"><asp:TextBox ID="UserName_T2" CssClass="form-control text_md" ReadOnly="true" runat="server" /></td>
        </tr>
        <tr>
          <td class="tdname">用户组：</td>
          <td class="tdcontent"><asp:TextBox ID="Plat_Group_T2" onkeydown="return GetEnterCode('focus','Status_T2');" CssClass="form-control text_md" Enabled="false" runat="server" /></td>
        </tr>
        <tr>
          <td class="tdname">状态：</td>
          <td class="tdcontent"><asp:TextBox ID="Status_T2" onkeydown="return GetEnterCode('focus','TrueName_T2');" CssClass="form-control text_md" runat="server" Enabled="false" Text="正常" /></td>
        </tr>
        <tr>
          <td class="tdname">昵称：</td>
          <td class="tdcontent"><asp:TextBox ID="TrueName_T2" onkeydown="return GetEnterCode('focus','Mobile_T2');" CssClass="form-control text_md" runat="server" />
            <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="TrueName_T2" ErrorMessage="昵称不能为空" ForeColor="Red" Display="Dynamic" ValidationGroup="Add" /></td>
        </tr>
        <tr>
          <td class="tdname">移动电话：</td>
          <td class="tdcontent"><asp:TextBox ID="Mobile_T2" onkeydown="return GetEnterCode('focus','Post_T2');" CssClass="form-control text_md" runat="server" /></td>
        </tr>
        <tr>
          <td class="tdname">工作岗位：</td>
          <td class="tdcontent"><asp:TextBox ID="Post_T2" onkeydown="return GetEnterCode('click','Save_Btn');" CssClass="form-control text_md" runat="server" /></td>
        </tr>
        <tr>
          <td class="tdname">个人头像：</td>
            <td>
                <ZL:SFileUp ID="SFile_Up" IsCompress="true" FType="Img" runat="server" />
            </td>
        </tr>
        <tr>
          <td class="tdname">用户角色：</td>
          <td><ul>
              <asp:Repeater runat="server" ID="Role_Rep" Visible="false">
                <ItemTemplate>
                    <li>
                        <label>
                            <input type="checkbox" name="UserRole_Chk" value="<%#Eval("ID") %>" <%#IsChecked() %> />
                            <%#Eval("RoleName") %>
                        </label>
                    </li>
                </ItemTemplate>
              </asp:Repeater>
              <asp:Repeater runat="server" ID="Role_View_Rep" Visible="false">
                <ItemTemplate>
                  <li><%#Eval("RoleName")+"," %></li>
                </ItemTemplate>
              </asp:Repeater>
            </ul></td>
        </tr>
        <tr>
          <td class="tdname">操作： </td>
          <td><asp:Button runat="server" CssClass="btn btn-primary" ID="AdminSave_Btn" OnClick="AdminSave_Btn_Click" Text="提交修改" ValidationGroup="Add" Visible="false" />
            <asp:Button runat="server" CssClass="btn btn-primary" ID="Save_Btn" OnClick="Save_Btn_Click" Text="提交修改" ValidationGroup="Add" Visible="false" /></td>
        </tr>
      </table>
    </div>
    <div id="Content_foot"> </div>
  </div>
  <div class="tab-pane" id="Tabs1">
    <table id="plattable" class="table table-bordered table-hover table-striped">
      <tr>
        <td><span runat="server" id="sinaimg" class="fa fa-weibo" style="font-size:30px;"></span> <span id="sinaStatu_D" runat="server" class="ViBoStatu_F">(未绑定)</span>
          <asp:Button runat="server" Text="绑定设置" OnClick="bindVibo_B_Click" CssClass="btn btn-primary" ID="Sina_Btn" /></td>
      </tr>
      <tr>
        <td><span runat="server" id="qqimg" class="fa fa-qq" style="font-size:30px;"></span> <span id="QQStatus_Div" runat="server" class="ViBoStatu_F">(未绑定)</span> 
          <a href="https://graph.qq.com/oauth2.0/authorize?client_id=101187045&amp;response_type=token&amp;scope=all&amp;redirect_uri=http://www.1th.cn/Plat/Common/GetViBoToken.aspx?s=qq">
             <span runat="server" id="QQSPan" class="btn btn-primary">绑定设置</span>
          </a></td>
      </tr>
    </table>
  </div>
</div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script src="/JS/DatePicker/WdatePicker.js" type="text/javascript"></script>
<script>
function checkFile() {
    var filename = $("#File_Up").val();
    if (filename != "") {
        var checkex = ["jpg", "png", "gif", "ico"];
        var exname = filename.substr(filename.lastIndexOf(".") + 1, filename.length - filename.lastIndexOf(".") + 1);
        for (var i = 0; i < checkex.length; i++) {
            if (checkex[i] == exname)
                return true;
        }
        alert("图片格式不正确！");
    } else {
        alert("没有选择图片！");
    }
    return false;
}
function SetPlat() {
    $(".nav-tabs li:eq(1) a").click();
}
$(function () {
    setactive("公司");
    $("#prefile_img").hide();
})
</script>
</asp:Content>