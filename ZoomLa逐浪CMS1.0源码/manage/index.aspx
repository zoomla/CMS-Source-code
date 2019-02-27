<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="ZoomLa.WebSite.Manage.index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>逐浪CMS后台管理主页</title>
    <link href="../App_Themes/AdminDefaultTheme/index.css" type="text/css" rel="stylesheet" />
    <script language="javascript" type="text/JavaScript">    
    var StyleSheetPath="/App_Themes/AdminDefaultTheme/";
    	
    function ShowHideLayer(val)
    {
        
        for(var i = 1 ;i < 6;i++)
        {
            if(i != val)
            {
                if(document.getElementById('Menu_'+i.toString()).style.display !="none")
                {
                    document.getElementById('Menu_'+i.toString()).style.display = "none";
                }
                document.getElementById('Main_'+i.toString()).className="Main_1";
            }
            else
            {
                document.getElementById('Menu_'+i.toString()).style.display = "";
				document.getElementById('Main_'+i.toString()).className="Main_2";
            }
        }
    }
    //** iframe自动适应页面 **//

 //输入你希望根据页面高度自动调整高度的iframe的名称的列表
 //用逗号把每个iframe的ID分隔. 例如: ["myframe1", "myframe2"]，可以只有一个窗体，则不用逗号。

 //定义iframe的ID
 //alert("begin");
 var iframeids=["main_right"];

 //如果用户的浏览器不支持iframe是否将iframe隐藏 yes 表示隐藏，no表示不隐藏
 var iframehide="yes";

 function dyniframesize() 
 {
  for (i=0; i<iframeids.length; i++)
  {
   if (document.getElementById)
   {
    var dyniframe = document.getElementById(iframeids[i]);
    if (dyniframe && !window.opera)
    {
     dyniframe.style.display="block";
     if (dyniframe.contentDocument && dyniframe.contentDocument.body.offsetHeight) //如果用户的浏览器是NetScape
     {
      dyniframe.style.height = (dyniframe.contentDocument.body.offsetHeight ) + "px"; 
      dyniframe.style.width = (dyniframe.contentDocument.body.offsetWidth ) + "px"; 
     }
     else if (dyniframe.Document && dyniframe.Document.body) //如果用户的浏览器是IE
     {
        if(dyniframe.Document.body.scrollHeight)
        {
            dyniframe.style.height = dyniframe.Document.body.scrollHeight ;
            dyniframe.style.width = dyniframe.Document.body.scrollWidth ;
        }
      }
    }
   }
   //根据设定的参数来处理不支持iframe的浏览器的显示问题
   if ((document.all || document.getElementById) && iframehide=="no")
   {
    var tempobj=document.all? document.all[iframeids[i]] : document.getElementById(iframeids[i]);
    tempobj.style.display="block";
   }
  }
 }
    function SetWinHeight(obj)
    {
        var win=obj;
        if (document.getElementById)
        {
            if (win && !window.opera)
            {
             if (win.contentDocument && win.contentDocument.body.offsetHeight) 
             {
              win.height = win.contentDocument.body.offsetHeight; 
              win.width =win.contentDocument.body.offsetWidth;
              }
             else if(win.Document && win.Document.body.scrollHeight)
             {
              win.height = win.Document.body.scrollHeight;
              win.width=win.Document.body.scrollWidth;
              }
            }
        }
    }
    
    function ShowMain(lefturl,righturl)
    {
        var objLeft =  window.frames['left'];
        var objContent = window.frames['main_right'];
        if(lefturl!="")
            objLeft.location.href=lefturl;
        objContent.location.href=righturl;
    }
    </script>
</head>
<body id="Indexbody">
<table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td colspan="3" style="height: 102px">
				<form id="myform" runat="server">
					<div id="content" align="center">					
					<ul id="ChannelMenuItems">
				        <li id="Main_1" class="Main_2" onclick="ShowHideLayer(1)">我的工作台</li>
				        <li id="Main_2" class="Main_1" onclick="ShowHideLayer(2)">内容管理</li>				               
				        <li id="Main_3" class="Main_1" onclick="ShowHideLayer(3)">用户管理</li>
				        <li id="Main_4" class="Main_1" onclick="ShowHideLayer(4)">附件管理</li>
				        <li id="Main_5" class="Main_1" onclick="ShowHideLayer(5)">系统设置</li>
				    </ul>
					<!-- 子菜单开始 -->
				      <div id="SubMenu">
				        <!-- 我的工作台 -->
				        <div id="Menu_1" style="width: 100%;">
				          <ul>            
				            <li> <a href="#" onclick="ShowMain('Profile/QuickLinks.aspx','Profile/Worktable.aspx')"> 工作台首页</a></li>
				            <li> <a href="#" onclick="ShowMain('','Profile/ModifyPassword.aspx')">修改密码</a></li>
				            <li> <a href="SignOut.aspx"><span class="SignOut">安全退出</span></a></li>
				          </ul>
				        </div>
				        <!-- 内容管理 -->
				        <div id="Menu_2" style="width: 100%; display: none;">
				          <ul>
				            <li><a href="#" onclick="ShowMain('Content/NodeTree.aspx','Content/ContentManage.aspx')">按栏目管理信息</a></li>
				            <li><a href="#" onclick="ShowMain('Content/SpecTree.aspx','Content/SpecContent.aspx')">按专题管理信息</a></li>
				            <li><a href="#" onclick="ShowMain('Content/CreateLink.aspx','Content/CreateHtmlContent.aspx')">生成管理</a></li>
				            <li><a href="#" onclick="ShowMain('Content/NodeTree.aspx?t=1','Content/CommentManage.aspx')">评论管理</a></li>
				            <li><a href="#" onclick="ShowMain('Content/NodeTree.aspx?t=2','Content/ContentRecycle.aspx')">回收站管理</a></li>				            				                        
				          </ul>
				        </div>				        
				        <!-- 用户管理 -->
				        <div id="Menu_3" style="width: 100%; display: none;">
				          <ul>
				            <li><a href="#" onclick="ShowMain('User/AdminLink.aspx','User/AdminManage.aspx')">管理员管理</a></li>
				            <li><a href="#" onclick="ShowMain('User/UserGuide.aspx','User/UserManage.aspx')">会员管理</a></li>            
				            <li><a href="#" onclick="ShowMain('User/MessageLink.aspx','User/Message.aspx')">信息发送</a> </li>            
				          </ul>
				        </div>
				        <!-- 附件管理 -->
				        <div id="Menu_4" style="width: 100%; display: none;">
				          <ul>
				            <li><a href="#" onclick="ShowMain('AddOn/ProjectGuide.aspx','AddOn/ProjectManage.aspx')">项目管理</a></li>
				            <li><a href="#" onclick="ShowMain('Plus/ADGuide.aspx','Plus/ADManage.aspx')">广告管理</a></li>				            				            
				            <li><a href="#" onclick="ShowMain('AddOn/OthersGuide.aspx','AddOn/CorrectManage.aspx')">纠错管理</a></li>
				            <li><a href="#" onclick="ShowMain('AddOn/OthersGuide.aspx','AddOn/SourceManage.aspx')">其他管理</a></li>				                        
				            <li><a href="#" onclick="ShowMain('Plus/UploadDir.aspx','Plus/UploadFile.aspx')">上传文件管理</a></li>
				            
				          </ul>
				        </div>
				        <!-- 系统设置 -->
				        <div id="Menu_5" style="width: 100%; display: none;">
				          <ul>
				            <li><a href="#" onclick="ShowMain('Config/ConfigGuide.aspx','Config/SiteInfo.aspx')"> 网站配置</a></li>
				            <li><a href="#" onclick="ShowMain('Content/ModelGuide.aspx','Content/ModelManage.aspx')">内容模型管理</a></li>
				            <li><a href="#" onclick="ShowMain('Content/SetNodeTree.aspx','Content/NodeManage.aspx')">节点管理</a></li>
				            <li><a href="#" onclick="ShowMain('Content/SpecLink.aspx','Content/SpecialManage.aspx')">专题管理</a></li>
				            <li><a href="#" onclick="ShowMain('Template/TemplateLink.aspx','Template/TemplateManage.aspx')">模板标签管理</a></li>
				          </ul>
				        </div>
				    </div>
				    <!-- 子菜单结束 --> 
				    <div id="Announce" style="right: 5px; top: 3px">
				        <a href="/" target="_blank" title="网站首页">网站首页</a>&nbsp;|&nbsp;<a href="http://bbs.zoomla.cn/"   target="_blank" title="逐浪官方帮助">技术社区</a>&nbsp;|&nbsp;<a href="http://www.zoomla.cn/"   target="_blank" title="访问逐浪官方网站">官方网站</a>
                        | <a href="SignOut.aspx" title="安全退出">安全退出</a>
				    </div>
					</div>
				</form>
			</td>
		</tr>
		<tr style="vertical-align: top;">
			<td id="frmTitle" style="width:195px">
                <iframe frameborder="0" id="left" name="left" scrolling="no" src="Profile/QuickLinks.aspx"
                    style="width: 195px; visibility:visible; z-index: 2;" onload="Javascript:SetWinHeight(this)"></iframe>
            </td>
            <td class="but" style="width:5px;">                
            </td>
            <td>
                <iframe frameborder="0" id="main_right" name="main_right" scrolling="auto" src="Profile/Worktable.aspx"
                    style="width: 800px; height: 800px;visibility:visible; z-index: 2; overflow-x:hidden;" onload="Javascript:SetWinHeight(this)"></iframe>
                <div class="clearbox2" />
            </td>
		</tr>
</table>     
</body>
</html>
