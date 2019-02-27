<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Balance.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.Balance" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>推广</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
     <div style="width: 100%; text-align: center; margin-left: auto;
        margin-right: auto;">
        <asp:HiddenField ID="hfId" runat="server" />
        <br />
        <table  class="table table-striped table-bordered table-hover">
            <tr>
                <td colspan="2" width="30%" height="28" align="center" >
                    结算&nbsp;
                </td>
            </tr>
            <tr>
                <td width="45%" height="28" align="right" >
                    推广用户&nbsp;
                </td>
                <td width="70%" align="center" class="tdbg" style="color:Black;font-weight:normal">
                    &nbsp;<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="30%" height="28" align="right" >
                    总额&nbsp;
                </td>
                <td width="70%" height="28" align="center" class="tdbg" style="color:Black;font-weight:normal">
                    &nbsp;<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="30%" height="28" align="right" >
                    返利比例&nbsp;
                </td>
                <td width="70%" height="28" align="center" class="tdbg" style="color:Black;font-weight:normal">
                    &nbsp;<input ID="Label3" type="text" runat="server" style="width:35px; height:15px;" />%<asp:Label
                        ID="Label5" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <td width="30%" height="28" align="right" >
                    返利金额&nbsp;
                </td>
                <td width="70%" height="28" align="center" class="tdbg" >
                   <asp:Label ID="label4" Text="" runat="server"></asp:Label>元
                </td>
            </tr>
            <tr>
                <td colspan="2" width="30%" height="28" align="center"  class="tdbg" style="color:Black;background-color:White;">
                <asp:Button ID="Button2" runat="server" Text="确定" title="结算" 
            class="C_input" style=" width:100px;" onclick="Button2_Click" />
                <asp:Button ID="Button1" runat="server" Text="取消" title="关闭" 
            class="C_input" style=" width:100px;" OnClientClick="Dialog.close();" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript" src="/js/Drag.js"></script>
    <script type="text/javascript" src="/js/Dialog.js"></script>
    <script>
        function gotourl(url) {
            try {
                parent.MDILoadurl(url); void (0);
            } catch (Error) {
                parent.frames["main_right"].location = "../" + url; void (0);
            }
        }
    </script>
</asp:Content>