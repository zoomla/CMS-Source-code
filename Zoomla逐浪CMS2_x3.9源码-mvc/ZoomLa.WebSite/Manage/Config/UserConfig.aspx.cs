using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Common;
using ZoomLa.Components;
using System.Collections.Generic;
using System.IO;
using ZoomLa.API;

//本页已经完成

namespace ZoomLaCMS.Manage.Config
{
    public partial class UserConfig : CustomerPageAction
    {
        protected B_Admin badmin = new B_Admin();
        private Dictionary<string, ListItem> m_RegFields;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!B_ARoleAuth.Check(ZLEnum.Auth.other, "SiteConfig"))
            {
                function.WriteErrMsg("没有权限进行此项操作");
            }
            m_RegFields = new Dictionary<string, ListItem>();
            m_RegFields.Add("Permissions", new ListItem("真实姓名", "Permissions"));
            m_RegFields.Add("UserSex", new ListItem("性别", "UserSex"));
            m_RegFields.Add("Birthday", new ListItem("出生日期", "Birthday"));
            m_RegFields.Add("Address", new ListItem("联系地址", "Address"));
            m_RegFields.Add("ZipCode", new ListItem("邮政编码", "ZipCode"));
            m_RegFields.Add("Homepage", new ListItem("个人主页", "Homepage"));
            m_RegFields.Add("QQ", new ListItem("QQ号码", "QQ"));
            // m_RegFields.Add("ICQ", new ListItem("ICQ号码", "ICQ"));
            m_RegFields.Add("MSN", new ListItem("MSN帐号", "MSN"));
            m_RegFields.Add("UC", new ListItem("UC号码", "UC"));
            //m_RegFields.Add("Yahoo", new ListItem("雅虎通帐号", "Yahoo"));
            m_RegFields.Add("OfficePhone", new ListItem("办公电话", "OfficePhone"));
            m_RegFields.Add("HomePhone", new ListItem("家庭电话", "HomePhone"));
            m_RegFields.Add("Mobile", new ListItem("手机号码", "Mobile"));
            m_RegFields.Add("Fax", new ListItem("传真号码", "Fax"));
            //m_RegFields.Add("PHS", new ListItem("小灵通", "PHS"));
            m_RegFields.Add("IDCard", new ListItem("身份证号码", "IDCard"));
            m_RegFields.Add("salt", new ListItem("用户头像", "UserFace"));
            m_RegFields.Add("FaceWidth", new ListItem("头像宽度", "FaceWidth"));
            m_RegFields.Add("FaceHeight", new ListItem("头像高度", "FaceHeight"));
            m_RegFields.Add("Sign", new ListItem("签名档", "Sign"));
            m_RegFields.Add("Privacy", new ListItem("隐私设定", "Privacy"));
            m_RegFields.Add("Province", new ListItem("省市县", "Province"));
            m_RegFields.Add("ParentUserID", new ListItem("推荐人", "ParentUserID"));
            m_RegFields.Add("ParentUser", new ListItem("推荐人ID", "ParentUser"));
            m_RegFields.Add("InvitCode", new ListItem("邀请码", "InvitCode"));

            //ZL_UserBaseField
            B_UserBaseField ufll = new B_UserBaseField();
            DataTable uftable = ufll.Select_All();
            if (uftable != null)
            {
                if (uftable.Rows.Count > 0)
                {
                    for (int c = 0; c < uftable.Rows.Count; c++)
                    {
                        string FieldName = uftable.Rows[c]["FieldName"].ToString();
                        string FieldAlias = uftable.Rows[c]["FieldAlias"].ToString();
                        m_RegFields.Add(FieldName, new ListItem(FieldAlias, FieldName));
                    }
                }
            }

            if (!IsPostBack)
            {
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + CustomerPageAction.customPath2 + "user/UserManage.aspx'>用户管理</a></li><li><a href='" + CustomerPageAction.customPath2 + "user/UserManage.aspx'>会员管理</a></li><li>会员参数配置</li>" + Call.GetHelp(40));
                if (SiteConfig.UserConfig.EnableUserReg)//true 则为1
                {
                    RadioButtonList1.Checked = true;
                }
                else
                {
                    RadioButtonList1.Checked = false;
                }
                if (SiteConfig.UserConfig.UserValidateType)//会员注册默认状态
                {
                    RadioButtonList9.SelectedIndex = 0;
                }
                else
                {
                    RadioButtonList9.SelectedIndex = 1;
                }
                //是否开户支付宝登录
                //if (SiteConfig.UserConfig.EnableAlipayCheckReg)
                //{
                //    rdoAlipayCheck.SelectedIndex = 0;
                //}
                //else 
                //{
                //    rdoAlipayCheck.SelectedIndex = 1;
                //}



                //是否开启注册后Email验证
                if (SiteConfig.UserConfig.EmailCheckReg)
                {
                    rdoEmailCheck.Checked = true;
                }
                else
                {
                    rdoEmailCheck.Checked = false;
                }
                //是否开启Email注册登录
                if (SiteConfig.UserConfig.EmailRegis)
                {
                    RadioButtonList2.Checked = true;
                }
                else
                {
                    RadioButtonList2.Checked = false;
                }
                //是否开启注册成功邮件提醒
                if (SiteConfig.UserConfig.EmailTell)
                {
                    radioEmail.Checked = true;
                }
                else
                {
                    radioEmail.Checked = false;
                }
                InviteCode_T.Text = (SiteConfig.UserConfig.InviteCodeCount > 0 ? SiteConfig.UserConfig.InviteCodeCount : 1).ToString();
                //是否开启UserID登录
                if (SiteConfig.UserConfig.UserIDlgn)
                {
                    radioUserID.Checked = true;
                }
                else
                {
                    radioUserID.Checked = false;
                }
                //是否开启手机注册
                radioMobile.Checked = SiteConfig.UserConfig.MobileReg;
                MobileCodeNum_T.Text = SiteConfig.UserConfig.MobileCodeNum.ToString();
                function.Script(this, "SetRadVal('mobilecode_rad'," + SiteConfig.UserConfig.MobileCodeType + ");");

                RadioButtonList3.Checked = SiteConfig.UserConfig.AdminCheckReg;
                RadioButtonList4.Checked = SiteConfig.UserConfig.EnableMultiRegPerEmail;
                RadioButtonList5.Checked = SiteConfig.UserConfig.EnableCheckCodeOfReg;

                TextBox6.Text = SiteConfig.UserConfig.UserNameLimit.ToString();
                TextBox7.Text = SiteConfig.UserConfig.UserNameMax.ToString();
                TextBox8.Text = SiteConfig.UserConfig.UserNameRegDisabled;

                RadioButtonList6.Checked = SiteConfig.UserConfig.EnableCheckCodeOfLogin;
                RadioButtonList7.Checked = SiteConfig.UserConfig.EnableMultiLogin;
                DisCuzNT.Checked = SiteConfig.UserConfig.DisCuzNT;
                string regFieldsMustFill = SiteConfig.UserConfig.RegFieldsMustFill;
                string regFieldsSelectFill = SiteConfig.UserConfig.RegFieldsSelectFill;
                HdnRegFields_MustFill.Value = regFieldsMustFill;
                HdnRegFields_SelectFill.Value = regFieldsSelectFill;

                List<string> list = new List<string>();
                List<string> list2 = new List<string>();
                if (!string.IsNullOrEmpty(regFieldsMustFill))
                {
                    foreach (string str3 in regFieldsMustFill.Split(new char[] { ',' }))
                    {
                        list.Add(str3);
                        if (m_RegFields.ContainsKey(str3))
                        {
                            LitRegFields_MustFill.Items.Add(m_RegFields[str3]);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(regFieldsSelectFill))
                {
                    foreach (string str4 in regFieldsSelectFill.Split(new char[] { ',' }))
                    {
                        list2.Add(str4);
                        if (m_RegFields.ContainsKey(str4))
                        {
                            LitRegFields_SelectFill.Items.Add(m_RegFields[str4]);
                        }
                    }
                }
                foreach (string str5 in m_RegFields.Keys)
                {
                    if (!list.Contains(str5) && !list2.Contains(str5))
                    {
                        LitRegFields.Items.Add(m_RegFields[str5]);
                    }
                }

                //TextBox12.Text = SiteConfig.UserConfig.EmailOfRegCheck;
                //txtEmailTell.Text = SiteConfig.UserConfig.EmailTellContent;
                txtMobileRegInfo.Text = SiteConfig.UserConfig.MobileRegInfo;
                tb_CommentRule.Text = SiteConfig.UserConfig.CommentRule.ToString();
                tb_InformationRule.Text = SiteConfig.UserConfig.InfoRule.ToString();
                tb_RecommandRule.Text = SiteConfig.UserConfig.RecommandRule.ToString();
                txtzi.Text = SiteConfig.UserConfig.PresentPointAll.ToString();
                TxtPresentExp.Text = SiteConfig.UserConfig.PresentExp.ToString();
                TxtPresentMoney.Text = SiteConfig.UserConfig.PresentMoney.ToString();
                TxtPresentPoint.Text = SiteConfig.UserConfig.PresentPoint.ToString();
                TxtPresentValidNum.Text = SiteConfig.UserConfig.PresentValidNum.ToString();
                DropPresentValidUnit.SelectedValue = SiteConfig.UserConfig.PresentValidUnit.ToString();
                TxtPresentExpPerLogin.Text = SiteConfig.UserConfig.PresentExpPerLogin.ToString();
                SignPurse_T.Text = SiteConfig.UserConfig.SigninPurse.ToString();
                TxtMoneyExchangePoint.Text = SiteConfig.UserConfig.MoneyExchangePointByMoney.ToString();
                TxtMoneyExchangeValidDay.Text = SiteConfig.UserConfig.MoneyExchangeValidDayByMoney.ToString();
                TxtUserExpExchangePoint.Text = SiteConfig.UserConfig.UserExpExchangePointByExp.ToString();
                TxtUserExpExchangeValidDay.Text = SiteConfig.UserConfig.UserExpExchangeValidDayByExp.ToString();
                TxtCMoneyExchangePoint.Text = SiteConfig.UserConfig.MoneyExchangePointByPoint.ToString();
                TxtCMoneyExchangeValidDay.Text = SiteConfig.UserConfig.MoneyExchangeValidDayByValidDay.ToString();
                TxtCUserExpExchangePoint.Text = SiteConfig.UserConfig.UserExpExchangePointByPoint.ToString();
                TxtCUserExpExchangeValidDay.Text = SiteConfig.UserConfig.UserExpExchangeValidDayByValidDay.ToString();
                txtCMoneyExchangeDummyPurse.Text = SiteConfig.UserConfig.MoneyExchangeDummyPurseByDummyPurse.ToString();
                txtMoneyExchangeDummyPurse.Text = SiteConfig.UserConfig.MoneyExchangeDummyPurseByMoney.ToString();
                TxtPointName.Text = SiteConfig.UserConfig.PointName;
                TxtPointUnit.Text = SiteConfig.UserConfig.PointUnit;
                TxtCUserExpExchangePoints.Text = SiteConfig.UserConfig.PointExp.ToString();
                TxtCUserExpExchangeMoney.Text = SiteConfig.UserConfig.PointMoney.ToString();

                TxtCUserExpExchangeExp.Text = SiteConfig.UserConfig.ChangeSilverCoinByExp.ToString();
                TxtCUserExpExchangeSilverCoin.Text = SiteConfig.UserConfig.PointSilverCoin.ToString();

                Agreement.SelectedValue = SiteConfig.UserConfig.Agreement;
                selPunch.Value = SiteConfig.UserConfig.PunchType.ToString();
                txtPunch.Text = SiteConfig.UserConfig.PunchVal.ToString();
                //txtEmailRegInfo.Text = SiteConfig.UserConfig.EmailRegInfo;
                //txtGetPassword.Text = SiteConfig.UserConfig.UserGetPasswordEmail;
                Txtintegral.Text = SiteConfig.UserConfig.Integral.ToString();
                TxtIntegralPercentage.Text = SiteConfig.UserConfig.IntegralPercentage.ToString();
                string userregrule = SiteConfig.UserConfig.RegRule;

                if (userregrule != null && userregrule != "")
                {
                    if (userregrule.IndexOf(',') > -1)
                    {
                        string[] rulearr = userregrule.Split(',');
                        for (int ii = 0; ii < rulearr.Length; ii++)
                        {
                            if (rulearr[ii].ToString() == "1")
                            {
                                RegRule.Items[0].Selected = true;
                            }

                            if (rulearr[ii].ToString() == "2")
                            {
                                RegRule.Items[1].Selected = true;
                            }

                            if (rulearr[ii].ToString() == "3")
                            {
                                RegRule.Items[2].Selected = true;
                            }
                        }
                    }
                    else
                    {
                        if (userregrule.ToString() == "1")
                        {
                            RegRule.Items[0].Selected = true;
                        }

                        if (userregrule.ToString() == "2")
                        {
                            RegRule.Items[1].Selected = true;
                        }

                        if (userregrule.ToString() == "3")
                        {
                            RegRule.Items[2].Selected = true;
                        }
                    }
                }
                if (SiteConfig.UserConfig.RegRule != null)
                {
                    RegRule.SelectedValue = SiteConfig.UserConfig.RegRule.ToString();
                }
                #region 可使用站内短信用户组
                B_Group b_Group = new B_Group();
                MessageGroup.DataSource = b_Group.GetGroupList();
                MessageGroup.DataBind();
                string MessageGroupStr = SiteConfig.UserConfig.MessageGroup;
                if (!string.IsNullOrEmpty(MessageGroupStr))
                {
                    if (MessageGroupStr.IndexOf(",") > -1)
                    {
                        foreach (string Mstr in MessageGroupStr.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries))
                        {
                            if (MessageGroup.Items.FindByValue(Mstr) != null)
                            {
                                MessageGroup.Items.FindByValue(Mstr).Selected = true;
                            }
                        }
                    }
                }
                //用户统计字段
                foreach (ListItem item in m_RegFields.Values)
                {
                    UserFields_list.Items.Add(item);
                }
                #endregion
                RadioButtonList10.SelectedValue = SiteConfig.UserConfig.PromotionType.ToString();
                RadioButtonList10_SelectedIndexChanged(null, null);
                CountFields_Hid.Value = SiteConfig.UserConfig.CountUserField;
                if (SiteConfig.UserConfig.PromotionType != 0)
                {
                    txtPromotion.Text = SiteConfig.UserConfig.Promotion.ToString();
                }
                MyBind();
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            SiteConfig.UserConfig.EnableUserReg = RadioButtonList1.Checked;
            SiteConfig.UserConfig.UserValidateType = RadioButtonList9.Items[0].Selected;//会员注册默认状态
            SiteConfig.UserConfig.EmailCheckReg = rdoEmailCheck.Checked;
            //obj2.UserConfig.EnableAlipayCheckReg = rdoAlipayCheck.Items[0].Selected;//支付宝
            SiteConfig.UserConfig.EmailRegis = RadioButtonList2.Checked;
            SiteConfig.UserConfig.UserIDlgn = radioUserID.Checked;//会员ID登录
            SiteConfig.UserConfig.MobileReg = radioMobile.Checked;//会员手机注册
            SiteConfig.UserConfig.MobileCodeNum = DataConverter.CLng(MobileCodeNum_T.Text);
            SiteConfig.UserConfig.MobileCodeType = DataConverter.CLng(Request.Form["mobilecode_rad"]);
            SiteConfig.UserConfig.EmailTell = radioEmail.Checked;
            SiteConfig.UserConfig.AdminCheckReg = RadioButtonList3.Checked;
            SiteConfig.UserConfig.EnableMultiRegPerEmail = RadioButtonList4.Checked;
            SiteConfig.UserConfig.EnableCheckCodeOfReg = RadioButtonList5.Checked;
            SiteConfig.UserConfig.UserNameLimit = int.Parse(TextBox6.Text);
            SiteConfig.UserConfig.UserNameMax = int.Parse(TextBox7.Text);
            SiteConfig.UserConfig.UserNameRegDisabled = TextBox8.Text;

            SiteConfig.UserConfig.EnableCheckCodeOfLogin = RadioButtonList6.Checked;
            SiteConfig.UserConfig.EnableMultiLogin = RadioButtonList7.Checked;
            SiteConfig.UserConfig.InviteCodeCount = DataConverter.CLng(InviteCode_T.Text);
            SiteConfig.UserConfig.CommentRule = int.Parse(tb_CommentRule.Text.Trim());
            SiteConfig.UserConfig.InfoRule = int.Parse(tb_InformationRule.Text.Trim());
            SiteConfig.UserConfig.RecommandRule = int.Parse(tb_RecommandRule.Text.Trim());
            //obj2.UserConfig.LoginRule = int.Parse(tb_LoginRule.Text.Trim());            
            //SiteConfig.UserConfig.UserGetPasswordEmail = txtGetPassword.Text;
            SiteConfig.UserConfig.RegFieldsMustFill = HdnRegFields_MustFill.Value;
            SiteConfig.UserConfig.RegFieldsSelectFill = HdnRegFields_SelectFill.Value;
            //SiteConfig.UserConfig.EmailOfRegCheck = TextBox12.Text;
            //SiteConfig.UserConfig.EmailTellContent = txtEmailTell.Text;
            SiteConfig.UserConfig.MobileRegInfo = txtMobileRegInfo.Text;
            SiteConfig.UserConfig.PointExp = DataConverter.CDouble(TxtCUserExpExchangePoints.Text);
            SiteConfig.UserConfig.PointMoney = DataConverter.CDouble(TxtCUserExpExchangeMoney.Text);

            SiteConfig.UserConfig.ChangeSilverCoinByExp = DataConverter.CDouble(TxtCUserExpExchangeExp.Text);
            SiteConfig.UserConfig.PointSilverCoin = DataConverter.CDouble(TxtCUserExpExchangeSilverCoin.Text);

            SiteConfig.UserConfig.PresentExp = DataConverter.CDouble(TxtPresentExp.Text);
            SiteConfig.UserConfig.PresentMoney = DataConverter.CDouble(TxtPresentMoney.Text);
            SiteConfig.UserConfig.PresentPoint = DataConverter.CLng(TxtPresentPoint.Text);
            SiteConfig.UserConfig.PresentPointAll = DataConverter.CLng(txtzi.Text);
            SiteConfig.UserConfig.PresentValidNum = DataConverter.CLng(TxtPresentValidNum.Text);
            SiteConfig.UserConfig.PresentValidUnit = DataConverter.CLng(DropPresentValidUnit.SelectedValue);
            SiteConfig.UserConfig.PresentExpPerLogin = DataConverter.CDouble(TxtPresentExpPerLogin.Text);
            SiteConfig.UserConfig.SigninPurse = DataConverter.CDouble(SignPurse_T.Text);
            SiteConfig.UserConfig.Integral = Convert.ToInt16(Txtintegral.Text);
            SiteConfig.UserConfig.IntegralPercentage = DataConverter.CDouble(TxtIntegralPercentage.Text);

            SiteConfig.UserConfig.MoneyExchangePointByMoney = DataConverter.CDouble(TxtMoneyExchangePoint.Text);
            SiteConfig.UserConfig.MoneyExchangeValidDayByMoney = DataConverter.CDouble(TxtMoneyExchangeValidDay.Text);
            SiteConfig.UserConfig.UserExpExchangePointByExp = DataConverter.CDouble(TxtUserExpExchangePoint.Text);
            SiteConfig.UserConfig.UserExpExchangeValidDayByExp = DataConverter.CDouble(TxtUserExpExchangeValidDay.Text);
            SiteConfig.UserConfig.MoneyExchangePointByPoint = DataConverter.CDouble(TxtCMoneyExchangePoint.Text);
            SiteConfig.UserConfig.MoneyExchangeValidDayByValidDay = DataConverter.CDouble(TxtCMoneyExchangeValidDay.Text);
            SiteConfig.UserConfig.UserExpExchangePointByPoint = DataConverter.CDouble(TxtCUserExpExchangePoint.Text);
            SiteConfig.UserConfig.UserExpExchangeValidDayByValidDay = DataConverter.CDouble(TxtCUserExpExchangeValidDay.Text);
            SiteConfig.UserConfig.MoneyExchangeDummyPurseByDummyPurse = DataConverter.CDouble(txtCMoneyExchangeDummyPurse.Text);
            SiteConfig.UserConfig.MoneyExchangeDummyPurseByMoney = DataConverter.CDouble(txtMoneyExchangeDummyPurse.Text);
            SiteConfig.UserConfig.PunchType = DataConverter.CLng(selPunch.Value);
            SiteConfig.UserConfig.PunchVal = DataConverter.CLng(txtPunch.Text);
            SiteConfig.UserConfig.PointName = TxtPointName.Text.Trim();
            SiteConfig.UserConfig.PointUnit = TxtPointUnit.Text.Trim();
            SiteConfig.UserConfig.PromotionType = DataConverter.CLng(RadioButtonList10.SelectedValue);
            SiteConfig.UserConfig.Promotion = DataConverter.CLng(txtPromotion.Text);
            //SiteConfig.UserConfig.EmailRegInfo = txtEmailRegInfo.Text.Trim();
            SiteConfig.UserConfig.DisCuzNT = DisCuzNT.Checked;
            string regrulelist = "";
            for (int i = 0; i < RegRule.Items.Count; i++)
            {
                if (RegRule.Items[i].Selected)
                {
                    regrulelist = regrulelist + RegRule.Items[i].Value;
                }

                if (i < RegRule.Items.Count - 1)
                {
                    regrulelist = regrulelist + ",";
                }
            }

            SiteConfig.UserConfig.RegRule = regrulelist;
            SiteConfig.UserConfig.Agreement = Agreement.SelectedValue;
            if (txtLimitTime.Text != "" && RBLDZ.Checked != false && txtBeginIP.Text != "" && txtEndIP.Text != "")
            {
                int LimitTime = DataConverter.CLng(txtLimitTime.Text.ToString());
                int IsLimit = DataConverter.CLng(RBLDZ.Checked.ToString());
                int IsIPpart = DataConverter.CLng(RadioButtonList1.Checked.ToString());
                string BeginIP = txtBeginIP.Text.ToString();
                string EndIP = txtEndIP.Text.ToString();
                B_UserRegisterIP.UpdateRegisterIP(LimitTime, IsLimit, IsIPpart, BeginIP, EndIP);
            }

            string mGroup = "";
            for (int i = 0; i < MessageGroup.Items.Count; i++)
            {
                if (MessageGroup.Items[i].Selected)
                {
                    mGroup += MessageGroup.Items[i].Value + ",";
                }
            }
            SiteConfig.UserConfig.MessageGroup = mGroup.TrimEnd(',');
            SiteConfig.UserConfig.CountUserField = CountFields_Hid.Value;
            SiteConfig.Update();
            function.WriteSuccessMsg("用户参数配置保存成功！", Request.RawUrl);
        }
        protected void RadioButtonList10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList10.SelectedValue == "0")
            {
                Tr6.Visible = false;
            }
            else
            {
                Tr6.Visible = true;
            }
        }
        public void MyBind()
        {
            M_UserRegisterIP userRegIP = B_UserRegisterIP.GetRegisterIPAll();
            if (userRegIP != null && userRegIP.Id > 0)
            {
                RBLDZ.Checked = userRegIP.IsLimit.ToString() == "true" ? true : false;
                txtLimitTime.Text = userRegIP.LimitTime.ToString();
                RadioButtonList1.Checked = userRegIP.IsIPpart.ToString() == "0" ? true : false;
                if (RadioButtonList1.Checked == true)
                {
                    IPpart.Visible = false;
                }
                if (userRegIP.BeginIP != null && userRegIP.BeginIP != "" && userRegIP.EndIP != null && userRegIP.EndIP != "")
                {
                    txtBeginIP.Text = userRegIP.BeginIP.ToString();
                    txtEndIP.Text = userRegIP.EndIP.ToString();
                }
            }

        }
        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.Checked == true)
                IPpart.Visible = false;
            else
                IPpart.Visible = true;
        }
    }
}