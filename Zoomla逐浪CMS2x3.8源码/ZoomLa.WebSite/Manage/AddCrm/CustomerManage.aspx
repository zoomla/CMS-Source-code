<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CustomerManage.aspx.cs" Inherits="manage_AddCRM_CustomerManage" EnableViewStateMac="false" ValidateRequest="false" MasterPageFile="~/Manage/I/Default.master"%>
<asp:Content runat="server" ContentPlaceHolderID="head">
<title id="User_Title" runat="server">客户列表</title>
<script src="/JS/ICMS/area.js"></script>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
    <asp:Label runat="server" ID="liTitle" Visible="false"></asp:Label>
    <asp:Label runat="server" ID="Label1" Visible="false" Text="添加个人客户"></asp:Label>
            <ul class="nav nav-tabs">
                <li class="active"><a href="#tab0" data-toggle="tab" onclick="ShowTabs(0)">基本信息</a></li>
                <li><a href="#tab1" data-toggle="tab" onclick="ShowTabs(1)">联络信息</a></li>
                <li><a href="#tab2" data-toggle="tab" onclick="ShowTabs(2)">企业信息</a></li>
                <li><a href="#tab3" data-toggle="tab" onclick="ShowTabs(7)">企业选项</a></li>
                <li><a href="#tab4" data-toggle="tab" onclick="ShowTabs(5)">备注信息</a></li>
                <li><a href="#tab5" data-toggle="tab" onclick="ShowTabs(4)">个人信息</a></li>
                <li><a href="#tab6" data-toggle="tab" onclick="ShowTabs(3)">业务信息</a></li>
                <li><a href="#tab7" data-toggle="tab" onclick="ShowTabs(6)">会员选项</a></li>
            </ul>
                 <table id="Tabs0"  class="table table-striped table-bordered table-hover margin_t5">
                        <tr>
                            <td align="right" class="tdbgleft">
                                客户类别：
                            </td>
                            <td>
                                <table id="RadlClientType" border="0">
                                    <tr>
                                        <td>
                                            <input id="RadlClientType_0" type="radio" checked name="RadlClientType"  runat="server" onclick="isPerson('Enterprise_Add')" />
                                            <label for="RadlClientType_0">
                                                企业客户</label>
                                        </td>
                                        <td>
                                            <input id="RadlClientType_1" type="radio" name="RadlClientType" runat="server" onclick="isPerson('Person_Add')" />
                                            <label for="RadlClientType_1">
                                                个人客户</label>
                                        </td>
                                    </tr>
                                </table> 
                            </td>
                            <td align="right" class="tdbgleft">
                            </td>
                            <td>
                                <table id="Table1" border="0">
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%" align="right" class="tdbgleft">
                                客户名称：
                            </td>
                            <td style="width: 38%">
                                <input type="text" maxlength="100" id="TxtClientName" class="form-control text_300" runat="server" />
                                <span style="color: Red;">* </span><span id="ctl00_CphContent_ValrClientName" style="color: Red;
                                    display: none;">客户名称不能为空！</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="TxtClientName" ErrorMessage="客户名称不能为空！"></asp:RequiredFieldValidator>
                            </td>
                            <td style="width: 15%" align="right" class="tdbgleft">
                                客户编号：
                            </td>
                            <td style="width: 38%">
                                <asp:Label runat="server" ID="TxtClientNum" maxlength="20"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                助记名称：
                            </td>
                            <td>
                                <input type="text" maxlength="20" runat="server" id="TxtShortedForm" class="form-control text_300" />
                                <span style="color: Red;">* </span><span id="ctl00_CphContent_ValrShortedForm" style="color: Red;
                                    display: none;">助记名称不能为空！</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="TxtShortedForm" ErrorMessage="助记名称不能为空！"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" class="tdbgleft">
                                上级客户：
                            </td>
                            <td>
                                <input type="text" id="ClientSelect" class="form-control text_300" runat="server" />&nbsp;
                                <input value=" ... " onclick="Openwin()"
                                    type="button" style="cursor: pointer;" class="btn btn-primary" />
                            </td>
                        </tr>
             <%--           <tr>
                            <td align="right" class="tdbgleft">
                                区域：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropArea" runat="server" CssClass="form-control text_300" AutoPostBack="false" EnableViewState="true">
                                </asp:DropDownList>
                            </td>
                            <td align="right" class="tdbgleft">
                                行业：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropClientField" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                价值评估：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropValueLevel" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                            <td align="right" class="tdbgleft">
                                信用等级：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropCreditLevel" runat="server" CssClass="form-control text_300" AutoPostBack="false" >
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                重要程度：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropImportance" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                            <td align="right" class="tdbgleft">
                                关系等级：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropConnectionLevel" runat="server" CssClass="form-control text_300">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                客户来源：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropSourceType" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                                <span style="color: Red;">*</span> <span id="ctl00_CphContent_ValxSourceType" style="color: Red;
                                    visibility: hidden;">请选择客户来源！</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                    ControlToValidate="DropSourceType" ErrorMessage="客户来源不能为空！"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" class="tdbgleft">
                                阶段：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropPhaseType" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                                <span style="color: Red;">*</span> <span id="ctl00_CphContent_ValxPhaseType" style="color: Red;
                                    visibility: hidden;">请选择阶段！</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                    ControlToValidate="DropPhaseType" ErrorMessage="阶段不能为空！"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" class="tdbgleft">
                                客户组别：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropGroupID" runat="server" CssClass="form-control text_300" AutoPostBack="false">
                                </asp:DropDownList>
                                <span style="color: Red;">*</span> <span id="ctl00_CphContent_ValxGroupID" style="color: Red;
                                    visibility: hidden;">请选择客户组别！</span>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                    ControlToValidate="DropGroupID" ErrorMessage="客户组别不能为空！"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" class="tdbgleft">
                            跟进人：
                            </td>
                            <td>
                                <asp:DropDownList runat="server" id="FPMandp" CssClass="form-control text_300"></asp:DropDownList>
                            </td>
                        </tr>--%>
                        <asp:Literal ID="ModelHtml" runat="server"></asp:Literal>
                        <%--<asp:Literal runat="server" ID="htmlStr"></asp:Literal>--%>
                    </table>
                    <table id="Tabs1" style="width: 100%; background-color: white; display:none;">
                        <tbody id="infoE">
                            <tr> 
                                <td colspan="3" style="height: 23px">
                                    <div id="ctl00_CphContent_Region_UpnlRegion">
                                        <table class="table table-striped table-bordered table-hover" border="0" cellpadding="2" cellspacing="1">
                                            <tr>
                                                <td style="width: 15%" align="right" class="tdbgleft">
                                                    国家/地区： 
                                                </td>
                                                <td colspan="2" align="left">
                                                    <select name="country" id="country" runat="server" class="form-control text_300">
                                                        <option value="">请选择</option>
                                                        <option value="中华人民共和国" selected>中华人民共和国</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td  align="right" class="tdbgleft">
                                                    省/市/自治区：
                                                </td>
                                                <td align="left">
                                                    <select name="province" id="province" class="form-control text_300">
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" class="tdbgleft">
                                                    市/县/区/旗：
                                                </td>
                                                <td align="left">
                                                    <select name="city" id="city" class="form-control text_300">
                                                    </select>
                                                    <select id="tbCounty" style="display:none;"></select>
                                                    <asp:HiddenField ID="Adrees_Hid" runat="server" />
                                                </td>
                                            </tr>
                                             <tr class="tdbg">
                                            <td  align="right" class="tdbgleft">
                                                联系地址：
                                            </td>
                                            <td>
                                                <input name="TxtAddress" type="text" runat="server" maxlength="255" id="TxtAddress"
                                                     class="form-control text_300"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" class="tdbgleft">
                                                邮政编码：
                                            </td>
                                            <td>
                                                <input name="TxtZipCodeW" type="text" id="TxtZipCodeW" class="form-control text_300" runat="server" />
                                                <span id="ctl00_CphContent_VzipZipCode" style="color: Red; display: none;">填写的邮政编码格式不正确！</span>
                                            </td>
                                        </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr> 
                            <tr style="display:none; width:100%" id="t1">
                                <td style="width:15%" align="right" class="tdbgleft">
                                    联系电话：
                                </td>
                                <td style="width: 38%">
                                    <input name="TxtPhone" type="text" maxlength="30" id="TxtPhone" class="form-control text_300" runat="server" />
                                    <span id="ctl00_CphContent_VtelPhone" style="color: Red; display: none;">电话格式不正确</span>
                                    <span id="spanPhone" style="display: none; color: Red">联系电话已经存在，请勿重复添加！</span>
                                </td>
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    传真号码：
                                </td>
                                <td style="width: 38%">
                                    <input name="TxtFax1" type="text" maxlength="30" id="TxtFax1" class="form-control text_300" runat="server" />
                                    <span id="ctl00_CphContent_VtelFax" style="color: Red; display: none;">传真格式不正确</span>
                                </td>
                            </tr>
                            <tr
                                style="display: none" id="s1">
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    办公电话：
                                </td>
                                <td style="width: 38%">
                                    <input name="TxtOfficePhone" type="text" id="TxtOfficePhone" class="form-control text_300" runat="server" />
                                    <span style="color: Red; display: none;" id="spanValidOfficePhone">办公电话已经存在，请勿重复添加！</span>
                                    <span id="ctl00_CphContent_LiaisonInformation1_VtelOfficePhone" style="color: Red;
                                        display: none;">填写的办公电话号码格式不正确</span>
                                </td>
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    住宅电话：
                                </td>
                                <td style="width: 38%">
                                    <input name="TxtHomePhone" type="text" id="TxtHomePhone" class="form-control text_300" runat="server" />
                                    <span style="color: Red; display: none;" id="spanHomePhone">住宅电话已经存在，请勿重复添加！</span>
                                    <span id="ctl00_CphContent_LiaisonInformation1_VtelHomePhone" style="color: Red;
                                        display: none;">填写的住宅电话号码格式不正确</span>
                                </td>
                            </tr>
                            <tr
                                style="display: none" id="s2">
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    移动电话：
                                </td>
                                <td>
                                    <input name="TxtMobile" type="text" id="TxtMobile" class="form-control text_300" runat="server" />
                                    <span style="color: Red; display: none;" id="spanMobile">移动电话已经存在，请勿重复添加！</span>
                                    <span id="ctl00_CphContent_LiaisonInformation1_VmblMobile" style="color: Red; display: none;">
                                        填写的移动电话号码格式不正确</span>
                                </td>
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    传真号码：
                                </td>
                                <td>
                                    <input name="TxtFax" type="text" id="TxtFax" class="form-control text_300" runat="server" />
                                </td>
                            </tr>
                            <tr
                                style="display: none" id="s3">
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    小灵通：
                                </td>
                                <td>
                                    <input name="TxtPHS" type="text" id="TxtPHS" class="form-control text_300" runat="server" />
                                </td>
                                <td class="tdbgleft" align="right">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr
                                style="display: none" id="s4">
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    个人主页：
                                </td>
                                <td>
                                    <input name="TxtHomepage" type="text" id="TxtHomepage" class="form-control text_300" runat="server" />
                                    <span style="color: Red; display: none;" id="spanHomepage">主页已经存在，请勿重复添加！</span>
                                </td>
                                <td class="tdbgleft" align="right">
                                    Email地址：
                                </td>
                                <td>
                                    <input name="TxtEmail" type="text" id="TxtEmail" class="form-control text_300" runat="server" />
                                    <span style="color: Red; display: none;" id="spanValidEmail">Email地址已经存在，请勿重复添加！</span>
                                    <span id="ctl00_CphContent_LiaisonInformation1_VmailEmail" style="color: Red; display: none;">
                                        填写的电子邮箱格式不正确！</span>
                                </td>
                            </tr>
                            <tr
                                style="display: none" id="s5">
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    QQ号码：
                                </td>
                                <td>
                                    <input name="TxtQQ" type="text" id="TxtQQ" class="form-control text_300" runat="server" />
                                    <span style="color: Red; display: none;" id="spanValidQQ">QQ号码已经存在，请勿重复添加！</span>
                                    <span id="ctl00_CphContent_LiaisonInformation1_VQQ" style="color: Red; display: none;">
                                        填写的QQ号码格式不正确！</span>
                                </td>
                                <td class="tdbgleft" align="right">
                                    MSN帐号：
                                </td>
                                <td>
                                    <input name="TxtMSN" type="text" id="TxtMSN" class="l_input" runat="server" />
                                    <span style="color: Red; display: none;" id="spanValidMsn">Msn号码已经存在，请勿重复添加！</span>
                                </td>
                            </tr>
                            <tr
                                style="display: none" id="s6">
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    ICQ号码：
                                </td>
                                <td>
                                    <input name="TxtICQ" type="text" id="TxtICQ" class="form-control text_300" runat="server" />
                                </td>
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    雅虎通帐号：
                                </td>
                                <td>
                                    <input name="TxtYahoo" type="text" id="TxtYahoo" class="form-control text_300" runat="server" />
                                </td>
                            </tr>
                            <tr style="display: none" id="s7">
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    UC帐号：
                                </td>
                                <td>
                                    <input name="TxtUC" type="text" id="TxtUC" class="form-control text_300" runat="server" />
                                </td>
                                <td style="width: 15%" align="right" class="tdbgleft">
                                    Aim帐号：
                                </td>
                                <td>
                                    <input name="TxtAim" type="text" id="TxtAim" class="l_input" runat="server" />
                                    <input type="hidden" name="ctl00$CphContent$LiaisonInformation1$HdnQQ" id="ctl00_CphContent_LiaisonInformation1_HdnQQ" />
                                    <input type="hidden" name="ctl00$CphContent$LiaisonInformation1$HdnEmail" id="ctl00_CphContent_LiaisonInformation1_HdnEmail" />
                                    <input type="hidden" name="ctl00$CphContent$LiaisonInformation1$HdnMsn" id="ctl00_CphContent_LiaisonInformation1_HdnMsn" />
                                    <input type="hidden" name="ctl00$CphContent$LiaisonInformation1$HdnHomepage" id="ctl00_CphContent_LiaisonInformation1_HdnHomepage" />
                                    <input type="hidden" name="ctl00$CphContent$LiaisonInformation1$HdnMobile" id="ctl00_CphContent_LiaisonInformation1_HdnMobile" />
                                    <input type="hidden" name="ctl00$CphContent$LiaisonInformation1$HdnHomephone" id="ctl00_CphContent_LiaisonInformation1_HdnHomephone" />
                                    <input type="hidden" name="ctl00$CphContent$LiaisonInformation1$HdnOfficePhone" id="ctl00_CphContent_LiaisonInformation1_HdnOfficePhone" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <table class="table table-striped table-bordered table-hover" id="Tabs2" cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;
                        display: none">
                        <tr>
                            <td style="width: 15%" align="right" class="tdbgleft">
                                开户银行：
                            </td>
                            <td style="width: 38%">
                                <input name="TxtBankOfDeposit" type="text" maxlength="50" id="TxtBankOfDeposit" class="form-control text_300"
                                    runat="server" />
                            </td>
                            <td align="right" class="tdbgleft">
                                银行帐号：
                            </td>
                            <td>
                                <input name="TxtBankAccount" type="text" maxlength="50" id="TxtBankAccount" class="form-control text_300"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                税号：
                            </td>
                            <td>
                                <input name="TxtTaxNum" type="text" maxlength="50" id="TxtTaxNum" class="form-control text_300"
                                    runat="server" />
                            </td>
                            <td class="tdbgleft" align="right">
                                网址：
                            </td>
                            <td>
                                <input name="TxtHomepage1" type="text" maxlength="255" id="TxtHomepage1" class="form-control text_300"
                                    runat="server" />
                                <span id="ctl00_CphContent_regHomePage" style="color: Red; display: none;">填写的URL地址格式不正确</span>
                                <span id="span1" style="display: none; color: Red">网址已经存在，请勿重复添加！</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                行业地位：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropStatusInField" runat="server" AutoPostBack="false" CssClass="form-control text_300">
                                </asp:DropDownList>
                            </td>
                            <td class="tdbgleft" align="right">
                                公司规模：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropCompanySize" runat="server" AutoPostBack="false" CssClass="form-control text_300">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                业务范围：
                            </td>
                            <td>
                                <input name="TxtBusinessScope" type="text" maxlength="100" id="TxtBusinessScope"
                                    class="form-control text_300" runat="server" />
                            </td>
                            <td class="tdbgleft" align="right">
                                年销售额：
                            </td>
                            <td>
                                <input name="TxtAnnualSales" type="text" maxlength="50" id="TxtAnnualSales" class="form-control text_300"
                                    runat="server" />
                                万元 <span id="ctl00_CphContent_rangAnnualSales" style="color: Red; display: none;">请输入正确的年销售额</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                经营状态：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropManagementForms" runat="server" AutoPostBack="false" CssClass="form-control text_300">
                                </asp:DropDownList>
                            </td>
                            <td class="tdbgleft" align="right">
                                注册资本：
                            </td>
                            <td>
                                <input name="TxtRegisteredCapital" type="text" maxlength="50" id="TxtRegisteredCapital"
                                    class="form-control text_300" runat="server" />
                                万元 <span id="ctl00_CphContent_rangRegisteredCapital" style="color: Red; display: none;">请输入正确的注册资本</span>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-striped table-bordered table-hover" id="Tabs3" cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;
                        display: none">
                        <tr>
                            <td style="width: 15%" align="right" class="tdbgleft">
                                单位名称：
                            </td>
                            <td style="width: 38%">
                                <input name="TxtCompany" type="text" maxlength="200" id="TxtCompany" class="form-control text_300"
                                    runat="server" />
                            </td>
                            <td style="width: 15%" align="right" class="tdbgleft">
                                所属部门：
                            </td>
                            <td style="width: 38%">
                                <input name="TxtDepartment" type="text" id="TxtDepartment" class="form-control text_300" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                职位：
                            </td>
                            <td>
                                <input name="TxtPosition" type="text" id="TxtPosition" class="form-control text_300" runat="server" />
                            </td>
                            <td class="tdbgleft" align="right">
                                负责业务：
                            </td>
                            <td>
                                <input name="TxtOperation" type="text" id="TxtOperation" class="form-control text_300" runat="server" />
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" align="right">
                                称谓：
                            </td>
                            <td>
                                <input name="TxtTitle" type="text" id="TxtTitle" class="form-control text_300" runat="server" />
                            </td>
                            <td class="tdbgleft" align="right">
                                单位地址：
                            </td>
                            <td>
                                <input name="TxtCompanyAddress" type="text" maxlength="255" id="TxtCompanyAddress"
                                    class="form-control text_300" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table class="table table-striped table-bordered table-hover" id="Tabs4" cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;
                        display: none">
                        <tr>
                            <td style="width: 15%" align="right" class="tdbgleft">
                                出生日期：
                            </td>
                            <td style="width: 38%">
                                <input name="DpkBirthday" type="text" id="DpkBirthday" value="1986-10-22 0:0:0" style="font-size: 9pt; " runat="server" onclick="WdatePicker({ dateFmt: 'yyyy/MM/dd HH:mm:ss' });" class="form-control text_300" />
                            </td>
                            <td style="width: 15%" align="right" class="tdbgleft">
                                证件号码：
                            </td>
                            <td style="width: 38%">
                                <input name="TxtIDCard" type="text" id="TxtIDCard" class="form-control text_300" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                籍贯：
                            </td>
                            <td>
                                <input name="TxtNativePlace" type="text" id="TxtNativePlace" class="form-control text_300" runat="server" />
                            </td>
                            <td class="tdbgleft" align="right">
                                民族：
                            </td>
                            <td>
                                <input name="TxtNation" type="text" id="TxtNation" class="form-control text_300" runat="server" />
                            </td>
                        </tr>
                        <tr class="tdbg">
                            <td class="tdbgleft" align="right">
                                性别：
                            </td>
                            <td>
                                <table id="RadlSex" border="0">
                                    <tr>
                                        <td>
                                            <input id="RadlSex_0" type="radio" name="RadlSex" value="0" checked="checked" onclick="Fsex('2')" />
                                            <label for="RadlSex_0">
                                                保密</label>
                                        </td>
                                        <td>
                                            <input id="RadlSex_1" type="radio" name="RadlSex" value="1" onclick="Fsex('1')" />
                                            <label for="RadlSex_1">
                                                男</label>
                                        </td>
                                        <td>
                                            <input id="RadlSex_2" type="radio" name="RadlSex" value="2" onclick="Fsex('0')" />
                                            <label for="RadlSex_2">
                                                女</label><input name="sex" type="text" id="sex" class="l_input" runat="server" style="display: none" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="tdbgleft" align="right">
                                婚姻状况：
                            </td>
                            <td>
                                <table id="RadlMarriage" border="0">
                                    <tr>
                                        <td>
                                            <input id="RadlMarriage_0" type="radio" name="RadlMarriage" value="0" checked="checked"
                                                onclick="FMarriage('0')" />
                                            <label for="RadlMarriage_0">
                                                保密</label>
                                        </td>
                                        <td>
                                            <input id="RadlMarriage_1" type="radio" name="RadlMarriage" value="1" onclick="FMarriage('1')" />
                                            <label for="RadlMarriage_1">
                                                未婚</label>
                                        </td>
                                        <td>
                                            <input id="RadlMarriage_2" type="radio" name="RadlMarriage" value="2" onclick="FMarriage('2')" />
                                            <label for="RadlMarriage_2">
                                                已婚</label>
                                        </td>
                                        <td>
                                            <input id="RadlMarriage_3" type="radio" name="RadlMarriage" value="3" onclick="FMarriage('3')" />
                                            <label for="RadlMarriage_3">
                                                离异</label><input name="Marriage" type="text" id="Marriage" class="l_input" runat="server"
                                                    style="display: none" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                学历：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropEducation" runat="server" AutoPostBack="false" CssClass="form-control text_300">
                                </asp:DropDownList>
                            </td>
                            <td class="tdbgleft" align="right">
                                毕业学校：
                            </td>
                            <td>
                                <input name="TxtGraduateFrom" type="text" id="TxtGraduateFrom" class="form-control text_300" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                生活爱好：
                            </td>
                            <td>
                                <input name="TxtInterestsOfLife" type="text" id="TxtInterestsOfLife" class="form-control text_300"
                                    runat="server" />
                            </td>
                            <td class="tdbgleft" align="right">
                                文化爱好：
                            </td>
                            <td>
                                <input name="TxtInterestsOfCulture" type="text" id="TxtInterestsOfCulture" class="form-control text_300"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                娱乐休闲爱好：
                            </td>
                            <td>
                                <input name="TxtInterestsOfAmusement" type="text" id="TxtInterestsOfAmusement" class="form-control text_300"
                                    runat="server" />
                            </td>
                            <td class="tdbgleft" align="right">
                                体育爱好：
                            </td>
                            <td>
                                <input name="TxtInterestsOfSport" type="text" id="TxtInterestsOfSport" class="form-control text_300"
                                    runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                其他爱好：
                            </td>
                            <td>
                                <input name="TxtInterestsOfOther" type="text" id="TxtInterestsOfOther" class="form-control text_300"
                                    runat="server" />
                            </td>
                            <td class="tdbgleft" align="right">
                                月收入：
                            </td>
                            <td>
                                <asp:DropDownList ID="DropIncome" runat="server" AutoPostBack="false" CssClass="form-control text_300">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdbgleft" align="right">
                                家庭情况：
                            </td>
                            <td colspan="3">
                                <textarea name="TxtFamily" rows="2" cols="20" id="TxtFamily" class="form-control text_300" style="height: 74px;
                                    width: 400px;" runat="server"></textarea>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-striped table-bordered table-hover" id="Tabs5" cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;
                        display: none">
                        <tr class="tdbg">
                            <td style="width: 15%" align="right" class="tdbgleft">
                                备注：
                            </td>
                            <td colspan="3">
                                <textarea name="TxtRemark" rows="2" cols="20" id="TxtRemark" class="form-control text_300" style="height: 100px;
                                    width: 400px;" runat="server"></textarea>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-striped table-bordered table-hover" id="Tabs6" cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;
                        display: none">
                        <tr class="tdbg">
                            <td colspan="4">
                                <div id="ctl00_CphContent_UpnlChildProducts">
                                    <table cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;">
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table class="table table-striped table-bordered table-hover" id="Tabs7" cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;
                        display: none">
                        <tr class="tdbg">
                            <td colspan="4">
                                <div id="ctl00_CphContent_UpnlCompany">
                                    <table cellpadding="2" cellspacing="1" style="width: 100%; background-color: white;">
                                    </table>
                                </div>
                            </td>
                        </tr>
                    </table>
        <div class="text-center" style="margin-bottom:10px;">
            <asp:Button ID="ctl00_CphContent_BtnSave" runat="server" Text="保存客户信息" OnClick="ctl00_CphContent_BtnSave_Click"
            class="btn btn-primary" />
            <asp:Button ID="ctl00_CphContent_BtnSave0" runat="server" Text="取消" 
                class="btn btn-primary" onclick="ctl00_CphContent_BtnSave0_Click" CausesValidation="false" />
            </div>
<%--        <input type="button" runat="server" id="FPBtn" value="添加跟进" class="btn btn-primary" onclick="$('#FPDiv').toggle(); $('#FPGrid').toggle(); createTxt();" visible="false"/>--%>
    <div id="FPDiv" style="display:none;">
        <table id="public" style="position: relative;" class="border" width="80%" cellpadding="0" cellspacing="0">
            <tr class="tdbg">
                <td class="style1" style="text-align:right;" ><strong>添加人信息：</strong></td>
                <td>
                    跟进人：<asp:Label runat="server" ID="FPMan" />
                    跟进时间：  <asp:TextBox ID="startDate" class="l_input" runat="server" onclick="WdatePicker();" ValidationGroup="txt"></asp:TextBox>
                           <asp:RequiredFieldValidator runat="server" ID="dateValid" ControlToValidate="startDate" ErrorMessage="跟进时间不能为空"
                               Display="Dynamic" ValidationGroup="txt"></asp:RequiredFieldValidator>    
                    
                    下次跟进时间：<asp:TextBox ID="endDate" class="l_input" runat="server" onclick="WdatePicker();" /><span style="color:green;"></span>
                    <asp:RequiredFieldValidator runat="server" ID="dateValid2" ControlToValidate="endDate" ErrorMessage="下次跟进时间不能为空"
                               Display="Dynamic" ValidationGroup="txt"></asp:RequiredFieldValidator>    
                </td>
            </tr>
            <tr class="tdbg">
                <td class="style1" align="right"><strong>内容：</strong></td>
                <td style="width: 85%">
                    <textarea runat="server" cols="80" id="TxtTContent" width="580px" height="300px" name="TxtTContent" rows="50" ValidationGroup="txt"></textarea>
                    <asp:RequiredFieldValidator ID="TxtTContentValidator" runat="server" ControlToValidate="TxtTContent"
                        ErrorMessage="内容不能为空!" SetFocusOnError="True" Display="Dynamic" ValidationGroup="txt"/>
                    <script id="headscript" type="text/javascript">
                        function createTxt() {
                            UE.getEditor('TxtTContent', {
                                toolbars: [['FullScreen', 'Source', 'Undo', 'Redo', 'Bold', 'Italic', 'NumberedList', 'BulletedList', 'Smiley', 'ShowBlocks',
                             'Maximize', 'bold', 'italic', 'underline', 'fontborder', 'strikethrough', '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'snapscreen', '|', 'help']]
                            });
                        }
                        //$.ajax({
                        //    type: "post",
                        //    url: "CustomerManage.aspx",
                        //    data: { action: "getInfo" },
                        //    success: function (data) { $("#FPDate").text(data.date); },//$("#FPMan").text(data.fbman); 
                        //    error: function (data) { alert("获取数据失败,将在提交时在服务端生成!") },
                        //    dataType:"json"
                        //});
                    </script>
                    <span id="Span3"></span>
            </tr>
            <tr class="tdbgbottom">
                <td colspan="2" style="text-align: center;">
                    <asp:Button ID="EBtnSubmit2" Text="提交跟进信息" CssClass="btn btn-primary" runat="server" ValidationGroup="txt" OnClick="EBtnSubmit2_Click" />
                </td>
            </tr>
        </table>
        </div>
    <%--<div id="FPGrid" style="font-size:12px;">--%>
            <ZL:ExGridView runat="server" ID="EGV" IsHoldState="false" DataSourceID="CRMData" AutoGenerateColumns="false" 
                AllowPaging="True"  OnRowCommand="EGV_RowCommand" CssClass="table table-striped table-bordered table-hover" EnableTheming="False" EmptyDataText="没有任何数据！">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <input type="checkbox" name="idchk" value="<%#Eval("ID") %>" style="float:left;"/>
                            <%#Eval("ID") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="内容">
                        <ItemTemplate>
                            <%#Server.HtmlDecode(Eval("userinfo")as string) %>
                        </ItemTemplate>
                         <EditItemTemplate>
                             <asp:TextBox runat="server" ID="editUserinfo" Text='<%#Server.HtmlDecode(Eval("userinfo")as string) %>'/>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="跟进时间">
                        <ItemTemplate>
                            <%# Convert.ToDateTime(Eval("BeginTime")).ToString("yyyy年MM月dd日") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="下次跟进时间">
                        <ItemTemplate>
                            <%# Convert.ToDateTime(Eval("CompletionTime")).ToString("yyyy年MM月dd日") %>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="editEndDate" class="l_input" runat="server" onclick="WdatePicker();" 
                                 Text='<%# Convert.ToDateTime(Eval("CompletionTime")).ToString("yyyy/MM/dd")%>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="跟进人">
                        <ItemTemplate>
                            <%#Eval("fpman") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                             <asp:LinkButton ID="editLinkBtn" runat="server" CommandName="Edit2" CommandArgument='<%# Container.DisplayIndex %>' CssClass="option_style"><i class="fa fa-pencil" title="修改"></i>修改</asp:LinkButton> 
                             <asp:LinkButton ID="delLinkBtn" runat="server" CommandName="Delete2" CommandArgument='<%# Eval("ID") %>' CssClass="option_style"
                                 OnClientClick="return confirm('你确定要删除吗!!');"><i class="fa fa-trash-o" title="删除"></i>删除</asp:LinkButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:LinkButton ID="Save" runat="server" CommandName="Save" CommandArgument='<%# Container.DisplayIndex+":"+Eval("ID") %>'>更新</asp:LinkButton> 
                            <asp:LinkButton ID="Cancel" runat="server" CommandName="Cancel">取消</asp:LinkButton>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
            <PagerStyle CssClass="tdbg" HorizontalAlign="Center" />
            <RowStyle Height="24px" HorizontalAlign="Center" />
            </ZL:ExGridView>
            <asp:ObjectDataSource runat="server" ID="CRMData" OldValuesParameterFormatString="original_{0}" SelectMethod="GetFPList" TypeName="GetDSData">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="0" Name="id" QueryStringField="id" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
     <script type="text/javascript" src="/Plugins/Ueditor/ueditor.config.js"></script>
    <script type="text/javascript" src="/Plugins/Ueditor/ueditor.all.min.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/ZL_Content.js"></script>
    <script type="text/javascript" src="/js/citylist.js"></script>
    <script type="text/javascript" src="/JS/DatePicker/WdatePicker.js"></script>
    <script type="text/javascript">
        $(function () {
            $("form").submit(function () {
                $(".ueditor").each(function () {
                    $(this).val(UE.getEditor($(this).attr('id')).getContent());
                });
            });
        });
        function isPerson(ele) {
            document.getElementById("routine").style.display = "";
            if (ele == "Person_Add") {
                //document.getElementById("TabTitle3").style.display = "";
                document.getElementById("t1").style.display = "none";
                document.getElementById("s1").style.display = "";
                document.getElementById("s2").style.display = "";
                document.getElementById("s3").style.display = "";
                document.getElementById("s4").style.display = "";
                document.getElementById("s5").style.display = "";
                document.getElementById("s6").style.display = "";
                document.getElementById("s7").style.display = "";
                document.getElementById("RadlClientType_1").checked = "checked";
            }
            else if (ele == "Enterprise_Add") {
               // document.getElementById("TabTitle3").style.display = "none";
                document.getElementById("t1").style.display = "";
                document.getElementById("s1").style.display = "none";
                document.getElementById("s2").style.display = "none";
                document.getElementById("s3").style.display = "none";
                document.getElementById("s4").style.display = "none";
                document.getElementById("s5").style.display = "none";
                document.getElementById("s6").style.display = "none";
                document.getElementById("s7").style.display = "none";
                document.getElementById("RadlClientType_0").checked = "checked";
            }
        }
        function displl(di) {
            if (di != "info") {
                document.getElementById("info").style.display = "none";
                document.getElementById(di).style.display = "block";
                var field = '<%=Request.QueryString["FieldName"].ToString() %>';
                if ((field == "Person_Add") || (field == "Enterprise_Add")) {
                    isPerson(field);
                }
                else {
                    document.getElementById("routine").style.display = "none";
                }
            } else {
                document.getElementById("routine").style.display = "none";
                document.getElementById(di).style.display = "";
            }
        }

        function ShowTabs(ID) {
            for (i = 0; i < 8; i++) {
                if (i == ID) {
                    document.getElementById("Tabs" + i).style.display = "";
                }
                else {
                    document.getElementById("Tabs" + i).style.display = "none";
                }
            }
        }
        function Fsex(a) {
            document.getElementById("sex").value = a;
        }
        function FMarriage(b) {
            document.getElementById("Marriage").value = b;
        }
        $(function () {
            $("#EGV tr:last").children().first().attr("colspan", "8").before("<td><input type=checkbox id='chkAll'/></td>");
            $("#chkAll").click(function () {//EGV 全选
                selectAll(this, "idchk");
            });
        });
        function selectAll(obj, name) {
            var allInput = document.getElementsByName(name);
            var loopTime = allInput.length;
            for (i = 0; i < loopTime; i++) {
                if (allInput[i].type == "checkbox") {
                    allInput[i].checked = obj.checked;
                }
            }
        }
    </script>
    <script type="text/javascript" src="/js/Drag.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_Dialog.js"></script>
    <script type="text/javascript" src="/JS/Controls/ZL_PCC.js"></script>
    <script type="text/javascript">
        var diag = new ZL_Dialog();
        function Openwin() {
            diag.title = "选择客户";
            diag.url = "SelectProjectName.aspx";
            diag.ShowModal();
        }
        function CloseCurDiag() {
            diag.CloseModal();
        }
        $().ready(function () {
            var pcc = new ZL_PCC("province", "city", "tbCounty");
            if ($("#Adrees_Hid").val() != "") {
                var attr = $("#Adrees_Hid").val().split(',');
                pcc.SetDef(attr[0], attr[1]);
            }
            pcc.ProvinceInit();
        });
    </script>
</asp:Content>