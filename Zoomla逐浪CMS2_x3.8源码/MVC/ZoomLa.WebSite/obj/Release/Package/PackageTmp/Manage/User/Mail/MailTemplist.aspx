<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MailTemplist.aspx.cs" Inherits="ZoomLaCMS.Manage.User.Mail.MailTemplist" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>邮件模板</title>
</asp:Content>

<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ZL:ExGridView ID="EGV" runat="server" 
        AllowPaging="true" OnPageIndexChanging="EGV_PageIndexChanging" AutoGenerateColumns="False" 
        OnRowCommand="Row_Command" class="table table-striped table-bordered table-hover" onrowdatabound="EGV_RowDataBound">
        <Columns>
            <asp:TemplateField>
                <ItemStyle CssClass="td_xs" />
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="模板名称">
                <ItemTemplate><a href="AddMailTemp.aspx?id=<%#Eval("ID")%>" title='<%# Eval("TempName")%>'><%# Eval("TempName")%></a> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="缩略图">
                <ItemTemplate>
                    <img src="<%# Eval("Pic")%>" alt="缩略图" width="50" height="50" onerror="shownopic(this);" />
                </ItemTemplate>
                <ItemStyle Width="10%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="模板类型">
                <ItemTemplate><a href="MailTemplist.aspx?type=<%#Eval("Type")%>" title='<%# Eval("TempName")%>'><%# GetType(Convert.ToInt32(Eval("Type")))%> </a></ItemTemplate>
                <ItemStyle Width="8%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建人">
                <ItemTemplate><%# Eval("AddUser")%> </ItemTemplate>
                <ItemStyle  Width="8%" HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建日期">
                <ItemTemplate><%# Eval("CreateTime", "{0:yyyy-MM-dd}")%> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                   <%-- <a href="ViewMailTemp.aspx?id=<%#Eval("ID") %>" class="option_style"><i class="fa fa-eye" title="预览"></i></a>--%>
                    <a href="AddMailTemp.aspx?id=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton ID="btnDel" runat="server" CommandName="DeleteMsg" OnClientClick="if(!this.disabled) return confirm('确实删除此模板?');" CommandArgument='<%# Eval("ID")%>' CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
    <input type="button" id="submit" value="批量删除" class="btn btn-primary" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        $().ready(function () {
            
        });
        function SelecteAllByName(obj,name) {
            var input = document.getElementsByName(name);
            var len = input.length;
            for (var i = 0; i < len; i++) {
                if(input[i].type=="checkbox"){
                    input[i].checked = obj.checked;
                }
            }
        };

    </script>

</asp:Content>
