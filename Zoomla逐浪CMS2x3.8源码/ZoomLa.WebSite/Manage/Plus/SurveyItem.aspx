<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SurveyItem.aspx.cs" Inherits="ZoomLa.WebSite.Manage.Plus.SurveyItem" ValidateRequest="false" EnableViewStateMac="false" MasterPageFile="~/Manage/I/Default.master" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>问卷投票问题</title>
    <script src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script src="/Plugins/Ueditor/ueditor.all.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-striped table-bordered table-hover">
        <tr>
            <td colspan="2" class="text-center">
                <asp:Literal ID="ltlAction" runat="server">添加</asp:Literal>
                <asp:Label ID="Label1" runat="server" Style="letter-spacing: normal"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="td_m">标题：</td>
            <td>
                <asp:TextBox ID="TxtQTitle" class="form-control text_300" runat="server" MaxLength="80" ToolTip="标题最大长度为80个字符" />
                <span class="rd_red">*</span>
                <asp:RequiredFieldValidator ID="R1" runat="server" ErrorMessage="问题标题不能为空" ForeColor="Red" ControlToValidate="TxtQTitle" Display="Dynamic" />
            </td>
        </tr>
        <tr>
            <td>类型：</td>
            <td>
                <label><input type="radio" name="type_rad" data-val="select" value="0" checked="checked" />单选|多选</label>
                <label><input type="radio" name="type_rad" data-val="text" value="1" />填空|问答</label>
        <%--        <label><input type="radio" name="type_rad" data-val="comp" value="2" />组合选择</label>--%>
            </td>
        </tr>
        <tr>
            <td>是否必填：</td>
            <td>
                <asp:RadioButtonList ID="NotNull" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                    <asp:ListItem Value="1">是</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        <tr><td>内容：</td><td><asp:TextBox runat="server" ID="Content_T" TextMode="MultiLine" CssClass="m715-50" style="height:150px;"></asp:TextBox>
            <%=Call.GetUEditor("Content_T",2) %></td></tr>
        <tbody class="select optab">
            <tr><td>类型：</td><td> 
                <label><input type="radio" name="sel_type_rad" value="radio" checked="checked" />单选</label>
                <label><input type="radio" name="sel_type_rad" value="checkbox" />多选</label>
                <label><input type="radio" name="sel_type_rad" value="select" />下拉</label></td></tr>
            <tr><td>规则：</td><td><span>每行显示</span>
                <select id="sel_num_dp" name="sel_num_dp" class="form-control text_xs">
                    <option value="1">1</option> <option value="2">2</option> <option value="3">3</option>
                    <option value="4">4</option> <option value="5">5</option> <option value="6">6</option>
                </select>
                            </td></tr>
            <tr><td>选项：</td><td id="sel_op_body"></td></tr>
        </tbody>
        <tbody class="text optab">
            <tr><td>类型：</td><td>
                <label><input type="radio" name="text_type_rad" value="text" checked="checked" />单行</label>
                <label><input type="radio" name="text_type_rad" value="textarea" />多行</label></td></tr>
            <tr><td>规则：</td><td>
                <span>只允许
                    <select name="text_str_dp" class="form-control text_md">
                        <option value="none" selected="selected">不限制</option>
                        <option value="date">日期</option>
                        <option value="num">数字</option>
                        <option value="email">Email</option>
                    </select></span></td></tr>
        </tbody>
        <tr><td>操作：</td>
            <td>
                <asp:Button ID="EBtnSubmit" Text="添加" Enabled="true" OnClick="Button_Click" runat="server" CssClass="btn btn-primary" CommandName="Add" />
                <a href="SurveyItemList.aspx?SID=" class="btn btn-primary">返回</a>
                <a href="javascript:;" class="btn btn-info">预览</a>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="Option_Hid" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <style type="text/css">
        .optab {display:none;}
        .sel_op_item {margin-bottom:3px;}
        .sel_op_item a {margin-left:5px;}
    </style>
    <script src="/JS/SelectCheckBox.js"></script>
    <script>
        $(function () {
            $("[name=type_rad]").click(function () {
                ShowByType($(this).data("val"));
            });
            AddSelOP(3);
            ShowByType($("[name=type_rad]:checked").data("val"));
            //----填充数据
            FillByModel();
        })
        //根据选择显示不同的配置项
        function ShowByType(val) {
            $(".optab").hide();
            $("." + val).show();
        }
        var sel_textTlp = '<div class="sel_op_item"><input type="text" name="sel_text" class="form-control text_300 sel_text" value="@value" />'
                    + '<a href="javascript:;" class="sel_add btn btn-default"><span class="fa fa-plus"></span></a>'
                    + '<a href="javascript:;" class="sel_del btn btn-default"><span class="fa fa-minus"></span></a></div>';
        function AddSelOP(num, valArr) {
            for (var i = 0; i < num; i++) {
                var val = valArr ? valArr[i] : "";
                $("#sel_op_body").append(sel_textTlp.replace("@value", val));
            }
            $(".sel_add,.sel_del").unbind("click");
            $(".sel_add").click(function () { AddSelOP(1,[""]); });
            $(".sel_del").click(function () {
                //不能少于两个
                if ($(".sel_op_item").length < 3) { return false; }
                $(this).closest(".sel_op_item").remove();
            });
        }
        function ClearSelOP() { $("#sel_op_body").html(""); }
        function FillByModel() {
            //如果值不为空则填充
            if ($("#Option_Hid").val() != "") {
                var option = JSON.parse($("#Option_Hid").val());
                SetRadVal("sel_type_rad", option.sel_type_rad);
                SetRadVal("text_type_rad", option.text_type_rad);
                if (option.sel_num_dp != "")
                { $("[name=sel_num_dp]").val(option.sel_num_dp); }
                if (option.text_str_dp != "")
                { $("[name=text_str_dp]").val(option.text_str_dp); }
                if (option.sel_type_rad != "") {
                    var valArr = option.sel_op_body.split(',');
                    ClearSelOP();
                    AddSelOP(valArr.length, valArr);
                }
            }
        }
    </script>
</asp:Content>
