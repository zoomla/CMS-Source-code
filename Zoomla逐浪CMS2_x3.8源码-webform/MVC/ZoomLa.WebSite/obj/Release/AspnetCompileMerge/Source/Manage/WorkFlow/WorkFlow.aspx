<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WorkFlow.aspx.cs" Inherits="ZoomLaCMS.Manage.WorkFlow.WorkFlow" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加流程</title>
    <link href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
    <script src="/dist/js/bootstrap-switch.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <div>
        <table class="table table-striped table-bordered">
            <tr>
                <td class="td_m">流程名称：</td>
                <td>
                    <asp:TextBox runat="server" CssClass="form-control text_300" ID="proNameT" MaxLength="30" />
                    <span style="color: #f00">*</span>
                    <asp:RequiredFieldValidator ID="R1" ControlToValidate="proNameT" runat="server" ErrorMessage="流程名称不能为空" Display="Dynamic" ForeColor="Red" />
                </td>
            </tr>
             <tr>
                <td>项目名称：</td>
                <td>
                    <asp:TextBox runat="server" ID="NickName_T" CssClass="form-control text_300" MaxLength="6" /> <span style="color: #f00">*</span>
                    <asp:RequiredFieldValidator ID="R2" ControlToValidate="NickName_T" runat="server" ErrorMessage="项目名称不能为空" Display="Dynamic" ForeColor="Red" />
                    <div id="listdiv" class="btn btn-group">
                        <button type="button" class="btn btn-default">收文</button>
                        <button type="button" class="btn btn-default">发文</button>
                        <button type="button" class="btn btn-default">会议</button>
                        <button type="button" class="btn btn-default">申请</button>
                        <button type="button" class="btn btn-default">请假</button>
                    </div>
                </td>
            </tr>
            <tr><td>流程类型：</td>
                <td>
                    <asp:DropDownList runat="server" ID="ProType_DP" CssClass="form-control text_300">
                        <asp:ListItem Value="1">自由流程</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">公文流程</asp:ListItem>
                        <asp:ListItem Value="3">限定流程</asp:ListItem>
                    </asp:DropDownList>
                    <div class="rd_green">公文流程:用户必须依预设好的步骤执行,可[自由指定]下一步骤主办人</div>
                    <div class="rd_green">自由流程:用户可[自由指定主办人],主办人可[终止流程]</div>
                    <div class="rd_green">限定流程:用户必须依据预设好的步骤与主办人,执行流程</div>
                </td>
            </tr>
            <tr>
                <td>流程分类：</td>
                <td>
                    <asp:DropDownList runat="server" ID="ProClass_DP" CssClass="form-control text_300"></asp:DropDownList>
                </td>
            </tr>
<%--            <tr><td>表单类型：</td><td>
                <asp:RadioButtonList runat="server" ID="FormType_Rad" RepeatDirection="Horizontal">
                    <asp:ListItem Value="1" Selected="True">模型表单</asp:ListItem>
                     <asp:ListItem Value="2">Html表单</asp:ListItem>
                          <asp:ListItem Value="3">Word表单</asp:ListItem>
                          <asp:ListItem Value="4">Excel表单</asp:ListItem>
                </asp:RadioButtonList></td></tr>--%>
            <tr>
                <td>表单模型：</td> 
                <td>
                    <asp:TextBox runat="server" ID="FormInfo_T" CssClass="form-control text_300" /><span style="color: #f00">*</span>
                    <button type="button" class="btn btn-info" onclick="SelModels('FormInfo_T',0)">选择模型</button>
                  <%--  <span>请输入表单的内容模型ID,或Word文档ID</span>--%>
                    <asp:RequiredFieldValidator ID="R3" ControlToValidate="FormInfo_T" runat="server" ErrorMessage="表单模型不能为空" Display="Dynamic" ForeColor="Red" />
                </td>
            </tr>
            <tr><td>流程模板：</td><td>
                <asp:DropDownList runat="server" ID="FlowTlp_DP" CssClass="form-control text_300">
                    <asp:ListItem Value="defTlp" Selected="True">默认模板</asp:ListItem>
                    <asp:ListItem Value="rece">收文模板</asp:ListItem>
                    <asp:ListItem Value="send">发文模板</asp:ListItem>
                </asp:DropDownList>
                <span class="rd_green">*模板目录/Office/Tlp,值为空则使用默认模板</span></td></tr>
            <tr><td>打印模板：</td><td>
                <asp:DropDownList runat="server" ID="PrintTlp_DP" CssClass="form-control text_300">
                    <asp:ListItem Value="defTlp" Selected="True">默认打印模板</asp:ListItem>
                    <asp:ListItem Value="receprint">收文打印模板</asp:ListItem>
                    <asp:ListItem Value="sendprint">发文打印模板</asp:ListItem>
                </asp:DropDownList>
                <span class="rd_green">*模板目录/Office/Tlp,值为空则使用默认模板</span></td></tr>
            <tr>
                <td>使用角色：</td>
                <td>
                    <div class="input-group" style="width: 300px;">
                        <asp:TextBox runat="server" CssClass="form-control text_300" disabled="disabled" ID="sponsor_T" />
                        <span class="input-group-btn">
                            <input type="button" value="选择" class="btn btn-info" onclick="ShowUserDiag('sponsor');"/>
                        </span>
                    </div>
                    <span class="rd_green">为空则所有角色可用,否则仅指定用户角色可发起该流程</span>
                    <asp:HiddenField runat="server" ID="sponsor_Hid" />
                </td>
            </tr>
            <%--<tr>
                <td>管理人员：</td>
                <td>
                    <div class="input-group" style="width: 300px;">
                        <asp:TextBox runat="server" CssClass="form-control text_300" ID="manager_T" />
                        <asp:HiddenField runat="server" ID="manager_Hid" />
                        <span class="input-group-btn">
                            <input type="button" value="选择" class="btn btn-info" onclick="ShowUserDiag('manager');" />
                        </span>
                    </div>
                    <asp:HiddenField runat="server" ID="HiddenField1" />
                </td>
            </tr>--%>
            <%--            <tr>
                            <td>可操作字段</td><td>
                                <asp:TextBox runat="server" ID="CanEditField_T" Text="*" TextMode="MultiLine" Height="100" CssClass="form-control text_300"></asp:TextBox>
                                <button type="button" class="btn btn-info" disabled="disabled" id="CanEditField_B" onclick="SelModels('CanEditField_T',$('#FormInfo_T').val())">选择字段</button>
                                <span class="rd_green">*代表所有控件可编辑</span></td>
                        </tr>--%>
            <%--            <tr>
                            <td>公文操作权限：</td>
                            <td>
                                <label><input type="checkbox" name="docauth_chk" value="head" />套红</label>
                                <label><input type="checkbox" name="docauth_chk" value="sign" />签章</label>
                                <label><input type="checkbox" name="docauth_chk" value="edit" />允许修改</label>
                                <label><input type="checkbox" name="docauth_chk" value="traceless" />无痕修订</label>
                                <label><input type="checkbox" name="docauth_chk" value="hastrace" />有痕修订</label>
                            </td>
                        </tr>--%>
            <tr>
                <td>是否归档：</td>
                <td>
                    <input type="checkbox" runat="server" class="switchChk" id="flowdoc_chk" value="是否归档" />
                    <span class="rd_green">流程完结后自动归档,只有拥有归档权限的角色可见</span>
                </td>
            </tr>
            <tr>
                <td style="line-height: 60px;">流程说明：</td>
                <td>
                    <asp:TextBox runat="server" ID="remindT" CssClass="form-control text_300" TextMode="MultiLine" Columns="60" Width="300" Height="60px" />
                </td>
            </tr>
            <tr style="display: none;">
                <td>自动文号表达式：</td>
                <td>
                    <input name="txtAutoName" type="text" size="30" id="txtAutoName" class="BigInput" />
                    &nbsp;&nbsp;<a href="javascript:my_tip()">查看说明</a>
                </td>
            </tr>
            <tr id="tip" style="display: none">
                <td>说明：</td>
                <td>表达式中可以使用以下特殊标记：<br>
                    {Y}：表示年 4位的年份<br>
                    {yy}：表示年 不包含纪元的年份，如果不包含纪元的年份小于 10，则显示具有前导0的年份<br>
                    {M}：表示月 一位数的月份没有前导0<br>
                    {mm}：表示月 一位数的月份有一个前导0<br>
                    {D}：月中的某一天,一位数的日期没有前导0<br>
                    {dd}：月中的某一天,一位数的日期有一个前导0<br>
                    {H}：表示时<br>
                    {I}：表示分<br>
                    {S}：表示秒<br>
                    {F}：表示流程名<br>
                    {U}：表示用户姓名<br>
                    {SD}：表示短部门<br>
                    {LD}：表示长部门<br>
                    {R}：表示角色<br>
                    {N}：表示编号，通过 <u>编号计数器</u> 取值并自动增加计数值<br>
                    <br>
                    例如，表达式为：成建委发[{Y}]{N}号，编号位数为4<br>
                    自动生成文号如：成建委发[2006]0001号<br>
                    <br>
                    例如，表达式为：BH{N}，编号位数为3<br>
                    自动生成文号如：BH001<br>
                    <br>
                    例如，表达式为：{F}流程（{Y}年{M}月{D}日{H}:{I}）{U}<br>
                    自动生成文号如：请假流程（2009年1月1日10:30）张三<br>
                    <br>
                    可以不填写自动文号表达式，则系统默认按以下格式，如：<br>
                    请假流程(2009-01-01 10:30)
                </td>
            </tr>
            <tr align="center">
                <td colspan="2">
                    <asp:Button runat="server" ID="saveBtn" CssClass="btn btn-primary" Text="保存流程" OnClick="saveBtn_Click" />
                    <input type="button" value="返回列表" class="btn btn-primary" name="back" onclick="history.back();">
                </td>
            </tr>
        </table>
    </div>
    <script src="/JS/SelectCheckBox.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script>
        function ShowUserDiag(source) {
            var url = "/Common/Dialog/SelUserRole.aspx?#" + source;
            ShowComDiag(url,"选择角色");
        }
        var udiag = new ZL_Dialog();
        function SelModels(cid,mid) {
            udiag.title = "选择模型";
            udiag.url = "SelModelFieds.aspx?cid=" + cid + "&mid=" + mid;
            udiag.width = "modeldiag";
            udiag.ShowModal();
        }
        function GetModelID(cid,mid) {
            $("#" + cid).val(mid);
            udiag.CloseModal();
            $("#CanEditField_T").val('*');
        }
       
        function UserFunc(list, select) {
            Def_RoleFunc(list,select);
        }
        function CloseDiag() {
            udiag.CloseModal();
        }
        $().ready(function () {
            $("#listdiv button").click(function () {
                $("#NickName_T").val($(this).text());
            });
        });
    </script>
</asp:Content>
