<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="CollectionStatus.aspx.cs" Inherits="Manage_I_Content_CollectionStatus" ClientIDMode="Static"  ValidateRequest="false" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <title>采集状态</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
	    <ContentTemplate>
		    <asp:Timer ID="Timer1" runat="server" Interval="2000" OnTick="Timer1_Tick"></asp:Timer>
		    <div class="clearbox">
		    </div>
		    <table cellpadding="2" cellspacing="1" class="border" style="background-color: white;" width="100%">
			    <tr class="tdbg">
				    <td align="center" class="title" colspan="2" height="24" width="100%">
					    <asp:Label ID="Label1" runat="server" Text="采集详细状态"></asp:Label>
				    </td>
			    </tr>
			    <tr class="tdbg" runat="server" id="Tr2" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
				    <td align="left" height="24">
					    <asp:TextBox ID="TextBox1" runat="server" class="l_input" Height="300px" TextMode="MultiLine" Width="100%"></asp:TextBox>
					    [提示] 系统已启动多线程模式，您可以离开本页面继续其他操作，采集任务将自动完成...<br />
					
					     <asp:Button ID="Button1" runat="server" Text="停止采集" OnClick="Button1_Click" CssClass="btn btn-primary"  style="width:110px;" />
				    </td>
			    </tr>
		    </table>
	    </ContentTemplate>
    </asp:UpdatePanel>
    <div style="border:1px solid #ddd;margin-top:10px;">
        <label>帮助说明：采集完成后,会返回以下三种状态</label><br />
        <span>信息为空：网址不正确,或目标页面不存在</span><br />
        <span>采集成功：正常采集,并添加入数据库</span><br />
        <span>采集异常：取值异常,具体见报错信息</span><br />
    </div>
</asp:Content>