<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PubRecycler.aspx.cs" Inherits="ZoomLaCMS.Manage.Pub.PubRecycler" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title><%=Resources.L.存档管理 %> </title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div class="panel panel-default" style="padding:0px;">
        <div class="panel panel-body" style="padding:0px;margin:0px;">
            <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="PubID" PageSize="10" OnRowDataBound="Egv_RowDataBound" 
        OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover"
         EnableTheming="False" EnableModelValidation="True" EmptyDataText="<%$Resources:L,暂无相关信息 %>">
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:L,选择 %>">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("PubID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="PubID" HeaderStyle-Width="5%" />
            <asp:BoundField HeaderText="<%$Resources:L,模块名称 %>" DataField="Pubname" HeaderStyle-Width="20%" />
            <asp:TemplateField HeaderText="<%$Resources:L,模块类型 %>">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                    <%#PubtypeName(Eval("Pubtype", "{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,信息类别 %>">
                <ItemStyle Width="10%" />
                <ItemTemplate>
                    <%#GetClassName(Eval("PubClass", "{0}"))%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="<%$Resources:L,模型表名 %>" DataField="PubTableName" HeaderStyle-Width="10%" />
            <asp:TemplateField HeaderText="<%$Resources:L,调用标签 %>">
                <ItemStyle Width="20%" />
                <ItemTemplate>
                    {Pub.Load_<%#Eval("PubLoadstr")%>/}
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,操作 %>">
                <ItemStyle Width="15%" />
                <ItemTemplate>
                    <a href="pubinfo.aspx?menu=edit&id=<%#Eval("Pubid")%>" class="option_style"><i class="fa fa-pencil" title="<%=Resources.L.修改 %>"></i></a>
                    <a href="pubinfo.aspx?menu=Truedelete&id=<%#Eval("Pubid")%>" onclick="return confirm('确实要彻底删除吗？');" class="option_style"><i class="fa fa-trash" title="<%=Resources.L.彻底删除 %>"></i></a>
                    <a href="pubinfo.aspx?menu=revert&id=<%#Eval("Pubid")%>" class="option_style"><i class="fa fa-recycle" title="<%=Resources.L.还原 %>"></i><%=Resources.L.还原 %></a>
                    <a href="Pubsinfo.aspx?Pubid=<%#Eval("Pubid") %>&type=0" class="option_style"><i class="fa fa-magic" title="<%=Resources.L.管理 %>"></i><%=Resources.L.管理信息 %></a>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
        </div>
        <div class="panel panel-footer" style="padding:3px; margin:0px;">
            <asp:Button runat="server" ID="Clear_Btn" Text="<%$Resources:L,批量删除 %>" OnClick="Clear_Btn_Click" CssClass="btn btn-primary" OnClientClick="return confirm('确定删除选中项?'); "/>
            <asp:Button runat="server" ID="Recyle_Btn" Text="<%$Resources:L,批量还原 %>" OnClick="Recyle_Btn_Click" CssClass="btn btn-primary"  />
            <asp:Button runat="server" ID="DelAll_Btn" Text="<%$Resources:L,清空文档 %>" OnClick="DelAll_Btn_Click" CssClass="btn btn-primary" OnClientClick="return confirm('确定清空文档?'); "/>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        function getinfo(id) {
            location.href = "pubinfo.aspx?menu=edit&id=" + id + "";
        }
        //$().ready(function () {
        //    $("#Egv th:eq(0)").html("<input type='checkbox' id='chkAll' onclick='selectAllByName(this,\"idchk\");'>");
        //});
       
    </script>
</asp:Content>
