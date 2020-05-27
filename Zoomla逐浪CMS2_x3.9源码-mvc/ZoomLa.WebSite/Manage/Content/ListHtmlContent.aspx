<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="ListHtmlContent.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.ListHtmlContent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <title><%=Resources.L.内容生成管理 %></title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="Server">
    <table class="table table-bordered">
        <tr>
            <td>
                <div class="pull-left">
                    <b><%=Resources.L.请选择排列顺序 %>：</b>
                    <asp:DropDownList ID="txtbyfilde" CssClass="form-control" Width="150" runat="server" OnSelectedIndexChanged="SelectBind"></asp:DropDownList>
                    <asp:DropDownList ID="txtbyOrder" CssClass="form-control" Width="150" runat="server" OnSelectedIndexChanged="SelectBind"></asp:DropDownList>
                </div>
                <div class="pull-left">
                    <div class="pull-left">
                        <b style="margin-left: 10px;"><%=Resources.L.查找 %>：</b>
                        <asp:DropDownList ID="DropDownList1" Width="120" CssClass="form-control" runat="server">
                            <asp:ListItem Value="" Selected="True" Text="<%$Resources:L,请选择 %>"></asp:ListItem>
                            <asp:ListItem Value="2" Text="<%$Resources:L,ID检索 %>"></asp:ListItem>
                            <asp:ListItem Value="1" Text="<%$Resources:L,标题检索 %>"></asp:ListItem>
                            <asp:ListItem Value="3" Text="<%$Resources:L,录入者检索 %>"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="input-group pull-left" style="width:300px;">
                        <asp:TextBox ID="TextBox1" runat="server" onkeydown="return GetEnterCode('click','Button4');" CssClass="form-control"></asp:TextBox>
                        <span class="input-group-btn">
                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="Button4_Click" CssClass="btn btn-primary"><span class="fa fa-search"></span></asp:LinkButton>
                        </span>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" style="display:none;" CssClass="btn btn-primary" /> 
    <div class="panel panel-default" style="padding:0px;">
        <div class="panel panel-body" style="padding:0px; margin:0px;">
            <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="GeneralID" PageSize="20" OnRowDataBound="Egv_RowDataBound" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="<%$Resources:L,暂无相关信息 %>">
        <Columns>
            <asp:TemplateField HeaderText="<%$Resources:L,选择 %>">
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value='<%#Eval("GeneralID") %>' />
                </ItemTemplate>
                <HeaderStyle Width="5%" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="GeneralID">
                <HeaderStyle Width="5%" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <a href="ListHtmlContent.aspx?orderby=title"><font color="green"><b><%=Resources.L.标题 %></b></font></a>
                </HeaderTemplate>
                <ItemTemplate>
                   <%# GetTitle(Eval("GeneralID", "{0}"), Eval("NodeID", "{0}"), Eval("Title", "{0}"), Eval("ModelID", "{0}"))%>
                </ItemTemplate>
                <HeaderStyle Width="55%" />
                <ItemStyle />
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>
                    <a href="ListHtmlContent.aspx?orderby=Status"><font color="green"><b><%=Resources.L.状态 %></b></font></a>
                </HeaderTemplate>
                <ItemTemplate>
                    <%# GetStatus(Eval("Status", "{0}")) %>
                </ItemTemplate>
                <HeaderStyle Width="10%" />
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="<%$Resources:L,生成操作 %>">
                <ItemTemplate>
                    <%# GetCteate(Eval("IsCreate", "{0}"))%>
                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="MakeHtml" CommandArgument='<%# Eval("GeneralID") %>' CausesValidation="false" CssClass="option_style"><i class="fa fa-spinner" title="<%=Resources.L.生成 %>"></i><%=Resources.L.生成文件 %></asp:LinkButton>
                    <a href="<%#(geturl(Eval("GeneralID","{0}"))=="")?"javascript:void(0);":geturl(Eval("GeneralID","{0}"))%>" class="option_style"><i class="fa fa-eye" title="预览" ></i><%=Resources.L.查看文件 %></a>
				<a href="ListHtmlContent.aspx?GeneralID=<%# Eval("GeneralID") %>" class="option_style"><i class="fa fa-trash-o" title="<%=Resources.L.删除 %>"></i><%=Resources.L.删除 %></a>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" /> 
    </ZL:ExGridView>
        </div>
        <div class="panel panel-footer" style="padding: 3px; margin: 0px;">
            <asp:Button ID="btnUnAudit" CausesValidation="false" runat="server" Text="<%$Resources:L,批量删除文件 %>" CssClass="btn btn-primary" OnClientClick="if(!IsSelectedId()){alert('请选择删除项');return false;}else{return confirm('你确定要取消审核选中内容吗？')}" OnClick="btnUnAudit_Click" />&nbsp;
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script>
        $().ready(function () {
           
        })
        function IsSelectedId() {
            var checkArr = $("input[type=checkbox][name=idchk]:checked");
            if (checkArr.length > 0)
                return true
            else
                return false;
        }
        function displayToolbar() {
            var dr = document.getElementById("search");
            if (dr.style.display == "none") {
                dr.style.display = "block";
            }
            else {
                dr.style.display = "none";
            }
        }
    </script>
</asp:Content>
