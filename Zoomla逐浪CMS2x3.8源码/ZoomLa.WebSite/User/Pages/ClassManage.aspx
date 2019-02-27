<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="ClassManage.aspx.cs" Inherits="User_Pages_ClassManage" ClientIDMode="Static" ValidateRequest="false" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>栏目管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="page" data-ban="page"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href='Default.aspx'>企业黄页</a></li>
        <li class="active">栏目管理<a href="Addnode.aspx">[添加栏目]</a></li>
	</ol>
</div>
    <div class="container btn_green">
        <table class="table table-bordered table-hover table-striped">
            <tr>
                <td colspan="5" class="text-center">黄页栏目管理
                </td>
            </tr>
            <tr>
                <td></td>
                <td width="10%"><b>栏目ID</b></td>
                <td width="30%"><b>栏目名称</b></td>
                <td width="30%"><b>显示状态</b></td>
                <td width="30%"><b>相关操作</b></td>
            </tr>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <tr>
                        <td class="td_s"><input type="checkbox" name="idchk" value="<%#Eval("TemplateID") %>" /></td>
                        <td width="10%"><%#Eval("TemplateID") %></td>
                        <td width="30%"><%#Eval("TemplateName") %></td>
                        <td width="30%"><%#Eval("IsTrue","{0}")=="1"?"显示":"隐藏" %></td>
                        <td width="30%"><%#(Eval("userid","{0}")!="0")?"<a href=\"addnode.aspx?menu=edit&id="+Eval("TemplateID")+"\">修改</a> <a href=\"?menu=delete&id="+Eval("TemplateID")+"\" onclick=\"return confirm('确实要删除吗？');\">删除</a>":"<font color=#999999>修改 删除</font>"%></td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td>
                    <input type="checkbox" id="CheckAll" />
                </td>
                <td class="text-center" colspan="4">
                    <asp:Button ID="BtnSubmit" runat="server" Text="批量隐藏" OnClick="BtnSubmit_Click" class="btn btn-primary" />
                    <asp:Button ID="BtnCancle" runat="server" OnClientClick="return confirm('是否删除!')" Text="批量删除" OnClick="BtnCancle_Click" class="btn btn-primary" />
                </td>
            </tr>
        </table>
    </div>
    <script>
        $(function () {
            $("#CheckAll").click(function () {
                var allcheck = $(this)[0];
                $("[name='idchk']").each(function () {
                    $(this)[0].checked = allcheck.checked;
                });
            });
        })
    </script>
</asp:Content>
