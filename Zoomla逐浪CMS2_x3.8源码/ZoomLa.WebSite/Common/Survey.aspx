<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Survey.aspx.cs" Inherits="Common_Survey" enableviewstatemac="false" MasterPageFile="~/Common/Common.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head"><title>问卷调查</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="panel panel-default">
    <div class="survey_body panel-body">
        <div class="survey_header">
            <h1 class="survey_title" runat="server" id="STitle_T"></h1>
        </div>
        <div class="survey_content">
            <asp:Repeater runat="server" ID="RPT" EnableViewState="false">
                <ItemTemplate>
                    <div class="survey_item">
                        <h4 class="item_title">
                            <span class="subject">
                                <%#ZoomLa.BLL.Helper.StrHelper.ConvertIntegral(Container.ItemIndex+1)+"," %>
                                <%#Eval("QTitle") %>
                            </span>
                            <%#(Eval("IsNull","")=="1")?"<span class='r_red'>*</span>":"" %>
                            <span class="rules">( <%#GetRules() %> )</span></h4>
                        <div class="item_content"><%#Eval("QContent") %></div>
                        <div class="item_result"><%#GetResult() %></div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <div class="survey_foot panel-footer">
        <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" OnClientClick="return CheckSubmit();" CssClass="btn btn-primary" />
    </div>
</div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Script">
    <style type="text/css">
        body {background:#eee;margin:0px;padding-bottom:10px;}
        .panel {width:768px;background:#fff;border-radius:5px;
                      line-height:22px;font-size:14px;margin:5px auto;}
        .survey_title {line-height:32px;font-size:2em;text-align:center;padding:10px;}
        .survey_body { padding:0 30px 20px;}
        .item_title {padding:5px;}
        .item_title > .subject {font-weight:bold;font-size:16px; }
        .survey_foot {text-align:center;}
        /*内容与回复区*/
        .survey_item {border-bottom:1px dashed #ddd;margin-bottom:5px;padding-bottom:5px;}
        .result_div {width:100%;display:block;padding:5px;}
        .result_area {height:80px;}
        .num_1 label{width:100%;}
        .num_2 label{width:50%;}
        .num_3 label{width:33%;}
        .num_4 label{width:25%;}
    </style>
    <script src="/JS/ZL_Regex.js"></script>
    <script src="/JS/DatePicker/WdatePicker.js"></script>
    <script src="/JS/jquery.validate.min.js"></script>
    <script>
        $(function () {
            ZL_Regex.B_Num(".num");
            $(".date").click(function () { WdatePicker({ dateFmt: 'yyyy-MM-dd HH:mm:ss' }) });
            //email正则验证
            jQuery.validator.addMethod("phone", function (value) {
                return ZL_Regex.isMobilePhone(value);
            }, "请输入正确的手机号码!");
            $("form").validate({});
        })
        function CheckSubmit() {
            if (!confirm("确定要提交吗")) { return false; }
            //1,是否有值未被选中
            //2,是否有空为空
           //先处理数据收集功能
        }
    </script>
</asp:Content>