<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddContent.aspx.cs" Inherits="test_Default" MasterPageFile="~/Manage/I/Default.master" ValidateRequest="false" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <script src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script src="/Plugins/Ueditor/ueditor.all.min.js"></script>
    <title>内容采集</title></asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table class="table table-bordered">
        <tr><td class="td_m">所属节点：</td><td>
             <div class="dropdown">
                        <button class="btn btn-default dropdown-toggle" type="button" style="width:300px;text-align:left;" id="dropdownMenu1" runat="server" data-toggle="dropdown" aria-expanded="false">
                            <span id="dr_text">不指定</span>
                           <span class="caret pull-right" style="margin-top:7px;"></span>
                            <asp:HiddenField id="selected_Hid" runat="server"  Value="0" />
                        </button>
                        <ul id="NodeList_DP" runat="server" class="dropdown-menu" style="overflow:auto;height:200px;width:300px;" role="menu" aria-labelledby="dropdownMenu1">
                            <li role="0" onclick="selectCate(this)"><a role="menuitem" tabindex="1" href="javascript:;">不指定</a></li>
                        </ul>
                    </div></td></tr>
        <tr><td>标题：</td><td><asp:TextBox runat="server" ID="Title_T" CssClass="form-control" /></td></tr>
        <tr><td>来源：</td><td><asp:TextBox runat="server" ID="SourceUrl_T" CssClass="form-control" /></td></tr>
        <tr><td>作者：</td><td><asp:TextBox runat="server" ID="Author_T" CssClass="form-control" /></td></tr>
        <tr><td>简述：</td><td><asp:TextBox runat="server" ID="Synopsis_T" CssClass="form-control" /></td></tr>
        <tr><td>内容：</td><td><asp:TextBox runat="server" ID="Content_T" TextMode="MultiLine" style="height:300px;width:900px;"></asp:TextBox></td></tr>
    </table>
    <div style="position:fixed;border:1px solid #ddd;width:100%;bottom:0px;z-index:1000;padding:5px;background-color:rgba(114, 111, 111, 0.5)" class="text-center">
        <asp:Button runat="server" ID="Add_Btn" Text="添加" CssClass="btn btn-primary" OnClick="Add_Btn_Click" OnClientClick="return CheckData();" />
    </div>
   
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script>
        $(function () {
            var editer = UE.getEditor("Content_T");
            editer.addListener("ready", function () {
                editer.fireEvent("catchRemoteImage");
                editer.focus();
            });
            $("#GType_Rad :radio").click(function () { if (this.value == "1") $(".onlybar").show(); else $(".onlybar").hide(); });
            $("#dropdownMenu1").find("#dr_text").text($("#NodeList_DP").find("[role=" + $("#selected_Hid").val() + "]").children().first().text().trim());
            InitIsCheck();
            $("#IsCheck_Ra").on('switchChange.bootstrapSwitch', function (e, data) {
                InitIsCheck();
            });
        })
        function CheckData() {
            if ($("#selected_Hid").val() < 1)
            {
                alert("请选择节点"); return false;
            }
        }
        function selectCate(data) {
            $("#selected_Hid").val($(data).attr("role"));
            $("#dropdownMenu1").find("#dr_text").text($(data).children().first().text().trim());
        }
    </script>
</asp:Content>