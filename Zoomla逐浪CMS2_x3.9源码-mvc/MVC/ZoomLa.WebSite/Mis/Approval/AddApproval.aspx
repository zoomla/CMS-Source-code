<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddApproval.aspx.cs" Inherits="ZoomLaCMS.MIS.Approval.AddApproval" MasterPageFile="~/Common/Master/Empty.master" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>新建申请单</title> 
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script src="/JS/MisView.js"></script> 
<script type="text/javascript">
function loadPage(id, url) {
    $("#" + id).addClass("loader");
        $("#" + id).append("Loading......");
        $.ajax({
            type: "get",
            url: url,
            cache: false,
            error: function () { alert('加载页面' + url + '时出错！'); },
            success: function (msg) {
                $("#" + id).empty().append(msg);
                $("#" + id).removeClass("loader");
            }
        });
    }
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="Meno">
<div id="pro_left">
<div class="new_tar">
<a href="AddApproval.aspx">新建申请单</a></div>
<div class="pro_li">
<ul>
<li><a href="Default.aspx?type=2">我的审批</a></li>
<li><a href="Default.aspx?type=3">已归档</a></li>
<li><a href="Default.aspx?type=8">抄送给我的</a></li>
<li><a href="Default.aspx?type=7">审批设置</a></li>
</ul>
</div>
</div>
<div id="pro_right">
<div class="Mis_Title">我的申请  >> 新建申请单 </div>
<div class="Approvalli">
    <table style="width:100%;"><tr><td style="width:150px;">申请人：</td><td><asp:Label ID="TxtInputer" runat="server"></asp:Label> </td></tr>
        <tr><td>申请部门：</td><td> 初始化向导 </td></tr>
        <tr><td>申请流程：</td><td> 
            <asp:Label ID="lblPro" runat="server"/> <a href="#" onclick="PopupDiv2('div_Procedure','lblChecked')">选择流程</a>  
            </td>  </tr>
        <tr><td>申请内容：</td><td> 
            <asp:TextBox ID="TxtContent" runat="server" TextMode="MultiLine"  Width="300" Height="50" CssClass="M_input" />
            <asp:RequiredFieldValidator  runat="server" ControlToValidate="TxtContent" Display="Dynamic" ErrorMessage="请填写内容" ForeColor="Red"/>
        </td></tr>
        <tr style="display:none;"><td>审批人</td><td><asp:TextBox ID="TxtApprover" runat="server" CssClass="M_input"></asp:TextBox><a href="javascript:void(0)" onclick="PopupDiv2('div_share','lblChecked')">选择</a>  </td></tr>
        <tr><td colspan="2"><asp:Button  Text="确定" runat="server" ID="Button" onclick="Button_Click" CssClass="i_bottom"/></td></tr>
    </table>
</div>

<div id="div_share" class="pop_box" style="width:500px;">
<div class="p_head">
<div class="p_h_title">人员选择</div>
<div class="p_h_close" onclick="HideDiv('div_share')">关闭</div>
</div>
<iframe src="/Mis/SelUser.aspx?OpenerText=TxtApprover" width="480" height="200" scrolling="no" frameborder="0"></iframe>
<div class="p_bottom">
<input type="button" value="确定" class="i_bottom" onclick="HideDiv('div_share')" />
</div>
</div>
<div id="div_Procedure" class="pop_box" style="width:500px;">
<div class="p_head">
<div class="p_h_title">流程选择</div>
<div class="p_h_close" onclick="HideDiv('div_Procedure')">关闭</div>
</div>
<div class="p_bodys">
    <asp:DropDownList ID="DrpType" runat="server" OnSelectedIndexChanged="DrpType_SelectedIndexChanged"></asp:DropDownList>
    <asp:Repeater ID="repProcedure" runat="server">
        <HeaderTemplate>
            <ul>
        </HeaderTemplate>
        <ItemTemplate>
            <li><a href="javascript:void(0)" onclick="GetProcedure('div_Procedure','<%#Eval("ID")%>','<%#Eval("ProcedureName")%>')"><%#Eval("ProcedureName")%></a></li>
        </ItemTemplate>
        <FooterTemplate>
            </ul>
        </FooterTemplate>
    </asp:Repeater>
</div>
</div>
</div> 
</div>
<input type="hidden" id="HidPro" runat="server" />
<asp:HiddenField ID="TxtResults" runat="server" />
</asp:Content>
