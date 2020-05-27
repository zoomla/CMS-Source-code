<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageList.aspx.cs" Inherits="Manage_Shop_Printer_MessageList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>打印流水</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
 <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="20" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand" OnRowDataBound="EGV_RowDataBound"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
        <Columns>
            <asp:TemplateField ItemStyle-CssClass="td_s">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="别名">
                <ItemTemplate>
                    <a href="MessageList.aspx?DevID=<%#Eval("DevID") %>" title="按打印机筛选"><%#Eval("Alias") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="所属门店" DataField="ShopName" />
            <asp:TemplateField HeaderText="内容">
                <ItemTemplate>
                    <%#GetContent() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="发送时间" DataField="ReqTime" DataFormatString="{0:yyyy-MM-dd  HH:mm:ss}" />
            <asp:TemplateField HeaderText="返回">
                <ItemTemplate>
                    <%#msgBll.DealReqStatus(Eval("ReqStatus","")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="AddMsg.aspx?ID=<%#Eval("ID") %>">查看详情</a> 
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="reprint" OnClientClick="return confirm('确定要重新发送打印吗');">重打印</asp:LinkButton>
                   <%-- <a class="option_style" href=".aspx?id=<%#Eval("ID") %>"><i class="fa fa-pencil" title="修改"></i></a>--%>
<%--                    <asp:LinkButton runat="server" CssClass="option_style" CommandName="del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除吗');"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>--%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>