<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PubList.aspx.cs" Inherits="Design_User_PubList"  MasterPageFile="~/Design/Master/User.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>互动信息</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="siteinfo container">
    <ol class="breadcrumb">
        <li><a href="/Design/">站点创作</a></li>
        <li><a href="/design/user">控制中心</a></li>
        <li class="active">互动信息</li>
    </ol>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
            OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
            CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="数据为空">
            <Columns>
                <asp:TemplateField ItemStyle-CssClass="td_s">
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="场景名称">
                    <ItemTemplate><a href="PubList.aspx?h5id=<%#Eval("H5ID") %>" title="点击筛选"><%#Eval("Title") %></a></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="表单名称">
                    <ItemTemplate>
                        <a href="PubList.aspx?fname=<%#HttpUtility.UrlEncode(Eval("FormName","")) %>" title="点击筛选"><%#Eval("FormName") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="表单内容">
                    <ItemTemplate>
                        <%#GetContent() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户">
                    <ItemTemplate><%#GetUser() %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="IP">
                    <ItemTemplate>
                        <%#GetIP() %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CDate" DataFormatString="{0:yyyy年MM月dd日}" HeaderText="创建时间" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del2" OnClientClick="return confirm('确定要删除吗');" class="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
    </ZL:ExGridView>
    <asp:Button runat="server" ID="BatDel_Btn" Text="批量删除" OnClick="BatDel_Btn_Click" OnClientClick="return confirm('确定要删除吗');" class="btn btn-info" />
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>