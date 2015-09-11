<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Worktable.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Worktable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>工作台首页</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
<script language="javascript" type="text/javascript">
// <!CDATA[

function TABLE1_onclick() {

}

// ]]>
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>工作台</span> &gt;&gt;<span>管理员首页</span>
	</div>
    <div class="clearbox"></div>
    <table class="border" cellspacing="1" cellpadding="2" width="100%" align="center" border="0">
        <tbody>
            <tr class="title" height="25px">
                <td>
                    欢迎使用Zoomla!逐浪CMS网站管理系统(版本号:1.0)
               </td>
            </tr>            
        </tbody>
    </table>
    <div class="clearbox"></div>
    <table class="border" cellspacing="0" cellpadding="2" width="100%" align="center" border="0">
        <tr class="title">
            <td height="25" align=left colspan="2">&nbsp;<b>信息记录</b></td>
        </tr>
        <tr class="sysinfo"  height="0">
            <td  colspan="2">
                <strong><asp:Literal runat="server" ID="litUserName"></asp:Literal></strong>&nbsp;您好！  今天是：<asp:Literal runat="server" ID="litDate"></asp:Literal></td>
        </tr>            
    </table>
    <div class="clearbox"></div>
    <table id="ShortCutTable" class="border" cellspacing="1" width="100%" cellpadding="2" align="center" border="0">
        <tbody>
            <tr class="title">
                <td align="left"  style="height: 25px">&nbsp;<b>系统帮助信息</b></td>
            </tr>
               <tr> <td style="height: 22px"> 如果您是第一次使用本系统，请按以下步骤配置：</td></tr>
               <tr>
                <td style="height: 22px">                
	            <a href="../Config/SiteInfo.aspx"><img src="../Images/config.png" border="0" alt="网站配置" /></a>&nbsp;&nbsp;
	            <a href="../Content/ModelManage.aspx"><img src="../Images/generic.png" border="0" alt="内容模型管理" /></a>&nbsp;&nbsp;
	            <a href="../Content/NodeManage.aspx"><img src="../Images/cpanel.png" border="0" alt="节点设置" /></a>&nbsp;&nbsp;
	            <a href="../Template/LabelManage.aspx"><img src="../Images/mediamanager.png" border="0" alt="标签设置" /></a>&nbsp;&nbsp;
	            <a href="../Template/TemplateManage.aspx"><img src="../Images/templatemanager.png" border="0" alt="模板设置" /></a>&nbsp;&nbsp;
	            <a href="../User/AdminManage.aspx"><img src="../Images/support.png" border="0" alt="管理员管理" /></a>&nbsp;&nbsp;
                </td>                   
               </tr>               
               <tr><td style="height: 23px">其他帮助资源：<a href="http://bbs.zoomla.cn" target="_blank"><font color="red">帮助文档</font></a> |  <a href="http://bbs.zoomla.cn" target=_blank><font color=red>官方论坛<span style="color: #000000"> | </span></a> <a href="http://www.zoomla.cn" target=_blank>
          <font color=red>官方网站</font></a><span style="color: #000000"> | </span><a href="http://www.hx008.cn" target="_blank" title="购买主机域名部署逐浪"><font color=red>主机域名</font></a></td></tr>
            
            
        </tbody>
    </table>    
    <div class="clearbox"></div>
    <table class="border" cellspacing="1" cellpadding="2" width="100%" align="center"
        border="0" id="TABLE1" onclick="return TABLE1_onclick()">
        <tbody>
            <tr class="title" height="25px">
                <td colspan="4" align=left>
                    <b>&nbsp;联系我们</b></td>
            </tr>
            <tr class="sysinfo">
                <td height="20">
                    <div align="center">
                        产品开发</div>
                </td>
                <td height="20">
                    逐浪软件(<a href="http://www.hx008.cn/corp/">江西聚合实业有限公司</a>)</td>
                <td>
                    <div align="center">
                        定制开发</div>
                </td>
                <td>
                    QQ:544472213&nbsp; &nbsp;69250566&nbsp; &nbsp;779630567</td>
            </tr>
            <tr class="sysinfo">
                <td width="13%" height="20">
                    <div align="center">
                        总机电话</div>
                </td>
                <td width="30%" height="20">
                   0791-8303707</td>
                <td width="12%">
                    <div align="center">
                        产品咨询</div>
                </td>
                <td width="45%">
                    TEL：0791-8303707(24H)</td>
            </tr>
            <tr class="sysinfo">
                <td width="13%" style="height: 24px">
                    <div align="center">
                        传 真</div>
                </td>
                <td width="30%" style="height: 24px">
                    0791-7183714-806</td>
                <td width="12%" style="height: 24px">
                    <div align="center">
                        客服电话</div>
                </td>
                <td width="45%" style="height: 24px">
                    0791-7532723-802</td>
            </tr>
            <tr class="sysinfo">
                <td width="13%" height="20">
                    <div align="center">
                        官方网站</div>
                </td>
                <td width="30%" height="20">
                    <a href="http://www.zoomla.cn/">www.zoomla.cn</a></td>
                <td width="12%">
                    <div align="center">
                        帮助中心</div>
                </td>
                <td width="45%">
                    <a href="http://bbs.zoomla.cn/" target="_blank">bbs.zoomla.cn</a></td>
            </tr>
            <tr class="sysinfo">
                <td colspan="4" height="20">
                    <div align="center">
                        &copy;  2008-2010 <a href="http://www.zoomla.cn">逐浪软件</a>  Inc. All Rights Reserved</div>
                </td>
            </tr>
        </tbody>
    </table>
    
    </form>
</body>
</html>
