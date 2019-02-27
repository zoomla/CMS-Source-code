<%@ Page Language="C#" MasterPageFile="~/User/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="User_Profile_Default" ClientIDMode="Static" ValidateRequest="false" EnableViewStateMac="false" %>
<asp:Content ContentPlaceHolderID="head" runat="Server">
    <title>我的返利</title>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="Content">
<div class="u_sign" id="u_store" data-nav="shop"></div>
<div class="container margin_t5">
	<ol class="breadcrumb">
        <li><a title="会员中心" href="/User/Default.aspx">会员中心</a></li>
        <li class="active">我的返利</li> 
    </ol>
</div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div align="center" class="us_seta container">
        <table class="table table-bordered table table-striped">
            <tr>
                <td colspan="3" class="text-center">我的返利</td>
            </tr>
            <tr>
                <td align="left" style="min-width:100px;">待审核的返利:</td>
                <td style="width: 100px"><b><asp:Label ID="lblAuditPro" runat="server" ForeColor="Red"></asp:Label></b></td>
                <td align="left"><a href="Profile.aspx">查看待返利详情>></a></td>
            </tr>
            <tr>
                <td align="left">返利帐户余额:</td>
                <td style="width: 100px"><b><asp:Label ID="lblFmoney" runat="server" ForeColor="Red"></asp:Label></b></td>
                <td align="left"><a href="Profile.aspx?state=1">查看返利详情>></a></td>
            </tr>
            <tr>
                <td align="left">申请中的返利:</td>
                <td><b><asp:Label ID="lblSMoney" runat="server"></asp:Label></b></td>
                <td align="left">为了账户安全,第一次申请兑现300元以上返利者,必须使用注册邮箱发送返利网用户名和收款人真实姓名 到：web@hx008.cn，以便确认!</td>
            </tr>
            <tr>
                <td align="left">已兑现的返利:</td>
                <td><b><asp:Label ID="lblOPro" runat="server"></asp:Label></b></td>
                <td align="left"><a href="ToCashRebates.aspx">详细记录>></a></td>
            </tr>
            <tr>
                <td align="left">红包函数量:</td>
                <td><b><asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label></b></td>
                <td align="left"><a href="../PromotUnion/Redindulgence.aspx" target="_blank">什么是红包函? </a></td>
            </tr>
            <tr>
                <td align="left">红包申请：</td>
                <td><b><asp:Label ID="Label2" runat="server"></asp:Label></b></td>
                <td align="left"><a href="RedEnvelopeRecard.aspx" target="_blank">查看红包申请记录>> </a></td>
            </tr>
        </table>
    </div>
    <div class="container">
        <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="Small" OnClientClick="showHideDIV('cashrequest');return false;"> <img src="../../App_Themes/UserThem/images/icon.gif" width="10" height="10" style="cursor: hand"/> <b>现金</b>兑现申请 </asp:LinkButton>
        <div id="cashrequest" style="display: none;">
            <table cellspacing="0" cellpadding="5" width="100%" align="center" border="0" bgcolor="#FFFAEA" style="border: 1px solid #EBD2AA;">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellspacing="1" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="6" width="100%" border="0">
                                                    <tr>
                                                        <td width="33%" height="30" align="left" style="border-bottom: dotted #ccc 0px; font-weight: bold;">
                                                            <asp:DropDownList ID="ddlAmount" runat="server"></asp:DropDownList>
                                                            &nbsp;
                                <asp:HyperLink ID="hfAccount" runat="server" Text="填写收款信息"></asp:HyperLink></td>
                                                        <td width="67%" style="border-bottom: dotted #ccc 0px; text-align: center;"><span style="color: #FF0000;">好消息！每次兑现返利，返利网会额外送您现金大红包！</span><a href="#">查看详情&gt;&gt;</a></td>
                                                    </tr>
                                                    <tr>
                                                        <td height="30" colspan="2" align="left">
                                                            <input type="button" id="Button1" runat="server" disabled="disabled" onclick="javascript: window.open('pass.aspx', 'new', 'width=350px,height=200px,top=300, left=300')" value="兑现" style="height: 25px; width: 120px; text-align: center;" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2" align="left"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2"><b>注意事项：</b><br>
                                                            <div id="divHonor" runat="server"></div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        
        <asp:LinkButton ID="LinkButton2" runat="server" Font-Size="Small" OnClientClick="showHideDIV('smallcash');return false;"> <img src="../../App_Themes/UserThem/images/icon.gif" width="10" height="10" style="cursor: hand"/><b> 小额</b>兑现申请 </asp:LinkButton>
        <div id="smallcash" style="display: none;">
            <table cellspacing="0" cellpadding="5" width="100%" align="center" border="0" bgcolor="#FFFAEA" style="border: 1px solid #EBD2AA;">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellspacing="1" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="6" width="100%" border="0">
                                                    <tr>
                                                        <td colspan="3" align="left">考虑到一些会员初次使用返利网，对如何获得返利还存有不解，为了帮助新手熟悉返利流程，凡返利余额<b>超过10元</b>的新会员可以先行兑换一张购物网站礼品卡，如果不需要礼品卡也可以用自己的支付宝账户申请兑现5元！(兑换后系统会自动从您的返利余额里扣除相应金额的返利)</td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="border-bottom: dotted #000 1px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" height="80" align="center" style="border-top: 1px dotted #ccc; border-bottom: 1px dotted #ccc;">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td width="50%" align="center">请选择兑换类型:
                                          <select id="ddtype" runat="server" onchange="Changes()">
                                              <option value="0">礼品卡</option>
                                              <option value="1">5元现金</option>
                                          </select></td>
                                                                            <td></td>
                                                                        </tr>
                                                                    </table>
                                                                    <div style="margin-top: 10px;">
                                                                        <div id="Gife">
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td align="right">返利金额值:</td>
                                                                                    <td align="left">
                                                                                        <asp:DropDownList ID="ddGife" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddGife_SelectedIndexChanged"></asp:DropDownList>
                                                                                        (即礼品卡所需返利金额)</td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="right">礼品卡:</td>
                                                                                    <td align="left">
                                                                                        <asp:DropDownList ID="ddGifeCard" runat="server"></asp:DropDownList>
                                                                                        (价格小于等于返利金额的礼品卡)</td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <asp:Button ID="btnExCh" runat="server" Text="兑换" OnClick="btnExCh_Click" />
                                                                    </div>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            <asp:Label ID="lblTips" runat="server" ForeColor="#FF0000" Text="您已经兑现过或您的帐户不满足小额兑现的条件！<br>如有问题或异议，请咨询返利网客服！"> </asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" style="border-bottom: dotted #000 1px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3"><b>注意：</b><br>
                                                            <div id="divSmell" runat="server"></div>
                                                        </td>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        
        
        <asp:LinkButton ID="LinkButton3" runat="server" Font-Size="Small" OnClientClick="showHideDIV('cashgift');return false;"> <img src="../../App_Themes/UserThem/images/icon.gif" width="10" height="10" style="cursor: hand"/> 此处申请兑现额外奖励的红包</asp:LinkButton>
        <div id="cashgift" style="display: none;">
            <table cellspacing="0" cellpadding="5" width="100%" align="center" border="0" bgcolor="#FFFAEA" style="border: 1px solid #EBD2AA;">
                <tr>
                    <td>
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>
                                    <table cellspacing="1" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td>
                                                <table cellspacing="0" cellpadding="6" width="100%" border="0">
                                                    <tr>
                                                        <td width="100%" height="60" align="center" style="border-bottom: dotted #fff 1px; font-weight: bold;">
                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                                <ContentTemplate>
                                                                    兑现申请时间:
                                    <asp:TextBox ID="txtData" onclick="calendar()" runat="server"></asp:TextBox>
                                                                    <asp:Button ID="btnOrder" runat="server" Text="申请红包" OnClick="btnOrder_Click" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                            
                                                            <asp:Label ID="lbltip" runat="server" ForeColor="#FF0000" Text="对不起，您目前不符合申领红包的条件，请查看规则。"></asp:Label></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>什么是红包？</b>
                                                            为感谢广大会员的支持，自2007年9月1日起，每次在返利网兑现返利，除正常返利外，返利网还额外送您现金大红包！</td>
                                                    </tr>
                                                    <tr>
                                                        <td height="5px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>奖励规则：</b>
                                                            <div id="divGu" runat="server"></div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="5px"></td>
                                                    </tr>
                                                    <tr>
                                                        <td><b>注意事项：</b>
                                                            <div id="divImp" runat="server"></div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent">
    <script src="/Common/Common.js" type="text/javascript"></script>
    <script src="/JS/calendar.js" type="text/javascript"></script>
    <script>
        function CheckAll(spanChk)//CheckBox全选
        {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ? spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++)
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
        }

        function Changes() {
            var type = document.getElementById("ddtype").value;
            if (type == "0") {
                document.getElementById("Gife").style.display = "";
            } else {
                document.getElementById("Gife").style.display = "none";
            }
        }

        function showHideDIV(id) {
            var doc = document.getElementById(id);
            doc.style.display = (doc.style.display == "none" ? "block" : "none");
        }
    </script>
</asp:Content>
