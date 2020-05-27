<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Jobsinfos.aspx.cs" Inherits="manage_User_Jobsinfos" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>会员组模型</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="manageinfo" runat="server" class="table table-striped table-bordered table-hover">
        <asp:HiddenField ID="HdnModelID" runat="server" />
        <asp:HiddenField ID="HdnModelName" runat="server" />
        <asp:HiddenField ID="HiddenPage" runat="server" />
        <asp:HiddenField ID="Hiddenpagenum" runat="server" />
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  GridLines="None"
             CellPadding="2" CellSpacing="1"  Width="98%" CssClass="table border" EmptyDataText="当前没有信息!!" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >
                <Columns>
                    <asp:TemplateField HeaderText="操作"></asp:TemplateField>
                   </Columns>
                    <PagerStyle HorizontalAlign="Center"/>
                   <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
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
        //判断是否选中记录，判断是否取消生成
        function IsCreateSelect() {
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
                alert("请选则要还原记录！");
                return result;
            }
            result = confirm("确认要还原选定记录？");
            return result;
        }
</script>
</asp:Content>

