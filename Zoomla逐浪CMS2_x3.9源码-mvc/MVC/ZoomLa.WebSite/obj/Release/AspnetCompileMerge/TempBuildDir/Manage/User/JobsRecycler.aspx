<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JobsRecycler.aspx.cs" Inherits="ZoomLaCMS.Manage.User.JobsRecycler"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>会员组模型</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField runat="server" ID="Label1_Hid" Value="添加房间" />
  <div class="us_seta"  id="manageinfo" runat ="server">
    <asp:HiddenField ID="HdnModelID" runat="server" />
    <asp:HiddenField ID="HdnModelName" runat="server" />
    <ZL:ExGridView ID="Egv" runat="server" AllowPaging="True" AutoGenerateColumns="False"  class="table table-striped table-bordered table-hover" DataKeyNames="ID"  Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" EmptyDataText="暂无数据" OnPageIndexChanging="Egv_PageIndexChanging" OnRowDataBound="Egv_RowDataBound">
      <EmptyDataRowStyle BackColor="White" Font-Bold="True" ForeColor="Black" Height="50px" HorizontalAlign="Center" VerticalAlign="Middle" />
      <FooterStyle BackColor="#DBF9D9" Font-Bold="True" ForeColor="White" />
      <RowStyle BackColor="#EBFCEA" Height="25px" />
      <EditRowStyle BackColor="#2461BF" />
      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
      <PagerStyle BackColor="#DBF9D9" ForeColor="#333333" HorizontalAlign="Center" />
      <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
      <AlternatingRowStyle BackColor="White" />
    </ZL:ExGridView>
    <div>
      <asp:CheckBox ID="CheckBox1" runat="server" Text="全选" AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged" />
      <asp:Button ID="btn_DeleteRecords" runat="server" OnClientClick="return judgeSelect();" Text="删除选中记录" OnClick="btn_DeleteRecords_Click" class="btn btn-primary"/>
      <asp:Button ID="btn_ResumeRecords" runat="server" OnClientClick="return judgeSelect();" Text="还原选中记录" OnClick="btn_ResumeRecords_Click" class="btn btn-primary"/>
      <asp:Button ID="Button1" runat="server"  Text="还原所有" onclick="Button1_Click" class="btn btn-primary"/>
      <asp:Button ID="Button2" runat="server" OnClientClick="return judgeSelect();" Text="删除所有" onclick="Button2_Click"  class="btn btn-primary"/>
    </div>
  </div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script type="text/javascript">
    //根据传入的checkbox的选中状态设置所有checkbox的选中状态
    function selectAll(obj) {
        var allInput = document.getElementsByTagName("input");
        //alert(allInput.length);
        var loopTime = allInput.length;
        for (i = 0; i < loopTime; i++) {
            //alert(allInput[i].type);
            if (allInput[i].type == "checkbox") {
                allInput[i].checked = obj.checked;
            }
        }
    }
    //判断是否选中记录，用户确认删除
    function judgeSelect() {
        var result = false;
        var allInput = document.getElementsByTagName("input");
        var loopTime = allInput.length;
        for (i = 0; i < loopTime; i++) {
            if (allInput[i].checked) {
                result = true;
                break;
            }
        }
        if (!result) {
            alert("请先选则要删除的记录！");
            return result;
        }
        result = confirm("你确认要删除选定的记录吗？");
        return result;
    }
</script>
</asp:Content>

