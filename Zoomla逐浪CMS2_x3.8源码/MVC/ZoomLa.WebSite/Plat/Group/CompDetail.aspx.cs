using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZoomLa.BLL.Plat;
using ZoomLa.Model.Plat;
using System.Data;
using System.Data.SqlClient;
using ZoomLa.Common;
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.IO;

namespace ZoomLaCMS.Plat.Group
{
    public partial class CompDetail : System.Web.UI.Page
    {
        B_Plat_Comp compBll = new B_Plat_Comp();
        B_User_Plat upBll = new B_User_Plat();
        B_Common_UserApply ualyBll = new B_Common_UserApply();
        M_User_Plat upMod = new M_User_Plat();
        M_Plat_Comp compMod = new M_Plat_Comp();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!B_User_Plat.IsAdmin())
                {
                    view_logo_tr.Visible = true;
                    CompShort_T.Attributes.Add("readonly", "false");
                    CompName_T.Attributes.Add("readonly", "false");
                    CompDesc_T.Attributes.Add("readonly", "false");
                    CompStatus_T.Attributes.Add("readonly", "false");
                    Telephone_T.Attributes.Add("readonly", "false");
                    Mobile_T.Attributes.Add("readonly", "false");
                    CompHref_T.Attributes.Add("readonly", "false");
                }
                else
                {
                    admin_logo_tr.Visible = true;
                    adminop_tr.Visible = true;
                }
                MyBind();
            }
        }
        private void MyBind()
        {
            upMod = B_User_Plat.GetLogin();
            compMod = compBll.SelReturnModel(upMod.CompID);
            if (compMod.Status == 0)
            {
                //检测是否已提交企业申请或加入公司申请
                if (ualyBll.JoinComp_Exist(upMod.UserID)) { person_div.InnerHtml = "<div>您已提交加入企业申请,请等待公司管理员审核！</div>"; }
                if (ualyBll.CompCert_Exist(upMod.UserID)) { person_div.InnerHtml = "<div>您已提交企业认证申请,请等待管理员审核。</div>"; }
                person_div.Visible = true;
            }
            else
            {
                compinfo_div.Visible = true;
                upMod = upBll.SelReturnModel(compMod.CreateUser);
                if (upMod == null) upMod = new M_User_Plat();
                CompShort_T.Text = compMod.CompShort;
                CompName_T.Text = compMod.CompName;
                CompDesc_T.Text = compMod.CompDesc;
                CompStatus_T.Text = "正常";// compMod.Status==1?"正常":"关闭";
                CompUser_T.Text = upMod.UserName;
                CompHref_T.Text = compMod.CompHref;
                CreateTime_T.Text = compMod.CreateTime.ToString("yyyy年MM月dd日");
                SFiles_Up.FVPath = compMod.CompLogo;
                Logo_Img.ImageUrl = compMod.CompLogo;
                Telephone_T.Text = compMod.Telephone;
                Mobile_T.Text = compMod.Mobile;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int CompID = B_User_Plat.GetLogin().CompID;
            compMod = compBll.SelReturnModel(CompID);
            compMod.CompName = CompName_T.Text;
            compMod.CompDesc = CompDesc_T.Text;
            //compMod.Status = Convert.ToInt32(CompStatus_T.Text);
            compMod.CompLogo = SaveFile();
            compMod.CompHref = CompHref_T.Text;
            compMod.CompShort = CompShort_T.Text;
            compMod.Telephone = Telephone_T.Text;
            compMod.Mobile = Mobile_T.Text;
            if (compBll.UpdateByID(compMod))
            {
                Response.Redirect(Request.RawUrl);
            }
            else
            {
                function.Script(this, "alert('修改失败!!');");
            }
        }
        public string SaveFile()
        {
            string vpath = B_Plat_Common.GetDirPath(B_Plat_Common.SaveType.Company_P);
            SFiles_Up.SaveUrl = vpath;
            return SFiles_Up.SaveFile();
        }
    }
}