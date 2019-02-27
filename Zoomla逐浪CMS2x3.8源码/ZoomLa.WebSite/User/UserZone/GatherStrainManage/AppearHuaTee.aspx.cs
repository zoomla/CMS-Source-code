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
using BDUModel;
using BDUBLL;
using ZoomLa.Components;
using System.Globalization;
using ZoomLa.BLL;

namespace ZoomLa.GatherStrainManage
{
    public partial class AppearHuaTee : Page
    {
        #region 业务对象 
        B_User ubll = new B_User();
        public  Guid GSID
        {
            get
            {
                if (ViewState["GSID"] != null)
                {
                    return new Guid(ViewState["GSID"].ToString());
                }
                else return Guid.Empty;
            }
            set
            {
                ViewState["GSID"] = value;
            }
        }

        public Guid HTID
        {
            get
            {
                if (ViewState["HTID"] != null)
                {
                    return new Guid(ViewState["HTID"].ToString());
                }
                else return Guid.Empty;
            }
            set
            {
                ViewState["HTID"] = value;
            }
        }

        public bool IsNew
        {
            get
            {
                if (ViewState["IsNew"] != null)
                {
                    return bool.Parse(ViewState["IsNew"].ToString());
                }
                else return true;
            }
            set
            {
                ViewState["IsNew"] = value;
            }
        }

        //GSManageBLL gsbll = new GSManageBLL();
        #endregion

        #region 对象初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Page.Request.QueryString["GSID"] != null)
                {
                    ViewState["GSID"] = Page.Request.QueryString["GSID"];
                    GetGS();
                    if (Page.Request.QueryString["HTID"] != null)
                    {
                        ViewState["HTID"] = Page.Request.QueryString["HTID"];
                        IsNew = false;
                        ObjToPage();
                    }
                }
            }
        }
        #endregion

        #region 辅助方法
        //群族信息
        private void GetGS()
        {
            //GatherStrain gs = gsbll.GetGatherStrainByID(GSID);
            //imgGSICQ.ImageUrl = PicUrl(gs.GSICO);
            //labGSName.Text = gs.GSName;
        }

        public string PicUrl(string picUrl)
        {
            string pic = picUrl;

            if (!string.IsNullOrEmpty(pic)) { pic = pic.ToLower(); }
            if (!string.IsNullOrEmpty(pic) && (pic.IndexOf(".gif") > -1 || pic.IndexOf(".jpg") > -1 || pic.IndexOf(".png") > -1))
            {
                string delpath = SiteConfig.SiteOption.UploadDir.Replace("/", "") + "/";
                if (pic.StartsWith("~/"))
                {
                    pic.Replace("~/", "/");
                }

                else if (pic.StartsWith("http://", true, CultureInfo.CurrentCulture) || pic.StartsWith("/", true, CultureInfo.CurrentCulture) || pic.StartsWith(delpath, true, CultureInfo.CurrentCulture))
                    pic = "/" + pic;
                else
                {
                    pic = "/" + delpath + "/" + pic;
                }
            }
            else
            {
                pic = "/UploadFiles/nopic.gif";
            }

            return pic;
        }
        private void ObjToPage()
        {
            //GSHuatee gsHT = gsbll.GetGSHuateeByID(HTID);
            //txtHuaTeeTitle.Text = gsHT.HuaTeeTitle;
            //FreeTextBox1.Value = gsHT.HuaTeeContent;
        }

        private void PageToObj(GSHuatee gsht)
        {
            gsht.GSID = GSID;
            gsht.HuaTeeContent = this.FreeTextBox1.Value;
            gsht.HuaTeeTitle = this.txtHuaTeeTitle.Text;
            gsht.UserID = ubll.GetLogin().UserID;
        }
        #endregion

        #region 页面功能
        protected void btnOK_Click(object sender, EventArgs e)
        {
            GSHuatee gsht = new GSHuatee();
            if (IsNew)
            {
                PageToObj(gsht);
                gsht.ReadCount = 0;
                gsht.RevertCount = 0;
                //HTID = gsbll.CreatGSHuatee(gsht);
            }
            else
            {
                gsht.ID = HTID;
                gsht.HuaTeeContent = this.FreeTextBox1.Value;
                gsht.HuaTeeTitle = this.txtHuaTeeTitle.Text;
                //gsbll.UpdataHT(gsht);
            }
            Page.Response.Redirect("HuaTeeRevert.aspx?GSID=" + GSID + "&HTID=" + HTID);

        }

        protected void btnCal_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("CreatHuaTee.aspx?GSID=" + GSID + "&HTID" + HTID);
        }
        #endregion

     
    }
}
