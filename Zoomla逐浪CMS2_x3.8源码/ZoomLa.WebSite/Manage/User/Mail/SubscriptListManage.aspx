<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubscriptListManage.aspx.cs" Inherits="manage_User_SubscriptListManage" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>邮件订阅</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ul class="nav nav-tabs" role="tablist">
    <li id="tab-10" role="presentation" class="active"><a href="SubscriptlistManage.aspx" role="tab">全部</a></li>
    <li id="tab1" role="presentation"><a href="SubscriptlistManage.aspx?audit=1"  role="tab">正常</a></li>
    <li id="tab-1" role="presentation"><a href="SubscriptlistManage.aspx?audit=-1" role="tab">退订</a></li>
    <li id="tab0" role="presentation"><a href="SubscriptlistManage.aspx?audit=0" role="tab">停用</a></li>
</ul>
<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="20" OnRowCommand="EGV_RowCommand"  EnableTheming="False"  
    CssClass="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!" 
    OnPageIndexChanging="EGV_PageIndexChanging"> 
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
            </ItemTemplate>
            <ItemStyle CssClass="td_s" />
        </asp:TemplateField>
        <asp:BoundField DataField="EMail" HeaderText="EMail" />
        <asp:TemplateField HeaderText="用户名">
            <ItemTemplate>
                <%#GetUserName() %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Source" HeaderText="来源网站" />
        <asp:BoundField DataField="browser" HeaderText="浏览器" />
        <asp:BoundField DataField="IP" HeaderText="来源IP" />
        <asp:TemplateField HeaderText="状态">
            <ItemTemplate>
                <%#GetStatus() %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="操作">
            <ItemTemplate>
                <asp:LinkButton runat="server" CommandName="Del" OnClientClick="return confirm('是否确定删除!')" CommandArgument='<%#Eval("ID") %>'><i class="fa fa-trash"></i>删除</asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</ZL:ExGridView>
<asp:Button ID="Dels_Btn" runat="server" CssClass="btn btn-primary" Text="批量删除" OnClick="Dels_Btn_Click" OnClientClick="return confirm('是否确定删除选定项!')" />
<div class="alert alert-info margin_t5">
             1,在需要订阅邮件功能的页面上引用此(<%:"<script src='/JS/ICMS/ZL_Subscript.js'></script>" %>)代码<br />
             2,用户在填完邮箱并点击订阅后会向用户发送确认邮件，未确认订阅邮件的状态为"停用",已确认订阅邮件的状态为"正常"
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        $(function () {
            if (window.top != window.self) {//选择模式

            }
        });
        function ShowTab(id) {
            $(".nav-tabs li").removeClass('active');
            $("#tab" + id).addClass('active');
        }
    </script>
</asp:Content>
