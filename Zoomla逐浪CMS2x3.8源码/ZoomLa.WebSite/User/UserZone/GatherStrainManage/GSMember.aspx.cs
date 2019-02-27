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
using ZoomLa.BLL;
using ZoomLa.Model;
using ZoomLa.Components;
using System.Globalization;

namespace ZoomLa.GatherStrainManage
{
    public partial class GSMember : Page
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
                else return Guid.Empty;
            }
            set
            {
                ViewState["GSID"] = value;
            }
        }

        //GSManageBLL gsbll = new GSManageBLL();
        #endregion

        #region 对象初始化
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
            {
                M_UserInfo uinfo = ubll.GetUserByUserID(ubll.GetLogin().UserID);
                if (Page.Request.QueryString["GSID"] != null)
                {
                    ViewState["GSID"] = Page.Request.QueryString["GSID"];
                    GetGS();
                    GetGSMember();

                    //gsbll.UserCallGS(ubll.GetLogin().UserID, GSID);//更新访问群族时间
                }
            }
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
        private void GetGSMember()
        {
            //List<User_R_GS> list = gsbll.GetUserMemberByGSID(GSID);
            //this.dlMember.DataSource = list;
            //this.dlMember.DataBind();
        }

        protected void gvMember_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }

        protected void dlMember_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item)
            {
                Label lb = e.Item.FindControl("Label2") as Label;
                if(lb !=null)
                {
                    if (lb.Text == new DateTime().ToString())
                        lb.Text = "未访问";
                }
            }
        }
        #endregion

        protected void lbtnAll_Click(object sender, EventArgs e)
        {
            GetGSMember();
            this.lbtnAll.Enabled = false;
            this.lbtnCome.Enabled = true;
            this.lbtnNewP.Enabled = true;
        }

        protected void lbtnNewP_Click(object sender, EventArgs e)
        {
            List<User_R_GS> list1 = new List<User_R_GS>();
            //List<User_R_GS> list = gsbll.GetUserMemberByGSID(GSID);
            //foreach (User_R_GS ur in list)
            //{
            //    if (ur.CreateTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
            //    {
            //        list1.Add(ur);
            //    }
            //}
            //this.dlMember.DataSource = list1;
            //this.dlMember.DataBind();
            this.lbtnAll.Enabled = true;
            this.lbtnCome.Enabled = true;
            this.lbtnNewP.Enabled = false;
        }

        protected void lbtnCome_Click(object sender, EventArgs e)
        {
            List<User_R_GS> list1 = new List<User_R_GS>();
            //List<User_R_GS> list = gsbll.GetUserMemberByGSID(GSID);
            //foreach (User_R_GS ur in list)
            //{
            //    if (ur.LastCallTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
            //    {
            //        list1.Add(ur);
            //    }
            //}
            this.dlMember.DataSource = list1;
            this.dlMember.DataBind();
            this.lbtnAll.Enabled = true;
            this.lbtnCome.Enabled = false;
            this.lbtnNewP.Enabled = true;
        }

       

      
    }
}
