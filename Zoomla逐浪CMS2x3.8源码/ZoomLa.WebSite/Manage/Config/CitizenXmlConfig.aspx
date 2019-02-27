<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CitizenXmlConfig.aspx.cs" Inherits="manage_Config_CitizenXmlConfig" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>国籍数据字典管理</title>
<script src="/JS/Controls/Control.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Label ID="lblCateName" runat="server" Text=""></asp:Label>
    <asp:Label ID="LblGradeLevel" runat="server" Text=""></asp:Label>
    <ZL:ExGridView ID="EGV" RowStyle-HorizontalAlign="Center" runat="server" DataKeyNames="FileName" AutoGenerateColumns="False" 
        AllowPaging="True" PageSize="10" OnRowDataBound="Gdv_RowDataBound" CssClass="table table-striped table-bordered table-hover" 
        OnPageIndexChanging="Egv_PageIndexChanging" OnRowCommand="Lnk_Click" EmptyDataText="无相关数据">
        <Columns>
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate><input type="checkbox" name="idchk" value="<%#Eval("FileName") %>" /></ItemTemplate>
                <ItemStyle CssClass="td_m" HorizontalAlign="Left" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="选项">
                <ItemTemplate><%#Eval("FileName") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit1" CommandArgument='<%# Eval("FileName") %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i>修改</asp:LinkButton> 
                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="DicList" CommandArgument='<%# Eval("FileName") %>' CssClass="option_style"><i class="fa fa-list-alt" title="列表"></i>下级选项列表</asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Width="60%"></ItemStyle>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
<table class="table table-striped table-bordered table-hover">
<tr>
<td>
        <asp:Button runat="server" ID="BatDel_Btn" OnClientClick="return confirm('确定要删除吗');" OnClick="BatDel_Btn_Click" Text="批量删除" CssClass="btn btn-primary" />
</td>
</tr>
</table>
    <div class="clearfix"></div>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td style="width: 10%; text-align: right">所属分类： </td>
            <td>
                <asp:Label ID="LblCate" runat="server" Text="国省市三级联动"></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 10%; text-align: right">级别： </td>
            <td>
                <asp:Label ID="LblLevel" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 10%; text-align: right">上级选项： </td>
            <td>
                <asp:Label ID="LblPreGrade" runat="server" Text=""></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 10%; text-align: right">分级选项： </td>
            <td>
                <asp:TextBox ID="txtGradeName" runat="server" CssClass="form-control text_300" data-enter="1"></asp:TextBox></td>
        </tr>
        <tr>             
            <td colspan="2">
                <asp:HiddenField ID="HdnFileName" Value="0" runat="server" />
                <asp:HiddenField ID="HdnCountry" Value="0" runat="server" />
                <asp:Button ID="btnSave" runat="server" Text="添加" OnClick="btnSave_Click" class="btn btn-primary" data-enter="2"/>
                <asp:Button ID="Button1" runat="server" Text="返回" OnClick="btnBack_Click" class="btn btn-primary" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>Control.EnableEnter();</script>
</asp:Content>