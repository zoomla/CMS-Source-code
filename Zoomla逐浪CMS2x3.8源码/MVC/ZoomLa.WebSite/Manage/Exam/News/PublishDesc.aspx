<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PublishDesc.aspx.cs" Inherits="ZoomLaCMS.Manage.Exam.News.PublishDesc" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False" 
                CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
                OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="ID"><ItemStyle Width="60px" /></asp:BoundField>
                    <asp:TemplateField HeaderText="报纸名">
                        <ItemTemplate>
                          <a href="/Rss/ViewPublish?Pid=<%#Eval("ID") %>" target="_blank" title="前台浏览"><%#Eval("NewsName") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="版面数">
                        <ItemTemplate>
                            <a href="NewsDiv.aspx?Pid=<%#Eval("ID") %>"><%#Eval("TitleNum") %></a>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Title" HeaderText="备注" />
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <a href="/Rss/ViewPublish?Pid=<%#Eval("ID") %>" target="_blank" title="前台浏览" class="option_style"><i class="fa fa-globe" title="浏览"></i></a><span class="sperspan"></span>
                            <asp:LinkButton runat="server" CommandName="Edit2" CommandArgument='<%#Eval("ID") %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i></asp:LinkButton><span class="sperspan"></span>
                            <asp:LinkButton runat="server" CommandName="Del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('确定要删除?');" ToolTip="删除" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i></asp:LinkButton>
                            <a href="NewsDiv.aspx?Pid=<%#Eval("ID") %>" title="版面管理" class="option_style"><i class="fa fa-magic" title="管理"></i>版面管理</a><span class="sperspan"></span>
                            <asp:Button runat="server" data-id="EditBtn" CommandName="Edit2" CommandArgument='<%#Eval("ID") %>' style="display:none;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
         <PagerStyle HorizontalAlign="Center" />
         <RowStyle Height="24px" HorizontalAlign="Center" />
     </ZL:ExGridView>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        $().ready(function () {
            $("#EGV tr").dblclick(function () {
                $(this).find("[data-id=EditBtn]").click();
            });
        });
        function ShowParent(id, title) {
            $obj = $(parent.document);
            $obj.find("#CurID_Hid").val(id);
            $obj.find("#NewsName_T").val(title);
            parent.ShowModal();
        }
    </script>
</asp:Content>
