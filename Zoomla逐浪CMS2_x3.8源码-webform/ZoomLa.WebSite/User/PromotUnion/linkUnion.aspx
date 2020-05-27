<%@ Page Language="C#" AutoEventWireup="true" CodeFile="linkUnion.aspx.cs" Inherits="User_PromotUnion_linkUnion" EnableViewStateMac="false" %>
<!DOCTYPE HTML>
<html>
<head runat="server">
<title>链接推广</title>
<link href="../App_Themes/UserThem/user_user.css" rel="stylesheet" type="text/css" />
<style type="text/css">
#container{	margin:0 18px; background:white; clear:both; padding:10px 0 0; overflow:auto;}
.MyMoneyTitle{margin-top:13px; margin-left:17px;}
.MyMoneyTitle div.Title{color:#434343; font-size:18px; font-weight:bold;}
.MyMoneyTitle div.Text{font-size:12px; color:#7B7B7B; padding-top:9px;}
.MyMoneyRoundTitle{font-size:14px; font-weight:bold; color:#313131; padding-top:10px;}
.myMoneyTableOne{}
.myMoneyTableOne td{ height:29px; text-align:center; color:#7C7C7C;}
.myMoneyTableOne td.One{ height:29px; text-align:left; color:#7C7C7C; padding-left:31px;}
.myMoneyTableOne td.Two{ height:29px; text-align:left; color:#7C7C7C; padding-left:14px; padding-right:14px; width:519px; +width:539px; }
.myMoneyTableOne th{ height:29px; background:#F1F1F1; text-align:center; font-weight:100; color:#313131;}
</style>
<script language="javascript" type="text/javascript">
    //url检查
    function chkurl(url) {
        var urlRegS = /^http:\/\/[A-Za-z0-9]+\.[A-Za-z0-9]+[\/=\?%\-&_~`@[\]\':+!]*([^\"\"])*$/;
        if (urlRegS.exec(url)) { return true; }
        else { return false; }
    }
    function createprolink(id1) {
        var url = document.getElementById(id1).value;
        if (!chkurl(url)) { alert('请输入正确的活动网址！'); document.getElementById(id1).focus(); return false; }
        else {return true;}
    }
    //复制命令
    function copytext(inputid) {
        var text = document.getElementById(inputid);
        if (text.value == "") { alert("没有内容"); text.focus(); return; }
        text.select(); //选择对象
        document.execCommand("Copy"); //执行浏览器复制命令
        alert("成功复制!请用CTRL+V即贴粘推广。");
    }
</script> 
</head>
<body>
   <div id="container">
    <div style="font-size:12px; color:#313131;">&nbsp;<a href="Default.aspx">推广联盟首页</a>
          </div>
     <div class="MyMoneyTitle" id="unionact">
                <div style="float:left; margin-left:24px;">
                    <div class="Title">通过链接邀请好友注册</div>
                    <div class="Text" style="line-height:1.8; width:830px;">--把包含您ID 的链接发在博客、论坛等地方，每邀请1位朋友成功注册，您就能获得最高3元现金奖励！<br />
</div>
                </div>
                <div style="clear:both; height:1px; overflow:hidden;"></div>
            </div>
     <div style="background:#EAF5FE; margin-top:12px;">
     	<div style="font-size:14px; font-weight:bold; color:#535353; height:16px; padding:23px 0 0 23px; ">获取您自己的ID链接：</div>
        <form style="margin:0 0 0 90px; padding:0;" runat="server">
        <div style="font-size:12px; color:#313131; margin-top:23px;">复制此链接发送给好友：</div>
        <div style="height:22px; overflow:hidden; font-size:14px; margin-top:11px; margin-left:20px;+margin-left: -45px; -margin-left:-54px;">
        <asp:TextBox ID="regurl" runat="server" style="width:425px; border:1px solid #C9C9C9;" ></asp:TextBox>
        <input ID="btnRegurl"  value="复制推广链接" onClick="copytext('regurl')" type="button" /></div>
        <div style="font-size:12px; color:#313131; margin-top:20px;">在支持HTML的网页（比如论坛、博客），可以复制下列HTML代码：</div>
        <div style="font-size:14px; margin-top:11px;">
        <textarea style="width:425px; height:46px; border:1px solid #C9C9C9; padding:1px 0;" id="regtext" runat="server"></textarea>
        <input ID="Button1" value="复制推广链接" onClick="copytext('regtext')" type="button" /></div>
        <div style="clear:both; height:39px; overflow:hidden;"></div>
        </form>
     </div>
     <!--如何推广-->
     <div id="unionm"></div>
     <div style="padding:0 20px;background:#F9F9F9;">
     	<div class="MyMoneyRoundTitle">奖励规则</div>
        <table class="myMoneyTableOne" cellpadding="0" cellspacing="1" style=" background:#DADADA; width:460px;">
		<tr style="background:#F1F1F1;">
        <th style="width:138px;">邀请会员数量</th>
        <th style="width:160px; ">朋友注册后，您得奖励</th>
        <th >朋友购物后，您得奖励</th></tr>
		<tr style="background:#fff;">
        <td class="One">1-10名会员</td>
        <td>0.2元/名</td>
        <td rowspan="4" >2元/名</td></tr>
		<tr style="background:#fff;">
        <td class="One">11-50名会员</td>
        <td>0.4元/名</td>
        </tr>
		<tr style="background:#fff;">
        <td class="One">51-100名会员</td>
        <td>0.8元/名</td>
        </tr>
		<tr style="background:#fff;">
        <td class="One">100名会员以上</td>
        <td>1.0元/名</td>
        </tr>
        </table>
        <div style="font-size:12px; color:#7C7C7C; font-weight:bold; margin:20px 0 8px 0;">注意事项：</div>
        <div style="font-size:12px; color:#646464; line-height:22px; padding-bottom:20px;">1、为保证推荐会员的质量，不允许以注册送钱或注册送积分等利益诱导来吸引注册，否则将给予处罚；<br/>
2、为了合作商城的正常利益，不允许到返利网的合作商城内进行推广；<br/>
 	       	 3、返利网保留对本活动的最终解释权。
        </div>
     </div>
     <div></div>   
     <!--/如何推广-->
</div>
</body>
</html>
