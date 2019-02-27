<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SurveyItemList.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.SurveyItemList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>问卷投票问题列表</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" DataKeyNames="QID"
        CssClass="table table-striped table-bordered table-hover" GridLines="None" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" PageSize="10"  OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="Lnk_Click" OnRowDataBound="EGV_RowDataBound">
        <EmptyDataTemplate>无相关数据</EmptyDataTemplate>
        <Columns>
            <asp:TemplateField><ItemTemplate><input type="checkbox" value="<%#Eval("QID") %>" name="idchk"/></ItemTemplate></asp:TemplateField>
            <asp:BoundField DataField="QID" HeaderText="ID" ItemStyle-CssClass="td_s" />
            <asp:TemplateField HeaderText="问题标题" HeaderStyle-Width="40%">
                <ItemTemplate>
                    <a href="SurveyItem.aspx?SID=<%=Sid %>&QID=<%# Eval("QID") %>"><%# Eval("QTitle") %></a>
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
                    <asp:LinkButton ID="lbtnMoveUp" runat="server" CommandName="MovePre" CommandArgument='<%# Eval("QID") %>' CssClass="option_style"><i class="fa fa-arrow-up" title="上移"></i>上移</asp:LinkButton>
                    <asp:LinkButton ID="lbtnMoveDown" runat="server" CommandName="MoveNext" CommandArgument='<%# Eval("QID") %>' CssClass="option_style"><i class="fa fa-arrow-down" title="下移"></i>下移</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:HiddenField ID="Order_Hid" runat="server" />
    <asp:Button runat="server" ID="BatDel_Btn" class="btn btn-primary" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('确定要删除吗');"  Text="批量删除"/>
    <asp:Button ID="Order_B" runat="server" OnClientClick="return SetOrder()" OnClick="Order_B_Click" Text="保存排序" CssClass="btn btn-primary" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/ZL_Regex.js"></script>
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
                var result = [];
                var $inputs = $("input[name='orderid']");
                for (var i = 0; i < $inputs.length; i++) {
                    var $obj = $($inputs[i]);
                    result.push({ id: $obj.data("id"), oid: $obj.val() });
                }
                $("#Order_Hid").val(JSON.stringify(result)); return true;
            }
            else { return false; }
        }
    </script>
</asp:Content>
