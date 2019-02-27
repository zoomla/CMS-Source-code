<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="CorrectManage.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.CorrectManage" %>
<asp:Content ContentPlaceHolderID="head" runat="Server"><title>纠错报告</title></asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div style="margin-bottom: 10px;">
        <span><a href="AddSource.aspx" class="btn btn-primary">添加来源</a></span>&nbsp;&nbsp;<span><a href="Author.aspx" class="btn btn-primary">添加作者</a></span>&nbsp;&nbsp;<span><a href="AddKeyWord.aspx" class="btn btn-primary">添加关键字</a></span>
    </div>
    <!--end navigation-->
    <div class="clearfix"></div>
    <div class="panel panel-default" style="padding: 0px;">
        <div class="panel panel-body" style="padding: 0px; margin: 0px;">
            <ZL:ExGridView ID="Egv" RowStyle-HorizontalAlign="Center" runat="server" AutoGenerateColumns="False" DataKeyNames="CorrectID" PageSize="10"
                OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" OnRowCommand="Lnk_Click"
                CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据">
                <Columns>
                    <asp:TemplateField HeaderText="选中">
                        <ItemTemplate>
                            <input type="checkbox" name="idchk" title="" value='<%#Eval("CorrectID") %>' />
                        </ItemTemplate>
                        <HeaderStyle Width="5%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CorrectID" HeaderText="ID">
                        <HeaderStyle Width="5%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="网页标题">
                        <HeaderStyle Width="20%" />
                        <ItemTemplate>
                            <a href="<%#Eval("CorrectUrl")%>" title="点击查看出错页" target="_parent"><%#Eval("CorrectTitle")%></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CorrectDetail" HeaderText="详细信息">
                        <HeaderStyle Width="30%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="错误类型">
                        <HeaderStyle Width="10%" />
                        <ItemTemplate>
                            <%# GetCTitle(Eval("CorrectType", "{0}"))%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CorrectPer" HeaderText="报告人">
                        <HeaderStyle Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PerEmail" HeaderText="报告人Email">
                        <HeaderStyle Width="10%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Del" CommandArgument='<%# Eval("CorrectID") %>' OnClientClick="return confirm('你确定将该数据删除吗？')"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" />
            </ZL:ExGridView>
        </div>
        <div class="panel panel-footer" style="padding: 5px; margin: 0px;">
            <asp:Button ID="btnDeleteAll" Style="width: 110px;" class="btn btn-primary" runat="server" Text="批量删除" OnClick="btnDeleteAll_Click"
                OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项删除吗？')}" UseSubmitBehavior="true" />
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {

        });
    </script>
</asp:Content>

