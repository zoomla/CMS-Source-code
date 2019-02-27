<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Structure.aspx.cs" Inherits="User_UserZone_Structure"  %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>企业结构</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="pageflag" data-nav="index" data-ban="zone"></div>
    <div class="container margin_t5">
        <ol class="breadcrumb">
            <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
            <li class="active">企业结构</li>
            <div class="clearfix"></div>
        </ol>
    </div>
    <div class="container">
        <div id="nostruct" runat="server">
            <div class="alert alert-info fade in">
                <button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>
                <h4>您还没有加入企业结构！</h4>
                <p>
                    <a href="Structure.aspx?Struct=yx" class="btn btn-danger">可加入的企业结构</a>
                    <a href="AddStructure.aspx" class="btn btn-danger">创建企业结构</a>
                </p>
            </div>
        </div>
        <div id="manageinfos" runat="server" class="us_seta" style="height: 25px;" visible="false">
            <div class=" btn-group">
                <a href="Structure.aspx?Struct=join" class="btn btn-primary">我加入的企业结构</a>
                <a href="Structure.aspx?Struct=my" class="btn btn-primary">我创建的企业结构</a>
                <a href="Structure.aspx?Struct=yx" class="btn btn-primary">可加入的企业结构</a>
                <a href="AddStructure.aspx" class="btn btn-primary">创建企业结构</a>
            </div>
            <ul style="width: 100%">
                <li style="width: 120px; text-align: center"></li>
                <li style="width: 120px; text-align: center"></li>
                <li style="width: 120px; text-align: center"></li>
                <li style="width: 120px; text-align: center"></li>
            </ul>
            <div id="joinstruct" runat="server" class="us_seta" style="margin-top: 5px;">
                <h1 align="center"></h1>

                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td colspan="3">我加入的企业结构
                        </td>
                    </tr>
                    <tr>
                        <th>ID</th>
                        <th width="10%">结构名称</th>
                        <th align="left">操作</th>
                    </tr>
                    <asp:Repeater ID="Repeater3" runat="server" OnItemCommand="Repeater3_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%#Eval("ID") %>
                                </td>
                                <td><a href='StructView.aspx?ID=<%#Eval("ID") %>' title="查看"><%#Eval("Name") %> </a></td>
                                <td>
                                    <asp:LinkButton ID="Linkbtn1" Text="退出" CommandArgument='<%#Eval("ID") %>' CommandName="joout" runat="server"></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div id="mystruct" runat="server" class="us_seta" style="margin-top: 5px;">
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td colspan="2" class="text-center">我创建的企业结构
                        </td>
                    </tr>
                    <tr>
                        <th>ID</th>
                        <th>名称</th>
                    </tr>
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ID") %></td>
                                <td>
                                    <a href='StructView.aspx?ID=<%#Eval("ID") %>' title="查看"><%#Eval("Name") %> </a>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
            <div id="yxstruct" runat="server" visible="false" class="us_seta" style="margin-top: 5px;">
                <table class="table table-striped table-bordered table-hover">
                    <tr>
                        <td colspan="3">可加入的企业结构
                        </td>
                    </tr>
                    <tr>
                        <th>ID</th>
                        <th width="10%">结构名称</th>
                        <th align="left">操作</th>
                    </tr>
                    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                        <ItemTemplate>
                            <tr>
                                <td><%#Eval("ID") %></td>
                                <td><%#Eval("Name") %></td>
                                <td>
                                    <asp:LinkButton ID="Linkbtn1" Text="加入" CommandArgument='<%#Eval("ID") %>' CommandName="join" runat="server"></asp:LinkButton>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
