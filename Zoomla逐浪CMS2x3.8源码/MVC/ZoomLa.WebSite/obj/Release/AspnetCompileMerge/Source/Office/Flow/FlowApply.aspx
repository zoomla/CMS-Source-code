<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowApply.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Flow.FlowApply" MasterPageFile="~/Common/Common.master"  ValidateRequest="false"%>
<%@ Register Src="~/Office/Tlp/defTlp.ascx" TagPrefix="oa" TagName="defTlp" %>
<%@ Register Src="~/Office/Tlp/send.ascx" TagPrefix="oa" TagName="send" %>
<%@ Register Src="~/Office/Tlp/rece.ascx" TagPrefix="oa" TagName="rece" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
    <title>表单申请</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
        <li><a href="/Office/Main.aspx">办公管理</a></li>
        <li><a href="FlowList.aspx">流程管理</a></li>
        <li class="active">
            <asp:Label runat="server" ID="CurPage_L"></asp:Label>
            [<a href="javascript:;" onclick="ShowImgDiag(<%=ProID %>)">查看流程图</a>]</li>
    </ol>
    <div style="height:40px;"></div>
    <asp:Panel runat="server" ID="OAForm_Div">
        <oa:send runat="server" ID="ascx_send" Visible="false" />
        <oa:rece runat="server" ID="ascx_rece" Visible="false" />
        <oa:defTlp runat="server" ID="ascx_def" Visible="false" />
    </asp:Panel>
    <table class="table table-bordered table_red1">
        <tr><td class="text-right">公文编辑器：</td><td><input type="button" onclick="ShowWord();" class="btn btn-info" value="打开公文" /></td></tr>
        <tr runat="server" id="attach_tr">
            <td class="text-right td_md">附件：</td>
            <td>
                <input type="button" id="upfile_btn" class="btn btn-info" value="添加文件" />
                <div style="margin-top: 10px;" id="uploader" class="uploader">
                    <ul class="filelist"></ul>
                </div>
                <asp:HiddenField runat="server" ID="Attach_Hid" />
            </td>
        </tr>
        <tr runat="server" id="SelUser_Tr">
            <td>
                <input type="button" class="btn btn-primary" onclick="selRuser();" value="选择主办人" /></td>
            <td>
                <asp:Label runat="server" ID="ReferUser_T" Style="height: 60px; word-wrap: break-word;"></asp:Label><asp:HiddenField runat="server" ID="ReferUser_Hid" />
            </td>
        </tr>
        <tr>
            <td class="text-right">操作：</td>
            <td>
                <asp:Button runat="server" ID="Save_Btn" OnClientClick="return CheckData();" Text="添加发文" OnClick="Save_Btn_Click" CssClass="btn btn-primary" />
                <a href="FlowList.aspx" class="btn btn-default">返回列表</a></td>
        </tr>
    </table>
     <asp:HiddenField runat="server" ID="FName_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<script type="text/javascript" src="/JS/OAKeyWord.js"></script>
<script type="text/javascript" src="/JS/ICMS/ZL_Common.js"></script>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/JS/chinese.js"></script>
<%--<script type="text/javascript" src="/JS/Common.js"></script>--%>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" src="/JS/ZL_Content.js"></script>
<script src="/JS/Controls/ZL_Webup.js"></script>
<script src="/JS/ZL_Regex.js"></script>
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
<script>
    $(function () {
        ZL_Webup.config.json.ashx = "action=OAattach";
        $("#upfile_btn").click(ZL_Webup.ShowFileUP);
    })
    function AddAttach(file, ret, pval) { return ZL_Webup.AddAttach(file, ret, pval); }
    function WordBack(fname) { $("#FName_Hid").val(fname); }
    function ShowWord() {
        var appid=parseInt("<%:Mid%>");
        var url="/Plugins/Office/office.aspx?Action=add&ProID=<%=ProID%>&fname=" + $("#FName_Hid").val();
        if (appid > 0) {
            var url = "/Plugins/Office/office.aspx?ID=" + appid;
        }
        ShowDiag(url, "正文管理");
    }
    var diag = new ZL_Dialog();
    function ShowImgDiag(proID) {
        diag.title = "查看流程图";
        diag.width = "diag";
        diag.url = "ImgWorkFlow.aspx?proid=" + proID;
        diag.maxbtn = false;
        diag.ShowModal();
    }
    function CheckData() {
        if ($("#ReferUser_Hid").length > 0) {
            var val = $("#ReferUser_Hid").val();
            if (ZL_Regex.isEmpty(val)) {
                alert("主办人不能为空"); return false;
            }
        }
        if (ZL_Regex.isEmpty($("#Title_T").val())) {
            alert("标题不能为空");
            return false;
        }
        if (!confirm("确定要保存吗?")) { return false; }
        return true;
    }
    //------
    function selRuser() {
        ShowComDiag("/Common/Dialog/SelStructure.aspx?Type=AllInfo#ReferUser", "选择主办人");
    }
    function UserFunc(json, select) {
        return user.deal_def(json, select);
    }
</script>
</asp:Content>
