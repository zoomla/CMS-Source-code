<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowView.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.FreeFlow.FlowView" MasterPageFile="~/MIS/OA.master" %>
<%@ Register Src="~/MIS/OA/Tlp/defTlp.ascx" TagPrefix="oa" TagName="defTlp" %>
<%@ Register Src="~/MIS/OA/Tlp/send.ascx" TagPrefix="oa" TagName="send" %>
<%@ Register Src="~/MIS/OA/Tlp/rece.ascx" TagPrefix="oa" TagName="rece" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>流程审批</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
            <li><a href="/MIS/OA/Main.aspx">办公管理</a></li>
            <li><a href="../Flow/FlowList.aspx">流程列表</a></li>
            <li><a href="<%=Request.RawUrl %>">公文查看</a>
                 <span title="打印公文"><a href="/Mis/OA/Flow/Print.aspx?appID=<%=appID %>" target="_blank">
                    <img class="startprint" onclick="doprint()" alt="" src="/Template/V3/style/images/print-btn.png" /></a></span>
            </li>
        </ol>
        <div style="margin-top:60px;">
            <asp:Panel runat="server" ID="OAForm_Div">
                <oa:send runat="server" id="ascx_send" visible="false" />
                <oa:rece runat="server" id="ascx_rece" visible="false" />
                <oa:deftlp runat="server" id="ascx_def" visible="false" />
            </asp:Panel>
            <table class="table table-bordered table-striped table-hover">
            <tr>
                <td class="text-right">发起人：</td><td><asp:Label runat="server" ID="SendMan_L"></asp:Label></td>
                <td class="text-right">当前步骤：</td><td><asp:Label runat="server" ID="stepNameL"></asp:Label></td>
                <td class="text-right">发文时间：</td><td><asp:Label runat="server" ID="createTimeL"></asp:Label></td>
            </tr>
           <tr runat="server" id="hqTr">
               <td class="text-right">主办人： </td>
               <td colspan="7"><asp:Label runat="server" ID="hqL" />
            <asp:Label runat="server" ID="unHql"></asp:Label></td></tr>
            <tbody runat="server" id="audit_body">
            <tr><td class="text-right">公文编辑器：</td><td><input type="button" onclick="ShowWord();" class="btn btn-info" value="打开公文" /></td></tr>
            <tr><td class="text-right">附件：</td><td runat="server" id="publicAttachTD" colspan="7">
                <div style="margin-top:10px;" id="uploader" class="uploader"><ul class="filelist"></ul></div>
                <asp:HiddenField runat="server" ID="Attach_Hid" /></td></tr>
        </tbody>
        </table>
        <div class="curDrag"></div>
        <!--审核进度-->
        <div id="main" style="margin:5px 0 0 0;">
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  GridLines="None"   Width="100%" CssClass="table table-striped table-bordered" OnPageIndexChanging="EGV_PageIndexChanging">
                <Columns>
               <%--     <asp:BoundField HeaderText="步骤序号" DataField="ProLevel" HeaderStyle-Width="8%"/>--%>
                    <asp:BoundField HeaderText="步骤名" DataField="ProLevelName" HeaderStyle-Width="20%" />
                    <asp:TemplateField HeaderText="处理结果" HeaderStyle-Width="20%">
                        <ItemTemplate>
                           <%#GetResult(Eval("Result")) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="处理人" DataField="UserName" HeaderStyle-Width="10%" />
                    <asp:BoundField HeaderText="备注" DataField="Remind" HeaderStyle-Width="30%"/>
                    <asp:TemplateField HeaderText="审批时间" HeaderStyle-Width="200px">
                        <ItemTemplate>
                           <%-- <%#HasEditDate() %>--%>
                            <%#Eval("CreateTime","{0:yyyy年MM月dd日}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                  </Columns>
                    <PagerStyle HorizontalAlign="Center"/>
                   <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
        </div>
    </div>
        <!--DataRegion-->
        <asp:HiddenField runat="server" ID="curPosD" Value="0|0" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
    #AllID_Chk {display:none;
    }
</style>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" src="/JS/ICMS/ZL_Common.js"></script>
<script type="text/javascript" src="/JS/ZL_Content.js"></script>
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
<script src="/JS/Controls/ZL_Webup.js"></script>
    <script>
        function ShowWord() {
            ShowDiag("/Plugins/Office/office.aspx?ID=<%=appID%>", "查看正文");
        }
        function sureF(s) {
            if (confirm(s)) {
                $(".opBtn").each(function () { o = this; setTimeout(function () { o.disabled = true; }, 100); });
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>

