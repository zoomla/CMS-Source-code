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
using System.IO;
using BDUModel;
using BDUBLL;
using ZoomLa.Model;
using ZoomLa.Components;
using ZoomLa.BLL;
using System.Globalization;
using ZoomLa.Common;

namespace ZoomLa.GatherStrainManage
{
    public partial class FileShareManage : Page
    {
        #region 业务对象
        B_User ubll = new B_User();
        public Guid GSID
        {
            get
            {
                if (ViewState["GSID"] != null)
                {
                    return new Guid(ViewState["GSID"].ToString());
                }
                else
                    return Guid.Empty;
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
        #region 对象初始化
        protected void Page_Load(object sender, EventArgs e)
        {
            Egv.txtFunc = txtPageFunc;
            if (!IsPostBack)
            {
                if (Page.Request.QueryString["GSID"] != null)
                {
                    ViewState["GSID"] = Page.Request.QueryString["GSID"];
                    GetGS();
                    //gsbll.UserCallGS(ubll.GetLogin().UserID, GSID);//更新访问群族时间
                    DataBind();
                }
                //DataBind();
            }
        }
        public void DataBind(string key = "")
        {
            //Egv.DataSource = gsbll.GetFileShareByGSID(GSID, null);
            //Egv.DataBind();
        }
        protected void txtPageFunc(string size)
        {
            int pageSize;
            if (!int.TryParse(size, out pageSize))//如果转换失败,即不是一个数字时
            {
                pageSize = Egv.PageSize;
            }
            else if (pageSize < 1)//小于1时,均恢复默认PageSize,默认PageSize是你给序的
            {
                pageSize = Egv.PageSize;
            }
            Egv.PageSize = pageSize;
            Egv.PageIndex = 0;//改变后回到首页
            size = pageSize.ToString();
            DataBind();
        }
        protected void Egv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Egv.PageIndex = e.NewPageIndex;
            DataBind();
        }
        #endregion
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

            if (!string.IsNullOrEmpty(pic))
            {
                pic = pic.ToLower();
            }
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

        private string useSize(double usesize)
        {
            if (usesize < 1024.00)
            {
                return usesize.ToString() + "字节";
            }
            else
            {
                double size = usesize / 1024;
                return size.ToString("N0") + "兆";
            }
        }
        #endregion
        #region 页面功能
        protected void lbtnDown_Click(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            GridViewRow gr = lb.Parent.Parent as GridViewRow;
            //BDUModel.FileShare file = gsbll.GetFileShareByID(new Guid(this.Egv.DataKeys[gr.RowIndex].Value.ToString()));
            //SafeSC.DownFile(file.FileURL, file.FactFileName);
        }
        #endregion
        protected void lbtnDel_Click(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            GridViewRow gvr = lb.Parent.Parent as GridViewRow;
            //gsbll.DelFile(new Guid(Egv.DataKeys[gvr.RowIndex].Value.ToString()));
            //DataBind();
        }
    }
}
