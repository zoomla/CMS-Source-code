using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using ZoomLa.BLL;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.SQLDAL;


namespace ZoomLaCMS.BU
{
    public partial class Register : System.Web.UI.Page
    {
        B_User buser = new B_User();
        B_UserBaseField bmf = new B_UserBaseField();
        B_ModelField fieldBll = new B_ModelField();
        B_Model b_Model = new B_Model();
        B_Group gpBll = new B_Group();
        B_Sensitivity sll = new B_Sensitivity();
        B_InviteRecord binv = new B_InviteRecord();
        B_User_Plat upBll = new B_User_Plat();
        B_Plat_Comp compBll = new B_Plat_Comp();
        B_User_Temp utBll = new B_User_Temp();
        B_MailManage mailBll = new B_MailManage();
        B_Safe_Mobile mobileBll = new B_Safe_Mobile();
        bool isok = false;
        string result = "";
        //页面控件(用户名),传参,Cookies
        private int ParentUserID
        {
            get
            {
                int puid = 0;
                puid = DataConvert.CLng(Request.QueryString["ParentUserID"]);
                if (puid < 1 && Request.Cookies["UserState2"] != null)
                {
                    puid = DataConvert.CLng(Request.Cookies["UserState2"]["ParentUserID"]);
                }
                return puid;
            }
        }
        //不允许http跳转,不允许带空格,如未指定返回Url,则以后台为准
        public string ReturnUrl
        {
            get
            {
                if (ViewState["ReturnUrl"] == null)
                {
                    string url = HttpUtility.UrlDecode(Request.QueryString["ReturnUrl"] ?? "").Split(' ')[0];
                    url = string.IsNullOrEmpty(url) ? SiteConfig.SiteOption.LoggedUrl : url;
                    ViewState["ReturnUrl"] = Server.HtmlEncode(url);
                }
                return ViewState["ReturnUrl"] as string;
            }
        }
        //用于绑定微信用户
        //public string WXOpenID { get { return Request.QueryString["WXOpenID"]; } }
        protected override void OnPreInit(EventArgs e)
        {
            #region 用户注册字段解析(用户配置中心选择的必填项目)
            string[] mustArr = SiteConfig.UserConfig.RegFieldsMustFill.Split(',');
            for (int i = 0; i < mustArr.Length; i++)
            {
                string field = mustArr[i];
                if (string.IsNullOrEmpty(field)) continue;
                if (field.Equals("Permissions")) { field = "TrueName"; }
                HtmlTableRow row = TableRegister.FindControl("tr" + field) as HtmlTableRow;
                if (row != null)
                {
                    row.Visible = true;
                    TableRegister.Rows.Remove(row);
                    TableRegisterMust.Rows.Add(row);
                }
            }
            //用户配置中心选择的选填项目
            string[] selectArr = SiteConfig.UserConfig.RegFieldsSelectFill.Split(',');
            for (int i = 0; i < selectArr.Length; i++)
            {
                string field = selectArr[i];
                if (string.IsNullOrEmpty(field)) continue;
                if (field.Equals("Permissions")) { field = "TrueName"; }
                HtmlTableRow row2 = TableRegisterSelect.FindControl("TR" + field) as HtmlTableRow;
                if (row2 != null)
                {
                    row2.Visible = true;
                    //Literal las = (Literal)FindControl("mustTR" + field);
                    //if (las != null) las.Text = "";
                    TableRegister.Rows.Remove(row2);
                    TableRegisterSelect.Rows.Add(row2);
                }
            }
            #endregion
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (function.isAjax())
            {
                #region AJAX
                string action = Request.Form["action"];
                string value = (Request.Form["value"] ?? "").Replace(" ", "");
                string result = "";
                switch (action)
                {
                    case "UserIsExist":
                        result = CheckUserData(value);
                        break;
                    case "ParentUser"://推荐人
                        result = CheckParentUser(value);
                        break;
                    case "ParentUserID":
                        result = buser.IsExit(DataConvert.CLng(value)) ? "1" : "0";
                        if (string.IsNullOrEmpty(value))
                            result = "-1";
                        break;
                    case "InvitCode":
                        result = CheckInvitCode(value);
                        break;
                    case "birthdate":
                        result = CheckBirthDay(value);
                        break;
                    case "GetModelFied":
                        result = GetUserGorupModel(value);
                        break;
                }
                Response.Write(result); Response.Flush(); Response.End();
                #endregion
            }
            if (!IsPostBack)
            {
                //DebugUser();
                if (SiteConfig.UserConfig.Agreement.Equals("4")) { Response.Redirect("/User/RegPlat.aspx"); return; }
                //根据IP,分析出地址,并填充
                IPCity cityMod = IPScaner.FindCity(IPScaner.GetUserIP());//"59.52.159.79"
                if (cityMod.IsValid)
                {
                    function.Script(this, "pcc.SetDef('" + cityMod.Province + "','" + cityMod.City + "','" + cityMod.County + "');");
                }
                //解析字段
                MustHtml_L.Text = fieldBll.InputallHtml(0, 0, new ModelConfig()
                {
                    Source = ModelConfig.SType.UserRegister,
                    Fields = SiteConfig.UserConfig.RegFieldsMustFill
                });
                SelectHtml_L.Text = fieldBll.InputallHtml(0, 0, new ModelConfig()
                {
                    Source = ModelConfig.SType.UserRegister,
                    Fields = SiteConfig.UserConfig.RegFieldsSelectFill
                });
                //------------
                TxtPassword.Attributes.Add("value", TxtPassword.Text);
                TxtPwdConfirm.Attributes.Add("value", TxtPwdConfirm.Text);
                //------------传值处理区
                if (ParentUserID > 0)//推荐人
                {
                    TxtParentUserID.Text = buser.SelReturnModel(ParentUserID).UserName;
                    Response.Cookies["UserState2"].Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies["UserState2"]["ParentUserID"] = ParentUserID.ToString();
                }
                regVcodeRegister.Visible = SiteConfig.UserConfig.EnableCheckCodeOfReg;
                //regEmail.Visible = SiteConfig.UserConfig.EmailCheckReg;
                if (!SiteConfig.UserConfig.EnableUserReg)
                {
                    PnlRegStep0.Visible = true;
                    PnlRegStep1.Visible = false;
                    PnlRegStep2.Visible = false;
                    PnlMRegStep1.Visible = false;
                    PnlStep3.Visible = false;
                }
                else
                {

                    PnlRegStep0.Visible = false;
                    PnlRegStep2.Visible = false;
                    PnlMRegStep1.Visible = false;
                    PnlStep3.Visible = false;
                    int userNameLimit = 2;
                    int userNameMax = 20;
                    if (SiteConfig.UserConfig.UserNameLimit > 0)
                    {
                        userNameLimit = SiteConfig.UserConfig.UserNameLimit;
                    }
                    if (SiteConfig.UserConfig.UserNameMax >= userNameLimit)
                    {
                        userNameMax = SiteConfig.UserConfig.UserNameMax;
                    }
                    Span1.InnerHtml = "用户名必须在" + userNameMax + "~" + userNameLimit + "个字符之间";
                    InitProtocol();
                    GropuRadListBind();
                    if (SiteConfig.UserConfig.MobileReg == true)
                    {
                        PnlMRegStep1.Visible = true;
                    }
                    else
                    {
                        PnlMRegStep1.Visible = false;
                        switch (SiteConfig.UserConfig.Agreement)
                        {
                            case "0":
                                BtnRegstep1();
                                checkAgreement.Visible = false;
                                PnlRegStep1.Visible = false;
                                PnlRegStep2.Visible = true;
                                break;
                            case "1":
                                function.Script(this, "endbtn();");
                                PnlRegStep1.Visible = true;
                                BtnRegStep1.Text = "同 意";
                                BtnRegStep1NotApprove.Text = "不同意";
                                break;
                            case "2"://Checkbox,能力中心
                            case "4":
                                BtnRegstep1();
                                //function.Script(this, "dispbtn();");
                                checkAgreement.Visible = true;
                                PnlRegStep1.Visible = false;
                                PnlRegStep2.Visible = true;
                                BtnRegStep1.Text = "注册";
                                BtnRegStep1NotApprove.Text = "返 回";
                                break;
                            case "3":
                                function.Script(this, "regload();");
                                checkAgreement.Visible = false;
                                PnlRegStep1.Visible = true;
                                BtnRegStep1.Text = "同意";
                                BtnRegStep1NotApprove.Text = "不同意";
                                break;
                        }
                    }
                }
            }
        }
        //用户协议,同意按钮
        protected void BtnRegStep1_Click(object sender, EventArgs e)
        {
            BtnRegstep1();
        }
        //用于组绑定
        public void GropuRadListBind()
        {
            DataTable ss = gpBll.GetSelGroup();
            UserGroup.DataSource = ss;
            UserGroup.DataValueField = "GroupID";
            UserGroup.DataTextField = "GroupName";
            UserGroup.DataBind();
            if (ss != null && ss.Rows.Count > 0)
            {
                UserGroup.Items[0].Attributes.Add("onclick", "hidenbtn();");
                if (!string.IsNullOrWhiteSpace(Request["Gid"]))
                    UserGroup.SelectedValue = Request["Gid"];
                else UserGroup.SelectedIndex = 0;
            }
        }
        //用户组模型字段
        public string GetUserGorupModel(string value)
        {
            int gid = DataConverter.CLng(value);
            int UserModelID = DataConverter.CLng(gpBll.GetGroupModel(gid));
            ///UserModelID!=0说明绑定了户模型，用要从模型中读取字段，没有绑定就不需要读取字段
            if (UserModelID != 0)
            {
                return fieldBll.InputallHtml(UserModelID, 0, new ModelConfig()
                {
                    Source = ModelConfig.SType.UserRegister
                });
            }
            return "";
        }
        public void BtnRegstep1()
        {
            GropuRadListBind();
            ReqTxtAddress.Enabled = GetEnableValid("Address");
            ReqTxtBirthday.Enabled = GetEnableValid("Birthday");
            ReqTxtFax.Enabled = GetEnableValid("Fax");
            ReqTxtHomepage.Enabled = GetEnableValid("Homepage");
            ReqTxtIDCard.Enabled = GetEnableValid("IDCard");
            RV3.Enabled = GetEnableValid("IDCard");
            ReqTxtHomePhone.Enabled = GetEnableValid("HomePhone");
            ReqTxtMobile.Enabled = GetEnableValid("Mobile");
            ReqTxtQQ.Enabled = GetEnableValid("QQ");
            ReqTxtSign.Enabled = GetEnableValid("Sign");
            ReqTxtTrueName.Enabled = GetEnableValid("Permissions");//TrueName
            ReqTxtUserFace.Enabled = GetEnableValid("UserFace");
            ReqTxtZipCode.Enabled = GetEnableValid("ZipCode");
            ReqTxtOfficePhone.Enabled = GetEnableValid("OfficePhone");
            ReqTxtPHS.Enabled = GetEnableValid("PHS");
            ReqTxtParentUserID.Enabled = GetEnableValid("ParentUserID");
            ReqTxtParentUser.Enabled = GetEnableValid("ParentUser");
            PnlRegStep1.Visible = false;
            PnlRegStep2.Visible = true;
            PnlStep3.Visible = false;
            PnlMRegStep1.Visible = false;
            checkAgreement.Visible = false;
            //ShowModelfield();
        }
        // 不同意按钮
        protected void BtnRegStep1NotApprove_Click(object sender, EventArgs e)
        {
            Response.Redirect("/User/Index");
        }
        //获取邮件内容模板标签格式
        private DataTable GetRegEmailDt(string username, string checknum, string checkurl)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CheckNum");
            dt.Columns.Add("CheckUrl");
            dt.Columns.Add("UserName");
            dt.Rows.Add(dt.NewRow());
            dt.Rows[0]["CheckNum"] = checknum;
            dt.Rows[0]["CheckUrl"] = checkurl;
            dt.Rows[0]["UserName"] = username;
            return dt;
        }
        //提交注册
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string siteurls = SiteConfig.SiteInfo.SiteUrl.TrimEnd('/');
            if (!SiteConfig.UserConfig.EnableUserReg) { function.WriteErrMsg("服务器已关闭用户注册"); return; }
            if (SiteConfig.UserConfig.UserNameLimit > TxtUserName.Text.Length || TxtUserName.Text.Length > SiteConfig.UserConfig.UserNameMax)
            {
                function.WriteErrMsg("用户名的长度必须小于" + SiteConfig.UserConfig.UserNameMax + "，并大于" + SiteConfig.UserConfig.UserNameLimit + "!");
            }
            if (ZoomlaSecurityCenter.CheckData(TxtUserName.Text))
            {
                function.WriteErrMsg("用户名不能包含特殊字段!");
            }
            else
            {
                if (!SafeSC.CheckUName(TxtUserName.Text)) { function.WriteErrMsg("用户名不能包含特殊字符!"); }
            }
            if (CheckBirthDay(TxtBirthday.Text).Equals("false"))
            {
                function.WriteErrMsg("您的年龄超过了150岁！！");
            }
            if (string.IsNullOrEmpty(TxtAnswer.Text))
            {
                function.WriteErrMsg("问题答案不能为空！");
            }
            #region  注册IP限制
            M_UserRegisterIP UserRegisterIP = B_UserRegisterIP.GetRegisterIPAll();
            if (UserRegisterIP.IsLimit == 0)
            {
                string RegIP = HttpContext.Current.Request.UserHostAddress;
                DateTime EndTime = DateTime.Now;
                double dhours = UserRegisterIP.LimitTime;
                DateTime BeginTime = DateTime.Now.AddHours(-dhours);
                if (UserRegisterIP.IsIPpart != 0 && UserRegisterIP.BeginIP != null && UserRegisterIP.EndIP != null)
                {
                    string beginIP = UserRegisterIP.BeginIP;
                    string endIP = UserRegisterIP.EndIP;
                    long LbeginIP = IpToInt(beginIP);
                    long LendIP = IpToInt(endIP);
                    long LRegIP = IpToInt(RegIP);
                    if (LRegIP > LbeginIP && LRegIP < LendIP)
                    {
                        if (!buser.GetRegisterIP(RegIP, BeginTime, EndTime))
                        {
                            function.WriteErrMsg("同一IP不能重复注册");
                            return;
                        }
                    }
                }
                else
                {
                    if (!buser.GetRegisterIP(RegIP, BeginTime, EndTime))
                    {
                        function.WriteErrMsg("同一IP不能重复注册");
                        return;
                    }
                }
            }
            #endregion
            CheckUserName();
            CheckEmail();
            CheckCode();
            Checkyes();
            CheckPUserName();
            M_UserInfo info = new M_UserInfo();
            info.UserBase = new M_Uinfo();
            info.UserName = TxtUserName.Text;
            info.UserPwd = TxtPassword.Text;
            info.Question = Question_DP.SelectedValue;
            info.Answer = TxtAnswer.Text.Trim();
            info.Email = TxtEmail.Text.Trim();
            info.CheckNum = function.GetRandomString(10);
            info.GroupID = DataConverter.CLng(UserGroup.SelectedValue);
            info.RegisterIP = EnviorHelper.GetUserIP();
            info.LastLoginIP = info.RegisterIP;
            //info.Purse = SiteConfig.UserConfig.PresentMoney;//注册赠送的余额,积分等
            //info.UserPoint = SiteConfig.UserConfig.PresentPoint;
            //info.UserExp = DataConverter.CLng(SiteConfig.UserConfig.PresentExp);
            info.LoginTimes = 0;
            info.ConsumeExp = 0;
            info.IsConfirm = 0;
            info.VIP = 0;
            string puname = TxtParentUserID.Text.Replace(" ", "");
            if (!string.IsNullOrEmpty(puname))
            {
                M_UserInfo pmu = buser.GetUserByName(puname);
                if (pmu.UserID < 1 && DataConvert.CLng(puname) > 0) { pmu = buser.SelReturnModel(DataConvert.CLng(puname)); }
                if (pmu.UserID < 1) { function.WriteErrMsg("推荐人不存在!"); }
                info.ParentUserID = pmu.UserID.ToString();
            }
            //邀请码
            if (!string.IsNullOrEmpty(TxtInvitCode.Text))
            {
                M_User_Temp utMod = new M_User_Temp();
                utMod = utBll.Code_SelModel(TxtInvitCode.Text);
                if (utMod != null) { info.ParentUserID = utMod.UserID.ToString(); utBll.Code_Used(utMod.ID); }
            }
            //用于初始状态
            if (SiteConfig.UserConfig.EmailCheckReg) { info.Status = 4; }//邮件认证
            else if (SiteConfig.UserConfig.AdminCheckReg) { info.Status = 2; } //管理员认证
            else if (SiteConfig.UserConfig.EmailCheckReg && SiteConfig.UserConfig.AdminCheckReg) { info.Status = 3; } //邮件认证及管理员认证
            else if (!SiteConfig.UserConfig.UserValidateType) { info.Status = 5; }
            else { info.Status = 0; }

            //会员基本信息
            info.UserBase.Address = TxtAddress.Text.Trim();
            info.UserBase.BirthDay = TxtBirthday.Text.Trim();
            info.UserFace = TxtUserFace.Text.Trim(); ;
            info.UserBase.Fax = TxtFax.Text.Trim();
            info.UserBase.HomePage = TxtHomepage.Text.Trim();
            info.UserBase.HomePhone = TxtHomePhone.Text.Trim();
            info.UserBase.IDCard = TxtIDCard.Text.Trim();
            info.UserBase.Mobile = TxtMobile.Text.Trim();
            info.UserBase.OfficePhone = TxtOfficePhone.Text.Trim();
            info.UserBase.Privating = DropPrivacy.SelectedIndex;
            info.UserBase.PHS = TxtPHS.Text.Trim();
            info.UserBase.QQ = TxtQQ.Text.Trim();
            info.UserBase.Sign = TxtSign.Text.Trim();
            info.TrueName = TxtTrueName.Text.Trim();
            info.UserBase.UserSex = DataConverter.CBool(DropSex.SelectedValue);
            info.UserBase.ZipCode = TxtZipCode.Text.Trim();
            info.UserBase.HoneyName = "";
            info.UserBase.CardType = "";
            info.UserBase.Province = Request["selprovince"];
            info.UserBase.City = Request["selcity"];
            info.UserBase.County = Request["selcoutry"];
            string RegMessage = "";
            //自定义字段信息
            DataTable table = new Call().GetDTFromPage(bmf.Select_All(), Page, ViewState);
            string[] strArray2 = string.IsNullOrEmpty(SiteConfig.UserConfig.RegFieldsMustFill) ? new string[0] : SiteConfig.UserConfig.RegFieldsMustFill.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str2 in strArray2)
            {
                if (Request.Form["txt_" + str2] == null || Request.Form["txt_" + str2] == "")
                {
                    DataTable tbles = bmf.SelByFieldName(str2);
                }
            }
            //info.Answer = info.UserPwd;//用于二元
            info.UserPwd = StringHelper.MD5(info.UserPwd);
            info.UserID = buser.AddModel(info);
            //关联绑定微信用户
            //if (!string.IsNullOrEmpty(WXOpenID))
            //{
            //    B_User_Token tokenBll = new B_User_Token();
            //    M_User_Token tokenMod = tokenBll.SelModelByUid(info.UserID);
            //    if (tokenMod == null) { tokenMod = new M_User_Token(); }
            //    tokenMod.uid = info.UserID;
            //    tokenMod.WXOpenID = WXOpenID;
            //    tokenBll.Insert(tokenMod);
            //}
            buser.SetLoginState(info, "Day");
            if (info.UserID > 0)
            {
                info.UserBase.UserId = info.UserID;
                buser.AddBase(info.UserBase);
                if (table != null && table.Rows.Count > 0)
                {
                    buser.UpdateUserFile(info.UserID, table);
                }
                #region 自定义模型
                int groupid = DataConverter.CLng(UserGroup.SelectedValue);
                int ModelID = DataConverter.CLng(gpBll.GetGroupModel(groupid));
                string usertablename = b_Model.GetModelById(ModelID).TableName;


                if (ModelID > 0 && usertablename != "" && usertablename != null)
                {
                    DataTable groupset = fieldBll.GetModelFieldListall(ModelID);
                    DataTable tablereg = new DataTable();
                    tablereg.Columns.Add(new DataColumn("FieldName", typeof(string)));
                    tablereg.Columns.Add(new DataColumn("FieldType", typeof(string)));
                    tablereg.Columns.Add(new DataColumn("FieldValue", typeof(string)));
                    if (groupset != null && groupset.Rows.Count > 0)
                    {
                        foreach (DataRow dr in groupset.Rows)
                        {
                            if (dr["FieldType"].ToString() == "FileType")
                            {
                                string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                                bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                                string sizefield = Sett[1].Split(new char[] { '=' })[1];
                                if (chksize && sizefield != "")
                                {
                                    DataRow row2 = tablereg.NewRow();
                                    row2[0] = sizefield;
                                    row2[1] = "FileSize";
                                    row2[2] = Page.Request.Form["txt_" + sizefield];
                                    tablereg.Rows.Add(row2);
                                }
                            }

                            if (dr["FieldType"].ToString() == "MultiPicType")
                            {
                                string[] Sett = dr["Content"].ToString().Split(new char[] { ',' });
                                bool chksize = DataConverter.CBool(Sett[0].Split(new char[] { '=' })[1]);
                                string sizefield = Sett[1].Split(new char[] { '=' })[1];
                                if (chksize && sizefield != "")
                                {
                                    if (string.IsNullOrEmpty(Page.Request.Form["txt_" + sizefield]))
                                    {
                                        function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
                                    }
                                    DataRow row1 = tablereg.NewRow();
                                    row1[0] = sizefield;
                                    row1[1] = "ThumbField";
                                    row1[2] = Page.Request.Form["txt_" + sizefield];
                                    tablereg.Rows.Add(row1);
                                }
                            }

                            DataRow row = tablereg.NewRow();
                            row[0] = dr["FieldName"].ToString();
                            string ftype = dr["FieldType"].ToString();
                            row[1] = ftype;
                            string fvalue = Page.Request.Form["txt_" + dr["FieldName"].ToString()];
                            if (ftype == "TextType" || ftype == "MultipleTextType" || ftype == "MultipleHtmlType")
                            {
                                fvalue = sll.ProcessSen(fvalue);
                                if (dr["IsNotNull"].Equals("True") && string.IsNullOrEmpty(fvalue))
                                    function.WriteErrMsg(dr["FieldAlias"] + ":不能为空!");
                            }
                            row[2] = fvalue;
                            tablereg.Rows.Add(row);
                        }
                        try
                        {
                            if (tablereg.Select("FieldName='UserID'").Length == 0)
                            {
                                DataRow rowsd1 = tablereg.NewRow();
                                rowsd1[0] = "UserID";
                                rowsd1[1] = "int";
                                rowsd1[2] = info.UserID;
                                tablereg.Rows.Add(rowsd1);
                            }
                            else
                            {
                                tablereg.Rows[0]["UserID"] = info.UserID;
                            }
                        }
                        catch (Exception)
                        {

                        }

                        try
                        {
                            if (tablereg.Select("FieldName='UserName'").Length == 0)
                            {
                                DataRow rowsd2 = tablereg.NewRow();
                                rowsd2[0] = "UserName";
                                rowsd2[1] = "TextType";
                                rowsd2[2] = info.UserName;
                                tablereg.Rows.Add(rowsd2);
                            }
                            else
                            {
                                tablereg.Rows[0]["UserName"] = info.UserName;
                            }
                        }
                        catch (Exception) { }
                        try
                        {
                            if (tablereg.Select("FieldName='Styleid'").Length == 0)
                            {
                                DataRow rowsd3 = tablereg.NewRow();
                                rowsd3[0] = "Styleid";
                                rowsd3[1] = "int";
                                rowsd3[2] = 0;
                                tablereg.Rows.Add(rowsd3);
                            }
                            else
                            {
                                tablereg.Rows[0]["UserName"] = 0;
                            }
                        }
                        catch (Exception) { }
                        try
                        {
                            if (tablereg.Select("FieldName='Recycler'").Length == 0)
                            {
                                DataRow rowsd4 = tablereg.NewRow();
                                rowsd4[0] = "Recycler";
                                rowsd4[1] = "int";
                                rowsd4[2] = 0;
                                tablereg.Rows.Add(rowsd4);
                            }
                            else
                            {
                                tablereg.Rows[0]["Recycler"] = 0;
                            }
                        }
                        catch (Exception) { }
                        try
                        {
                            if (tablereg.Select("FieldName='IsCreate'").Length == 0)
                            {
                                DataRow rowsd5 = tablereg.NewRow();
                                rowsd5[0] = "IsCreate";
                                rowsd5[1] = "int";
                                rowsd5[2] = 0;
                                tablereg.Rows.Add(rowsd5);
                            }
                            else
                            {
                                tablereg.Rows[0]["IsCreate"] = 0;
                            }
                        }
                        catch (Exception) { }
                        try
                        {
                            if (tablereg.Select("FieldName='NewTime'").Length == 0)
                            {
                                DataRow rs6 = tablereg.NewRow();
                                rs6[0] = "NewTime";
                                rs6[1] = "DateType";
                                rs6[2] = DateTime.Now;
                                tablereg.Rows.Add(rs6);
                            }
                            else
                            {
                                tablereg.Rows[0]["NewTime"] = DateTime.Now;
                            }
                        }
                        catch (Exception) { }
                    }

                    if (tablereg != null && tablereg.Rows.Count > 0)
                    {
                        buser.InsertModel(tablereg, usertablename);
                    }
                }
                #endregion
                if (SiteConfig.UserConfig.EmailCheckReg && !string.IsNullOrEmpty(info.Email))//发送认证邮件，当需要邮件认证时
                {
                    MailInfo mailInfo = new MailInfo();
                    mailInfo.IsBodyHtml = true;
                    mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
                    MailAddress address = new MailAddress(info.Email);
                    mailInfo.ToAddress = address;
                    //SiteConfig.UserConfig.EmailOfRegCheck
                    string regurl = siteurls + "/User/RegisterCheck.aspx?UserName=" + HttpUtility.UrlEncode(info.UserName) + "&CheckNum=" + info.CheckNum;
                    string mailcontent = mailBll.SelByType(B_MailManage.MailType.NewUserReg);
                    mailInfo.MailBody = new OrderCommon().TlpDeal(mailcontent, GetRegEmailDt(info.UserName, info.CheckNum, regurl));
                    //mailInfo.MailBody = mailInfo.MailBody.Replace("{$UserName}", info.UserName).Replace("{$UserPwd}", TxtPassword.Text);
                    mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "网站会员注册验证码";
                    if (SendMail.Send(mailInfo) == SendMail.MailState.Ok)
                    {
                        RegMessage = "注册验证码已成功发送到你的注册邮箱，请到邮箱查收并验证!";
                        RegMessage = RegMessage + "<a href=\"/\">返回首页</a>";
                    }
                    else
                    {
                        RegMessage = "注册成功，但发送验证邮件失败，请检查邮件地址是否正确，或与网站管理员联系！";
                        RegMessage = RegMessage + "<a href=\"/\">返回首页</a>";
                    }
                }
                switch (info.Status)
                {
                    case 0:
                        #region 直接注册成功
                        if (!string.IsNullOrEmpty(info.Email) && SiteConfig.UserConfig.EmailTell)
                        {
                            MailInfo mailInfo = new MailInfo();
                            mailInfo.IsBodyHtml = true;
                            mailInfo.FromName = SiteConfig.SiteInfo.SiteName;
                            MailAddress address = new MailAddress(info.Email);
                            mailInfo.ToAddress = address;
                            //SiteConfig.UserConfig.EmailOfRegCheck
                            string regurl = siteurls + "/User/RegisterCheck.aspx?UserName=" + HttpUtility.UrlEncode(info.UserName) + "&CheckNum=" + info.CheckNum;
                            mailInfo.MailBody = new OrderCommon().TlpDeal(mailBll.SelByType(B_MailManage.MailType.NewUserReg), GetRegEmailDt(info.UserName, info.CheckNum, regurl));
                            //mailInfo.MailBody = mailInfo.MailBody.Replace("{$UserName}", info.UserName).Replace("{$UserPwd}", TxtPassword.Text);
                            mailInfo.Subject = SiteConfig.SiteInfo.SiteName + "_注册成功提醒";
                            if (SendMail.Send(mailInfo) == SendMail.MailState.Ok)
                            {
                                RegMessage = "注册基本信息已成功发送到你的注册邮箱！";
                                RegMessage = RegMessage + "<a href=\"/\">返回首页</a>";
                            }
                            else
                            {
                                RegMessage = "注册成功，但发送注册基本信息邮件失败，请检查邮件地址是否正确，或与网站管理员联系！";
                                RegMessage = RegMessage + "<a href=\"/\">返回首页</a>";
                            }
                        }
                        else
                        {
                            RegMessage = "注册成功！";
                            if (string.IsNullOrEmpty(ReturnUrl))
                            {
                                RegMessage = RegMessage + "<a href=\"/\">返回首页</a>&nbsp;&nbsp;<a href=\"/User/Index\">进入会员中心</a>,5秒后系统自动跳转到会员中心!";
                                hfReturnUrl.Value = "/User/Index";
                            }
                            else
                            {
                                RegMessage = RegMessage + "<a href=\"/\">返回首页</a>&nbsp;&nbsp;<a href=\"" + ReturnUrl + "\">进入默认页面</a>,5秒后系统自动跳转到默认页面!";
                                hfReturnUrl.Value = ReturnUrl;
                            }
                            isok = true;
                        }
                        #endregion
                        break;
                    case 2: //等待管理员认证
                        RegMessage = "注册成功！新注册会员需管理员认证才能有效，请耐心等待！";
                        RegMessage = RegMessage + "若长期没有通过管理员认证,请及时和管理员联系！";
                        RegMessage = RegMessage + "<a href=\"/\">返回首页</a>";
                        break;
                    default:
                        //未开启邮箱验证，则可以登录
                        if (!SiteConfig.UserConfig.EmailCheckReg) { RegMessage = "注册成功!"; }
                        if (string.IsNullOrEmpty(ReturnUrl))
                        {
                            RegMessage = RegMessage + "<a href=\"/\">返回首页</a>&nbsp;&nbsp;<a href=\"/User/Index\">进入会员中心</a>,5秒后系统自动跳转到会员中心!";
                            hfReturnUrl.Value = "default";
                        }
                        else
                        {
                            RegMessage = RegMessage + "<a href=\"/\">返回首页</a>&nbsp;&nbsp;<a href=\"" + ReturnUrl + "\">进入默认页面</a>,5秒后系统自动跳转到默认页面!";
                            hfReturnUrl.Value = ReturnUrl;
                        }
                        isok = true;
                        break;
                }
            }
            else
            {
                RegMessage = "注册失败！";
                RegMessage = RegMessage + "<a href=\"/\">返回首页</a>&nbsp;&nbsp;<a href=\"Register\">重新注册</a>";
            }
            if (SiteConfig.UserConfig.EmailCheckReg)
            {
                PnlRegStep2.Visible = false;
                PnlMRegStep1.Visible = false;
                PnlStep3.Visible = true;
                LitRegResult.Text = "<div class='emptyDiv'><br/>注册成功! &nbsp;&nbsp;<a href='http://mail." + TxtEmail.Text.Substring(TxtEmail.Text.LastIndexOf('@') + 1) + "' target='_blank'>立即登录邮箱进行验证>></a><br/></div>";
            }
            else
            {
                PnlRegStep2.Visible = false;
                PnlMRegStep1.Visible = false;
                PnlStep3.Visible = true;
                LitRegResult.Text = RegMessage;
                //未开启邮箱验证则自动跳转
                if (isok) { function.Script(this, "gotouser();"); }
            }
        }
        // 推广注册添加信息
        private void Redindul(int userid)
        {
            if (ViewState["SendID"] != null)
            {
                int id = DataConverter.CLng(ViewState["SendID"]);
                M_InviteRecord minv = binv.GetSelByRuid(userid);
                if (minv != null && minv.id > 0)
                {
                    return;
                }
                if (minv == null)
                {
                    minv = new M_InviteRecord();
                }
                minv.userid = id;  //推荐用户,发推广信息的用户
                minv.RecommUserId = userid; //推荐注册的用户
                minv.RegData = DateTime.Now;
                minv.isReset = 0;
                minv.isValid = 0;
                int bid = binv.GetInsert(minv);
            }
        }
        // 读取用户协议
        private void InitProtocol()
        {
            try
            {
                LitProtocol.Text = FileSystemObject.ReadFile(Server.MapPath("~/BU/Protocol.txt"));
                Agreement_Lit.Text = SafeSC.ReadFileStr(Server.MapPath("~/BU/Protocol.txt"), true);
            }
            catch (Exception) { }
        }
        // 检验验证码
        private void CheckCode()
        {
            if (SiteConfig.UserConfig.EnableCheckCodeOfReg)
            {
                if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], VCode.Text.Trim()))
                {
                    function.WriteErrMsg("您输入的验证码和系统产生的不一致，请重新输入", "javascript:history.go(-1);");
                }
            }
        }
        // 检验邮件Email是否重复
        private void CheckEmail()
        {
            TxtEmail.Text = TxtEmail.Text.Trim();
            if (string.IsNullOrEmpty(TxtEmail.Text) || buser.IsExistMail(TxtEmail.Text))
            {
                function.WriteErrMsg("Email为空或已被他人注册，请输入不同的Email!");
            }
        }
        //检测会员名是否有效
        private void CheckUserName()
        {
            TxtUserName.Text = TxtUserName.Text.Replace(" ", "");
            if (StringHelper.FoundInArr(SiteConfig.UserConfig.UserNameRegDisabled, TxtUserName.Text, "|"))
            {
                function.WriteErrMsg("该用户名禁止注册，请输入不同的用户名!");
            }
            if (buser.IsExistUName(TxtUserName.Text))
            {
                function.WriteErrMsg("该用户名已被他人占用，请输入不同的用户名");
            }
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
                            string resultString = null;
                            try
                            {
                                resultString = Regex.Match(TxtUserName.Text, @"[0-9]*").Value;
                            }
                            catch (ArgumentException)
                            {
                            }
                            if (TxtUserName.Text.Trim() == resultString.Trim())
                            {
                                function.WriteErrMsg("用户名不允许纯数字");
                            }
                        }

                        if (rulearr[ii].ToString() == "2")
                        {
                            string resultString = null;
                            try
                            {
                                resultString = Regex.Match(TxtUserName.Text, @"[a-zA-Z]*").Value;
                            }
                            catch (ArgumentException)
                            {
                            }

                            if (TxtUserName.Text == resultString)
                            {
                                function.WriteErrMsg("用户名不允许纯英文");
                            }

                        }

                        if (rulearr[ii].ToString() == "3")
                        {
                            bool foundMatch = false;
                            try
                            {
                                foundMatch = Regex.IsMatch(TxtUserName.Text, @"[\u0391-\uFFE5]$");
                            }
                            catch (ArgumentException)
                            {
                            }

                            if (foundMatch)
                            {
                                function.WriteErrMsg("用户名不允许带有中文");
                            }
                        }
                    }
                }
                else
                {
                    if (userregrule.ToString() == "1")
                    {
                        string resultString = null;
                        try
                        {
                            resultString = Regex.Match(TxtUserName.Text, @"[0-9]*").Value;
                        }
                        catch (ArgumentException)
                        {
                        }


                        if (TxtUserName.Text.Trim() == resultString.Trim())
                        {
                            function.WriteErrMsg("用户名不允许纯数字");
                        }
                    }

                    if (userregrule.ToString() == "2")
                    {
                        string resultString = null;
                        try
                        {
                            resultString = Regex.Match(TxtUserName.Text, @"[a-zA-Z]*").Value;
                        }
                        catch (ArgumentException)
                        {
                        }

                        if (TxtUserName.Text == resultString)
                        {
                            function.WriteErrMsg("用户名不允许纯英文");
                        }

                    }

                    if (userregrule.ToString() == "3")
                    {
                        bool foundMatch = false;
                        try
                        {
                            foundMatch = Regex.IsMatch(TxtUserName.Text, @"[\u0391-\uFFE5]$");
                        }
                        catch (ArgumentException)
                        {
                        }

                        if (foundMatch)
                        {
                            function.WriteErrMsg("用户名不允许带有中文");
                        }
                    }
                }
            }
        }
        protected bool GetEnableValid(string field)
        {
            return StringHelper.FoundCharInArr(SiteConfig.UserConfig.RegFieldsMustFill, field, ",");
        }
        public string CheckUserData(string eventArgument)
        {
            eventArgument = eventArgument.Trim();
            if (string.IsNullOrEmpty(eventArgument))
            {
                result = "empty";
            }
            else
            {
                if (SiteConfig.SiteOption.ServerType == "Server")
                {

                    if (StringHelper.FoundInArr(SiteConfig.UserConfig.UserNameRegDisabled, eventArgument, "|"))
                    {
                        result = "disabled";
                    }
                    else if (buser.IsExistUName(eventArgument))
                    {
                        result = "true";
                    }
                    else
                    {
                        if (SiteConfig.UserConfig.UserNameLimit > eventArgument.Length || eventArgument.Length > SiteConfig.UserConfig.UserNameMax)
                        {
                            result = "error";
                        }
                        else
                        {
                            if (buser.IsExitcard(eventArgument))
                            {
                                result = "Fidtrue";//身份证存在
                            }
                            else
                            {
                                bool foundMatch = false;
                                try
                                {
                                    foundMatch = Regex.IsMatch(eventArgument, @"^\d{17}([0-9]|X)$");
                                    if (foundMatch)
                                    {
                                        string birth = eventArgument.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                                        DateTime time = new DateTime();
                                        DateTime newDate = DateTime.Now.AddYears(-200);
                                        DateTime now = DateTime.Now;

                                        if (DateTime.TryParse(birth, out time) == false)
                                        {
                                            result = "Fyidtrue"; //false 生日验证
                                        }
                                        else
                                        {
                                            DateTime data1 = Convert.ToDateTime(birth);
                                            TimeSpan ts = newDate - data1;
                                            TimeSpan ts2 = data1 - now;
                                            if (ts.Days > 0)
                                                result = "Fcidtrue";//小于当前200年后
                                            else if (ts2.Days > 0)
                                                result = "Fxidtrue";//大于当前时间
                                            else
                                                result = "Fno";//身份证18位且不存在
                                        }
                                    }
                                    else
                                    {
                                        result = "Fidtrue2";//身份证格式不正确
                                    }
                                }
                                catch (ArgumentException)
                                {
                                    result = "false";
                                    // Syntax error in the regular expression
                                }
                            }
                        }
                        string chestring = null;
                        try
                        {
                            chestring = Regex.Match(eventArgument, @"[a-zA-Z0-9\u4e00-\u9fa5\@\.]*").Value;
                        }
                        catch (ArgumentException)
                        {
                            // Syntax error in the regular expression
                        }

                        if (chestring != eventArgument)
                        {
                            string[] chr = eventArgument.Split(new char[] { '|' });

                            try
                            {

                                if (chr.Length > 1)
                                {
                                    if (buser.IsExistUName(chr[1]))
                                    {
                                        result = "Ptrue";//用户存在
                                    }
                                    else
                                    {
                                        result = "Pno";
                                    }
                                }
                            }
                            catch
                            {
                                result = "error04";//用户名不允许纯数字
                            }
                        }


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
                                        string resultString = null;
                                        try
                                        {
                                            resultString = Regex.Match(eventArgument, @"[0-9]*").Value;
                                        }
                                        catch (ArgumentException)
                                        {
                                            // Syntax error in the regular expression
                                        }


                                        if (resultString == eventArgument)
                                        {
                                            if (buser.IsExitcard(eventArgument))
                                            {
                                                result = "Nidtrue";//身份证存在
                                            }
                                            else
                                            {
                                                bool foundMatch = false;
                                                try
                                                {
                                                    foundMatch = Regex.IsMatch(eventArgument, @"^\d{18}$");
                                                    if (foundMatch)
                                                    {
                                                        string birth = eventArgument.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                                                        DateTime time = new DateTime();
                                                        DateTime newDate = DateTime.Now.AddYears(-200);
                                                        DateTime now = DateTime.Now;

                                                        if (DateTime.TryParse(birth, out time) == false)
                                                        {
                                                            result = "Nyidtrue"; //false 生日验证
                                                        }
                                                        else
                                                        {
                                                            DateTime data1 = Convert.ToDateTime(birth);
                                                            TimeSpan ts = newDate - data1;
                                                            TimeSpan ts2 = data1 - now;
                                                            if (ts.Days > 0)
                                                                result = "Ncidtrue";
                                                            else if (ts2.Days > 0)
                                                                result = "Nxidtrue";
                                                            else
                                                                result = "Nno";//身份证18位且不存在
                                                        }
                                                    }
                                                    else
                                                    {
                                                        result = "Nidtrue2";//身份证格式不正确
                                                    }
                                                }
                                                catch (ArgumentException)
                                                {
                                                    result = "error01";  // Syntax error in the regular expression
                                                }
                                            }
                                        }
                                    }

                                    if (rulearr[ii].ToString() == "2")
                                    {
                                        string resultString = null;
                                        try
                                        {
                                            resultString = Regex.Match(eventArgument, @"[a-zA-Z]*").Value;
                                        }
                                        catch (ArgumentException)
                                        {
                                            // Syntax error in the regular expression
                                        }


                                        if (eventArgument == resultString)
                                        {
                                            result = "error02";//用户名不允许纯英文
                                        }

                                    }

                                    if (rulearr[ii].ToString() == "3")
                                    {
                                        bool foundMatch = false;
                                        try
                                        {
                                            foundMatch = Regex.IsMatch(eventArgument, @"[\u0391-\uFFE5]$");
                                        }
                                        catch (ArgumentException)
                                        {
                                            // Syntax error in the regular expression
                                        }
                                        if (foundMatch)
                                        {
                                            result = "error03";//用户名不允许带有中文
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (userregrule.ToString() == "1")
                                {
                                    string resultString = null;
                                    try
                                    {
                                        resultString = Regex.Match(eventArgument, @"[0-9]*").Value;
                                    }
                                    catch (ArgumentException)
                                    {
                                        // Syntax error in the regular expression
                                    }


                                    if (resultString == eventArgument)
                                    {
                                        result = "error01";//用户名不允许纯数字
                                        //result = resultString;
                                    }
                                }

                                if (userregrule.ToString() == "2")
                                {
                                    string resultString = null;
                                    try
                                    {
                                        resultString = Regex.Match(eventArgument, @"[a-zA-Z]*").Value;
                                    }
                                    catch (ArgumentException)
                                    {
                                        // Syntax error in the regular expression
                                    }


                                    if (eventArgument == resultString)
                                    {
                                        result = "error02";//用户名不允许纯英文
                                        //result = resultString;
                                    }

                                }

                                if (userregrule.ToString() == "3")
                                {
                                    bool foundMatch = false;
                                    try
                                    {
                                        foundMatch = Regex.IsMatch(eventArgument, @"[\u0391-\uFFE5]$");
                                    }
                                    catch (ArgumentException)
                                    {
                                        // Syntax error in the regular expression
                                    }
                                    if (foundMatch)
                                    {
                                        result = "error03";//用户名不允许带有中文
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
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
                                    string resultString = null;
                                    try
                                    {
                                        resultString = Regex.Match(eventArgument, @"[0-9]*").Value;
                                    }
                                    catch (ArgumentException)
                                    {
                                        // Syntax error in the regular expression
                                    }

                                    if (TxtUserName.Text.Trim() == resultString.Trim())
                                    {
                                        result = "error01";//用户名不允许纯数字
                                        //result = resultString;
                                    }
                                }

                                if (rulearr[ii].ToString() == "2")
                                {
                                    string resultString = null;
                                    try
                                    {
                                        resultString = Regex.Match(eventArgument, @"[a-zA-Z]*").Value;
                                    }
                                    catch (ArgumentException)
                                    {
                                        // Syntax error in the regular expression
                                    }


                                    if (eventArgument == resultString)
                                    {
                                        result = "error02";//用户名不允许纯英文
                                    }

                                }
                                if (rulearr[ii].ToString() == "3")
                                {
                                    bool foundMatch = false;
                                    try
                                    {
                                        foundMatch = Regex.IsMatch(eventArgument, @"[\u0391-\uFFE5]$");
                                    }
                                    catch (ArgumentException)
                                    {
                                        // Syntax error in the regular expression
                                    }
                                    if (foundMatch)
                                    {
                                        result = "error03";//用户名不允许带有中文
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (userregrule.ToString() == "1")
                            {
                                string resultString = null;
                                try
                                {
                                    resultString = Regex.Match(eventArgument, @"[0-9]*").Value;
                                }
                                catch (ArgumentException)
                                {
                                    // Syntax error in the regular expression
                                }

                                if (TxtUserName.Text.Trim() == resultString.Trim())
                                {
                                    result = "error01";//用户名不允许纯数字
                                    //result = resultString;
                                }
                            }

                            if (userregrule.ToString() == "2")
                            {
                                string resultString = null;
                                try
                                {
                                    resultString = Regex.Match(eventArgument, @"[a-zA-Z]*").Value;
                                }
                                catch (ArgumentException)
                                {
                                    // Syntax error in the regular expression
                                }

                                if (eventArgument == resultString)
                                {
                                    result = "error02";//用户名不允许纯英文
                                }
                            }

                            if (userregrule.ToString() == "3")
                            {
                                bool foundMatch = false;
                                try
                                {
                                    foundMatch = Regex.IsMatch(eventArgument, @"[\u0391-\uFFE5]$");
                                }
                                catch (ArgumentException)
                                {
                                    // Syntax error in the regular expression
                                }
                                if (foundMatch)
                                {
                                    result = "error03";//用户名不允许带有中文
                                }
                            }
                        }
                    }

                    if (buser.IsExitcard(eventArgument))
                    {
                        result = "idtrue";//身份证存在
                    }
                    else
                    {
                        bool foundMatch = false;
                        try
                        {
                            foundMatch = Regex.IsMatch(eventArgument, @"^\d{17}([0-9]|X)$");
                            if (foundMatch)
                            {
                                string birth = eventArgument.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                                DateTime time = new DateTime();
                                DateTime newDate = DateTime.Now.AddYears(-200);
                                DateTime now = DateTime.Now;

                                if (DateTime.TryParse(birth, out time) == false)
                                {
                                    result = "yidtrue"; //false 生日验证
                                }
                                else
                                {
                                    DateTime data1 = Convert.ToDateTime(birth);
                                    TimeSpan ts = newDate - data1;
                                    TimeSpan ts2 = data1 - now;
                                    if (ts.Days > 0)
                                        result = "cidtrue";
                                    else if (ts2.Days > 0)
                                    {
                                        result = "xidtrue";
                                    }
                                    else
                                    {
                                        result = "no";//身份证18位且不存在
                                    }
                                }
                            }
                            else
                            {
                                result = "idtrue2";//身份证格式不正确
                            }
                        }
                        catch (ArgumentException)
                        {
                            // Syntax error in the regular expression
                        }

                    }

                }
            }
            return result;
        }
        //检测出生日期是否合逻辑
        string CheckBirthDay(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "2";
            if (DateTime.Parse(value) < DateTime.Now.AddYears(-150))
            {
                return "1";
            }
            return "0";
        }
        //推荐人校验
        public string CheckParentUser(string uname)
        {
            string result = "0";//用户不存在
            if (string.IsNullOrEmpty(uname))
                result = "-1";
            else if (buser.IsExistUName(uname.Trim()))
                result = "1";
            return result;
        }
        //邀请码校验
        public string CheckInvitCode(string code)
        {
            string result = "";
            if (string.IsNullOrEmpty(code))
            {
                result = "-1";
            }
            else if (utBll.Code_IsExist(code))
            {
                result = "1";
            }
            else { result = "0"; }
            return result;
        }
        // IP地址转换成Int数据
        private long IpToInt(string ip)
        {
            char[] dot = new char[] { '.' };
            string[] ipArr = ip.Split(dot);
            if (ipArr.Length == 3)
                ip = ip + ".0";
            ipArr = ip.Split(dot);

            long ip_Int = 0;
            if (ipArr.Length > 3)
            {
                long p1 = long.Parse(ipArr[0]) * 256 * 256 * 256;
                long p2 = long.Parse(ipArr[1]) * 256 * 256;
                long p3 = long.Parse(ipArr[2]) * 256;
                long p4 = long.Parse(ipArr[3]);
                ip_Int = p1 + p2 + p3 + p4;
            }
            return ip_Int;
        }
        //用户信息验证,身份证号,生日等
        public void Checkyes()
        {
            if (TxtIDCard.Text.Length != 0)
            {
                string eventArgument = TxtIDCard.Text;
                if (buser.IsExitcard(eventArgument))
                {
                    function.WriteErrMsg("该身份证号已被注册，请输入不同的身份证号！");//身份证存在
                }
                else
                {
                    bool foundMatch = false;
                    try
                    {
                        foundMatch = Regex.IsMatch(eventArgument, @"^\d{17}([0-9]|X)$");
                        if (foundMatch)
                        {
                            string birth = eventArgument.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                            DateTime time = new DateTime();
                            DateTime newDate = DateTime.Now.AddYears(-120);
                            DateTime now = DateTime.Now;

                            if (DateTime.TryParse(birth, out time) == false)
                            {
                                function.WriteErrMsg("该身份证生日不正确！");
                                //result = "yidtrue"; //false 生日验证
                            }
                            else
                            {
                                DateTime data1 = Convert.ToDateTime(birth);
                                TimeSpan ts = newDate - data1;
                                TimeSpan ts2 = data1 - now;
                                if (ts.Days > 0)
                                    function.WriteErrMsg("您超过了120岁？请核对身份证号。");
                                else if (ts2.Days > 0)
                                {
                                    function.WriteErrMsg("您还没出生吧？请核对身份证号。");
                                }
                                else
                                {
                                    // result = "no";//身份证18位且不存在
                                }
                            }
                        }
                        else
                        {
                            function.WriteErrMsg("该身份证格式不正确！");//身份证格式不正确
                        }
                    }
                    catch (ArgumentException)
                    {
                        // Syntax error in the regular expression
                    }

                }
            }
        }
        //推荐人是否存在
        protected void CheckPUserName()
        {
            if (TxtParentUserID.Text.Length != 0)
            {
                if ((buser.IsExit(DataConvert.CLng(TxtParentUserID.Text))) || buser.IsExistUName(TxtParentUserID.Text))
                {
                }
                else
                {
                    function.WriteErrMsg("该推荐人不存在！");
                }

            }

        }
        // 发送短信调用接口
        private string SendMsg(string uid, string pwd, string mob, string msg)
        {
            //return "000";
            string Send_URL = "http://service.winic.org/sys_port/gateway/?id=" + uid + "&pwd=" + pwd + "&to=" + mob + "&content=" + msg + "&time=";
            MSXML2.XMLHTTP xmlhttp = new MSXML2.XMLHTTP();
            xmlhttp.open("GET", Send_URL, false, null, null);
            xmlhttp.send("");
            Byte[] b = (Byte[])xmlhttp.responseBody;
            string andy = System.Text.Encoding.GetEncoding("GB2312").GetString(b).Trim();
            return andy;
        }
        private string getDictionaryData(Dictionary<string, object> data)
        {
            string ret = null;
            foreach (KeyValuePair<string, object> item in data)
            {
                if (item.Value != null && item.Value.GetType() == typeof(Dictionary<string, object>))
                {
                    ret += item.Key.ToString() + "={";
                    ret += getDictionaryData((Dictionary<string, object>)item.Value);
                    ret += "};";
                }
                else
                {
                    ret += item.Key.ToString() + "=" + (item.Value == null ? "null" : item.Value.ToString()) + ";";
                }
            }
            return ret;
        }
        protected string Islimited()
        {
            string str = "";
            if (HttpContext.Current.Request.Cookies["postTime"] == null)
            {
                HttpContext.Current.Response.Cookies["postTime"].Expires = DateTime.Now.AddMinutes(1);
            }
            else
            {
                str = "一分钟内只能发送一条信息，请稍后！";
            }
            return str;
        }
        //手机校验码,用于手机校验注册时
        public string CheckNum { get { return ViewState["CheckNum"] as string; } set { ViewState["CheckNum"] = value; } }
        //发送短信
        protected void SendMob_Click(object sender, EventArgs e)
        {
            CheckNum = function.GetRandomString(SiteConfig.UserConfig.MobileCodeNum, SiteConfig.UserConfig.MobileCodeType).ToLower();
            if (Page.IsValid)
            {
                string str = Islimited();
                if (str == "")
                {
                    string mob = TxtMobile1.Text.Trim();
                    if (string.IsNullOrEmpty(mob))
                    {
                        function.WriteErrMsg("请输入手机号码");
                    }
                    string msg = SiteConfig.UserConfig.MobileRegInfo.Replace("{$CheckNum}", CheckNum).Replace("{$SiteName}", SiteConfig.SiteInfo.SiteName).Replace("{$SiteUrl}", SiteConfig.SiteInfo.SiteUrl);
                    if (string.IsNullOrEmpty(msg))
                    {
                        function.WriteErrMsg("短信内容不能为空");
                    }
                    if (DataSecurity.Len(msg) > 70)
                    {
                        function.WriteErrMsg("短信内容不能超过70个字");
                    }
                    string[] mobarr = mob.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (mobarr.Length == 0)
                    {
                        function.WriteErrMsg("请输入手机号码");
                    }
                    else
                    {
                        //if (mobarr.Length > 100)
                        //{
                        //    function.WriteErrMsg("每次只能发送100条短信");
                        //}
                        if (mobarr.Length > 1)
                        {
                            function.WriteErrMsg("每次只能发送一条短信");
                        }
                        else
                        {
                            string req = SendMsg(SiteConfig.SiteOption.MssUser, SiteConfig.SiteOption.MssPsw, mob, msg);
                            string[] reqs = req.Split(new char[] { '/' });
                            string res = "";
                            switch (reqs[0])
                            {
                                case "000":
                                    res = "发送成功！";
                                    addMessage();
                                    //res += "发送条数:" + reqs[1].Split(new char[] { ':' })[1] + "<br/>";
                                    //res += "当次消费金额" + reqs[2].Split(new char[] { ':' })[1] + "<br/>";
                                    //res += "总体余额:" + reqs[3].Split(new char[] { ':' })[1] + "<br/>";
                                    //res += "短信编号:" + reqs[4];
                                    break;
                                case "-01":
                                    res = "当前短信接口账号余额不足！";
                                    break;
                                case "-02":
                                    res = "当前短信接口用户ID错误！";
                                    break;
                                case "-03":
                                    res = "当前短信接口密码错误！";
                                    break;
                                case "-04":
                                    res = "参数不够或参数内容的类型错误！";
                                    break;
                                case "-05":
                                    res = "手机号码格式不对！";
                                    break;
                                case "-06":
                                    res = "短信内容编码不对！";
                                    break;
                                case "-07":
                                    res = "短信内容含有敏感字符！";
                                    break;
                                case "-8":
                                    res = "无接收数据";
                                    break;
                                case "-09":
                                    res = "系统维护中..";
                                    break;
                                case "-10":
                                    res = "手机号码数量超长！（100个/次 超100个请自行做循环）";
                                    break;
                                case "-11":
                                    res = "短信内容超长！（70个字符）";
                                    break;
                                case "-12":
                                    res = "其它错误！";
                                    break;
                            }
                            ClientScript.RegisterStartupScript(typeof(string), "script", "<script>alert('" + res + "');window.onload = TimeClose;</script>");
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(string), "script", "<script>alert('" + str + "');</script>");
                }
            }
        }
        //添加一条信息
        protected void addMessage()
        {
            M_Message messInfo = new M_Message();
            messInfo.Sender = "1";
            messInfo.Title = "注册验证手机短信";
            messInfo.PostDate = DataConverter.CDate(DateTime.Now.ToShortDateString());
            messInfo.Content = SiteConfig.UserConfig.MobileRegInfo.Replace("{$CheckNum}", CheckNum).Replace("{$SiteName}", SiteConfig.SiteInfo.SiteName).Replace("{$SiteUrl}", SiteConfig.SiteInfo.SiteUrl);
            messInfo.Receipt = "";
            messInfo.MsgType = 2;
            messInfo.Incept = TxtMobile1.Text.Trim();
            B_Message.Add(messInfo);
        }
        protected void BtnMRegStep1_Click(object sender, EventArgs e)
        {
            if (!mobileBll.CheckVaildCode(TxtMobile1.Text, MobileCode_T.Text)) { function.WriteErrMsg("短信验证码不正确!"); }
            try
            {
                TxtMobile.Text = TxtMobile1.Text;
                TxtUserName.Text = TxtMobile1.Text;
                BtnRegStep1.Visible = false;
                BtnRegStep1.Visible = false;
                PnlMRegStep1.Visible = false;
                BtnRegstep1();
            }
            catch
            {
                function.Script(this, "alert('请重新发送短信验证码!');");
            }
        }
        //返回用户名,Email,或密码,前台页面使用
        public string GetInfo(int a)
        {
            switch (a)
            {
                case 0:
                    return TxtUserName.Text;
                case 1:
                    return TxtEmail.Text;
                case 2:
                    {
                        string password = TxtPassword.Text.Substring(0, 3);
                        for (int i = 0; i < TxtPassword.Text.Length - 3; i++)
                        {
                            password += "*";
                        }
                        return password;
                    }
                default:
                    return null;
            }
        }

        public void DebugUser()
        {
            TxtUserName.Text = "test" + DataConvert.CLng(SqlHelper.ExecuteTable("SELECT COUNT(UserID) FROM ZL_User").Rows[0][0]);
            TxtPassword.Text = "123123aa";
            TxtPwdConfirm.Text = "123123aa";
            TxtAnswer.Text = "TestForThis";
            TxtEmail.Text = TxtUserName.Text + "@qqq.com";
        }
        public string GetBKImg()
        {
            if (SiteConfig.SiteOption.SiteManageMode == 1) { return ""; }
            else { return "http://code.z01.com/user_login.jpg"; }
        }
    }
}