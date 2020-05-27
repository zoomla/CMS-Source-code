<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="ModelManage.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.ModelManage" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
<title><%=Resources.L.模型管理 %></title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" Runat="Server">
    <div class="text-center"></div> 
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" DataKeyNames="ModelID" PageSize="20" OnRowDataBound="Egv_RowDataBound" 
        OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" 
        AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" 
        EnableTheming="False" EnableModelValidation="True" EmptyDataText="<%$Resources:L,暂无模型信息 %>">
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:L,操作 %>">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <div class="option_area dropdown" >
                    <a class="option_style" href="javascript:;" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"><i class="fa fa-bars"></i><%=Resources.L.操作 %><span class="caret"></span></a>
                    <ul class="dropdown-menu" role="menu"> 
                    <li><asp:LinkButton runat="server" CssClass="option_style" CommandName="Field" CommandArgument='<%# Eval("ModelID") %>'><i class="fa fa-align-justify"></i><%=Resources.L.字段 %></asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" CssClass="option_style" CommandName="Edit1" CommandArgument='<%# Eval("ModelID") %>'><i class="fa fa-pencil" title="<%=Resources.L.编辑 %>"></i><%=Resources.L.编辑 %></asp:LinkButton></li>
                    <li><asp:LinkButton runat="server" CssClass="option_style" CommandName="Copy" CommandArgument='<%# Eval("ModelID")%>' Enabled='<%#GetEnabled(Eval("SysModel").ToString()) %>'><i class="fa fa-paste" title="<%=Resources.L.复制 %>"></i><%=Resources.L.复制 %></asp:LinkButton></li>  
                    <li><asp:LinkButton runat="server" CssClass="option_style" CommandName="Del2" CommandArgument='<%# Eval("ModelID") %>' OnClientClick="return confirm('确实要删除此模型吗？');"><i class="fa fa-trash-o"></i><%=Resources.L.删除 %></asp:LinkButton></li>                      
                    </ul>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ID">
                <HeaderStyle Width="5%" />
                <ItemTemplate>
                    <strong><%# Eval("ModelID") %></strong>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,模型名称 %>">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <%#GetModelIcon() %>
                    <strong><%# Eval("ModelName")%></strong>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,表名 %>">
                <HeaderStyle Width="15%" />
                <ItemTemplate>
                    <strong><%# Eval("TableName")%></strong>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,项目名称 %>">
                <HeaderStyle Width="10%" />
                <ItemTemplate>
                    <strong><%# Eval("ItemName")%></strong>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="<%$Resources:L,模型描述 %>" DataField="Description" HeaderStyle-Width="30%" />           
        </Columns>
        
    </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .allchk_l {display:none;}
    </style>
<script type="text/javascript">
	function getinfo(id) {
		location.href = "AddEditModel.aspx?ModelID=" + id + '&ModelType=<%=ModelType %>';
	}
	HideColumn("1,4,5");
</script>
</asp:Content>