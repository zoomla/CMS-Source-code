<%@ Page Language="C#" MasterPageFile="~/Manage/I/Default.master" AutoEventWireup="true" CodeFile="CollectionInfoPage.aspx.cs"  Inherits="Manage_I_Content_CollectionInfoPage"  ValidateRequest="false" %>
<asp:Content ContentPlaceHolderID="head" Runat="Server">
    <title>采集配置字段设置</title>
</asp:Content>
<asp:Content ContentPlaceHolderID="Content" runat="Server">
    <div>
        <asp:DropDownList runat="server" ID="Link_DP" CssClass="form-control pull-left" Style="max-width: 500px; width: 500px;" AutoPostBack="true" OnSelectedIndexChanged="Link_DP_SelectedIndexChanged"></asp:DropDownList><br />
    </div>
    <table class="table table-bordered table-striped">
        <tr class="tdbg">
            <td style="width: 600px;">
                <asp:Label runat="server" ID="CurLink_L" Style="color: green;"></asp:Label>
                <asp:TextBox ID="txtHtml" CssClass="form-control" runat="server" TextMode="MultiLine" Style="width: 98%; height: 420px; max-width: 98%"></asp:TextBox>
            </td>
            <td>
                <table>
                    <tr>
                        <td>元素类型</td><td>
                            <asp:TextBox runat="server" ID="EType_T" Text="DIV" CssClass="form-control text_md" />
                            <div id="etype_list" class="btn btn-group">
                                <button type="button" class="btn btn-default">DIV</button>
                                <button type="button" class="btn btn-default">TITLE</button>
                                <button type="button" class="btn btn-default">UL</button>
                                <button type="button" class="btn btn-default">BODY</button>
                            </div>
                        </td></tr>
                        <tr><td>ID=</td>
                        <td><asp:TextBox runat="server" ID="ID_T" CssClass="form-control text_md"/></td></tr>
                        <tr><td>Class=</td>
                        <td><asp:TextBox runat="server" ID="Class_T" CssClass="form-control text_md" /><span class="rd_green">(书写与需取值的目标一致)</span></td>                      
                    </tr>
                    <tr><td>开始字符串</td><td><asp:TextBox runat="server" ID="Start_T" TextMode="MultiLine" CssClass="form-control" style="height:100px;" /></td></tr>
                    <tr><td>结束字符串</td><td><asp:TextBox runat="server" ID="End_T" TextMode="MultiLine" CssClass="form-control" style="height:100px;" /></td></tr>
                </table>
                <div class="opdiv" id="byid">
                </div>
                <div>
                   <asp:CheckBox runat="server" ID="Script_Chk" Text="允许Script脚本" />
                   <asp:CheckBox runat="server" ID="Flash_Chk" Text="允许Flash插件" Checked="true" />
                </div>
                <div><asp:Button ID="Test_Btn" CssClass="btn btn-primary" runat="server" Text="测试获取" OnClick="Test_Btn_Click" ValidationGroup="Add" /></div>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="text-center">
                <asp:Button CssClass="btn btn-primary" runat="server" ID="Save_Btn" OnClick="Save_Btn_Click" Text="保存"  />
				<input type="button" value="取消" class="btn btn-primary" onclick="parent.Close();" />
            </td>
        </tr>
    </table>
<asp:HiddenField ID="Json_Hid" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        $(function () {
            $("#etype_list button").click(function () {
                $("#EType_T").val($(this).text());
            });
        })
        function SaveConfig() {//将值保存回父窗口
            var name = "<%=Request.QueryString["name"]%>";
            var json = document.getElementById("Json_Hid").value;
            parent.SaveConfig(name, json);
            parent.Close();
        }
    </script>
</asp:Content>
