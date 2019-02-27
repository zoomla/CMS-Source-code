<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StatisticalCode.aspx.cs" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master" Inherits="Manage_Counter_StatisticalCode" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>统计代码</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered stati_code">
        <tr>
            <td class="td_l tdleft" style="line-height:115px;">第三方站长统计脚本：</td>
            <td>
                <asp:TextBox ID="Code_T" runat="server" CssClass="form-control codetext m715-50" TextMode="MultiLine" Height="115"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdleft">获取第三方代码： </td>
            <td>
                <div class="couter_div"><a title="百度统计" target="_blank" href="http://tongji.baidu.com"><img src="/images/bdidu.png" /></a></div>
                <div class="couter_div"><a title="腾讯分析" target="_blank" href="http://mta.qq.com/"><span class="fa fa-qq"></span></a></div>
                <div class="couter_div"><a title="站长统计" target="_blank" href="http://www.cnzz.com/"><span class="fa fa-line-chart"></span></a></div>
                <div class="couter_div"><a title="我要啦统计" target="_blank" href="http://www.51.la/"><span>51</span></a></div>

            </td>
        </tr>
        <tr>
            <td class="tdleft">引入站内统计：</td>
            <td>
               <a href="javascript:;" onclick="AddZLCount();" class="btn btn-primary"><i class="fa fa-line-chart"></i> 点击引入</a>
            </td>
        </tr>
        <tr>
            <td></td><td>
                <asp:Button ID="SaveCode_T" OnClick="SaveCode_T_Click" Text="保存" runat="server" CssClass="btn btn-primary" /> 
                <button type="button" onclick="$('.codetext').val('')" class="btn btn-primary">重置</button>
            </td>
        </tr>
    </table>
    <style>
        .stati_code .couter_div{float:left;margin-left:20px;}
        .stati_code .couter_div a{line-height:30px;text-decoration:none;}
         .stati_code .couter_div a:hover{text-decoration:none;}
        .stati_code .couter_div span{font-size:30px; font-weight:700;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
  <script>
      var code = '<iframe style=\"display:none;\" src=\"/CallCounter.aspx\"></iframe>';
      function AddZLCount() {
          var val = $("#Code_T").val();
          val = val.replace(code, "");
          $("#Code_T").val(val + code);
      }
  </script>
</asp:Content>


