<%@ Page Language="C#" AutoEventWireup="true" validateRequest="false"  CodeFile="Advertisement.aspx.cs" Inherits="ZoomLa.WebSite.Manage.AddOn.Advertisement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>添加广告</title>
    <link href="../../App_Themes/AdminDefaultTheme/Guide.css" type="text/css" rel="stylesheet" />
    <link href="../../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />    
    <link href="../../App_Themes/AdminDefaultTheme/main.css" type="text/css" rel="stylesheet" />
    <script src="../JS/Common.js" type="text/javascript"></script>
    <script src="../JS/RiQi.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="r_navigation">
		<div class="r_n_pic"></div>
		<span>后台管理</span>&gt;&gt;<span>附件管理</span> &gt;&gt;<span><a href="ADManage.aspx" title="网站广告管理">广告管理</a></span> &gt;&gt;<span>添加广告</span>
	    </div> 
        <div class="clearbox"></div>
                                         


    <table border="0" cellpadding="2" cellspacing="1" class="border" width="100%">
      <tr valign="middle">
        <td class="spacingtitle" colspan="2" style="height: 26px; text-align:center"><strong><asp:Label ID="Label1" runat="server" Text="添加广告"></asp:Label></strong></td></tr>
      <tr valign="middle" class="tdbg">
        <td class="tdbg" valign="top" style="width: 159px; height:auto ;">
            <table border="0" cellpadding="2" cellspacing="1" width="100%">
                <tr>
                    <td valign="middle" class="tdbgleft"  style="width:20%;">
                        <b>所属版位</b></td>
                </tr>
                <tr>
                    <td style="width: 80%">
                        <asp:ListBox ID="LstZoneName" runat="server" Height="313px" Width="159px" SelectionMode="multiple">
                </asp:ListBox></td>
                </tr>
            </table>
        </td>
        <td valign="top"  class="tdbgleft" align="left" style="height: auto " width="100%">
            <table border="0" cellpadding="2" cellspacing="1" style="width: 100%">
                <tr class="tdbg">
                    <td align="right" class="tdbgleft"  style="width: 20%;">
                        <strong>广告名称：</strong></td>
                    <td align="left" class="tdbgleft"  style="width: 80%; color: red;">
                        <asp:TextBox ID="TxtADName" Width="400px" MaxLength="150" runat="server"></asp:TextBox>*
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="广告名称不能为空！" ControlToValidate="TxtADName" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right"  class="tdbgleft" style="width: 20%">
                        <strong>广告类型：</strong></td>
                    <td align="left" style="width: 80%;">
                        <asp:RadioButtonList id="RadlADType" AutoPostBack="False" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server">
                            <asp:ListItem Selected="True" Value="1">图片</asp:ListItem>
                            <asp:ListItem Value="2">动画</asp:ListItem>
                            <asp:ListItem Value="3">文本</asp:ListItem>
                            <asp:ListItem Value="4">代码</asp:ListItem>
                            <asp:ListItem Value="5">页面</asp:ListItem>
                        </asp:RadioButtonList></td>
                </tr>
                <tr class="tdbg">
                    <td align="right" style="width: 20%;" class="tdbgleft">
                        <strong>广告内容：</strong></td>
                    <td align="left" style="width: 80%" valign="top">
                       <div id="ADContent" runat="server">
                       <!-- 广告内容设置--图片 -->
                       <table id="ADContent1" border="0" cellpadding="2" cellspacing="1" width="100%" runat="server">
                            <tr valign="middle" class="tdbg2">
                                <td colspan="2" style="text-align: center; height: 22px;">
                                    <strong>广告内容设置--图片</strong></td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right" style="width: 20%; height: 55px;" class="tdbgleft">
                                    图片上传：</td>
                                <td style="width:352px; height: 55px; color: red;">
                                    <asp:TextBox ID="txtpic" runat="server" Width="194px" Height="19px"></asp:TextBox>*<asp:Label
                                        ID="LabPicPath" runat="server" Text="请选择上传路径！" Visible="False"></asp:Label><br />
                                    <iframe id="Upload_Pic" src="../Common/Upload.aspx?CID=pic&ftype=1" marginheight="0" marginwidth="0" frameborder="0" width="100%" height="30" scrolling="no"></iframe>
                                    </td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right" style="width: 20%;" class="tdbgleft">
                                    图片尺寸：</td>
                                <td style="width: 352px;">
                                    宽：<asp:TextBox ID="TxtImgWidth" MaxLength="5" Style="width: 40px" runat="server"></asp:TextBox>
                                    像素 &nbsp;  高：<asp:TextBox ID="TxtImgHeight" MaxLength="5" Style="width: 40px" runat="server"></asp:TextBox>
                                    像素
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right" style="width:20%; height: 41px;" class="tdbgleft">
                                    链接地址：</td>
                                <td style="width: 352px; height: 41px;">
                                <asp:TextBox ID="TxtLinkUrl" Text="http://" runat="server" Width="341px"></asp:TextBox>                                
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right" style="width: 20%; text-align:  right " class="tdbgleft">
                                    链接提示：</td>
                                <td style="width: 352px; height: 16px">
                                    <asp:TextBox ID="TxtLinkAlt" runat="server" MaxLength="255" Width="341px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right" style="width: 20%" class="tdbgleft">
                                    链接目标：</td>
                                <td style="width: 352px">
                                    <asp:RadioButtonList ID="RadlLinkTarget" runat="server" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="0">原窗口</asp:ListItem>
                                        <asp:ListItem Value="1">新窗口</asp:ListItem>
                                    </asp:RadioButtonList></td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right" style="width: 71px;" class="tdbgleft">
                                    广告简介：</td>
                                <td style="width: 352px;">
                                    <asp:TextBox ID="TxtADIntro" runat="server" TextMode="MultiLine" Height="87px" Width="341px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <!-- 广告内容设置--动画 -->
                        <table id="ADContent2" border="0" cellpadding="2" cellspacing="1" width="100%" runat="server" style="display: none">
                            <tr valign="middle" class="tdbg2">
                                <td colspan="2" style="height: 23px; text-align: center;">
                                    <strong>广告内容设置--动画</strong></td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right" style="width: 104px; height: 49px;" class="tdbgleft">
                                    动画上传：</td>
                                <td style="width: 323px; height: 49px; color: red;">
                                    <asp:TextBox ID="txtFlashPath" runat="server"></asp:TextBox>
                                    *<asp:Label ID="LabFlashPath" runat="server" Text="请选择上传路径！" Visible="False"></asp:Label>
                                    <br />
                                    <iframe id="Upload_Flash" src="../Common/../Common/Upload.aspx?CID=FlashPath&ftype=1" marginheight="0" marginwidth="0" frameborder="0" width="100%" height="30" scrolling="no"></iframe>
                                    </td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right" style="width: 104px" class="tdbgleft">
                                    动画尺寸：</td>
                                <td style="width: 323px">
                                    宽：<asp:TextBox ID="TxtFlashWidth" runat="server" Style="width: 40px" MaxLength="5"></asp:TextBox>
                                    像素 &nbsp; &nbsp;高：<asp:TextBox ID="TxtFlashHeight" Style="width: 40px" runat="server" MaxLength="5"></asp:TextBox>
                                    像素
                                </td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right" colspan="1" style="width: 104px; height: 6px" class="tdbgleft">
                                    背景透明：</td>
                                <td align="right" colspan="2" style="height: 6px; width: 460px; text-align: left;">
                                                    <asp:RadioButtonList ID="RadlFlashMode" runat="server" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0" Selected="True">不透明</asp:ListItem>
                                                        <asp:ListItem Value="1">透明</asp:ListItem>
                                                    </asp:RadioButtonList></td>
                            </tr>
                        </table>
                        <!-- 广告内容设置--文本 -->
                        <table id="ADContent3" border="0" cellpadding="2" cellspacing="1" width="100%" runat="server" style="display: none">
                            <tr valign="middle" class="tdbg2">
                                <td colspan="2" style=" width: 20%; text-align: left;">
                                    <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 广告内容设置--文本</strong></td>
                            </tr>
                            <tr class="tdbg">
                                <td valign="middle" colspan="2" style="width: 80%;">
                                    <asp:TextBox ID="TxtADText" TextMode="multiLine" runat="server" Height="90px" Width="380px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <!-- 广告内容设置--代码 -->
                        <table id="ADContent4" border="0" cellpadding="2" cellspacing="1" width="100%" runat="server" style="display: none">
                            <tr valign="middle" class="tdbg2">
                                <td style="text-align: left">
                                    <strong>&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; 广告内容设置--代码</strong></td>
                            </tr>
                            <tr class="tdbg">
                                <td valign="middle" style="width: 100%">
                                    <asp:TextBox ID="TxtADCode" TextMode="multiLine" runat="server" Height="90px" Width="380px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <!-- 广告内容设置--页面 -->
                        <table id="ADContent5" border="0" cellpadding="2" cellspacing="1" width="100%" runat="server" style="display: none">
                            <tr valign="middle" class="tdbg2">
                                <td colspan="2" style="text-align: center">
                                    <strong>广告内容设置--页面</strong></td>
                            </tr>
                            <tr class="tdbg">
                                <td align="right" style=" width : 20%" class="tdbgleft">
                                    页面地址：</td>
                                <td style=" width:80%">
                                    <asp:TextBox ID="TxtWebFileUrl" class="inputtext" style="width: 300px; height: 90px" runat="server"></asp:TextBox>                                        
                                    <br />
                                    <span style="color: blue">注意：</span>页面地址需要加上http://
                                </td>
                            </tr>
                        </table>
                        </div>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" style="width: 827px" class="tdbgleft">
                        <strong>广告权重：</strong></td>
                    <td align="left" style="width: 1086px">
                        <asp:TextBox ID="TxtPriority" runat="server" TextMode="singleLine" MaxLength="3" Text="1" Style="width: 20px"></asp:TextBox>
                        * 此项为版位广告随机显示时的优先权，权重越大显示机会越大。
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right"  class="tdbgleft" style="width: 20%;">
                        <strong>广告统计：</strong></td>
                    <td align="left" style="width: 1086px; height: 42px;"> 
                        <asp:CheckBox ID="ChkCountView" runat="server" />
                        统计浏览数 浏览数：<asp:TextBox ID="TxtViews" MaxLength="5" runat="server" Width="32px" Text="0"></asp:TextBox><br />
                        <asp:CheckBox ID="ChkCountClick" runat="server" />
                        统计点击数 点击数：<asp:TextBox ID="TxtClicks" runat="server" MaxLength="5" Width="35px" Text="0"></asp:TextBox>
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft" style="width: 827px">
                        <strong>广告过期时间：</strong></td>
                    <td align="left" style="width: 1086px">
                        <asp:TextBox ID="txtOverdueDate" style="font-size: 9pt; height: 15px" maxlength="10" runat="server" Text="2008-10-19"></asp:TextBox>                        
                    </td>
                </tr>
                <tr class="tdbg">
                    <td align="right" class="tdbgleft" style="width: 827px; height: 24px">
                        <strong>审核状态：</strong></td>
                    <td align="left" style="width: 1086px; height: 24px">
                        <asp:CheckBox ID="ChkPassed" Checked="true" runat="server" />
                        通过审核
                    </td>
                </tr>
            </table>
         </td>
       </tr>
    </table>
    <table border="0" cellpadding="2" cellspacing="1" width="100%">
        <tr>
            <td valign="middle" colspan="2" style="width: 100%; height: 25px" align="center">
                <asp:Button ID="EBtnSubmit" runat="server" Text="保存" OnClientClick="javascript:return CheckForm();" OnClick="EBtnSubmit_Click" />          
                <input id="Cancel" class="inputbutton" name="Cancel" onclick="GoBack();" style="cursor: hand"
                    type="button" value="取消" /><asp:HiddenField ID="HdnID" runat="server" />
            </td>
        </tr>
    </table> 
    </form>
<script language="javascript" type="text/javascript">
    function CheckSelect()
    {
       var s=document.getElementById("<%= LstZoneName.ClientID %>");
       for(var i=0;i<s.length;i++)
       {
         if(s.options[i].selected)
         {
           return true;
         }
       }
       return false;
    }
    function CheckUploadFile()
    {
        if(document.getElementById("<%=RadlADType.ClientID %>_0").checked)
        {
            var s= document.getElementById("<%=txtpic.ClientID %>");
            if(s.value=="")
            {
                alert("还没有上传图片！");
                return false;
            }
        }
       if(document.getElementById("<%=RadlADType.ClientID %>_1").checked)
       {
         s=document.getElementById("<%=txtFlashPath.ClientID %>");
         if(s.value=="")
         {
            alert("还没有上传Flash");
            return false;
         }
       }
       return true;
    }
    function GoBack()
    {
       window.location.href="ADManage.aspx";
    }
    function Change_ADType()
{
  for (var j=1;j<=5;j++)
  {
	var ot = eval($("<%=ADContent.ClientID %>" + j));
	if($("<%=RadlADType.ClientID %>_"+(j-1)).checked)
	{
		ot.style.display = '';
		if(j==1)
		{
			$("<%=ChkCountClick.ClientID %>").disabled = false;
			$("<%=TxtClicks.ClientID %>").disabled = false;
		}
		else
		{
			$("<%=ChkCountClick.ClientID %>").disabled = true;
			$("<%=TxtClicks.ClientID %>").disabled = true;
		}
	}
	else
	{
		ot.style.display = 'none';
	}
  }
}
function ADTypeChecked(i)
{
	$("<%=RadlADType.ClientID %>_"+(i-1)).checked=true;
	Change_ADType();
}
function CheckForm()
{
	if($("<%=TxtADName.ClientID %>").value=='')
	{
		alert('广告名称不能为空！');
		$("<%=TxtADName.ClientID %>").focus();
		return false;
	}
	if(!CheckUploadFile())
	{
		return false;
	}
	if($("<%=RadlADType.ClientID %>_2").checked && $("<%=TxtADText.ClientID %>").value=='')
	{
		alert('广告文字不能空');
		$("<%=TxtADText.ClientID %>").focus();
		return false;
	}
	if($("<%=RadlADType.ClientID %>_3").checked && $("<%=TxtADCode.ClientID %>").value=='')
	{
		alert('广告代码不能空');
		$("<%=TxtADCode.ClientID %>").focus();
		return false;
	}
	if($("<%=TxtPriority.ClientID %>").value=='')
	{
		alert('权重系数不能空');
		$("<%=TxtPriority.ClientID %>").focus();
		return false;
	}
}
    </script>    
</body>
</html>
