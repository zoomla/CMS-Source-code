<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="FileShareCreat.aspx.cs" Inherits="ZoomLa.GatherStrainManage.FileShareCreat" %>

<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<%@ Register Src="WebUserControlGztherLetf.ascx" TagName="WebUserControlGztherLetf" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>上传共享文件</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="GSManage.aspx">我的群族</a></li>
        <li><a href="GSBuid.aspx?GSID=<%=GSID %>&where=5"><asp:Label ID="labGSName" runat="server"></asp:Label></a></li>
        <li><a href="FileShareManage.aspx?GSID=<%=GSID %>">群族共享文件列表</a></li>
        <li class="active">上传共享文件</li>
    </ol>
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" /><br />
    <uc2:WebUserControlGztherLetf ID="WebUserControlGztherLetf2" runat="server" />
    <div class="us_topinfo" style="margin-top: 10px;">
        <div><a href="GSBuid.aspx?GSID=<%=GSID %>&where=5"><asp:Image ID="imgGSICQ" runat="server" Width="150px" Height="100px" /></a></div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" class="text-center"><strong>上传文件</strong></td>
            </tr>
            <tr>
                <td style="width: 165px" align="right">&nbsp;上传文件路径：</td>
                <td>
                    <ZL:FileUpload runat="server" ID="FileUpload1"/>
              <%--      <asp:FileUpload CssClass="form-control" style="max-width:530px;" ID="FileUpload1" runat="server" />--%>

                </td>
            </tr>
            <tr>
                <td style="width: 165px" align="right">&nbsp;文件注释：</td>
                <td><asp:TextBox ID="txtMono" CssClass="form-control" style="max-width:530px;" runat="server" MaxLength="25" Width="527px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 165px"></td>
                <td align="center">
                    <asp:Button ID="btnOK" runat="server" CssClass="btn btn-primary" OnClick="btnOK_Click" Text="上传" />
                    <asp:Button ID="btnCal" runat="server" CssClass="btn btn-primary" OnClick="btnCal_Click" Text="取消" /></td>
            </tr>
        </table>
    </div>
</asp:Content>
