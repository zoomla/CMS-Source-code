<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowAudit.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.FreeFlow.FlowAudit" MasterPageFile="~/User/Empty.master" %>


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
            <li><a href="<%=Request.RawUrl %>">流程审批</a>
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
                <tr runat="server" id="formop_tr">
                    <td class="text-right">操作</td>
                    <td colspan="5">
                        <asp:Button runat="server" ID="SaveForm_Btn" Text="修改表单" OnClick="SaveForm_Btn_Click" CssClass="btn btn-primary"  Visible="false"/>
                        <input type="button" value="显示公文" onclick="TogWord();" class="btn btn-info" id="wordbtn" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <iframe src="/Plugins/Office/office.aspx?ID=<%=appID%>" class="wordifr"></iframe>
                    </td>
                </tr>
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
       <%--     <tr><td class="text-right">公文编辑器：</td><td><input type="button" onclick="ShowWord();" class="btn btn-info" value="打开公文" /></td></tr>--%>
            <tr><td class="text-right">附件：</td><td runat="server" id="publicAttachTD" colspan="7">
                <div style="margin-top:10px;" id="uploader" class="uploader"><ul class="filelist"></ul></div>
                <asp:HiddenField runat="server" ID="Attach_Hid" /></td></tr>
            <tr id="signTr" runat="server" visible="false">
                <td class="text-right">签章：</td>
                <td colspan="7">
                    <asp:RadioButtonList runat="server" ID="signRadio" RepeatDirection="Horizontal"></asp:RadioButtonList>
                    <span runat="server" id="signTrRemind" visible="false">你尚未配置个人签章</span>
                </td>
            </tr>
            <tr runat="server" id="remindTr"><td class="text-right">审核意见：</td>
                <td colspan="7">
                <asp:TextBox runat="server" ID="remindT" TextMode="MultiLine" Width="500px" Height="100px"></asp:TextBox></td></tr>
            <tr runat="server" id="Free_OP_Tr" visible="false"><td class="text-right">自由流程：</td>
                    <td colspan="7">
                    <input type="button" runat="server" id="Free_Next_Btn" value="继续流转" class="btn btn-primary" onclick="disWin(url);" />
                    <asp:Button runat="server" ID="Free_Sure_Btn" Text="文件归档" class="btn btn-primary opBtn" OnClick="Free_Sure_Btn_Click" OnClientClick="return sureF('确定归档文件吗!!');"/>
                    </td>
                </tr>
            <tr runat="server" id="AdminFree_OP_Tr" visible="false">
                <td class="text-right">公文流程：</td>
                <td colspan="7">
                    <input type="button" runat="server" value="选择主办人" class="btn btn-primary" onclick="disWin(url);" />
                </td>
            </tr>
            <tr runat="server" id="opBar"><td class="text-right">操作：</td>
                <td colspan="7">
                    <asp:Button runat="server" ID="agreeBtn" Visible="false" Text="同意" CssClass="btn btn-primary opBtn" OnClick="agreeBtn_Click" OnClientClick="return sureF('请点击确认同意!!');"/>
                    <asp:Button runat="server" ID="rejectBtn" Visible="false" Text="拒绝" CssClass="btn btn-primary opBtn" OnClick="rejectBtn_Click" OnClientClick="return sureF('请点击确认拒绝!!');"/>
                    <span runat="server" id="rollBackSpan" visible="false" class="opSpan">
                    回退至：
                    <asp:DropDownList runat="server" ID="rollBackDP"></asp:DropDownList>
                    <asp:Button runat="server" ID="rollBackBtn" Visible="false" Text="回退" CssClass="btn btn-primary opBtn" OnClick="rollBackBtn_Click" OnClientClick="return sureF('确定要回退吗!!');"/>
                    </span>
                        <span runat="server" id="zjSpan" visible="false" class="opSpan"><!--只能转交给之后的步骤-->
                        转交至：
                    <asp:DropDownList runat="server" ID="zjDP"></asp:DropDownList>
                    <asp:Button runat="server" ID="zjBtn" Text="转交" CssClass="btn btn-primary opBtn" OnClick="zjBtn_Click" OnClientClick="return sureF('确定要转交吗!!');"/>
                    </span>
                </td>
            </tr>
            <tr runat="server" id="ccOPBar" visible="false">
                <td class="text-right">操作：</td>
                <td colspan="7">
                    <asp:Button runat="server" CssClass="btn-primary" ID="ccUser_Btn" Text="批复" OnClick="ccUser_Btn_Click" OnClientClick="return CCUserCheck();"/>
                    <asp:Label ID="ccUser_Lab" runat="server" Text="您已经处理过该文件了！！" ForeColor="Red" Visible="false"></asp:Label>
                </td>
            </tr>
        </tbody>
        </table>
        <div class="curDrag"></div>
        <!--审核进度-->
        <div id="main" style="margin:5px 0 0 0;">
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  GridLines="None"   Width="100%" CssClass="table table-striped table-bordered" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >
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
        <script type="text/javascript">
            var url = 'FreeProNext.aspx?AppID=<%:appID%>';
            function disWin(url) {
                var iTop = (window.screen.availHeight - 30 - 550) / 2;
                var iLeft = (window.screen.availWidth - 10 - 960) / 2;
                var myWin = window.open(url, 'newwindow', 'height=800, width=960,top=' + iTop + ',left=' + iLeft + ',toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');
            }
            //回复不能为空
            function CCUserCheck() {
                var flag = true;
                v = $("#remindT").val();
                if (v == "") {
                    flag = false;
                    alert("审核意见不能为空!!!"); $("#remindT").focus();
                }
                return flag;
            }
            function PostDate(id) {
                if (confirm("确定要修改时间吗?")) {
                    var a = "PostDate";
                    var v = id + ":" + $("#SignDate_T_" + id).val();
                    PostToCS(a, v, function (data) {
                        if (data == 0) { alert('修改失败!!'); }
                        else { alert('修改成功,重新加载!!'); location = location; }
                    });
                }
            }
            function PostToCS(a, v, callback) {
                $.ajax({
                    type: "Post",
                    data: { action: a, value: v },
                    success: function (data) {
                        callback(data);
                    },
                    error: function (data) {
                    }
                });
            }
        </script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
<style type="text/css">
    #AllID_Chk {display:none;}
    .wordifr {border:1px solid #000;width:100%;width:1px;height:1px;}
</style>
<script type="text/javascript" src="/JS/OAKeyWord.js"></script>
<script type="text/javascript" src="/JS/ICMS/ZL_Common.js"></script>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/JS/chinese.js"></script>
<script type="text/javascript" src="/JS/Common.js"></script>
<script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
<script type="text/javascript" src="/JS/ZL_Content.js"></script>
<link href="/JS/Controls/ZL_Webup.css" rel="stylesheet" />
<script src="/JS/Controls/ZL_Webup.js"></script>
    <script>
        function TogWord(isshow) {//true显示
            var $ifr = $(".wordifr");
            if ($ifr.width() > 2)
            { $ifr.css("width", "1px"); $ifr.css("height", "1px"); $("#wordbtn").val("显示公文"); }
            else
            { $ifr.css("width", "100%"); $ifr.css("height", "700px"); $("#wordbtn").val("隐藏公文"); }
        }
        function ShowWord() {
            <%-- ShowDiag("/Plugins/Office/office.aspx?ID=<%=appID%>", "查看正文");--%>
            var $ifr = $(".wordifr");
            $ifr.css("width", "100%"); $ifr.css("height", "700px"); $("#wordbtn").val("隐藏公文");
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