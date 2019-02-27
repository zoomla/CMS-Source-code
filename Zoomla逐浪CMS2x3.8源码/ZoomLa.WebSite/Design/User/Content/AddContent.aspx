<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddContent.aspx.cs" Inherits="Design_User_Content_AddContent" MasterPageFile="~/Design/Master/User.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>添加内容</title>
<script src="/Plugins/Ueditor/ueditor.config.js"></script>
<script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<link href="/design/res/css/user.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="siteinfo addcontent container">
<ol class="breadcrumb">
    <li><a href="/Design/User/?SiteID=<%:SiteID %>">动力模块</a></li>
    <li><a href="list.aspx?SiteID=<%:SiteID %>">内容列表</a></li>
    <li class="active">内容管理</li>
</ol>
<table id="mainTable" class="table table-bordered table_padding0 addcontent_modeltale" >
		<tr>
			<td colspan="2" class="text-center">
				<asp:Label ID="Tips_L" runat="server"></asp:Label>
			</td>
		</tr>
		<tr>
	<td class="text-right">所属节点</td>
		<td>
			<asp:Label ID="Node_L" runat="server" Text=""></asp:Label>
		</td>
	</tr>
		<tr>
			<td style="overflow-x:hidden;" class="col-sm-1 col-xs-1 text-right"><asp:Label ID="bt_txt" runat="server" Text="标题"></asp:Label>&nbsp;&nbsp;</td>
			<td class="col-sm-11 col-xs-11">
				<asp:TextBox ID="txtTitle" style="background:url(/Images/bg1.gif) repeat-x;" CssClass="form-control m715-50" onkeyup="isgoEmpty('txtTitle','span_txtTitle');Getpy('txtTitle','PYtitle')"  runat="server"></asp:TextBox>
				<span class="vaild_tip">*</span>
				<a href="javascript:;" id="Button11" class="btn btn-info btn-sm" onclick="ShowTitle()" ><i class="fa fa-info"></i> 标题属性</a>
				<button type="button" class="btn btn-info btn-sm" onclick="ShowSys();"><i class="fa fa-list"></i></button>
				<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTitle" ErrorMessage="标题不能为空" ForeColor="Red" Display="Dynamic" />
				<span id="span_txtTitle" name="span_txtTitle"></span>
				<asp:HiddenField ID="ThreadStyle" runat="server" />
				<div id="duptitle_div" class="alert alert-warning" style="position: absolute;margin-left:315px;display:none;">
					<ul id='duptitle_ul'></ul>
				</div>
			</td>
		</tr>
		<tbody id="Sys_Fied" runat="server" style="display:none;">
		<tr>
			<td class="text-right"><asp:Label ID="py" runat="server" Text="拼音缩写"></asp:Label>&nbsp;&nbsp;</td>
			<td>
				<asp:TextBox ID="PYtitle" CssClass="form-control m715-50" runat="server" />
			</td>
		</tr>
	   <tr id="spec_tr">
			<td class="text-right">所属专题&nbsp;&nbsp;</td>
			<td>
				<div class="specDiv"></div>
				<span id="specbtn_span"><asp:Literal ID="SpecInfo_Li" runat="server"></asp:Literal></span>
				<asp:HiddenField ID="Spec_Hid" runat="server" />
			</td>
		</tr>
		<tr runat="server">
			<td class="tdbgleft" align="right"> 
				<asp:Label ID="gjz_txt" runat="server" Text="关键字"></asp:Label>
				&nbsp;
			</td>
			<td>                             
				<div id="OAkeyword"></div>
				<asp:TextBox ID="Keywords" runat="server" CssClass="form-control" /><span>(空格或回车键分隔，长度不超过10字符或5汉字)</span>  
			 </td>                        
		</tr>
		<tr runat="server">
			<td class="text-right"><asp:Label ID="Label4" runat="server" Text="副标题"></asp:Label>&nbsp;&nbsp;</td>
			<td>
				<asp:TextBox ID="Subtitle" CssClass="form-control m715-50" runat="server"></asp:TextBox>
			</td>
		</tr>
		</tbody>
		<asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
		<tr id="CreateHTML" runat="server">
			<td class="text-right">
				<asp:Label ID="Label1" runat="server" Text="生成"></asp:Label>&nbsp;
			</td>
			<td>
				<asp:CheckBox ID="quickmake" runat="server" Checked="false" Text="是否立即生成" />&nbsp;
			</td>
		</tr>
    <tr>
        <td></td>
        <td>
            <asp:HiddenField runat="server" ID="RelatedIDS_Hid" />
            <asp:Button runat="server" CssClass="btn btn-primary" ID="EBtnSubmit" Text="添加项目" OnClick="EBtnSubmit_Click" />
            <a href="List.aspx?SiteID=<%:SiteID %>" class="btn btn-default">返回列表</a>
        </td>
    </tr>
	</table>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script"></asp:Content>