<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="ZoomLa.WebSite.User._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" lang="UTF-8">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link href="../App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
<link href="../App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
<link href="css/user.css" rel="stylesheet" type="text/css" />
<link href="css/default1.css" rel="stylesheet" type="text/css" />
<script language="javascript" type="text/javascript" src="/js/common.js"></script>
<script language="javascript" type="text/javascript">

//** iframe自动适应页面 **//

 //输入你希望根据页面高度自动调整高度的iframe的名称的列表
 //用逗号把每个iframe的ID分隔. 例如: ["myframe1", "myframe2"]，可以只有一个窗体，则不用逗号。

 //定义iframe的ID
 //alert("begin");
 var iframeids=["contentright"];

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
      dyniframe.style.height = (dyniframe.contentDocument.body.offsetHeight + 5) + "px"; 
     }
     else if (dyniframe.Document && dyniframe.Document.body) //如果用户的浏览器是IE
     {
        if(dyniframe.Document.body.scrollHeight)
        {
            dyniframe.style.height = dyniframe.Document.body.scrollHeight + 5;
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
</script>
<title>
	会员中心 >> 首页
</title></head>
<body>

<!-- 顶部 -->

<div id="top_all">  
  <div id="menu">
    <div id="menubox_pic"> </div>
    <div id="menubox"><a href="/">网站首页</a></div>
  </div>
</div>
<!-- 中部 -->
<div id="center_all">
  <div class="u_management_bg">
    <div class="c_main_box">
    <script id="ShowTab" type="text/javascript">
          function ShowUTabs(ID)
          {
               for (i=0;i<3;i++)
               {
                    if(document.getElementById("UTabTitle" + i) && document.getElementById("UTab" + i))
                    {
                        if(i == ID)
                        {
                            document.getElementById("UTabTitle" + i).className="U_titlemouseover";
                            document.getElementById("UTab" + i).style.display="";
                        }
                        else
                        {
                            document.getElementById("UTabTitle" + i).className="U_tabtitle";
                            document.getElementById("UTab" + i).style.display="none";
                        }
                    }
               }
          } 

      </script>
        <!-- 我的控制菜单开始 -->
        <div class="c_title">
            <div class="u_navigation_pic">
            </div>
            <div class="u_navigation_menu">
                <div class="U_tabtitle">
                    <a href="Default.aspx">首页</a>
                </div>                
                <div class="U_tabtitle" id="UTabTitle0" onclick="ShowUTabs(0)"><a href="javascript:ShowUTabs(0)">信息管理</a></div>            
                <div class="U_tabtitle" id="UTabTitle1" onclick="ShowUTabs(1)"><a href="javascript:ShowUTabs(1)">短消息</a></div>            
                <div class="U_titlemouseover" id="UTabTitle2" onclick="ShowUTabs(2)"><a href="javascript:ShowUTabs(2)">帐户管理</a></div>            
            </div>
            <div class="clearbox"></div>
        </div>
        <!-- 我的控制菜单开结束 -->
        <div class="u_navigation">    
            <div id="UTab0" style="display: none;">
                <ul>                    
                    <li><a href="/User/Content/Index.aspx?t=1"><img src="/User/Images/article_all.gif" style="border: 0" alt="所有信息" /><br />所有信息</a></li>
                    <li><a href="/User/Content/Index.aspx?t=2"><img src="/User/Images/article_unpassed.gif" style="border: 0" alt="待审核的信息" /><br />待审核的信息</a></li>                        
                    <li><a href="/User/Content/Index.aspx?t=3"><img src="/User/Images/article_passed.gif" style="border: 0" alt="已审核的信息" /><br />已审核的信息</a></li>
                    <li><a href="/User/Content/Index.aspx?t=4"><img src="/User/Images/article_favorite.gif" style="border: 0" alt="我收藏的信息" /><br />我收藏的信息</a></li>                        
                    <li><a href="/User/Content/Index.aspx?t=5"><img src="/User/Images/article_add.gif" style="border: 0" alt="我发表的评论" /><br />我发表的评论</a></li>                        
                </ul>
            </div> 
            <div id="UTab1" style="display: none;">
                <ul>                    
                    <li><a href="/User/Message/MessageSend.aspx" target="main_right"><img src="/User/Images/m_new.gif" style="border: 0" alt="撰写短消息" /><br />撰写短消息</a></li>                   
                    <li><a href="/User/Message/Message.aspx" target="main_right"><img src="/User/Images/m_box_in.gif" style="border: 0" alt="收件箱" /><br />收件箱</a></li>                        
                </ul>
            </div>
            <div id="UTab2" style="">
                <ul>                    
                    <li><a href="/User/MyInfo.aspx" target="main_right"><img src="/User/Images/userfriend_info.gif" style="border: 0" alt="我的信息" /><br />我的信息</a></li>                        
                    <li><a href="/User/ChangPSW.aspx" target="main_right"><img src="/User/Images/userfriend_password.gif" style="border: 0" alt="修改密码" /><br />修改密码</a></li>                        
                    <li><a href="/User/Logout.aspx"><img src="/User/Images/userfriend_logout.gif" style="border: 0" alt="退出登录" /><br />退出登录</a></li>                        
                </ul>
            </div>        
        </div>
<!-- 用户快捷导航结束 -->
        <div class="clearbox"> </div>   
      <div id="main_box">        
          <iframe id="contentright" width="100%" height="500px" onload="dyniframesize()" ondatabinding="dyniframesize()" src="MyInfo.aspx" frameborder="0" name="main_right"></iframe>        
      </div>
      <div class="clearbox"> </div>
    
    </div>
  </div>
</div>
<!-- 底部 -->

<!-- 底部 -->
</body>
</html>