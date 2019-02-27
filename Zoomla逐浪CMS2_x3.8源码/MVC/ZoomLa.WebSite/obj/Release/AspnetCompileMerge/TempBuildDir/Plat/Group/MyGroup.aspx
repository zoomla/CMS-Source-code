<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyGroup.aspx.cs" Inherits="ZoomLaCMS.Plat.Group.MyGroup"  MasterPageFile="~/Plat/Main.master"%>
<asp:Content runat="server" ContentPlaceHolderID="Head">
    <title>我的部门</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="首页" href="/">首页</a></li>
        <li><a title="会员中心" href="/Plat/Blog/">能力中心</a></li>
        <li class="active">部门管理</li>
    </ol>
    <div class="child_head">
        <span class="child_head_span1"></span>
        <span class="child_head_span2">部门列表</span>
    </div>
    <asp:DropDownList runat="server" ID="Group_DP" style="margin:10px 0 10px 0">
        <asp:ListItem Value="1">所有部门</asp:ListItem>
        <asp:ListItem Value="2">我加入的</asp:ListItem>
        <asp:ListItem Value="3">我创建的</asp:ListItem>
        <asp:ListItem Value="4">我管理的</asp:ListItem>
    </asp:DropDownList>
    <div>
        <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EnableTheming="False" IsHoldState="false"
            class="table table-striped table-bordered table-hover" EmptyDataText="当前没有信息!!"
            OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                         <input type="checkbox" name="idChk" value="<%#Eval("ID") %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="部门名">
                    <ItemTemplate><a href="GroupDetail.aspx?ID=<%#Eval("ID") %>"><%#Eval("GroupName") %></a></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="成员人数">
                    <ItemTemplate>
                        <%#Eval("MemberIDS").ToString().Split(new char[]{','},StringSplitOptions.RemoveEmptyEntries).Length %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建人">
                    <ItemTemplate>
                        <%#Eval("UserName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="管理员">
                    <ItemTemplate>
                        <%#Eval("UserName") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="GroupDetail.aspx?ID=<%#Eval("ID") %>">详情</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerStyle HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
        </ZL:ExGridView>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .child_head {margin-top:20px;border-bottom:1px solid #7fB2E5;padding-bottom:5px;}
        .child_head_span1 {display:inline-block;background-color:#0066cc;width:3px;height:20px;margin-left:5px;}
        .child_head_span2 {font-size:18px;}
        .child_head_a {float:right;}
    </style>
    <script type="text/javascript">
        $("#top_nav_ul li[title='公司']").addClass("active");
    </script>
</asp:Content>
