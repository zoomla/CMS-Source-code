<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClassShow.aspx.cs" Inherits="ZoomLaCMS.Manage.Zone.ClassShow"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>班级浏览</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="td_m text-right">班级名称:</td>
            <td><asp:Label runat="server" ID="ClassName_L"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">班标:</td>
            <td><asp:Label runat="server" ID="ClassIcon_L"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">所属学校:</td>
            <td><asp:Label ID="SchoolName_L" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">班级年级:</td>
            <td>
                <asp:Label ID="Grade_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-right">班主任:</td>
            <td>
                <asp:Label ID="CreateUser_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-right">是否毕业:</td>
            <td><asp:Literal ID="IsDonw_L" runat="server" EnableViewState="false"></asp:Literal></td> 
        </tr>
        <tr>
            <td class="text-right">班级星级:</td>
            <td>
                <asp:Literal EnableViewState="false" ID="Star_Li" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td class="text-right">建立时间:</td>
            <td>
                <asp:Label ID="CreateTime_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="text-right">班级信息:</td>
            <td>
                <asp:Label ID="ClassInfo_L" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
    <div class="text-center">
    <a href="AddClassRoom.aspx?id=<%=ClassID %>" class="btn btn-primary">重新修改</a>
    <a href="SnsClassRoom.aspx" class="btn btn-primary">返回列表</a>
    </div>
</asp:Content>


