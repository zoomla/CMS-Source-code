<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="IDCOrderInfo.aspx.cs" Inherits="ZoomLaCMS.Manage.Shop.IDC.IDCOrderInfo" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>订单详情</title>
    <style>
    .iserver{background-color:red;color:#fff;width:17px;display:inline-block;font-size:12px;border-radius:50%;text-align: center;}
    </style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<ol id="BreadNav" class="breadcrumb navbar-fixed-top">
    <li><a href='<%=CustomerPageAction.customPath2 %>Main.aspx'><%=Resources.L.工作台 %></a></li>
    <li><a href='../ProductManage.aspx'>商城管理</a></li>
    <li><a href='IDCOrder.aspx'>订单管理</a></li>
    <li class='active'><a href='" + Request.RawUrl + "'>订单详情</a></li>
    <div class="pull-right">
        <asp:LinkButton runat="server" ID="SendMail_B" title="发送邮件" OnClientClick="return confirm('确定发送邮件吗？');" OnClick="SendMail_B_Click"><i class="fa fa-envelope-o fa-fw"></i></asp:LinkButton>
        <asp:LinkButton runat="server" ID="DownWord_B" title="下载Word文档" OnClick="DownWord_B_Click"><i class="fa fa-file-word-o"></i></asp:LinkButton>
        <asp:LinkButton runat="server" ID="ExportExcel_B" title="导出Excel表格" OnClick="ExportExcel_B_Click"><i class="fa fa-file-excel-o fa-fw" ></i></asp:LinkButton>
        <a href="javascript:;" onclick="showiserver('<%:Mid %>');"><i class="fa fa-commenting-o"></i> 服务记录<%=GetiServerNum() %></a>
    </div>
</ol>
<%--<ul class="nav nav-tabs" role="tablist">
    <li class="active"><a href="#OrderState" role="tab" data-toggle="tab">订单状态</a></li>
   <li><a href="#Selled" role="tab" data-toggle="tab">售后管理</a></li>
</ul>--%>
<div class="tab-content">
    <div class="tab-pane active" id="OrderState">
        <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="3" align="center" class="title">
                <asp:Label ID="HeadTitle_L" runat="server"></asp:Label>
            </td>
        </tr>
        <tr><td style="width:400px;">绑定用户：<asp:Label runat="server" ID="IDC_UName_L"></asp:Label></td>
            <td style="width:300px;">商品名称：<asp:Label runat="server" ID="IDC_Proname_L"></asp:Label></td>
            <td>付款状态：<asp:Label ID="Paymentstatus" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>绑定域名：<asp:TextBox runat="server" ID="IDC_Domain_T" CssClass="form-control text_300"></asp:TextBox></td>
            <td>需开发票：<asp:Label ID="Invoiceneeds" runat="server"></asp:Label></td>
            <td>已开发票：<asp:Label ID="Developedvotes" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td>生效日期：<asp:TextBox runat="server" ID="IDC_STime_T" CssClass="form-control text_300" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd' });" /></td>
            <td>到期日期：<asp:Label runat="server" ID="IDC_ETime_L" /></td>
            <td>订单金额：<asp:Label runat="server" ID="OrderAmount_L"></asp:Label></td>
        </tr>
        <tr><td colspan="3" class="text-center"><div runat="server" id="prog_order_div"></div></td></tr>
</table>
    </div>
    <div role="tabpanel" class="tab-pane" id="Selled">
            <div class="panel panel-primary">
                <div class="panel-heading"><i class="fa fa-chevron-left"></i> 退款操作</div>
                <div class="panel-body">
                    <div>用户退款理由：<asp:Label ID="DrawBackStr" runat="server"></asp:Label></div>
                    <div>退款处理记录：<asp:Label ID="isCheckRe_L" runat="server"></asp:Label></div>
                </div>
                <div class="panel-footer">
                    <asp:Button ID="CheckReturn" Enabled="false" CssClass="btn btn-primary" runat="server" Text="确认退款" OnClientClick="return ShowDrawDiag(1);" />
                    <asp:Button ID="UnCheckRetrun" Enabled="false" CssClass="btn btn-primary" Width="100" runat="server" Text="拒绝退款" OnClientClick="return ShowDrawDiag(2);" /></div>
            </div>
        </div>
</div>
<table class="table table-striped table-bordered table-hover">
        <tr>
            <td><i class="fa fa-sticky-note-o"></i> 详记</td>
            <td>
                <div class="tab-pane" id="tab2">
                    <asp:TextBox runat="server" ID="Ordermessage_T" CssClass="form-control"  TextMode="MultiLine" placeholder="备注留言"></asp:TextBox>
                </div>
            </td>
        </tr>
       <tr>
            <td><i class="fa fa-pencil"></i> 内注</td>
           <td>
                <div>
                    <asp:TextBox runat="server" ID="Internalrecords_T" CssClass="form-control" TextMode="MultiLine" placeholder="内部记录"></asp:TextBox>
                </div>
            </td>
        </tr>
        <tr>
            <td class="td_m">订单流程</td>
            <td>
                <asp:Button ID="OS_Normal_Btn" CssClass="btn btn-info" runat="server" Text="重启订单" OnClick="OS_Normal_Btn_Click" OnClientClick="return confirm('确定重启订单吗,订单与付款状态将还原');" />
                <input runat="server" type="button" id="IDC_Complete_Btn" class="btn btn-danger" value="完结订单" onclick="idc.complete();" />
                <asp:Button runat="server" ID="IDC_Complete_Submit" OnClick="IDC_Complete_Submit_Click" Style="display: none;" />
                <asp:Label runat="server" ID="IDC_Complete_L" Style="display: none;"></asp:Label>
            </td>
        </tr>
   <%--     <tr>
            <td>支付状态</td>
            <td>
                <asp:Button runat="server" ID="Pay_Cancel_Btn" CssClass="btn btn-info" Text="取消支付" OnClientClick="return confirm('确定要变更状态为未支付吗?');" OnClick="Pay_Cancel_Btn_Click"/>
                <asp:Button runat="server" ID="Pay_Has_Btn" CssClass="btn btn-info" Text="已经支付" OnClick="Pay_Has_Btn_Click" OnClientClick="return confirm('确定修改为已支付吗');" />
            </td>
        </tr>--%>
        <tr><td>IDC操作</td><td>
                <a href="javascript:;" class="btn btn-primary" onclick="idc.showren();">订单续费</a>
                <asp:Button runat="server" ID="IDC_Save_Btn" Text="保存修改" OnClick="IDC_Save_Btn_Click" CssClass="btn btn-primary"/></td></tr>
        <tr>
            <td>附加操作</td>
            <td>
                <a runat="server" id="give_score_a" class="btn btn-info">赠送积分</a>
                <a runat="server" id="give_purse_a" class="btn btn-info">现金返利</a>
                <asp:Button ID="OS_Invoice_Btn" CssClass="btn btn-info" runat="server" Text="已开发票" OnClick="OS_Invoice_Btn_Click" />
                <a href="addon/printorder.aspx?id=<%:Mid %>" target="_blank" class="btn btn-info">打印订单</a>
                
            </td>
        </tr>
        <tr runat="server" visible="false">
            <td colspan="5" class="text-center">
                <asp:Button ID="btnPre" runat="server" Text="上一个订单" OnClick="btnPre_Click" CssClass="btn btn-primary" />
                <asp:Button ID="btnNext" runat="server" Text="下一个订单" OnClick="btnNext_Click" CssClass="btn btn-primary" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<link href="/Plugins/Third/alert/sweetalert.css" rel="stylesheet" />
<script src="/Plugins/Third/alert/sweetalert.min.js?v=1"></script>
<script src="/JS/Controls/ZL_Dialog.js"></script>
<script src="/JS/Controls/ZL_Array.js"></script>
<script src="/JS/Controls/Control.js"></script>
<script src="/JS/DatePicker/WdatePicker.js"></script>
<script src="/JS/Controls/Textarea.js"></script>
<script>
$(function () {
    $("#Internalrecords_T").autoHeight();
    $("#Ordermessage_T").autoHeight();
    $("#msg_tabs a").click(function () {
        $("#msg_tabs a").removeClass("active");
        $(this).addClass("active");
        $("#msg_content .tab-pane").removeClass("active");
        $($(this).data("tar")).addClass("active");
    });
})
function showuinfo(uid) {
    var url = siteconf.path + "User/UserInfo.aspx?id=" + uid
    comdiag.maxbtn = false;
    comdiag.ShowModal(url, "会员信息");
}
function showiserver(mid) {
    var url = siteconf.path + "iServer/BiServer.aspx?orderid=" + mid
    comdiag.maxbtn = false;
    comdiag.ShowModal(url, "服务记录");
}
var idc = {};
idc.showren = function () { ShowComDiag("IDC_Ren.aspx?ID=<%:OrderNO%>", "续费"); }
idc.complete = function () {
    var text = $("#IDC_Complete_L").text();
    swal({ title: "确认结算", "text": text, type: "info", showCancelButton: true, confirmButtonColor: "#DD6B55", confirmButtonText: "确定结算", closeOnConfirm: false }, function () { $("#IDC_Complete_Submit").click(); });
}
</script>
</asp:Content>