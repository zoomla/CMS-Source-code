<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewCert.aspx.cs" Inherits="Manage_Site_ViewCert" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>域名证书查询</title>
<script type="text/javascript" src="/JS/Common.js"></script>
<script type="text/javascript">
function setValue(v) {
	var arr = v.split(",");
	var temp;
	for (i = 0; i < arr.length; i++)
	{
		temp = arr[i].split(":");
		$("#" + temp[0]).val(temp[1]);
		$("." + temp[0]).text(temp[1]);
	}
}
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<table width="600" border="0" cellspacing="0" cellpadding="0">
<tr> 
<td height="76">
  <table border="0" cellspacing="0" cellpadding="0" align="center" width="100%">
	<tr> 
	  <td rowspan="12" width="1" bgcolor="#000000"><!-- img src="http://www.hx008.com/imge/v_dmo/dot.gif" width="1" height="1"--></td>
	  <td bgcolor="#000000" height="1" colspan="3"><!-- img src="http://www.hx008.com/imge/v_dmo/dot.gif" width="1" height="1"--></td>
	  <td rowspan="12" bgcolor="#000000" width="1"><!-- img src="http://www.hx008.com/imge/v_dmo/dot.gif" width="1" height="1"--></td>
	</tr>
	<tr> 
	  <td colspan="3" align="center"  bgcolor="#FFFFFF"> <!--background="http://www.hx008.com/imge/v_dmo/fontbg.gif"-->
			  <TABLE cellSpacing=0 cellPadding=0 width="100%" align=center border=0>
				<TBODY>
				<tr>
				  <td align=middle style="font-size: 9pt"><BR>
					<TABLE cellSpacing=0 cellPadding=0 width="94%" 
					  border=0><TBODY>
					  <tr>
						<td vAlign=center style="font-size: 9pt">
						<img src="/manage/site/images/certificate_i.gif" /></td>
						</tr>
					  </TBODY></TABLE>
					<TABLE cellSpacing=0 cellPadding=3 width="94%" 
					  border=0><TBODY>
					  <tr>
						<td style="font-size: 9pt">
						  本证书由互联网名称与数字地址分配机构ICANN（The Internet Corporation for Assigned Names and Numbers）授权江西吉瑞数码印务有限公司并由华夏互联hx008.com（hx008.com）制作并颁发此证。<B><BR>
						  <BR>证明</B>：<BR><BR><B>域名<FONT 
						  color=#ff0000> <FONT 
						  face="Arial, Helvetica, sans-serif"><%= DomainName() %></FONT> 
						  </FONT>已由<FONT color=#ff0000> <FONT 
						  face="Arial, Helvetica, sans-serif"><span class="rname1"></span></FONT></FONT> 
						  注册，并已在中国国家顶级域名数据库中记录。</B></td></tr></TBODY></TABLE><BR>
					<TABLE cellSpacing=0 borderColorDark=#ffffff 
					cellPadding=2 width="94%" borderColorLight=#666666 
					border=1>
					  <TBODY>
					  <tr>
						<td align=right width="45%" style="font-size: 9pt">
						  <div align=left><FONT 
						  face="Arial, Helvetica, sans-serif">域名(Domain 
						  Name)：</FONT></div></td>
						<td vAlign=center align=left width="55%" style="font-size: 9pt"><FONT 
						  face="Arial, Helvetica, sans-serif"><%= DomainName() %></FONT></td></tr>
					  <tr>
						<td align=right width="45%" style="font-size: 9pt">
						  <div align=left><FONT 
						  face="Arial, Helvetica, sans-serif">域名注册人(Registrant,中文)：</FONT></div></td>
						<td vAlign=center align=left width="55%" style="font-size: 9pt"><FONT 
						  face="Arial, Helvetica, sans-serif"><span class="rname1"></span></FONT></td></tr>
					  <tr>
						<td align=right width="45%" style="font-size: 9pt">
						  <div align=left><FONT 
						  face="Arial, Helvetica, sans-serif">域名注册人(Registrant,English)：</FONT></div></td>
						<td vAlign=center align=left width="55%" style="font-size: 9pt"><FONT 
						  face="Arial, Helvetica, sans-serif"><span class="rname2"></span></FONT></td></tr>
					  <tr>
						<td align=right width="45%" style="font-size: 9pt">
						  <div align=left><FONT 
						  face="Arial, Helvetica, sans-serif">注册时间(Registration 
						  Date)：</FONT></div></td>
						<td vAlign=center align=left width="55%" style="font-size: 9pt"><FONT 
						  face="Arial, Helvetica, sans-serif">2014-01-17</FONT></td></tr>
					  <tr>
						<td align=right width="45%" style="font-size: 9pt">
						  <div align=left><FONT 
						  face="Arial, Helvetica, sans-serif">到期时间(Expiration 
						  Date)：</FONT></div></td>
						<td vAlign=center align=left width="55%" style="font-size: 9pt"><FONT 
						  face="Arial, Helvetica, sans-serif"><%= DomainEndDate() %></FONT></td></tr>
					  <tr>
						<td align=right width="45%" style="font-size: 9pt">
						  <div align=left><FONT 
						  face="Arial, Helvetica, sans-serif">域名服务器(Domain Name Server)1：</FONT></div>
						</td>
						<td vAlign=center align=left width="55%" style="font-size: 9pt"><FONT 
						  face="Arial, Helvetica, sans-serif"><span ></span>dns.hx008.net</FONT></td></tr>
					  <tr>
						<td align=right width="45%" style="font-size: 9pt">
						  <div align=left><FONT 
						  face="Arial, Helvetica, sans-serif">域名服务器(Domain 
						  Name Server)2：</FONT></div></td>
						<td vAlign=center align=left width="55%" style="font-size: 9pt"><FONT 
						  face="Arial, Helvetica, sans-serif">dns1.hx008.net 
						  </FONT></td></tr></TBODY></TABLE>
					<BR>
					<BR>
					<TABLE width="94%" align=center border=0>
					  <TBODY>
					  <tr>
						<td style="font-size: 9pt">
						  <P 
						  align=left>以下说明与本证书主文一起构成本证书统一整体，不可分割：<BR><BR><FONT 
						  style="FONT-SIZE: 9pt; LINE-HEIGHT: 13pt">1．本证书表明证书上列出的组织或者个人是列出的域名的合法注册人。该注册人依法享有该域名项下之各项权利。<BR>
							2．本证书并不表明华夏互联hx008.com(hx008.com)的运营商---江西逐浪软件科技有限公司（Jiangxi Zoomla! Softwar CO.,LTD）对本证书所列域名是否贬斥、侵害或毁损任何第三人之合法权利或利益作出任何明示或默示之评判、确认、担保，或作出其它任何形式之意思表示。江西逐浪软件科技有限公司（Jiangxi Zoomla! Softwar CO.,LTD）亦无任何责任或义务作出上述之评判、确认、担保，或作出其它任何形式之意思表示。<BR>
							3．因本证书中所列域名之注册或使用而可能引发与任何第三人之纠纷或冲突,均由该域名注册人本人承担，江西逐浪软件科技有限公司（Jiangxi Zoomla! Softwar CO.,LTD）不承担任何法律责任。江西逐浪软件科技有限公司（Jiangxi Zoomla! Softwar CO.,LTD）亦不在此类纠纷或冲突中充当证人、调停人或其它形式之参与人。<BR>
							4．本证书不得用于非法目的，江西逐浪软件科技有限公司（Jiangxi Zoomla! Softwar CO.,LTD）不承担任何由此而发生或可能发生之法律责任。</FONT></P>
						  <div id="div1" style="POSITION: absolute; LEFT: 347px; TOP: 369px;" onmousedown="down()" onmousemove="move()" onmouseup="up()">
							<img border="0" src="/manage/site/images/dmo_zhan.gif" /></div>
							<script>
								var isdown = false
								var beginx, beginy
								function down() {
									isdown = true;
								}

								function move() {
									if (isdown) {
										var endx = event.clientX;
										var endy = event.clientY;
										div1.style.left = parseInt(div1.style.left) + endx - beginx;
										div1.style.top = parseInt(div1.style.top) + endy - beginy;
									}
									beginx = event.clientX;
									beginy = event.clientY;
								}

								function up() {
									isdown = false;
								}
								document.body.onmousemove = move
								document.body.onmouseup = up
</script> 

						  <p align="left">当本证书持有、出具、展示或以其它任何形式使用时，即表明本证书之持有人或接触人已审读、理解并同意以上各条款之规定。</p></td></tr></TBODY></TABLE><BR></td></tr>
				<tr>
				  <td height=20 rowSpan=2 style="font-size: 9pt">
					<TABLE height=20 cellSpacing=0 cellPadding=0 
					width="100%" border=0>
					  <TBODY>
					  <tr bgColor=#cccccc>
						<td style="font-size: 9pt">
						  <div align=center>关于域名的相关情况，请查询 
					 
							<a target="_blank" href="http://www.hx008.com/" style="color: #000000; text-decoration: none">
							<font color="#808000">http://www.hx008.com/</font></a></div></td></tr></TBODY></TABLE></td></tr></TBODY></TABLE>
	  </td>
	</tr>
	<tr> 
	  <td  align="center" colspan="3"> <!--background="di.gif"-->
		<table width="100%" border="0" cellspacing="0" cellpadding="0" bgcolor="#FF9900">
		  <tr bgcolor="#CC0000"> 
			<td height="20"> 
			  <div align="center"><font color="#FFFFFF">鉴别本证书的真伪，请访问</font><a href="http://www.hx008.com" target="_blank"><font color="#FFFF00">华夏互联hx008.com</font></a><font color="#FFFFFF">进行查询！</font></div>
			</td>
		  </tr>
		</table>
	  </td>
	</tr>
	<tr> 
	  <td height="1" bgcolor="#000000" colspan="3"><!--img src="img/dot.gif" width="1" height="1"--></td>
	</tr>
  </table>
</td>
<td width="5" valign="top" bgcolor="#666666" height="76"> 
  <table width="5" border="0" cellspacing="0" cellpadding="0" height="5">
	<tr bgcolor="#CCCCCC"> 
	  <td><!--img src="http://www.hx008.com/imge/v_dmo/cdot.gif" width="1" height="1"--></td>
	</tr>
  </table>
</td>
</tr>
<tr bgcolor="#666666"> 
<td colspan="2"> 
  <table width="5" border="0" cellspacing="0" cellpadding="0" height="5">
	<tr bgcolor="#CCCCCC"> 
	  <td><!--img src="http://www.hx008.com/imge/v_dmo/cdot.gif" width="1" height="1"--></td>
	</tr>
  </table>
</td>
</tr>
</table>
</asp:Content>