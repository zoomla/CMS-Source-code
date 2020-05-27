<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FlowAudit.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Flow.FlowAudit" MasterPageFile="~/User/Empty.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>流程审批</title>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <ol id="BreadNav" class="breadcrumb navbar-fixed-top">
            <li><a href="/MIS/OA/Main.aspx">办公管理</a></li>
            <li><a href="FlowList.aspx">流程列表</a></li>
            <li><a href="<%=Request.RawUrl %>">流程审批</a>
              <%--   <span title="打印公文"><a href="/Common/PrintPage.aspx?appID=<%=appID %>" target="_blank">
                    <img class="startprint" onclick="doprint()" alt="" src="/Template/V3/style/images/print-btn.png" /></a></span>--%>
            </li>
        </ol>
        <div style="margin-top:60px;">
<%--            <ul class="nav nav-tabs">
                <li class="active"><a href="#Tabs0" data-toggle="tab">模型表单</a></li>
                <li><a href="#Tabs1" data-toggle="tab" onclick="ShowWord();">正文内容</a></li>
            </ul>--%>
        <table class="table table-striped table-bordered">
        <tr><td colspan="6" class="flow_title" style="text-align:center;font-family:'Microsoft YaHei';font-size:24px;"><asp:Label runat="server" ID="ProceName_L"></asp:Label></td></tr>
        <tr>
<%--        <td style="width:120px;" class="text-right">流程：</td><td><asp:Label runat="server" ID="titleL" /></td>--%>
            <td style="width:120px;" class="text-right">发起人：</td><td>
                <asp:Label runat="server" ID="sendManL" />
                <input type="button" class="btn btn-info" value="打开公文" onclick="ShowWord();" /></td>
            <td class="text-right">当前步骤：</td><td><asp:Label runat="server" ID="stepNameL" /></td>
            <td class="text-right">发起时间：</td><td><asp:Label runat="server" ID="createTimeL" /></td>
        </tr>
        <tr runat="server" visible="false" id="hqTr"><td class="text-right">已会签人: </td><td colspan="7"><asp:Label runat="server" ID="hqL" />
            <asp:Label runat="server" ID="unHql"></asp:Label></td></tr>
        <tr><td colspan="8" id="contentTr">
            <table class="table table-bordered table-striped">
                <asp:Literal runat="server" ID="Html_Lit" EnableViewState="false"></asp:Literal>
                <tr runat="server" id="formop_tr" visible="false"><td></td><td>
                    <asp:Button runat="server" ID="SaveForm_Btn" Text="修改表单" OnClick="SaveForm_Btn_Click" CssClass="btn btn-primary" />
                    </td></tr>
            </table>
            </td></tr><!--内容-->
            <tbody runat="server" id="audit_body">
                <tr><td class="text-right">附件：</td><td runat="server" id="publicAttachTD" colspan="7">
                 <div style="margin-top:10px;" id="uploader" class="uploader"><ul class="filelist"></ul></div>
                 <asp:HiddenField runat="server" ID="Attach_Hid" /></td></tr>
                <tr id="signTr" runat="server">
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
                       <input type="button" runat="server" id="Free_Next_Btn" value="继续流转" class="btn-primary" onclick="disWin(url);" />
                       <asp:Button runat="server" ID="Free_Sure_Btn" Text="文件归档" class="btn-primary opBtn" OnClick="Free_Sure_Btn_Click" OnClientClick="return sureF('确定归档文件吗!!');"/>
                     </td>
                 </tr>
                <tr runat="server" id="opBar"><td class="text-right">操作：</td>
                    <td colspan="7">
                        <asp:Button runat="server" ID="agreeBtn" Visible="false" Text="同意" CssClass="btn btn-primary opBtn" OnClick="agreeBtn_Click" OnClientClick="return sureF('请点击确认同意!!');"/>
                        <asp:Button runat="server" ID="rejectBtn" Visible="false" Text="拒绝" CssClass="btn btn-primary opBtn" OnClick="rejectBtn_Click" OnClientClick="return sureF('请点击确认拒绝!!');"/>
                        <span runat="server" id="rollBackSpan" visible="false" class="opSpan">
                        回退至：
                        <asp:DropDownList runat="server" ID="rollBackDP"></asp:DropDownList>
                        <asp:Button runat="server" ID="rollBackBtn" Visible="false" Text="回退" CssClass="btn-primary opBtn" OnClick="rollBackBtn_Click" OnClientClick="return sureF('确定要回退吗!!');"/>
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
                            <%#HasEditDate() %>
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
            //签章方法处理
            $().ready(function () {
                //$("#signRadio :radio:not(:first)").click(function () {
                //    $(".curDrag").show();
                //    $(".curDrag img").attr("src", $(this).parent().attr("picUrl"));
                //    var img = $(".curDrag img"); //获取img元素
                //    $(".curDrag").css("width", img.width());//设置宽度
                //});//click end;
                //$("#signRadio :radio:first").click(function () {
                //    $(".curDrag").hide();
                //    $("#curPosD").val("0|0");
                //});//click end;
                //$(".curDrag").draggable
                //    ({
                //        addClasses: false,
                //        axis: false,
                //        cursor: 'crosshair',
                //        //start: function () { alert(12);},
                //        //drag: function (){},
                //        stop: function () { GetPos(); },
                //        containment: 'parent'
                //    });
            });
            function GetPos() {
                //无时为auto:auto
                obj = $($(".curDrag")[0]);
                x = obj.css("top");
                y = obj.css("left");
                $("#curPosD").val(x + "|" + y);
            }
            //ID:x|y:img,
            function InitPos(v) {
                if (v == "") return;
                var imgArr = v.split(',');
                for (var i = 0; i < imgArr.length; i++) {
                    var signDiv = "<div class='drag' style='top:{x/};left:{y/};'><img src='{img/}'/></div>";
                    var x = imgArr[i].split(':')[1].split('|')[0];
                    var y = imgArr[i].split(':')[1].split('|')[1];
                    var img = imgArr[i].split(':')[2];
                    signDiv = signDiv = signDiv.replace("{x/}", x);
                    signDiv = signDiv.replace("{y/}", y);
                    signDiv = signDiv.replace("{img/}", img);
                    $("#contentDiv").append(signDiv);
                }
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
        <script type="text/javascript">
            var url = 'FreeProNext.aspx?AppID=<%:appID%>';
            function disWin(url) {
                var iTop = (window.screen.availHeight - 30 - 550) / 2;
                var iLeft = (window.screen.availWidth - 10 - 960) / 2;
                var myWin = window.open(url, 'newwindow', 'height=700, width=960,top=' + iTop + ',left=' + iLeft + ',toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');
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
#popImg {display:none;}
.tdbgleft{width:100px;}
.width1100{width:1100px;}
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
        function ShowWord() {
            ShowDiag("/Plugins/Office/office.aspx?ID=<%=appID%>", "查看正文");
        }
    </script>
</asp:Content>