<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="PubManage.aspx.cs" Inherits="Manage_I_Pub_PubManage" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title><%=Resources.L.互动模块管理 %></title>
    <style>
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <div>
        <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="PubID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Egv_RowCommand"
            IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" 
            EnableTheming="False" EnableModelValidation="True" EmptyDataText="<%$Resources:L,没有互动模块信息 %>" OnRowDataBound="Egv_RowDataBound">
            <Columns>
                <asp:TemplateField HeaderText="<%$Resources:L,选择 %>">
                    <ItemTemplate>
                        <input type="checkbox" name="idchk" value="<%#Eval("PubID") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <a href="ViewPub.aspx?Pubid=<%#Eval("PubID")%>&guang=all"> <%#Eval("Pubid") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,模块名称 %>">
                    <ItemTemplate>
                        <a href="pubinfo.aspx?menu=edit&id=<%#Eval("Pubid")%>"><%#Eval("Pubname") %></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,模块类型 %>">
                    <ItemTemplate>
                        <%#PubtypeName(Eval("Pubtype", "{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,信息类别 %>">
                    <ItemTemplate>
                        <%#GetClassName(Eval("PubClass", "{0}"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="<%$Resources:L,模型表名 %>" DataField="PubTableName" />
                <asp:TemplateField HeaderText="<%$Resources:L,调用标签 %>">
                    <ItemTemplate>
                        <%#GetLabel(Eval("PubType"),Eval("PubLoadstr"),Eval("Pubname")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:L,操作 %>">
                    <ItemTemplate>
                        <a href="pubinfo.aspx?menu=edit&id=<%#Eval("Pubid")%>" class="option_style"><i class="fa fa-pencil" title="<%=Resources.L.修改 %>"></i><%=Resources.L.修改 %></a>
                        <a href="Pubsinfo.aspx?Pubid=<%#Eval("PubID")%>&type=0" class="option_style"><i class="fa fa-magic" title="信息列表"></i>信息列表</a>
                        <a href="pubinfo.aspx?menu=copy&id=<%#Eval("Pubid")%>" onclick="return confirm('<%=Resources.L.确实要复制吗 %>?');" class="option_style"><i class="fa fa-copy" title="<%=Resources.L.复制 %>"></i><%=Resources.L.复制 %></a>
                        <a href="pubinfo.aspx?menu=delete&id=<%#Eval("Pubid")%>" onclick="return confirm('<%=Resources.L.确实要放入存档吗 %>?<%=Resources.L.确实要放入存档吗 %>!');" class="option_style"><i class="fa fa-save" title="<%=Resources.L.存档 %>"></i><%=Resources.L.存档 %></a>
                        <asp:LinkButton runat="server" CssClass="option_style" OnClientClick="return confirm('是否删除该项!')" CommandName="Del" CommandArgument='<%#Eval("PubID") %>'><i class="fa fa-trash-o"></i><%=Resources.L.删除 %></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center"  />
		    <RowStyle HorizontalAlign="Center" />
        </ZL:ExGridView>
        <asp:Button ID="Dels_Btn" runat="server" CssClass="btn btn-primary" OnClientClick="return confirm('是否删除该项!')" OnClick="Dels_Btn_Click" Text="<%$Resources:L,批量删除 %>" />
    </div>
    <script>
        HideColumn('1,3', "", 'Egv');
    </script>
</asp:Content>
