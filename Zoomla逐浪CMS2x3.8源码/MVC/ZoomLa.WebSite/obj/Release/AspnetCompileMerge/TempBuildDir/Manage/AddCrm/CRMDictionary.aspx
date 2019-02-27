<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CRMDictionary.aspx.cs" Inherits="ZoomLaCMS.Manage.AddCrm.CRMDictionary" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>CRM配置</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Literal ID="liTitle" runat="server" Visible="false"></asp:Literal>
    <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
    <asp:Panel ID="asdf" runat="server">
        <br />
        <table  class="table table-striped table-bordered table-hover">
            <tr class="gridtitle">
                <td colspan="5" align="center" style="height: 25px;">
                    <asp:Label ID="Tit" runat="server" Text="客户区域表"></asp:Label>
                </td>
            </tr>
            <tr class="tdbgleft">
                <td align="center">
                    序号
                </td>
                <td align="center">
                    选项值
                </td>
                <td align="center">
                    默认显示
                </td>
                <td align="center">
                    是否启用
                </td>
                <td align="center">
                    操作
                </td>
            </tr>
            <asp:Repeater ID="repeater1" runat="server" OnItemCommand="repeater1_ItemCommand">
                <ItemTemplate>
                    <tr style="width: 500px">
                        <td align="center">
                            <asp:Label runat="server" ID="nodeSort" Text='<%# Eval("sort")%>'></asp:Label>
                        </td>
                        <td align="center">
                            <asp:TextBox ID="TextBox1" class="l_input" runat="server" Text='<%# Eval("content")%>' Style="text-align: center"></asp:TextBox>
                        </td>
                        <td align="center">
                            <input type="radio" id="raa<%# Eval("sort")%>" name="raa" value="<%# Eval("sort")%>"<%#Eval("default_","{0}")=="True"?"checked":""%> />
                        </td>
                        <td align="center">
                            <asp:CheckBox ID="enableCK" runat="server" Checked='<%#Eval("enable")%>' />
                        </td>
                        <td align="center">
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Del" CommandArgument='<%#Eval("sort") %>' OnClientClick="return confirm('你确定要删除吗!');" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
        <asp:Button ID="Button4" runat="server" Text="新增" class="btn btn-primary" OnClick="Button4_Click" CausesValidation="false" />
        <asp:Button ID="Button1" runat="server" Text="保存" OnClick="Save_Click" class="btn btn-primary" />
        <asp:TextBox ID="tex" runat="server" Style="display: none" />
        <br />
        <div id="Panel1" runat="server" visible="false">
            <table class="table table-striped table-bordered table-hover">
                <tr class="gridtitle">
                    <td colspan="2" align="center" style="height: 25px;">
                        <asp:Label ID="Tit0" runat="server" Text="添加区域"></asp:Label>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right">
                        选 项 值：
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txt_content" runat="server" CssClass="l_input"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="选项值不能为空！" ControlToValidate="txt_content"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right">
                        默认显示：
                    </td>
                    <td align="left">
                        <input type="radio" id="ra1" name="ra" value="True" checked /><label for="ra1">是</label>
                        <input type="radio" id="ra2" name="ra" value="False" /><label for="ra2">否</label>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right">
                        是否启用：
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="txt_enable" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="Button2" runat="server" Text="添加" class="btn btn-primary" OnClick="Button2_Click" />
                        <asp:Button ID="Button3" runat="server" Text="取消" class="btn btn-primary" CausesValidation="false" OnClick="Button3_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
        <script type="text/javascript" src="/js/Common.js"></script>
</asp:Content>
