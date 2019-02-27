<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="GSManage.aspx.cs" Inherits="ZoomLa.GatherStrainManage.GSManage" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<%@ Register Src="WebUserControlGztherLetf.ascx" TagName="WebUserControlGztherLetf" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的空间</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="zone"></div>
    <div class="container margin_t5">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="/User/Userzone/Default.aspx">我的空间</a></li>
        <li class="active">我的群族</li>
		<div class="clearfix"></div>
    </ol></div>
    <div class="container btn_green">
        <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" />
        <uc2:WebUserControlGztherLetf ID="WebUserControlGztherLetf1" runat="server" />
        <table class="table table-bordered">
        <tr>
            <td style="width: 70%" valign="top">
                <table class="table table-striped table-bordered table-hover">
                    <tr>

                        <td>
                            <div class="input-group" style="width:240px;margin:auto;">
                                <asp:TextBox ID="TextBox1" placeholder="搜索族群: 输入关键字" runat="server" CssClass="form-control"></asp:TextBox>
                                <span class="input-group-btn">
                                    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="搜索" OnClick="Button1_Click" />
                                </span>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <ZL:ExGridView ID="gvMu" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="ID">
                                <Columns>
                                    <asp:BoundField DataField="GSName" HeaderText="群族名称">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserName" HeaderText="群族管理员">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">加入该族群</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    无族群记录
                                </EmptyDataTemplate>
                            </ZL:ExGridView>
                        </td>
                    </tr>
                    <tr>
                        <td>邀请我加入的群</td>
                    </tr>
                    <tr>
                        <td valign="top" align="center">
                            <ZL:ExGridView ID="GV_Intive" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" DataKeyNames="ID">
                                <Columns>
                                    <asp:BoundField DataField="GSName" HeaderText="群族名称">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserName" HeaderText="群族管理员">
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="操作">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LBintive_Click">加入该族群</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    无邀请的族群
                                </EmptyDataTemplate>
                            </ZL:ExGridView>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table class="table table-bordered">
                    <tr>
                        <td style="height: 18px">我加入的群
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <ZL:ExGridView ID="gvGS" ShowHeader="false" runat="server" AutoGenerateColumns="False"
                                CellPadding="4" CssClass="table table-striped table-bordered table-hover" OnRowDataBound="gvGS_RowDataBound" DataKeyNames="ID,UserID">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td rowspan="2" style="width: 45px">
                                                        <a href='GSBuid.aspx?GSID=<%#DataBinder.Eval(Container.DataItem, "ID")%>&where=5'>
                                                            <asp:Image ID="imgGSICQ" runat="server" ImageUrl='<%#PicUrl(DataBinder.Eval(Container.DataItem, "GSICO").ToString())%>'
                                                                Height="100px" Width="150px" /></a>
                                                    </td>
                                                    <td>
                                                        <a href='GSBuid.aspx?GSID=<%#DataBinder.Eval(Container.DataItem, "ID")%>&where=5'>
                                                            <%#DataBinder.Eval(Container.DataItem, "GSName")%>
                                                        </a>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="Label1" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "UserName")%>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <table class="tinputbody" cellpadding="0" cellspacing="1" border="0" width="100%">
                                        <tr>
                                            <td align="center">您还没有创建群族,也没有加入其他群
                                            </td>
                                        </tr>
                                    </table>
                                </EmptyDataTemplate>
                            </ZL:ExGridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script language="javascript">
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }
    </script>
</asp:Content>
