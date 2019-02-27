<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JsonPManage.aspx.cs" Inherits="ZoomLaCMS.Manage.APP.JsonPManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>跨域调用</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" IsHoldState="false" PageSize="10" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="调用名称">
                <ItemTemplate>
                    <a href="AddJsonP.aspx?ID=<%#Eval("ID") %>"><%#Eval("Alias") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="主表">
                <ItemTemplate>
                     <%#GetStr(Eval("T1")) %>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="次表">
                <ItemTemplate>
                     <%#GetStr(Eval("T2")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="开放参数">
                <ItemTemplate>
                    <%#GetStr(Eval("Params")) %>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="备注">
                <ItemTemplate>
                    <%#GetStr(Eval("Remark")) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="启用">
                <ItemTemplate>
                    <%#Eval("MyState","").Equals("1")?"<i class='fa fa-check rd_green'></i>":" <i class='fa fa-close rd_red'></i>" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="AddJsonP.aspx?ID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i>修改</a>
                    <a target="_blank" href="<%#GetJsLink() %>">生成实例</a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <asp:Button ID="Dels_Btn" runat="server" CssClass="btn btn-primary" OnClientClick="return confirm('是否确定删除所选项?')" Text="批量删除" OnClick="Dels_Btn_Click" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
