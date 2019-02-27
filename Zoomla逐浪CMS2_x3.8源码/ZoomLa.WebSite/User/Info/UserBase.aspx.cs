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
using ZoomLa.Common;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.SQLDAL;
using ThoughtWorks.QRCode.Codec;
using System.Drawing;
using System.IO;

public partial class User_Info_UserBase : CustomerPageAction
{
        
    B_User buser = new B_User();
    B_Group gpBll = new B_Group();
    B_UserBaseField ubfbll = new B_UserBaseField();
    B_ModelField fieldBll = new B_ModelField();
    B_PointGrounp pointBll = new B_PointGrounp();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            switchID.Value = HttpUtility.HtmlEncode(Request.QueryString["sel"]);
            //if (SiteConfig.UserConfig.UserMobilAuth.Equals("0"))
            //{
            //    Mobile_L.Enabled = false;
            //}
            M_UserInfo mu = buser.GetLogin();
            UserFace_Hid.Value = Path.GetFileName(mu.UserFace);
            UserFace_T.Text = mu.UserFace;
            CompanyName.Text = mu.CompanyName;
            UserFace_Img.ImageUrl = mu.UserFace;
            SFile_Up.FileUrl = mu.UserFace;
            //CompanyGroup是否为企业用户组0-否 1-是
            //if (gpBll.GetByID(Convert.ToInt32(mu.GroupID)).CompanyGroup == 1)
            //{
            //    DivCompany.Visible = true;
            //    txtCompanyName.Text = mu.CompanyName;
            //    txtCompanyDescribe.Text = mu.CompanyDescribe;
            //}
            M_Uinfo binfo = buser.GetUserBaseByuserid(mu.UserID);
            tbTrueName.Text = mu.TrueName;
            txtHonName.Text = mu.HoneyName;
            if (!binfo.IsNull)
            {
                tbUserSex.SelectedValue = binfo.UserSex ? "1" : "0";
                address.Value = binfo.Province + "," + binfo.City + "," + binfo.County;
                tbAddress.Text = binfo.Address;
                tbBirthday.Text = binfo.BirthDay;
                tbFaceWidth.Text = binfo.FaceWidth.ToString();
                tbFaceHeight.Text = binfo.FaceHeight.ToString();
                tbFax.Text = binfo.Fax;
                tbHomepage.Text = binfo.HomePage;
                tbHomePhone.Text = binfo.HomePhone;
                tbIDCard.Text = binfo.IDCard;
                Mobile_L.Text = binfo.Mobile;
                Email_L.Text = mu.Email;
                tbOfficePhone.Text = binfo.OfficePhone;
                tbPrivacy.SelectedValue = binfo.Privating.ToString();
                tbQQ.Text = binfo.QQ;
                tbSign.Text = binfo.Sign;
                tbUC.Text = binfo.UC;
                tbZipCode.Text = binfo.ZipCode;
                //公司信息与职务
                Position.Text = binfo.Position;
            }
            lblHtml.Text = fieldBll.InputallHtml(0, 0, new ModelConfig()
            {
                Fields = SiteConfig.UserConfig.RegFieldsMustFill + "," + SiteConfig.UserConfig.RegFieldsSelectFill,
                Source = ModelConfig.SType.UserRegister,
                ValueDT = buser.GetUserBaseByuserid(mu.UserID.ToString())
            });
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DataTable dt = ubfbll.Select_All();
        Call commonCall = new Call();
        DataTable table = commonCall.GetDTFromPage(dt, Page, ViewState);
        M_UserInfo uinfo = buser.GetLogin();
        uinfo.UserFace = UserFace_T.Text.Trim();
        uinfo.HoneyName = Server.HtmlEncode(txtHonName.Text.Trim());
        uinfo.CompanyName = Server.HtmlEncode(CompanyName.Text.Trim());
        M_Uinfo binfo = buser.GetUserBaseByuserid(uinfo.UserID);
        binfo.Address = Server.HtmlEncode(tbAddress.Text.Trim());
        binfo.BirthDay = Server.HtmlEncode(tbBirthday.Text.Trim());
        binfo.FaceHeight = DataConverter.CLng(tbFaceHeight.Text.Trim());
        binfo.FaceWidth = DataConverter.CLng(tbFaceWidth.Text.Trim());
        binfo.UserFace = uinfo.UserFace;
        binfo.Fax = tbFax.Text.Trim();
        binfo.HomePage = Server.HtmlEncode(tbHomepage.Text.Trim());
        //binfo.ICQ = Server.HtmlEncode(tbICQ.Text.Trim());
        binfo.HomePhone = Server.HtmlEncode(tbHomePhone.Text.Trim());
        binfo.IDCard = Server.HtmlEncode(tbIDCard.Text.Trim());
        //binfo.Mobile = Server.HtmlEncode(tbMobile.Text.Trim());
        binfo.OfficePhone = Server.HtmlEncode(tbOfficePhone.Text.Trim());
        binfo.Privating = tbPrivacy.SelectedIndex;
        //binfo.PHS = Server.HtmlEncode(tbPHS.Text.Trim());
        binfo.QQ = Server.HtmlEncode(tbQQ.Text.Trim());
        binfo.Sign = Server.HtmlEncode(tbSign.Text.Trim());
        uinfo.TrueName = Server.HtmlEncode(tbTrueName.Text.Trim());
        binfo.UC = Server.HtmlEncode(tbUC.Text.Trim());
        binfo.UserSex = DataConverter.CBool(tbUserSex.SelectedValue);
        //binfo.Yahoo = Server.HtmlEncode(tbYahoo.Text.Trim());
        binfo.ZipCode = Server.HtmlEncode(tbZipCode.Text.Trim());
        binfo.HoneyName = uinfo.HoneyName;
        string[] adrestr = address.Value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        binfo.Province = adrestr[0];
        binfo.City = adrestr[1];
        binfo.County = adrestr[2];
        binfo.Position = Server.HtmlEncode(Position.Text.Trim());
        if (binfo.IsNull)
        {
            binfo.UserId = uinfo.UserID;
            buser.UpDateUser(uinfo);
            buser.AddBase(binfo);
        }
        else
        {
            uinfo.CompanyName = Server.HtmlEncode(txtCompanyName.Text);
            uinfo.CompanyDescribe = Server.HtmlEncode(txtCompanyDescribe.Text);
            buser.UpDateUser(uinfo);
            buser.UpdateBase(binfo);//更新用户信息 
        }
        if (table.Rows.Count > 0)
        {
            buser.UpdateUserFile(binfo.UserId, table);
        }
        Response.Redirect(Request.RawUrl);
    }
    protected void BtUpImage_Btn_Click(object sender, EventArgs e)
    {
        M_UserInfo mu = buser.GetLogin();
        string vpath = SiteConfig.SiteOption.UploadDir + "User/" + mu.UserName + mu.UserID + "/";
        SFile_Up.SaveUrl = vpath;
        string imgurl = SFile_Up.SaveFile();
        UserFace_Img.ImageUrl = imgurl;
        UserFace_Hid.Value = Path.GetFileName(imgurl);
        UserFace_T.Text = imgurl;
        SFile_Up.FVPath = imgurl;
    }
}