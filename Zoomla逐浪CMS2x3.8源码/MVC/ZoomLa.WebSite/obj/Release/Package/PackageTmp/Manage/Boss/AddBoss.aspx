<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddBoss.aspx.cs" Inherits="ZoomLaCMS.Manage.Boss.AddBoss" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
        <title>添加加盟商</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <ul class="nav nav-tabs">
        <li class="active"><a href="#Tabs0" data-toggle="tab">企业信息</a></li>
        <li><a href="#Tabs1" data-toggle="tab">银行账户信息</a></li>
        <li><a href="#Tabs2" data-toggle="tab">联系人信息</a></li>
    </ul>
    <asp:HiddenField ID="HdnParentId" Value="0" runat="server" />
    <asp:HiddenField ID="HdnDepth" Value="0" runat="server" />
    <asp:HiddenField ID="HdnOrderID" Value="0" runat="server" />
    <asp:HiddenField ID="HdnMenu" Value="0" runat="server" />
    <div class="tab-content panel-body padding10">
        <div class="tab-pane active" id="Tabs0">
            <table class="table table-striped table-bordered table-hover">
                    <tbody>
                        <%--<tr class="tdbg">
                            <td class="tdbgleft" style="width: 180px;">
                                <strong>代理商用户名：<span style="color: Red;">*</span></strong></td>
                            <td>
                                <asp:TextBox ID="username" runat="server" CssClass="form-control text_300"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                    ControlToValidate="username" ErrorMessage="代理商用户名不能为空"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>代理商用户密码：<span style="color: Red;">*</span></strong></td>
                            <td>
                                <asp:TextBox ID="password" runat="server" TextMode="Password" CssClass="form-control text_300"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                    ControlToValidate="password" ErrorMessage="代理商用户密码不能为空"></asp:RequiredFieldValidator>
                            </td>
                        </tr>--%>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>地区：</strong>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlProvince"  CssClass="form-control"  Width="100px"  runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProvince_SelectedIndexChanged">
                                </asp:DropDownList>
                                &nbsp;<asp:DropDownList ID="ddlCity"  CssClass="form-control" Width="100px" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>代理商级别：</strong></td>
                            <td>
                                <asp:RadioButtonList ID="tx_ShopType" runat="server" RepeatColumns="4">
                                    <asp:ListItem Value="1" Selected="True">市服务中心</asp:ListItem>
                                    <asp:ListItem Value="2">服务E店</asp:ListItem>
                                    <asp:ListItem Value="3">省服务中心</asp:ListItem>
                                    <asp:ListItem Value="4">售卡员</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>代理商名称：</strong> <span style="color: Red;">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="tx_cname" runat="server" CssClass="form-control text_300"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                    ControlToValidate="tx_cname" ErrorMessage="代理商名称不能为空"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>代理商费用：</strong><span style="color: Red;">*</span>
                            </td>
                            <td>
                                <asp:TextBox ID="CMoney" data-error="代理商费用不能为空且必须为数字" runat="server"   CssClass="form-control text_300 notnull numtxt"></asp:TextBox>元
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                    ControlToValidate="CMoney" ErrorMessage="代理商费用不能为空"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                    ErrorMessage="代理商费用必须为数字" ValidationExpression="^(\d*)([.]{0,1})(\d{0,5})$"
                                    ControlToValidate="CMoney"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>代理商地址：</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="tx_adderss" runat="server"  CssClass="form-control text_300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>法定代理人：</strong></td>
                            <td>

                                <asp:TextBox ID="cx_Agent" runat="server"  CssClass="form-control text_300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg" runat="server" visible="false">
                            <td class="tdbgleft">
                                <strong>营业执照：</strong></td>
                            <td>
                                <asp:TextBox ID="cx_license" runat="server"  CssClass="form-control text_300"></asp:TextBox>
                                <iframe id="Iframe1" style="top: 2px" src="/manage/Shop/fileupload.aspx?menu=cx_license" width="100%" height="25px" frameborder="0" marginheight="0" marginwidth="0" scrolling="no"></iframe>
                            </td>
                        </tr>

                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>代理商电话：</strong></td>
                            <td>
                                <asp:TextBox ID="tx_CTel" runat="server"  CssClass="form-control text_300"></asp:TextBox>
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft">
                                <strong>公司介绍：</strong></td>
                            <td>
                                <asp:TextBox ID="tx_CInfo" runat="server"  TextMode="MultiLine" CssClass="form-control text_300"></asp:TextBox>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        <div class="tab-pane" id="Tabs1">
            <table class="table table-striped table-bordered table-hover">
                <tbody>
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 180px;">
                            <strong>合同协议号：<span style="color: Red;">*</span></strong></td>
                        <td>
                            <asp:TextBox ID="ContractNum" data-error="合同协议号不能为空" runat="server"  CssClass="form-control text_300 notnull"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                ControlToValidate="ContractNum" ErrorMessage="合同协议号不能为空"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>开户人：<span style="color: Red;">*</span> </strong></td>
                        <td>
                            <asp:TextBox ID="AccountPeople" data-error="开户人不能为空" runat="server" CssClass="form-control text_300 notnull" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                ControlToValidate="AccountPeople" ErrorMessage="开户人不能为空"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>开户银行1：</strong>
                            <span style="color: Red;">* </span></td>
                        <td>
                            <asp:TextBox ID="Bank" data-error="开户银行不能为空" runat="server" CssClass="form-control text_300 notnull" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                ControlToValidate="Bank" ErrorMessage="开户银行不能为空"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>银行账号1：<span style="color: Red;">*</span> </strong></td>
                        <td>
                            <asp:TextBox ID="BankNum" runat="server" data-error="银行账号不能为空" CssClass="form-control text_300 notnull" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                ControlToValidate="BankNum" ErrorMessage="银行账号不能为空"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>开户人：</strong></td>
                        <td>
                            <asp:TextBox ID="AccountPeople2" runat="server" CssClass="form-control text_300" ></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>开户银行2：</strong>
                        </td>
                        <td>
                            <asp:TextBox ID="Bank2" runat="server" CssClass="form-control text_300" ></asp:TextBox></td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>银行账号2：</strong></td>
                        <td>
                            <asp:TextBox ID="BankNum2" runat="server" CssClass="form-control text_300" ></asp:TextBox></td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="tab-pane" id="Tabs2">
            <table class="table table-striped table-bordered table-hover">
                <tbody class="tab-pane"  id="Tabs2">
                    <tr class="tdbg">
                        <td class="tdbgleft" style="width: 180px;">
                            <strong>联系人姓名：</strong><br />

                        </td>
                        <td>
                            <asp:TextBox ID="linkname" runat="server" CssClass="form-control text_300" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>性别：</strong><br />

                        </td>
                        <td>
                            <asp:RadioButtonList ID="linksex" runat="server" RepeatColumns="2"  >
                                <asp:ListItem Selected="True">男</asp:ListItem>
                                <asp:ListItem>女</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>

                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>联系人职务：</strong><br />

                        </td>
                        <td>
                            <asp:TextBox ID="linkPositions" runat="server" CssClass="form-control text_300" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>联系电话：</strong><br />

                        </td>
                        <td>
                            <asp:TextBox ID="linktel" runat="server" CssClass="form-control text_300" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>传真：</strong><br />

                        </td>
                        <td>
                            <asp:TextBox ID="fax" runat="server" CssClass="form-control text_300" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>邮编：</strong><br />

                        </td>
                        <td>
                            <asp:TextBox ID="PostCode" runat="server" data-error="邮政编码格式不正确!" CssClass="form-control text_300 emailtxt" ></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="PostCode"
                                ErrorMessage="邮政编码格式不正确!" ValidationExpression="^\d{6}$" SetFocusOnError="True"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>Email：</strong><br />
                        </td>
                        <td>
                            <asp:TextBox ID="email" runat="server" CssClass="form-control text_300" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>证件类型：<span style="color: Red;">*</span> </strong>
                            <br />
                        </td>
                        <td>
                            <asp:RadioButtonList ID="Documents" runat="server"   RepeatColumns="10">
                                <asp:ListItem Selected="True" Value="身份证">身份证</asp:ListItem>
                                <asp:ListItem Value="军人证">军人证</asp:ListItem>
                                <asp:ListItem Value="学生证">学生证</asp:ListItem>
                                <asp:ListItem Value="警官证">警官证</asp:ListItem>
                                <asp:ListItem Value="其他证件">其他证件</asp:ListItem>
                            </asp:RadioButtonList>

                        </td>
                    </tr>
                    <tr class="tdbg">
                        <td class="tdbgleft">
                            <strong>证件号：<span style="color: Red;">*</span> </strong>
                            <br />

                        </td>
                        <td>
                            <asp:TextBox ID="DocumentsNUm" runat="server" data-error="证件号不能为空" CssClass="form-control text_300 notnull" ></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                ControlToValidate="DocumentsNUm" ErrorMessage="证件号不能为空"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        
    </div>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td align="center">&nbsp; &nbsp;
                <asp:Button ID="EBtnSubmit" Text="保存" runat="server" CssClass="btn btn-primary" OnClick="EBtnSubmit_Click" />&nbsp; &nbsp;
                <input name="Cancel" type="button" id="BtnCancel" class="btn btn-primary" value="取消" onclick="window.location.href = 'Bosstree.aspx'" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
        <script src="/JS/ZL_Regex.js"></script>
        <script src="/JS/Common.js" type="text/javascript"></script>
        <script type="text/JavaScript">
            function showCustom(value) {
                var index = parseInt(value);

                for (i = 2; i <= 20; i++) {
                    if (i <= index)
                        document.getElementById("Custom" + i).style.display = "";
                    else
                        document.getElementById("Custom" + i).style.display = "none";
                }

            }
            $().ready(function () {
                $('form').submit(function () {
                    $(".notnull").each(function () {
                        if ($(this).val().trim() == "")
                        { alert($(this).data('error')); return false;}
                    });
                    $(".numtxt").each(function () {
                        if (!ZL_Regex.isNum($(this).val()))
                        { alert($(this).data('error')); return false; }
                    });
                });
            });
    </script>
</asp:Content>