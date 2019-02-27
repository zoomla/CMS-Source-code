<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddUAgent.aspx.cs" Inherits="ZoomLaCMS.Manage.AddOn.AddUAgent" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="Head">
<link type="text/css" href="/dist/css/bootstrap-switch.min.css"  rel="stylesheet"/>
<script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
<title>设置设备</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content" >
<table width="100%" border="0" cellpadding="2" cellspacing="1"  class="table table-bordered table-hover table-striped" style="margin: 0 auto;">
<tbody id="Tabs0">
    <tr class="spacingtitle" style="height:30px;">
		<td align="center" colspan="2" ><asp:Label ID="lblText" runat="server"><%=lang.LF("添加自适配设备")%></asp:Label></td>
    </tr>  
    <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
        <td style="width: 180px" class="tdbgleft"><strong>设备名称：</strong></td>
        <td>
            <asp:TextBox ID="TxtHeaders" class="form-control" runat="server" Width="200"   /><span style="color: #ff0066;">*</span><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtHeaders" ErrorMessage="设备名称"></asp:RequiredFieldValidator> 
            <asp:Label ID="LblMessage" runat="server" Text=""></asp:Label>
        </td>
    </tr>    
    <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
        <td style="width: 180px" class="tdbgleft"><strong>Agent关键词：</strong></td>
        <td>
            <asp:TextBox ID="TxtUserAgent" class="form-control" runat="server" Width="200"  /><span style="color: #ff0066;">*</span><span class="tips" style="color: #808000;"> 根据此关键词判断Url目标地址</span><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtUserAgent" ErrorMessage="Agent关键词" ></asp:RequiredFieldValidator> 
           <span style="color:#808000;"><a style="color:#f00;" href="http://bbs.z01.com/showtopic-192284.aspx" title="关键字参照" target="_blank">关键字参照</a></span>
        </td>
    </tr>  
     <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
        <td style="width: 180px" class="tdbgleft"><strong>Url目标地址：</strong></td>
        <td>
            <asp:TextBox ID="TxtUrl" class="form-control" runat="server" Width="200" /><span style="color: #ff0066;">* </span><span class="tips" style="color: #808000;">注：外部地址请加上"http://" </span><asp:RequiredFieldValidator ID="ReqFv" runat="server" ControlToValidate="TxtUrl" ErrorMessage="请录入Url目标地址" ></asp:RequiredFieldValidator> 
        </td>
    </tr>  
     <tr class="tdbg" onmouseover="this.className='tdbgmouseover'" onmouseout="this.className='tdbg'">
        <td style="width: 180px" class="tdbgleft"><strong>启用：</strong></td>
        <td>
             <input runat="server" type="checkbox" id="Status" class="switchChk" checked="checked" />
        </td>
    </tr>
</table>
        <div class="clearbox"></div>
<div style=" text-align:center; width:800px"><asp:Button ID="BtnCommit" runat="server" Text="提交"  class="btn btn-primary" onclick="Button1_Click"/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <input type="button"  value="返回" class="btn btn-primary" onclick="location.href = 'UAgent.aspx';"
</div> 
</asp:Content>