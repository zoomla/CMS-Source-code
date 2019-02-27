<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CreateSite.aspx.cs" Inherits="manage_IISManage_CreateSite"  MasterPageFile="~/Manage/I/Default.master" Title="创建新站点"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>配置管理</title>
<style>#site_nav .site01 a{ background:url(/App_Themes/AdminDefaultTheme/images/site/menu_cur.png) left no-repeat;}</style>
<script type="text/javascript" src="/JS/chinese.js"></script>
<script type="text/javascript" src="/JS/Common.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div class="n_site_bread">
<ul class="breadcrumb">
<li><a href="<%= CustomerPageAction.customPath2 %>/Main.aspx">工作台</a></li>
<li><a href="Default.aspx">站群中心</a></li>
<li class="active">配置管理</li>
</ul> 
</div> 
<div id="site_main">
<div id="CSWSDiv" class="cswsDiv"><!--Create Single Web Site-->
	 <ul><li><strong>站点名称：</strong><asp:TextBox runat="server" ID="CSWebName" CssClass="form-control text_md" onKeyUp="get()" onblur="checkValue(this,'R1',1)" /><span style="color:red" id="R1">*</span></li>
	 <li><strong>物理路径：</strong><asp:TextBox runat="server" CssClass="form-control text_md" ID="CSPhysicalPath" onblur="checkValue(this,'R2',2)"/><span style="color:red" id="R2">*非自动模式，必须在服务器上已经建立好文件夹，如:D:\WebSite\</span></li>
		 <li><strong>网站端口：</strong><asp:TextBox runat="server" ID="CSPort" CssClass="form-control text_md"  MaxLength="6" txt="端口"  value="80" onblur="checkValue(this,'R3',3)"/><span style="color:red" id="R3">*默认为80，最多6位数字格式。</span></li>
		 <li><strong>动态域名：</strong><asp:TextBox runat="server" ID="CSDomain" CssClass="form-control text_md"  txt="域名"/><span style="color:red">*例:www.z01.com,需要绑定的域名</span></li>
		 <li id="batTr"><strong><label for="newa">批量绑定：</label></strong>
		 <a href="javascript:" onclick="newadd()" title="添列一列" id="newa"><i class="fa fa-plus-square"></i></a> </li>
   <li><strong>应用程序池：</strong><asp:TextBox runat="server" ID="CSAppPool" CssClass="form-control text_md"  /><span style="color:red"></span></li>
   <li><strong>立即启动网站：</strong> <input type="checkbox" name="chk" checked="checked"/> </li>
   <li><strong>操作：</strong><asp:Button runat="server" ID="CSWSBtn" Text="确定"  CssClass="btn btn-sm btn-primary" OnClick="CSWSBtn_Click"  OnClientClick="return checkinfo('sitePort', 'Domain');"/>
			<input type="button" id="rBtn1" value="返回管理界面" class="btn btn-sm btn-primary" onclick="back();"/>
       <input type="button" id="closeBtn" value="关闭" class="btn btn-sm btn-primary" onclick="parent.closeDiv();"/>
   </li></ul>
  <script type="text/javascript">
      function get()
      {
          document.getElementById("<%= this.CSAppPool.ClientID%>").value = document.getElementById("<%= this.CSWebName.ClientID%>").value;
      }
	  $().ready(function () {
		  $("#<%=CSWebName.ClientID%>").keyup(function () { Getpy('<%=CSWebName.ClientID%>', '<%=CSAppPool.ClientID%>') });
	  });
	  function oldadd() {//Disuse
		  var tr = "<tr class='tdbg'><td><label>端口：</label><input type='text' name='sitePort'  txt='端口' style='width:40px;'/>" +
			  "</td><td><label>域名：</label><input type='text' name='Domain' txt='域名'/></td><td><a href='javascript:' onclick='remove(this)' title='移除'>" +
			  "<i class='fa fa-remove'></i></a></td></tr>";
		  $("#batTable").append(tr);
	  }
	  function newadd() {
	      var tr = "<li><strong><label style='color:#999;'>批量绑定：</label></strong></td><td><label>端口：</label><input type='text' name='sitePort' value='80' class='form-control text_md'   maxlength='6' txt='端口'/> " +
			 "<label> 域名：</label><input type='text' name='Domain' class='form-control text_md' txt='域名'/><a href='javascript:' onclick='remove(this)' title='移除'>" +
			 "<i class='fa fa-remove'></i></a></li>";
		  $("#batTr").after(tr);
	  }
	  function remove(obj) {
		  $(obj).parent().remove();
	  }
	  function checkinfo()//Detect whether the domains and ports is empty;
	  {
		  for (i = 0; i < arguments.length; i++) {
			  var arr = document.getElementsByName(arguments[i]);
			  for (j = 0; j < arr.length; j++) {
				  if (arr[j].value == "") { alert(arr[j].txt + "未填写"); arr[j].focus(); return false; }
			  }
		  }
		  return true;
	  }
	  function checkValue(obj, t, Mark)//要检测的控件对象,显示提示信息用的span,标识自身，以便后台调用相应方法处理
	  {
		  //为空也不前台判断，全交于后台判断，避免伪造信息
		  $.ajax({
			  type: "post",
			  url: "CreateSite.aspx",
			  data: { data: obj.value, mark: Mark },
			  success: function (data) { $("#" + t).html(data); },
			  error: {}
		  });//ajax end;
	  }

	  function disParent(url)//快云框架调用该页面,判断其自身是否被框架调用
	  {
	      if (parent)
	      { parent.CreateSuccess(); }
	      else { location = locationl; }
	  }
	  function back() {
	      url = "Default.aspx";
	      if (getParam("remote") == "true") {
	          url += "?remote=true"
	      }
	      location = url;
	  }
  </script>
 
</div>
</div>
</asp:Content>