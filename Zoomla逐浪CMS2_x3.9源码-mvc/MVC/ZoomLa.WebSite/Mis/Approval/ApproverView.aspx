<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproverView.aspx.cs" Inherits="ZoomLaCMS.MIS.Approval.ApproverView"  MasterPageFile="~/Common/Master/Empty.master"  %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
<title>审批详情</title>
<link href="/App_Themes/User.css" type="text/css" rel="stylesheet" />
<script type="text/javascript" src="/JS/MisView.js"></script>
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
        <ul><li><a href="Default.aspx?type=2">我的审批</a></li>
            <li><a href="Default.aspx?type=3">已归档</a></li>
            <li><a href="Default.aspx?type=8">抄送给我的</a></li>
            <li><a href="Default.aspx?type=7">审批设置</a></li>
        </ul>
          </div>
    </div>
    <div id="pro_right">
          <div class="Mis_Title">我的申请  >> 查看申请单 </div>
        <input type="hidden" id="txtPeson" runat="server" />
        <table class="table table-bordered"  style="padding-left:10px; padding-top:10px;">
            <tr><td>申请人：</td><td><asp:Label ID="TxtInputer" runat="server"></asp:Label></td></tr>
            <tr><td>申请部门：</td><td><asp:Label ID="Txtdepartment" runat="server"></asp:Label></td></tr>
            <tr><td>创建日期：</td><td><asp:Label ID="TxtTime" runat="server"></asp:Label></td></tr>
            <tr><td>申请流程：</td><td><asp:Label ID="TxtProcess" runat="server"></asp:Label></td></tr>
            <tr><td>申请内容：</td><td><asp:Label ID="TxtContent" runat="server"></asp:Label></td></tr>
            <tr><td>抄送：</td><td><asp:Label ID="TxtSend" runat="server"></asp:Label></td></tr> 
            <tr><td>处理结果：</td><td><asp:Label ID="resultL" runat="server" ></asp:Label></td></tr> 
            <tr><td colspan="2">
                <div id="appDiv" runat="server" style="margin-left:10px;">
                <asp:Button  runat="server" CssClass="btn btn-primary" Text="同意" OnClick="yesBtn_Click" ID="yesBtn"/>
                <asp:Button  runat="server" CssClass="btn btn-danger" Text="不同意" OnClick="noBtn_Click" ID="noBtn"/>
                </div>
                </td></tr>
        </table>
        <div id="resultDiv">
        <table class="table" style="text-align:center;">
             <tr><td>审批人</td><td>结果</td><td>备注</td><td>时间</td></tr>
        <asp:Repeater runat="server" ID="proRepeater">
            <ItemTemplate>
               <tr><td><%#Eval("UserName") %></td><td><%#GetResult(Eval("Result")) %></td><td><%#Eval("Remind") %></td><td><%#Eval("CreateTime") %></td></tr>
            </ItemTemplate>
        </asp:Repeater></table>
             <div style="clear:both;"></div>
             <div style="position:fixed;bottom:15px;"><asp:Literal runat="server" ID="pageHtmlLi"></asp:Literal></div> 
        </div>
<%--        <div style="text-align:center;"><asp:Button ID="Sends" Text="抄送" CssClass="i_bottom" OnClientClick="return PopupDiv2('div_share','lblChecked')" runat="server" />
            &nbsp;&nbsp;<asp:Button ID="btnReset" runat="server"  OnClick="btnReset_Click" CssClass="i_bottom" Text="取消"/></div>--%>

<div id="div_share" class="pop_box">
<div class="p_head">
<div class="p_h_title">人员选择</div>
<div class="p_h_close" onclick="HideDiv('div_share')">关闭</div>
</div>
<iframe src="/Mis/SelUser.aspx?OpenerText=txtPeson" width="420" height="120" scrolling="no" frameborder="0"></iframe>
<div class="p_bottom">
<asp:Button ID="BtnSubPeson" CssClass="btn btn-primary" runat="server" Text="确定"  OnClick="BtnSubPeson_Click"/>
</div>
</div>
</div>
</div>
</asp:Content>
