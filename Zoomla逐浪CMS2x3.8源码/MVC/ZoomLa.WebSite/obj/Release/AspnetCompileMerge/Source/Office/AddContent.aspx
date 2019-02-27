<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContent.aspx.cs" Inherits="ZoomLaCMS.MIS.OA.AddContent"  MasterPageFile="~/Common/Master/UserEmpty.master" ValidateRequest="false"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title>
发送公文
</title>
<script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
<script type="text/javascript" src="/JS/SelectCheckBox.js"></script>
<script type="text/javascript" src="/JS/OAKeyWord.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.config.js"></script>
<script type="text/javascript" charset="utf-8" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
        <div class="draftnav">
            <a href="/Office/Main.aspx">行政公文</a>/<a href="Drafting.aspx"><asp:Label runat="server" ID="nodeNameL"></asp:Label></a>
        </div>
        <div style="padding-left: 10px; padding-right: 10px;">
            <table class="table table-striped table-bordered table-hover" cellspacing="0" cellpadding="0">
                <tr>
                    <td colspan="8" class="trhead">
                        <asp:Label runat="server" ID="nodeNameL2"></asp:Label></td>
                </tr>
                <tr>
                    <td class="text-right">密 级:</td>
                    <td>
                        <asp:DropDownList ID="Secret" runat="server">
                            <asp:ListItem Value="机密">机密</asp:ListItem>
                            <asp:ListItem Value="绝密">绝密</asp:ListItem>
                            <asp:ListItem Value="秘密">秘密</asp:ListItem>
                            <asp:ListItem Value="一般">一般</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="text-right">紧急程度:</td>
                    <td>
                        <asp:DropDownList ID="Urgency" runat="server">
                            <asp:ListItem Value="较紧急">较紧急</asp:ListItem>
                            <asp:ListItem Value="紧急">紧急</asp:ListItem>
                            <asp:ListItem Value="非常紧急">非常紧急</asp:ListItem>
                            <asp:ListItem Value="很紧急">很紧急</asp:ListItem>
                            <asp:ListItem Value="一般">一般</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="text-right" style="width: 100px;">重要程度</td>
                    <td>
                        <asp:DropDownList ID="Importance" runat="server">
                            <asp:ListItem Value="较重要">较重要</asp:ListItem>
                            <asp:ListItem Value="很重要">很重要</asp:ListItem>
                            <asp:ListItem Value="非常重要">非常重要</asp:ListItem>
                            <asp:ListItem Value="一般">一般</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="text-right">表格模板</td>
                    <td>
                        <asp:DropDownList ID="TypeDP" runat="server" AutoPostBack="true" OnSelectedIndexChanged="Type_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="text-right">标 题：</td>
                    <td colspan="3">
                        <asp:TextBox ID="Title_T" runat="server" CssClass="form-control" /><span class="required">*</span>
                    </td>
                    <td class="text-right">栏目：</td>
                    <td colspan="3">
                        <asp:DropDownList runat="server" ID="nodeDP" Width="120"></asp:DropDownList><span class="required">*</span>提示：仅显示你拥有录入权限的栏目</td>
                </tr>
                <tr>
                    <td class="text-right">主题词:</td>
                    <td colspan="5">
                        <asp:TextBox ID="TxtTagKey" runat="server" CssClass="form-control"></asp:TextBox>
                    </td>
                    <td class="text-right">发文日期</td>
                    <td>
                        <asp:TextBox ID="CreateTime" runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" CssClass="form-control"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="text-right">发文人</td>
                    <td colspan="3">
                        <asp:Label ID="userNameT" runat="server" /></td>
                    <td class="text-right">发文部门</td>
                    <td colspan="3">
                        <asp:Label runat="server" ID="userGroupL"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        <asp:TextBox ID="content" name="content" TextMode="MultiLine" Width="100%" Height="400px" runat="server" ClientIDMode="Static"></asp:TextBox>
                    </td>
                </tr>
                <tr id="hasFileTR" runat="server" visible="true">
                    <td class="text-right">已上传文件：<asp:HiddenField runat="server" ID="hasFileData" ClientIDMode="Static" />
                    </td>
                    <td colspan="7" id="hasFileTD" runat="server"></td>
                </tr>
                <tr id="signTr" runat="server" style="display: none;">
                    <td class="text-right">签章：</td>
                    <td colspan="7">
                        <asp:RadioButtonList runat="server" ID="signRadio" RepeatDirection="Horizontal">
                        </asp:RadioButtonList>
                        <span runat="server" id="signTrRemind" visible="false">你尚未配置个人签章</span>
                    </td>
                </tr>
                <tr runat="server" id="upFileTR">
                    <td style="text-align: right;">
                        <input type="button" class="btn btn-primary" value="添加附件" onclick="addAttach();" />
                    </td>
                    <td colspan="7">
                        <table id="attachTB">
                            <tr>
                                <td>
                                    <input type="file" name="fileUP" class="fileUP" /><input type="button" value="删除" onclick="delAttach(this);" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="text-right">操作</td>
                    <td colspan="7">
                        <asp:Button runat="server" CssClass="btn-primary" ID="saveBtn" Text="添加" OnClick="EBtnSubmit_Click" OnClientClick="disBtn(this,2000);" />
                        <input type="button" class="btn-primary" value="重置" onclick="location = location;" style="margin-left: 10px;" />
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
            //------------附件
            var uptr = '<tr><td><input type="file" name="fileUP" class="fileUP" /><input type="button" value="删除" onclick="delAttach(this);" /></td></tr>';
            function addAttach() {
                $("#attachTB").append(uptr);
            }
            function delAttach(obj) {
                $(obj).parent().remove();
            }
            function delHasFile(v, obj) {
                if (confirm('确定要删除该附件吗!')) {
                    rv = $("#hasFileData").val().replace(v, "");
                    $("#hasFileData").val(rv)
                    $(obj).parent().remove();
                }
            }
            //------------
            <%=GetEditor("content")%>
            function setContent(v) {
                setTimeout(function () { editor.setContent(v); }, 800);
            }
            $(function () {
                $("#OAkeyword").tabControl({ maxTabCount: 5, tabW: 80 });
                $("#saveBtn").click(function () {
                    var v = $("#OAkeyword").getTabVals();
                    $("#Keywords").val(v.join(","));
                });
                $("#AddNewBtn").click(function () {
                    var v = $("#OAkeyword").getTabVals();
                    $("#Keywords").val(v.join(","));
                });
                $("#DraftBtn").click(function () {
                    var v = $("#OAkeyword").getTabVals();
                    $("#Keywords").val(v.join(","));
                });
            });
            function closediv(obj) {
                $(obj).parent().remove();
            }
        </script>
</asp:Content>
