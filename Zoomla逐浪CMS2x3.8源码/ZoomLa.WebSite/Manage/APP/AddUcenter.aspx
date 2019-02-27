<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUcenter.aspx.cs" Inherits="manage_APP_AddUcenter" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css"  rel="stylesheet"/>
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <title>添加授权</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <table class="table table-striped table-bordered table-hover">
      <tr class="spacingtitle" style="height:30px;">
        <td align="center" colspan="2" ><asp:Label ID="lblText" runat="server">添加授权网站</asp:Label></td>
      </tr>
         <tr><td class="tdleft"><strong>授权名称：</strong></td><td><asp:TextBox runat="server" ID="Alias_T" CssClass="form-control text_300 required" /></td></tr>
      <tr>
        <td class="td_l tdleft"><strong>网站域名：</strong></td>
        <td><asp:TextBox ID="TxtWebSite" class="form-control text_300 isurl" runat="server" placeholder="www.z01.com" />
            <asp:RequiredFieldValidator runat="server" ID="R1" ControlToValidate="TxtWebSite" Display="Dynamic" ErrorMessage="网址不能为空" ForeColor="Red" />
        </td>
      </tr>
         <tr><td class="tdleft"><strong>App Key：</strong></td><td><asp:Label runat="server" ID="Key_L" /></td></tr>
         <tr><td class="tdleft"><strong>用户管理：</strong></td>
             <td>
                 <label><input type="checkbox" value="adduser" name="userauth" />增加</label>
                 <label><input type="checkbox" value="deluser" name="userauth"/>删除</label>
                 <label><input type="checkbox" value="edituser" name="userauth"/>修改</label>
                 <label><input type="checkbox" value="seluser" name="userauth"/>查询</label>
             </td></tr>
             <tr><td class="tdleft"><strong>百科问答：</strong></td>
             <td>
                 <label><input type="checkbox" value="addask" name="askauth"/>增加</label>
                 <label><input type="checkbox" value="delask" name="askauth"/>删除</label>
                 <label><input type="checkbox" value="editask" name="askauth"/>修改</label>
                 <label><input type="checkbox" value="selask" name="askauth"/>查询</label>
             </td></tr>
             <tr><td class="tdleft"><strong>数据库用户与密码：</strong></td>
             <td>
                 <asp:TextBox runat="server" ID="DBUName_T" CssClass="form-control text_300" placeholder="数据库用户名" /><br />
                 <asp:TextBox runat="server" ID="DBPwd_T" CssClass="form-control text_300 margin_t5" TextMode="Password" /><br />
                 <span class="rd_green" >
                     1,数据库中添加一个用户<br />
                     2,为这个用指定权限(例如对 ZL_User表的查询权限)<br />
                     3,在这里填入用户名与密码<br />
                     (该用户名与密码仅针对 Insert,Del,Update,Select四个方法,用于实现WebServices中未提供的接口)
                 </span>
             </td></tr>
             <tr><td class="tdleft"><strong>启用：</strong></td><td><input type="checkbox" runat="server" id="Status_Chk" checked="checked" class="switchChk" /></td></tr>
         <tr><td colspan="2" style="text-align:center" >
             <asp:Button ID="BtnCommit" runat="server" Text="提交" class="btn btn-primary" OnClick="Button1_Click" />
             <a href="WsApi.aspx" class="btn btn-primary" style="margin-left:5px;">返回</a></td></tr>
  </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
<script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
<script src="/JS/SelectCheckBox.js"></script>
<script src="/JS/jquery.validate.min.js"></script>
<script src="/JS/ZL_Regex.js"></script>
    <script>
        $(function () {
            $.validator.addMethod("isurl", function (value) {
                value = StrHelper.UrlDeal(value);
                return ZL_Regex.isUrl(value);
            }, "链接格式不正确");
            $("form").validate({});
        })
    </script>
</asp:Content>