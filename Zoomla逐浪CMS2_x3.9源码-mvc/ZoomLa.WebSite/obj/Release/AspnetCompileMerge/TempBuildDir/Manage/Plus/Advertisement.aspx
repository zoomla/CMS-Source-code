<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Advertisement.aspx.cs" Inherits="ZoomLaCMS.Manage.Plus.Advertisement" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <link type="text/css" href="/dist/css/bootstrap-switch.min.css"  rel="stylesheet"/>
    <link rel="stylesheet" href="/Plugins/CodeMirror/lib/codemirror.css">
    <link rel="stylesheet" href="/Plugins/CodeMirror/theme/eclipse.css">
    <script type="text/javascript" src="/dist/js/bootstrap-switch.js"></script>
    <script src="/Plugins/CodeMirror/lib/codemirror.js"></script>
    <script src="/Plugins/CodeMirror/mode/css.js"></script>
    <script src="/Plugins/CodeMirror/mode/xml.js"></script>
    <script src="/Plugins/CodeMirror/mode/javascript/javascript.js"></script>
    <script src="/Plugins/CodeMirror/addon/selection/active-line.js"></script>
    <script src="/Plugins/CodeMirror/addon/edit/matchbrackets.js"></script>
    <script src="/Plugins/CodeMirror/mode/htmlmixed.js"></script>
    <title>添加广告</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered">
        <tr valign="middle">
            <td colspan="2" style="text-align: center">
                <strong>
                    <asp:Label ID="Label1" runat="server" Text="添加广告内容"></asp:Label>
                </strong>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <div style="width: 170px; height: 380px; overflow: hidden">
                    <table class="table table-striped table-bordered table-hover">
                        <tr>
                            <td valign="middle" style="width: 100px;">
                                <b>所属版位</b>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" class="tdbg">
                                <asp:ListBox ID="LstZoneName" CssClass="form-control text_md" runat="server" Height="320px" Width="150px" SelectionMode="multiple"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
            <td style="vertical-align: top;">
                <div style="overflow: hidden;" id="addpart">
                    <table class="table table-striped table-bordered table-hover" style="margin-bottom:0;">
                        <tr>
                            <td align="right" style="width: 150px;">
                                <strong>选择广告内容：</strong>
                            </td>
                            <td align="left">
                                <a href="javascript:changeBody(1)" class="btn btn-info" id="add">广告内容</a>
                                <a href="javascript:changeBody(2)" class="btn btn-info" id="con">过期内容</a>
                            </td>
                        </tr>
                        <tbody id="tad1" style="border-top-width: 1px;">
                            <tr>
                                <td class="text-right"><strong>广告名称：</strong></td>
                                <td>
                                    <asp:TextBox ID="TxtADName" class="form-control text_300" MaxLength="150" runat="server"></asp:TextBox>*
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="广告名称不能为空！" ControlToValidate="TxtADName" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 150px">
                                    <strong>广告类型：</strong>
                                </td>
                                <td align="left">
                                    <asp:RadioButtonList ID="RadlADType" AutoPostBack="False" RepeatDirection="Horizontal" RepeatLayout="Flow" runat="server" onclick="Change_ADType()">
                                        <asp:ListItem Selected="True" Value="1">图片</asp:ListItem>
                                        <asp:ListItem Value="2">动画</asp:ListItem>
                                        <asp:ListItem Value="3">文本</asp:ListItem>
                                        <asp:ListItem Value="4">代码</asp:ListItem>
                                        <asp:ListItem Value="5">页面</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                        </tbody>
                        <tbody style="border-top-width: 1px;">
                            <tr id="type">
                                <td align="right"><strong>广告内容：</strong></td>
                                <td>
                                    <div id="ADContent" runat="server">
                                        <div id="ADContent1" runat="server">
                                            <table class="table table-striped table-bordered table-hover">
                                                <tr valign="middle">
                                                    <td colspan="2" style="text-align: center;">
                                                        <strong>广告内容设置--图片</strong>
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td align="right" style="min-width: 100px;">图片上传：</td>
                                                    <td>
                                                        <asp:TextBox ID="txtpic" class="form-control text_300" runat="server"></asp:TextBox>
                                                        <span style="color: Red">*</span> <a href="javascript:void{};" onclick="CheckImg('pic')">[从我的作品中提取]</a>
                                                        <asp:Label ID="LabPicPath" runat="server" Text="请选择上传路径！" Visible="False"></asp:Label><br />
                                                        <iframe id="Upload_Pic" src="/Common/Upload.aspx?CID=pic&ftype=4" marginheight="0" marginwidth="0" frameborder="0" height="30" scrolling="no"></iframe>
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td align="right">图片尺寸：
                                                    </td>
                                                    <td>宽：<asp:TextBox ID="TxtImgWidth" class="form-control text_x" MaxLength="5" runat="server"></asp:TextBox>
                                                        像素 ; 高：<asp:TextBox ID="TxtImgHeight" class="form-control text_x" MaxLength="5" runat="server"></asp:TextBox>
                                                        像素
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td align="right" style="width: 70px; height: 41px;">链接地址：
                                                    </td>
                                                    <td style="height: 41px;">
                                                        <asp:TextBox ID="TxtLinkUrl" class="form-control text_300" Text="http://" runat="server"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td align="right" style="width: 70px; text-align: right">链接提示：
                                                    </td>
                                                    <td style="height: 16px">
                                                        <asp:TextBox ID="TxtLinkAlt" class="form-control text_300" runat="server" MaxLength="255"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td align="right">链接目标：
                                                    </td>
                                                    <td>
                                                        <asp:RadioButtonList ID="RadlLinkTarget" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0">原窗口</asp:ListItem>
                                                            <asp:ListItem Value="1" Selected="True">新窗口</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr style="width: 100%">
                                                    <td align="right">广告简介：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtADIntro" class="form-control text_300" runat="server" TextMode="MultiLine" Rows="5"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="ADContent2" runat="server" width="100%" style="display: none">
                                            <table class="table table-striped table-bordered table-hover">
                                                <tr valign="middle">
                                                    <td colspan="2" style="text-align: center; height: 25px;">
                                                        <strong>广告内容设置--动画</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 100px;">动画上传：
                                                    </td>
                                                    <td style=" color: red;">
                                                        <asp:TextBox ID="txtFlashPath" class="form-control text_300" runat="server"></asp:TextBox>
                                                        *<asp:Label ID="LabFlashPath" runat="server" Text="请选择上传路径！" Visible="False"></asp:Label>
                                                        <br />
                                                        <iframe id="Upload_Flash" src="/Common/Upload.aspx?CID=FlashPath&ftype=1" marginheight="0" marginwidth="0" frameborder="0" width="100%" height="30" scrolling="no"></iframe>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 70px">动画尺寸：
                                                    </td>
                                                    <td>宽：<asp:TextBox ID="TxtFlashWidth" class="form-control text_md" runat="server" Style="width: 80px" MaxLength="5"></asp:TextBox>
                                                        像素 ; ;高：<asp:TextBox ID="TxtFlashHeight" class="form-control text_md" Style="width: 80px" runat="server" MaxLength="5"></asp:TextBox>
                                                        像素
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="1" style="width: 70px; height: 6px">背景透明：
                                                    </td>
                                                    <td align="right" colspan="2" style="height: 6px; text-align: left;">
                                                        <asp:RadioButtonList ID="RadlFlashMode" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0" Selected="True">不透明</asp:ListItem>
                                                            <asp:ListItem Value="1">透明</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="ADContent3" runat="server" width="100%" style="display: none">
                                            <table border="0" cellpadding="2" cellspacing="1">
                                                <tr valign="middle">
                                                    <td colspan="2" style="text-align: left; padding-left: 50px;">
                                                        <strong>广告内容设置--文本</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="middle" colspan="2">
                                                        <asp:TextBox ID="TxtADText" class="form-control text_300" TextMode="multiLine" runat="server" Rows="6"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div id="ADContent4" runat="server" style="width: 520px; height: 550px; border: 1px solid #ddd; display: none;">
                                            <iframe style="height: 30px; width: 530px; z-index: -1;" frameborder="0" name="I1" id="I1" scrolling="no" src="/manage/Template/label.htm"></iframe>
                                            <%--<input type="button" value="素材选择" class="btn btn-xs btn-primary" onclick="SelPic({'callback':'addub'});" />--%>
                                            <asp:TextBox ID="TxtADCode" TextMode="multiLine" runat="server" />
                                        </div>
                                        <div id="ADContent5" runat="server" width="100%" style="display: none">
                                            <table border="0" cellpadding="2" cellspacing="1">
                                                <tr valign="middle">
                                                    <td colspan="2" style="text-align: center;">
                                                        <strong>广告内容设置--页面</strong>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" style="width: 70px">页面地址：
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtWebFileUrl" class="form-control text_300" runat="server" Rows="6"></asp:TextBox>
                                                        <br />
                                                        <span style="color: blue">注意：</span>页面地址需要加上http://
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                        <tbody id="adv" style="border-top-width: 1px;">
                            <tr>
                                <td align="right">
                                    <strong>通过审核:</strong>
                                </td>
                                <td align="left">
                                     <input  runat="server" type="checkbox" id="ChkPasses" class="switchChk" checked />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <strong>广告权重：</strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TxtPriority" runat="server" CssClass="form-control text_md" TextMode="singleLine" MaxLength="3" Text="1" Style="width: 50px"></asp:TextBox>
                                    * 此项为版位广告随机显示时的优先权，权重越大显示机会越大。
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <strong>广告统计：</strong>
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="ChkCountView" runat="server" />
                                    统计浏览数 浏览数：<asp:TextBox ID="TxtViews" MaxLength="5" CssClass="form-control text_md" runat="server" Width="50px" Text="0"></asp:TextBox><br />
                                    <asp:CheckBox ID="ChkCountClick" runat="server" />
                                    统计点击数 点击数：<asp:TextBox ID="TxtClicks" CssClass="form-control text_md" runat="server" MaxLength="5" Width="50px" Text="0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <strong>广告过期时间：</strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtOverdueDate" CssClass="form-control text_300" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy-MM-dd' });"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <strong>出售价格(按天计)：</strong>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="Price" class="form-control text_300" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <strong>是否开放竟价：</strong>
                                </td>
                                <td align="left">
                                    <input  runat="server" type="checkbox" id="ADBuy" class="switchChk" />
                                </td>
                            </tr>

                        </tbody>
                    </table>
                    <div id="Table" style="display:none;">
                        <div id="Table1" runat="server" width="100%">
                            <table class="table table-striped table-bordered table-hover">
                                <tr valign="middle" id="shezhi1">
                                    <td colspan="2" style="text-align: center;">
                                        <div>
                                            <strong>广告内容设置--图片</strong>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width:150px;">图片上传：</td>
                                    <td style="">
                                        <asp:TextBox ID="txtpic1" class="form-control text_md" runat="server"></asp:TextBox>
                                        <span style="color: Red">*</span> [<a href="javascript:void{};" onclick="CheckImg('pic')">从我的作品中提取</a>]
                                        <asp:Label ID="LabPicPath1" runat="server" Text="请选择上传路径！" Visible="False"></asp:Label><br />
                                        <iframe id="Upload_Pic1" src="../../Common/Upload.aspx?CID=pic1&ftype=4" marginheight="0" marginwidth="0" frameborder="0" width="100%" height="30" scrolling="no"></iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">图片尺寸：
                                    </td>
                                    <td>宽：<asp:TextBox ID="TxtImgWidth1" class="form-control text_md" MaxLength="5" Style="width: 80px" runat="server"></asp:TextBox>
                                        像素 ; 高：<asp:TextBox ID="TxtImgHeight1" class="form-control text_md" MaxLength="5" Style="width: 80px" runat="server"></asp:TextBox>
                                        像素
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">链接地址：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtLinkUrl1" class="form-control text_md" Text="http://" runat="server" Width="341px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="text-align: right">链接提示：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtLinkAlt1" class="form-control text_md" runat="server" MaxLength="255" Width="341px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">链接目标：
                                    </td>
                                    <td>
                                        <asp:RadioButtonList ID="RadlLinkTarget1" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">原窗口</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">新窗口</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">广告简介：
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TxtADIntro1" class="form-control text_md" runat="server" TextMode="MultiLine" Height="87px" Width="341px"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="Table2" runat="server" width="100%" style="display: none">
                            <table class="table table-striped table-bordered table-hover">
                                <tr valign="middle">
                                    <td colspan="2" style="height: 23px; text-align: center;">
                                        <strong>广告内容设置--动画</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 150px;">动画上传：</td>
                                    <td style="color: red;">
                                        <asp:TextBox ID="txtFlashPath1" class="form-control text_md" runat="server"></asp:TextBox>
                                        *<asp:Label ID="LabFlashPath1" runat="server" Text="请选择上传路径！" Visible="False"></asp:Label>
                                        <br />
                                        <iframe id="Upload_Flash1" src="../../Common/Upload.aspx?CID=FlashPath1&ftype=1" marginheight="0" marginwidth="0" frameborder="0" width="100%" height="30" scrolling="no"></iframe>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 70px">动画尺寸：
                                    </td>
                                    <td>宽：<asp:TextBox ID="TxtFlashWidth1" class="form-control text_md" runat="server" Style="width: 80px" MaxLength="5"></asp:TextBox>
                                        像素 ; ;高：<asp:TextBox ID="TxtFlashHeight1" class="form-control text_md" Style="width: 80px" runat="server" MaxLength="5"></asp:TextBox>
                                        像素
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" colspan="1" style="width: 70px; height: 6px">背景透明：
                                    </td>
                                    <td align="right" colspan="2" style="height: 6px; text-align: left;">
                                        <asp:RadioButtonList ID="RadlFlashMode1" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Selected="True">不透明</asp:ListItem>
                                            <asp:ListItem Value="1">透明</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="Table3" runat="server" width="100%" style="display: none">
                            <table class="table table-striped table-bordered table-hover">
                                <tr valign="middle">
                                    <td colspan="2" style="text-align:center;">
                                        <strong>广告内容设置--文本</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 150px;">广告内容：</td>
                                    <td valign="middle">
                                        <asp:TextBox ID="TxtADText1" class="form-control text_md" TextMode="multiLine" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="Table4" runat="server" width="100%" style="display: none">
                            <table class="table table-striped table-bordered table-hover">
                                <tr valign="middle">
                                    <td style="text-align: center;" colspan="2">
                                        <strong>广告内容设置--代码</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 150px;">广告内容：</td>
                                    <td valign="middle">
                                        <asp:TextBox ID="TxtADCode1" class="form-control text_300" TextMode="multiLine" runat="server" Rows="6"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="Table5" runat="server" width="100%" style="display: none">
                            <table class="table table-striped table-bordered table-hover">
                                <tr valign="middle">
                                    <td colspan="2" style="text-align: center;">
                                        <strong>广告内容设置--页面</strong>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="width: 150px">页面地址：</td>
                                    <td>
                                        <asp:TextBox ID="TxtWebFileUrl1" class="form-control text_300" runat="server"></asp:TextBox>
                                        <br />
                                        <span style="color: blue">注意：</span>页面地址需要加上http://
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td valign="middle" colspan="2" align="center">
                <asp:Button ID="EBtnSubmit" runat="server" class="btn btn-primary" Text="保存" OnClientClick="javascript:return CheckForm();" OnClick="EBtnSubmit_Click" />
                <input id="Cancel" name="Cancel" onclick="GoBack();" type="button" value="取消" class="btn btn-primary" />
                <asp:HiddenField ID="HdnID" runat="server" />
            </td>
        </tr>
    </table>

    <div style="clear: both"></div>

    <table border="0" cellpadding="2" cellspacing="1" width="100%">
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script>
        var editor, editorFlag = false;
        $(function () {
            if ($("#RadlADType_3:checked").length > 0)//修改代码
            {
                InitEditor();
                editorFlag = true;
            }
        })
        function InitEditor()
        {
            editor = CodeMirror.fromTextArea(document.getElementById("TxtADCode"), {
                mode: "text/html",
                tabMode: "indent",
                lineNumbers: true,
                styleActiveLine: true,
                matchBrackets: true,
                lineWrapping: true
            });
            editor.setOption("theme", "eclipse");
        }
        function addubb(code) { PasteValue(code); }
        function PasteValue(code) {
            if (!editor || !editor.setOption) { setTimeout(function () { PasteValue(code); }, 500); return; }
            if (code && code != "")
                editor.replaceSelection(code, null, "paste");
        }
        function SelPic(pval) {
            comdiag.maxbtn = false;
            ShowComDiag("/Common/SelFiles.aspx?pval=" + JSON.stringify(pval), "选择图片");
        }
        function PageCallBack(action, vals, pval) {
            var val = vals.split('|')[0];
            addubb(val);
            CloseComDiag();
        }
        //--------------------------------------------
        function CheckImg(txtName) {
            window.open('../Flex/MyWorkList.aspx?txtName=' + txtName, 'newWin', 'modal=yes,width=650,height=750,resizable=yes,scrollbars=yes');
        }
        function CheckSelect() {
            var s = document.getElementById("<%= LstZoneName.ClientID %>");
            for (var i = 0; i < s.length; i++) {
                if (s.options[i].selected) {
                    return true;
                }
            }
            return false;
        }
        function CheckUploadFile() {
            if (document.getElementById("RadlADType_0").checked) {
                var s = document.getElementById("<%=txtpic.ClientID %>");
                if (s.value == "") {
                    alert("还没有上传图片！");
                    return false;
                }
            }
            if (document.getElementById("RadlADType_1").checked) {
                s = document.getElementById("<%=txtFlashPath.ClientID %>");
                if (s.value == "") {
                    alert("还没有上传Flash");
                    return false;
                }
            }
            return true;
        }
        function GoBack() {
            window.location.href = "ADManage.aspx";
        }
        function Change_ADType() {
            for (var j = 1; j <= 5; j++) {
                var ot = document.getElementById("ADContent" + j);
                var ott = document.getElementById("Table" + j);
                if ($("input[id=RadlADType_" + (j - 1) + "]")[0].checked) {
                    ot.style.display = '';
                    ott.style.display = '';
                    if (j == 1) {
                        $("#ChkCountClick").disabled = false;
                        $("#TxtClicks").disabled = false;
                    }
                    else if (j == 4)//代码
                    {
                        if (!editorFlag)
                        {
                            InitEditor();
                            editorFlag = true;
                        }
                    }
                    else {
                        $("#ChkCountClick").disabled = true;
                        $("#TxtClicks").disabled = true;
                    }
                }
                else {
                    ot.style.display = 'none';
                    ott.style.display = 'none';
                }
            }
        }
        function ADTypeChecked(i) {
            $("#RadlADType_" + (i - 1)).checked = true;
            Change_ADType();
        }
        function CheckForm() {
            if ($("#TxtADName").value == '') {
                alert('广告名称不能为空！');
                $("#TxtADName").focus();
                return false;
            }
            if (!CheckUploadFile()) {
                return false;
            }
            if ($("#RadlADType_2").checked && $("#TxtADText").value == '') {
                alert('广告文字不能空');
                $("#TxtADText").focus();
                return false;
            }
            //if ($("#RadlADType_3").checked && $("#TxtADCode").value == '') {
            //    alert('广告代码不能空');
            //    $("TxtADCode").focus();
            //    return false;
            //}
            if ($("#TxtPriority").value == '') {
                alert('权重系数不能空');
                $("#TxtPriority").focus();
                return false;
            }
        }
        function changeBody(index) {
            switch (index) {
                case 1:
                    {
                        document.getElementById('ADContent').style.display = "";
                        document.getElementById('Table').style.display = "none";
                        $("#adv").show();
                        document.getElementById('adv').style.display = "";
                        document.getElementById('tad1').style.display = "";
                        document.getElementById('type').style.display = "";
                        document.getElementById('shezhi1').style.display = "";
                        document.getElementById('add').style.background = "#AACC12";
                    }
                    break;
                case 2:
                    {
                        document.getElementById('ADContent').style.display = "none";
                        document.getElementById('Table').style.display = "";
                        $("#adv").hide();
                        document.getElementById('adv').style.display = "none";
                        document.getElementById('tad1').style.display = "none";
                        document.getElementById('type').style.display = "none";
                        document.getElementById('shezhi1').style.display = "none";
                        document.getElementById('add').style.background = "#8F9F56";
                    }
                    break;
            }
        }
        //页面大小
        var Width = document.body.offsetWidth;
        Width -= 200;
        document.getElementById("addpart").style.width = Width + "px";
    </script>
</asp:Content>
