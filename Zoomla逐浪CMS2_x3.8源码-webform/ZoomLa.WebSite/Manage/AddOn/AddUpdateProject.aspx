<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddUpdateProject.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="manage_AddOn_AddUpdateProject" EnableViewStateMac="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>查看或修改项目资料</title>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>

<script language="javascript" src="../../JS/calendar.js"></script>
<script type="text/javascript" src="../../JS/DatePicker/WdatePicker.js"></script>
<style type="text/css">
.style1
{background: #e0f7e5;/*line-height: 120%;*/    padding: 2px;width: 262px;height: 49px;        }
.style2{height: 49px;        }
.style3{background: #e0f7e5;/*line-height: 120%;*/    padding: 2px;width: 262px;}
</style>
<script language="javascript">
var tab = '<%= Request.QueryString["tab"]%>';
$(function () {
    var paytypes = $("#PayType_Hid").val().split(',');
    $(paytypes).each(function (i, d) {
        $("input[value='" + d + "']")[0].checked = true;
    });
})

</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs">
        <li class="active"><a href="#Tabs0" data-toggle="tab">基本资料</a></li>
        <li><a href="#Tabs1" data-toggle="tab">关联信息</a></li>
        <li><a href="#Tabs2" data-toggle="tab">项目需求</a></li>
        <li><a href="#Tabs3" data-toggle="tab">项目流程</a></li>
    </ul>
   
    <div class="tab-content">
<div class="tab-pane active" id="Tabs0">
<table class="table table-striped table-bordered table-hover">
<tbody>
    <tr>
		<td align="center" colspan="2" ><asp:Label ID="lblText" runat="server">修改项目</asp:Label></td>
    </tr>
    <tr>
        <td style="width:260px;"><strong>名称：</strong><br />项目名称</td>
        <td>
            <asp:TextBox ID="TxtProName" class="form-control text_300" runat="server" Width="222px" autofocus="true"/>
            <asp:RequiredFieldValidator ID="rfv" runat="server" ControlToValidate="TxtProName" Display="Dynamic" ErrorMessage="项目名称不可为空">项目名称不可为空</asp:RequiredFieldValidator>
        </td>
    </tr>
    <tr>
        <td><strong>项目类型：</strong><br />请选择项目类型</td>
        <td><asp:DropDownList ID="DDListType" runat="server" Width="145px" CssClass="form-control text_300"></asp:DropDownList>
            <asp:LinkButton ID="LkBtn" runat="server" PostBackUrl="ProjectsAddType.aspx"  CausesValidation="False">新增项目类型</asp:LinkButton>
        </td>
    </tr>
     <tr>
        <td><strong>项目价格：</strong><br />该项目的价格(人民币)</td>
        <td><asp:TextBox ID="TxtPrice" class="form-control text_300" runat="server" Width="222px" />元<asp:RangeValidator ID="RvPrice" runat="server" ControlToValidate="TxtPrice" Display="Dynamic"  ErrorMessage="价格格式不对" MaximumValue="99999" MinimumValue="0"></asp:RangeValidator>
        </td>
    </tr>
    <tr>
        <td><strong>项目经理：</strong><br />该项目的负责人或组长</td>
        <td><asp:TextBox ID="TxtLeader" class="form-control text_300" runat="server" Width="222px" /></td>
    </tr>
    <tr>
        <td><strong>技术负责人：</strong><br />该项目的技术负责人或技术组长</td>
        <td><asp:TextBox ID="WebCoding" class="form-control text_300" runat="server" Width="222px" /></td>
    </tr>
    </tbody>
    </table>
    </div>
    <div class="tab-pane" id="Tabs1">
<table class="table table-striped table-bordered table-hover">
<tbody>
    <tr>
        <td style="width:200px;"><strong>关联订单：</strong><br />相关联的订单ID</td>
        <td><asp:TextBox ID="TxtOrderID" class="form-control text_300" runat="server" Width="222px" />
            <asp:RangeValidator ID="Rv" runat="server" ControlToValidate="TxtOrderID"  Display="Dynamic" ErrorMessage="*" MaximumValue="9999" MinimumValue="0">请输入数字1-9999</asp:RangeValidator>
        </td>
    </tr>        
    <tr>
        <td><strong>关联会员：</strong><br />相关联的会员ID</td>
        <td><asp:TextBox ID="TxtUserID" class="form-control text_300" runat="server" Width="222px" />
            <asp:RangeValidator ID="RvTxtUser" runat="server" ControlToValidate="TxtUserID" ErrorMessage="*" MaximumValue="9999" MinimumValue="0">请输入数字1-9999</asp:RangeValidator>
        </td>
    </tr>
    <tr runat="server" id="info">
        <td><strong>所属客户：</strong></td>
        <td>    
            <table>
                <tr><td align="right" style="font-weight:bold"><asp:Label ID="lblName" runat="server" Text="Label"></asp:Label></td></tr>
            </table>
        </td>
    </tr>
    </tbody>
    </table>
    </div>
    <div class="tab-pane" id="Tabs2">
<table class="table table-striped table-bordered table-hover">
<tbody>
     <tr>
        <td><strong>项目要求：</strong><br />项目需求说明</td>
        <td><textarea cols="80" id="TxtRequire" width="80px" height="82px" name="infoeditor" rows="30" runat="server" readonly="readonly"></textarea>
            <%=Call.GetUEditor("TxtRequire",4) %>
        </td>

    </tr>
    </tbody>
    </table>
    </div>
    <div class="tab-pane" id="Tabs3">
<table class="table table-striped table-bordered table-hover">
<tbody>
    <tr>
        <td style="width:260px;"><strong>审核状态：</strong><br />审核中.已通过审核</td>
        <td style="padding-top:15px;">
            <asp:RadioButtonList ID="RBLAudit" runat="server" RepeatDirection="Horizontal" Width="222px" AutoPostBack="True">
                <asp:ListItem Value="1">等待审核</asp:ListItem>
                <asp:ListItem Value="2">通过审核</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td><strong>项目状态：</strong><br />未启动:已通过审核,等待启动中.<br />启动:项目开始做了.<br />完成-2.<br />存档:存档后该项目不可再度修改</td>
        <td>
            <asp:RadioButtonList ID="RBListStatus" runat="server" AutoPostBack="True">
                <asp:ListItem Value="0">未启动</asp:ListItem>
                <asp:ListItem Value="1">启动</asp:ListItem>
                <asp:ListItem Value="2">完成</asp:ListItem>
                <asp:ListItem Value="3">存档</asp:ListItem>
            </asp:RadioButtonList>
        </td>
    </tr>
    <tr>
        <td ><strong>启动时间：</strong><br />通过审核后，项目开工的时间</td>
        <td><asp:TextBox ID="TxtBeginTime" class="form-control text_300" runat="server" Width="222px" onfocus="calendar(this)"/></td>
    </tr>
    <tr>
        <td ><strong>当前进度：</strong><br /></td>
        <td><asp:DropDownList ID="DDListProcess" runat="server" CssClass="form-control text_300" Width="150px"></asp:DropDownList><a href="ProjectsProcesses.aspx?id=<%=GetID() %>">查看详情</a></td>
    </tr>
    <tr>
        <td ><strong>完成时间：</strong><br /></td>
        <td><asp:TextBox ID="TxtCompleteTime" class="form-control text_300" runat="server" Width="222px" onfocus="calendar(this)"/></td>
    </tr>
    <tr>
        <td ><strong>评分：</strong><br />请输入0-100间的数字</td>
        <td class="tdbg"><asp:TextBox ID="TxtRating" class="form-control text_300" runat="server" Width="222px"/>
            <asp:RangeValidator ID="RvRating" runat="server" ControlToValidate="TxtRating" 
                ErrorMessage="*" MaximumValue="99" MinimumValue="0">请打分0-99之间</asp:RangeValidator>
        </td>
    </tr>
    <tr>
        <td ><strong>评价：</strong><br />项目完成后，客户对它的评价</td>
        <td><asp:TextBox ID="TxtEvaluation" class="form-control text_300" runat="server" Width="380px" Height="84px" TextMode="MultiLine"/></td>
                   
    </tr>
    <tr>
        <td ><strong>申请时间：</strong><br /></td>
        <td><asp:TextBox ID="TxtApplicationTime" class="form-control text_300" runat="server" Width="222px" onfocus="calendar(this)" ToolTip="选择申请时间" /></td>
    </tr>
    <asp:Literal ID="lblHtml" runat="server"></asp:Literal>
     </tbody>
</table>
    </div>
        </div>
<div class="clearbox"></div>
<div style=" text-align:center; width:792px">
    <asp:Button ID="BtnCommit" 
        runat="server" Text="修改"  class="btn btn-primary" onclick="BtnCommit_Click"/>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="BtnBack" runat="server" Text="返回" class="btn btn-primary" onclick="Button2_Click" />
</div>
</asp:Content>
