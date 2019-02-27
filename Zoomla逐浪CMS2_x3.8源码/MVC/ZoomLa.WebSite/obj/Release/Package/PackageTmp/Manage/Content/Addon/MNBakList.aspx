<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MNBakList.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Addon.MNBakList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>节点备份</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="BreadDiv" class="container-fluid mysite">
        <div class="row">
            <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
                <li><a href="/Admin/Main.aspx">工作台</a></li>
                <li><a href="/Admin/Config/DatalistProfile.aspx">扩展功能</a></li>
                <li><a href="/Admin/Config/RunSql.aspx">开发中心</a></li>
                <li><a href="/test/mnbaklist.aspx">备份列表</a>
                    <asp:LinkButton runat="server" ID="CreateBak_Btn" Style="margin-left: 10px;" OnClientClick="return confirm('确定要创建当前备份吗?');" OnClick="CreateBak_Btn_Click">[创建备份]</asp:LinkButton></li>
            </ol>
        </div>
    </div>
    <div style="height: 20px;"></div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" PageSize="10" IsHoldState="false"
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="没有备份数据">
        <Columns>
            <%--  <asp:TemplateField>
                <ItemTemplate>
              <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="文件名称">
                <ItemTemplate>
                    <%#Eval("Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="文件类型">
                <ItemTemplate>
                    <%#System.IO.Path.GetExtension(Eval("Name","")).ToLower().Equals(".config")?"备份文件":Eval("Type") %>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="文件大小">
                <ItemTemplate>
                    <%#GetFileSize() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="创建时间" DataField="CreateTime" DataFormatString="{0:yyyy/MM/dd HH:mm}" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("name") %>' CommandName="download" CssClass="option_style" ToolTip="下载"><i class="fa fa-cloud-download"> </i></asp:LinkButton>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("name") %>' CommandName="restore" CssClass="option_style" OnClientClick="return confirm('[确定恢复该备份吗?]改为[系统将还原所有节点模型数据(此操作不可逆),是否继续?] 恢复前清空数据');"><i class="fa fa-recycle"> 数据恢复</i></asp:LinkButton>
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("name") %>' CommandName="del2" CssClass="option_style" OnClientClick="return confirm('确定要删除该备份吗?');"><i class="fa  fa-trash-o"> 删除备份</i></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <div class="alert alert-info" style="margin-bottom:3px;">
        <ul>
            <li>元数据是指与当前模板方案对应的节点、模型、模板设置、会员管理员权限、字段设置的相关信息</li>
            <li>请检查对应方案目录(Template/目录名/）下是否有system文件夹及相关文件,格式为：[Template/{您的目录名}/system]</li>
        </ul>
    </div>
    <div class="alert alert-danger">
        <ul>
            <li>元数据还原后,系统的[ZL_Node,ZL_Node_ModelTemplate,ZL_Model,ZL_ModelField,ZL_UserPromotions,ZL_NodeRole,ZL_Pub]中的数据会进行还原</li>
            <li>[ZL_CommonModel]和所有附加表[ZL_C_**]中的数据将会清空,我们强烈推荐执行此操作前,先<a href="../../Config/BackupRestore.aspx">[备份系统数据库]</a></li>
        </ul>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">

</asp:Content>
