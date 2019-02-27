using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using BDUModel;
using BDUBLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL;
using System.Globalization;

namespace ZoomLa.GatherStrainManage
{
    public partial class CreatHuaTee : Page
    {
        #region 业务对象
        B_User ubll = new B_User();
        public  Guid GSID
        {
            get
            {
                if (ViewState["GSID"] != null)
                {
                    try
                    {
                        return new Guid(ViewState["GSID"].ToString());
                    }
                    catch
                    {
                        return Guid.Empty;
                    }
                }
                else return Guid.Empty;
            }
            set
            {
                ViewState["GSID"] = value;
            }
        }
        private Dictionary<string, string> Order
        {
            get
            {
                Dictionary<string, string> ht = new Dictionary<string, string>();
                ht.Add("CreatTime", "0");
                return ht;
            }
        }
        //GSManageBLL gsbll = new GSManageBLL();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        { 
            ubll.CheckIsLogin();
            if (!IsPostBack)
            {
                if (Page.Request.QueryString["GSID"] != null)
                {
                    ViewState["GSID"] = Page.Request.QueryString["GSID"];
                    GetGS();
                    GetHuaTeeList(null);

                    //gsbll.UserCallGS(ubll.GetLogin().UserID, GSID);//更新访问群族时间
                }
            }
        }
        #region 辅助方法
        //群族信息
        private void GetGS()
        {
            M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
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
        //获取话题列表
        private void GetHuaTeeList(BDUModel.PagePagination pagepag)
        {
            BDUModel.PagePagination newpage = new BDUModel.PagePagination();
            newpage.PageOrder = Order;
            //List<GSHuatee> list = gsbll.GetGSHuateeByGSID(GSID, newpage);
            //this.gvHuaTee.DataSource = list;
            //this.gvHuaTee.DataBind();
            
        }
        protected void gvHuaTee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == new DateTime().ToString())
                {
                    e.Row.Cells[3].Text = "";
                }
            }
        }
        #endregion
    }
}
