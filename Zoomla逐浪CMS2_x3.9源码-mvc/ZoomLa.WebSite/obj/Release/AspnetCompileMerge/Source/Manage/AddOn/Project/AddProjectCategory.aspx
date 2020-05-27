<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddProjectCategory.aspx.cs"  MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLaCMS.Manage.AddOn.Project.AddProjectCategory" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>添加项目分类</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered">
    <tr align="center">
        <td colspan="2" class="spacingtitle">
            <asp:Label ID="LblTitle" runat="server" Text="创建项目分类" Font-Bold="True"></asp:Label>
            </td>
    </tr>
    <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
        <td class="tdbgleft" align="right" style="width: 105px">
            <strong>分类名称：&nbsp;</strong></td>
        <td class="tdbg" align="left">
            <asp:TextBox ID="TxtProCateName" runat="server" class="form-control"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ValrKeywordText" ControlToValidate="TxtProCateName"
                runat="server" ErrorMessage="分类名称不能为空！" Display="Dynamic"></asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
        <td class="tdbgleft" align="right" style="width: 105px">
            <strong>分类简述：&nbsp;</strong></td>
        <td class="tdbg" align="left">
            <asp:TextBox ID="TxtProCateIntro" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="8" 
                Columns="50" class="l_input" Height="89px"></asp:TextBox>                
        </td>
    </tr>       
    <tr class="tdbgbottom">
        <td colspan="2">
          <asp:Button ID="EBtnModify" Text="修改" OnClick="EBtnModify_Click" runat="server" Visible="false" class="btn btn-primary"/>
            <asp:Button ID="EBtnSubmit" Text="保存" OnClick="EBtnSubmit_Click" runat="server" class="btn btn-primary"/>&nbsp;&nbsp;
            <input name="Cancel" type="button"  id="Cancel" value="取消" runat="server" onclick="javascript:window.location.href='ProjectCategoryManage.aspx'" class="btn btn-primary"/>
        </td>
    </tr>        
</table>
</asp:Content>

