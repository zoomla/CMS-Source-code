<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MoveToSpec.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Content.MoveToSpec" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>

<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>移动到其他专题</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" class="text-center">移动专题信息到其他专题</td>
        </tr>
        <tr>
            <td>
                <strong>选定的专题信息ID：</strong>
            </td>
            <td>
                <asp:TextBox ID="TxtGeneralID" runat="server" Width="280px" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <strong>移动到目标专题：</strong>
                <br />
                <span style="color: Red"></span>
            </td>
            <td>
                <asp:ListBox ID="LstSpec" CssClass=" form-control" runat="server" Rows="10" Width="280px"></asp:ListBox>
                <br />
                <input id="Button1" onclick="SelectAll()" type="button" class="btn btn-primary" value="  选定所有专题  " />
                <input id="Button2" onclick="UnSelectAll()" type="button" class="btn btn-primary" value="取消选定所有专题" />
            </td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <asp:Button ID="BtnBacthSet" runat="server" Text="执行批处理" CssClass="btn btn-primary" OnClick="BtnBacthSet_Click" />
                <asp:Button ID="BtnCancel" runat="server" Text="取消" CssClass="btn btn-primary" OnClick="BtnCancel_Click" />
            </td>
        </tr>
    </table>
</asp:Content>
