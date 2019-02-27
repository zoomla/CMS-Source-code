<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Vote.aspx.cs" Inherits="ZoomLaCMS.Plugins.Vote" EnableViewStateMac="false" ClientIDMode="Static" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>投票调查</title>
<style>
.list li { float: none; }
.qtitle{font-size:24px;font-weight:bolder;color:#065189;padding-bottom:5px;margin-bottom:5px;}
</style>
<script src="/JS/Verify.js"></script>
<script src="/JS/ZL_ValidateCode.js"></script>
<script>
    //检查答题情况， 假定每个都是必填的
    var goto = false;
    var vflag = true;
    function CheckAns() {
        var flag = false;
        var tables = document.getElementsByTagName("table");
        preview(1);
        for (var i = 0; i < tables.length; i++) {
            var options = document.getElementsByName("vote_" + i);
            flag = false;
            if (options.length > 1) {
                for (var j = 0; j < options.length; j++) {
                    if (options[j].checked) {
                        flag = true;
                        break;
                    }
                }
            }
            else if (options.length == 1) {
                if (options.value.length > 0)
                    flag = true;
            }
            if (flag == false || vflag == false) {
                alert('问卷未完成， 请继续答题。。。');
                // document.getElementById("mao_" + i).focus();
                // GotoAnchor(i);
                return false;
            }
        }
        return true;
    }
    //跳转到指定位置
    function GotoAnchor(pos) {
        var url = location.href;
        if (url.indexOf("#mao_") > 0) {
            url = url.substring(0, url.lastIndexOf('_') + 1);
            url = url + pos;
        }
        else {
            url = url + "&#mao_" + pos;
        }
        location.href = url;
    }
    //特殊文本的验证
    function IsLegalValue(id, type) {
        var value = document.getElementById("txt_" + id).value;
        switch (type) {
            case 0:
                vflag = CheckEmail(value);
                break;
            case 1:
                vflag = CheckMobile(value);
                break;
            case 2:
                vflag = CheckPhone(value);
                break;
            case 3:
                vflag = CheckIdCard(value);
                break;
            case 4:
                vflag = CheckSchCard(value);
                break;
        }
        if (vflag == false) {
            document.getElementById("span_" + id).style.display = 'block';
            //document.getElementById("txt_" + id).focus();
            // document.getElementById("txt_" + id).select();
        }
        else { document.getElementById("span_" + id).style.display = 'none'; }
    }
</script>
<script>
    function preview(oper) {
        if (oper < 10) {
            bdhtml = document.getElementById("divContent").innerHTML;//获取当前页的html代码  
            sprnstr = "<!--startprint" + oper + "-->";//设置打印开始区域  
            eprnstr = "<!--endprint" + oper + "-->";//设置打印结束区域  
            prnhtml = bdhtml.substring(bdhtml.indexOf(sprnstr) + 18); //从开始代码向后取html  

            prnhtmlprnhtml = prnhtml.substring(0, prnhtml.indexOf(eprnstr));//从结束代码向前取html  
            document.getElementById("divContent").innerHTML = prnhtml;
            window.print();
            document.getElementById("divContent").innerHTML = bdhtml;
        } else {
            window.print();
        }
    }
    function ShowflaUp(id) {
        if (document.getElementById("viewid_" + id).style.display != 'none')
            document.getElementById("viewid_" + id).style.display = 'none';
        else document.getElementById("viewid_" + id).style.display = '';
    }
    function CheckIsNull() {
        var flag = true;
        return flag;
    }
</script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div id="main" class="container">
    <div id="divContent" class="content" runat="server">
        <div class="panel panel-primary margin_t10">
            <div class="panel-heading">
                <h3 class="panel-title"><i class="fa fa-file"></i>
                    <asp:Literal ID="SurveyName_L" runat="server"></asp:Literal>
                    <span class="pull-right"><i class="fa fa-calendar"></i>
                        <asp:Literal ID="CreateDate_L" runat="server"></asp:Literal></span></h3>
            </div>
            <div class="panel-body">
                <div runat="server" id="qtitle" class="qtitle">
                   <i class="fa fa-file"></i> <asp:Literal ID="Description_L" runat="server"></asp:Literal>
                </div>
                <div class="list title">
                    <ul>
                        <asp:Literal ID="ltlResultHtml" runat="server"></asp:Literal>
                    </ul>
                </div>
            </div>
            <div id="regVcodeRegister" class="list panel-footer" runat="server" visible="false">
                <div class="reg_put">
                    <span>验证码</span>
                    <asp:TextBox ID="SendVcode" MaxLength="6" runat="server" CssClass="td_m"></asp:TextBox>
                    <asp:Image ID="SendVcode_img" runat="server"
                        ToolTip="点击刷新验证码" Style="cursor: pointer; border: 0; vertical-align: middle;" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="SendVcode"
                        Display="Dynamic" ErrorMessage="验证码不能为空!"></asp:RequiredFieldValidator>
                    <asp:Label ID="Validateinfo" runat="server" Text=""></asp:Label>
                    <input type="hidden" id="SendVcode_hid" name="VCode_hid" />
                    <script>
                        $(function () { $("#SendVcode").ValidateCode(); })
                    </script>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="IsNull_H" runat="server" />
</div>
<div class="bottom text-center">
    <asp:Button ID="btnSubmit" runat="server" Text="提交问卷" CssClass="btn btn-info" OnClick="btnSubmit_Click" OnClientClick="return CheckIsNull()" />
    <button type="button" class="btn btn-info" onclick=" CheckAns();">打印问卷</button>
    <asp:Button ID="Button2" runat="server" Text="保存问卷" OnClick="Button2_Click" CssClass="btn btn-info" />
    <asp:Button ID="btnExport" runat="server" Text="导出问券" CssClass="btn btn-info" OnClick="btnExport_Click" />
</div>
<asp:HiddenField runat="server" ID="Random_Hid" />
</asp:Content>
