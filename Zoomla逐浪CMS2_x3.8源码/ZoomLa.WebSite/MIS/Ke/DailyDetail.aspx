<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DailyDetail.aspx.cs" Inherits="Plat_Blog_Declined" MasterPageFile="~/Plat/Empty.master" %>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<title>日程详情</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
  <div>
    <div style="padding-left:1em; ">
      <table id="DetailTable">
          <tr>
              <td><span class="fa fa-map-marker"></span> <span>名称： </span></td>
              <td>
                   <asp:TextBox runat="server" ID="Name" CssClass="form-control date" />
                   <asp:LinkButton runat="server" ID="Del_Btn" OnClick="Del_Btn_Click" OnClientClick="return confirm('确定要删除吗!!');"> <span class="fa fa-trash-o" title="删除"></span></asp:LinkButton>
              </td>
          </tr>
        <tr>
          <td><span class="fa fa-calendar"></span> <span>时间： </span></td>
          <td><asp:TextBox class="form-control" ID="StartDate" runat="server" onclick="WdatePicker();" Style="width: 150px;display:inline-block"></asp:TextBox>
            -
            <asp:TextBox class="form-control" ID="EndDate" runat="server" onclick="WdatePicker();" Style="width: 150px;display:inline-block"></asp:TextBox></td>
        </tr>
        <%--<tr>
          <td colspan="2"><span class="fa fa-user"></span> <a href="#">管理员</a><span>创建</span></td>
        </tr>--%>
        <%--<tr>
          <td><span class="fa fa-user"></span> <span>主负责人：</span> <a href="#"><span class="fa fa-plus" title="添加"></span></a></td>
          <td><asp:Label runat="server" ID="LeaderIDS_L"></asp:Label></td>
        </tr>
        <tr>
          <td><span class="fa fa-user"></span> <span>任务成员：</span> <a href="#"><span class="fa fa-plus" title="添加"></span></a></td>
          <td><asp:Label runat="server" ID="ParterIDS_L"></asp:Label></td>
        </tr>--%>
        <tr>
          <td><span class="fa fa-list-alt"></span> <span>详情： </span></td>
          <td><asp:TextBox runat="server" ID="Describe" CssClass="form-control required date" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
          <td colspan="2"><label><asp:CheckBox runat="server" ID="IsOpen_Chk" />
            <span class="fa fa-lock"></span><span>公开日程</span></label></td>
        </tr>
        <tr>
          <td colspan="2" class="text-center" runat="server"><asp:Button runat="server" ID="Edit_Btn" Text="保存" OnClick="Edit_Btn_Click" CssClass="btn btn-primary" />
            <input type="button" value="关闭" class="btn btn-default" onclick="HideMe();" />
            </td>
        </tr>
      </table>
    </div>
  </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<style type="text/css">
.fa { color: #AFAFAF; font-size: 1.3em; margin-right: 5px; }
#DetailTable tr td { padding-top: 10px; padding-bottom: 10px; }
#Name{width:314px;display:inline-block;}
#Del_Btn span{line-height:34px;}
#Describe{width:500px;height:80px;max-width:800px;}
</style>
<script>
    function UpdateData(id, content) {
        parent.UpdateData(id,content)
    }
    function DelData(id) {
        parent.DelData(id);
    }
    function HideMe() {
        parent.HideMe();
    }
</script>
</asp:Content>