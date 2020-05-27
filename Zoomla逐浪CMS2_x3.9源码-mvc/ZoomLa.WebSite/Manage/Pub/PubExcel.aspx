<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PubExcel.aspx.cs" Inherits="ZoomLaCMS.Manage.Pub.PubExcel" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title><%=Resources.L.互动Excel导出配置 %></title>
    <style>
        #AllID_Chk{display:none;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
        class="table table-striped table-bordered table-hover" EmptyDataText="<%$Resources:L,当前没有信息 %>"
        OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
        <Columns>
            <asp:BoundField HeaderText="ID" ItemStyle-CssClass="PubID" DataField="ID"/>
            <asp:BoundField HeaderText="<%$Resources:L,表名 %>" DataField="TableName"/>
            <asp:BoundField HeaderText="<%$Resources:L,字段名_中文 %>" DataField="CNames"/>
            <asp:BoundField HeaderText="<%$Resources:L,字段名_数据库 %>" DataField="Fields"/>
            <asp:TemplateField HeaderText="<%$Resources:L,操作 %>">
                <ItemTemplate>
                     <asp:LinkButton runat="server" CommandName="Edit2" CommandArgument='<%#Eval("ID") %>' ToolTip="<%$Resources:L,修改 %>" CssClass="option_style"><i class="fa fa-pencil" title="<%=Resources.L.修改 %>"></i></asp:LinkButton>
                    <asp:LinkButton runat="server" CommandName="Del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗!');" ToolTip="<%$Resources:L,删除 %>" CssClass="option_style"><i class="fa fa-trash-o" title="<%=Resources.L.删除 %>"></i><%=Resources.L.删除 %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle Height="24px" HorizontalAlign="Center" />
    </ZL:ExGridView>
    <asp:HiddenField ID="curid_hid" runat="server" />
    <asp:Button ID="CurEdit_B" style="display:none;" runat="server" OnClick="CurEdit_B_Click" />
    <div id="add_div" style="display:none;">
                    <asp:HiddenField runat="server" ID="ID_Hid"/><!--标识是修改还是添加-->
                    <table class="table table-bordered table-striped">
                        <tr><td><%=Resources.L.表名 %>：</td><td><asp:TextBox runat="server" ID="TableName_T" CssClass="form-control text_md"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="r1" ErrorMessage="<%$Resources:L,表名不能为空 %>" ForeColor="Red" ValidationGroup="add" Display="Dynamic" ControlToValidate="TableName_T" /></td></tr>
                        <tr><td><%=Resources.L.字段名_数据库 %>：</td><td><asp:TextBox runat="server" ID="Fields_T" TextMode="MultiLine" style="width:500px;height:100px;"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="r2" ErrorMessage="<%$Resources:L,字段名不能为空 %>" ForeColor="Red" ValidationGroup="add" Display="Dynamic" ControlToValidate="Fields_T" />
                            示例:UserID,UserName</td></tr>
                        <tr><td><%=Resources.L.字段名_中文 %>：</td><td><asp:TextBox runat="server" ID="CNames_T" TextMode="MultiLine" style="width:500px;height:100px;"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ID="r3" ErrorMessage="<%$Resources:L,字段名_中文不能为空 %>" ForeColor="Red" ValidationGroup="add" Display="Dynamic" ControlToValidate="CNames_T" />
                            示例:用户ID，用户名</td></tr>
                        <tr>
                            <td colspan="2" class="text-center"><asp:Button runat="server" ID="Add_Btn" Text="<%$Resources:L,保存 %>" OnClick="Add_Btn_Click"  CssClass="btn btn-primary" ValidationGroup="add" />
                    <button type="button" class="btn btn-default" data-dismiss="modal"><%=Resources.L.关闭 %></button></td>
                        </tr>
                    </table>
       </div>
         
    <a href="javascript:;" data-toggle='modal' data-target='#add_div' id="model_a"></a>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript">
        var exceldiag = new ZL_Dialog();
        $().ready(function () {
            $("#EGV tr").dblclick(function () {
                var id = $(this).find(".PubID").text();
                $("#curid_hid").val(id);
                $("#CurEdit_B").click();
            });
        });
        function ShowModel()
        {
            exceldiag.title = "<%=Resources.L.添加Excel输出规则 %>";
            exceldiag.content = "add_div";
            exceldiag.ShowModal();
        }
        function Clear()
        {
            $("#ID_Hid").val("");
            $("#TableName_T").val("");
            $("#Fields_T").val("");
            $("#CNames_T").val("");
            ShowModel();
        }
    </script>
</asp:Content>