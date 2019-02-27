<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Questions_Knowledge_Show.aspx.cs" Inherits="manage_Question_Questions_Knowledge_Show" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>知识点管理</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HyperLink ID="hlAdd" runat="server" Visible="false"></asp:HyperLink>
    <div class="clearbox">
    </div>
    <div class="divbox" id="nocontent" runat="server" style="display: none">
        暂无知识点信息</div>
    <div class="clearbox">
    </div>
    <div>
        <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" AllowSorting="false"  AllowPaging="true" CssClass="table table-striped table-bordered table-hover"  EnableTheming="false"
            GridLines="None" OnRowCommand="gvCard_RowCommand"  OnPageIndexChanging="EGV_PageIndexChanging" 
            class="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!"
            IsHoldState="false" PageSize="10"   >
            <Columns>
                <asp:BoundField DataField="TestPoint" HeaderText="知识点" />
                <asp:BoundField DataField="OrderBy" HeaderText="排序" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbUpdate" runat="server" CommandName="Upd" CommandArgument='<%#Eval("ID") %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton>
                        <asp:LinkButton ID="lbDetele" runat="server" CommandName="Del" CommandArgument='<%#Eval("ID") %>'
                            OnClientClick="if(confirm('确定删除?')) return true; else return false;" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    </div>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
</asp:Content>
    
