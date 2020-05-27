<%@ Page Language="C#"  MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="CollectionList.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Collect.CollectionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>采集状态</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
        <ZL:ExGridView ID="Egv" CssClass="table table-bordered table-hover table-striped" runat="server" AllowPaging="True" AutoGenerateColumns="False" DataKeyNames="C_IID" PageSize="20" Width="100%" OnPageIndexChanging="Egv_PageIndexChanging">
        <Columns>
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSel" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="4%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="C_IID">
                <HeaderStyle Width="5%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="标  题">
                <ItemTemplate>
                    <%#GetTitle(DataBinder.Eval(Container.DataItem, "CollID").ToString())%>
                </ItemTemplate>
                <HeaderStyle Width="30%" />
                <ItemStyle CssClass="tdbg" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="网站名称">
                <ItemTemplate>
                    <%#GetItemName(DataBinder.Eval(Container.DataItem, "ItemID").ToString())%>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="所属栏目">
                <ItemTemplate>
                    <%#GetNode(DataBinder.Eval(Container.DataItem, "NodeID").ToString())%>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="所属模型">
                <ItemTemplate>
                    <%#GetMode(DataBinder.Eval(Container.DataItem, "ModeID").ToString())%>
                </ItemTemplate>
                <HeaderStyle Width="8%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="采集页面">
                <ItemTemplate>
                    <a href='<%#DataBinder.Eval(Container.DataItem, "OldUrl")%>' target="_blank">浏览</a>
                </ItemTemplate>
                <HeaderStyle Width="4%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="结果">
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem, "State").ToString()=="1"?"<font color=\"#00cc00\">成功</font>":"<font color=\"#cc0000\">失败</font>"%>
                </ItemTemplate>
                <HeaderStyle Width="5%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick="return confirm('确定删除该数据?')" OnClick="LinkButton1_Click"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
                <HeaderStyle Width="5%" />
                <ItemStyle CssClass="tdbg" HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <RowStyle ForeColor="Black"/>
        <PagerStyle ForeColor="Black" HorizontalAlign="Center" />
        <HeaderStyle Font-Bold="True" />
    </ZL:ExGridView>
</asp:Content>