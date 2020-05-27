<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RoleAuthList.aspx.cs" Inherits="ZoomLaCMS.Manage.User.RoleAuthList"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>角色权限设置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li>后台管理</li>
    <li><a href="AdminManage.aspx">管理员管理</a></li>
    <li><a href="RoleManage.aspx">角色管理</a></li>
    <li class="active">权限管理[<asp:Label runat="server" ID="RoleName_L"></asp:Label>]</li>
</ol>
    <ul class="nav nav-tabs">
        <li class="active"><a href="all" data-toggle="tab">全部</a></li>
        <asp:Repeater runat="server" ID="TabRPT" EnableViewState="false">
            <ItemTemplate>
                <li><a href="<%#Eval("name") %>" data-toggle="tab"><%#Eval("text") %></a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
<table class="table table-hover table-bordered table-striped table-condensed">
    <tr><td class="td_s">选择</td><td>名称</td><td>说明</td></tr>
    <asp:Repeater runat="server" ID="ownerRPT" EnableViewState="false" OnItemDataBound="ownerRPT_ItemDataBound">
        <ItemTemplate>
            <tbody id="<%#Eval("name") %>" class="authbody">
                <tr>
                    <td><label class="filltd"><input type="checkbox" class="chkbody" data-for="<%#Eval("name") %>" /></label></td>
                    <td colspan ="2"><i class="fa fa-list"></i><%#Eval("text") %></span></td>
                </tr>
                <asp:Repeater runat="server" ID="AuthRPT" EnableViewState="false">
                    <ItemTemplate>
                        <tr class="childtr">
                            <td>
                                <label class="filltd"><input type="checkbox" name="<%#Eval("owner") %>" value="<%#Eval("name") %>" /></label></td>
                            <td>
                                <img src="/Images/TreeLineImages/tree_line4.gif" />
                                <img src="/Images/TreeLineImages/t.gif" />
                                <i class="fa fa-bookmark"></i><%#Eval("text") %></td>
                            <td>页面</td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </ItemTemplate>
    </asp:Repeater>
</table>
    <div class="Conent_fix">
        <asp:Button runat="server" ID="Save_Btn" OnClick="Save_Btn_Click" Text="保存" CssClass="btn btn-primary" />
        <a href="RoleManage.aspx" class="btn btn-primary">返回</a>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .filltd {display:block;height:100%;}
    </style>
    <script src="/JS/SelectCheckBox.js"></script>
    <script>
        //给行加上状态,点击为选中
        $(function () {
            //全选下面的子级
            $(".chkbody").click(function () {
                var name = $(this).data("for");
                var checked=this.checked;
                $("input[name=" + name + "]").each(function () { this.checked = checked; });
            });
            //Tabs切换
            $("[data-toggle]").click(function () {
                var href = $(this).attr("href");
                switch (href)
                {
                    case "all":
                        $(".authbody").show();
                        break;
                    default:
                        $(".authbody").hide();
                        $("#" + href).show();
                        break;
                }
            });
        })
    </script>
</asp:Content>