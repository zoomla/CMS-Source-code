<%@ Page Language="C#"  MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeBehind="CollectionInfoPage.aspx.cs" Inherits="ZoomLaCMS.Manage.Content.Collect.CollectionInfoPage" ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server"><title>采集配置字段设置</title></asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
<table class="table table-bordered">
        <tr>
            <td style="width: 600px;">
                <asp:TextBox ID="txtHtml" CssClass="form-control" runat="server" TextMode="MultiLine" Style="width: 98%; height: 420px; max-width: 98%"></asp:TextBox>
            </td>
            <td class="regextd">
                <div class="input-group">
                    <span class="input-group-addon">字段名</span>
                    <input type="text" class="form-control" disabled="disabled" value="<%:Request["Alias"] %>" />
                </div>
                <div class="input-group">
                    <span class="input-group-addon">URL</span>
                    <asp:DropDownList runat="server" ID="Link_DP" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="Link_DP_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="input-group">
                    <span class="input-group-addon">元素类型</span>
                    <asp:TextBox runat="server" ID="EType_T" Text="DIV" CssClass="form-control" />
                    <span id="etype_list" class="input-group-btn">
                        <button type="button" class="btn btn-default">DIV</button>
                        <button type="button" class="btn btn-default">TITLE</button>
                        <button type="button" class="btn btn-default">UL</button>
                        <button type="button" class="btn btn-default">BODY</button>
                    </span>
                </div>
                <div class="input-group">
                    <span class="input-group-addon">ID=</span>
                    <asp:TextBox runat="server" ID="ID_T" CssClass="form-control"/>
                </div>
                <div class="input-group">
                    <span class="input-group-addon">Class=</span>
                    <asp:TextBox runat="server" ID="Class_T" CssClass="form-control" />
                </div>
                <div class="input-group">
                    <span class="input-group-addon">开始字符串</span>
                    <asp:TextBox runat="server" ID="Start_T" CssClass="form-control" />
                </div>
                <div class="input-group">
                    <span class="input-group-addon">结束字符串</span>
                    <asp:TextBox runat="server" ID="End_T" CssClass="form-control" />
                </div>
                <div class="opdiv" id="byid"></div>
                <div><asp:Label runat="server" ID="CurLink_L" Style="color: green;"></asp:Label></div>
                <div class="margin_t5">
                   <asp:CheckBox runat="server" ID="Script_Chk" Text="允许Script脚本" />
                   <asp:CheckBox runat="server" ID="Flash_Chk" Text="允许Flash插件" Checked="true" />
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button CssClass="btn btn-primary" runat="server" ID="Save_Btn" OnClick="Save_Btn_Click" Text="保存信息"  />
				<input type="button" value="关闭窗口" class="btn btn-primary" onclick="parent.Close();" />
            </td>
            <td>
                <asp:Button ID="Test_Btn" CssClass="btn btn-info" runat="server" Text="测试获取" OnClick="Test_Btn_Click" ValidationGroup="Add" />
            </td>
        </tr>
    </table>
<asp:HiddenField ID="Json_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
<style type="text/css">
.regextd .input-group{margin-bottom:5px;}
.regextd .input-group-addon{min-width:94px;text-align:center;}
</style>
    <script type="text/javascript">
        $(function () {
            $("#etype_list button").click(function () {
                $("#EType_T").val($(this).text());
            });
        })
        function SaveConfig() {//将值保存回父窗口
            var name = "<%:Request.QueryString["name"]%>";
            var json = document.getElementById("Json_Hid").value;
            parent.SaveConfig(name, json);
            parent.Close();
        }
    </script>
</asp:Content>
