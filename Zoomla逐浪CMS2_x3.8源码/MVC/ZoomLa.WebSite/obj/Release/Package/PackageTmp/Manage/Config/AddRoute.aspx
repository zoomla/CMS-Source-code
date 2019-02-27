<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRoute.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.AddRoute" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>路由管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered">
        <tr>
          <td class="td_m">类型</td>
          <td><asp:DropDownList runat="server" ID="SType_DP" CssClass="form-control text_300">
              <asp:ListItem Value="1">CMS_站点路由</asp:ListItem>
              <asp:ListItem Value="2">动力模块_域名路由</asp:ListItem>
            </asp:DropDownList></td>
        </tr>
        <tr>
          <td>域名</td>
          <td><div class="input-group"> <span class="input-group-addon">http://</span>
              <asp:TextBox runat="server" ID="domain_t" CssClass="form-control" style="width:232px;" data-enter="1"/>
            </div>
           <span class="rd_green">必须解析到网站服务器并开放80端口独占状态,示例:bbs.z01.com</span>
          </td>
        </tr>
        <tr>
          <td>指向</td>
          <td>
              <asp:TextBox runat="server" ID="Url_T"  CssClass="form-control text_300" data-enter="2" placeholder="域名路由该项可为空"/>
            <div class="rd_green">指向目录:/Guest/Bar/*.aspx</div>
            <div class="rd_green">指向页面:/Class_1/Default.aspx</div>
          </td>
        </tr>
        <tr><td>关联用户</td><td>
            <asp:TextBox runat="server" ID="UserID_T" CssClass="form-control text_300" /> <button type="button" onclick="ShowSelUser()" class="btn btn-primary">选择用户</button>
            <asp:HiddenField ID="UserID_Hid" runat="server" />
        </td></tr>
        <tr><td>备注</td><td><asp:TextBox runat="server" ID="Remind_T" CssClass="form-control text_300" style="height:100px;"  TextMode="MultiLine"/></td></tr>
        <tr><td></td><td>
            <asp:Button runat="server" CssClass="btn btn-primary" ID="Save_Btn" Text="保存" OnClick="Save_Btn_Click"  />
            <a class="btn btn-primary" href="RouteConfig.aspx">返回</a></td></tr>
      </table>
    
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        var userDiag = new ZL_Dialog();
        function UserFunc(list) {
            $("#UserID_T").val(list[0].UserName);
            $("#UserID_Hid").val(list[0].UserID);
            CloseComDiag();
        }
    </script>
</asp:Content>