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
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.BLL;
using ZoomLa.BLL.User;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;
using ZoomLa.API;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using ZoomLa.SQLDAL;

namespace ZoomLaCMS.Manage.User
{
    public partial class UserModify :CustomerPageAction
    {
        B_User bll = new B_User();
        B_UserBaseField bub = new B_UserBaseField();
        B_Sensitivity sll = new B_Sensitivity();
        B_Model bm = new B_Model();
        B_ModelField bmf = new B_ModelField();
        B_User_Plat PlatBll = new B_User_Plat();
        B_Permission perBll = new B_Permission();
        public string tabTitles = "";
        public string tabs = "";
        public int UserID { get { return DataConvert.CLng(Request.QueryString["UserID"]); } }
        protected void Page_Init()
        {
            //RegularExpressionValidator2.ValidationExpression = RegexHelper.S_Mobile;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            B_Admin badmin = new B_Admin();
            if (!IsPostBack)
            {
                ShowPlatInfo(Convert.ToInt32(UserID));
                ViewState["PageUrl"] = customPath2 + "User/Userinfo.aspx?id=" + UserID;
                ViewState["url"] = "/Manage/User/UserManage.aspx";
                if (Request.QueryString["type"] == "x")
                {
                    ViewState["PageUrl"] = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
                    ViewState["url"] = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();
                }
                if (UserID < 1) { function.WriteErrMsg("请指定会员ID", ViewState["PageUrl"].ToString()); }
                M_UserInfo uinfo = bll.SeachByID(DataConverter.CLng(UserID));
                if (uinfo.IsNull)
                {
                    function.WriteErrMsg("指定ID：" + UserID + "的用户不存在！可能已被删除！", ViewState["url"].ToString());
                }
                B_Group bgr = new B_Group();
                DDLGroup.DataSource = bgr.GetGroupList();
                DDLGroup.DataTextField = "GroupName";
                DDLGroup.DataValueField = "GroupID";
                DDLGroup.DataBind();

                lblUser.Text = uinfo.UserName;
                tbEmail.Text = uinfo.Email;
                OldEmail_Hid.Value = uinfo.Email;
                txtPurm.Text = uinfo.DummyPurse.ToString();
                tbQuestion.Text = uinfo.Question;
                tbAnswer.Text = uinfo.Answer;
                M_Uinfo binfo = bll.GetUserBaseByuserid(UserID);
                tbTrueName.Text = uinfo.TrueName;
                tbUserSex.SelectedValue = binfo.UserSex ? "1" : "0";
                tbBirthday.Text = binfo.BirthDay;
                tbOfficePhone.Text = binfo.OfficePhone;
                tbHomePhone.Text = binfo.HomePhone;
                tbMobile.Text = binfo.Mobile;
                tbPHS.Text = binfo.PHS;
                tbFax.Text = binfo.Fax;
                tbAddress.Text = binfo.Address;
                tbZipCode.Text = binfo.ZipCode;
                txtHoneyName.Text = uinfo.HoneyName;
                tbIDCard.Text = binfo.IDCard;
                tbQQ.Text = binfo.QQ;
                tbHomepage.Text = binfo.HomePage;
                tbICQ.Text = binfo.ICQ;
                tbMSN.Text = binfo.MSN;
                tbYahoo.Text = binfo.Yahoo;
                tbUC.Text = binfo.UC;
                tbUserFace.Text = uinfo.UserFace;
                tbFaceWidth.Text = binfo.FaceWidth.ToString();
                tbFaceHeight.Text = binfo.FaceHeight.ToString();
                tbSign.Text = binfo.Sign;
                tbPrivacy.SelectedValue = binfo.Privating.ToString();
                DDLGroup.SelectedValue = uinfo.GroupID.ToString();
                txtUserCrite.Text = uinfo.UserCreit.ToString();
                txtMoney.Text = uinfo.Purse.ToString();
                txtPoint.Text = uinfo.UserExp.ToString();
                txtSilverCoin.Text = uinfo.SilverCoin.ToString();
                txtUserPoint.Text = uinfo.UserPoint.ToString();//点券
                txtboffExp.Text = uinfo.boffExp.ToString();//卖家积分
                txtConsumeExp.Text = uinfo.ConsumeExp.ToString();//消费积分
                txtDeadLine.Text = uinfo.DeadLine.ToString();//有效期截止时间
                txtCerificateDeadLine.Text = uinfo.CerificateDeadLine.ToString();
                VIPUser.SelectedValue = binfo.VIPUser.ToString();
                Adress_Hid.Value = binfo.Province + "," + binfo.City + "," + binfo.County;
                tbParentUserID.Text = uinfo.ParentUserID.ToString();
                CompanyNameT.Text = uinfo.CompanyName;
                CompanyDescribeT.Text = uinfo.CompanyDescribe;
                WorkNum_T.Text = uinfo.WorkNum;
                DataTable dtuser = bll.GetUserBaseByuserid(UserID.ToString());
                lblHtml.Text = bub.GetUpdateHtml(dtuser);
                BindUserRole(uinfo);
                Call.SetBreadCrumb(Master, "<li><a href='" + CustomerPageAction.customPath2 + "Main.aspx'>工作台</a></li><li><a href='" + customPath2 + "User/UserManage.aspx'>会员管理</a></li><li class='active'><a href='" + Request.RawUrl + "'>修改会员信息</a> 当前用户:" + uinfo.UserName + "</li>");
            }
        }
        //绑定用户角色
        void BindUserRole(M_UserInfo mu)
        {
            DataTable dt = perBll.Sel();
            foreach (DataRow item in dt.Rows)
            {
                ListItem li = new ListItem(item["RoleName"].ToString(), item["ID"].ToString());
                if (mu.UserRole.Contains("," + item["ID"] + ","))
                    li.Selected = true;
                cblRoleList.Items.Add(li);
            }
        }
        void ShowPlatInfo(int id)
        {
            M_User_Plat upMod = PlatBll.SelReturnModel(id);
            if (upMod != null)
            {
                DataTable compdt = new B_Plat_Comp().Sel();
                platInfo_A.Visible = true;
                tbTrueName_T.Text = upMod.TrueName;
                tbCompName_D.DataSource = compdt;
                tbCompName_D.DataBind();
                tbCompName_D.Items.Insert(0, new ListItem() { Text = "未指定公司", Value = "0" });
                tbCompName_D.SelectedValue = upMod.CompID.ToString();
                tbPost_T.Text = upMod.GroupName;
                //tbPhone_T.Text = platModel.Mobile;
            }
        }
        string GetUserRole()
        {
            string values = "";
            foreach (ListItem item in cblRoleList.Items)
            {
                if (item.Selected)
                    values += "," + item.Value + ",";
            }
            return values;
        }
        //保存
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            if (tbConPwd.Text.Trim().Equals(tbNewPwd.Text.Trim()))
            {
                if (!string.IsNullOrEmpty(WorkNum_T.Text) && !bll.CheckWorkNumIsOnly(WorkNum_T.Text, UserID))
                    function.WriteErrMsg("工号重复请重新输入！！");
                DataTable dt = bub.Select_All();//获取你额外添加的字段。
                Call commonCall = new Call();
                DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState);
                M_UserInfo info = bll.SeachByID(UserID);
                if (!string.IsNullOrEmpty(tbNewPwd.Text.Trim()))
                {
                    info.UserPwd = tbNewPwd.Text.Trim();
                }
                if (!info.Email.Equals(tbEmail.Text.Trim()))
                {
                    M_UserInfo mu = bll.GetSelectByEmail(tbEmail.Text.Trim());
                    if (mu.UserID > 0) { function.WriteErrMsg("您所填的邮箱已被注册!"); }
                }

                info.ParentUserID = DataConvert.CLng(tbParentUserID.Text.Replace(" ", "")).ToString();
                if (info.ParentUserID.Equals(info.UserID.ToString())) { function.WriteErrMsg("推荐人不能是用户本身"); }

                info.Email = tbEmail.Text.Trim();
                info.GroupID = DataConverter.CLng(DDLGroup.SelectedValue);
                info.boffExp = DataConverter.CLng(txtboffExp.Text);//卖家积分
                info.ConsumeExp = DataConverter.CLng(txtConsumeExp.Text);//消费积分
                info.DeadLine = DataConverter.CDate(txtDeadLine.Text);//有效期截止时间
                info.CerificateDeadLine = DataConverter.CDate(txtCerificateDeadLine.Text);
                info.ParentUserID = DataConvert.CLng(tbParentUserID.Text.Replace(" ", "")).ToString();
                if (info.ParentUserID.Equals(info.UserID.ToString())) { function.WriteErrMsg("推荐人不能是用户本身"); }
                info.CompanyName = CompanyNameT.Text;
                info.CompanyDescribe = CompanyDescribeT.Text;
                info.WorkNum = WorkNum_T.Text.Trim().ToString();
                info.UserFace = tbUserFace.Text.Trim();
                info.HoneyName = txtHoneyName.Text.Trim();
                info.UserRole = GetUserRole();
                info.Question = tbQuestion.Text;
                info.Answer = tbAnswer.Text.Trim();
                M_Uinfo binfo = bll.GetUserBaseByuserid(UserID);
                info.TrueName = tbTrueName.Text.Trim();
                binfo.UserSex = DataConverter.CBool(tbUserSex.SelectedValue);
                binfo.BirthDay = tbBirthday.Text.Trim();
                binfo.OfficePhone = tbOfficePhone.Text.Trim();
                binfo.HomePhone = tbHomePhone.Text.Trim();
                binfo.Mobile = tbMobile.Text.Trim();
                binfo.PHS = tbPHS.Text.Trim();
                binfo.Fax = tbFax.Text.Trim();
                binfo.Address = tbAddress.Text.Trim();
                binfo.ZipCode = tbZipCode.Text.Trim();
                binfo.IDCard = tbIDCard.Text.Trim();
                binfo.HomePage = tbHomepage.Text.Trim();
                binfo.QQ = tbQQ.Text.Trim();
                binfo.ICQ = tbICQ.Text.Trim();
                binfo.MSN = tbMSN.Text.Trim();
                binfo.Yahoo = tbYahoo.Text.Trim();
                //binfo.HoneyName = txtHoneyName.Text.Trim();
                binfo.UC = tbUC.Text.Trim();
                binfo.UserFace = info.UserFace;
                binfo.FaceHeight = DataConverter.CLng(tbFaceHeight.Text.Trim());
                binfo.FaceWidth = DataConverter.CLng(tbFaceWidth.Text.Trim());
                binfo.Sign = tbSign.Text.Trim();
                binfo.Province = Request.Form["selprovince"];
                binfo.City = Request.Form["selcity"];
                binfo.County = Request.Form["selcoutry"];
                binfo.Privating = tbPrivacy.SelectedIndex;
                binfo.VIPUser = DataConverter.CLng(VIPUser.SelectedValue);
                #region
                DataTable dtModelUser = bm.GetListUser();
                for (int i = 0; i < dtModelUser.Rows.Count; i++)
                {
                    string tablename = dtModelUser.Rows[i]["TableName"].ToString();
                    DataTable modelinfo = bmf.SelectTableName(tablename, "userid=" + UserID);
                    if (modelinfo != null && modelinfo.Rows.Count > 0)
                    {

                        int modelid = Convert.ToInt32(dtModelUser.Rows[i]["ModelId"].ToString());
                        DataTable groupset = bmf.GetModelFieldListall(modelid);
                        DataTable tablereg = new DataTable();
                        tablereg.Columns.Add(new DataColumn("FieldName", typeof(string)));
                        tablereg.Columns.Add(new DataColumn("FieldType", typeof(string)));
                        tablereg.Columns.Add(new DataColumn("FieldValue", typeof(string)));
                        if (groupset != null && groupset.Rows.Count > 0)
                        {
                            foreach (DataRow dr in groupset.Rows)
                            {
                                if (DataConverter.CBool(dr["IsNotNull"].ToString()))
                                {
                                    if (string.IsNullOrEmpty(Page.Request.Form["txt_" + dr["FieldName"].ToString()]))
                                    {
                                        function.WriteErrMsg(dr["FieldAlias"].ToString() + "不能为空!");
                                    }
                                }

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
                                }
                                row[2] = fvalue;
                                tablereg.Rows.Add(row);
                            }
                        }
                        bll.UpdateModelInfo(tablereg, tablename, DataConverter.CLng(modelinfo.Rows[0]["id"]));
                    }
                }

                #endregion

                if (!string.IsNullOrEmpty(tbNewPwd.Text.Trim()))
                    info.UserPwd = StringHelper.MD5(tbNewPwd.Text.Trim());
                bll.UpDateUser(info);
                bll.UpdateBase(binfo);
                if (table.Rows.Count > 0)
                {
                    bll.UpdateUserFile(binfo.UserId, table);
                }
                EditPlatInfo(UserID);
                function.WriteSuccessMsg("修改成功！", ViewState["PageUrl"].ToString());
            }
        }
        private void EditPlatInfo(int UserID)
        {
            M_User_Plat platmodel = PlatBll.SelReturnModel(UserID);
            M_UserInfo mu = bll.SelReturnModel(UserID);
            if (platmodel != null)
            {
                mu.HoneyName = tbTrueName_T.Text;
                platmodel.CompID = Convert.ToInt32(tbCompName_D.SelectedValue);
                platmodel.Post = tbPost_T.Text;
                platmodel.Mobile = tbMobile.Text;
                platmodel.Status = 1;
                PlatBll.UpdateByID(platmodel);
                bll.UpdateByID(mu);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(ViewState["PageUrl"].ToString());
        }
    }
}