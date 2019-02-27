<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="CreatHuaTee.aspx.cs" Inherits="ZoomLa.GatherStrainManage.CreatHuaTee" %>
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
        <li class="active">话题列表</li>
    </ol>
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" /><br />
    <uc2:WebUserControlGztherLetf ID="WebUserControlGztherLetf2" runat="server" />
    <div>
        <a href="GSBuid.aspx?GSID=<%=GSID %>&where=5"><asp:Image ID="imgGSICQ" runat="server" Width="150px" Height="100px" /></a>
    </div>
    <table class="table table-bordered">
        <tr>
            <td>
                <div class="btn-group">
                    <a href='GSBuid.aspx?GSID=<%=GSID %>' class="btn btn-primary">群族首页</a>
                    <a href='CreatHuaTee.aspx?GSID=<%=GSID %>' class="btn btn-primary">话题</a>
                    <a href='FileShareManage.aspx?GSID=<%=GSID %>' class="btn btn-primary">文件共享</a>
                    <a href='GSMember.aspx?GSID=<%=GSID %>' class="btn btn-primary">成员</a>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <ZL:ExGridView ID="gvHuaTee" runat="server" CssClass="table table-striped table-bordered table-hover" CellPadding="4" Width="100%" AutoGenerateColumns="False"  GridLines="Horizontal" OnRowDataBound="gvHuaTee_RowDataBound">
                    <RowStyle HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateField HeaderText="话题">
                            <ItemTemplate>
                                <a href="HuaTeeRevert.aspx?GSID=<%=GSID %>&HTID=<%#DataBinder.Eval(Container.DataItem, "ID")%>">
                                    <%#DataBinder.Eval(Container.DataItem, "HuaTeeTitle")%>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="RevertCount" HeaderText="回复次数" />
                        <asp:TemplateField HeaderText="作者">
                            <ItemTemplate>
                                <%--<asp:Image ID="imgUserICQ" runat="server" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Userpic")%>' />--%>
                                <a href="#">
                                    <%#DataBinder.Eval(Container.DataItem, "UserName")%>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LastTime" HeaderText="最后回复" />
                    </Columns>
                    <EmptyDataTemplate>
                        <table class="tinputbody" cellpadding="0" cellspacing="1" border="0" width="100%">
                            <tr>
                                <td align="center" style="height: 200px">没有话题
                                </td>
                            </tr>
                        </table>
                    </EmptyDataTemplate>
                </ZL:ExGridView>
            </td>
        </tr>
        <tr>
            <td>
                <a href='AppearHuaTee.aspx?GSID=<%=GSID %>'>发表话题</a>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
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
