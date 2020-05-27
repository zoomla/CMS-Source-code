<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="ManageJsContent.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.ManageJsContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title><%=Resources.L.文章JS文件管理 %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <div class="opion_header">
        <%=Resources.L.管理导航 %>:
    <div class="btn-group">
        <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown"><%=Resources.L.js文件管理 %><span class="caret"></span></button>
        <ul class="dropdown-menu" role="menu">
            <li><a href="AddJsContent.aspx"><i class="fa fa-plus"></i><%=Resources.L.添加JS文件_普通列表 %></a></li>
            <li><a href="AddJsPic.aspx"><i class="fa fa-plus"></i><%=Resources.L.添加JS文件_图片列表 %></a></li>
            <li></li>
        </ul>
    </div>
        <div class="btn-group">
            <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown"><%=Resources.L.图片列表操作 %><span class="caret"></span></button>
            <ul class="dropdown-menu" role="menu">
                <li><a href="?menu=rsallimg"><i class="fa fa-refresh"></i><%=Resources.L.刷新图片列表 %></a></li>
                <li><a href="?menu=rsalllist"><i class="fa fa-refresh"></i><%=Resources.L.刷新普通列表 %></a></li>
                <li></li>
            </ul>
        </div>
    </div>
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" PageSize="10" IsHoldState="false" 
        OnPageIndexChanging="EGV_PageIndexChanging" AllowPaging="True" AllowSorting="True" OnRowCommand="EGV_RowCommand"
        CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True">
        <Columns>
           <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                   <%#Eval("id")%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="<%$Resources:L,简介 %>">
                <ItemTemplate>
                    <%#Eval("JsReadme")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,JS代码名称 %>">
                <ItemTemplate>
                  <a href="<%#(Eval("JsType","{0}")=="1")?"AddJsPic.aspx":"AddJsContent.aspx" %>?menu=edit&id=<%#Eval("id") %>"><%#Eval("Jsname")%></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,代码类型 %>">
                <ItemTemplate>
                    <%#(Eval("JsType","{0}") == "1") ? Resources.L.图片列表: Resources.L.普通列表%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="<%$Resources:L,JS文件名 %>">
                <ItemTemplate>
                    <asp:HiddenField ID="hfGID" runat="server" Value='<%#Eval("id")%>' /> 
                   <%#Eval("JsFileName")%>
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="<%$Resources:L,JS调用代码 %>">
                <ItemTemplate>
                     <textarea name='textarea' cols='36' rows='3' class="l_input" style="height: 40px"><%#Getscript(Eval("id","{0}")) %></textarea>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,操作 %>">
                <ItemTemplate>
                   <a href="<%#(Eval("JsType","{0}")=="1")?"AddJsPic.aspx":"AddJsContent.aspx" %>?menu=edit&id=<%#Eval("id") %>" ><i class="fa fa-gear" title="<%=Resources.L.设置 %>"></i><%=Resources.L.参数设置 %></a>
                   <asp:LinkButton runat="server" CssClass="option_style" CommandName="refresh" CommandArgument='<%#Eval("id") %>'><i class="fa fa-refresh" title="<%=Resources.L.刷新 %>"></i><%=Resources.L.刷新 %></asp:LinkButton>
                   <asp:LinkButton runat="server" CssClass="option_style" CommandName="delInfo" CommandArgument='<%#Eval("id") %>' OnClientClick="return confirm('真的要删除此JS文件吗？如果有文件或模板中使用此JS文件，请注意修改过来！');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView> 
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td>
                <b><%=Resources.L.说明 %>：</b>
                <br />
                <%=Resources.L.这些JS代码是为了加快访问速度特别生成的 %>
				<br />
                <font color="red"><%=Resources.L.表示此JS文件还没有生成 %></font>
                <br />
                <b><%=Resources.L.使用方法 %></b>
                <br />
                <%=Resources.L.将相关JS调用代码复制到页面或模板中的相关位置即可 %>
            </td>
        </tr>
    </table>
    <style>
        .table > tbody > tr > td { width:5%;}
        .table > tbody > tr > td:first-child{ width:1%;}
        .table > tbody > tr > td:nth-child(6){ width:1%;}
    </style>
    <script>
        function getinfo(id, types) {
            types == "1" ? location.href = 'AddJsPic.aspx?menu=edit&id=' + id : location.href = 'AddJsContent.aspx?menu=edit&id=' + id;
        }
        $("#AllID_Chk").hide();
        $(".allchk_sp").hide();
    </script>
</asp:Content>
