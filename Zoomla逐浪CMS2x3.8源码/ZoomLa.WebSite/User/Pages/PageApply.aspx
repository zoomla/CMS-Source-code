<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="PageApply.aspx.cs" Inherits="User_Pages_PageApply" ClientIDMode="Static" ValidateRequest="false" %>

<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的内容</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <table width="100%" border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td align="center" valign="top" bgcolor="e5e5e5">
                <table width="980" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td width="10" background="/User/develop/images/RegSite/peizhi_07.png"></td>
                        <td width="960" valign="top">
                            <table width="960" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="18" background="/User/develop/images/RegSite/peizhi_08.png"></td>
                                </tr>
                            </table>
                            <table width="960" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td height="37" bgcolor="#FFFFFF">&nbsp;</td>
                                </tr>
                            </table>
                            <table width="960" border="0" cellspacing="0" cellpadding="0">
                                <tr id="proc">
                                    <td width="234">
                                        <img src="/User/develop/images/RegSite/peizhi_09.png" width="234" height="42" /></td>
                                    <td background="/User/develop/images/RegSite/peizhi_10_3.png" colspan="5">&nbsp;</td>
                                </tr>
                            </table>
                            <table width="960" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td bgcolor="#FFFFFF">
                                        <table width="960" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="20">
                                                    <table width="960" border="0" cellspacing="0" cellpadding="0">
                                                        <tr>
                                                            <td height="20" colspan="3">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td width="180"></td>
                                                            <td width="600">
                                                                <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                                    <tr>
                                                                        <td colspan="3" align="left"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="3" colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="1" colspan="3"></td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td height="10" colspan="3" align="left">
                                                                            <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td height="10" colspan="2"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="160"></td>
                                                                                    <td width="440" valign="baseline"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="25" colspan="2"></td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3">
                                                                            <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr align="left">
                                                                                    <td>
                                                                                        <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td colspan="3"></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <span class="STYLE15">填写网站及联系信息：</span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="3"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="1" bgcolor="878787"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td height="10" colspan="3"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="100" align="right">
                                                                                        <span class="STYLE15"><span class="STYLE13">*会员帐号</span>：</span>
                                                                                    </td>
                                                                                    <td align="left" class="style4">
                                                                                        <asp:TextBox ID="txtSiteName" runat="server" MaxLength="100" Style="width: 150px;"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSiteName"
                                                                                            Display="Dynamic" ErrorMessage="*">请填写网站名称</asp:RequiredFieldValidator>
                                                                                    </td>
                                                                                    <td>&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="10" colspan="3">
                                                                                        <span class="STYLE12"></span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td width="100" align="right">
                                                                                        <span class="STYLE15">网站关键字：</span>
                                                                                    </td>
                                                                                    <td align="left" class="style4">
                                                                                        <input name="txtSiteKeywords" type="text" maxlength="100" id="txtSiteKeywords" style="width: 150px;"
                                                                                            runat="server" />
                                                                                        <br />
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <span class="STYLE12"></span>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="10" colspan="3">
                                                                                        <span class="STYLE12"></span>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td width="100" align="right">
                                                                                        <span class="STYLE15">&nbsp;网站描述：</span>
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <textarea name="txtSiteCon" rows="2" cols="20" id="txtSiteCon" onkeyup="javascript:Mynetcn_chkInputLength(this,128);"
                                                                                            onfocus="javascript:ClearTip();" style="height: 70px; width: 319px;" runat="server"></textarea>
                                                                                    </td>
                                                                                    <td align="left">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="10" colspan="3" align="left"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <div id="mynetpanel">
                                                                                <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                                                    <tr>
                                                                                        <td width="100" align="right">
                                                                                            <span class="STYLE15"><span class="STYLE13">*</span>联系人：</span>
                                                                                        </td>
                                                                                        <td width="300" align="left">
                                                                                            <asp:TextBox ID="txt_Contactor" runat="server" MaxLength="100" Style="width: 150px;"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_Contactor"
                                                                                                Display="Dynamic" ErrorMessage="*">请填写联系人</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td>&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td height="10" colspan="3">
                                                                                            <span class="STYLE12"></span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td width="100" align="right">
                                                                                            <span class="STYLE15"><span class="STYLE13">*</span>联系电话/手机：</span>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="txt_Telephone" runat="server" MaxLength="100" Style="width: 150px;"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_Telephone"
                                                                                                Display="Dynamic" ErrorMessage="*">请输入联系电话/手机</asp:RequiredFieldValidator>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="Dynamic"
                                                                                                ValidationExpression="^1\d{10}$"
                                                                                                ControlToValidate="txt_Telephone">请输入正确的手机号码</asp:RegularExpressionValidator>
                                                                                        </td>
                                                                                        <td>&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td height="10" colspan="3">
                                                                                            <span class="STYLE12"></span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <span class="STYLE15"><span class="STYLE13">*</span>省、市：</span>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <iframe id="Drop_a" src="/User/Develop/Model/MultiDropList.aspx?CateID=1&FieldName=a&FValue="
                                                                                                runat="server" marginheight="0" marginwidth="0" frameborder="0" width="100%"
                                                                                                height="30" scrolling="no"></iframe>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <div id="Area" style="display: none; color: Red">
                                                                                                请选择地区
                                                                                            </div>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td height="10" colspan="3" align="right">
                                                                                            <span class="STYLE15"></span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <span class="STYLE15"><span class="STYLE13">*</span>市内地址：</span>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="jAddress" runat="server" MaxLength="100" Style="width: 150px;"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="jAddress"
                                                                                                Display="Dynamic" ErrorMessage="*">请填写市内地址</asp:RequiredFieldValidator>
                                                                                        </td>
                                                                                        <td align="left">&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td height="10" colspan="3">
                                                                                            <span class="STYLE12"></span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <span class="STYLE15">邮编：</span>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="jCode" runat="server" MaxLength="100" Style="width: 150px;"></asp:TextBox>
                                                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                                                                                ValidationExpression="[1-9]\d{5}(?!\d)" ControlToValidate="jCode">请输入正确的邮编</asp:RegularExpressionValidator>
                                                                                        </td>
                                                                                        <td align="left">&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td height="10" colspan="3">
                                                                                            <span class="STYLE12"></span>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <span class="STYLE15">版权信息：</span>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <textarea id="Textarea1" name="Copyright" rows="2" cols="20" style="height: 70px; width: 319px;"
                                                                                                runat="server"></textarea>
                                                                                        </td>
                                                                                        <td align="left">&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td height="15" colspan="3"></td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td align="right">
                                                                                            <span class="STYLE15">Logo地址：</span>
                                                                                        </td>
                                                                                        <td align="left">
                                                                                            <asp:TextBox ID="Logo" runat="server" MaxLength="100" Style="width: 150px;"></asp:TextBox>
                                                                                        </td>
                                                                                        <td align="left">&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                </table>
                                                                            </div>
                                                                            <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td width="100"></td>
                                                                                </tr>
                                                                            </table>
                                                                            <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                                                <tr>
                                                                                    <td width="100" rowspan="2">&nbsp;
                                                                                    </td>
                                                                                    <td align="left">
                                                                                        <table width="600" border="0" cellspacing="0" cellpadding="0">
                                                                                            <tr>
                                                                                                <td width="100" rowspan="2"></td>
                                                                                                <td width="129" align="left"></td>
                                                                                                <td width="20" align="left"></td>
                                                                                                <td align="left">
                                                                                                    <asp:ImageButton ID="btnSubmit" runat="server" ImageUrl="/User/develop/images/RegSite/peizhi_icon3.png"
                                                                                                        OnClientClick="sub()" />
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td height="30" colspan="3">&nbsp;
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td height="30">&nbsp;
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td width="180"></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="960" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="20">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="960" border="0" cellspacing="0" cellpadding="0">
                                            <tr>
                                                <td height="18" bgcolor="d1d1d1">&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="10" background="/User/develop/images/RegSite/peizhi_07_0.png"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <div id="hideArea" style="display: none;">
        <input name="status" value="Sub" />
        <input name="SkinID" id="SkinID" runat="server" />
        <input id="txt_a" name="txt_a" runat="server" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script type="text/javascript">
        //<![CDATA[
        var theForm = document.forms['form1'];
        if (!theForm) {
            theForm = document.form1;
        }
        function __doPostBack(eventTarget, eventArgument) {
            if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
                theForm.__EVENTTARGET.value = eventTarget;
                theForm.__EVENTARGUMENT.value = eventArgument;
                theForm.submit();
            }
        }
        //]]>
    </script>
    <script type="text/javascript">
        //<![CDATA[
        function WebForm_OnSubmit() {
            if (typeof (ValidatorOnSubmit) == "function" && ValidatorOnSubmit() == false) return false;
            return true;
        }
        //]]>
    </script>
    <script type="text/javascript">
        var Page_ValidationActive = false;
        if (typeof (ValidatorOnLoad) == "function") {
            ValidatorOnLoad();
        }

        function ValidatorOnSubmit() {
            if (Page_ValidationActive) {
                return ValidatorCommonOnSubmit();
            }
            else {
                return true;
            }
        }
    </script>
    <script type="text/javascript">
        function UpdateMultiDrop(values, id) {
            document.getElementsByName(id)[0].value = values;
        }
        function sub() {
            var strTemp = document.getElementById("txt_a").value;
            var arr = strTemp.split('|');
            if (arr.length == 2) {
                document.getElementById("Area").style.display = "none";
            }
            else {
                document.getElementById("Area").style.display = "block";
            }
            if (document.getElementById("Area").style.display == "none") {
                return true;
            }
            else {
                return false;
            }
        }
    </script>
</asp:Content>
