<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master"  AutoEventWireup="true" CodeBehind="AuthorManage.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.AuthorManage" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>添加来源</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" Runat="Server">
    <div class="btn-group marginbot10">
<a href="AddSource.aspx" class="btn btn-primary" >添加来源</a>
<a href="Author.aspx"  class="btn btn-primary" >添加作者</a>
<a href="AddKeyWord.aspx"  class="btn btn-primary" >添加关键字</a>
    </div>
    <div class="clearfix"></div>
    <div class="panel panel-default" style="padding:0px;">
        <div class="panel panel-body" style="padding:0px; margin:0px;">
            <ZL:ExGridView RowStyle-HorizontalAlign="Center"  CssClass="table table-striped table-bordered table-hover" ID="EGV" DataKeyNames="ID" runat="server" 
                AutoGenerateColumns="False" AllowPaging="True" PageSize="10" Width="100%" EnableTheming="False" GridLines="None" CellPadding="2" CellSpacing="1" 
                OnPageIndexChanging="GridView1_PageIndexChanging" EmptyDataText="当前没有信息">
        <Columns>
            <asp:TemplateField HeaderText="选择">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" title="" value='<%#Eval("ID") %>' />
                </ItemTemplate>
                <ItemStyle CssClass="tdbg" />
            </asp:TemplateField>
            <asp:BoundField DataField="ID" HeaderText="序号">
                <ItemStyle CssClass="authid" />
                </asp:BoundField>                  
            <asp:TemplateField HeaderText="作者名称">
                <ItemTemplate>
                    <a href="Author.aspx?Action=Modify&AUId=<%#Eval("ID")%>">
                        <%# DataBinder.Eval(Container.DataItem,"Name")%>
                    </a>
                </ItemTemplate>
                    <ItemStyle  CssClass="tdbg" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="作者分类">
            <HeaderStyle/>
                <ItemTemplate>
                    <%#DataBinder.Eval(Container.DataItem, "Type")%>
                </ItemTemplate>
                    <ItemStyle CssClass="tdbg" />
            </asp:TemplateField>                     
            <asp:TemplateField HeaderText="操作">
            <HeaderStyle Width="19%" />
            <ItemTemplate>
                <a href='Author.aspx?Action=Modify&AUId=<%# Eval("ID")%>' class="option_style" ><i class="fa fa-pencil" title="修改"></i></a>
                <a href="javascript:if(confirm('你确定要删除吗?')) window.location.href='AuthorManage.aspx?AUId=<%# Eval("ID")%>';" class="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</a> 
            </ItemTemplate>
                <ItemStyle CssClass="tdbg" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
    </ZL:ExGridView>
        </div>
        <div class="panel panel-footer" style="padding:5px; margin:0px;">
                            <asp:Button ID="btndelete" class="btn btn-primary" style="width:110px;"  runat="server" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要将所有选择项删除吗？')}" Text="删除选定作者" OnClick="btndelete_Click" />
                <input name="Cancel" type="button" style="width:127px;"  class="btn btn-primary" id="Cancel" value="添加一个新作者" onclick="javascript: window.location.href = 'Author.aspx'" />
        </div>
    </div>
    <div class="clearfix"></div>                   
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
            $("#EGV tr").dblclick(function () {
                var id = $(this).find(".authid").text();
                if (id) {
                    location = "Author.aspx?Action=Modify&AUId=" + id;
                }
            });
        });
    </script>
    <style>
        th{ text-align:center;}
    </style>        
</asp:Content>
