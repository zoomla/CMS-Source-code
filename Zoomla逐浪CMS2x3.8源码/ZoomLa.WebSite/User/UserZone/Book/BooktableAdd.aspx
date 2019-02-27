<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="BooktableAdd.aspx.cs" Inherits="BooktableAdd" %>

<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc3" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>添加读书</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li><a href="BookList.aspx">读书</a></li>
        <li class="active">添加书籍</li>
		<div class="clearfix"></div>
    </ol>
    <div style="width: 100%">
        <uc3:WebUserControlTop ID="WebUserControlTop1" runat="server"></uc3:WebUserControlTop>
        <br />
        <div style="margin-top:10px;">
            <table class="table table-striped table-bordered table-hover">
                <tr>
                    <td colspan="2" class="text-center"><img src="../Images/icon_80.gif" style="width: 57px; height: 54px" />添加书籍</td>
                </tr>
                <tr>
                    <td width="29%" align="center">名称： </td>
                    <td width="71%">
                        <asp:TextBox ID="titletxt" CssClass="form-control text_md" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="titletxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="center">又名： </td>
                    <td>
                        <asp:TextBox ID="othertitletxt" CssClass="form-control" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td align="center"><span class="pl">作者：</span></td>
                    <td>
                        <asp:TextBox ID="antxt" CssClass="form-control text_md" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="antxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="center"><span class="pl">ISBN：</span></td>
                    <td>
                        <asp:TextBox ID="isbntxt" CssClass="form-control text_md" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="isbntxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="center" style="height: 24px"><span class="pl">定价：</span></td>
                    <td style="height: 24px">
                        <asp:TextBox ID="pricetxt" CssClass="form-control text_md" runat="server"></asp:TextBox>
                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="pricetxt"
                            ErrorMessage="请输入正确的书本价格" Operator="DataTypeCheck" Type="Double"></asp:CompareValidator></td>
                </tr>
                <tr>
                    <td align="center"><span class="pl">出版社：</span></td>
                    <td>
                        <asp:TextBox ID="concermtxt" CssClass="form-control text_md" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="yeartxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td align="center"><span class="pl">出版年：</span></td>
                    <td>
                        <asp:TextBox ID="yeartxt" runat="server" CssClass="form-control text_md" MaxLength="4"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="不能为空" ControlToValidate="yeartxt"></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="yeartxt"
                            ErrorMessage="请输入正确的年份 如2008" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator></td>
                </tr>
                <tr>
                    <td align="center">图片： </td>
                    <td>
                        <ZL:FileUpload ID="FileUpload1" class="form-control text_md" runat="server" name="FileUpload1" />
                        <%--<input type="file" name="FileUpload1" class="form-control text_md" id="FileUpload1" runat="server" />--%>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="FileUpload1" ErrorMessage="不能为空"></asp:RequiredFieldValidator>
                        <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center"><span class="pl">内容：</span></td>
                    <td>
                        <textarea id="contenttxt" class="form-control text_md" runat="server" rows="10" style="max-width: 330px"></textarea>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="contenttxt" ErrorMessage="不能为空"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td></td>
                    <td><asp:Button ID="sBtn" runat="server" CssClass="btn btn-primary" OnClick="sBtn_Click" Text="添加保存" /></td>
                </tr>
            </table>
        </div>
    </div>

</asp:Content>
