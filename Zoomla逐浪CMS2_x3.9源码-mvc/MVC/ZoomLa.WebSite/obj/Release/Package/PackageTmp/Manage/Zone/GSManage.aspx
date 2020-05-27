<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GSManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.GSManage"  MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>空间族群列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="暂无相关信息！！">
            <Columns>
                <asp:TemplateField HeaderText="选择">
                    <ItemTemplate>
                        <input name="chkSel" type="checkbox" value='<%#Eval("ID")  %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="族群图标">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" Height="54px" Width="60px" ImageUrl='<%# ZoomLa.Components.SiteConfig.SiteOption.UploadDir + Eval("GSICO")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="GSName" HeaderText="族群名称">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="族群创建者">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <%#GetUserName(Eval("UserID","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="族群状态">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <%#GetState(Eval("GSstate").ToString())%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatTime" HeaderText="创建时间">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>

                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="GSEdit.aspx?pid=<%# Eval("ID") %>">查看详细信息</a>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <asp:Button ID="Button1" class="btn btn-primary" runat="server" Text="加亮族群" CommandName="1" OnClick="btn_DeleteRecords_Click" />
                    <asp:Button ID="Button2" class="btn btn-primary" runat="server" CommandName="2" Text="冻结族群" OnClick="btn_DeleteRecords_Click" />
                    <asp:Button ID="Button3" class="btn btn-primary" runat="server" CommandName="0" Text="设为普通" OnClick="btn_DeleteRecords_Click" />
                    <asp:Button ID="btn_DeleteRecords" class="btn btn-primary" runat="server" CommandName="5" OnClientClick="if(!IsSelectedId()){alert('请先选则要删除的记录!')} else { return confirm('你确认要删除选定的记录吗？')}" Text="解散族群" OnClick="btn_DeleteRecords_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        $().ready(function () {
            $("#Egv tr>th:eq(0)").html("<input type=checkbox id='chkAll'/>");//EGV顶部
            //$("#Egv  tr>th").css("height", "30px").css("line-height", "30px");
            $("#chkAll").click(function () {//EGV 全选
                selectAllByName(this, "chkSel");
            });
        })
        function IsSelectedId()
        {
            var checkArr = $("input[type=checkbox][name=chkSel]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
    </script>
</asp:Content>
