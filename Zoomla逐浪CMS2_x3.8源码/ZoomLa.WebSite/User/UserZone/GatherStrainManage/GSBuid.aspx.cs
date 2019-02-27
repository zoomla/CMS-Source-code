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
using BDULogic;
using ZoomLa.SQLDAL;
using System.Globalization;
using ZoomLa.Common;
using System.Data.SqlClient;

namespace ZoomLa.GatherStrainManage
{
    public partial class GSBuid : Page
    {
        #region 业务对象
        B_User ubll = new B_User();
        int currentUser = 0;
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

        //GSManageBLL gsbll = new GSManageBLL();


        private Dictionary<string, string> Order
        {
            get
            {
                Dictionary<string, string> ht = new Dictionary<string, string>();
                ht.Add("CreatTime", "0");
                return ht;
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            currentUser = ubll.GetLogin().UserID;
            if (currentUser == 0)
                Page.Response.Redirect(@"~/user/login.aspx");
            if (!IsPostBack)
            {
                if (Page.Request.QueryString["GSID"] != null)
                {
                    ViewState["GSID"] = Page.Request.QueryString["GSID"];
                    GetGS();
                    GetHuaTeeList(null);

                    //gsbll.UserCallGS(currentUser, GSID);//更新访问群族时间
                }
            }
        }
        #region 辅助方法
        //群族信息
        private void GetGS()
        {
            M_UserInfo uinfo = ubll.GetUserByUserID(currentUser);
            //GatherStrain gs = gsbll.GetGatherStrainByID(GSID); 
            //imgGSICQ.ImageUrl = PicUrl(gs.GSICO);
            //txtpic.Text = PicUrl(gs.GSICO);
            //this.TB_Name.Text = gs.GSName;
            //this.LB_name.Text = gs.GSName;
            //labCreattime.Text = gs.CreatTime.ToString("yyyy-MM-dd");
            //labInfo.Text = gs.GSIntro;
            //TB_Info.Text = gs.GSIntro;
            a_talk.HRef = "CreatHuaTee.aspx?GSID=" + GSID + "&where=5";
        }
        //获取话题列表
        private void GetHuaTeeList(BDUModel.PagePagination pagepag)
        {
            BDUModel.PagePagination newpage = new BDUModel.PagePagination();
            newpage.PageOrder = Order;　 
            //newpage.PageSize = this.AspNetPager1.PageSize;
            if (pagepag != null)
            {
                newpage.PageIndex = pagepag.PageIndex;
            }
            else newpage.PageIndex = 1;
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
        #region 删除群族
        protected void lbtnDeleteGS_Click(object sender, EventArgs e)
        {
            //gsbll.DeleteGS(GSID);
            Page.Response.Redirect("GSManage.aspx?where=5");
        }
        #endregion
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
        #region 修改备注
        protected void LK_rename_Click1(object sender, EventArgs e)
        {
            LB_name.Visible = false;
            TB_Name.Visible = true;
            lab1.Visible = true;
            TB_Info.Visible = true;
            LK_rename.Visible = false;
            LK_sure.Visible = true;
            labInfo.Visible = false;

        }
        protected void LK_sure_Click(object sender, EventArgs e)
        {

            string name = TB_Name.Text;
            SqlParameter[] sp = new SqlParameter[] { new SqlParameter("GSName", name), new SqlParameter("GSIntro", TB_Info.Text.Trim()), new SqlParameter("GSICO", txtpic.Text.Trim()), new SqlParameter("ID", GSID) };
          
            LB_name.Visible = true;
            TB_Name.Visible = false;
            LK_rename.Visible = true;
            labInfo.Visible = true;
            LK_sure.Visible = false;
            lab1.Visible = false;
            TB_Info.Visible = false;
            //GatherStrain gs = gsbll.GetGatherStrainByID(GSID);
            //LB_name.Text = gs.GSName;
            //labInfo.Text = gs.GSIntro;
            //imgGSICQ.ImageUrl = PicUrl(gs.GSICO);
        }
        #endregion
    }
}
