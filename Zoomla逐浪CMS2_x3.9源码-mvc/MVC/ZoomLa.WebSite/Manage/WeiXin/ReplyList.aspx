<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReplyList.aspx.cs" Inherits="ZoomLaCMS.Manage.WeiXin.ReplyList"  MasterPageFile="~/Manage/I/Default.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>微信回复管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" OnRowDataBound="EGV_RowDataBound" 
        OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有内容">
        <Columns>
            <asp:TemplateField HeaderStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="关键词" DataField="Filter" ItemStyle-CssClass="td_m" />
            <asp:TemplateField HeaderText="类型" ItemStyle-CssClass="td_m" >
                <ItemTemplate>
                    <%#GetMsgType() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="默认">
                <ItemTemplate>
                    <%#GetIsDefault() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="回复信息" DataField="Content"/>
            <asp:TemplateField HeaderText="操作" ItemStyle-CssClass="td_l">
                <ItemTemplate>
                    <a href="AddReply.aspx?ID=<%#Eval("ID") %>&appid=<%=AppId %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del2" OnClientClick="return confirm('确定要删除吗');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
<asp:Button runat="server" ID="BatDel_Btn" OnClick="BatDel_Btn_Click" Text="批量删除" CssClass="btn btn-primary" OnClientClick="return confirm('确定要删除吗');" />
</asp:Content>
