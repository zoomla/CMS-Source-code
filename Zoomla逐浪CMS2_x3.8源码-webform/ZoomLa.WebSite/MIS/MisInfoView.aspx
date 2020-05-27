<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MisInfoView.aspx.cs" Inherits="MIS_MisInfoView" %>
<!DOCTYPE html>
<html>
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<title></title>
<link rel="stylesheet" href="../App_Themes/User.css" />
</head>
<body>
<form id="form1" runat="server">
<div class="Mis_Title"><strong><a href="javascript:void(0)" onclick="loadPage('see', 'AddMisInfo.aspx?ProID=<%=Request["ProID"] %>&MID=<%=Request["MID"]%>&Type=<%=Request["Type"] %>')">新建协商</a></strong></div>
<div class="td_center">
<div class="arr_sub" style="width: 100%;">
<div onclick="showChangeAttentDiv(event,92612,6)"><b class="ico_le tnor_label_ico"/></div>
<span>主题:<b><asp:Label ID="lblTitle" runat="server"></asp:Label> </b></span>
</div>
<div class="arr_posi">
<table class="arr" width="100%">
<tr><td class="arr_pers_mess" rowSpan="2"><div class="floor"><asp:Label ID="lblName" runat="server"></asp:Label> </div><div class="pers_img"><asp:Image ID="UserFace" runat="server" ImageUrl="" Width="100" /> </div><div class="pers_mess"><ul><li>部门:<asp:Label ID="lblPar" runat="server"></asp:Label>鹏飞纺织</li>
<li><a href="?Uid=">查看员工资料</a></li></ul></div></td>
    <td class="arr_text"><div class="ti"><strong>#顶楼</strong> 发表时间:<asp:Label ID="lblCreateTime" runat="server"></asp:Label></div><div class="text"><asp:Label ID="lblContent" runat="server"></asp:Label> </div></td></tr>
<tr><td class="boper">
    <div class="fl">
        <input class="btn_sty" onclick="addOneToForum('messageContent92612', '', 0);" type="button" value="加入论坛"/> <input class="btn_sty" onclick="    addOneToKnowledge('messageContent92612', '', '颜飞', '项目协商');" type="button" value="加入知识"/> <input class="btn_sty" onclick="    transmitInf('messageContent92612', '4', '1212861');" type="button" value="转发"/>
    </div>
    <div class="fr">
        回到顶端
         返回列表
    </div></td></tr>
</table>
</div>
</div>
</form>
</body>
</html>