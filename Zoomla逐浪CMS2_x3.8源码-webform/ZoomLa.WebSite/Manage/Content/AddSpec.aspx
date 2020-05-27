<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSpec.aspx.cs" Inherits="Manage_I_Content_AddSpec" MasterPageFile="~/Manage/I/Default.master" Debug="true" %>
<%@ Register Src="~/Manage/I/ASCX/TlpDP.ascx" TagPrefix="ZL" TagName="TlpDown" %>
<%@ Register Src="~/Manage/I/ASCX/SFileUp.ascx" TagPrefix="ZL" TagName="SFileUp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>专题列表</title>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs">
        <li class="active"><a href="#Tabs0" data-toggle="tab">基本信息</a></li>
        <li><a href="#Tabs1" data-toggle="tab">专题选项</a></li>
    </ul>
    <div class="tab-content">
        <div class="tab-pane active" id="Tabs0">
            <table class="table table-striped table-bordered table-hover">
                <tbody>
                    <tr class="tdbg">
                        <td class="td_l text-right"><strong>所属父专题：</strong></td>
                        <td>
                            <asp:Label ID="lblCate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-right">
                            <strong>专题名称：</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtSpecName" onchange="CheckSpecName()" runat="server" onblur="Getpy('TxtSpecName','TxtSpecDir')" onkeyup="Getpy('TxtSpecName','TxtSpecDir')" class="form-control pull-left" style="max-width:200px;" autofocus="true"  />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="tips" runat="server" ErrorMessage="专题不能为空!" ControlToValidate="TxtSpecName" Display="Dynamic" SetFocusOnError="True" />
                        </td>
                    </tr>
                    <tr>
                        <td class="text-right">
                            <strong>专题标识名：</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtSpecDir" onchange="CheckSpecName()" class="form-control pull-left" style="max-width:200px;" runat="server" Columns="20"/>
                            <span class="tips" style="color: Blue">注意，目录名只能是字母、数字、下划线组成，首字符不能是数字</span>
                            <span style="color:red" id="spectip_span"></span>
                                <asp:RequiredFieldValidator ID="ValrCateDir" CssClass="tips" runat="server" ErrorMessage="专题标识名不能为空!" ControlToValidate="TxtSpecDir" Display="Dynamic" SetFocusOnError="True" />
                                <asp:RegularExpressionValidator  ID="ValrCateDir_Exp" CssClass="tips" runat="server" ControlToValidate="TxtSpecDir" Display="Dynamic" ValidationExpression="[_a-zA-Z][_a-zA-Z0-9]*" ErrorMessage="专题标识格式错误" SetFocusOnError="True"></asp:RegularExpressionValidator>
                        </td>
                    </tr>

                    <tr class="tdbg">
                        <td class="text-right">
                            <strong>专题图片地址：</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="Image_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
                            <%--<ZL:SFileUp ID="SFile_Up" IsNull="True" FType="Img" runat="server" />--%>
                        </td>
                    </tr>

                    <tr class="tdbg">
                        <td class="text-right">
                            <strong>专题提示：</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtSpeacTips" class="form-control" style="max-width:300px;" runat="server" Columns="60" Height="65px" Width="500px" Rows="2" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-right">
                            <strong>专题说明：</strong></td>
                        <td>
                            <asp:TextBox ID="TxtSpecDesc" class="form-control" style="max-width:300px;" runat="server" Columns="50" Height="65px" Width="500px" Rows="5" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="text-right">
                            <strong>专题META关键词：</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtSpeacKeyword" class="form-control" style="max-width:300px;" runat="server" Height="65" Width="500" Columns="60" Rows="4" TextMode="MultiLine" />

                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="text-right">
                            <strong>专题META网页描述：
                
                            </strong>
                        </td>
                        <td>
                            <asp:TextBox ID="TxtSpeacDescriptive" class="form-control" style="max-width:300px;" runat="server" Height="65" Width="500" Columns="60" Rows="4" TextMode="MultiLine" />
                        </td>
                    </tr>
                    <tr>
                        <td class="text-right">
                            <strong>创建人：</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="CUser_T" runat="server" CssClass="form-control text_300"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-right">
                            <strong>创建时间：</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="CDate_T" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' })" CssClass="form-control text_300"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab-pane" id="Tabs1">
            <table width="100%" border="0" cellpadding="2" cellspacing="1" class="table table-striped table-bordered table-hover">
                <tbody>

                    <tr class="tdbg">
                        <td style="width: 288px" class="tdbgleft">
                            <strong>打开方式：</strong>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RBLOpenType" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">原窗口打开</asp:ListItem>
                                <asp:ListItem Value="1">新窗口打开</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td style="width: 288px" class="tdbgleft">
                            <strong>专题列表页扩展名：</strong>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RBLFileExt" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">.html</asp:ListItem>
                                <asp:ListItem Value="1">.htm</asp:ListItem>
                                <asp:ListItem Value="2">.shtml</asp:ListItem>
                                <asp:ListItem Selected="True" Value="3">.aspx</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td style="width: 288px" class="tdbgleft">
                            <strong>专题列表页文件名规则：</strong>
                        </td>
                        <td>
                            <asp:RadioButtonList ID="RBLListFileRule" runat="server">
                                <asp:ListItem Selected="True" Value="1">专题类别目录/专题目录/List_专题ID_PageIndex</asp:ListItem>
                                <asp:ListItem Value="2">专题类别目录/List/List_专题ID_PageIndex</asp:ListItem>
                                <asp:ListItem Value="3">Special/List_专题ID_PageIndex</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td style="width: 288px" class="tdbgleft">
                            <strong>专题列表页模板：</strong>
                        </td>
                        <td>
                            <%=PageCommon.GetTlpDP("TxtListTemplate") %>
                            <asp:HiddenField ID="TxtListTemplate_hid" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" style="margin-top: 8px;">
        <tr>
            <td align="center">&nbsp; &nbsp;
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存" OnClick="EBtnSubmit_Click" runat="server" />&nbsp; &nbsp;
                <input name="Cancel" type="button" class="btn btn-primary" id="BtnCancel" value="取消" onclick="window.location.href = 'SpecialManage.aspx'" />
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="EditSpecName_Hid" runat="server" />
    <asp:HiddenField ID="EditSpecDir_Hid" runat="server" />
    <ZL:TlpDown runat="server" />
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/chinese.js" ></script>
    <script>
        $().ready(function () {
            Tlp_initTemp();
           
        });
        var TempDiag = new ZL_Dialog();
        function WinOpenDialog(url) {
            TempDiag.url = url;
            TempDiag.title = "选择模板";
            TempDiag.ShowModal();
        }
        function Getpy(ontxt, id) {
            if ($("#EditSpecName_Hid").val())
            { return;}
            var str = document.getElementById(ontxt).value.trim();
            if (str == "") {
                document.getElementById(id).value = "";
                return;
            }
            var arrRslt = makePy(str);
            if (arrRslt.length > 0) {
                document.getElementById(id).value = arrRslt.toString().toLowerCase();
                if (document.getElementById(id).value.indexOf(',') > -1) {//判断栏目名称有多音字后栏目标识名“，”并去掉逗号后面的数据
                    var str = document.getElementById(id).value;
                    document.getElementById(id).value = str.split(',', 1);
                }
            }
        }
        function CloseDialog() {
            TempDiag.CloseModal();
        }
        function CheckSpecName() {
            var action = "";
            if ($("#EditSpecName_Hid").val() == "" || $("#EditSpecName_Hid").val() != $("#TxtSpecName").val())//判断值是否修改过
            { action = "checkspecname"; }
            else { return;}
            if ($("#EditSpecDir_Hid").val() == "" || $("#EditSpecDir_Hid").val() != $("#TxtSpecDir").val())
            { action = action == "" ? "checkspecdir" : "checkall"; }
            $.post('AddSpec.aspx', { action: action, specname: $("#TxtSpecName").val(), specdir: $("#TxtSpecDir").val() }, function (result) {
                var resultobj = JSON.parse(result);
                if (resultobj.status == "-1") {
                    $("#spectip_span").text('专题名重复!请重新设置专题名或标识名');
                    $("#EBtnSubmit").attr('disabled', 'disabled');
                } else {
                    $("#spectip_span").text('');
                    $("#EBtnSubmit").removeAttr('disabled');
                }
            })
        }

    </script>
</asp:Content>