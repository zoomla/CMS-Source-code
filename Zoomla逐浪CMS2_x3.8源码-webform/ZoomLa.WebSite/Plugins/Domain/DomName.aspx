<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DomName.aspx.cs" Inherits="Plugins_Domain_DomName" MasterPageFile="~/Manage/Site/SiteMaster2.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<style type="text/css">
.redStar {color: #ff0000;padding: 0 0 0 3px;}
.nochoose, .choose{	width: 100px;height: 22px;padding-bottom: 1px;padding-left: 1px;padding-right: 1px;padding-top: 1px;font-weight: normal;cursor: pointer;/*line-height: 120%;*/ font-size:14px;}
.nochoose { color: #ffffff;}
.choose{ background:#03a1f0;color: #ffffff; font-weight:bold;}
</style>
<link rel="stylesheet" href="/App_Themes/V3.css" type="text/css" media="all" />
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/JS/Common.js"></script>
<script type="text/javascript" src="site.js"></script>
<style type="text/css">
    #leftDiv {font-family: "Helvetica Neue", Helvetica, Arial, sans-serif;width:15%;float:left;margin-top:5px;}
    #leftDiv span:hover{text-decoration:underline;}
    #leftDiv a:active{text-decoration:none;}
    .topOption {background-color:#08C;color:#FFF;height:30px;width:100%;font-size:14px;cursor:pointer;border:solid;border-width:1px;border-color:grey;border-style:none none solid none;}
    .topWord {display:block;height:30px;line-height:30px;padding-left:8px;}
    .oneOption { background-color:#DFDFDF;color:#7E7E7E;height:30px;width:100%;font-size:14px;cursor:pointer;border:solid;border-width:1px;border-color:grey; border-style:none none solid none;}
    .oneWord {display:block;height:30px;line-height:30px;padding-left:24px;}
    .twoOption {height:30px;width:100%;font-size:14px;cursor:pointer;border:solid;border-width:1px;border-color:grey; border:none ; }
    .twoWord {color:#039DC2;display:block;height:30px;line-height:30px;padding-left:40px; }
    .optionChoose {background:#F2FBFD;}
    #rightDiv { float:right; margin-top:5px; width:84%;}
</style>
<script type="text/javascript">
    function frameInit(obj, name) {
        obj.height = window.frames[name].document.body.scrollHeight * 0.95
    }
    function openFrame(url) {
        $("#tab1Frame").attr("src", url);
    }
</script>
<title>IDC管理</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="site_main">
    <div id="leftDiv">
        <div class="panel panel-primary">
          <div class="panel-heading">域名管理</div>
          <div class="list-group">
            <a href="javascript:;" id="tabBtn1" onclick="ShowTabs(this,'tab1');openFrame('/Site/Domain.aspx?remote=true')" class="list-group-item">域名注册</a>
            <a href="javascript:;" class="list-group-item" id="tabBtn3" onclick="ShowTabs(this,'tab3')">域名管理</a>
            <a href="/Site/Default.aspx" target="_blank" class="list-group-item">智能建站</a>
              <a href="javascript:;" class="list-group-item" id="tabBtn8" onclick="ShowTabs(this,'tab1');openFrame('MySiteManage.aspx')">站点管理</a>
              <a href="javascript:;" class="list-group-item" id="tabBtn4" onclick="ShowTabs(this,'tab1');openFrame('ViewUserLog.aspx');">操作日志</a>
              <a href="javascript:;" class="list-group-item" id="tabBtn6" onclick="ShowTabs(this,'tab1');openFrame('ViewProduct.aspx');">购买服务</a>
              <a href="javascript:;" class="list-group-item" id="tabBtn7" onclick="ShowTabs(this,'tab1');openFrame('ViewHave.aspx')"">已购买服务</a>
              <a href="javascript:;" class="list-group-item" id="tabBtn5" onclick="ShowTabs(this,'tab5')">用户充值</a>
          </div>
        </div>
    </div>
    <div id="rightDiv">
    <div id="tab1">
        <iframe id="tab1Frame" src="/Site/Domain.aspx?remote=true" style="width:98%;height:700px;" frameborder=0 scrolling=yes></iframe>
</div>
<div id="tab3" style="display:none;">
        <div class="input-group text_300">
            <asp:TextBox runat="server" CssClass="form-control" ID="keyWord"  />
          <span class="input-group-btn">
            <asp:Button runat="server" ID="searchBtn" Text="搜索" OnClick="searchBtn_Click" CssClass="btn btn-default"/>
          </span>
        </div>
		
		<ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand"
		  OnRowCancelingEdit="EGV_RowCancelingEdit" PageSize="10" CssClass="table table-striped table-bordered table-hover margin_t5"
		EnableTheming="False" EmptyDataText="没有任何数据！" AllowSorting="True" CheckBoxFieldHeaderWidth="3%" EnableModelValidation="True" IsHoldState="false">
			<Columns>
			   <%-- <asp:BoundField HeaderText="ID" DataField="ID" />--%>
				 <asp:BoundField HeaderText="序号" DataField="ID" ReadOnly="true" />
			  <%--   <asp:BoundField HeaderText="站点ID" DataField="SiteID" ReadOnly="true" />--%>
				   <asp:TemplateField HeaderText="域名">
					<ItemTemplate>
						<a href="<%# "http://"+Eval("DomName") %>"  target="_blank" title="打开站点"><%#Eval("DomName") %></a>
					</ItemTemplate>
					   <EditItemTemplate>
						   <asp:Label runat="server" ID="lDomain" Text='<%#Eval("DomName").ToString().ToLower().Replace("www.","") %>'></asp:Label>
					   </EditItemTemplate>
				</asp:TemplateField>
				<asp:BoundField HeaderText="所属用户" DataField="UserName"/>
<%--                     <asp:TemplateField HeaderText="站点名">
					<ItemTemplate>
						<a href="SiteDetail.aspx?SiteName=<%#Server.UrlEncode(Eval("SiteName") as string) %>" target="_blank" title="站点详情"><%#Eval("SiteName") %></a>
					</ItemTemplate>
				</asp:TemplateField>--%>
				<asp:TemplateField HeaderText="到期日">
					<ItemTemplate>
						<%#DataBinder.Eval(Container.DataItem, "EndDate", "{0:yyyy年M月d日}") %>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="证书">
					<ItemTemplate>
					<a href="ViewCert.aspx?id=<%#Eval("ID") %>" target="_viewCert">查看证书</a>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField HeaderText="续费多久" Visible="false">
					<ItemTemplate>
					</ItemTemplate>
					<EditItemTemplate>
						<select name="periodDP">
							<option value="1">1年</option>
							<option value="2">2年</option>
							<option value="3">3年</option>
							<option value="4">4年</option>
							<option value="5">5年</option>
						</select>
					</EditItemTemplate>
				</asp:TemplateField>
			   <%--  <asp:BoundField HeaderText="到期日期" DataField="EndDate" />--%>
				 <asp:TemplateField HeaderText="操作">
					<ItemTemplate>
					   <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%#Container.DisplayIndex %>' CommandName="Edit2">续费</asp:LinkButton>
					</ItemTemplate>
					 <EditItemTemplate>
						 <asp:LinkButton runat="server" CommandArgument='<%#Container.DisplayIndex %>' CommandName="Renewals" OnClientClick="return confirm('你确定要续费该域名吗');">确定</asp:LinkButton>
						 <asp:LinkButton runat="server" CommandArgument='<%#Container.DisplayIndex %>' CommandName="Cancel">取消</asp:LinkButton>
					 </EditItemTemplate>
				 </asp:TemplateField>
			</Columns>
	</ZL:ExGridView> 
	</div>
<table id="templateTable" style="margin:auto;display:none;" >
	<tr><td>模板名：</td><td><span class="redStar">*&nbsp;</span><input type="text" id="tempName" name="tempName"  class="site_input" size="30" />
		<asp:DropDownList runat="server" ID="tempListDP" AutoPostBack="true" OnSelectedIndexChanged="tempListDP_SelectedIndexChanged"></asp:DropDownList> </td>
	</tr>
	<tr>
		<td>单位名称（中文名）：</td>
		<td><span class="redStar">*&nbsp;</span><input id="uname1" type="text" class="site_input" size="30" name="uname1" /></td>
	</tr>
	<tr>
		<td>单位名称（英文名）：</td>
		<td><span class="redStar">*&nbsp;</span><input id="uname2" type="text" class="site_input" size="30" name="uname2"  /></td>
	</tr>
	<tr>
		<td></td>
		<td style="color: red;">联系人中文名中至少含有1个中文字符，英文名信息中名和姓必须以空格分开。</td>
	</tr>
	<tr>
		<td>联系人（中文名）：</td>
		<td><span class="redStar">*&nbsp;</span><input id="rname1" type="text" class="site_input" size="30" name="rname1" /></td>
	</tr>
	<tr>
		<td>联系人（英文名）：</td>
		<td><span class="redStar">*&nbsp;</span><input id="rname2" type="text" class="site_input" size="30" name="rname2" /></td>
	</tr>
	<%--<tr class="CNAddr">
		<td>URL指向:</td>
		<td>
			<span class="redStar">*</span>
			<input id="urlId" type="text" class="site_input" size="30" name="url" value="http://www." />
		</td>
	</tr>--%>
	<tr>
		<td>电子邮箱：</td>
		<td><span class="redStar">*&nbsp;</span><input id="aemail" type="text" class="site_input" size="30" name="aemail"  /></td>
	</tr>
	<tr>
		<td>所属区域：</td>
		<td><span class="redStar">*</span>
			 <asp:DropDownList ID="DropDownList1" runat="server" class="dpclass"><asp:ListItem Value="01">中国</asp:ListItem></asp:DropDownList><br />
			 <span class="redStar">*</span>
			 <asp:DropDownList runat="server" ID="prvinceDP" ClientIDMode="Static" class="dpclass"></asp:DropDownList><br />
			 <span class="redStar">*</span>
			 <input type="text" id="cityText" name="cityText" class="site_input"/>
		</td>
	</tr>
	<tr>
		<td></td>
		<td style="color: red;">通迅地址（中文）信息中必须至少含有1个中文字符</td>
	</tr>
	<tr>
		<td>通迅地址（中文）：</td>
		<td><span class="redStar" style="position:relative;bottom:70px;">*</span>
			<textarea id="uaddr1" rows="4" cols="28" name="uaddr1" class="site_input" style="height:150px; margin-bottom:5px;" ></textarea>
		</td>
	</tr>
	<tr>
		<td>通迅地址（英文）：</td>
		<td><span class="redStar" style="position:relative;bottom:70px;">*</span>
			<textarea id="uaddr2" rows="4" cols="28" name="uaddr2" class="site_input" style="height:150px;" ></textarea>
		</td>
	</tr>
	<tr>
		<td>邮编：</td>
		<td><span class="redStar">*</span>
			<input id="uzip" type="text" name="uzip" class="site_input" size="30"  />
		</td>
	</tr>
	<tr>
		<td>手机：</td>
		<td><span class="redStar">*</span>
			<input id="uteln" type="text" class="site_input" name="uteln" />
		</td>
	</tr>
 <%--   <tr>
		<td>传真：</td>
		<td><span class="redStar">*</span>
		<input id="ufaxa" type="text" class="site_input" size="6" name="ufaxa"  style="width:60px;"/>--
		<input id="ufaxn" type="text" class="site_input" size="12" name="ufaxn" style="width:114px;"  />
		</td>
	</tr>--%>
	<tr>
		<td>操作：</td>
		<td>
			<asp:Button runat="server" ID="addTempBtn" Text="添加模板" Style="cursor: pointer; margin-left:12px;" CssClass="site_button"  OnClick="addTempBtn_Click"
				OnClientClick="return checkValue();"/>
		</td>
	</tr>
	  <tr><td colspan="2"></td></tr>
</table>
<div id="tab5" style="display:none;">
    <div class="input-group" style="width:220px;">
       <asp:TextBox runat="server" ID="chargeText" CssClass="form-control text_md" placeholder="请输入充值金额"/>
        <span class="input-group-btn">
             <asp:Button runat="server" ID="beginCharge" Text="充值" OnClick="beginCharge_Click" CssClass="btn btn-default" ValidationGroup="chargeG"/>
        </span>
    </div>
<asp:RequiredFieldValidator ID="RV1" runat="server" ControlToValidate="chargeText" Display="Dynamic" ForeColor="Red" ErrorMessage="不能为空!" ValidationGroup="chargeG"/>
<asp:RegularExpressionValidator ID="RV2" runat="server" ControlToValidate="chargeText"  Display="Dynamic" ForeColor="Red" ErrorMessage="必须为整数" ValidationExpression="^\d+(\d+)?$" ValidationGroup="chargeG"/>
</div>
</div>

<br />
<asp:HiddenField runat="server" ID="dataField" />
</div>
<script type="text/javascript">
    $(function () {
        p = getParam("Page");
        if (p) {
            $("#tabBtn" + p).trigger("click");
        }
        id = $("#<%=dataField.ClientID%>").val();
        if (id != "") {
            $("#" + id).trigger("click");
        }
        $("#chargeText").keydown(function () { if (event.keyCode == 13) { $("#beginCharge").click(); return false; } });
    })
    function ShowTabs(obj, id) {//Div切换
        $("#" + id).show().siblings().hide();
        $("#<%=dataField.ClientID%>").val(obj.id);
    }
    function setDefaultCheck(v) {
        //a = v.split(',');
        //for (var i = 0; i < a.length; i++) {
        //    $("input:[value=" + a[i] + "]").attr("checked", "checked");
        //}
    }
</script>
</asp:Content>
