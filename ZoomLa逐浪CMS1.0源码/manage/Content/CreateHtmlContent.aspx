<%@ Page Language="C#" AutoEventWireup="true" enableEventValidation="false" CodeFile="CreateHtmlContent.aspx.cs" Inherits="ZoomLa.WebSite.Manage.CreateHtmlContent" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>发布管理</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
    <!-- 
    /* Button 样式 */
.btn
{
	padding: 1px 1px 1px 1px;
	font-size: 12px;
	height: 22px;
	background: url(../images/btnbg.gif);
	border-right: #999999 1px solid;
	border-top: #999999 1px solid;
	border-left: #999999 1px solid;
	border-bottom: #999999 1px solid;
}
 -->
</style>
    <script type="text/javascript" src="../js/RiQi.js"></script>
	<script language="javascript" type="text/javascript" src="../JS/Common.js"></script>
    <script language="javascript" type="text/javascript">
           
           function myfun()
           {
                if(document.form1.ColumnList.value=="")
                {
                    alert("请选择栏目");
                    return false;
                }
           }
           
           function lbColumnCheck()
           {
                if(document.form1.lbColumn.value=="")
                {
                    alert("请选择栏目");
                    return false;
                }                
           }
           
           function lbSingleCheck()
           {
                if(document.form1.lbSingle.value=="")
                {
                    alert("请选择单页");
                    return false;
                } 
           }
           function lbSpeacilCheck()
           {
                if(document.form1.lbSpeacil.value=="")
                {
                    alert("请选择专题");
                    return false;
                }  
           }

		 function ShowCreate(result)
		{
			HideCreate();
			result.style.display="";
		}

		function HideCreate()
		{
			for(i=1;i<5;i++)
			{
				eval("crt"+i).style.display="none";	
			}
		}		
    </script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>内容管理</span> &gt;&gt;<span>生成发布</span>
	</div>
	<div class="clearbox"></div>
    <div class="divline">
		<ul style="cursor:hand;">
            <li onclick="ShowCreate(crt1)">发布主页与内容</li>
            <li onclick="ShowCreate(crt3)">发布单页</li>
            <li onclick="ShowCreate(crt2)">发布栏目</li>
            <li onclick="ShowCreate(crt4)">发布专题</li>
        </ul>
	</div>
    <div class="clearbox"></div>
    <!--发布全站主页-->
    <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" align="center">
        <tr class="title"><td colspan="2" align="left">&nbsp;发布站点主页</td></tr>
        <tr class="tdbg">
            <td align="right" width="20%">&nbsp;发布站点主页：</td>
            <td  align="left" class="bqright">
            <asp:Button ID="btnCreate" runat="server" Text="开始发布" CssClass="btn" OnClick="btnCreate_Click" />
            </td>
        </tr>
    </table>
    <!--end发布全站主页-->
    <div class="clearbox"></div>
    
    <!--top生成内容页-->
        <table width="100%" border="0" cellpadding="0" cellspacing="1" class="border" align="center" id="crt1">
            <tr class="title"><td colspan="2" align="left">&nbsp;发布内容</td></tr>
            
            <tr class="tdbg">
                <td align="right" style="height: 24px">&nbsp;发布所有：</td>
                <td align="left" class="bqright" style="height: 24px">
                <asp:Button ID="btnNewsContent" runat="server" Text="开始发布" CssClass="btn" OnClick="btnNewsContent_Click" />
                </td>
            </tr>
            
            <tr class="tdbg"><td align="right"  class="bqleft">&nbsp;按照ID发布：</td>
                <td align="left" class="bqright">
                    从<asp:TextBox ID="txtIdStart" runat="server"></asp:TextBox>到 
                    <asp:TextBox ID="txtIdEnd" runat="server"></asp:TextBox> 
                    <asp:Button ID="btnCreateId" runat="server" Text="开始发布" CssClass="btn" OnClick="btnCreateId_Click" />
                </td>
            </tr>
            
            <tr class="tdbg">
                <td align="right">发布最新：</td>
                <td class="bqright">
                <asp:TextBox ID="txtNewsCount" runat="server"></asp:TextBox>个 
                <asp:Button ID="btnNewsCount" runat="server" Text="开始发布" CssClass="btn" OnClick="btnNewsCount_Click" /></td>
            </tr>
            
            <tr class="tdbg">
                <td align="right">按照日期发布：</td>
                <td class="bqright">
                    从<asp:TextBox ID="txtStartDate" runat="server" onblur="setday(this);" onclick="setday(this);"></asp:TextBox>到
                    <asp:TextBox ID="txtEndDate" runat="server" onblur="setday(this);" onclick="setday(this);"></asp:TextBox> 
                    <asp:Button ID="Button2" runat="server" Text="开始发布" CssClass="btn" OnClick="Button2_Click" />
                </td>
            </tr>            
            <tr class="tdbg">
                <td align="right" valign="middle">按照栏目发布：</td>
                <td class="bqright" valign="middle" style="padding:0px;">                    
                    <div style="float:left;"><asp:ListBox ID="ColumnList" runat="server" Width="200" Height="220" SelectionMode="Multiple"></asp:ListBox></div> 
                    <div style="margin-top:100px; float:left;">&nbsp;<asp:Button ID="btnColumnCreate" runat="server" Text="开始发布" CssClass="btn" OnClick="btnColumnCreate_Click" OnClientClick="return myfun()" /></div>
                </td>
            </tr>
            <tr class="tdbg">
                <td align="left" colspan=2 valign="middle"><span style="color: Blue"><strong>注意：</strong></span>如果选择了栏目，则只生成选择栏目下的内容；如果不选择直接生成的，则生成全站的内容。</td>                
            </tr>
        </table> 
               
        <!--end生成内容页-->       
        
        <!--start发布栏目-->
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" align="center" id="crt2" style="display:none;">
            <tr class="title"><td colspan="2" align="left" style="height: 23px">&nbsp;发布栏目</td></tr>
            
			<tr class="tdbg">
				<td align="right">选择栏目：</td>
				<td class="bqright"><asp:Button ID="btnCreateColumnAll" runat="server" Text="发布所有栏目" CssClass="btn" OnClick="btnCreateColumnAll_Click" /></td>
			</tr>
            
            <tr class="tdbg">
                <td align="right" valign="middle">选择栏目：</td>
                <td class="bqright" valign="middle" style="padding:0px;">
                    <div style="float:left;"><asp:ListBox ID="lbColumn" runat="server" Width="200" Height="220" SelectionMode="Multiple"></asp:ListBox></div> 
                    <div style="margin-top:100px; float:left;">&nbsp;<asp:Button ID="btnCreateColumn" runat="server" Text="开始发布" CssClass="btn" OnClientClick="return lbColumnCheck()" OnClick="btnCreateColumn_Click" /></div>
                </td>
            </tr>
        </table>
        <!--end发布栏目-->
        
        <!--start发布单页-->
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" align="center" id="crt3" style="display:none;">
            <tr class="title"><td colspan="2" align="left">&nbsp;发布单页</td></tr>
            
			<tr class="tdbg">
				<td align="right">发布所有单页：</td>
				<td class="bqright"><asp:Button ID="Button1" runat="server" Text="发布所有单页" CssClass="btn" OnClick="btnCreateSingleAll_Click" /></td>
			</tr>
            
            <tr class="tdbg">
                <td align="right" valign="middle">选择单页：</td>
                <td class="bqright" valign="middle" style="padding:0px;">
                    <div style="float:left;"><asp:ListBox ID="lbSingle" runat="server" Width="200" Height="220" SelectionMode="Multiple"></asp:ListBox></div> 
                    <div style="margin-top:100px; float:left;">&nbsp;<asp:Button ID="Button3" runat="server" Text="开始发布" CssClass="btn" OnClientClick="return lbSingleCheck()" OnClick="btnCreateSingle_Click" /></div>
                </td>
            </tr>
        </table>
        <!--end发布单页-->
        <div class="clearbox"></div>
        <!--start发布主题-->
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="border" align="center" id="crt4" style="display:none;">
            <tr class="title"><td colspan="2" align="left">&nbsp;发布专题</td></tr>
       
            <tr class="tdbg">
                <td align="right" valign="middle">选择专题：</td>
                <td class="bqright" valign="middle" style="padding:0px;">
                    <div style="float:left;"><asp:ListBox ID="lbSpeacil" runat="server" Width="200" Height="220" SelectionMode="Multiple"></asp:ListBox></div> 
                    <div style="margin-top:100px; float:left;">&nbsp;<asp:Button ID="btnCreateSpeacil" runat="server" Text="开始发布" CssClass="btn" OnClientClick="return lbSpeacilCheck()" OnClick="btnCreateSpeacil_Click" /></div>
                </td>
            </tr>
        </table>        
        <!--end发布主题--> 
    
    <asp:Literal ID="litMessage" runat="server"></asp:Literal>
    </form>
</body>
</html>
