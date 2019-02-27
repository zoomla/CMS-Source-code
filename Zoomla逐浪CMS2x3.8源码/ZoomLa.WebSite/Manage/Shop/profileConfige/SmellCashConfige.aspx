<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SmellCashConfige.aspx.cs" Inherits="manage_Shop_profileConfige_SmellCashConfige" EnableViewStateMac="false"  MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>小额兑现信息配置</title>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" align="center">
                <asp:Literal ID="LTitle" runat="server" Text="修改小额兑现信息配置"></asp:Literal>
            </td>
        </tr>
        <tr class="tdbg">
            <td valign="top" style="margin-top: 0px; margin-left: 0px;">
              <table class="table table-striped table-bordered table-hover">
                 <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td class="tdbgleft"><strong>返利金额：</strong></td>
                    <td>
                        <asp:TextBox ID="txtAcou" runat="server"  class="form-control pull-left"></asp:TextBox>(即最少返利金额)
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ErrorMessage="返利金额不能为空!" ControlToValidate="txtAcou"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                    <td class="tdbgleft"><strong>兑现金额：</strong></td>
                    <td>
                        <asp:TextBox ID="txtCash" runat="server"  class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ErrorMessage="兑现金额不能为空!" ControlToValidate="txtCash"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
                   <td class="tdbgleft"><strong>注意事项：</strong></td>
                   <td>
                    <textarea id="info" class="form-control" style="width:100%;max-width:580px;" name="infoeditor" rows="10" runat="server"></textarea>
                        <%=Call.GetUEditor("info",4) %>
                    </td>
                 </tr>
             </table>
            </td>
        </tr>
    </table>
    <table id="TABLE1">
        <tr>
            <td align="left" style="height: 59px">
                &nbsp; &nbsp;
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="修改" runat="server"  onclick="EBtnSubmit_Click" />
                <input type="button" class="btn btn-primary" name="Button2" value="返回列表" onclick="location.href = '../profile/HonorManage.aspx'" id="Button2" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
</asp:Content>
