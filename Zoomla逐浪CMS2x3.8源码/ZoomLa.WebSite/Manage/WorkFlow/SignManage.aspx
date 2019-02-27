<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SignManage.aspx.cs" Inherits="Manage_WorkFlow_SignManage" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>签章管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div style="margin-bottom: 10px;">
        <div class="input-group" style="width: 300px;">
            <asp:TextBox runat="server" ID="searchText" placeholder="请输入需要查询的信息" CssClass=" form-control" />
            <span class="input-group-btn">
                <asp:Button runat="server" CssClass="btn btn-primary" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click" />
            </span>
        </div>
    </div>
    <div class="tab3">
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" EnableTheming="False" GridLines="None" CellPadding="2" CellSpacing="1" Width="100%" CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" OnRowDataBound="EGV_RowDataBound" DataKeyNames="ID" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="ID" />
                <asp:BoundField HeaderText="签章名" DataField="SignName" />
                <asp:BoundField HeaderText="绑定用户" DataField="UserName" />
                <asp:TemplateField HeaderText="签章图">
                    <ItemTemplate>
                        <a onmouseout="showdiv(this)" onmouseover="showdiv(this)" class="lightbox" title="<%#Eval("SignName")%>">
                            <img src="<%#Eval("VPath") %>" title="<%#Eval("SignName")%>" alt="" style="width: 60px; height: 60px;" /></a>
                        <div class="imagediv" style="display: none; position: absolute;">
                            <img src="<%#Eval("VPath") %>" alt="" />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="签章密码">
                    <ItemTemplate>
                        <input type="button" class="btn btn-primary" value="查看" onclick="$(this).hide();$(this).siblings('span').show();" />
                        <span style="display:none;"><%#Eval("SignPwd") %></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="添加时间" DataField="CreateTime" />
                <asp:TemplateField HeaderText="状态">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status","{0}")) %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="AddSign.aspx?ID=<%#Eval("ID") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a> 
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Del2" CommandArgument='<%#Eval("ID") %>' OnClientClick="return confirm('你确定要删除吗!');" ToolTip="删除" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        function showdiv(obj)
        {
            imagediv = $(obj).parent().find(".imagediv").toggle();
        }
        $().ready(function () {
            $(":button").addClass("btn btn-primary");
            $(":submit").addClass("btn btn-primary");
            $("#EGV tr:last >td>:text").css("line-height", "normal");
            $("#EGV tr:first >th").css("text-align", "center");
        });
    </script>
</asp:Content>
