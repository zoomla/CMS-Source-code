<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChartCite.aspx.cs" Inherits="Manage_Content_ECharts_ChartCite" MasterPageFile="~/User/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>图表引用</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <div class="panel panel-info">
          <div class="panel-heading"><span class="margin_l5">图表引用</span></div>
          <div class="panel-body">
              <table class="table table-bordered table-striped">
                  <tr><td class="td_m">图表名:</td><td><asp:Label runat="server" ID="TName_L"></asp:Label></td></tr>
                  <tr><td>类型:</td><td><asp:Label runat="server" ID="SType_L"></asp:Label></td></tr>
                  <tr>
                  <td>引用:</td>
                  <td style="position:relative;">
                      <asp:TextBox runat="server" ID="Cite_T" TextMode="MultiLine" Height="100" Width="400" CssClass="form-control" />
                      <a id="copycode" href="javascript:;"  class="btn btn-primary btn-xs">获取代码</a> <a  href="Default.aspx"  class="btn btn-primary btn-xs">返回列表</a>
                  </td>
                  </tr>
              </table>
              <div class="alert alert-info">点击获取代码按钮，粘贴到您想引用的html代码里</div>
          </div></div>
    <script type="text/javascript" src="/JS/jquery.zclip.min.js"></script>
    <script>
        $().ready(function () {
            $('#copycode').zclip({
                path: '/JS/ZeroClipboard.swf',
                copy: function () {
                    return $("#Cite_T").val();
                },
                afterCopy: function () { alert("复制完成"); }
            });
        });
    </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>