<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="HuaTeeRevert.aspx.cs" Inherits="ZoomLa.GatherStrainManage.HuaTeeRevert" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<%@ Register Src="WebUserControlGztherLetf.ascx" TagName="WebUserControlGztherLetf" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的空间</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="GSManage.aspx">我的群族</a></li>
        <li><a href="GSBuid.aspx?GSID=<%=GSID %>&where=5"><asp:Label ID="labGSName" runat="server"></asp:Label></a></li>
        <li><a href="CreatHuaTee.aspx?GSID=<%=GSID %>&where=5">话题列表</a></li>
        <li class="active">话题内容</li>
    </ol>
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" /><br />
    <uc2:WebUserControlGztherLetf ID="WebUserControlGztherLetf2" runat="server" />
    <div class="us_topinfo" style="margin-top: 10px;">
        <div>
            <a href="GSBuid.aspx?GSID=<%=GSID %>&where=5">
                <asp:Image ID="imgGSICQ" runat="server" Width="150px" Height="100px" /></a>
        </div>
        <table class="table table-striped table-bordered table-hover">
            <tr>
                <td class="text-center">
                    <strong>标题:<asp:Label ID="Label1" runat="server" Text=""></asp:Label></strong>
                </td>
            </tr>
            <tr>
                <td>
                    <ZL:ExGridView ID="gvHuaTess" PageSize="20" CssClass="table table-bordered table-striped" runat="server" Width="100%" CellPadding="4" AutoGenerateColumns="False"
                        DataKeyNames="ID">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 100px">
                                                <a href="#">
                                                    <%#DataBinder.Eval(Container.DataItem,"UserName") %>
                                                </a>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CreatTime") %>'
                                                    ForeColor="DimGray"></asp:Label>
                                            </td>
                                            <td align="right">楼主
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <br />
                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"HuaTeeContent") %>'></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="tr1" runat="server" visible='<%#GetVisible(DataBinder.Eval(Container.DataItem,"ID").ToString(),DataBinder.Eval(Container.DataItem,"UserID").ToString() )%>'>
                                            <td colspan="3" align="right">
                                                <a href="?menu=edit&id=<%#DataBinder.Eval(Container.DataItem,"ID").ToString() %>&Gid=<%=GSID %>">编辑</a>&nbsp;
									|&nbsp; <a href="?menu=delete&id=<%#DataBinder.Eval(Container.DataItem,"ID").ToString()%>&Gid=<%=GSID %>"
                                        onclick="return confirm('你确定要删除吗？');">删除</a>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </ZL:ExGridView>
                    <ZL:ExGridView ID="gvRes" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False" CellPadding="4" Width="100%" DataKeyNames="ID" GridLines="Horizontal">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td style="width: 100px">
                                                <a href="#">
                                                    <%#DataBinder.Eval(Container.DataItem,"UserName") %>
                                                </a>
                                            </td>
                                            <td>
                                                <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"CreatTime") %>'
                                                    ForeColor="DimGray"></asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Taxis") %>'></asp:Label>楼
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <br />
                                                <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Content") %>'></asp:Label>
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3" align="right">
                                                <asp:LinkButton ID="LinkButton2" runat="server" OnClientClick="return confirm('你确定要删除吗？');return false"
                                                    OnClick="LinkButton2_Click" CausesValidation="False">删除</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <table class="tinputbody" cellpadding="0" cellspacing="1" border="0" width="100%">
                                <tr>
                                    <td align="center" width="100%">当前无回复
                                    </td>
                                </tr>
                            </table>
                        </EmptyDataTemplate>
                    </ZL:ExGridView>
                </td>
            </tr>
            
            <tr>
                <td>
                    <div runat="server" id="reply">
                        <table class="table table-bordered">
                            <tr>
                                <td style="width: 117px" align="right" valign="top">
                                    <strong>回复话题：</strong><span style="color: #d01e3b">*&nbsp; </span>&nbsp;
                                </td>
                                <td>
                                    <textarea cols="40" class="form-control" style="max-width:300px;" rows="5" id="FreeTextBox1" runat="server"></textarea>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FreeTextBox1" Display="Dynamic" ErrorMessage="请填写回复内容" Font-Size="10pt"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <asp:Button ID="btnOK" CssClass="btn btn-primary" runat="server" Text="提交回复" OnClick="btnOK_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
