<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTemplateSet.aspx.cs" Inherits="Manage_I_Content_AddTemplateSet" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>方案设置</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table style="width: 100%; margin: 0 auto;" cellpadding="2" cellspacing="1" class="table table-striped table-bordered table-hover">
        <tr align="center" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td colspan="2" class="spacingtitle">
                <asp:Label ID="LblTitle" runat="server" Text="发布当前站点方案" Font-Bold="True"></asp:Label></td>
        </tr>
        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td class="tdbgleft" align="right" style="width: 105px"><strong>方案名称：&nbsp;</strong></td>
            <td align="left">
                <asp:TextBox ID="proname" runat="server" class=" form-control" Width="220px"></asp:TextBox></td>
        </tr>
        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td class="tdbgleft" align="right" style="width: 105px"><strong>作者：&nbsp;</strong></td>
            <td align="left">
                <asp:TextBox ID="txtAuthor" runat="server" class="form-control" Width="220px"></asp:TextBox></td>
        </tr>
        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td class="tdbgleft" align="right" style="width: 105px"><strong>授权方式：&nbsp;</strong></td>
            <td align="left">
                <asp:RadioButtonList ID="protype" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True" Value="1">付费</asp:ListItem>
                    <asp:ListItem Value="0">免费</asp:ListItem>
                </asp:RadioButtonList></td>
        </tr>
        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td class="tdbgleft" align="right" style="width: 105px"><strong>模板目录：&nbsp;</strong></td>
            <td align="left">
                <asp:DropDownList ID="tempdirlist" CssClass="form-control" Width="150" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
            <td class="tdbgleft" align="right" style="width: 105px"><strong>排序：&nbsp;</strong></td>
            <td align="left">
                <asp:TextBox ID="ornum" runat="server" Width="50" Columns="5" class=" form-control pull-left">0</asp:TextBox>
                <span class="tips" style="color: blue">数字越大越高排在越前</span></td>
        </tr>
        <tr class="tdbgbottom">
            <td colspan="2" class="text-center">
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" runat="server" OnClick="EBtnSubmit_Click" />
                <input name="Cancel" type="button" class="btn btn-primary" id="Cancel" value="取消" onclick="javascript: window.location.href = 'TemplateSet.aspx'" />
            </td>
        </tr>
    </table>
</asp:Content>
