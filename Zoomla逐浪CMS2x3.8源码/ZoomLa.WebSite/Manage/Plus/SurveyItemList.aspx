<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurveyItemList.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Plus.SurveyItemList" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>问卷投票问题列表</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField ID="HdnSID" runat="server" />
    <div class="clear"></div>
    <ZL:ExGridView ID="EGV" RowStyle-HorizontalAlign="Center" DataKeyNames="QID"
        CssClass="table table-striped table-bordered table-hover" GridLines="None" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" PageSize="10"  OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="Lnk_Click">
        <EmptyDataTemplate>无相关数据</EmptyDataTemplate>
        <EmptyDataRowStyle BackColor="#e8f4ff" Height="45px" BorderColor="#4197e2" />
        <Columns>
            <asp:BoundField DataField="QID" HeaderText="序号">
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="5%" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="问题标题" HeaderStyle-Width="40%" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <a href="SurveyItem.aspx?SID=<%=Request["SID"] %>&QID=<%# Eval("QID") %>"><%# Eval("QTitle") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="类别">
                <ItemTemplate>
                    <%# GetQType(Eval("TypeID","{0}"),Eval("QID", "{0}")) %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
                <HeaderStyle Width="10%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="排序">
                <ItemTemplate>
                    <input type="text" name="orderid" data-oid="<%#Eval("Orderid") %>" data-id="<%# Eval("QID") %>" class="form-control text_xs text-center" value="<%#Eval("Orderid") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("QID") %>'>修改</asp:LinkButton>
                <asp:LinkButton ID="lbtnDel" runat="server" CommandName="Del" CommandArgument='<%# Eval("QID") %>' OnClientClick="return confirm('确实要删除吗？');">删除</asp:LinkButton>
                <asp:LinkButton ID="lbtnMoveUp" runat="server" CommandName="MovePre" CommandArgument='<%# Eval("QID") %>' CssClass="option_style"><i class="fa fa-arrow-up" title="上移"></i>上移</asp:LinkButton>
                <asp:LinkButton ID="lbtnMoveDown" runat="server" CommandName="MoveNext" CommandArgument='<%# Eval("QID") %>' CssClass="option_style"><i class="fa fa-arrow-down" title="下移"></i>下移</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
        <RowStyle Height="24px" HorizontalAlign="Center" />
    </ZL:ExGridView>
                <asp:HiddenField ID="Order_Hid" runat="server" Value="" />
    <asp:Button ID="Order_B" runat="server" OnClientClick="return SetOrder()" OnClick="Order_B_Click" Text="保存排序" CssClass="btn btn-primary" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/ZL_Regex.js"></script>
    <script>
        function CheckOrder() {
            var bl = true;
            $("[name='orderid']").each(function (i, v) {
                if (!ZL_Regex.isNum($(v).val())) {
                    alert("排序ID必须为数字!");
                    $(v).focus();
                    bl = false;
                    return false;
                }
            });
            return bl;

        }
        function SetOrder() {
            if (CheckOrder()) {
                $("[name='orderid']").each(function (i, v) {
                    if ($(v).val() != $(v).data('old')) {
                        $("#Order_Hid").val($("#Order_Hid").val() + "," + $(v).data('id') + "|" + $(v).val());
                    }
                });
                return true;
            }
            return false;
        }
       
    </script>
</asp:Content>
