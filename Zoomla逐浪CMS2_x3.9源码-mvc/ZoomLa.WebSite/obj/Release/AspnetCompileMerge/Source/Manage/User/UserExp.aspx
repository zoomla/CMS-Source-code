<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserExp.aspx.cs" Inherits="ZoomLaCMS.Manage.User.UserExp"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>会员<%=GetTypeName() %></title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div id="UserPDiv" runat="server">
        <table class="table table-striped table-bordered table-hover">
            <tr class="text-center">
                <td colspan="2"><asp:Label ID="Lbl_t" runat="server" Text="会员积分操作"></asp:Label></td>
            </tr>
            <tr>
                <td class="text-right td_l">会员名：</td>
                <td>
                    <a href="UserInfo.aspx?id=<%:UserID %>"><asp:Label ID="lbUserName" runat="server" Text=""></asp:Label></a>
                </td>
            </tr>
            <tr>
                <td class="text-right">会员<%=GetTypeName() %>：</td>
                <td><asp:Label ID="lblExp" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td class="text-right"><%=GetTypeName() %>操作：</td>
                <td>
                    <asp:RadioButtonList ID="rblExp" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1" Selected="True">增加</asp:ListItem>
                        <asp:ListItem Value="2">扣减</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>

            <tr>
                <td class="text-right"><%=GetTypeName() %>：</td>
                <td>
                    <asp:TextBox ID="TxtScore" runat="server" class="form-control text_md num"></asp:TextBox>
                    <asp:RegularExpressionValidator runat="server" Display="Dynamic" ID="RV2" ValidationExpression="^(\d*\.\d{0,2}|[1-9]+).*$" ControlToValidate="TxtScore" ErrorMessage="只能输入非0整数" ForeColor="Red" />
                    <asp:RequiredFieldValidator runat="server" Display="Dynamic" ID="ScoreVa" ControlToValidate="TxtScore" ErrorMessage="不能为空!" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td class="text-right">备注：</td>
                <td>
                    <asp:TextBox ID="TxtDetail" runat="server" TextMode="MultiLine" class="form-control m715-50" Height="150"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="text-right">同时操作会员：</td>
                <td>
                    <asp:TextBox ID="SourceMem" runat="server" CssClass="form-control text_300" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="EBtnSubmit" Text="确定" OnClick="EBtnSubmit_Click" runat="server" class="btn btn-primary" />
                    <a href="Userinfo.aspx?id=<%:UserID %>" class="btn btn-primary">取消</a>
                </td>
            </tr>
        </table>
        <ZL:ExGridView ID="EGV" runat="server" AutoGenerateColumns="False" DataKeyNames="ExpHisID" PageSize="20" OnPageIndexChanging="EGV_PageIndexChanging" IsHoldState="false" OnRowCommand="EGV_RowCommand" AllowPaging="True" AllowSorting="True" CellPadding="2" CellSpacing="1" BackColor="White" ForeColor="Black" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" GridLines="None" EnableModelValidation="True">
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="ExpHisID" HeaderStyle-Height="22" />
                <asp:BoundField HeaderText="操作时间" DataField="HisTime" />
                <asp:BoundField HeaderText="操作金额" DataField="Score" DataFormatString="{0:f2}" />
                <asp:TemplateField HeaderText="操作人">
                    <ItemTemplate>
                        <a href='AdminDetail.aspx?roleid=0&id=<%#Eval("Operator") %>' target="_blank"><%#GetUserName(Eval("Operator", "{0}"))%></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="详细" DataField="Detail" />
                <asp:TemplateField HeaderText="操作">
                    <ItemTemplate>
                        <a href="UserExp.aspx?UserID=<%=UserID %>&type=<%=ExpType %>&editid=<%#Eval("ExpHisID") %>">修改</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </ZL:ExGridView>
    </div>
    <table id="EditMoney" class="table table-striped table-bordered table-hover" runat="server" visible="false">
        <tr>
            <td class="text-right td_l">ID：</td>
            <td>
                <asp:Label ID="UserExpDomPID" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td class="text-right">操作时间：</td>
            <td>
                <asp:TextBox ID="HisTime" CssClass="form-control text_md" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="text-right">会员<%=GetTypeName() %>：</td>
            <td>
                <asp:TextBox ID="Score" CssClass="form-control text_md" runat="server" Text="0"></asp:TextBox>
                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ValidationExpression="^(\d*\.\d{0,2}|[1-9]+).*$" Display="Dynamic" ControlToValidate="Score" ErrorMessage="只能输入非0整数" ForeColor="Red" />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="Dynamic" ControlToValidate="Score" ErrorMessage="不能为空!" ForeColor="Red" />
            </td>
        </tr>
        <tr>
            <td class="text-right">详细：</td>
            <td>
                <asp:TextBox ID="Detail" Rows="10" TextMode="MultiLine"  CssClass="form-control m715-50" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="EditBtn" Text="修改" class="btn btn-primary" OnClick="EditBtn_Click" runat="server" />
                <a href="UserExp.aspx?UserID=<%=UserID %>&type=<%=ExpType %>" class="btn btn-primary">取消</a>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="UserPH" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript" src="/JS/ZL_Regex.js"></script>
    <script>
        $(function () { ZL_Regex.B_Float(".num") });
    </script>
</asp:Content>
