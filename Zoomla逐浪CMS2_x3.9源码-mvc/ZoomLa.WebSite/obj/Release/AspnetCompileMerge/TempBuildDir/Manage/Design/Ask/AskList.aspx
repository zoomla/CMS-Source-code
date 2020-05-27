<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AskList.aspx.cs" Inherits="ZoomLaCMS.Manage.Design.Ask.AskList" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>问卷管理</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href='Default.aspx'>动力模块</a></li>
    <li><a href='AskList.aspx'>问卷调查</a></li>
    <div id="help" class="pull-right text-center"><a href="javascript::" id="sel_btn" class="help_btn"><i class="fa fa-search"></i></a></div>
    <div id="sel_box" runat="server" class="padding5">
        <div style="display: inline-block; width: 230px;">
            <div class="input-group" style="position: relative; margin-bottom: -12px;">
                <asp:TextBox ID="Skey_T" placeholder="问卷标题" runat="server" CssClass="form-control text_md" />
                <span class="input-group-btn">
                    <asp:Button ID="Search_B" runat="server" Text="<%$Resources:L,搜索 %>" class="btn btn-primary" OnClick="Search_B_Click" />
                </span>
            </div>
        </div>
    </div>
</ol>
<div class="template margin_t5" id="template" runat="server">
    <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="false" OnRowDataBound="EGV_RowDataBound" PageSize="20" AllowPaging="true" CssClass="table table-striped table-bordered table-hover" OnRowCommand="EGV_RowCommand" EmptyDataText="没有数据">
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:TemplateField HeaderText="标题">
                <ItemTemplate>
                    <a href="EditAsk.aspx?id=<%#Eval("ID") %>"><%#Eval("Title") %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="创建用户">
                <ItemTemplate>
                    <a href="javascript:;" onclick="showuser(<%#Eval("CUser") %>)"><%#GetUserName() %></a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="类型">
                <ItemTemplate>
                    <%#GetZType() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%#GetStatus() %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="创建时间" DataField="CDate" DataFormatString="{0:yyyy-MM-dd}" />
            <asp:BoundField HeaderText="备注说明" DataField="Remind" />
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
             <%--       <a href="Add.aspx?id=<%#Eval("ID") %>" ><i class="fa fa-pencil"></i> 编辑</a>  --%>
                    <a href="/design/ask/default.aspx#/tab/ask_view/<%#Eval("ID") %>" target="_blank"><i class="fa fa-globe"></i> 浏览</a>
                    <a href="/design/ask/AskResult.aspx?id=<%#Eval("ID") %>" target="_blank"><i class="fa fa-list"></i> 结果</a>
                    <a href="QuestionList.aspx?askid=<%#Eval("ID") %>" title="查看问题" ><i class="fa fa-list"></i> 问题</a> 
                    <a href="anslist.aspx?askid=<%#Eval("ID") %>" title="查看回答" ><i class="fa fa-list-alt"></i> 答题</a> 
                    <asp:LinkButton runat="server" CommandArgument='<%#Eval("ID") %>' CommandName="del" OnClientClick="return confirm('确认删除？')" CssClass="option_style"><i class="fa fa-trash-o" title="<%=Resources.L.删除 %>"></i><%=Resources.L.删除 %></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </ZL:ExGridView>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script>
    function showuser(id) { ShowComDiag("../../User/Userinfo.aspx?id=" + id, "查看用户"); }
    $("#sel_btn").click(function (e) {
        if ($("#sel_box").css("display") == "none") {
            $(this).addClass("active");
            $("#sel_box").slideDown(300);
            $(".template").css("margin-top", "44px");
        }
        else {
            $(this).removeClass("active");
            $("#sel_box").slideUp(200);
            $(".template").css("margin-top", "0px");
        }
    });
</script>
</asp:Content>