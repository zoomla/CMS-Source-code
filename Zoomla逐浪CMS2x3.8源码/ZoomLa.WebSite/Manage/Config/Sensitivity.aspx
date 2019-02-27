<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sensitivity.aspx.cs" Inherits="Manage_I_Config_Sensitivity" MasterPageFile="~/Manage/I/Default.master" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>敏感词汇</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table width="99%" cellspacing="1" cellpadding="0" class="table table-striped table-bordered table-hover">
        <td class="spacingtitle" colspan="2" align="center"><%:lang.LF("导入敏感词汇") %></td>
    <tr class="tdbg">
        <td class="tdbgleft" style="width: 200px"><strong><%:lang.LF("选择库文件") %>：(<%:lang.LF("txt文件") %>)</strong></td>
        <td height="50px">&nbsp;
            <ZL:FileUpload  ID="InputTxt" CssClass="l_input" runat="server" Width="379px" AllowExt="txt" />
        <%--<asp:FileUpload ID="InputTxt" CssClass="l_input" runat="server" Width="379px" />--%></td>
    </tr>
    <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
        <td  style="width: 200px"></td>
        <td class="tdbg">&nbsp;
        <asp:Button ID="Button1" runat="server" Text="导入列表" CssClass="btn btn-primary" OnClick="Button1_Click" />
        <asp:Button ID="Button6" runat="server" Text="导出列表" CssClass="btn btn-primary"  onclick="Button6_Click" /></td>
    </tr>
    </table>
    <ZL:ExGridView ID="Egv" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" PageSize="10" OnPageIndexChanging="Egv_PageIndexChanging" IsHoldState="false" OnRowCommand="Egv_RowCommand" AllowPaging="True" AllowSorting="True" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EnableModelValidation="True" EmptyDataText="无相关数据">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField HeaderText="ID" DataField="ID" />
            <asp:BoundField HeaderText="词汇" DataField="keyname" />
            <asp:TemplateField HeaderText="状态">
                <ItemTemplate>
                    <%#Eval("istrue","{0}")=="0"?"<font color=red>停用</font>":"<font color=green>启用</font>"%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="操作">
                <ItemTemplate>
                    <a href="AddSensitivity.aspx?menu=edit&id=<%#Eval("id") %>" class="option_style"><i class="fa fa-pencil" title="修改"></i></a>
                    <asp:LinkButton ID="LinkButton1" CommandName="Del1" OnClientClick="return confirm('你确定将该数据删除到回收站吗？')" CommandArgument='<%# Eval("ID") %>' runat="server" CssClass="option_style"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <PagerStyle HorizontalAlign="Center" />
    </ZL:ExGridView>
    <div class="text-center">
        <asp:Button ID="Button3" runat="server" Text="批量删除" CssClass="btn btn-primary"  onclick="Button3_Click" OnClientClick="return confirm('你确定将该数据删除到回收站吗？')" />
        <asp:Button ID="Button5" runat="server" Text="批量启用" CssClass="btn btn-primary"  onclick="Button5_Click" />
        <asp:Button ID="Button4" runat="server" Text="批量停用" CssClass="btn btn-primary"   onclick="Button4_Click" />
    </div>
    <script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
    <script type="text/javascript">
        
        $().ready(function () {
            
        })
    </script>
</asp:Content>