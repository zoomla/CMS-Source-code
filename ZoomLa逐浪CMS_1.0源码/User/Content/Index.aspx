<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="ZoomLa.WebSite.User.Content._Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>会员中心>> 内容管理</title>
    <link href="/App_Themes/UserDefaultTheme/Default.css" type="text/css" rel="stylesheet" />
    <link href="/App_Themes/UserDefaultTheme/xtree.css" type="text/css" rel="stylesheet" />
    <link href="../css/user.css" rel="stylesheet" type="text/css" />
    <link href="../css/default1.css" rel="stylesheet" type="text/css" />
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
 
 function JumpToMain(val)
 {
    var objLeft =  window.frames['main_left'];
    var objContent = window.frames['main_right'];
    switch(val)
    {
        default:
        case 1:
            objLeft.location.href="NodeTree.aspx?t=1";
            objContent.location.href="MyContent.aspx";
            break;
        case 2:
            objLeft.location.href="NodeTree.aspx?t=2";
            objContent.location.href="MyContent.aspx?type=UnAudit"; 
            break;
        case 3:
            objLeft.location.href="NodeTree.aspx?t=3";
            objContent.location.href="MyContent.aspx?type=Audit";
           break; 
        case 4:
            objLeft.location.href="NodeTree.aspx?t=4";
            objContent.location.href="MyFavori.aspx";
            break;
        case 5:
            objLeft.location.href="NodeTree.aspx?t=5";
            objContent.location.href="MyComment.aspx" 
            break;            
    } 
 }
</script>
</head>
<body>
    <form id="form1" runat="server">
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
          function 
      </script>
        <!-- 我的控制菜单开始 -->
        <div class="c_title">
            <div class="u_navigation_pic">
            </div>
            <div class="u_navigation_menu">
                <div class="U_tabtitle">
                    <a href="/User/Default.aspx">首页</a>
                </div>                
                <div class="U_tabtitle" id="UTabTitle0" onclick="ShowUTabs(0)"><a href="javascript:ShowUTabs(0)">信息管理</a></div>            
                <div class="U_tabtitle" id="UTabTitle1" onclick="ShowUTabs(1)"><a href="javascript:ShowUTabs(1)">短消息</a></div>            
                <div class="U_titlemouseover" id="UTabTitle2" onclick="ShowUTabs(2)"><a href="javascript:ShowUTabs(2)">帐户管理</a></div>             
            </div>
            <div class="clearbox"></div>
        </div>
        <!-- 我的控制菜单开结束 -->
        <div class="u_navigation">    
            <div id="UTab0" style="">
                <ul>                    
                    <li><a href="#" onclick="JumpToMain(1);">所有信息</a></li>
                    <li><a href="#" onclick="JumpToMain(2);">待审核的信息</a></li>                        
                    <li><a href="#" onclick="JumpToMain(3);">已审核的信息</a></li>
                    <li><a href="#" onclick="JumpToMain(4);">我收藏的信息</a></li>                        
                    <li><a href="#" onclick="JumpToMain(5);">我发表的评论</a></li>                        
                </ul>
            </div> 
            <div id="UTab1" style="display: none;">
                <ul>                    
                    <li><a href="/User/default.aspx">撰写短消息</a></li>                   
                    <li><a href="/User/default.aspx">收件箱</a></li>                        
                </ul>
            </div>
            <div id="UTab2" style="display: none;">
                <ul>                    
                    <li><a href="/User/default.aspx">我的信息</a></li>                        
                    <li><a href="/User/default.aspx">修改密码</a></li>                        
                    <li><a href="/User/Logout.aspx">退出登录</a></li>                        
                </ul>
            </div>        
        </div>
<!-- 用户快捷导航结束 -->
        <div class="clearbox"> </div>   
      <div id="main_bg">
      <div id="main_left">
        <div class="user_box2">
          <dl>
            <dt>栏目导航</dt>
            <dd><iframe frameborder="0" width="100%" height="500px" src="NodeTree.aspx" scrolling="auto" id="contentleft" name="main_left"></iframe></dd>
          </dl>
        </div>
      </div>
      <div id="main_right">
        <div id="main_right_box">
          <div class="user_box_right"><iframe id="contentright" width="760px" height="500px" onload="dyniframesize()" ondatabinding="dyniframesize()" src="MyContent.aspx" frameborder="0" name="main_right"></iframe></div>
        </div>
      </div>      
      <div class="clearbox"> </div>
    </div>    
    </div>
  </div>
</div>
<!-- 底部 -->

<!-- 底部 -->
    </form>
 <script type="text/javascript">
    <!--
    JumpToMain(<%= Tnum %>);
    //-->
 </script>
</body>
</html>
