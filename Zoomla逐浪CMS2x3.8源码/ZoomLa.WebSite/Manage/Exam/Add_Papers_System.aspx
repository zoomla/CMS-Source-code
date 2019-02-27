<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Add_Papers_System.aspx.cs" Inherits="manage_Question_Add_Papers_System" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master"%>
<%@ Register Src="~/Manage/I/ASCX/TreeTlpDP.ascx" TagPrefix="ZL" TagName="TreeTlp" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>试卷管理</title>
    <script type="text/javascript" src="/JS/OAKeyWord.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td class="td_l">
               试卷名称：
            </td>
            <td class="bqright">
                <asp:TextBox ID="txtPaperName" runat="server" class="form-control text_300" /><span class="rd_red">*</span>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ErrorMessage="试卷名不能为空!" ControlToValidate="txtPaperName"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
               阅卷类型：
            </td>
            <td class="bqright">
                <asp:DropDownList ID="ddRtyle" runat='server' CssClass="form-control text_300">
                    <asp:ListItem Value="1" Selected="True">自动阅卷</asp:ListItem>
                    <asp:ListItem Value="2">手工阅卷</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="出题方式："></asp:Label>
                &nbsp;
            </td>
            <td class="bqright">
                <asp:DropDownList ID="ddType" runat='server' CssClass="form-control text_300">
                    <asp:ListItem Value="1" Selected="True">固定试卷(手工)</asp:ListItem>
                    <asp:ListItem Value="2">固定试卷(随机)</asp:ListItem>
                    <asp:ListItem Value="3" >随机试卷</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
               所属科目：
            </td>
            <td>
               <ZL:TreeTlp ID="Quest_Tree" runat="server" NodeID="C_id" Pid="C_Classid" Name="C_ClassName" Selected="NodeID_Hid" />
                <asp:HiddenField ID="NodeID_Hid" runat="server" />
            </td>
        </tr>
        <tr>
            <td>关键字：</td>
            <td>
                <div id="Examkeyword"></div>
                <asp:TextBox ID="TagKey_T" runat="server" style="display:none;"  CssClass="form-control text_300"></asp:TextBox> 
                <button type="button" onclick="ShowKeyWords()" class="btn btn-primary btn-sm">选择关键字</button>
                <span class="rd_green">(使用空格键或回车键分隔关键字，每个关键字长度不超过5个)</span>
            </td>
        </tr>
        <tr>
            <td>
               考试时间：
            </td>
            <td class="bqright">
                <asp:TextBox ID="txtTime" runat="server" class="form-control text_300" Text="0"></asp:TextBox><span>分钟</span>
                <span class="rd_green">(为0不限时间,否则到时自动提交)</span>
            </td>
        </tr>
        <tr>
            <td>有效时间：</td>
            <td>
                <asp:TextBox ID="txtBegionTime" runat="server" class="form-control text_md"   onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' });" placeholder="起始时间" />
                <span>至</span>
                <asp:TextBox ID="txtEndTime" runat="server" class="form-control text_md"  onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm' });" placeholder="结束时间"/>
                <span class="rd_green">为空则不限定时间</span>
            </td>
        </tr>
        <tr>
            <td>
               说明：
            </td>
            <td>
                <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" class="form-control tarea_l"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="保存试卷" OnClick="EBtnSubmit_Click" runat="server" />
                <a href="Papers_System_Manage.aspx" class="btn btn-primary">返回列表</a>
            </td>
        </tr> 
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        #option_ul li{margin-top:3px;}
        .tabinput{ border:none; padding-left:5px; height:30px; line-height:30px;}
        .radius{margin:2px;margin-top:20px;height:24px; line-height:24px; background:#eee; border:1px solid #ddd;border-radius:3px;padding:3px;float:none!important;}
        #errormes{z-index:9999;}
    </style>
    <script src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script>
        var tabarr = [];
        $(function () {
            InitKeyWord($("#TagKey_T").val());
        });
        function InitKeyWord(value) {
            tabarr = [];
            $("#Examkeyword").html('');
            if ($("#Examkeyword").length > 0) {
                $("#Examkeyword").tabControl({
                    maxTabCount: 5,
                    tabW: 80,
                    onAddTab: function (value) {
                        tabarr.push(value);
                        console.log(tabarr);
                    },
                    onRemoveTab: function (removeval) {
                        for (var i = 0; i < tabarr.length; i++) {
                            if (tabarr[i] == removeval) {
                                tabarr.splice(i, 1);
                                break;
                            }
                        }
                    }
                }, value);
            }//关键词
        }
        function ShowKeyWords() {
            comdiag.reload = true;
            comdiag.maxbtn = false;
            comdiag.width = "none";
            ShowComDiag("/Common/SelKeyWords.aspx?type=3", "选择关键字");
        }
        function GetKeyWords(keystr) {
            tabarr = tabarr.concat(keystr.split(','));
            var values = "";
            var length = tabarr.length <= 5 ? tabarr.length : 5;
            for (var i = 0; i < length; i++) {
                if (IsDis(values, tabarr[i]))
                { values += tabarr[i] + ","; }
            }
            InitKeyWord(values);
            CloseComDiag();
        }
        //判断重复
        function IsDis(arrstr, value) {
            for (var i = 0; i < tabarr.length; i++) {
                if (("," + arrstr).indexOf("," + value + ",") > -1) {
                    return false;
                }
            }
            return true;
        }
    </script>
</asp:Content>