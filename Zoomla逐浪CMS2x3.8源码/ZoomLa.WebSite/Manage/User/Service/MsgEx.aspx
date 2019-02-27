<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MsgEx.aspx.cs" Inherits="manage_User_MsgEx"  MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>客服聊天记录</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" PageSize="10" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据！！" OnRowCommand="EGV_RowCommand" OnPageIndexChanging="EGV_PageIndexChanging">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发送者">
                <ItemTemplate>
                    <a href="Userinfo.aspx?id=<%#Eval("UserID") %>" target="_blank"><%#GetSender() %></a> 
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="接收者">
                <ItemTemplate>
                    <%#GetReceUser() %>
                </ItemTemplate>
            </asp:TemplateField>
<%--            <asp:TemplateField HeaderText="内容">
                <ItemTemplate>
                    <%#ZoomLa.Common.StringHelper.SubStr(Eval("Content")) %>
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:BoundField HeaderText="发送时间" DataField="CDate" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="MsgInfo.aspx?id=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton ID="Del" runat="server" CommandName="del" OnClientClick="return confirm('确定删除该记录?')" CommandArgument='<%#Eval("ID") %>'><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    <a href="MsgList.aspx?suid=<%#Eval("UserID") %>&ruid=<%#Eval("ReceUser") %>" title="查看聊天详情" target="_blank" class="option_style"><i class="fa fa-eye" title="查看"></i>查看详情</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div>
      <asp:Button ID="DelChats" class="btn btn-primary" Style="width: 110px;" runat="server" OnClientClick="return confirm('确定删除选中记录？')" OnClick="DelChats_Click"  Text="批量删除" />
      <asp:Button ID="DelByWeek" class="btn btn-primary" OnClientClick="return confirm('确定删除上一周的记录？')" OnClick="DelByWeek_Click" runat="server" Text="删除上周数据" />
    </div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        $().ready(function () {
            $("#EGV tr").dblclick(function () {
                var id = $(this).find("input[name=idchk]").val();
                if (id) {
                    window.location = "MsgInfo.aspx?id=" + id;
                }
            });
        });
        var userDiag = new ZL_Dialog();
        function ShowUInfo(uid) {
            userDiag.title = "用户信息";
            userDiag.reload = true;
            userDiag.url = "Userinfo.aspx?id=" + uid;
            userDiag.ShowModal();
        }
    </script>
</asp:Content>
