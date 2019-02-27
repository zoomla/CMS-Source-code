<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateView.aspx.cs" Inherits="ZoomLaCMS.Manage.Config.CreateView"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>创建视图</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" align="center">
                <label id="lbTag" runat="server">创建视图</label>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 80px"><strong>视图名：</strong></td>
            <td>ZL_V_<input type="text" class="form-control" id="txtVName" runat="server" />
                <asp:Button ID="CheckName" runat="server" Text="检测视图名" CssClass="btn btn-primary" OnClick="CheckName_Click" />
                <label id="lbCheck" runat="server" visible="false"></label>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 80px"><strong>选择数据表：</strong></td>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:ListBox ID="ListBox1" runat="server" CssClass="form-control" Height="100px" Width="200px" SelectionMode="Multiple"></asp:ListBox>
                            <asp:Button ID="BtnAdd" runat="server" Text="确认" CssClass="btn btn-primary" OnClick="BtnAdd_Click" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 80px">选择数据：</td>
            <td>
                <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater1_ItemDataBound" OnItemCommand="Repeater1_ItemCommand">
                    <ItemTemplate>
                        <div style="float: left; width: 220px; margin-top: 10px;">
                            <ul>
                                <li style="background-color: #9AC7F0;"><strong style="width: 165px; overflow: hidden;"><%#Eval("tableName") %></strong><input type="hidden" runat="server" id="tN" value='<%#Eval("tableName") %>' />
                                    <div style="float: right;">
                                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Del" OnClientClick="return confirm('你确认要移除该表吗？')">移除</asp:LinkButton></div>
                                </li>
                                <li>
                                    <asp:ListBox ID="ListBox3" CssClass="form-control" runat="server" Height="100px" Width="100%" SelectionMode="Multiple"></asp:ListBox>
                                </li>
                            </ul>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </td>
        </tr>
        <tr>
            <td align="right" style="width: 120px">
                <strong>连接条件(可为空)：</strong>
                <p></p>
            </td>
            <td>
                <textarea runat="server" class="form-control" id="taWhere" style="width: 800px; height: 50px"></textarea>
                <p>连接条件格式为： 表1.字段名=表2.字段名 and 表3.字段=表4.字段名，依次类推</p>
            </td>
        </tr>

        <tr>
            <td align="right" style="width: 80px"><strong>SQL语句：</strong></td>
            <td>
                <textarea runat="server" id="taSQL" class="form-control" readonly="readonly" style="width: 800px; height: 100px"></textarea>
                <asp:Button ID="BtnSQL" runat="server" Text="预览SQL" CssClass="btn btn-primary" OnClick="BtnSQL_Click" />
            </td>
        </tr>

        <tr>
            <td align="right" style="width: 80px"><strong>视图说明(选填)：</strong></td>
            <td>功能：<input type="text" class="form-control" runat="server" id="explain" />
            </td>
        </tr>

        <tr>
            <td align="right" style="width: 80px"></td>
            <td>
                <asp:Button ID="BtnSub" runat="server" Text="生成视图" CssClass="btn btn-primary" OnClick="BtnSub_Click" />
                <a href="ViewList.aspx" class="btn btn-primary" target="_self">取消</a>
            </td>
        </tr>
    </table>
</asp:Content>
