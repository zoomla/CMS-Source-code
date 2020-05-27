<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReadOffice.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.Office.ReadOffice" MasterPageFile="~/User/Empty.master"  %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>公文处理</title>
<script type="text/javascript" src="/JS/jquery-ui.min.js"></script>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
<style type="text/css">
* {font-family:'Microsoft YaHei';}
.drag{z-index:999;width:60px;height:1px;position:relative;}/*设为1,这样下方不会有错位*/
.curDrag {z-index:999;display:none;}
.opBtn {margin: 0 5px 0 5px;}
.opSpan {height:35px;line-height:35px;font-size:14px;font-family:'Microsoft YaHei';}
.btn {cursor:pointer;}
.btn :hover {background:blue;}
.filea{ margin-left:5px; margin-right:5px;}
.edui-default{ margin:auto;}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <div class="draftnav"><a href="/MIS/OA/Main.aspx">行政公文</a>/<span>公文详情</span>
            <span title="打印公文"><a href="/Common/PrintPage.aspx?appID=<%=Request.QueryString["AppID"] %>" target="_blank">
                <img class="startprint" onclick="doprint()" alt="" src="/Template/V3/style/images/print-btn.png" style="position:relative;width:80px;height:25px;top:5px;" /></a></span></div>
        <div style="margin:10px;">
        <table class="table table-bordered">
        <tr>
            <td style="width:120px;" class="text-right">标题：</td><td><asp:Label runat="server" ID="titleL" /></td>
            <td class="text-right">发起人：</td><td><asp:Label runat="server" ID="sendManL" /></td>
            <td class="text-right">当前步骤：</td><td><asp:Label runat="server" ID="stepNameL" /></td>
            <td class="text-right">发起时间：</td><td><asp:Label runat="server" ID="createTimeL" /></td>
        </tr>
        <tr runat="server" visible="false" id="hqTr"><td class="text-right">已会签人: </td><td colspan="7"><asp:Label runat="server" ID="hqL" />
            <asp:Label runat="server" ID="unHql"></asp:Label></td></tr>
        <tr><td colspan="8" id="contentTr">
            <div style="width:815px;margin:auto;" id="contentDiv">
                <asp:TextBox runat="server" Width="815" TextMode="MultiLine" ID="txt_Content" style="min-height:900px;" ></asp:TextBox>
                <div class="curDrag" style="height:1px;"><img src="#" /></div>
            </div>
            </td></tr><!--内容-->
        <tr><td class="text-right">附件：</td><td runat="server" id="publicAttachTD" colspan="7">
            <%--<asp:Button runat="server" ID="delAttachBtn" Text="删除附件" CssClass="btn btn-danger" Visible="false"/>--%>
                        </td></tr>
        <tr id="signTr" runat="server">
            <td class="text-right">签章：</td>
            <td colspan="7">
                <asp:RadioButtonList runat="server" ID="signRadio" RepeatDirection="Horizontal"></asp:RadioButtonList>
                <span runat="server" id="signTrRemind" visible="false">你尚未配置个人签章</span>
            </td>
        </tr>
        <tr runat="server" id="remindTr"><td class="text-right">审核意见：</td>
            <td colspan="7">
            <asp:TextBox runat="server" ID="remindT" CssClass="form-control" TextMode="MultiLine" Width="500px" Height="100px"></asp:TextBox></td></tr>
            <tr runat="server" id="Free_OP_Tr" visible="false">
                <td class="text-right">自由流程：</td>
                <td colspan="7">
                    <input type="button" runat="server" id="Free_Next_Btn" value="继续流转" class="btn btn-primary" onclick="disWin(url);" />
                    <asp:Button runat="server" ID="Free_Sure_Btn" Text="文件归档" class="btn btn-primary opBtn" OnClick="Free_Sure_Btn_Click" OnClientClick="return sureF('确定归档文件吗!!');" />
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
                <asp:Button runat="server" ID="rollBackBtn" Visible="false" Text="回退" CssClass="btn-primary opBtn" OnClick="rollBackBtn_Click" OnClientClick="return sureF('确定要回退吗!!');"/>
                </span>
                 <span runat="server" id="zjSpan" visible="false" class="opSpan"><!--只能转交给之后的步骤-->
                 转交至：
                <asp:DropDownList runat="server" ID="zjDP"></asp:DropDownList>
                <asp:Button runat="server" ID="zjBtn" Text="转交" CssClass="btn-primary opBtn" OnClick="zjBtn_Click" OnClientClick="return sureF('确定要转交吗!!');"/>
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
    </table>
        <!--审核进度-->
        <div id="main" style="margin:5px 0 0 0;">
            <ZL:ExGridView runat="server" ID="EGV" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"  EnableTheming="False"  
                 CssClass="table table-bordered table-striped" OnPageIndexChanging="EGV_PageIndexChanging" OnRowCommand="EGV_RowCommand" >
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
            </ZL:ExGridView>
        </div>
    </div>
        <!--DataRegion-->
        <asp:HiddenField runat="server" ID="curPosD" Value="0|0" />
        <script src="/JS/ICMS/ZL_Common.js"></script>
        <script type="text/javascript">
            //为radio绑定单击事件
            $().ready(function () {
                $("#signRadio :radio:not(:first)").click(function () {
                    $(".curDrag").show();
                    $(".curDrag img").attr("src", $(this).parent().attr("picUrl"));
                    var img = $(".curDrag img"); //获取img元素
                    $(".curDrag").css("width", img.width());//设置宽度
                });//click end;
                $("#signRadio :radio:first").click(function () {
                    $(".curDrag").hide();
                    $("#curPosD").val("0|0");
                });//click end;
                $(".curDrag").draggable
                    ({
                        addClasses: false,
                        axis: false,
                        cursor: 'crosshair',
                        //start: function () { alert(12);},
                        //drag: function (){},
                        stop: function () { GetPos(); },
                        containment: 'parent'
                    });
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
            var url = '../FreeFlow/FreeProNext.aspx?AppID=<%:appID%>&s=old';
            function disWin(url) {
                var iTop = (window.screen.availHeight - 30 - 550) / 2;
                var iLeft = (window.screen.availWidth - 10 - 960) / 2;
                var myWin = window.open(url, 'newwindow', 'height=700, width=960,top=' + iTop + ',left=' + iLeft + ',toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no');
            }
        </script>
        <script type="text/javascript">
            var ue;
            $().ready(function () {
                setTimeout(function () {
                    ue = UE.getEditor('txt_Content');
                }, 200);
                setTimeout(function () { ue.setDisabled(); },1000);
            });
            //回复不能为空
            function CCUserCheck()
            {
                var flag=true;
                v = $("#remindT").val();
                if (v == "")
                {
                    flag = false;
                    alert("审核意见不能为空!!!"); $("#remindT").focus();
                }
                return flag;
            }
            function PostDate(id)
            {
                if (confirm("确定要修改时间吗?"))
                {
                    var a = "PostDate";
                    var v = id + ":" + $("#SignDate_T_" + id).val();
                    PostToCS(a, v, function (data) {
                        if (data == 0) {alert('修改失败!!');}
                        else { alert('修改成功,重新加载!!'); location = location;}
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
