using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using ZoomLa.BLL;
using ZoomLa.BLL.API;
using ZoomLa.BLL.Helper;
using ZoomLa.BLL.Plat;
using ZoomLa.BLL.User;
using ZoomLa.Common;
using ZoomLa.Components;
using ZoomLa.Model;
using ZoomLa.Model.User;
using ZoomLa.SQLDAL;
using ZoomLaCMS.Models.Front;

namespace ZoomLaCMS.Areas.User.Controllers
{
    public class IndexController : Ctrl_Guest
    {
        B_User_Plat upBll = new B_User_Plat();
        B_Plat_Comp compBll = new B_Plat_Comp();
        B_User_InviteCode utBll = new B_User_InviteCode();
        B_MailManage mailBll = new B_MailManage();
        B_Safe_Mobile mobileBll = new B_Safe_Mobile();
        B_Group gpBll = new B_Group();
        B_Model modBll = new B_Model();
        B_ModelField fieldBll = new B_ModelField();
        //手机注册,已验证过的手机号,注册完成或关闭浏览器后清除
        private string RegisterMobile { get { return Session["Register_Mobile_Checked"] as string; } set { Session["Register_Mobile_Checked"] = value; } }
        public ActionResult Index()
        {
            if (!B_User.CheckIsLogged(Request.RawUrl)) { return null; }
            M_UserInfo mu = buser.GetLogin();
            if (!B_User.CheckUserStatus(mu, ref err)) { function.WriteErrMsg(err); return null; }
            B_PointGrounp pgBll = new B_PointGrounp();
            B_Search shBll = new B_Search();
            M_Uinfo basemu = buser.GetUserBaseByuserid(mu.UserID);
            //--------------------------------------------------
            DataTable dt = shBll.SelByUserGroup(mu.GroupID);
            string userapptlp = "<div class='col-lg-2 col-md-3 col-sm-4 col-xs-4 padding10'><div class='user_menu'><a target='@target' href='@fileUrl'>@ico<br/>@name</a></div></div>";
            string onthertlp = "<li><a target='@target' href='@fileUrl'>@ico<span>@name</span></a></li>";
            string userhtml = "";
            string ontherhtml = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string targetlink = GetLinkTarget(dt.Rows[i]["OpenType"].ToString());
                if (DataConverter.CLng(dt.Rows[i]["EliteLevel"]) == 1)//抽出推荐应用
                {
                    userhtml += ReplaceData(userapptlp, dt.Rows[i]).Replace("@target", targetlink);
                    continue;
                }
                else
                    ontherhtml += ReplaceData(onthertlp, dt.Rows[i]).Replace("@target", targetlink);
            }
            //---------------------
            ViewBag.userhtml = MvcHtmlString.Create(userhtml);
            ViewBag.ontherhtml = MvcHtmlString.Create(ontherhtml);
            ViewBag.pgMod = pgBll.SelectPintGroup((int)mu.UserExp);
            ViewBag.basemu = basemu;
            return View(mu);
        }
        public void Default() { Response.Redirect("/User/Index"); return; }
        public ActionResult Login()
        {
            if (buser.CheckLogin()) { return RedirectToAction("Index", "Index"); }
            return View(buser.GetLogin());
        }
        public ActionResult Register()
        {
            if (SiteConfig.UserConfig.Agreement.Equals("4")) { Response.Redirect("/User/RegPlat"); return null; }
            if (!SiteConfig.UserConfig.EnableUserReg) { function.WriteErrMsg("未开放注册,请和网站管理员联系!"); return null; }
            VM_Register model = new VM_Register();
            if (SiteConfig.UserConfig.MobileReg == true && string.IsNullOrEmpty(RegisterMobile))
            {
                return View("Register_Mobile");
            }
            model.Mobile = RegisterMobile;
            return View(model);
        }
        [HttpPost]
        public void Register_MobileCheck()
        {
            string mobile = Request.Form["TxtMobile"];
            string code = Request.Form["MobileCode_T"];
            if (!mobileBll.CheckVaildCode(mobile, code, ref err))
            {
                function.WriteErrMsg(err); return;
            }
            else//验证通过
            {
                RegisterMobile = mobile;
                Response.Redirect("/User/Register");
            }
        }
        [HttpPost]
        public string Register_API()
        {
            M_APIResult retMod = new M_APIResult(M_APIResult.Success);
            string action = Request["action"];
            string value = (Request.Form["value"] ?? "").Replace(" ", "");
            string result = "";
            switch (action)
            {
                case "uname":
                    if (!CheckUserName(value, ref result))
                    {
                        retMod.retcode = M_APIResult.Failed;
                        retMod.retmsg = result;
                    }
                    break;
                case "puser"://推荐人为空则不检测
                    if (!string.IsNullOrEmpty(value) && CheckParentUser(value).IsNull)
                    {
                        retMod.retcode = M_APIResult.Failed;
                        retMod.retmsg = "推荐人不存在";
                    }
                    break;
                case "email":
                    if (!CheckEmail(value, ref result))
                    {
                        retMod.retcode = M_APIResult.Failed;
                        retMod.retmsg = result;
                    }
                    break;
                case "invite":
                    if (!CheckInvitCode(value, ref result))
                    {
                        retMod.retcode = M_APIResult.Failed;
                        retMod.retmsg = result;
                    }
                    break;
                case "birth":
                    if (!CheckBirthDay(value, ref result))
                    {
                        retMod.retcode = M_APIResult.Failed;
                        retMod.retmsg = result;
                    }
                    break;
                //case "GetModelFied":
                //    result = GetUserGorupModel(value);
                //    break;
                default:
                    retMod.retmsg = "[" + action + "]接口不存在";
                    break;
            }
            return retMod.ToString();
        }
        [HttpPost]
        public ActionResult Register_Submit()
        {
            B_UserBaseField bmf = new B_UserBaseField();
            string siteurls = SiteConfig.SiteInfo.SiteUrl.TrimEnd('/');
            if (!SiteConfig.UserConfig.EnableUserReg) { function.WriteErrMsg("服务器已关闭用户注册"); return null; }
            //-----------------------------------
            M_UserInfo info = new M_UserInfo();
            info.UserBase = new M_Uinfo();
            info.UserName = Request.Form["TxtUserName"].Replace(" ", "");
            info.UserPwd = Request.Form["TxtPassword"];
            info.Question = Request.Form["Question_DP"];
            info.Answer = Request.Form["TxtAnswer"];
            info.Email = Request.Form["TxtEmail"].Replace(" ", "");
            info.CheckNum = function.GetRandomString(10);
            info.GroupID = DataConverter.CLng(Request.Form["UserGroup"]);
            info.RegisterIP = EnviorHelper.GetUserIP();
            info.LastLoginIP = info.RegisterIP;
            //info.Purse = SiteConfig.UserConfig.PresentMoney;//注册赠送的余额,积分等
            //info.UserPoint = SiteConfig.UserConfig.PresentPoint;
            //info.UserExp = DataConverter.CLng(SiteConfig.UserConfig.PresentExp);
            info.TrueName = Request.Form["TxtTrueName"];
            info.UserPwd = StringHelper.MD5(info.UserPwd);;
            //-----------------------------------------------------
            //会员基本信息
            info.UserBase.Address = Request.Form["TxtAddress"];
            info.UserBase.BirthDay = Request.Form["TxtBirthday"];
            info.UserFace = Request.Form["TxtUserFace"];
            info.UserBase.Fax = Request.Form["TxtFax"];
            info.UserBase.HomePage = Request.Form["TxtHomepage"];
            info.UserBase.HomePhone = Request.Form["TxtHomePhone"];
            info.UserBase.IDCard = Request.Form["TxtIDCard"];
            info.UserBase.Mobile = Request.Form["TxtMobile"];
            info.UserBase.OfficePhone = Request.Form["TxtOfficePhone"];
            info.UserBase.Privating = DataConvert.CLng(Request.Form["DropPrivacy"]);
            info.UserBase.PHS = Request.Form["TxtPHS"];
            info.UserBase.QQ = Request.Form["TxtQQ"];
            info.UserBase.Sign = Request.Form["TxtSign"];
            info.UserBase.UserSex = DataConverter.CBool(Request.Form["DropSex"]);
            info.UserBase.ZipCode = Request.Form["TxtZipCode"];
            info.UserBase.HoneyName = "";
            info.UserBase.CardType = "";
            info.UserBase.Province = Request["selprovince"];
            info.UserBase.City = Request["selcity"];
            info.UserBase.County = Request["selcoutry"];
            if (!string.IsNullOrEmpty(RegisterMobile)) { info.UserBase.Mobile = RegisterMobile; RegisterMobile = null; }
            //-----------------------------------------------------
            #region 信息检测
            string err = "";
            if (SiteConfig.UserConfig.EnableCheckCodeOfReg)
            {
                if (!ZoomlaSecurityCenter.VCodeCheck(Request.Form["VCode_hid"], Request.Form["VCode"]))
                {
                    function.WriteErrMsg("您输入的验证码和系统产生的不一致，请重新输入", "javascript:history.go(-1);");return null;
                }
            }
            if (!CheckUserName(info.UserName, ref err)) { function.WriteErrMsg(err); return null; }
            else if (!CheckUserInfo(info.UserBase)) { return null; }
            else if (string.IsNullOrEmpty(info.Answer)) { function.WriteErrMsg("问题答案不能为空！"); return null; }
            else if (!CheckEmail(info.Email, ref err)) { function.WriteErrMsg(err); return null; }
            #endregion
            //推荐人处理
            M_User_InviteCode utMod = null;
            {
                //支持使用用户名和用户ID
                info.ParentUserID = CheckParentUser(Request.Form["TxtParentUser"]).UserID.ToString();
                //邀请码推荐用户,高于填写的推荐人
                string inviteCode = Request.Form["TxtInvitCode"];
                if (!string.IsNullOrEmpty(inviteCode))
                {
                    utMod = utBll.Code_SelModel(inviteCode);
                    if (utMod != null)
                    {
                        if (utMod.ZStatus != 0) { function.WriteErrMsg("该邀请码已被使用"); return null; }
                        info.ParentUserID = utMod.UserID.ToString();
                        info.GroupID = utMod.JoinGroup;
                    }
                }
                //计算深度
                if (DataConvert.CLng(info.ParentUserID) > 0)//从1开始
                {
                    info.RoomID = (buser.SelParentTree(DataConvert.CLng(info.ParentUserID)).Split(',').Length + 1);
                }
            }
            //用于初始状态
            if (SiteConfig.UserConfig.EmailCheckReg) { info.Status = 4; }//邮件认证
            else if (SiteConfig.UserConfig.AdminCheckReg) { info.Status = 2; } //管理员认证
            else if (SiteConfig.UserConfig.EmailCheckReg && SiteConfig.UserConfig.AdminCheckReg) { info.Status = 3; } //邮件认证及管理员认证
            else if (!SiteConfig.UserConfig.UserValidateType) { info.Status = 5; }
            else { info.Status = 0; }
            //自定义字段信息
            DataTable table;
            try
            {
                table = new Call().GetDTFromMVC(bmf.Select_All(), Request);
            }
            catch (Exception e)
            {
                function.WriteErrMsg(e.Message); return Content("");
            }
            string[] strArray2 = string.IsNullOrEmpty(SiteConfig.UserConfig.RegFieldsMustFill) ? new string[0] : SiteConfig.UserConfig.RegFieldsMustFill.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string str2 in strArray2)
            {
                if (Request.Form["txt_" + str2] == null || Request.Form["txt_" + str2] == "")
                {
                    DataTable tbles = bmf.SelByFieldName(str2);
                }
            }
            //------------添加新用户
         
            info.UserID = buser.AddModel(info);
            info.UserBase.UserId = info.UserID;
            buser.AddBase(info.UserBase);
            if (table != null && table.Rows.Count > 0) { buser.UpdateUserFile(info.UserID, table); }
            buser.SetLoginState(info);
            if (info.UserID < 1) { function.WriteErrMsg("注册失败");return null; }
            //邀请码状态变更
            if (utMod != null) { utBll.Code_Used(utMod.ID, info); }
            //----------------------------------------------------------------------------
            string ReturnUrl = Request.Form["ReturnUrl_Hid"];
            string RegMessage = "";
            string RegRUrl = "";
            bool isok = false;
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
            #region 自定义模型
            int ModelID = DataConverter.CLng(gpBll.GetGroupModel(info.GroupID));
            string usertablename = modBll.GetModelById(ModelID).TableName;
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
                                row2[2] = Request.Form["txt_" + sizefield];
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
                                if (string.IsNullOrEmpty(Request.Form["txt_" + sizefield]))
                                {
                                    function.WriteErrMsg(dr["FieldAlias"].ToString() + "的缩略图不能为空!");
                                }
                                DataRow row1 = tablereg.NewRow();
                                row1[0] = sizefield;
                                row1[1] = "ThumbField";
                                row1[2] = Request.Form["txt_" + sizefield];
                                tablereg.Rows.Add(row1);
                            }
                        }

                        DataRow row = tablereg.NewRow();
                        row[0] = dr["FieldName"].ToString();
                        string ftype = dr["FieldType"].ToString();
                        row[1] = ftype;
                        string fvalue = Request.Form["txt_" + dr["FieldName"].ToString()];
                        if (ftype == "TextType" || ftype == "MultipleTextType" || ftype == "MultipleHtmlType")
                        {
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
                string regurl = siteurls + "/User/RegisterCheck?UserName=" + HttpUtility.UrlEncode(info.UserName) + "&CheckNum=" + info.CheckNum;
                string mailcontent = mailBll.SelByType(B_MailManage.MailType.NewUserReg);
                mailInfo.MailBody = new OrderCommon().TlpDeal(mailcontent, GetRegEmailDt(info.UserName, info.CheckNum, regurl));
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
                        string regurl = siteurls + "/User/RegisterCheck?UserName=" + HttpUtility.UrlEncode(info.UserName) + "&CheckNum=" + info.CheckNum;
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
                            RegRUrl = "/User/Index";
                        }
                        else
                        {
                            RegMessage = RegMessage + "<a href=\"/\">返回首页</a>&nbsp;&nbsp;<a href=\"" + ReturnUrl + "\">进入默认页面</a>,5秒后系统自动跳转到默认页面!";
                            RegRUrl = ReturnUrl;
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
                        RegRUrl = "default";
                    }
                    else
                    {
                        RegMessage = RegMessage + "<a href=\"/\">返回首页</a>&nbsp;&nbsp;<a href=\"" + ReturnUrl + "\">进入默认页面</a>,5秒后系统自动跳转到默认页面!";
                        RegRUrl = ReturnUrl;
                    }
                    isok = true;
                    break;
            }
            if (SiteConfig.UserConfig.EmailCheckReg)
            {
                RegMessage = "<div class='emptyDiv'><br/>注册成功! &nbsp;&nbsp;<a href='http://mail." + info.Email.Substring(info.Email.LastIndexOf('@') + 1) + "' target='_blank'>立即登录邮箱进行验证>></a><br/></div>";
                isok = false;
            }
            ViewBag.RegMessage = RegMessage;
            ViewBag.RegRUrl = RegRUrl;
            ViewBag.isok = isok;//为true则自动跳转
            ViewBag.pwd = Request.Form["TxtPassword"];
            return View("Register_Finish", info);
        }
        //替换userapp字符串
        private string ReplaceData(string value, DataRow dr)
        {
            string[] replce = "ico,fileUrl,name".Split(',');
            foreach (string item in replce)
            {
                string temptext = dr[item].ToString();
                if (item.Equals("ico"))
                {//图标替换
                    temptext = StringHelper.GetItemIcon(temptext, "width:50px;height:50px;");

                }
                value = value.Replace("@" + item, temptext);
            }
            return value;
        }
        private string GetLinkTarget(string target)
        {
            switch (target)
            {
                case "1":
                    return "_blank";
                default:
                    return "_self";
            }
        }
        #region Register Logical
        //检测会员名是否有效
        private bool CheckUserName(string uname,ref string err)
        {
            if (string.IsNullOrEmpty(uname)) { return false; }
            uname = uname.Replace(" ","");
            if (SiteConfig.UserConfig.UserNameLimit > uname.Length || uname.Length > SiteConfig.UserConfig.UserNameMax)
            {
                err = "用户名的长度必须小于" + SiteConfig.UserConfig.UserNameMax + "，并大于" + SiteConfig.UserConfig.UserNameLimit + "!"; return false;
            }
            else if (ZoomlaSecurityCenter.CheckData(uname))
            {
                err = "用户名不能包含特殊字段!"; return false;
            }
            else if (!SafeSC.CheckUName(uname))
            {
                err = "用户名不能包含特殊字符!"; return false;
            }
            if (StringHelper.FoundInArr(SiteConfig.UserConfig.UserNameRegDisabled, uname, "|"))
            {
                err = "该用户名禁止注册，请输入不同的用户名!"; return false;
            }
            if (buser.IsExistUName(uname))
            {
                err = "该用户名已被他人占用，请输入不同的用户名"; return false;
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
                                resultString = Regex.Match(uname, @"[0-9]*").Value;
                            }
                            catch (ArgumentException)
                            {
                            }
                            if (uname == resultString.Trim())
                            {
                                err = "用户名不允许纯数字"; return false;
                            }
                        }

                        if (rulearr[ii].ToString() == "2")
                        {
                            string resultString = null;
                            try
                            {
                                resultString = Regex.Match(uname, @"[a-zA-Z]*").Value;
                            }
                            catch (ArgumentException)
                            {
                            }

                            if (uname == resultString)
                            {
                                err = "用户名不允许纯英文"; return false;
                            }

                        }

                        if (rulearr[ii].ToString() == "3")
                        {
                            bool foundMatch = false;
                            try
                            {
                                foundMatch = Regex.IsMatch(uname, @"[\u0391-\uFFE5]$");
                            }
                            catch (ArgumentException)
                            {
                            }

                            if (foundMatch)
                            {
                                err = "用户名不允许带有中文"; return false;
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
                            resultString = Regex.Match(uname, @"[0-9]*").Value;
                        }
                        catch (ArgumentException)
                        {
                        }
                        if (uname == resultString.Trim())
                        {
                            err = "用户名不允许纯数字"; return false;
                        }
                    }
                    if (userregrule.ToString() == "2")
                    {
                        string resultString = null;
                        try
                        {
                            resultString = Regex.Match(uname, @"[a-zA-Z]*").Value;
                        }
                        catch (ArgumentException)
                        {
                        }

                        if (uname == resultString)
                        {
                            err = "用户名不允许纯英文"; return false;
                        }
                    }
                    if (userregrule.ToString() == "3")
                    {
                        bool foundMatch = false;
                        try
                        {
                            foundMatch = Regex.IsMatch(uname, @"[\u0391-\uFFE5]$");
                        }
                        catch (ArgumentException)
                        {
                        }

                        if (foundMatch)
                        {
                            err = "用户名不允许带有中文"; return false;
                        }
                    }
                }
            }
            return true;
        }
        //用户信息验证,身份证号,生日等
        private bool CheckUserInfo(M_Uinfo basemu)
        {
            if (string.IsNullOrEmpty(basemu.IDCard)) { return true; }
            if (buser.IsExitcard(basemu.IDCard)) { function.WriteErrMsg("该身份证号已被注册，请输入不同的身份证号！"); return false; }
            bool foundMatch = false;
            try
            {
                foundMatch = Regex.IsMatch(basemu.IDCard, @"^\d{17}([0-9]|X)$");
                if (foundMatch)
                {
                    string birth = basemu.IDCard.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                    DateTime time = new DateTime();
                    DateTime newDate = DateTime.Now.AddYears(-120);
                    DateTime now = DateTime.Now;
                    if (DateTime.TryParse(birth, out time) == false)
                    {
                        function.WriteErrMsg("该身份证生日不正确！"); return false;
                    }
                    else
                    {
                        DateTime data1 = Convert.ToDateTime(birth);
                        TimeSpan ts = newDate - data1;
                        TimeSpan ts2 = data1 - now;
                        if (ts.Days > 0) { function.WriteErrMsg("您超过了120岁？请核对身份证号。"); return false; }
                        else if (ts2.Days > 0) { function.WriteErrMsg("您还没出生吧？请核对身份证号。"); return false; }
                    }
                }
                else
                {
                    function.WriteErrMsg("该身份证格式不正确！"); return false;
                }
            }
            catch (ArgumentException)
            {
                return true;
            }
            return true;
        }
        //推荐人是否存在支持ID与用户名(暂也支持推荐码)
        private M_UserInfo CheckParentUser(string puname)
        {
            M_UserInfo pmu = new M_UserInfo(true);
            if (string.IsNullOrEmpty(puname)) { return pmu; }
            int puid = DataConvert.CLng(puname);
            if (puid >= 100001)//只有9级,所以只有首位需去除[delete]
            {
                int depth = Convert.ToInt32(puname.Substring(0, 1));
                int uid = Convert.ToInt32(puname.Substring(1, (puname.Length - 1)));
                pmu = buser.SelReturnModel(uid);
                if (pmu.RoomID == 0) { pmu.RoomID = 1; }
                if (pmu.RoomID != depth) { return new M_UserInfo(true); }
            }
            else if (puid > 0)//100001
            {
                pmu = buser.SelReturnModel(puid);
            }
            else
            {
                pmu = buser.GetUserByName(puname);
            }
            return pmu;
        }
        private bool CheckEmail(string email, ref string err)
        {
            if (string.IsNullOrEmpty(email)) { err = "邮箱不能为空"; return false; }
            if (!RegexHelper.IsEmail(email)) { err = "邮箱格式不正确"; return false; }
            if (buser.IsExist("ume", email)) { err = "该邮箱已存在"; return false; }
            return true;
        }
         //邀请码校验
        private bool CheckInvitCode(string code, ref string err)
        {
            if (string.IsNullOrEmpty(code)) { err = "邀请码为空"; return false; }
            M_User_InviteCode inviteMod = utBll.Code_SelModel(code);
            if (inviteMod == null) { err = "邀请码不存在"; return false; }
            if (inviteMod.ZStatus != 0) { err = "邀请码已被使用过"; return false; }
            return true;
        }
         //检测出生日期是否合逻辑
        private bool CheckBirthDay(string value,ref string err)
        {
            DateTime time = DateTime.Now;
            if (string.IsNullOrEmpty(value)) { err = "日期格式为空"; return false; }
            else if (!DateTime.TryParse(value, out time)) { err = "不是有效的日期格式"; return false; }
            else if (time < DateTime.Now.AddYears(-150)) { err = "您超过了150岁?-吉尼斯世界纪录最长寿的人是132岁!"; return false; }
            else if (time > DateTime.Now) { err = "日期大于当前时间"; return false; }
            return true;
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
        #endregion
    }
}
