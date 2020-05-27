<%@ Page Language="C#"  MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="AddStruct.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.AddStruct" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
<link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
<title>添加项目类型</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <table class="table table-striped table-bordered table-hover">
        <tr class="spacingtitle" style="height: 30px;">
            <td align="center" colspan="2">
                <asp:Label ID="lblText" runat="server">组织结构</asp:Label></td>
        </tr>
        <tr>
            <td style="width:120px;"><strong>父结构：</strong></td>
            <td>
                <asp:Label runat="server" ID="parent_L" />
            </td>
        </tr>
        <tr>
            <td style="width:120px;"><strong>结构名：</strong></td>
            <td>
                <asp:TextBox ID="TxtStructName" class="form-control text_md" runat="server" />
                <asp:RequiredFieldValidator runat="server" ID="r1" ControlToValidate="TxtStructName" ForeColor="Red" ErrorMessage="结构名不能为空" />
                <asp:Label ID="LblMessage" runat="server" Text=""></asp:Label>
            </td>
        </tr>
         <tr>
            <td><strong>结构描述：</strong></td>
            <td>
                <asp:TextBox runat="server" ID="Remind_T" CssClass="form-control text_md" TextMode="MultiLine" style="height:150px;"/>
            </td>
        </tr>
         <tr>
            <td><strong>是否启用：</strong></td>
            <td>
                <input type="checkbox" runat="server" id="rd1" class="switchChk" checked="checked"/>
            </td>
        </tr>
        <tr>
            <td><strong>操作：</strong></td>
            <td>
                <asp:Button ID="BtnCommit" runat="server" Text="提交" class="btn btn-primary" OnClick="Button1_Click" />
                <a href="StructList.aspx?type=0" class="btn btn-primary">返回</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
</asp:Content>

