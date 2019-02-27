<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Addnode.aspx.cs" Inherits="User_Pages_addnode" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>添加栏目</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="page" data-ban="page"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="Default.aspx">企业黄页</a></li>
        <li><a href="ClassManage.aspx">栏目管理</a></li>
        <li class="active">添加栏目</li> 
    </ol>

    <asp:HiddenField ID="tid" runat="server" />
    </div>
    <div class="container btn_green">
        <div>
            <table class="table table-bordered table-hover table-striped">
                <tr>
                    <td colspan="2" class="text-center">
                        <asp:Literal ID="nodetxt" runat="server" Text="添加栏目"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td>栏目名称： </td>
                    <td>
                        <asp:TextBox ID="Title" runat="server" class="form-control text_md" Width="430px" /></td>
                </tr>
                <tr>
                    <td>栏目排序： </td>
                    <td>
                        <asp:TextBox ID="NodeOrder" runat="server" class="form-control text_md" Width="72px" Text="0"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>显示状态： </td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="显示" Value="1" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="隐藏" Value="0"></asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr>
                    <td>栏目捆绑： </td>
                    <td>
                        <asp:DropDownList ID="ClassNode" CssClass="form-control text_md" runat="server"></asp:DropDownList></td>
                </tr>
                <tr>
                    <td>META描述： </td>
                    <td>
                        <asp:TextBox ID="PageMetakeyinfo" runat="server" Height="106px" style="max-width:300px;" TextMode="MultiLine" class="form-control text_md"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>关键字： </td>
                    <td>
                        <asp:TextBox ID="keyword" runat="server" class="form-control text_md"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>栏目提示： </td>
                    <td>
                        <asp:TextBox ID="Nodeimgtext" runat="server" class="form-control text_md"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: center">
                        <asp:Button ID="BtnSubmit" runat="server" Text="确认" class="btn btn-primary" OnClick="BtnSubmit_Click" />
                        <asp:Button ID="BtnCancle" runat="server" Text="取消" class="btn btn-primary" CausesValidation="false" OnClick="BtnCancle_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>