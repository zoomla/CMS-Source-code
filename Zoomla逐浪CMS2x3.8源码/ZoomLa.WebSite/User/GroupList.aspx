<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="GroupList.aspx.cs" Inherits="User_GroupList" ClientIDMode="Static" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>我的团定</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="pageflag" data-nav="shop" data-ban="shop"></div>
<div class="container margin_t5">
<ol class="breadcrumb">
<li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
<li class="active">我的团定</li>
<div class="clearfix"></div>
</ol>
</div>
<div class="container margin_t5">
<div style="border: 1px solid #ddd;padding:10px;margin-bottom:10px;">
<span>
	<a href="GroupList.aspx?start=true">已付订金团定</a> | <a href="GroupList.aspx?start=false">未付订金团定</a>
</span>
</div>
<table class="table table-striped table-bordered table-hover">
<tr>
	<td class="text-center" colspan="8"><b>我的团定列表</b></td>
</tr>
<tr>
	<td width="5%" align="center"><strong>ID</strong></td>
	<td width="10%" align="center"><strong>商品名称</strong></td>
	<td width="10%" align="center"><strong>团购价格</strong></td>
	<td width="10%" align="center"><strong>订金支付</strong></td>
	<td width="5%" align="center"><strong>购买支付</strong></td>
	<td width="5%" align="center"><strong>团购人数</strong></td>
	<td width="10%" align="center"><strong>发起时间</strong></td>
	<td width="10%" align="center"><strong>操作</strong></td>
</tr>
<ZL:ExRepeater ID="Pagetable" runat="server" PagePre="<tr><td colspan='8' class='text-center'><input type='checkbox' id='CheckAll' />" PageEnd="</td></tr>">
	<ItemTemplate>
		<tr class="tdbg">
			<td align="center"><%#Eval("ID") %></td>
			<td align="center"><%#Getproname(Eval("ProID","{0}"))%></td>
			<td align="center"><%#Eval("buymoney")%></td>
			<td align="center"><%#GetGroupBuy(Eval("DepositPayID", "{0}"))%></td>
			<td align="center"><%#GetGroupBuy(Eval("PayID", "{0}"))%></td>
			<td align="center"><%#Eval("Snum")%></td>
			<td align="center"><%#Eval("Btime")%></td>
			<td align="center"><%#getactionbutton(Eval("ID","{0}")) %></td>
		</tr>
	</ItemTemplate>
	<FooterTemplate></FooterTemplate>
</ZL:ExRepeater>
</table>
<div id="view" runat="server"></div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<script language="javascript" type="text/javascript">
function CheckAll(spanChk)//CheckBox全选
{
	var oItem = spanChk.children;
	var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
	xState = theBox.checked;
	elm = theBox.form.elements;
	for (i = 0; i < elm.length; i++)
		if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
			if (elm[i].checked != xState)
				elm[i].click();
		}
}

function GetCheckvalue() {
	var spanChk = window.document.getElementById("CheckBox1");
	var elm = document.form1.elements;
	var listbox = "";
	var listheight = 0;
	var listnum = 0;
	var boxlist = 0;
	var innterhtml = "";
	var hiddenidvalue = opener.document.getElementById("Hiddenbind").value; //获取已经存在的ID值
	hiddenidvalue = "," + hiddenidvalue + ","; //初始查询

	for (i = 0; i < elm.length; i++) {
		if (elm[i].type == "checkbox" && elm[i].id != spanChk.id) {
			boxlist = boxlist + 1;
			if (elm[i].checked == true) {
				var tempvalue = "," + elm[i].value + ","; //初始ID
				if (hiddenidvalue.indexOf(tempvalue) == -1) {
					listheight = listheight + 1;
					innterhtml = innterhtml + "<TR>" + form1.getElementsByTagName("tr").item(boxlist + 2, 0).innerHTML + "<td align=center onclick=deleteRow('txtTables',this.parentElement.rowIndex,'" + elm[i].value + "')><u style=cursor:pointer>删除</u></td></TR>";
				}
				else//修改父页中绑定的商品数量
				{
					var topinputvalue = document.getElementById("pronum" + elm[i].value).value;
					opener.document.getElementById("pronum" + elm[i].value).value = parseInt(opener.document.getElementById("pronum" + elm[i].value).value) + parseInt(topinputvalue);
				}
			}
		}
	}

	for (i = 0; i < elm.length; i++) {
		if (elm[i].type == "checkbox" && elm[i].id != spanChk.id) {
			if (elm[i].checked == true) {
				var tempvalue = "," + elm[i].value + ","; //初始ID
				//判断是否存在此ID
				if (hiddenidvalue.indexOf(tempvalue) == -1) {
					//添加ID到隐藏控件
					listnum++;
					if (listnum == listheight) {
						listbox = listbox + elm[i].value;
					} else {
						listbox = listbox + elm[i].value + ',';
					}
				}
			}
		}
	}

	if (listbox != "") {
		var bindstr = opener.document.getElementById("Hiddenbind").value;
		if (bindstr != "") { bindstr = bindstr + ","; }
		var tempboxstrs = bindstr + listbox;
		var reg = new RegExp(",,", "g")
		tempboxstrs = tempboxstrs.replace(reg, ",");
		opener.document.getElementById("Hiddenbind").value = tempboxstrs;
	}

	var topstr = "<table width=100% border=0 cellspacing=1 cellpadding=1 id=txtTables> <tr class=tdbgleft> <td width=5% height=24 align=center><strong>ID</strong></td> <td width=5% height=24 align=center> <span name=CheckBox1><input id=CheckAllCheckBox1 type=checkbox name=CheckAllCheckBox1 onclick=CheckAll(this); /></span></td> <td width=10% height=24 align=center><strong>商品图片</strong></td><td width=35% height=24 align=center><strong>商品名称</strong></td><td width=15% height=24 align=center><strong>商品数量</strong></td><td width=20% height=24 align=center><strong>商品零售价</strong></td><td width=15% height=24 align=center><strong>操作</strong></td></tr>";
	if (innterhtml != "") {
		if (opener.document.getElementById("Span1").innerHTML == "") {
			opener.document.getElementById("Span1").innerHTML = topstr + innterhtml;
			opener.document.getElementById("Span1").innerHTML = opener.document.getElementById("Span1").innerHTML + "</TABLE>";
		}
		else {
			var tempstr = opener.document.getElementById("Span1").innerHTML;
			var reg = new RegExp("</TBODY>", "g")
			tempstr = tempstr.replace(reg, "");
			reg = new RegExp("</TABLE>", "g")
			tempstr = tempstr.replace(reg, "");
			tempstr = tempstr + innterhtml;
			opener.document.getElementById("Span1").innerHTML = tempstr + "</TBODY></table>";

		}
	}
	window.close();
}
</script>
</asp:Content>