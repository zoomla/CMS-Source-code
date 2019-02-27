<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="GSBuid.aspx.cs" Inherits="ZoomLa.GatherStrainManage.GSBuid" %>
<%@ Register Src="../WebUserControlTop.ascx" TagName="WebUserControlTop" TagPrefix="uc1" %>
<%@ Register Src="WebUserControlGztherLetf.ascx" TagName="WebUserControlGztherLetf" TagPrefix="uc2" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>创建群族</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li><a href="GSManage.aspx">我的群族</a></li>
        <li class="active">群族信息</li>
    </ol>
    <uc1:WebUserControlTop ID="WebUserControlTop1" runat="server" />
    <br />
    <uc2:WebUserControlGztherLetf ID="WebUserControlGztherLetf1" runat="server" />
    <table class="table table-striped table-bordered table-hover    ">
        <tr>
            <td rowspan="5" style="width:20%;">
                <asp:Image ID="imgGSICQ" runat="server" Width="150px" Height="100px" /><br />
                <label id="lab1" runat="server" visible="false">
                    <asp:TextBox ID="txtpic" CssClass="form-control" style="max-width:300px;" runat="server" Width="300px" /><br />
                    <iframe id="Clearimgs" style="top: 2px" src="../../FileUpload.aspx?menu=txtpic" width="400px" height="25px" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe>
                </label>
            </td>
            <td>
                名称：<asp:Label runat="server" ID="LB_name" Text=""></asp:Label>
                <asp:TextBox ID="TB_Name" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
                <asp:LinkButton runat="server" ID="LK_rename" ForeColor="Red" OnClick="LK_rename_Click1">修改</asp:LinkButton>
                <asp:LinkButton runat="server" ID="LK_sure" ForeColor="Red" Visible="false" OnClick="LK_sure_Click">确定</asp:LinkButton>
            </td>

            <td width="200px" rowspan="4">

                <div runat="server" id="DV_addmanager">
                    <a href="ManagerZone.aspx?GSID=<%=GSID %>&type=2">添加副群长 </a>
                    <br />
                </div>
                <div runat="server" id="DV_manager">
                    <a href="ManagerZone.aspx?GSID=<%=GSID %>&type=6">邀请好友加入本群族 </a>
                    <br />
                    <a href="ManagerZone.aspx?GSID=<%=GSID %>&type=4">冻结会员 </a>
                    <br />
                    <a href="ManagerZone.aspx?GSID=<%=GSID %>&type=5">黑名单 </a>
                    <br />
                </div>
                <asp:LinkButton ID="lbtnDeleteGS" runat="server" OnClientClick='return confirm("确定要删除本群吗？")' OnClick="lbtnDeleteGS_Click">删除群族</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="width: 527px">角色：<asp:Label ID="Label2" runat="server" Text=''></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 527px">创建时间：<asp:Label ID="labCreattime" runat="server" Text=''></asp:Label></td>
        </tr>
        <tr>
            <td style="width: 527px">介绍：<asp:Label ID="labInfo" runat="server" Text=''></asp:Label>
                <asp:TextBox ID="TB_Info" CssClass="form-control" runat="server" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table class="table table-bordered">
        <tr>
            <td colspan="2">
                <div class="btn-group">
                    <a href='GSBuid.aspx?GSID=<%=GSID %>&where=5' class="btn btn-primary">群族首页</a>
                    <a id="a_talk" runat="server" class="btn btn-primary">话题</a>
                    <a href='FileShareManage.aspx?GSID=<%=GSID %>&where=5' class="btn btn-primary">文件共享</a>
                    <a href='GSMember.aspx?GSID=<%=GSID %>&where=5 class="btn btn-primary"' class="btn btn-primary">成员</a>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table class="table table-striped table-bordered">
                    <tr>
                        <td colspan="2">
                            <ZL:ExGridView ID="gvHuaTee" PageSize="20" runat="server" CssClass="table table-striped table-bordered table-hover" CellPadding="4" Width="100%" AutoGenerateColumns="False" GridLines="Horizontal" OnRowDataBound="gvHuaTee_RowDataBound">
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <asp:TemplateField HeaderText="话题">
                                        <ItemTemplate>
                                            <a href="HuaTeeRevert.aspx?GSID=<%=GSID %>&HTID=<%#DataBinder.Eval(Container.DataItem, "ID")%>&where=5">
                                                <%#DataBinder.Eval(Container.DataItem, "HuaTeeTitle")%>
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="RevertCount" HeaderText="回复次数" />
                                    <asp:TemplateField HeaderText="作者">
                                        <ItemTemplate>
                                            <%--<asp:Image ID="imgUserICQ" runat="server" ImageUrl='<%#DataBinder.Eval(Container.DataItem, "Userpic")%>' />
								<a href="#">--%>
                                            <%#DataBinder.Eval(Container.DataItem, "UserName")%>
                                            <%--</a>--%>
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
                            <div runat="server" id="a_publish"><strong><a href='AppearHuaTee.aspx?GSID=<%=GSID %>&where=5'>发表话题</a></strong></div>
                        </td>
                    </tr>
                </table>
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
