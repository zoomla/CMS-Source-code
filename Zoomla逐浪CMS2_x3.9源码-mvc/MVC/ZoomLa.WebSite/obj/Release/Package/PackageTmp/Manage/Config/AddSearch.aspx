<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSearch.aspx.cs" MasterPageFile="~/Manage/I/Default.master" Inherits="ZoomLaCMS.Manage.Config.AddSearch" %>
<asp:Content runat="server" ContentPlaceHolderID="head">
    <title>添加管理导航</title>
<link type="text/css" href="/dist/css/bootstrap-switch.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:HiddenField ID="hideid" runat="server" />
    <table width="100%" border="0" cellpadding="5" cellspacing="1" class="table table-striped table-bordered table-hover">
        <tr>
            <td class="spacingtitle" colspan="2" align="center">
                <asp:Literal ID="LTitle" runat="server" Text="添加管理导航"></asp:Literal>
            </td>
        </tr>
        <tr>
            <td valign="top" style="margin-top: 0px; margin-left: 0px;">
                <table width="100%" cellpadding="2" cellspacing="1" class="table table-striped table-bordered table-hover" style="margin: 0;">
                    <tbody id="Tabs0">
                        <tr>
                            <td style="width: 100px;">
                                <strong>功能名称：</strong>
                            </td>
                            <td style="height: 26px;">
                                <asp:TextBox ID="txtName" onblur="checkname()" Width="300" runat="server" class="form-control"></asp:TextBox>*方便记忆的名称
                                <span style="color:red;" id="txtName_span"></span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ForeColor="Red" ErrorMessage="名称不能为空！" Display="Dynamic" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                <br />
                                <span runat="server" id="Span1"></span>
                            </td>
                        </tr>
                    </tbody>
                    <tbody id="Tabs1">
                        <tr>
                            <td>
                                <strong>文件路径：</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFileUrl" Width="300" runat="server" class="form-control"></asp:TextBox>
                                *您可以直接填入系统内文件名,以便后台访问,如[/manage/Config/SiteInfo.aspx]为系统配置,或输入外部网址提交</td>
                        </tr>
                        <tr onmouseover="this.className='tdbgmouseover'" onmouseout="this,className='tdbg'">
                            <td>
                                <strong>图标地址：</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="ItemIcon_T" Width="300" runat="server" style="display:inline;" CssClass="form-control"></asp:TextBox>
                                <!-- /.modal -->
                                <!-- /.modal -->
                            </td>
                        </tr>  
                        <tr>
                            <td>
                                <strong>打开方式：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rdoOpenType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">原窗口打开</asp:ListItem>
                                    <asp:ListItem Value="1">新窗口打开</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>支持移动：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="SupportMobile" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="1">支持</asp:ListItem>
                                    <asp:ListItem Value="0">不支持</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <strong>块大小：</strong>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="MotroSize" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="3">大</asp:ListItem>
                                    <asp:ListItem Value="2">中</asp:ListItem>
                                    <asp:ListItem Value="1">小</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                             <asp:HiddenField ID="txtOrderID" runat="server" />
                            </tr>

                    </tbody>
                </table>
            </td>
        </tr>
        <tr>
            <td class="text-center" colspan="2">
                <button type="button" id="Edit_Btn" runat="server" class="btn btn-primary" onclick="CheckSubmit()">添加导航</button>
                <asp:Button ID="EBtnSubmit" class="btn btn-primary" Text="添加导航" style="display:none;" runat="server" OnClick="EBtnSubmit_Click" />
                <asp:HiddenField ID="EditSearchName_Hid" runat="server" />
                <input type="button" class="btn btn-primary" name="Button2" value="返回列表" onclick="location.href = 'SearchFunc.aspx'" id="Button2" />
            </td>
        </tr>
    </table>
    <div id="icons" style="display:none"></div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/dist/js/bootstrap-switch.js"></script>
    <script src="/JS/Controls/ZL_Dialog.js" type="text/javascript"></script>
    <script src="/JS/Plugs/IconSelector.js" type="text/javascript"></script>
    <script>
        
        var diag = new ZL_Dialog();
        $().ready(function () {
            //$("input[type=radio][name=glyphicon]").each(function () {
            //    $(this).attr("data-dismiss", "modal");
            //})
            //$("input[type=radio][name=glyphicon]").change(function () {
            //    $("#TextBox1").val($(this).val());
            //})
            var iconsel = new iconSelctor("ItemIcon_T");
            $("[name='linktype_ch']").change(function () {//显示用户组
                ShowUserGroup();
            });
            ShowUserGroup();
            //全选操作
            $("#usercheck_ch").click(function () {
                var obj = $(this);
                obj.closest('td').find('input').each(function (i,v) {
                    $(v)[0].checked = obj[0].checked;
                });
            });
        })
        function ShowUserGroup() {
            if ($("[name='linktype_ch']:checked").val() == "3")
                $(".user_hid_set").show();
            else
                $(".user_hid_set").hide();
        }
        function CheckUserType() {
            $("[name='linktype_ch'][value=3]")[0].checked = true;
        }
        //检查名字是否重复
        function checkname() {
            if ($("#EditSearchName_Hid").val() != "" && $("#EditSearchName_Hid").val() == $("#txtName").val())
            {
                if(formflag)
                { $("#EBtnSubmit").click(); }
                return;
            }
            $.post('AddSearch.aspx', { action: 'checkname', name: $("#txtName").val() }, function (data) {
                if (data == "-1") {
                    $("#Edit_Btn").attr('disabled', 'disabled');
                    $("#txtName_span").text('功能名重复!请重新修改功能名');
                    formflag = false;
                }
                else {
                    $("#txtName_span").text('');
                    $("#Edit_Btn").removeAttr('disabled');
                    if (formflag)
                    { $("#EBtnSubmit").click(); }
                }
            })
        }
        var formflag=false;//表单控制

        function CheckSubmit() {
            formflag = true;
            checkname();
        }
    </script>
    <style>
        .glyphicon-text{font-size:12px;}
        .glyphicon-class .fa{ font-size:24px;}
        #myModal .table td{ vertical-align:top;}
    </style>
</asp:Content>
