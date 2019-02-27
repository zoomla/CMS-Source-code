<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="FileShareManage.aspx.cs" Inherits="ZoomLa.GatherStrainManage.FileShareManage" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<%@ Register Src="WebUserControlGztherLetf.ascx" TagName="WebUserControlGztherLetf" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>群族共享文件</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="GSManage.aspx">我的群族</a></li>
        <li><a href="GSBuid.aspx?GSID=<%=GSID %>&where=5">
            <asp:Label ID="labGSName" runat="server"></asp:Label></a></li>
        <li class="active">群族共享文件列表</li>
    </ol>
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" /><br />
    <uc2:WebUserControlGztherLetf ID="WebUserControlGztherLetf2" runat="server" />
    <div>
        <a href="GSBuid.aspx?GSID=<%=GSID %>&where=5"><asp:Image ID="imgGSICQ" runat="server" Width="150px" Height="100px" /></a>
    </div>
    <table class="table table-bordered table-striped">
        <tr>
            <td>
                <div class="btn-group">
                    <a href='GSBuid.aspx?GSID=<%=GSID %>' class="btn btn-primary">群族首页</a>
                    <a href='CreatHuaTee.aspx?GSID=<%=GSID %>' class="btn btn-primary">话题</a>
                    <a href='FileShareManage.aspx?GSID=<%=GSID %>' class="btn btn-primary">文件共享</a>
                    <a href='GSMember.aspx?GSID=<%=GSID %>' class="btn btn-primary">成员</a>
                    <a href="FileShareCreat.aspx?GSID=<%=GSID %>&where=5" class="btn btn-primary">上传文件</a>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10"  OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="当前无共享文件">
                    <Columns>
                        <asp:BoundField DataField="FactFileName" HeaderText="文件名称" />
                        <asp:TemplateField HeaderText="大小">
                            <ItemTemplate>
                                <%# Eval("FileSize") %><span style="color:#f00;">B</span>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DownCount" HeaderText="下载" />
                        <asp:TemplateField HeaderText="上传者">
                            <ItemTemplate>
                                <a href="#">
                                    <%#DataBinder.Eval(Container.DataItem, "UserName")%>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="CreatTime" HeaderText="上传时间" />
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDown" runat="server" OnClick="lbtnDown_Click">下载</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnDel" OnClientClick="return confirm('你确定要删除吗？');"  CommandArgument='<%# Eval("ID") %>' runat="server" OnClick="lbtnDel_Click">删除</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="80px" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" />
                    <RowStyle HorizontalAlign="Center" />
                </ZL:ExGridView>
            </td>
        </tr>
    </table>
</asp:Content>
